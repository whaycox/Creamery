using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;

namespace Gouda.Application.Communication
{
    public delegate BaseResponse RequestHandler(Request request);

    public interface IListener : IDisposable
    {
        RequestHandler Handler { get; }

        bool IsStarted { get; }

        void Start();
        void Stop();
    }
}
