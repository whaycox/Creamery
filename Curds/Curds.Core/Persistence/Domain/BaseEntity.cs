namespace Curds.Persistence.Domain
{
    using Abstraction;

    public abstract class BaseEntity : IEntity
    {
        public abstract object[] Keys { get; }
    }
}
