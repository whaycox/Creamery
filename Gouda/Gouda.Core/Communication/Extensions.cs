using System;
using System.Collections.Generic;
using System.IO;

namespace Gouda.Communication
{
    using Abstraction;
    using Enumerations;

    public static class Extensions
    {
        public static List<byte> BuildBuffer(this ICommunicableObject communicableObject) => new List<byte>().Append(communicableObject.Type);
        public static Stream ConvertToStream(this List<byte> buffer) => new MemoryStream(buffer.ToArray());

        public static List<byte> Append(this List<byte> buffer, bool toAppend)
        {
            buffer.Add(Convert.ToByte(toAppend));
            return buffer;
        }
        public static List<byte> Append(this List<byte> buffer, int toAppend)
        {
            buffer.AddRange(BitConverter.GetBytes(toAppend));
            return buffer;
        }
        public static List<byte> Append(this List<byte> buffer, long toAppend)
        {
            buffer.AddRange(BitConverter.GetBytes(toAppend));
            return buffer;
        }

        public static List<byte> Append(this List<byte> buffer, string toAppend)
        {
            if (string.IsNullOrWhiteSpace(toAppend))
                AppendInternal(buffer, string.Empty);
            else
                AppendInternal(buffer, toAppend);

            return buffer;
        }
        private static void AppendInternal(List<byte> buffer, string checkedString)
        {
            byte[] toAdd = Constants.TextEncoding.GetBytes(checkedString);

            buffer.AddRange(BitConverter.GetBytes(toAdd.Length));
            buffer.AddRange(toAdd);
        }

        public static List<byte> Append(this List<byte> buffer, DateTimeOffset toAppend)
        {
            buffer.AddRange(BitConverter.GetBytes(toAppend.Ticks));
            buffer.AddRange(BitConverter.GetBytes(toAppend.Offset.Ticks));
            return buffer;
        }

        public static List<byte> Append(this List<byte> buffer, CommunicableType type) => buffer.Append((int)type);
    }
}
