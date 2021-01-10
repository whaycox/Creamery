namespace Curds.Persistence.Domain
{
    using Abstraction;

    public abstract class BaseSimpleEntity : BaseEntity, ISimpleEntity
    {
        public sealed override object[] Keys => new object[] { ID };
        public int ID { get; set; }
    }
}
