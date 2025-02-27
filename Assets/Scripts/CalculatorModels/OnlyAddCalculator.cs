using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;

namespace CalculatorModels
{
    public class OnlyAddCalculator : ICalculatorModel
    {
        private static readonly Regex InputPattern = new(@"^(\d+(\+\d+)+)$");
        private const string errorValue = "ERROR";
        
        public bool Calculate(string input, out string result)
        {
            input = input.Replace(" ", string.Empty);
            result = string.Empty;
            if (!TryParseInput(input, out var parsedNums))
            {
                result = errorValue;
                return false;
            }

            var sum = parsedNums.Aggregate<BigInteger, BigInteger>(0, (c, n) => c + n);
            result = sum.ToString(CultureInfo.InvariantCulture);
            return true;
        }

        private static bool TryParseInput(string input, out BigInteger[] nums)
        {
            nums = null;
            if(string.IsNullOrEmpty(input)) return false;
            var result = InputPattern.IsMatch(input);
            if (!result) return false;
            
            var args = input.Split('+');
            nums = new BigInteger[args.Length];
            for (var i = 0; i < args.Length; i++)
                if (!BigInteger.TryParse(args[i], out nums[i]))
                    return false;
            
            return true;
        }
    }
}