using System.Collections.Generic;

namespace Parmesan.Implementation
{
    using Abstraction;

    internal class ScopeResolver : IScopeResolver
    {
        private static Dictionary<string, string> KnownScopes { get; } = new Dictionary<string, string>
        {
            { "openid", "Your Identity" },
            { "profile", "Your Profile" },
            { "email", "Your Email" },
            { "address", "Your Address" },
            { "phone", "Your Phone Number" },
        };

        public string Resolve(string scope)
        {
            if (KnownScopes.TryGetValue(scope, out string resolved))
                return resolved;
            return scope;
        }
    }
}
