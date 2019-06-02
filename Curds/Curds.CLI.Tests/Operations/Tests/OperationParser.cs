using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Curds.CLI.Operations.Tests
{
    [TestClass]
    public class OperationParser : Template.Console<Implementation.OperationParser>
    {
        protected override Implementation.OperationParser TestObject { get; } = new Implementation.OperationParser();

        private void VerifyOperationParsed(List<Implementation.OperationParser.ParsedPair> results, bool argIsExpected, bool boolArgIsExpected)
        {
            var operation = results.Where(o => o.Operation.Name == nameof(Implementation.Operation)).First();
            if (argIsExpected)
                VerifyArgumentParsed(operation);
            if (boolArgIsExpected)
                VerifyBooleanArgumentParsed(operation);
        }
        private void VerifyArgumentParsed(Implementation.OperationParser.ParsedPair parsedPair)
        {
            var values = parsedPair.Arguments[nameof(Implementation.Argument)];
            Assert.AreEqual(3, values.Count);
            Assert.AreEqual(One, values[0].RawValue);
            Assert.AreEqual(Two, values[1].RawValue);
            Assert.AreEqual(Three, values[2].RawValue);
        }
        private void VerifyBooleanArgumentParsed(Implementation.OperationParser.ParsedPair parsedPair)
        {
            var values = parsedPair.Arguments[nameof(Implementation.BooleanArgument)];
            Assert.AreEqual(0, values.Count);
        }
        private void VerifyArgumentlessOperationParsed(List<Implementation.OperationParser.ParsedPair> results)
        {
            var operation = results.Where(o => o.Operation.Name == nameof(Implementation.ArgumentlessOperation)).First();
            Assert.AreEqual(1, operation.Arguments.Count);
            var constant = operation.Arguments[Implementation.ArgumentlessOperation.ArgumentlessKey];
            Assert.AreEqual(3, constant.Count);
            Assert.AreEqual(One, constant[0].RawValue);
            Assert.AreEqual(Two, constant[1].RawValue);
            Assert.AreEqual(Three, constant[2].RawValue);
        }
        private void VerifyBooleanOperationParsed(List<Implementation.OperationParser.ParsedPair> results)
        {
            var operation = results.Where(o => o.Operation.Name == nameof(Implementation.BooleanOperation)).First();
            Assert.AreEqual(0, operation.Arguments.Count);
        }

        private string[] OperationStrings(bool argIsRequired, bool boolArgIsRequired)
        {
            List<string> toReturn = new List<string>();
            toReturn.Add(Implementation.Operation.PrependIdentifier(nameof(Implementation.Operation)));

            if (argIsRequired)
            {
                toReturn.Add(Implementation.Argument.PrependIdentifier(nameof(Implementation.Argument)));
                toReturn.Add(One);
                toReturn.Add(Two);
                toReturn.Add(Three);
            }

            if (boolArgIsRequired)
                toReturn.Add(Implementation.Argument.PrependIdentifier(nameof(Implementation.BooleanArgument)));

            return toReturn.ToArray();
        }
        private string[] ArgumentlessOperationStrings() => new string[]
        {
            Implementation.Operation.PrependIdentifier(nameof(Implementation.ArgumentlessOperation)),
            One,
            Two,
            Three,
        };
        private string[] BooleanOperationStrings() => new string[]
        {
            Implementation.Operation.PrependIdentifier(nameof(Implementation.BooleanOperation)),
        };

        [DataTestMethod]
        [DataRow(false, false)]
        [DataRow(false, true)]
        [DataRow(true, false)]
        [DataRow(true, true)]
        public void ParsesAllOperations(bool argIsRequired, bool boolArgIsRequired)
        {
            var parsedOperations = TestObject.Parse(BuildTestOperations(argIsRequired, boolArgIsRequired), AllOperations(argIsRequired, boolArgIsRequired));
            Assert.AreEqual(3, parsedOperations.Count);
            VerifyOperationParsed(parsedOperations, argIsRequired, boolArgIsRequired);
            VerifyArgumentlessOperationParsed(parsedOperations);
            VerifyBooleanOperationParsed(parsedOperations);
        }
        private string[] AllOperations(bool argIsRequired, bool boolArgIsRequired)
        {
            List<string> toReturn = new List<string>();
            toReturn.AddRange(OperationStrings(argIsRequired, boolArgIsRequired));
            toReturn.AddRange(ArgumentlessOperationStrings());
            toReturn.AddRange(BooleanOperationStrings());
            return toReturn.ToArray();
        }

        [DataTestMethod]
        [DataRow(true, false, false)]
        [DataRow(false, true, false)]
        [DataRow(false, false, true)]
        public void ParsesOperationsInAnyOrder(bool operationFirst, bool argumentlessOperationFirst, bool booleanOperationFirst)
        {
            var parsedOperations = TestObject.Parse(BuildTestOperations(true, true), OperationsInAnOrder(operationFirst, argumentlessOperationFirst, booleanOperationFirst));
            Assert.AreEqual(3, parsedOperations.Count);
            VerifyOperationParsed(parsedOperations, true, true);
            VerifyArgumentlessOperationParsed(parsedOperations);
            VerifyBooleanOperationParsed(parsedOperations);
        }
        private string[] OperationsInAnOrder(bool operationFirst, bool argumentlessOperationFirst, bool booleanOperationFirst)
        {
            List<string> toReturn = new List<string>();
            if (operationFirst)
            {
                toReturn.AddRange(OperationStrings(true, true));
                toReturn.AddRange(ArgumentlessOperationStrings());
                toReturn.AddRange(BooleanOperationStrings());
            }
            else if (argumentlessOperationFirst)
            {
                toReturn.AddRange(ArgumentlessOperationStrings());
                toReturn.AddRange(BooleanOperationStrings());
                toReturn.AddRange(OperationStrings(true, true));
            }
            else if (booleanOperationFirst)
            {
                toReturn.AddRange(BooleanOperationStrings());
                toReturn.AddRange(OperationStrings(true, true));
                toReturn.AddRange(ArgumentlessOperationStrings());
            }
            else
                Assert.Fail();

            return toReturn.ToArray();
        }
    }
}
