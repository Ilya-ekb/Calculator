using System;

namespace Tests.Editor
{
    using CalculatorModels;
    using CalculatorStorages;
    using CalculatorViews;

    public class FakeCalculatorModel : ICalculatorModel
    {
        public bool Calculate(string input, out string result)
        {
            result = string.Empty;
            if (input == "1+1")
            {
                result = "2";
                return true;
            }
            result = "Error. Check your input.";
            return false;
        }
    }
    

    public class FakeStorage : IStorage
    {
        public string LastValue { get; set; } = string.Empty;
        public string History { get; set; } = string.Empty;

        public void Load()
        {
            LastValue = "1+1"; 
            History = "1+1 = 2";
        }

        public void Save() { /* not needed for tests */ }

        public void ClearHistory() => History = string.Empty;

        public void UpdateHistory(string result)
        {
            History = result + "/n" + History;
        }
    }
    
    public class FakeCalculatorView : ICalculatorView
    {
        public string Input { get; private set; }
        public string Output { get; private set; }

        public event Action<string> OnChangeInput;
        public event Action OnExecuteInteraction;
        public event Action OnClearHistoryInteraction;


        public string GetInput()
        {
            return Input;
        }

        public void ChangedInput(string input)
        {
            OnChangeInput?.Invoke(input);
        }

        public void ExecuteInteraction()
        {
            OnExecuteInteraction?.Invoke();
        }


        public void ClearedHistory()
        {
            OnClearHistoryInteraction?.Invoke();
        }
        

        public void SetInput(string input)
        {
            Input = input;
            OnChangeInput?.Invoke(input);
        }

        public void DisplayResult(string result)
        {
            Output = Input + "=" + result;
        }

        public void DisplayHistory(string history)
        {
            //test
        }

        public void DisplayError()
        {
            Output = "Error. Check your input.";
        }

        public void ClearHistory() => OnClearHistoryInteraction?.Invoke();
    }

}