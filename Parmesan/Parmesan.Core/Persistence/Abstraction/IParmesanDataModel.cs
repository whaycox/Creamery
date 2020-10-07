using Curds.Persistence.Abstraction;

namespace Parmesan.Persistence.Abstraction
{
    using Parmesan.Domain;

    public interface IParmesanDataModel : IDataModel
    {
        Client Clients { get; }
        ClientRedirectionUri ClientRedirectionUris { get; }
    }
}
