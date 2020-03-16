using CMG.Tools.Evaluators;
using CMG.Tools.Evaluators.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text.Json;

namespace CMG.Tools.Tests.Evaluators
{
    [TestFixture]
    public class SensorEvaluatorTests
    {

        [Test]
        public void Evaluate_ShouldReturnTheSensorsReturnedFromParserAsJsonDictionaryString()
        {
            const string LOG_FILE_CONTENT = "blah blah blah";
            var mockParser = new Mock<ISensorLogParser>(MockBehavior.Strict);
            mockParser
                .Setup(s => s.Parse(It.Is<string>(p => p == LOG_FILE_CONTENT)))
                .Returns(new[] {new TestSensor("killer", "bee")});

            var evaluator = CreateEvaluator(mockParser.Object);
            var result = evaluator.Evaluate(LOG_FILE_CONTENT);
            var obj = JsonSerializer.Deserialize<IDictionary<string, string>>(result);
            Assert.That(obj.ContainsKey("killer"));
            Assert.That(obj["killer"], Is.EqualTo("bee"));
        }

        [Test]
        public void StaticEvaluateLogFile_ShouldCallEvaluateOnInstancePassedToInitialize()
        {
            // Adding the following assertion since we're testing static behavior, spreading the tests
            // around caused random success so putting this one here to ensure behavior
            Assert.That(() => SensorEvaluator.EvaluateLogFile("blah"), Throws.InvalidOperationException);

            const string HELLO_WORLD = "hello world";
            var mockSensor = new Mock<ISensorEvaluator>(MockBehavior.Strict);
            mockSensor
                .Setup(s => s.Evaluate(It.Is<string>(p => p == HELLO_WORLD)))
                .Returns(HELLO_WORLD);

            SensorEvaluator.Initialize(mockSensor.Object);
            var actual = SensorEvaluator.EvaluateLogFile(HELLO_WORLD);
            Assert.That(actual, Is.EqualTo(HELLO_WORLD));
            mockSensor.Verify(s => s.Evaluate(It.Is<string>(p => p == HELLO_WORLD)), Times.Once);
        }

        
        private ISensorEvaluator CreateEvaluator(ISensorLogParser logParser = null)
        {
            return new SensorEvaluator(logParser ?? Mock.Of<ISensorLogParser>());
        }
    }
}
