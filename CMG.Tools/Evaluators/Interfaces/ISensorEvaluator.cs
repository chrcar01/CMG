namespace CMG.Tools.Evaluators.Interfaces
{
    /// <summary>
    /// Defines the interface for evaluating sensor test results.
    /// </summary>
    public interface ISensorEvaluator
    {
        /// <summary>
        /// Evaluates the sensor data from a log file.
        /// </summary>
        /// <param name="logFile">Content of a log file.</param>
        /// <returns>Evaluated results for the sensors in the file.</returns>
        string Evaluate(string logFile);
    }
}
