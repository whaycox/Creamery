using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Gouda.Check.Data.Domain
{
    using Communication.Abstraction;
    using Communication.Domain;
    using Communication.Enumerations;
    using Communication;

    public class Set : ICommunicableObject
    {
        public List<Series> Data { get; } = new List<Series>();

        public CommunicableType Type => CommunicableType.DataSet;

        public Set(IEnumerable<Series> series)
        {
            Data.AddRange(series);
        }

        public Set(BufferReader reader)
        {
            int seriesCount = reader.ParseInt();
            for (int i = 0; i < seriesCount; i++)
                Data.Add((Series)reader.ParseObject());
        }

        public List<byte> Content() => this
            .BuildBuffer()
            .Append(Data.Count)
            .Append(Data.SelectMany(s => s.Content()).ToList());
    }

    public class SetParser : IParser
    {
        public CommunicableType ParsedType => CommunicableType.DataSet;

        public ICommunicableObject Parse(BufferReader reader) => new Set(reader);
    }
}
