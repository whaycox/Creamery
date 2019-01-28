using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.Parsing.CSV
{
    public class NewLineHandlerCase
    {
        public char First { get; protected set; }
        public char? Second { get; protected set; }
        public NewLineHandler Handler { get; protected set; }
        public bool Expected { get; protected set; }
    }
}
