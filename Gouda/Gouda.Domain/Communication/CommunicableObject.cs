using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gouda.Domain.Communication
{
    public abstract class CommunicableObject
    {
        private const int NumberLengthInBytes = 4;
        private const int GuidLengthInBytes = 16;
        private static readonly Encoding TextEncoding = Encoding.UTF8;

        protected List<byte> AddStringToBuffer(List<byte> buffer, string text)
        {
            byte[] raw = TextEncoding.GetBytes(text);
            buffer.AddRange(BitConverter.GetBytes(raw.Length));
            buffer.AddRange(raw);
            return buffer;
        }

        protected List<byte> AddGuidToBuffer(List<byte> buffer, Guid guid)
        {
            buffer.AddRange(guid.ToByteArray());
            return buffer;
        }

        protected static string ReadString(byte[] buffer, ref int index)
        {
            int stringLength = ReadNumber(buffer, ref index);
            string parsed = TextEncoding.GetString(buffer, index, stringLength);
            index += stringLength;
            return parsed;
        }

        protected static Guid ReadGuid(byte[] buffer, ref int index)
        {
            byte[] guid = buffer.Skip(index).Take(GuidLengthInBytes).ToArray();
            index += GuidLengthInBytes;
            return new Guid(guid);
        }

        protected static int ReadNumber(byte[] buffer, ref int index)
        {
            int parsed = BitConverter.ToInt32(buffer, index);
            index += NumberLengthInBytes;
            return parsed;
        }
    }
}
