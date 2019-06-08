namespace Curds.Application.Security.Domain
{
    using Curds.Security.Abstraction;
    using Application.Domain;
    using Abstraction;
    using Command.Abstraction;

    public abstract class SecureApplication : CurdsApplication
    {
        internal ISecurity Security { get; }

        public ISecurityCommands SecurityCommands { get; }

        public SecureApplication(ISecureOptions options)
            : base(options)
        {
            Security = options.Security;
        }

        private class Commands : ISecurityCommands
        {
            public Command.CreateInitialUser.Definition CreateInitialUser { get; }
            public Command.Login.Definition Login { get; }
            public Command.LogoutSeries.Definition LogoutSeries { get; }
            public Command.ValidateSession.Definition ValidateSession { get; }

            public Commands(SecureApplication application)
            {
                CreateInitialUser = new Command.CreateInitialUser.Definition(application);
                Login = new Command.Login.Definition(application);
                LogoutSeries = new Command.LogoutSeries.Definition(application);
                ValidateSession = new Command.ValidateSession.Definition(application);
            }
        }
    }
}
