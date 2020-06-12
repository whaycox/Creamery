using System.Threading.Tasks;

namespace Curds.Persistence.Abstraction
{
    public interface IExecutableObject
    {
        Task Execute();
    }
}
