using System.Threading.Tasks;

namespace Parmesan.UI.Web.Abstraction
{
    using Parmesan.Domain;

    public interface IPkceFactory
    {
        CodeChallengeMethod CodeChallengeMethod { get; }

        Task<string> CodeVerifier();
        Task<string> CodeChallenge();

        void SetVerifier(string verifier);
    }
}
