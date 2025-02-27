using CalculatorModels;
using UnityEngine;

namespace MonoScripts
{
    [CreateAssetMenu(menuName = nameof(OnlyAddCalculator), fileName = nameof(OnlyAddCalculator))]
    public class OnlyAddModel : ModelConfig
    {
        public override ICalculatorModel GetModel()
        {
            return new OnlyAddCalculator();
        }
    }
}