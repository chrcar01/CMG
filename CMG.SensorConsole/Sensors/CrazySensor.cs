using CMG.Tools.Evaluators.Sensors;
using System;
using System.Collections.Generic;

namespace CMG.SensorConsole.Sensors
{
    /// <summary>
    /// Just an example of creating a new kind of Sensor that only exists in this app.
    /// </summary>
    public class CrazySensor : Sensor
    {
        private readonly List<double> _readings = new List<double>();
        public CrazySensor(string name) : base(name)
        {
        }

        public override string GetStatus()
        {
            return _readings.Count % 2 == 0 ? "even" : "odd";
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
