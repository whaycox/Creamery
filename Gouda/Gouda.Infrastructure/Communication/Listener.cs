using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Gouda.Domain.Check;

namespace Gouda.Infrastructure.Communication
{
    public class Listener : BaseListener
    {
        private Communicator Communicator = new Communicator();

        private CancellationTokenSource CancelSource { get; set; }
        private TcpListener Server { get; }

        public Listener(IPEndPoint endpoint)
        {
            Server = new TcpListener(endpoint.Address, endpoint.Port);
        }

        public override void Start()
        {
            base.Start();
            Server.Start();
            StartListeningThread();
        }
        private void StartListeningThread()
        {
            CancelSource = new CancellationTokenSource();
            Task.Factory.StartNew(() => ListenThread(CancelSource.Token), CancelSource.Token);
        }

        public override void Stop()
        {
            base.Stop();
            Server.Stop();
            StopListeningThread();
        }
        private void StopListeningThread()
        {
            CancelSource.Cancel();
            CancelSource = null;
        }

        private void ListenThread(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                try
                {
                    using (TcpClient client = Server.AcceptTcpClient())
                    using (NetworkStream stream = client.GetStream())
                    {
                        token.ThrowIfCancellationRequested();
                        Response response = Handler(Request.Parse(Communicator.ReadPacket(stream, client.ReceiveBufferSize)));
                        Communicator.Send(stream, response.ToBytes());
                    }
                }
                catch (SocketException socketException)
                {
                    if (socketException.Message != "A blocking operation was interrupted by a call to WSACancelBlockingCall")
                        throw socketException;
                }
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }
        #endregion
    }
}
