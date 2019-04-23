using Curds.Domain.Security;
using System.Threading.Tasks;

namespace Curds.Application.Security
{
    using Command;
    using DateTimes;
    using Persistence;

    public interface ISecurity
    {
        IDateTime Time { get; }
        ISecurityPersistence Persistence { get; }

        Task<bool> Validate(ValidateSession command);
        Task<Authentication> Login(Login command);
        Task<Authentication> ReAuthenticate(ReAuthenticate command);
        Task Logout(LogoutUser command);
        Task Logout(LogoutSeries command);

        Task<Authentication> CreateInitialUser(CreateInitialUser command);
    }
}
