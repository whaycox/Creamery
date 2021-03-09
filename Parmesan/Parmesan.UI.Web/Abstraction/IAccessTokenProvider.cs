using System.Net.Http;
using System.Threading.Tasks;

namespace Parmesan.UI.Web.Abstraction
{
    using Parmesan.Domain;

    public interface IAccessTokenProvider
    {
        Task<AccessTokenResponse> Request(HttpClient httpClient, string authorizationCode);
    }
}
