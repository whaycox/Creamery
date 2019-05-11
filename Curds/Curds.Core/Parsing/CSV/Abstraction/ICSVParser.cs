using System.Collections.Generic;
using System.IO;

namespace Curds.Parsing.CSV.Abstraction
{
    using Domain;

    public interface ICSVParser
    {
        IEnumerable<Row> Parse(Stream stream);
    }
}
