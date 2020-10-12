using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace Parmesan.Server.Implementation
{
    using Abstraction;
    using Parmesan.Domain;
    using Domain;

    internal class OidcProviderMetadataFactory : IOidcProviderMetadataFactory
    {
        private OidcSettings Settings { get; }

        public OidcProviderMetadataFactory(IOptions<OidcSettings> oidcOptions)
        {
            Settings = oidcOptions.Value;
        }

        public OidcProviderMetadata Build() => new OidcProviderMetadata
        {
            AuthorizationEndpoint = UriPath.Combine(Settings.Issuer, ServerConstants.AuthorizeRoute),
        };
    }
}
