namespace Gouda.WebApp.DeferredValues.Abstraction
{
    using Application.Abstraction;
    using Application.DeferredValues.Domain;
    using Domain;

    public interface IDestinationDeferredValue : IDeferredValue<DestinationDeferredKey, DestinationItem>
    { }
}
