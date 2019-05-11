namespace Curds.Persistence.Mock
{
    using Persistence.Persistor.Abstraction;
    using Security.Domain;

    public class ISecurityPersistence : Abstraction.ISecurityPersistence
    {
        public Persistor.Mock.IUserPersistor MockUsers = new Persistor.Mock.IUserPersistor();
        public IUserPersistor<User> Users => MockUsers;

        public Persistor.Mock.ISessionPersistor MockSessions = new Persistor.Mock.ISessionPersistor();
        public ISessionPersistor<Session> Sessions => MockSessions;

        public Persistor.Mock.IReAuthPersistor MockReAuths = new Persistor.Mock.IReAuthPersistor();
        public IReAuthPersistor<ReAuth> ReAuthentications => MockReAuths;
    }
}
