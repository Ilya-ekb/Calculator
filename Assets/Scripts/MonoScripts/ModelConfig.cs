using CalculatorModels;
using UnityEngine;

namespace MonoScripts
{
    public abstract class ModelConfig : ScriptableObject
    {
        public abstract ICalculatorModel GetModel();
    }
}