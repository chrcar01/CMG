using System.Collections.Generic;
using System.Linq;
using CMG.Tools.Evaluators;
using CMG.Tools.Evaluators.Interfaces;
using Moq;
using NUnit.Framework;

namespace CMG.Tools.Tests.Evaluators
{
    [TestFixture]
    public class SensorFactoryTests
    {
        private ISensorFactory CreateFactory(ICalculate calculator = null)
        {
            return new SensorFactory(calculator ?? Mock.Of<ICalculate>());
        }

        [Test]
        public void CanRegisterAndCreateSensor()
        {
            var factory = CreateFactory();
            factory.RegisterSensor("awesome", (name, refs, calc) => new TestSensor(name, "jelly"));
            Assert.That(factory.SensorNames.Count(), Is.EqualTo(1));
            Assert.That(factory.SensorNames.Contains("awesome"), Is.True);
            var awesome = factory.Create(new Dictionary<string, double>(), "awesome", "green");
            Assert.That(awesome.Name, Is.EqualTo("green"));
            Assert.That(awesome.GetStatus(), Is.EqualTo("jelly"));
        }
    }
}
