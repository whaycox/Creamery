using System.IO;

namespace Gouda.Communication.Mock
{
    using Domain;
    using Enumerations;

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

        public ICommunicableObject(Parser parser)
        {
            Name = parser.ParseString();
        }

        public Stream ObjectStream() => this
            .BuildBuffer()
            .Append(Name)
            .ConvertToStream();
    }
}
