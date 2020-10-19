using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Parmesan.UI.Web.Implementation
{
    using Domain;
    using Parmesan.Abstraction;

    internal class SecureRandom : ISecureRandom
    {
        private IJSRuntime JavaScript { get; }

        public SecureRandom(IJSRuntime javaScript)
        {
            JavaScript = javaScript;
        }

        public string Generate(int bytes) => GenerateAsync(bytes).GetAwaiter().GetResult();
        public async Task<string> GenerateAsync(int bytes)
        {
            string random = await JavaScript.InvokeAsync<string>(JavaScriptFunctionNames.GenerateRandom, bytes);
            return random.Base64UrlEncode();
        }
    }
}
