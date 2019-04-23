using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Curds.Domain;
using Curds.Domain.Application;
using Curds.Domain.CLI.Operations;
using Curds.Domain.Application.Message.Command;

namespace Curds.CLI.Operations.Tests
{
    using Formatting;

    [TestClass]
    public class OperationParser : FormattingTemplate<OperationParser<MockApplication>>
    {
        private MockOptions Options = new MockOptions();
        private MockApplication Application = null;
        private List<Operations.Operation> Operations = null;

        private string[] MockOptionalOperationArguments => new string[]
        {
            CLI.Operations.Operation.PrependIdentifier(nameof(MockOperation)),
            CLI.Operations.Argument.PrependIdentifier(MockArgument.IdentifiedName(MockOperation.OptionalIdentifier)),
            nameof(MockValue),
        };
        private string[] MockRequiredOperationArguments => new string[] 
        {
            CLI.Operations.Operation.PrependIdentifier(nameof(MockOperation)),
            CLI.Operations.Argument.PrependIdentifier(MockArgument.IdentifiedName(MockOperation.RequiredIdentifier)),
            nameof(MockValue),
        };

        private string[] MockArgumentlessOperationArguments => new string[]
        {
            CLI.Operations.Operation.PrependIdentifier(nameof(MockArgumentlessOperation)),
            nameof(MockValue),
            nameof(MockValue),
            nameof(MockValue),
        };

        protected override OperationParser<MockApplication> TestObject { get; } = new OperationParser<MockApplication>();

        [TestInitialize]
        public void Init()
        {
            Application = new MockApplication(Options);
            Operations = new List<Operations.Operation>
            {
                new MockOperation(),
                new MockArgumentlessOperation(),
            };
        }

        [TestMethod]
        public void ParseThrowsWithNoArgs()
        {
            Assert.ThrowsException<ArgumentNullException>(() => TestObject.Parse(Operations, null));
            Assert.ThrowsException<ArgumentNullException>(() => TestObject.Parse(Operations, new string[0]));
        }

        [TestMethod]
        public void ParseThrowsWithInvalidArg()
        {
            Assert.ThrowsException<KeyNotFoundException>(() => TestObject.Parse(Operations, new string[] { nameof(ParseThrowsWithInvalidArg) }));
        }

        [TestMethod]
        public void CanAcceptMultipleOfSameOperation()
        {
            List<string> testArgs = new List<string>();
            testArgs.AddRange(MockRequiredOperationArguments);
            testArgs.AddRange(MockRequiredOperationArguments);
            testArgs[testArgs.Count - 1] = nameof(CanAcceptMultipleOfSameOperation);

            var results = TestObject.Parse(Operations, testArgs.ToArray());
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(nameof(MockValue), results[0].Arguments[MockArgument.IdentifiedName(MockOperation.RequiredIdentifier)][0].RawValue);
            Assert.AreEqual(nameof(CanAcceptMultipleOfSameOperation), results[1].Arguments[MockArgument.IdentifiedName(MockOperation.RequiredIdentifier)][0].RawValue);
        }

        [TestMethod]
        public void ParseThrowsIfRequiredArgumentIsMissing()
        {
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.Parse(Operations, MockOptionalOperationArguments));
        }

        [TestMethod]
        public void ParseDoesNotRequireOptionalArgument()
        {
            TestObject.Parse(Operations, MockRequiredOperationArguments);
        }

        [TestMethod]
        public void AllValuesForArgumentAreRequired()
        {
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.Parse(Operations, MockRequiredOperationArguments.Take(2).ToArray()));
        }

        [TestMethod]
        public void OperationOrderDoesNotMatter()
        {
            List<string> firstArgs = new List<string>();
            firstArgs.AddRange(MockOptionalOperationArguments);
            firstArgs.AddRange(MockRequiredOperationArguments);
            var firstParsed = TestObject.Parse(Operations, firstArgs.ToArray());

            List<string> secondArgs = new List<string>();
            secondArgs.AddRange(MockOptionalOperationArguments);
            secondArgs.AddRange(MockRequiredOperationArguments);
            var secondParsed = TestObject.Parse(Operations, secondArgs.ToArray());

            Assert.AreEqual(firstParsed.Count, secondParsed.Count);
            Assert.AreEqual(firstParsed[0].Operation.Name, secondParsed[1].Operation.Name);
            Assert.AreEqual(firstParsed[1].Operation.Name, secondParsed[0].Operation.Name);
        }

        [TestMethod]
        public void CanSupplyAnyAliasForOperation()
        {
            string[] args = MockRequiredOperationArguments;
            var firstParsed = TestObject.Parse(Operations, args);
            args[1] = CLI.Operations.Argument.PrependIdentifier($"{MockOperation.RequiredIdentifier}{nameof(MockArgument.Aliases)}");
            var secondParsed = TestObject.Parse(Operations, args);

            Assert.AreEqual(firstParsed.Count, secondParsed.Count);
            Assert.AreEqual(firstParsed[0].Operation.Name, secondParsed[0].Operation.Name);
            Assert.AreEqual(firstParsed[0].Arguments[MockArgument.IdentifiedName(MockOperation.RequiredIdentifier)][0].RawValue, 
                secondParsed[0].Arguments[MockArgument.IdentifiedName(MockOperation.RequiredIdentifier)][0].RawValue);
        }

        [TestMethod]
        public void ArgumentlessOperationHasImplicitKey()
        {
            var parsed = TestObject.Parse(Operations, MockArgumentlessOperationArguments);
            Assert.AreEqual(3, parsed[0].Arguments[MockArgumentlessOperation.ArgumentlessKey].Count);
            Assert.AreEqual(nameof(MockValue), parsed[0].Arguments[MockArgumentlessOperation.ArgumentlessKey][0].RawValue);
            Assert.AreEqual(nameof(MockValue), parsed[0].Arguments[MockArgumentlessOperation.ArgumentlessKey][1].RawValue);
            Assert.AreEqual(nameof(MockValue), parsed[0].Arguments[MockArgumentlessOperation.ArgumentlessKey][2].RawValue);
        }
    }
}
