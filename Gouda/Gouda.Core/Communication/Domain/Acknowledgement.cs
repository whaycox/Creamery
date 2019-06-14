using System;
using System.IO;

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

        public Acknowledgement(Parser parser)
        {
            Time = parser.ParseDateTime();
        }

        public Stream ObjectStream() => this
            .BuildBuffer()
            .Append(Time)
            .ConvertToStream();
    }
}
