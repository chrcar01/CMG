using CMG.Tools.Evaluators.Interfaces;
using CMG.Tools.Evaluators.Sensors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMG.Tools.Evaluators
{
    /// <summary>
    /// String based parser.  This one is considered MVP.  The SpanSensorLogParser should be considered top priority for production.
    /// </summary>
    public class StringSensorLogParser : ISensorLogParser
    {
        private readonly ISensorFactory _sensorFactory;

        public StringSensorLogParser(ISensorFactory sensorFactory)
        {
            _sensorFactory = sensorFactory;
        }

        public IEnumerable<Sensor> Parse(string content)
        {
            if (String.IsNullOrWhiteSpace(content))
            {
                return new Sensor[0];
            }

            var result = new List<Sensor>();
            var referenceValues = new Dictionary<string, double>();
            Sensor currentSensor = null;

            foreach (var line in content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
            {
                var lineParts = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                
                if (line.StartsWith("reference"))
                {
                    referenceValues.Clear();
                    referenceValues.Add("temperature", Convert.ToDouble(lineParts[1]));
                    referenceValues.Add("humidity", Convert.ToDouble(lineParts[2]));
                    referenceValues.Add("ppm", Convert.ToDouble(lineParts[3]));
                    continue;
                }

                if (_sensorFactory.SensorNames.Contains(lineParts[0]))
                {
                    currentSensor = _sensorFactory.Create(referenceValues, lineParts[0], lineParts[1]);
                    result.Add(currentSensor);
                    continue;
                }

                currentSensor?.AddReading(lineParts[1]);
            }
            
            return result;
        }
    }
}
