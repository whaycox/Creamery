using System;
using System.Collections.Generic;

namespace Gouda.Domain.Check
{
    using Communication;

    public class Response : CommunicableObject
    {
        public Dictionary<string, string> Arguments { get; }

        public Response(Response response)
            : this(response.Arguments)
        { }

        public Response(Dictionary<string, string> args)
        {
            Arguments = args;
        }

        public override bool Equals(object obj)
        {
            Response toTest = obj as Response;
            if (toTest == null)
                return false;
            if (toTest.Arguments.Count != Arguments.Count)
                return false;
            foreach (var pair in toTest.Arguments)
            {
                if (!Arguments.ContainsKey(pair.Key))
                    return false;
                if (!pair.Value.Equals(Arguments[pair.Key]))
                    return false;
            }

            return true;
        }

        public byte[] ToBytes()
        {
            List<byte> buffer = new List<byte>();
            buffer.AddRange(BitConverter.GetBytes(Arguments.Count));
            foreach (var pair in Arguments)
            {
                buffer = AddStringToBuffer(buffer, pair.Key);
                buffer = AddStringToBuffer(buffer, pair.Value);
            }

            return buffer.ToArray();
        }

        public static Response Parse(byte[] buffer)
        {
            int index = 0;
            int numberOfArguments = ReadNumber(buffer, ref index);
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            for (int i = 0; i < numberOfArguments; i++)
            {
                string key = ReadString(buffer, ref index);
                string value = ReadString(buffer, ref index);
                arguments.Add(key, value);
            }

            return new Response(arguments);
        }
    }
}
