using CMG.Tools.Evaluators.Sensors;
using System.Collections.Generic;

namespace CMG.Tools.Evaluators.Interfaces
{
    /// <summary>
    /// Defines the methods used for parsing log files.
    /// </summary>
    public interface ISensorLogParser
    {
        /// <summary>
        /// Extracts sensor data from the contents of a log file.
        /// </summary>
        /// <param name="content">Content of the log file.</param>
        /// <returns>List of sensors loaded with test readings.</returns>
        IEnumerable<Sensor> Parse(string content);
    }
}
