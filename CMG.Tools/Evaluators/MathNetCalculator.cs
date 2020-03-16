using CMG.Tools.Evaluators.Interfaces;
using MathNet.Numerics.Statistics;
using System.Collections.Generic;

namespace CMG.Tools.Evaluators
{
    public class MathNetCalculator : ICalculate
    {
        public double StandardDeviation(IEnumerable<double> values)
        {
            return values.PopulationStandardDeviation();
        }

        public double Mean(IEnumerable<double> values)
        {
            return values.Mean();
        }
    }
}
