using CMG.Tools.Evaluators.Sensors;
using System;
using System.Collections.Generic;

namespace CMG.SensorWebApi.Sensors
{
    /// <summary>
    /// Just an example of creating a new kind of Sensor that only exists in this app.
    /// </summary>
    public class ChaosSensor : Sensor
    {
        private readonly List<decimal> _readings = new List<decimal>();
        public ChaosSensor(string name) : base(name)
        {
        }

        public override string GetStatus()
        {
            return $"chaos-{_readings.Count}";
        }

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
