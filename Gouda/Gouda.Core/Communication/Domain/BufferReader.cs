using System;
using System.Collections.Generic;
using System.Linq;

namespace Gouda.Communication.Domain
{
    using Abstraction;
    using Check.Data.Enumerations;
    using Check.Domain;
    using Enumerations;

    public class BufferReader
    {
        private const int IntLengthInBytes = 4;
        private const int LongLengthInBytes = 8;
        private const int GuidLengthInBytes = 16;
        private const int DecimalLengthInInts = 4;

        private int Index = 0;

        private Dictionary<CommunicableType, IParser> Parsers { get; }
        private byte[] Buffer { get; }

        public BufferReader(byte[] buffer)
            : this(Communication.Parsers.DefaultParsers, buffer)
        { }

        public BufferReader(IEnumerable<IParser> parsers, byte[] buffer)
        {
            Parsers = parsers.ToDictionary(k => k.ParsedType);
            Buffer = buffer;
        }

        public byte ParseByte() => Buffer[Index++];
        public bool ParseBoolean()
        {
            bool toReturn = BitConverter.ToBoolean(Buffer, Index);
            Index++;
            return toReturn;
        }
        public int ParseInt()
        {
            int toReturn = BitConverter.ToInt32(Buffer, Index);
            Index += IntLengthInBytes;
            return toReturn;
        }
        public long ParseLong()
        {
            long toReturn = BitConverter.ToInt64(Buffer, Index);
            Index += LongLengthInBytes;
            return toReturn;
        }
        public decimal ParseDecimal()
        {
            int[] bits = new int[DecimalLengthInInts];
            for (int i = 0; i < DecimalLengthInInts; i++)
                bits[i] = ParseInt();
            return new decimal(bits);
        }

        public Guid ParseGuid()
        {
            byte[] guid = Buffer
                .Skip(Index)
                .Take(GuidLengthInBytes)
                .ToArray();

            Guid toReturn = new Guid(guid);
            Index += GuidLengthInBytes;
            return toReturn;
        }

        public string ParseString()
        {
            int length = ParseInt();
            string toReturn = Constants.TextEncoding.GetString(Buffer, Index, length);
            Index += length;
            return toReturn;
        }

        public DateTimeOffset ParseDateTime()
        {
            long timeTicks = ParseLong();
            long offsetTicks = ParseLong();

            return new DateTimeOffset(timeTicks, TimeSpan.FromTicks(offsetTicks));
        }

        public Dictionary<T, U> ParseDictionary<T, U>(Func<BufferReader, T> keyParser, Func<BufferReader, U> valueParser)
        {
            Dictionary<T, U> toReturn = new Dictionary<T, U>();
            int count = ParseInt();
            for (int i = 0; i < count; i++)
            {
                T key = keyParser(this);
                U value = valueParser(this);
                toReturn.Add(key, value);
            }
            return toReturn;
        }
        public Dictionary<string, string> ParseArguments() => ParseDictionary((p) => p.ParseString(), (p) => p.ParseString());

        public CommunicableType ParseType() => (CommunicableType)ParseInt();
        public SeriesType ParseSeriesType() => (SeriesType)ParseByte();

        public ICommunicableObject ParseObject()
        {
            CommunicableType type = ParseType();
            return Parsers[type].Parse(this);
        }
    }
}
