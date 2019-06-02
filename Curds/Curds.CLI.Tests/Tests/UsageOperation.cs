using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Curds.CLI.Tests
{
    using Domain;
    using Enumerations;
    using Operations.Implementation;

    [TestClass]
    public class UsageOperation : Template.Console<Implementation.UsageOperation>
    {
        private const string TestError = nameof(TestError);
        private const string TestDescription = nameof(TestDescription);

        protected override Implementation.UsageOperation TestObject { get; } = new Implementation.UsageOperation();

        [DataTestMethod]
        [DataRow(false, false)]
        [DataRow(false, true)]
        [DataRow(true, false)]
        [DataRow(true, true)]
        public void FormatsAllOperationsUsage(bool argIsRequired, bool boolArgIsRequired)
        {
            TestObject.Format(TestError, TestDescription, BuildTestOperations(argIsRequired, boolArgIsRequired)).Write(MockConsole);
            MockConsole
                .VerifyOperations(AllOperationsExpectedTokens(argIsRequired, boolArgIsRequired))
                .VerifyUsageText(TestError, TestDescription)
                .VerifyNewLine()
                .VerifyUsageOperations(false, true, argIsRequired, boolArgIsRequired, true, true)
                .IsFinished();
        }
        private int AllOperationsExpectedTokens(bool argIsRequired, bool boolArgIsRequired)
        {
            if (!argIsRequired && !boolArgIsRequired)
                return 181;
            else if (!argIsRequired || !boolArgIsRequired)
                return 179;
            else
                return 177;
        }

        [DataTestMethod]
        [DataRow(false, false, false)]
        [DataRow(false, false, true)]
        [DataRow(false, true, false)]
        [DataRow(false, true, true)]
        [DataRow(true, false, false)]
        [DataRow(true, false, true)]
        [DataRow(true, true, false)]
        [DataRow(true, true, true)]
        public void FormatsSomeOperations(bool operation, bool argumentlessOperation, bool booleanOperation)
        {
            TestObject.Format(TestError, TestDescription, BuildSomeOperations(operation, argumentlessOperation, booleanOperation)).Write(MockConsole);
            MockConsole
                .VerifyOperations(SomeExpectedTokens(operation, argumentlessOperation, booleanOperation))
                .VerifyUsageText(TestError, TestDescription)
                .VerifyNewLine()
                .VerifyUsageOperations(false, operation, true, true, argumentlessOperation, booleanOperation)
                .IsFinished();
        }
        private List<Operation> BuildSomeOperations(bool operation, bool argumentlessOperation, bool booleanOperation)
        {
            List<Operation> toReturn = new List<Operation>();

            if (operation)
                BuildMockOperation(toReturn, true, true);

            if (argumentlessOperation)
                BuildMockArgumentlessOperation(toReturn);

            if (booleanOperation)
                BuildMockBooleanOperation(toReturn);

            return toReturn;
        }
        private int SomeExpectedTokens(bool operation, bool argumentlessOperation, bool booleanOperation)
        {
            int tokens = 19;

            if (operation)
            {
                tokens += 89;
                if (argumentlessOperation || booleanOperation)
                    tokens += 2;
            }

            if (argumentlessOperation)
            {
                tokens += 52;
                if (booleanOperation)
                    tokens += 2;
            }

            if (booleanOperation)
                tokens += 13;

            return tokens;
        }

        [TestMethod]
        public void FormatsWithoutMessage()
        {
            TestObject.Format(null, TestDescription, new List<CLI.Operations.Implementation.Operation>()).Write(MockConsole);
            MockConsole
                .VerifyOperations(18)
                .VerifyUsageText(null, TestDescription)
                .VerifyNewLine()
                .VerifyUsageOperations(false, false, false, false, false, false)
                .IsFinished();
        }

        [TestMethod]
        public void FormatsWithoutDescription()
        {
            TestObject.Format(TestError, null, new List<Operation>()).Write(MockConsole);
            MockConsole
                .VerifyOperations(18)
                .VerifyUsageText(TestError, null)
                .VerifyNewLine()
                .VerifyUsageOperations(false, false, false, false, false, false)
                .IsFinished();
        }
    }
}
