using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Text;

namespace Queso.CLI
{
    public abstract class OptionValue
    {
        public const string Indent = "\t";

        public abstract string Syntax { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }

        public abstract void Append(IIndentedWriter writer);
    }
}
