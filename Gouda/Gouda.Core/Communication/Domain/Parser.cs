using System;

namespace Gouda.Communication.Domain
{
    using Abstraction;
    using Enumerations;

    public class Parser
    {
        private const int IntLengthInBytes = 4;
        private const int LongLengthInBytes = 8;

        private int Index = 0;

        private byte[] Buffer { get; }

        public Parser(byte[] buffer)
        {
            Buffer = buffer;
        }

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

        public CommunicableType ParseType() => (CommunicableType)ParseInt();

        public virtual ICommunicableObject ParseObject(CommunicableType type)
        {
            switch (type)
            {
                case CommunicableType.Acknowledgement:
                    return new Acknowledgement(this);
                case CommunicableType.Error:
                    return new Error(this);
                default:
                    throw new InvalidOperationException($"Unexpected type: {type}");
            }
        }
    }
}
