using System.Threading.Tasks;

namespace Parmesan.UI.Web.Abstraction
{
    public interface ILoginRequestStorage
    {
        Task Store(string verifier, string state);
        Task<(string verifier, string state)> Consume();
    }
}
