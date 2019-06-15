using System.Collections.Generic;

namespace Gouda.Communication.Mock
{
    using Abstraction;

    public class BufferReader : Domain.BufferReader
    {
        private static List<IParser> MockParsers
        {
            get
            {
                var toReturn = Parsers.DefaultParsers;
                toReturn.Add(new ICommunicableObjectParser());
                return toReturn;
            }
        }

        public BufferReader(byte[] buffer)
            : base(MockParsers, buffer)
        { }
    }
}
