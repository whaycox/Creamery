﻿using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parmesan.UI.Web.Implementation
{
    using Abstraction;
    using Parmesan.Domain;

    internal class LoginRequestFactory : ILoginRequestFactory
    {
        private IWebAssemblyHostEnvironment HostEnvironment { get; }
        private IRemoteOidcMetadataProvider OidcProviderMetadataFactory { get; }
        private IClientIDFactory ClientIDFactory { get; }
        private IStateFactory StateFactory { get; }
        private IPkceFactory PkceFactory { get; }

        public LoginRequestFactory(
            IWebAssemblyHostEnvironment hostEnvironment,
            IRemoteOidcMetadataProvider oidcProviderMetadataFactory,
            IClientIDFactory clientIDFactory,
            IStateFactory stateFactory,
            IPkceFactory pkceFactory)
        {
            HostEnvironment = hostEnvironment;
            OidcProviderMetadataFactory = oidcProviderMetadataFactory;
            ClientIDFactory = clientIDFactory;
            StateFactory = stateFactory;
            PkceFactory = pkceFactory;
        }

        public async Task<string> Build(HttpClient httpClient)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add(AuthorizationRequest.ResponseTypeName, ResponseType.code.ToString());
            arguments.Add(AuthorizationRequest.ClientIDName, ClientIDFactory.ClientID);
            arguments.Add(AuthorizationRequest.RedirectUriName, UriPath.Combine(HostEnvironment.BaseAddress, "loginRedirect"));
            arguments.Add(AuthorizationRequest.ScopeName, "openid profile");
            string requestState = await StateFactory.Generate();
            arguments.Add(AuthorizationRequest.StateName, requestState);
            arguments.Add(AuthorizationRequest.CodeChallengeMethodName, PkceFactory.CodeChallengeMethod.ToString());
            arguments.Add(AuthorizationRequest.CodeChallengeName, await PkceFactory.CodeChallenge());

            OidcProviderMetadata metadata = await OidcProviderMetadataFactory.Fetch(httpClient);

            return QueryHelpers.AddQueryString(metadata.AuthorizationEndpoint, arguments);
        }
    }
}
