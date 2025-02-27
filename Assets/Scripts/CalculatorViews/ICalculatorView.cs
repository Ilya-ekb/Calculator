using System;

namespace CalculatorViews
{
    public interface ICalculatorView
    {
        event Action<string> OnChangeInput;
        event Action OnExecuteInteraction; 
        event Action OnClearHistoryInteraction;
        void SetInput(string input);
        string GetInput();
        void DisplayResult(string result);
        void DisplayHistory(string history);
        void DisplayError();
    }
}