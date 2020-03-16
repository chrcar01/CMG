using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using CMG.Tools.Evaluators;
using CMG.Tools.Evaluators.Interfaces;
using CMG.Tools.Evaluators.Sensors;
using Moq;
using NUnit.Framework;

namespace CMG.Tools.Tests.Evaluators.Sensors
{
    [TestFixture]
    public class ThermometerSensorTests
    {
        [Test]
        public void MakeSureTheNameWorks()
        {
            Assert.That(new ThermometerSensor("thermo1", 70d, Mock.Of<ICalculate>()).Name, Is.EqualTo("thermo1"));
        }

        [TestCaseSource(typeof(ThermometerTestCases))]
        public void VerifyStatus(
            double mean,
            double meanDeviation,
            double referenceTemperature,
            double standardDeviation,
            string expectedStatus,
            IEnumerable<double> readings)
        {
            var calculatorMock = new Mock<ICalculate>(MockBehavior.Strict);
            calculatorMock
                .Setup(x => x.Mean(It.IsAny<IEnumerable<double>>()))
                .Returns(mean);

            calculatorMock
                .Setup(x => x.StandardDeviation(It.Is<IEnumerable<double>>(p => p.Contains(referenceTemperature) && p.Contains(mean))))
                .Returns(meanDeviation);

            calculatorMock
                .Setup(x => x.StandardDeviation(It.Is<IEnumerable<double>>(p => readings.All(p.Contains))))
                .Returns(standardDeviation);

            var sensor = new ThermometerSensor("thermo1", referenceTemperature, calculatorMock.Object);
            foreach (var reading in readings)
            {
                sensor.AddReading(reading.ToString(CultureInfo.InvariantCulture));
            }

            Assert.That(sensor.GetStatus(), Is.EqualTo(expectedStatus));
        }

        public class ThermometerTestCases : IEnumerable
        {
            public IEnumerator GetEnumerator()
            {
                // should return: mean, meanDev, referenceTemp, standardDev, expectedStatus, readings
                yield return new TestCaseData(70.1, .4, 70.5, 2, "ultra precise", new[] { 70.2, 70.7 }).SetName("ShouldBeUltraPrecise");
                yield return new TestCaseData(70.1, .4, 70.5, 3, "very precise", new[] { 69.2, 70.7 }).SetName("ShouldBeVeryPrecise");
                yield return new TestCaseData(70.1, .4, 70.5, 5, "precise", new[] { 69.2, 70.7 }).SetName("ShouldBePrecise");
            }
        }
    }


}
