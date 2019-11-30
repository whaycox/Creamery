namespace Gouda.WebApp.Adapters.Abstraction
{
    using Application.DeferredValues.Domain;

    public interface IDestinationAdapter
    {
        string Adapt(DestinationDeferredKey destination);
    }
}
