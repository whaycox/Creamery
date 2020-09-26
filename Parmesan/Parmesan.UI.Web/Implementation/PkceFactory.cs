using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.JSInterop;
using System.Security.Cryptography;
using Microsoft.AspNetCore.WebUtilities;
using System.Threading.Tasks;

namespace Parmesan.UI.Web.Implementation
{
    using Abstraction;
    using Parmesan.Domain;

    internal class PkceFactory : IPkceFactory
    {
        private const int VerifierLengthInBytes = 96;

        private IJSRuntime JavaScript { get; }

        private string Verifier { get; set; }
        private string Challenge { get; set; }

        public async Task<string> CodeVerifier()
        {
            if (Verifier == null)
                Verifier = await GenerateCodeVerifier();
            return Verifier;
        }
        private async Task<string> GenerateCodeVerifier()
        {
            string random = await JavaScript.InvokeAsync<string>("generateRandom", VerifierLengthInBytes);
            byte[] randomBytes = Convert.FromBase64String(random);
            return Base64UrlTextEncoder.Encode(randomBytes);
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
                return Base64UrlTextEncoder.Encode(hasher.ComputeHash(Encoding.ASCII.GetBytes(await CodeVerifier())));
        }

        public CodeChallengeMethod CodeChallengeMethod => CodeChallengeMethod.S256;

        public PkceFactory(IJSRuntime javaScript)
        {
            JavaScript = javaScript;
        }
    }
}
