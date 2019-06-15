using System;
using System.Collections.Generic;

namespace Gouda.Communication.Domain
{
    using Abstraction;
    using Enumerations;

    public class Error : ICommunicableObject
    {
        public string ExceptionText { get; }

        public CommunicableType Type => CommunicableType.Error;

        public Error(Exception exception)
        {
            ExceptionText = exception.ToString();
        }

        public Error(BufferReader reader)
        {
            ExceptionText = reader.ParseString();
        }

        public List<byte> Content() => this
            .BuildBuffer()
            .Append(ExceptionText);
    }

    public class ErrorParser : IParser
    {
        public CommunicableType ParsedType => CommunicableType.Error;

        public ICommunicableObject Parse(BufferReader reader) => new Error(reader);
    }
}
