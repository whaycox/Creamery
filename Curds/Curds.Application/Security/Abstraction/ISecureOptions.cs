namespace Curds.Application.Security.Abstraction
{
    using Curds.Security.Abstraction;
    using Application.Abstraction;

    public interface ISecureOptions : ICurdsOptions
    {
        ISecurity Security { get; }
    }
}
