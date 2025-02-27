using System;
using CalculatorModels;
using CalculatorStorages;
using CalculatorViews;

namespace CalculatorPresenters
{
    public class CalculatorPresenter
    {
        private ICalculatorModel calculatorModel;
        private IStorage calculatorStorage;
        private ICalculatorView calculatorView;
        private bool isAlive;

        public CalculatorPresenter(ICalculatorModel calculator, IStorage storage, ICalculatorView view)
        {
            calculatorModel = calculator;
            calculatorView = view;
            calculatorStorage = storage;
        }

        public void SetAlive()
        {
            if (isAlive) return;
            isAlive = true;
            calculatorStorage.Load();
            calculatorView.SetInput(calculatorStorage?.LastValue);
            calculatorView.DisplayHistory(calculatorStorage?.History);
            calculatorView.OnChangeInput += OnChangeInput;
            calculatorView.OnExecuteInteraction += Execute;
            calculatorView.OnClearHistoryInteraction += OnClearHistory;
        }

        public void Drop()
        {
            if (!isAlive) return;
            isAlive = false;
            calculatorView.OnExecuteInteraction -= Execute;
            calculatorView.OnChangeInput -= OnChangeInput;
            calculatorStorage.Save();
        }

        private void OnClearHistory()
        {
            calculatorStorage.ClearHistory();
        }

        ~CalculatorPresenter()
        {
            calculatorView = null;
            calculatorModel = null;
            calculatorStorage = null;
        }

        private void Execute()
        {
            var input = calculatorView.GetInput();
            if (calculatorModel.Calculate(input, out var result))
                calculatorView.DisplayResult(result);
            else
                calculatorView.DisplayError();
            calculatorStorage.UpdateHistory(input + "=" + result);
        }

        private void OnResultReady(string formatedResult)
        {
            calculatorStorage.UpdateHistory(formatedResult);
        }

        private void OnChangeInput(string input)
        {
            if (calculatorStorage is null)
            {
                Console.WriteLine("No calculator storage available");
                return;
            }

            calculatorStorage.LastValue = input;
        }
    }
}