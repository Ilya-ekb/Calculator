using CalculatorPresenters;
using NUnit.Framework;

namespace Tests.Editor
{
    [TestFixture]
    public class CalculatorPresenterTests
    {
        private CalculatorPresenter calculatorPresenter;
        private FakeCalculatorModel calculatorModel;
        private FakeStorage storage;
        private FakeCalculatorView calculatorView;

        [SetUp]
        public void Setup()
        {
            calculatorModel = new FakeCalculatorModel();
            storage = new FakeStorage();
            calculatorView = new FakeCalculatorView();
            calculatorPresenter = new CalculatorPresenter(calculatorModel, storage, calculatorView);
            calculatorPresenter.SetAlive();
        }

        [TearDown]
        public void Drop()
        {
            calculatorPresenter.Drop();
            calculatorPresenter = null;
        }

        [Test]
        public void Execute_ValidInput_ShouldDisplayResult()
        {
            calculatorView.SetInput("1+1");
            calculatorView.ExecuteInteraction();

            Assert.AreEqual("1+1=2", calculatorView.Output);
        }

        [Test]
        public void Execute_InvalidInput_ShouldDisplayError()
        {
            calculatorView.SetInput("invalid input");
            calculatorView.ExecuteInteraction();

            Assert.AreEqual("Error. Check your input.", calculatorView.Output);
        }

        [Test]
        public void OnChangeInput_ShouldUpdateStorageLastValue()
        {
            var newValue = "5+5";
            calculatorView.SetInput(newValue);

            Assert.AreEqual(newValue, storage.LastValue);
        }

        [Test]
        public void OnResultReady_ShouldUpdateHistory()
        {
            calculatorView.SetInput("1+1");
            calculatorView.ExecuteInteraction();
            
            var notes = storage.History.Split("/n");
            Assert.AreEqual(2, notes.Length);
            Assert.AreEqual("1+1=2", notes[0]);
        }

        [Test]
        public void ClearHistory_ShouldEmptyHistory()
        {
            calculatorView.SetInput("1+1");
            calculatorView.ExecuteInteraction();
            calculatorView.ClearHistory();

            Assert.AreEqual(0, storage.History.Length);
        }
    }
}
