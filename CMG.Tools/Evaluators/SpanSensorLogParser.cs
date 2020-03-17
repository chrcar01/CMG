using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CMG.Tools.Evaluators.Interfaces;
using CMG.Tools.Evaluators.Sensors;

namespace CMG.Tools.Evaluators
{
    /// <summary>
    /// WIP.  This parser would minimize the number of string allocations by scanning the one string using ReadOnlySpans.
    /// </summary>
    public class SpanSensorLogParser : ISensorLogParser
    {
        private readonly ISensorFactory _sensorFactory;

        public SpanSensorLogParser(ISensorFactory sensorFactory)
        {
            _sensorFactory = sensorFactory;
        }

        public IEnumerable<Sensor> Parse(string content)
        {
            var result = new List<Sensor>();
            if (String.IsNullOrWhiteSpace(content))
            {
                return result;
            }
            
            var start = 0;
            IDictionary<string, double> refValues = null;
            Sensor currentSensor = null;
            while (true)
            {
                var reading = true;
                var length = content.IndexOf(Environment.NewLine, start, StringComparison.Ordinal) - start;
                if (length <= 0)
                {
                    break;
                }
                var line = content.AsSpan(start, length);
                var firstChunk = line.Slice(0, line.IndexOf(' '));
                if (firstChunk.SequenceEqual("reference"))
                {
                    refValues = ExtractReferenceValues(line.Slice(firstChunk.Length, line.Length - firstChunk.Length));
                    if (refValues == null || refValues.Count != 3)
                    {
                        throw new InvalidOperationException($"Invalid values for refValues, should have had 3 but had {refValues.Count}");
                    }
                    reading = false;
                }

                if (refValues != null && refValues.Count == 3)
                {
                    foreach (var name in _sensorFactory.SensorNames)
                    {
                        if (firstChunk.SequenceEqual(name.AsSpan()))
                        {
                            var sensorName = line.Slice(firstChunk.Length, line.Length - firstChunk.Length);
                            currentSensor = _sensorFactory.Create(refValues, firstChunk.Trim().ToString(), sensorName.Trim().ToString());
                            result.Add(currentSensor);
                            reading = false;
                            break;
                        }
                    }
                }

                if (reading)
                {
                    currentSensor?.AddReading(line.Slice(firstChunk.Length, line.Length - firstChunk.Length));
                }
                start += length + Environment.NewLine.Length;
            }
            return result;
        }

        public IDictionary<string, double> ExtractReferenceValues(ReadOnlySpan<char> line)
        {
            line = line.Slice(1);
            var result = new Dictionary<string, double>();
            var values = new[] {"temperature", "humidity", "ppm"};
            var valueIndex = 0;
            while (true)
            {
                var length = line.IndexOf(' ');
                if (length <= 0)
                {
                    length = line.Length;
                }
                var chunk = line.Slice(0, length);
                if (Double.TryParse(chunk, out var d))
                {
                    result.Add(values[valueIndex], d);
                    valueIndex++;
                }

                var start = chunk.Length + 1;
                if (start > line.Length)
                {
                    break;
                }
                line = line.Slice(start);
                
            }

            return result;
        }
    }
}