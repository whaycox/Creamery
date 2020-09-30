using Microsoft.JSInterop;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Parmesan.UI.Web.Implementation
{
    using Abstraction;
    using Parmesan.Domain;

    internal class PkceFactory : IPkceFactory
    {
        private const int VerifierLengthInBytes = 32;

        private IJSRuntime JavaScript { get; }

        private string Verifier { get; set; }
        private string Challenge { get; set; }

        public CodeChallengeMethod CodeChallengeMethod => CodeChallengeMethod.S256;

        public PkceFactory(IJSRuntime javaScript)
        {
            JavaScript = javaScript;
        }

        private string Base64UrlEncode(byte[] bytes) => //https://tools.ietf.org/html/rfc7636#appendix-A
            Base64UrlEncode(Convert.ToBase64String(bytes));
        private string Base64UrlEncode(string base64)
        {
            base64 = base64.Split('=')[0];
            base64 = base64.Replace('+', '-');
            base64 = base64.Replace('/', '_');
            return base64;
        }

        public async Task<string> CodeVerifier()
        {
            if (Verifier == null)
                Verifier = await GenerateCodeVerifier();
            return Verifier;
        }
        private async Task<string> GenerateCodeVerifier()
        {
            string random = await JavaScript.InvokeAsync<string>("generateRandom", VerifierLengthInBytes);
            return Base64UrlEncode(random);
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
                return Base64UrlEncode(
                    hasher.ComputeHash(
                        Encoding.ASCII.GetBytes(
                            await CodeVerifier())));
        }
    }
}
