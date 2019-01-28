using Curds.Domain.Parsing.CSV;
using System.Collections.Generic;
using System.IO;

namespace Curds.Application.Parsing.CSV
{
    public interface IParser
    {
        IEnumerable<Row> Parse(Stream stream);
    }
}
