using System;
using System.Collections.Generic;
using System.Text;
using Curds.CLI;
using Curds.CLI.Formatting;

namespace Curds.Domain.CLI.Formatting
{
    public class MockTextToken : BaseTextToken
    {
        public const string Output = nameof(MockTextToken);

        public override void Write(IConsoleWriter writer) => writer.Write(Output);
    }
}
