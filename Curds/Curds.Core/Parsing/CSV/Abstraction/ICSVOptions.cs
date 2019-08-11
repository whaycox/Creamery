using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Parsing.CSV.Abstraction
{
    using Reader.Domain;
    using Reader.Abstraction;

    public interface ICSVOptions : ICharReaderOptions
    {
        char Separator { get; set; }
        char Qualifier { get; set; }
    }
}
