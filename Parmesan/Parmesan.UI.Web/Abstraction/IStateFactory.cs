using System.Threading.Tasks;

namespace Parmesan.UI.Web.Abstraction
{
    public interface IStateFactory
    {
        Task<string> Generate();
    }
}
