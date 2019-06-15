using System;
using System.Collections.Generic;
using System.IO;

namespace Gouda.Communication
{
    using Abstraction;
    using Enumerations;
    using Check.Data.Enumerations;

    public static class Extensions
    {
        public static List<byte> BuildBuffer(this ICommunicableObject communicableObject) => new List<byte>().Append(communicableObject.Type);
        public static Stream ConvertToStream(this List<byte> buffer) => new MemoryStream(buffer.ToArray());

        public static List<byte> Append(this List<byte> buffer, byte toAppend)
        {
            buffer.Add(toAppend);
            return buffer;
        }
        public static List<byte> Append(this List<byte> buffer, List<byte> toAppend)
        {
            buffer.AddRange(toAppend);
            return buffer;
        }

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
        public static List<byte> Append(this List<byte> buffer, decimal toAppend)
        {
            foreach (int chunk in decimal.GetBits(toAppend))
                buffer.Append(chunk);
            return buffer;
        }

        public static List<byte> Append(this List<byte> buffer, Guid toAppend)
        {
            buffer.AddRange(toAppend.ToByteArray());
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

        public static List<byte> Append<T, U>(this List<byte> buffer, Dictionary<T, U> dictionary, Action<List<byte>, T> keyAppender, Action<List<byte>, U> valueAppender)
        {
            buffer.Append(dictionary.Count);
            foreach (var pair in dictionary)
            {
                keyAppender(buffer, pair.Key);
                valueAppender(buffer, pair.Value);
            }
            return buffer;
        }
        public static List<byte> Append(this List<byte> buffer, Dictionary<string, string> dictionary) =>
            Append(buffer, dictionary, (b, k) => b.Append(k), (b, v) => b.Append(v));

        public static List<byte> Append(this List<byte> buffer, CommunicableType type) => buffer.Append((int)type);
        public static List<byte> Append(this List<byte> buffer, SeriesType seriesType) => buffer.Append((byte)seriesType);
    }
}
