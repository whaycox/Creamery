using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.CLI.Formatting.Token.Template
{
    using Enumerations;

    public abstract class PlainText<T> : CLI.Template.Console where T : Implementation.PlainText
    {
        protected const string TestText = nameof(TestText);

        protected abstract T BuildWithText(string testText);

        [TestMethod]
        public void WritesValueToConsole()
        {
            T testObject = BuildWithText(TestText);
            testObject.Write(MockConsole);
            MockConsole
                .VerifyOperations(1)
                .Test(ConsoleOperation.TextWritten, TestText);
        }

        [TestMethod]
        public void NullAndEmptyValueWritesNothing()
        {
            NullAndEmptyValueWritesNothingHelper(BuildWithText(string.Empty));
            NullAndEmptyValueWritesNothingHelper(BuildWithText(null));
        }
        private void NullAndEmptyValueWritesNothingHelper(T token)
        {
            token.Write(MockConsole);
            Assert.AreEqual(0, MockConsole.Operations.Count);
        }
    }
}
