using System.Collections.Generic;

namespace CMG.Tools.Evaluators.Interfaces
{
    public interface ICalculate
    {
        double StandardDeviation(IEnumerable<double> values);
        double Mean(IEnumerable<double> values);
    }
}
