using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;

namespace Gouda.Application.Check
{
    using Communication;

    public interface IEvaluator
    {
        INotifier Notifier { get; set; }

        void Evaluate(Definition definition, BaseResponse response);
    }
}
