using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gouda.Communication.Abstraction
{
    public delegate Task ConversationHandler(IConversation conversation);

    public interface IListener : IDisposable
    {
        ConversationHandler Handler { get; set; }

        void Start();
        void Stop();
    }
}
