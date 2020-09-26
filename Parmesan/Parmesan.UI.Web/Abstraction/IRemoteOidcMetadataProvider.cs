using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Parmesan.UI.Web.Abstraction
{
    using Parmesan.Domain;

    public interface IRemoteOidcMetadataProvider
    {
        Task<OidcProviderMetadata> Fetch(HttpClient httpClient);
    }
}
