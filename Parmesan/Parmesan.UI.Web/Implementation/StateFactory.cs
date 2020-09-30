using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Parmesan.UI.Web.Implementation
{
    using Abstraction;

    internal class StateFactory : IStateFactory
    {
        private const int LengthInBytes = 12;

        private IJSRuntime JavaScript { get; }

        public StateFactory(IJSRuntime javaScript)
        {
            JavaScript = javaScript;
        }

        public Task<string> Generate() => JavaScript
            .InvokeAsync<string>("generateRandom", LengthInBytes)
            .AsTask();
    }
}
