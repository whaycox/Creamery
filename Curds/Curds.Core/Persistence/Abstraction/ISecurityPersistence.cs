namespace Curds.Persistence.Abstraction
{
    using Persistor.Abstraction;
    using Security.Domain;

    public interface ISecurityPersistence
    {
        IUser<User> Users { get; }
        ISession<Session> Sessions { get; }
        IReAuth<ReAuth> ReAuthentications { get; }
    }
}
