using System;
using System.Collections.Generic;
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

            var reference = "reference".AsSpan();
            var span = content.AsSpan();
            var length = 0;
            var start = 0;
            length = content.IndexOf(Environment.NewLine, start, StringComparison.Ordinal) - start;
            var line = content.Substring(start, length);
            var spanLine = span.Slice(start, length);
            if (spanLine.StartsWith(reference))
            {
                
            }
            Console.WriteLine(line);
            while (start < content.Length)
            {
                start += length + Environment.NewLine.Length;
                length = content.IndexOf(Environment.NewLine, start, StringComparison.Ordinal) - start;
                if (length <= 0)
                {
                    break;
                }
                line = content.Substring(start, length);
                Console.WriteLine(line);
            }
            return result;
        }
    }
}