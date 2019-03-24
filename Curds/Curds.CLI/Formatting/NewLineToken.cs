using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.CLI.Formatting
{
    public sealed class NewLineToken : PlainTextToken
    {
        public NewLineToken()
            : base(Environment.NewLine)
        { }

        public static NewLineToken New => new NewLineToken();
    }
}
