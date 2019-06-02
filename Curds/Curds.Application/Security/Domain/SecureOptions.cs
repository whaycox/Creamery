namespace Curds.Application.Security.Domain
{
    using Abstraction;
    using Application.Domain;
    using Curds.Security.Abstraction;

    public abstract class SecureOptions : CurdsOptions, ISecureOptions
    {
        public abstract ISecurity Security { get; }
    }
}
