using Curds.Persistence.Abstraction;

namespace Parmesan.Persistence.Abstraction
{
    using Parmesan.Domain;

    public interface IParmesanDataModel : IDataModel
    {
        Client Clients { get; }
        ClientRedirectionUri ClientRedirectionUris { get; }

        User Users { get; }
        PasswordAuthentication Passwords { get; }

        AuthorizationCode AuthorizationCodes { get; }
    }
}
