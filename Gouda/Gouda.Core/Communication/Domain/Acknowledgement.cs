using System;
using System.Collections.Generic;

namespace Gouda.Communication.Domain
{
    using Abstraction;
    using Enumerations;

    public sealed class Acknowledgement : ICommunicableObject
    {
        public DateTimeOffset Time { get; }

        public CommunicableType Type => CommunicableType.Acknowledgement;

        public Acknowledgement(DateTimeOffset time)
        {
            Time = time;
        }

        public Acknowledgement(BufferReader reader)
        {
            Time = reader.ParseDateTime();
        }

        public List<byte> Content() => this
            .BuildBuffer()
            .Append(Time);
    }

    public sealed class AcknowledgementParser : IParser
    {
        public CommunicableType ParsedType => CommunicableType.Acknowledgement;

        public ICommunicableObject Parse(BufferReader reader) => new Acknowledgement(reader);
    }
}
