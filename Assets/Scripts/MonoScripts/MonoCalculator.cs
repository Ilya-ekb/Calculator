using CalculatorPresenters;
using UnityEngine;

namespace MonoScripts
{
    public class MonoCalculator : MonoBehaviour
    {
        [SerializeField] private CalculatorPresenterConfig presenterConfig;
        private CalculatorPresenter presenter;

        
        private void OnEnable()
        {
            presenter = presenterConfig.GetInstance();
            presenter.SetAlive();
        }
        

        private void OnDisable()
        {
            presenter.Drop();
            presenter = null;
        }
    }
}