using Curds.Persistence.Abstraction;

namespace Parmesan.Persistence.Abstraction
{
    using Parmesan.Domain;

    public interface IParmesanDatabase : IDatabase
    {
        IClientRepository Clients { get; }
        IUserRepository Users { get; }
        IRepository<IParmesanDataModel, PasswordAuthentication> Passwords { get; }
        IRepository<IParmesanDataModel, AuthorizationCode> AuthorizationCodes { get; }
    }
}
