namespace Curds.Persistence.Mock
{
    using Persistence.Persistor.Abstraction;
    using Security.Domain;

    public class ISecurityPersistence : Abstraction.ISecurityPersistence
    {
        public Persistor.Mock.IUser MockUsers = new Persistor.Mock.IUser();
        public IUser<User> Users => MockUsers;

        public Persistor.Mock.ISession MockSessions = new Persistor.Mock.ISession();
        public ISession<Session> Sessions => MockSessions;

        public Persistor.Mock.IReAuth MockReAuths = new Persistor.Mock.IReAuth();
        public IReAuth<ReAuth> ReAuthentications => MockReAuths;
    }
}
