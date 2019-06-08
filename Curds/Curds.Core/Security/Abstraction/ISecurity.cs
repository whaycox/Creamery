using System.Threading.Tasks;

namespace Curds.Security.Abstraction
{
    using Credentials.Domain;
    using Domain;
    using Persistence.Abstraction;
    using Time.Abstraction;

    public interface ISecurity
    {
        ITime Time { get; }
        ISecurityPersistence Persistence { get; }

        Task<bool> Validate(string sessionIdentifier);

        Task<Authentication> Login(Password passwordCredentials);
        Task<Authentication> ReAuthenticate(string seriesID, string token);
        Task Logout(int userID);
        Task Logout(string seriesID);
    }
}
