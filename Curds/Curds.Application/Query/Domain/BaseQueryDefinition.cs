namespace Curds.Application.Query.Domain
{
    using Application.Domain;

    public abstract class BaseQueryDefinition<T, U, V> : BaseMessageDefinition<T, U, V>
        where T : CurdsApplication
        where U : BaseQuery
        where V : BaseViewModel
    {
        public BaseQueryDefinition(T application)
            : base(application)
        { }
    }
}
