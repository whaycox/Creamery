namespace Curds.Persistence.EventArgs
{
    using Domain;

    public class EntityModified<T> : System.EventArgs where T : BaseEntity
    {
        public T Entity { get; }

        public EntityModified(T entity)
        {
            Entity = entity;
        }
    }
}
