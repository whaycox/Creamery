using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Check
{
    using Communication;

    public class Request : CommunicableObject 
    {
        public Guid ID { get; }
        public Dictionary<string, string> Arguments { get; }

        public Request(Guid guid, Dictionary<string, string> args)
        {
            ID = guid;
            Arguments = args;
        }

        public byte[] ToBytes()
        {
            List<byte> buffer = new List<byte>();
            buffer = AddGuidToBuffer(buffer, ID);
            buffer.AddRange(BitConverter.GetBytes(Arguments.Count));
            foreach (var pair in Arguments)
            {
                buffer = AddStringToBuffer(buffer, pair.Key);
                buffer = AddStringToBuffer(buffer, pair.Value);
            }

            return buffer.ToArray();
        }

        public static Request Parse(byte[] buffer)
        {
            int index = 0;
            Guid id = ReadGuid(buffer, ref index);
            int numberOfArguments = ReadNumber(buffer, ref index);
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            for (int i = 0; i < numberOfArguments; i++)
            {
                string key = ReadString(buffer, ref index);
                string value = ReadString(buffer, ref index);
                arguments.Add(key, value);
            }

            return new Request(id, arguments);
        }
    }
}
