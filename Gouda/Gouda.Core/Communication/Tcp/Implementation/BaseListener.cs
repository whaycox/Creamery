using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System;

namespace Gouda.Communication.Tcp.Implementation
{
    using Abstraction;
    using Domain;
    using Communication.Domain;

    public abstract class BaseListener : IListener
    {
        private CancellationTokenSource CancelSource = new CancellationTokenSource();

        private InformativeTcpListener Listener { get; }

        public abstract ConversationHandler Handler { get; set; }

        public BaseListener(IPEndPoint endpoint)
        {
            Listener = new InformativeTcpListener(endpoint);
        }

        public void Start()
        {
            Listener.Start();
            Task.Run(() => Listen(CancelSource.Token));
        }
        private async Task Listen(CancellationToken cancellation)
        {
            while (!cancellation.IsCancellationRequested)
            {
                TcpClient client = await Listener.AcceptTcpClientAsync();
                Conversation conversation = BuildConversation(client);
                Task intentionallyNothing = Task.Run(() => HandleConversation(conversation));
                cancellation.ThrowIfCancellationRequested();
            }
        }
        protected virtual Conversation BuildConversation(TcpClient client) => new Conversation(client);
        private async Task HandleConversation(Conversation conversation)
        {
            try
            {
                if (Handler == null)
                    await conversation.Send(new Error(new InvalidOperationException($"No handler has been provided")));
                else
                    await Handler(conversation);
            }
            catch (Exception ex)
            {
                await conversation.Send(new Error(ex));
            }
            finally
            {
                conversation.Dispose();
            }
        }

        public void Stop()
        {
            Listener.Stop();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (Listener.IsActive)
                        Listener.Stop();
                }
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
