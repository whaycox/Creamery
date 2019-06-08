namespace Curds.Application.Security.Domain
{
    using Query.Domain;

    public class SecureQuery<T> : SecureMessage<T> where T : BaseQuery
    {
        public SecureQuery(string sessionID, T query)
            : base(sessionID, query)
        { }
    }
}
