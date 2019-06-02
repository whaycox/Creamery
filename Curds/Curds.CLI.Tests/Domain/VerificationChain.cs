using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.CLI.Domain
{
    using Enumerations;
    using Mock;

    public class VerificationChain
    {
        private IConsole MockConsole { get; }

        public int CurrentIndex { get; set; }

        public VerificationChain(IConsole mockConsole)
        {
            MockConsole = mockConsole;
        }

        public VerificationChain Test(ExpectedEvent expected) => Test(expected.Operation, expected.Value);
        public VerificationChain Test(ConsoleOperation operation, object value)
        {
            Assert.AreEqual(operation, MockConsole.Operations[CurrentIndex].Operation);
            Assert.AreEqual(value, MockConsole.Operations[CurrentIndex].Value);
            CurrentIndex++;
            return this;
        }
        public VerificationChain TestTextStartsWith(string startsWith)
        {
            Assert.AreEqual(ConsoleOperation.TextWritten, MockConsole.Operations[CurrentIndex].Operation);
            string written = (string)MockConsole.Operations[CurrentIndex].Value;
            Assert.IsTrue(written.StartsWith(startsWith));
            CurrentIndex++;
            return this;
        }

        public VerificationChain IsFinished()
        {
            Assert.AreEqual(MockConsole.Operations.Count, CurrentIndex);
            return this;
        }
    }
}
