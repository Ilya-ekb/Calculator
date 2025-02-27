using CalculatorViews;
using UnityEngine;

namespace MonoScripts
{
    public abstract class ViewConfig : ScriptableObject
    {
        public abstract ICalculatorView GetView();
    }
}