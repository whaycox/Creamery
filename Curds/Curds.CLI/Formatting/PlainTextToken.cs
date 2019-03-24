using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.CLI.Formatting
{
    public class PlainTextToken : BaseTextToken
    {
        private string Value { get; }

        public PlainTextToken(string value)
        {
            Value = value;
        }

        public override void Write(ConsoleWriter writer) => writer.Write(Value);

        public static PlainTextToken Create(string value) => new PlainTextToken(value);
    }
}
