using CMG.Tools.Evaluators.Sensors;
using NUnit.Framework;

namespace CMG.Tools.Tests.Evaluators.Sensors
{
    [TestFixture]
    public class MonoxideSensorTests
    {
        [Test]
        public void MakeSureNameIsCorrect()
        {
            Assert.That(new MonoxideSensor("mono1", 4).Name, Is.EqualTo("mono1"));
        }

        [Test]
        public void ShouldKeep_WhenAllReadingsWithin3PpmOfReference()
        {
            var sensor = new MonoxideSensor("monoxide1", 5);
            sensor.AddReading("4");
            sensor.AddReading("7");
            sensor.AddReading("3");
            Assert.That(sensor.GetStatus(), Is.EqualTo("keep"));
        }
        [Test]
        public void ShouldDiscard_WhenAnyReadingGreaterThanOrEqualTo3PpmOfReference()
        {
            var sensor = new MonoxideSensor("monoxide1", 5);
            sensor.AddReading("4");
            sensor.AddReading("7");
            sensor.AddReading("2");
            Assert.That(sensor.GetStatus(), Is.EqualTo("discard"));
        }

    }
}
