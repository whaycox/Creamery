using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Parmesan.UI.Web.Implementation
{
    using Abstraction;
    using Parmesan.Abstraction;
    using Parmesan.Domain;

    internal class PkceFactory : IPkceFactory
    {
        private const int VerifierLengthInBytes = 48;

        private ISecureRandom Random { get; }

        private string Verifier { get; set; }
        private string Challenge { get; set; }

        public CodeChallengeMethod CodeChallengeMethod => CodeChallengeMethod.S256;

        public PkceFactory(ISecureRandom random)
        {
            Random = random;
        }

        public async Task<string> CodeVerifier()
        {
            if (Verifier == null)
                Verifier = await Random.GenerateAsync(VerifierLengthInBytes);
            return Verifier;
        }

        public async Task<string> CodeChallenge()
        {
            if (Challenge == null)
                Challenge = await GenerateCodeChallenge();
            return Challenge;
        }
        private async Task<string> GenerateCodeChallenge()
        {
            using (SHA256 hasher = SHA256.Create())
                return hasher
                    .ComputeHash(Encoding.ASCII.GetBytes(await CodeVerifier()))
                    .Base64UrlEncode();
        }

        public void SetVerifier(string verifier)
        {
            if (Verifier != null)
                throw new InvalidOperationException("Cannot set a verifier when one is set");
            Challenge = null;
            Verifier = verifier;
        }
    }
}
