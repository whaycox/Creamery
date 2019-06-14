using System.IO;

namespace Gouda.Communication.Abstraction
{
    using Enumerations;

    public interface ICommunicableObject
    {
        CommunicableType Type { get; }

        Stream ObjectStream();
    }
}
