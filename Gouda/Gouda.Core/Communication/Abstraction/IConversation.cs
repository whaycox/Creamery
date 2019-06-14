using System;
using System.Threading.Tasks;

namespace Gouda.Communication.Abstraction
{
    public interface IConversation : IDisposable
    {
        Task Send(ICommunicableObject communicableObject);
        Task<ICommunicableObject> ReceiveObject();
    }
}
