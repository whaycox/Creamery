namespace Curds.Persistence.Query.Abstraction
{
    using Persistence.Domain;

    public interface ISqlQuery
    {
        void Write(ISqlQueryWriter queryWriter);
    }

    public interface ISqlQuery<TEntity> : ISqlQuery
        where TEntity : BaseEntity
    { }
}
