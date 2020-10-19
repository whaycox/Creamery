namespace Parmesan.Security.Abstraction
{
    public interface IAuthenticationData
    {
        string UserName { get; }
        bool RememberMe { get; }
    }
}
