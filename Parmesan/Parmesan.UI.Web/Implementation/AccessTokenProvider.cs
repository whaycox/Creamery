using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parmesan.UI.Web.Implementation
{
    using Abstraction;
    using Domain;
    using Parmesan.Domain;

    internal class AccessTokenProvider : IAccessTokenProvider
    {
        private IWebAssemblyHostEnvironment HostEnvironment { get; }
        private IClientIDFactory ClientIDFactory { get; }
        private IRemoteOidcMetadataProvider MetadataProvider { get; }

        public AccessTokenProvider(
            IWebAssemblyHostEnvironment hostEnvironment,
            IClientIDFactory clientIDFactory,
            IRemoteOidcMetadataProvider metadataProvider)
        {
            HostEnvironment = hostEnvironment;
            ClientIDFactory = clientIDFactory;
            MetadataProvider = metadataProvider;
        }

        public async Task<AccessTokenResponse> Request(HttpClient httpClient, string authorizationCode)
        {
            OidcProviderMetadata metadata = await MetadataProvider.Fetch(httpClient);
            AccessTokenRequest request = BuildRequest(authorizationCode);
            FormUrlEncodedContent requestContent = BuildContent(request);

            HttpResponseMessage message = await httpClient.PostAsync(metadata.TokenEndpoint, requestContent);


            throw new NotImplementedException();
        }
        private AccessTokenRequest BuildRequest(string authorizationCode) => new AccessTokenRequest
        {
            ClientID = ClientIDFactory.ClientID,
            GrantType = GrantType.authorization_code,
            RedirectUri = UriPath.Combine(HostEnvironment.BaseAddress, ClientConstants.LoginCallbackRoute),
            Code = authorizationCode,
        };
        private FormUrlEncodedContent BuildContent(AccessTokenRequest request)
        {
            Dictionary<string, string> arguments = new Dictionary<string, string>();
            arguments.Add(AccessTokenRequest.ClientIDName, request.ClientID);
            arguments.Add(AccessTokenRequest.GrantTypeName, request.GrantType.ToString());
            arguments.Add(AccessTokenRequest.RedirectUriName, request.RedirectUri);
            arguments.Add(AccessTokenRequest.CodeName, request.Code);

            return new FormUrlEncodedContent(arguments);
        }
    }
}
