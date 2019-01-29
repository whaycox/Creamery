using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;

namespace Gouda.Application.Communication
{
    public delegate Response RequestHandler(Request request);

    public interface IListener : IDisposable
    {
        RequestHandler Handler { get; set; }

        bool IsStarted { get; }

        void Start();
        void Stop();
    }
}
