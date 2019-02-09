using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;

namespace Gouda.Application.Check
{
    using Communication;
    using Persistence;

    public interface IEvaluator
    {
        IPersistence Persistence { get; set; }
        INotifier Notifier { get; set; }

        void Evaluate(Definition definition, BaseResponse response);
    }
}
