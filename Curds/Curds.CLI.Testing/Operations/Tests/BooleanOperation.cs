using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.Application;
using Curds.Domain.CLI;
using Curds.Domain.CLI.Operations;
using Curds.Domain.Application.Message.Query;

namespace Curds.CLI.Operations.Tests
{
    [TestClass]
    public class BooleanOperation : OperationTemplate<Operations.BooleanOperation>
    {
        private Operations.BooleanOperation _obj = null;
        protected override Operations.BooleanOperation TestObject => _obj;

        protected override int ExpectedUsageWrites => 19;

        [TestInitialize]
        public void BuildObj()
        {
            _obj = new MockBooleanOperation();
        }
        
        protected override void VerifyUsage(MockConsoleWriter writer) =>
            writer.StartsWith(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Operation))
                .ThenHas(NewLine(false))
                .ThenHas(AliasedOptionValue.AliasStart)
                .ThenHas($"{Operations.Operation.OperationIdentifier}{nameof(MockBooleanOperation)}")
                .ThenHas(AliasedOptionValue.AliasSeparator)
                .ThenHas($"{Operations.Operation.OperationIdentifier}{nameof(MockBooleanOperation)}{nameof(MockBooleanOperation.Aliases)}")
                .ThenHas(AliasedOptionValue.AliasEnd)
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas(" ")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true))
                .ThenHas(TextColor(CLIEnvironment.Operation))
                .ThenHas(NewLine(false))
                .ThenHas(nameof(MockBooleanOperation))
                .ThenHas(TextColor(CLIEnvironment.DefaultTextColor))
                .ThenHas($": {nameof(MockBooleanOperation)}{nameof(MockBooleanOperation.Description)}")
                .ThenHas(Environment.NewLine)
                .ThenHas(NewLine(true));
    }
}
