using System;
using System.Linq;
using UnityEngine;

namespace CalculatorViews
{
    public abstract class BaseCalculatorView : MonoBehaviour, ICalculatorView
    {
        public event Action<string> OnChangeInput;
        public event Action OnExecuteInteraction;
        public event Action<string> OnResultOutput;
        public event Action OnClearHistoryInteraction;

        protected bool isAlive;
        protected string lastExecutedInput;

        private const string errorValue = "ERROR";

        public void SetInput(string input)
        {
            if (!isAlive) return;
            OnSetInput(input);
        }

        public string GetInput()
        {
            return !isAlive ? null : OnGetInput();
        }

        protected virtual void OnEnable()
        {
            isAlive = true;
        }

        protected virtual void OnDisable()
        {
            isAlive = false;
        }

        public void DisplayResult(string result)
        {
            if (!isAlive) return;
            var input = GetInput();
            if (HasEmptyOrSameValue(input))
            {
                UpdateExecuteButton(input);
                lastExecutedInput = input;
                return;
            }

            Output(result, input);

            ClearInputField();
        }
        
        public void DisplayError()
        {
            if (!isAlive) return;
            Output(errorValue, GetInput());
            OnShowErrorPopup();
        }
        
        public void DisplayHistory(string history)
        {
            if (!isAlive) return;
            OnDisplayHistory(history);
        }

        protected abstract string OnGetInput();
        protected virtual void OnChangeInputValue(string value) => OnChangeInput?.Invoke(value);
        protected virtual void OnOutput(string formatedResult) => OnResultOutput?.Invoke(formatedResult);
        protected virtual void OnExecuteClick<T>(T evt) => OnExecuteInteraction?.Invoke();
        protected virtual void OnClickClearHistory() => OnClearHistoryInteraction?.Invoke();


        protected virtual void OnDisplayHistory(string history) { }

        protected virtual void OnUpdateExecuteButton(string input) { }

        protected virtual void OnShowErrorPopup() { }

        protected virtual void OnHideErrorPopup() { }

        protected virtual void OnClearInputField() => OnChangeInput?.Invoke("");

        protected virtual void OnSetInput(string input) { }

        protected virtual void OnClickErrorPopup() => HideErrorPopup();

        protected bool HasEmptyOrSameValue(string input) => string.IsNullOrEmpty(input) || input == lastExecutedInput;

        protected bool IsExistWarning<T>(T uiElement)
        {
            if (uiElement is not null) return false;
            Debug.LogWarning($"{GetType().FullName} has not been set");
            return true;
        }

        
        private void Output(string result, string input)
        {
            lastExecutedInput = input;
            result = input + "=" + result;
            OnOutput(result);
        }


        private void HideErrorPopup()
        {
            if (!isAlive) return;
            OnHideErrorPopup();
        }

        private void ClearInputField()
        {
            if (!isAlive) return;
            OnClearInputField();
        }

        private void UpdateExecuteButton(string input)
        {
            if (!isAlive) return;
            OnUpdateExecuteButton(input);
        }
    }
}