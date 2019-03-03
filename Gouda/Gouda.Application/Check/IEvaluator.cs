using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;
using System.Threading.Tasks;

namespace Gouda.Application.Check
{
    using Communication;
    using Persistence;

    public interface IEvaluator
    {
        IPersistence Persistence { get; }
        INotifier Notifier { get; }

        Task Evaluate(Definition definition, BaseResponse response);
    }
}
