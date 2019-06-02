using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.CLI.Operations.Tests
{
    using Exceptions;
    using Mock;

    [TestClass]
    public class Operation : Template.Console<Implementation.Operation>
    {
        private Mock.Operation _obj = new Mock.Operation();
        protected override Implementation.Operation TestObject => _obj;

        [DataTestMethod]
        [DataRow(false, false)]
        [DataRow(false, true)]
        [DataRow(true, false)]
        [DataRow(true, true)]
        public void SyntaxIsJustAliases(bool argIsRequired, bool boolArgIsRequired)
        {
            _obj.ArgumentIsRequired = argIsRequired;
            _obj.BooleanArgumentIsRequired = boolArgIsRequired;

            TestObject.Syntax.Write(MockConsole);
            MockConsole
                .VerifyOperations(7)
                .VerifyOperationSyntax()
                .IsFinished();
        }

        [DataTestMethod]
        [DataRow(false, false)]
        [DataRow(false, true)]
        [DataRow(true, false)]
        [DataRow(true, true)]
        public void UsageIsSyntaxWithArgumentUsages(bool argIsRequired, bool boolArgIsRequired)
        {
            _obj.ArgumentIsRequired = argIsRequired;
            _obj.BooleanArgumentIsRequired = boolArgIsRequired;

            TestObject.Usage.Write(MockConsole);
            MockConsole
                .VerifyOperations(UsageExpectedTokens(argIsRequired, boolArgIsRequired))
                .VerifyOperationUsage(argIsRequired, boolArgIsRequired)
                .IsFinished();
        }
        private int UsageExpectedTokens(bool argIsRequired, bool boolArgIsRequired)
        {
            if (!argIsRequired && !boolArgIsRequired)
                return 93;
            else if (!argIsRequired || !boolArgIsRequired)
                return 91;
            else
                return 89;
        }

        [DataTestMethod]
        [DataRow(false)]
        [DataRow(true)]
        public void ParsesAllArgumentsIfPossible(bool argumentFirst)
        {
            MockArgumentCrawler.LoadScript(AllArgumentScript(argumentFirst));
            var parsed = TestObject.Parse(MockArgumentCrawler);
            Assert.AreEqual(2, parsed.Count);

            var argParsed = parsed[nameof(Implementation.Argument)];
            Assert.AreEqual(3, argParsed.Count);
            Assert.AreEqual(One, argParsed[0].RawValue);
            Assert.AreEqual(Two, argParsed[1].RawValue);
            Assert.AreEqual(Three, argParsed[2].RawValue);

            var boolArgParsed = parsed[nameof(Implementation.BooleanArgument)];
            Assert.AreEqual(0, boolArgParsed.Count);
        }
        private string[] AllArgumentScript(bool argumentFirst)
        {
            List<string> toReturn = new List<string>();
            if (argumentFirst)
            {
                toReturn.Add(Implementation.Argument.PrependIdentifier(nameof(Implementation.Argument)));
                toReturn.Add(One);
                toReturn.Add(Two);
                toReturn.Add(Three);
                toReturn.Add(Implementation.Argument.PrependIdentifier(nameof(Implementation.BooleanArgument)));
            }
            else
            {
                toReturn.Add(Implementation.Argument.PrependIdentifier(nameof(Implementation.BooleanArgument)));
                toReturn.Add(Implementation.Argument.PrependIdentifier(nameof(Implementation.Argument)));
                toReturn.Add(One);
                toReturn.Add(Two);
                toReturn.Add(Three);
            }
            return toReturn.ToArray();
        }

        [TestMethod]
        public void ThrowsWithDuplicateArgument()
        {
            MockArgumentCrawler.LoadScript(DuplicateArgumentScript);
            Assert.ThrowsException<DuplicateArgumentException>(() => TestObject.Parse(MockArgumentCrawler));
        }
        private string[] DuplicateArgumentScript => new string[]
        {
            Implementation.Argument.PrependIdentifier(nameof(Implementation.Argument)),
            One,
            Two,
            Three,
            Implementation.Argument.PrependIdentifier(nameof(Implementation.Argument)),
            One,
            Two,
            Three,
        };

        [TestMethod]
        public void ThrowsIfRequiredArgumentIsntSupplied()
        {
            MockArgumentCrawler.LoadScript(MissingArgument);
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.Parse(MockArgumentCrawler));
        }
        private string[] MissingArgument => new string[]
        {
            Implementation.Argument.PrependIdentifier(nameof(Implementation.BooleanArgument))
        };

        [TestMethod]
        public void ThrowsIfRequiredBooleanArgumentIsntSupplied()
        {
            MockArgumentCrawler.LoadScript(MissingBooleanArgument);
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.Parse(MockArgumentCrawler));
        }
        private string[] MissingBooleanArgument => new string[]
        {
            Implementation.Argument.PrependIdentifier(nameof(Implementation.Argument)),
            One,
            Two,
            Three,
        };

        [TestMethod]
        public void AllowsOptionalArgumentMissing()
        {
            _obj.ArgumentIsRequired = false;
            MockArgumentCrawler.LoadScript(MissingArgument);
            var parsed = TestObject.Parse(MockArgumentCrawler);
            Assert.AreEqual(1, parsed.Count);
            var boolArgParsed = parsed[nameof(Implementation.BooleanArgument)];
            Assert.AreEqual(0, boolArgParsed.Count);
        }

        [TestMethod]
        public void AllowsBooleanOptionalArgumentMissing()
        {
            _obj.BooleanArgumentIsRequired = false;
            MockArgumentCrawler.LoadScript(MissingBooleanArgument);
            var parsed = TestObject.Parse(MockArgumentCrawler);
            Assert.AreEqual(1, parsed.Count);
            var argParsed = parsed[nameof(Implementation.Argument)];
            Assert.AreEqual(3, argParsed.Count);
            Assert.AreEqual(One, argParsed[0].RawValue);
            Assert.AreEqual(Two, argParsed[1].RawValue);
            Assert.AreEqual(Three, argParsed[2].RawValue);
        }
    }
}
