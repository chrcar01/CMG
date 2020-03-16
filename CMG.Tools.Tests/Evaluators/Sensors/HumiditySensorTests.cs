using System.Collections.Generic;
using CMG.Tools.Evaluators.Sensors;
using NUnit.Framework;

namespace CMG.Tools.Tests.Evaluators.Sensors
{
    [TestFixture]
    public class HumiditySensorTests
    {
        [Test]
        public void NameIsSetCorrectly()
        {
            var sensor = new HumiditySensor("sensor1", 50m);
            Assert.That(sensor.Name, Is.EqualTo("sensor1"));
        }

        [Test]
        public void ShouldKeepSensor_WhenAllValuesWithinOnePercentOfReference()
        {
            var sensor = new HumiditySensor("myHumiditySensor", 50m);
            sensor.AddReading("50.1");
            sensor.AddReading("49.2");
            Assert.That(sensor.GetStatus(), Is.EqualTo("keep"));
        }

        [Test]
        public void ShouldDiscardSensor_WhenAnyReadingGreaterThanOrEqualToOne()
        {
            var sensor = new HumiditySensor("myHumiditySensor", 50m);
            sensor.AddReading("50.1");
            sensor.AddReading("49.2");
            sensor.AddReading("49");
            Assert.That(sensor.GetStatus(), Is.EqualTo("discard"));
        }
    }

    
}
