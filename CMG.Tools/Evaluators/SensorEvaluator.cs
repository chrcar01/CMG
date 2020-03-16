using CMG.Tools.Evaluators.Interfaces;
using System;
using System.Linq;
using System.Text.Json;

namespace CMG.Tools.Evaluators
{
    public class SensorEvaluator : ISensorEvaluator
    {
        private readonly ISensorLogParser _logParser;

        public SensorEvaluator(ISensorLogParser logParser)
        {
            _logParser = logParser;
        }

        public string Evaluate(string logFile)
        {
            var sensors = _logParser.Parse(logFile)?.ToDictionary(k => k.Name, v => v.GetStatus());
            return JsonSerializer.Serialize(sensors, new JsonSerializerOptions { WriteIndented = true });
        }

        private static ISensorEvaluator _evaluator;
        /// <summary>
        /// Old school dependency injection, this allows us to actually use the container to construct an
        /// instance of SensorEvaluator to send in.
        /// The Initialize method is handled in the ServiceCollectionExtensions in the AddSensorEvaluator extension method.
        /// </summary>
        /// <param name="evaluator"></param>
        public static void Initialize(ISensorEvaluator evaluator)
        {
            _evaluator = evaluator ?? throw new ArgumentNullException(nameof(evaluator));
        }

        /// <summary>
        /// This is the interface requested in the Audition notes.  All this does is wrap a call to the instance
        /// setup in the Initialize method.
        /// </summary>
        /// <param name="logContentsStr"></param>
        /// <returns></returns>
        public static string EvaluateLogFile(string logContentsStr)
        {
            if (_evaluator == null) throw new InvalidOperationException($"Evaluator instance is null, did you forget to call Initialize?");
            return _evaluator.Evaluate(logContentsStr);
        }
    }
}
