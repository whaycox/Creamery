namespace Curds.Persistence.EventArg
{
    using Domain;

    public class EntityModifiedArgs<T> : System.EventArgs where T : BaseEntity
    {
        public T Entity { get; }

        public EntityModifiedArgs(T entity)
        {
            Entity = entity;
        }
    }
}
