using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Check
{
    using Communication;

    public class Request : CommunicableObject 
    {
        public string Name { get; }
        public Dictionary<string, string> Arguments { get; }

        public Request(string name, Dictionary<string, string> args)
        {
            Name = name;
            Arguments = args;
        }

        public override bool Equals(object obj)
        {
            Request toTest = obj as Request;
            if (toTest == null)
                return false;
            if (!toTest.Name.Equals(Name))
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
            buffer = AddStringToBuffer(buffer, Name);
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
            string name = ReadString(buffer, ref index);
            int numberOfArguments = ReadNumber(buffer, ref index);
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            for (int i = 0; i < numberOfArguments; i++)
            {
                string key = ReadString(buffer, ref index);
                string value = ReadString(buffer, ref index);
                arguments.Add(key, value);
            }

            return new Request(name, arguments);
        }
    }
}
