using System.IO;
using System.Collections.Generic;

namespace Gouda.Communication.Mock
{
    using Domain;
    using Enumerations;
    using Abstraction;

    public class ICommunicableObject : Abstraction.ICommunicableObject
    {
        private string Name { get; }

        public CommunicableType Type => CommunicableType.Mock;

        public ICommunicableObject()
            : this(nameof(ICommunicableObject))
        { }

        public ICommunicableObject(string name)
        {
            Name = name;
        }

        public ICommunicableObject(Domain.BufferReader reader)
        {
            Name = reader.ParseString();
        }

        public List<byte> Content() => this
            .BuildBuffer()
            .Append(Name);
    }

    public class ICommunicableObjectParser : IParser
    {
        public CommunicableType ParsedType => CommunicableType.Mock;

        public Abstraction.ICommunicableObject Parse(Domain.BufferReader reader) => new ICommunicableObject(reader);
    }
}
