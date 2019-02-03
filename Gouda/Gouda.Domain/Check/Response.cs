using System;
using System.Collections.Generic;

namespace Gouda.Domain.Check
{
    using Communication;
    using Enumerations;
    using Responses;

    public abstract class BaseResponse : CommunicableObject
    {
        public ResponseType Type { get; }
        public Dictionary<string, string> Arguments { get; }

        protected BaseResponse(Dictionary<string, string> args, ResponseType type)
        {
            Type = type;
            Arguments = args;
        }

        public override bool Equals(object obj)
        {
            BaseResponse toTest = obj as BaseResponse;
            if (toTest == null)
                return false;
            if (toTest.Type != Type)
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
            buffer.AddRange(BitConverter.GetBytes((int)Type));
            buffer.AddRange(BitConverter.GetBytes(Arguments.Count));
            foreach (var pair in Arguments)
            {
                buffer = AddStringToBuffer(buffer, pair.Key);
                buffer = AddStringToBuffer(buffer, pair.Value);
            }

            return buffer.ToArray();
        }

        public static BaseResponse Parse(byte[] buffer)
        {
            int index = 0;
            ResponseType type = (ResponseType)ReadNumber(buffer, ref index);
            int numberOfArguments = ReadNumber(buffer, ref index);
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            for (int i = 0; i < numberOfArguments; i++)
            {
                string key = ReadString(buffer, ref index);
                string value = ReadString(buffer, ref index);
                arguments.Add(key, value);
            }

            switch (type)
            {
                case ResponseType.Success:
                    return new Success(arguments);
                case ResponseType.Failure:
                    return new Failure(arguments);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
