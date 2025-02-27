using CalculatorPresenters;
using UnityEngine;

namespace MonoScripts
{
    [CreateAssetMenu(menuName = nameof(CalculatorPresenter), fileName = nameof(CalculatorPresenter))]
    public class CalculatorPresenterConfig : ScriptableObject
    {
        [SerializeField] private ModelConfig model;
        [SerializeField] private StorageConfig storage;
        [SerializeField] private ViewConfig view;
    
        public CalculatorPresenter GetInstance()
        {
            if (!model || !storage || !view)
            {
                Debug.LogError($"{nameof(CalculatorPresenterConfig)} is not set");
                return null;
            }
            var modelIns = model.GetModel();
            var storageIns = storage.GetStorage();
            var viewIns = view.GetView();
            if (modelIns is null || storageIns is null || viewIns is null)
            {
                Debug.LogError($"{nameof(CalculatorPresenterConfig)} is not set");
                return null;
            }
        
            return new CalculatorPresenter(modelIns, storageIns, viewIns);
        }
    }
}