namespace Curds.Application.Command.Domain
{
    using Application.Domain;

    public abstract class BaseCommandDefinition<T, U, V> : BaseMessageDefinition<T, U, V>
        where T : CurdsApplication
        where U : BaseCommand
    {
        public BaseCommandDefinition(T application)
            : base(application)
        { }
    }

    public abstract class BaseCommandDefinition<T, U> : BaseMessageDefinition<T, U>
        where T : CurdsApplication
        where U : BaseCommand
    {
        public BaseCommandDefinition(T application)
            : base(application)
        { }
    }
}
