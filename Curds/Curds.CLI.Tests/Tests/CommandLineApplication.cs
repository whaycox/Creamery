using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.CLI.Tests
{
    using Application.Abstraction;
    using Application.Mock;
    using Enumerations;
    using Operations.Domain;
    using Operations.Implementation;

    [TestClass]
    public class CommandLineApplication : Template.Console<Mock.CommandLineApplication>
    {
        private ICurdsOptions MockOptions = null;
        private CurdsApplication MockApplication = null;
        private Mock.CommandLineApplication _obj = null;
        protected override Mock.CommandLineApplication TestObject => _obj;

        [TestInitialize]
        public void BuildObj()
        {
            MockOptions = new Application.Domain.CurdsOptions() { Time = MockTime };
            MockApplication = new CurdsApplication(MockOptions);
            _obj = new Mock.CommandLineApplication(MockApplication, MockConsole);
        }

        [TestMethod]
        public void NoArgumentsPrintsUsageAndExits()
        {
            TestObject.Execute(new string[0]);
            MockConsole
                .VerifyOperations(193)
                .VerifyUsageText("Failed to Execute", nameof(OptionValue.Description))
                .VerifyNewLine()
                .VerifyUsageOperations(true, true, true, true, true, true)
                .Test(ConsoleOperation.EnvironmentExited, 1)
                .IsFinished();
        }

        [TestMethod]
        public void CanExecuteBooleanOperation()
        {
            string[] args = new string[]
            {
                Operation.PrependIdentifier(nameof(BooleanOperation))
            };
            TestObject.Execute(args);
            MockConsole
                .VerifyOperations(1)
                .Test(ConsoleOperation.EnvironmentExited, 0)
                .IsFinished();
            Assert.AreEqual(1, TestObject.ExecutedPairs.Count);
            var executed = TestObject.ExecutedPairs[0];
            Assert.AreEqual(0, executed.Arguments.Count);
            Assert.IsInstanceOfType(executed.Operation, typeof(BooleanOperation));
        }

        [TestMethod]
        public void CanExecuteMultipleBooleanOperations()
        {
            string[] args = new string[]
            {
                Operation.PrependIdentifier(nameof(BooleanOperation)),
                Operation.PrependIdentifier(nameof(BooleanOperation))
            };
            TestObject.Execute(args);
            MockConsole
                .VerifyOperations(1)
                .Test(ConsoleOperation.EnvironmentExited, 0)
                .IsFinished();

            Assert.AreEqual(2, TestObject.ExecutedPairs.Count);

            var first = TestObject.ExecutedPairs[0];
            Assert.AreEqual(0, first.Arguments.Count);
            Assert.IsInstanceOfType(first.Operation, typeof(BooleanOperation));

            var second = TestObject.ExecutedPairs[1];
            Assert.AreEqual(0, second.Arguments.Count);
            Assert.IsInstanceOfType(second.Operation, typeof(BooleanOperation));
        }

        [TestMethod]
        public void CanExecuteArgumentlessOperation()
        {
            string[] args = new string[]
            {
                Operation.PrependIdentifier(nameof(ArgumentlessOperation)),
                Three,
                Two,
                One,
            };
            TestObject.Execute(args);
            MockConsole
                .VerifyOperations(1)
                .Test(ConsoleOperation.EnvironmentExited, 0)
                .IsFinished();
            Assert.AreEqual(1, TestObject.ExecutedPairs.Count);
            var executed = TestObject.ExecutedPairs[0];
            Assert.AreEqual(1, executed.Arguments.Count);
            Assert.IsInstanceOfType(executed.Operation, typeof(ArgumentlessOperation));
            var constantArg = executed.Arguments[ArgumentlessOperation.ArgumentlessKey];
            Assert.AreEqual(3, constantArg.Count);
            Assert.AreEqual(Three, constantArg[0].RawValue);
            Assert.AreEqual(Two, constantArg[1].RawValue);
            Assert.AreEqual(One, constantArg[2].RawValue);
        }
    }
}
