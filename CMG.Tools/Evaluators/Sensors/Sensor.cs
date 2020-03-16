using System;
using System.Collections.Generic;

namespace CMG.Tools.Evaluators.Sensors
{
    public abstract class Sensor
    {
        public string Name { get; }

        protected Sensor(string name)
        {
            Name = name;
        }

        public abstract string GetStatus();
        public abstract void AddReading(string value);
        public abstract void AddReading(ReadOnlySpan<char> value);
    }
}
