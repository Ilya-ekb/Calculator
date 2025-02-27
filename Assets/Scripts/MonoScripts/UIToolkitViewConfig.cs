using CalculatorViews;
using UnityEngine;
using UnityEngine.UIElements;

namespace MonoScripts
{
    [CreateAssetMenu(menuName = nameof(CalculatorUIToolkitView), fileName = nameof(CalculatorUIToolkitView))]
    public class UIToolkitViewConfig : ViewConfig
    {
        [SerializeField] private VisualTreeAsset template;
        [SerializeField] private PanelSettings panelSettings;
        public override ICalculatorView GetView()
        {
            if (!template || !panelSettings)
            {
                Debug.LogError($"{nameof(UIToolkitViewConfig)} is not set");
                return default;
            }
            var view = new GameObject("UI");
            var uiDoc = view.AddComponent<UIDocument>();
            uiDoc.panelSettings = panelSettings;
            template.CloneTree(uiDoc.rootVisualElement);
            return view.AddComponent<CalculatorUIToolkitView>();
        }
    }
}