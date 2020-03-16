using System;
using System.Collections.Generic;
using System.Linq;

namespace CMG.Tools.Evaluators.Sensors
{
    public class MonoxideSensor : Sensor
    {
        private readonly int _referencePpm;
        private readonly List<int> _readings = new List<int>();
        public MonoxideSensor(string name, int referencePpm) : base(name)
        {
            _referencePpm = referencePpm;
        }

        public override string GetStatus() => _readings.All(r => (int)_referencePpm - r < 3) ? "keep" : "discard";


        public override void AddReading(string value)
        {
            if (Int32.TryParse(value, out var result))
            {
                _readings.Add(result);
            }
        }

        public override void AddReading(ReadOnlySpan<char> value)
        {
            if (Int32.TryParse(value, out var result))
            {
                _readings.Add(result);
            }
        }
    }
}
