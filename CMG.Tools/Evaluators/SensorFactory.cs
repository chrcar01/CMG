using CMG.Tools.Evaluators.Interfaces;
using CMG.Tools.Evaluators.Sensors;
using System;
using System.Collections.Generic;

namespace CMG.Tools.Evaluators
{
    public class SensorFactory : ISensorFactory
    {
        private readonly ICalculate _calculator;
        public IEnumerable<string> SensorNames => _sensorBuilders.Keys;
        public SensorFactory(ICalculate calculator)
        {
            _calculator = calculator ?? throw new ArgumentNullException(nameof(calculator));
        }

        private readonly Dictionary<string, Func<string, IDictionary<string, double>, ICalculate, Sensor>> _sensorBuilders 
            = new Dictionary<string, Func<string, IDictionary<string, double>, ICalculate, Sensor>>();
        
        public ISensorFactory RegisterSensor(string type, Func<string, IDictionary<string, double>, ICalculate, Sensor> builder)
        {
            if (String.IsNullOrWhiteSpace(type)) throw new ArgumentNullException(type);
            if (builder == null) throw new ArgumentNullException(nameof(builder));
            _sensorBuilders.Add(type, builder);
            return this;
        }

        public Sensor Create(IDictionary<string, double> referenceValues, string type, string name)
        {
            if (referenceValues == null) throw new ArgumentNullException(nameof(referenceValues));
            if (String.IsNullOrWhiteSpace(type)) throw new ArgumentNullException(nameof(type));
            if (String.IsNullOrWhiteSpace(name)) throw new ArgumentNullException(nameof(name));
            return _sensorBuilders[type](name, referenceValues, _calculator);
        }


    }
}
