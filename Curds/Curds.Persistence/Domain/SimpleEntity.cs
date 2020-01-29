namespace Curds.Persistence.Domain
{
    public abstract class SimpleEntity : BaseEntity
    {
        public sealed override object[] Keys => new object[] { ID };
        public int ID { get; set; }
    }
}
