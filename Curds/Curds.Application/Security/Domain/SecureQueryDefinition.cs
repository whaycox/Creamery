namespace Curds.Application.Security.Domain
{
    using Application.Domain;
    using Query.Domain;

    public abstract class SecureQueryDefinition<T, U, V> : BaseMessageDefinition<T, SecureQuery<U>, V>
        where T : CurdsApplication
        where U : BaseQuery
        where V : BaseViewModel
    {
        public SecureQueryDefinition(T application)
            : base(application)
        { }
    }
}
