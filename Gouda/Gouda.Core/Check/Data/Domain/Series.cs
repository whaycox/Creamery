using System;
using System.Collections.Generic;

namespace Gouda.Check.Data.Domain
{
    using Communication;
    using Communication.Abstraction;
    using Communication.Domain;
    using Communication.Enumerations;
    using Enumerations;

    public abstract class Series : ICommunicableObject
    {
        public string Name { get; }

        public CommunicableType Type => CommunicableType.DataSeries;

        public abstract SeriesType SeriesType { get; }

        public Series(string name)
        {
            Name = name;
        }

        public List<byte> Content() => this
            .BuildBuffer()
            .Append(SeriesType)
            .Append(ContentBuffer());
        protected abstract List<byte> ContentBuffer();
    }

    public abstract class Series<T> : Series
        where T : struct
    {
        public T Value { get; }

        public Series(string name, T value)
            : base(name)
        {
            Value = value;
        }
    }

    public class SeriesParser : IParser
    {
        public CommunicableType ParsedType => CommunicableType.DataSeries;

        public ICommunicableObject Parse(BufferReader reader)
        {
            SeriesType seriesType = reader.ParseSeriesType();
            switch (seriesType)
            {
                case SeriesType.Int:
                    return new IntSeries(reader);
                case SeriesType.Long:
                    return new LongSeries(reader);
                case SeriesType.Decimal:
                    return new DecimalSeries(reader);
                default:
                    throw new InvalidOperationException($"Unexpected {nameof(seriesType)}: {seriesType}");
            }
        }
    }
}
