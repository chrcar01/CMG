using System;
using System.Collections.Generic;
using CMG.Tools.Evaluators.Sensors;

namespace CMG.Tools.Evaluators.Interfaces
{
    public interface ISensorFactory
    {
        IEnumerable<string> SensorNames { get; }
        Sensor Create(IDictionary<string, double> referenceValues, string type, string name);
        ISensorFactory RegisterSensor(string type, Func<string, IDictionary<string, double>, ICalculate, Sensor> builder);
    }
}