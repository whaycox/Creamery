namespace Curds.Application.Security.Domain
{
    using Application.Command.Domain;
    using Application.Domain;

    public abstract class SecureCommandDefinition<T, U, V> : BaseMessageDefinition<T, SecureCommand<U>, V>
        where T : CurdsApplication
        where U : BaseCommand
    {
        public SecureCommandDefinition(T application)
            : base(application)
        { }
    }
}
