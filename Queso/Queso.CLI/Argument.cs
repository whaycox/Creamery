using System;
using System.Collections.Generic;
using System.Text;

namespace Queso.CLI
{
    public abstract class Argument : OptionValue
    {
        public const string ArgumentIdentifier = "--";

        public abstract IEnumerable<string> ArgumentAliases { get; }

        public abstract bool Required { get; }

        public abstract List<Value> Values { get; }

        public List<Value> Parse(ArgumentCrawler crawler)
        {
            throw new NotImplementedException();
        }
    }
}
