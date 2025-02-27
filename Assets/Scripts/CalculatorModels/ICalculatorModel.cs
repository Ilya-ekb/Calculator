namespace CalculatorModels
{
    public interface ICalculatorModel
    {
        bool Calculate(string input, out string result);
    }
}