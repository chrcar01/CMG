using System;
using System.Collections.Generic;
using CMG.Tools.Evaluators.Interfaces;

namespace CMG.Tools.Evaluators.Sensors
{
    public class ThermometerSensor : Sensor
    {
        private readonly ICalculate _calculator;
        private readonly double _referenceTemperature;
        public ThermometerSensor(string name, double referenceTemperature, ICalculate calculator) : base(name)
        {
            _referenceTemperature = referenceTemperature;
            _calculator = calculator;
        }

        private readonly List<double> _readings = new List<double>();
        public override string GetStatus()
        {
            var mean = _calculator.Mean(_readings);
            var meanDev = _calculator.StandardDeviation(new[] { _referenceTemperature, mean });
            var stdDev = _calculator.StandardDeviation(_readings);

            if (meanDev < 0.5 && stdDev < 3)
            {
                return "ultra precise";
            }

            if (meanDev < 0.5 && stdDev >= 3 && stdDev < 5)
            {
                return "very precise";
            }

            return "precise";
        }

        public override void AddReading(string value)
        {
            if (Double.TryParse(value, out var result))
            {
                _readings.Add(result);
            }
        }

        public override void AddReading(ReadOnlySpan<char> value)
        {
            if (Double.TryParse(value, out var result))
            {
                _readings.Add(result);
            }
        }
    }
}