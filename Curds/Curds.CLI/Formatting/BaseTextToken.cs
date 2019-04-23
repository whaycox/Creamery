using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.CLI.Formatting
{
    public abstract class BaseTextToken
    {
        public abstract void Write(IConsoleWriter writer);
    }
}
