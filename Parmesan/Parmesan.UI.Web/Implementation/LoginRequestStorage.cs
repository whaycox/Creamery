using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Parmesan.UI.Web.Implementation
{
    using Abstraction;
    using Domain;

    internal class LoginRequestStorage : ILoginRequestStorage
    {
        private IJSRuntime JavaScript { get; }

        public LoginRequestStorage(IJSRuntime javaScript)
        {
            JavaScript = javaScript;
        }

        public Task Store(string verifier, string state) => JavaScript
            .InvokeVoidAsync(
                JavaScriptFunctionNames.StoreLoginRequest,
                verifier,
                state)
            .AsTask();

        public async Task<(string verifier, string state)> Consume()
        {
            string verifier = await JavaScript.InvokeAsync<string>(JavaScriptFunctionNames.ConsumeLoginRequest, "parmesan.loginVerifier");
            string state = await JavaScript.InvokeAsync<string>(JavaScriptFunctionNames.ConsumeLoginRequest, "parmesan.loginState");
            return (verifier, state);
        }
    }
}
