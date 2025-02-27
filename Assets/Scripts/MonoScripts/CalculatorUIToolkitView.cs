using UnityEngine;
using UnityEngine.UIElements;

namespace CalculatorViews
{
    public class CalculatorUIToolkitView : BaseCalculatorView
    {
        private UIDocument uiDocument;
        private Label resultLabel;
        private TextField inputField;
        private Button executeButton;
        private VisualElement errorPopup;
        private Button closeErrorPopupButton;
        private Button clearHistoryButton;

        private const string showErrorPopupName = "show-error-popup";

        protected override void OnEnable()
        {
            uiDocument = GetComponent<UIDocument>();
            if(IsExistWarning(uiDocument)) return;
            
            var root = uiDocument.rootVisualElement;
            resultLabel = new Label
            {
                name = "result-label"
            };
            resultLabel.AddToClassList("result-label");
            root.Q<ScrollView>("result")?.contentContainer.Add(resultLabel);

            inputField = root.Q<TextField>("input-field");
            executeButton = root.Q<Button>("execute-button");
            errorPopup = root.Q<VisualElement>("error-popup");
            closeErrorPopupButton = root.Q<Button>("close-error-popup-button");
            clearHistoryButton = root.Q<Button>("clear-history-button");
            isAlive = !IsExistWarning(inputField) &&
                      !IsExistWarning(executeButton) &&
                      !IsExistWarning(errorPopup) &&
                      !IsExistWarning(closeErrorPopupButton) &&
                      !IsExistWarning(clearHistoryButton);
            if (!isAlive) return;

            inputField.RegisterValueChangedCallback(ChangeValueEvent); 
            executeButton.RegisterCallback<ClickEvent>(OnExecuteClick);
            closeErrorPopupButton.RegisterCallback<ClickEvent>(ClickOnErrorPopup);
            clearHistoryButton.RegisterCallback<ClickEvent>(ClearHistoryClick);
            OnHideErrorPopup();
        }

        protected override void OnDisable()
        {
            if(!isAlive) return;
            clearHistoryButton.UnregisterCallback<ClickEvent>(ClearHistoryClick);
            closeErrorPopupButton.UnregisterCallback<ClickEvent>(ClickOnErrorPopup);
            executeButton.UnregisterCallback<ClickEvent>(OnExecuteClick);
            inputField.UnregisterValueChangedCallback(ChangeValueEvent);
            resultLabel = null;
            inputField = null;
            errorPopup = null;
            closeErrorPopupButton = null;
            clearHistoryButton = null;
        }


        protected override void OnSetInput(string input)
        {
            base.OnSetInput(input);
            inputField.value = input;
        }

        protected override string OnGetInput() => inputField.value;
        
        protected override void OnOutput(string result) 
        {
            base.OnOutput(result);
            resultLabel.text = result + "\n" + resultLabel.text;
        }

        protected override void OnDisplayHistory(string history)
        {
            base.OnDisplayHistory(history);
            if (resultLabel is null) return;
            resultLabel.text = history;
        }

        protected override void OnChangeInputValue(string value)
        {
            base.OnChangeInputValue(value);
            OnUpdateExecuteButton(value);
        }

        protected override void OnExecuteClick<T>(T evt)
        {
            base.OnExecuteClick(evt);
            OnUpdateExecuteButton(lastExecutedInput);
        }

        protected override void OnUpdateExecuteButton(string input)
        {
            base.OnUpdateExecuteButton(input);
            executeButton.enabledSelf = !HasEmptyOrSameValue(input);
        }

        protected override void OnShowErrorPopup()
        {
            base.OnShowErrorPopup();
            errorPopup.AddToClassList(showErrorPopupName);
        }

        protected override void OnClearInputField()
        {
            base.OnClearInputField();
            inputField.SetValueWithoutNotify(string.Empty);
        }

        protected override void OnClickClearHistory()
        {
            base.OnClickClearHistory();
            resultLabel.text = string.Empty;
        }

        protected override void OnClickErrorPopup()
        {
            base.OnClickErrorPopup();
            inputField.Focus();
        }
        
        protected override void OnHideErrorPopup()
        {
            base.OnHideErrorPopup();
            OnUpdateExecuteButton(lastExecutedInput);
            errorPopup.RemoveFromClassList(showErrorPopupName);
        }
        
        private void ChangeValueEvent(ChangeEvent<string> evt) => OnChangeInputValue(evt.newValue);
        
        private void ClickOnErrorPopup(ClickEvent evt) => OnClickErrorPopup();

        private void ClearHistoryClick(ClickEvent evt) => OnClickClearHistory();
    }
}