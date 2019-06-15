using System;
using System.Collections.Generic;

namespace Gouda.Check.Domain
{
    using Communication;
    using Communication.Abstraction;
    using Communication.Domain;
    using Communication.Enumerations;

    public class Request : ICommunicableObject
    {
        public Guid ID { get; }
        public Dictionary<string, string> Arguments { get; }

        public CommunicableType Type => CommunicableType.Request;

        public Request(Guid id, Dictionary<string, string> arguments)
        {
            ID = id;
            Arguments = arguments;
        }

        public Request(BufferReader reader)
        {
            ID = reader.ParseGuid();
            Arguments = reader.ParseArguments();
        }

        public List<byte> Content() => this
            .BuildBuffer()
            .Append(ID)
            .Append(Arguments);
    }

    public class RequestParser : IParser
    {
        public CommunicableType ParsedType => CommunicableType.Request;

        public ICommunicableObject Parse(BufferReader reader) => new Request(reader);
    }
}
