using System.Collections;
using CalculatorModels;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Tests.Editor
{
    public class CalculatorModelTest
    {
        private OnlyAddCalculator calculator;
        private const string errorMessage = "ERROR";

        [SetUp]
        public void Setup()
        {
            calculator = new OnlyAddCalculator();
        }

        [Test]
        public void Calculate_ValidInput_ShouldReturnSum()
        {
            string input = "34+46";
            calculator.Calculate(input, out var result);
            Assert.AreEqual("80", result);
        }
    
        [Test]
        public void Calculate_ValidMultiInput_ShouldReturnSum()
        {
            string input = "34+46+50";
            calculator.Calculate(input, out var result);
            Assert.AreEqual("130", result);
        }

        [Test]
        public void Calculate_InvalidInput_NonNumeric_ShouldReturnError()
        {
            string input = "45+-88";
            calculator.Calculate(input, out var result);
            Assert.AreEqual(errorMessage, result);
        }

        [Test]
        public void Calculate_InvalidInput_NonArithmetic_ShouldReturnError()
        {
            string input = "98.12+48.1";
            calculator.Calculate(input, out var result);
            Assert.AreEqual(errorMessage, result);
        }

        [Test]
        public void Calculate_OnlyNumbers_ShouldReturnError()
        {
            string input = "98.12";
            calculator.Calculate(input, out var result);
            Assert.AreEqual(errorMessage, result);
        }

        [Test]
        public void Calculate_EmptyInput_ShouldReturnError()
        {
            string input = "";
            calculator.Calculate(input, out var result);
            Assert.AreEqual("ERROR", result);
        }

        [Test]
        public void Calculate_ValidInput_WithLeadingZeros_ShouldReturnSum()
        {
            string input = "045+0055";
            calculator.Calculate(input, out var result);
            Assert.AreEqual("100", result);
        }

        [Test]
        public void Calculate_ValidInput_LongInput_ShouldReturnSum()
        {
            string input = "99999999999999999999999999999+99999999999999999999999999";
            calculator.Calculate(input, out var result);
            Assert.AreEqual("100099999999999999999999999998", result);
        }

        [Test]
        public void Calculate_ValidInput_SingleDigit_ShouldReturnSum()
        {
            string input = "2+3";
            calculator.Calculate(input, out var result);
            Assert.AreEqual("5", result);
        }

        [UnityTest]
        public IEnumerator CalculatorModelTestWithEnumeratorPasses()
        {
            Calculate_ValidInput_ShouldReturnSum();
            Calculate_ValidMultiInput_ShouldReturnSum();
            Calculate_InvalidInput_NonNumeric_ShouldReturnError();
            Calculate_InvalidInput_NonArithmetic_ShouldReturnError();
            Calculate_OnlyNumbers_ShouldReturnError();
            Calculate_EmptyInput_ShouldReturnError();
            Calculate_ValidInput_WithLeadingZeros_ShouldReturnSum();
            Calculate_ValidInput_SingleDigit_ShouldReturnSum();
            yield return null;
        }
    }
}