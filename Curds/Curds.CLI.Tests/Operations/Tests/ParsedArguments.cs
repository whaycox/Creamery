using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.CLI.Operations.Tests
{
    using Exceptions;

    [TestClass]
    public class ParsedArguments : Template.Console<Implementation.ParsedArguments>
    {
        private Implementation.ParsedArguments _obj = null;
        protected override Implementation.ParsedArguments TestObject => _obj;

        [TestInitialize]
        public void BuildObj()
        {
            _obj = new Implementation.ParsedArguments(BuildMap(true, true));
        }

        private Dictionary<string, Implementation.Argument> BuildMap(bool argIsRequired, bool boolArgIsRequired)
        {
            Dictionary<string, Implementation.Argument> toReturn = new Dictionary<string, Implementation.Argument>();
            AddArgumentAliases(toReturn, argIsRequired);
            AddBooleanArgumentAliases(toReturn, boolArgIsRequired);
            return toReturn;
        }
        private void AddArgumentAliases(Dictionary<string, Implementation.Argument> map, bool isRequired)
        {
            Mock.Argument mockArgument = new Mock.Argument(isRequired);
            foreach (string alias in mockArgument.Aliases)
                map.Add(Implementation.Argument.PrependIdentifier(alias), mockArgument);
        }
        private void AddBooleanArgumentAliases(Dictionary<string, Implementation.Argument> map, bool isRequired)
        {
            Mock.BooleanArgument mockBooleanArgument = new Mock.BooleanArgument(isRequired);
            foreach (string alias in mockBooleanArgument.Aliases)
                map.Add(Implementation.Argument.PrependIdentifier(alias), mockBooleanArgument);
        }

        private void VerifyArgumentParsed(Dictionary<string, List<Implementation.Value>> parsed)
        {
            var argParsed = parsed[nameof(Implementation.Argument)];
            Assert.AreEqual(3, argParsed.Count);
            Assert.AreEqual(One, argParsed[0].RawValue);
            Assert.AreEqual(Two, argParsed[1].RawValue);
            Assert.AreEqual(Three, argParsed[2].RawValue);
        }
        private void VerifyBooleanArgumentParsed(Dictionary<string, List<Implementation.Value>> parsed)
        {
            var boolArParsed = parsed[nameof(Implementation.BooleanArgument)];
            Assert.AreEqual(0, boolArParsed.Count);
        }

        [DataTestMethod]
        [DataRow(true)]
        [DataRow(false)]
        public void ParsesAllArgumentsCorrectly(bool argumentFirst)
        {
            MockArgumentCrawler.LoadScript(AllArgumentScript(argumentFirst));
            TestObject.Parse(MockArgumentCrawler);

            Assert.AreEqual(2, TestObject.Provided.Count);
            VerifyArgumentParsed(TestObject.Provided);
            VerifyBooleanArgumentParsed(TestObject.Provided);
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
        public void ThrowsWithInvalidArgumentAlias()
        {
            MockArgumentCrawler.LoadScript(InvalidArgumentAliasScript);
            Assert.ThrowsException<KeyNotFoundException>(() => TestObject.Parse(MockArgumentCrawler));
        }
        private string[] InvalidArgumentAliasScript => new string[]
        {
            nameof(ThrowsWithInvalidArgumentAlias),
        };

        [TestMethod]
        public void ThrowsWithDuplicateArgument()
        {
            MockArgumentCrawler.LoadScript(DuplicateArgumentScript);
            Assert.ThrowsException<DuplicateArgumentException>(() => TestObject.Parse(MockArgumentCrawler));
        }
        private string[] DuplicateArgumentScript => new string[]
        {
            Implementation.Argument.PrependIdentifier(nameof(Implementation.BooleanArgument)),
            Implementation.Argument.PrependIdentifier(nameof(Implementation.BooleanArgument)),
        };

        [TestMethod]
        public void DifferentAliasesAreStillDuplicate()
        {
            MockArgumentCrawler.LoadScript(DifferentDuplicateAliases);
            Assert.ThrowsException<DuplicateArgumentException>(() => TestObject.Parse(MockArgumentCrawler));
        }
        private string[] DifferentDuplicateAliases => new string[]
        {
            Implementation.Argument.PrependIdentifier(nameof(Implementation.BooleanArgument)),
            Implementation.Argument.PrependIdentifier($"{nameof(Implementation.BooleanArgument)}{nameof(Implementation.BooleanArgument.Aliases)}"),
        };

        [TestMethod]
        public void ThrowsIfRequiredArgumentMissing()
        {
            MockArgumentCrawler.LoadScript(MissingRequiredArgumentScript);
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.Parse(MockArgumentCrawler));
        }
        private string[] MissingRequiredArgumentScript => new string[]
        {
            Implementation.Argument.PrependIdentifier(nameof(Implementation.BooleanArgument)),
        };

        [TestMethod]
        public void AllowsOptionalArgumentMissing()
        {
            _obj = new Implementation.ParsedArguments(BuildMap(false, true));
            MockArgumentCrawler.LoadScript(MissingRequiredArgumentScript);
            TestObject.Parse(MockArgumentCrawler);

            Assert.AreEqual(1, TestObject.Provided.Count);
            VerifyBooleanArgumentParsed(TestObject.Provided);
        }

        [TestMethod]
        public void ThrowsIfRequiredBooleanArgumentMissing()
        {
            MockArgumentCrawler.LoadScript(MissingRequiredBooleanArgumentScript);
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.Parse(MockArgumentCrawler));
        }
        private string[] MissingRequiredBooleanArgumentScript => new string[]
        {
            Implementation.Argument.PrependIdentifier(nameof(Implementation.Argument)),
            One,
            Two,
            Three,
        };

        [TestMethod]
        public void AllowsOptionalBooleanArgumentMissing()
        {
            _obj = new Implementation.ParsedArguments(BuildMap(true, false));
            MockArgumentCrawler.LoadScript(MissingRequiredBooleanArgumentScript);
            TestObject.Parse(MockArgumentCrawler);

            Assert.AreEqual(1, TestObject.Provided.Count);
            VerifyArgumentParsed(TestObject.Provided);
        }
    }
}
