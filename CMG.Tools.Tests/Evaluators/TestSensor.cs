using System;
using CMG.Tools.Evaluators.Sensors;

namespace CMG.Tools.Tests.Evaluators
{
    public class TestSensor : Sensor
    {
        private readonly string _expectedStatus;

        public TestSensor(string name, string expectedStatus = "keep") : base(name)
        {
            _expectedStatus = expectedStatus;
        }

        public override string GetStatus() => _expectedStatus;


        public override void AddReading(string value)
        {
            // do nothing
        }

        public override void AddReading(ReadOnlySpan<char> value)
        {
            // do nothing
        }
    }
}