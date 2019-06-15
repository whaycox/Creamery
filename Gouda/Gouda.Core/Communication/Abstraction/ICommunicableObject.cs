using System.Collections.Generic;

namespace Gouda.Communication.Abstraction
{
    using Enumerations;

    public interface ICommunicableObject
    {
        CommunicableType Type { get; }

        List<byte> Content();
    }
}
