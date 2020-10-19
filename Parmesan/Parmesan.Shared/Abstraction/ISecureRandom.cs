using System.Threading.Tasks;

namespace Parmesan.Abstraction
{
    public interface ISecureRandom
    {
        string Generate(int bytes);
        Task<string> GenerateAsync(int bytes);
    }
}
