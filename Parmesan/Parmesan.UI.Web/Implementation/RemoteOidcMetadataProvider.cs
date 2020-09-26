using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;

namespace Parmesan.UI.Web.Implementation
{
    using Abstraction;
    using Parmesan.Domain;

    internal class RemoteOidcMetadataProvider : IRemoteOidcMetadataProvider
    {
        private OidcProviderMetadata Metadata { get; set; }

        public async Task<OidcProviderMetadata> Fetch(HttpClient httpClient)
        {
            if (Metadata == null)
                Metadata = await httpClient.GetFromJsonAsync<OidcProviderMetadata>(OidcProviderMetadata.ConfigurationRoute);

            return Metadata;
        }
    }
}
