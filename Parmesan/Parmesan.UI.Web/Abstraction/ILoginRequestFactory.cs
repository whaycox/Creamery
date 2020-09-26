using System.Threading.Tasks;
using System.Net.Http;

namespace Parmesan.UI.Web.Abstraction
{
    public interface ILoginRequestFactory
    {
        Task<string> Build(HttpClient httpClient);
    }
}
