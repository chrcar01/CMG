using System;
using System.Collections.Generic;
using System.Linq;

namespace CMG.Tools.Evaluators.Sensors
{
    public class HumiditySensor : Sensor
    {
        private readonly decimal _referenceHumidity;

        public HumiditySensor(string name, decimal referenceHumidity) : base(name)
        {
            _referenceHumidity = referenceHumidity;
        }

        private readonly List<decimal> _readings = new List<decimal>();

        public override string GetStatus() => _readings.All(r => _referenceHumidity - r < 1m) ? "keep" : "discard";

        public override void AddReading(string value)
        {
            if (Decimal.TryParse(value, out var result))
            {
                _readings.Add(result);
            }
        }

        public override void AddReading(ReadOnlySpan<char> value)
        {
            if (Decimal.TryParse(value, out var result))
            {
                _readings.Add(result);
            }
        }
    }
}
