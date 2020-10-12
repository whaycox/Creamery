namespace Parmesan.Security.Domain
{
    using Abstraction;

    public abstract class BaseAuthenticationData : IAuthenticationData
    {
        public string UserName { get; set; }
    }
}
