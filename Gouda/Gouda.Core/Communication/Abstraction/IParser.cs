namespace Gouda.Communication.Abstraction
{
    using Domain;
    using Enumerations;

    public interface IParser
    {
        CommunicableType ParsedType { get; }

        ICommunicableObject Parse(BufferReader reader);
    }
}
