using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Domain.Check;
using Gouda.Domain.EventArgs;

namespace Gouda.Application.Check
{
    public interface IEvaluator
    {
        event EventHandler<StatusChanged> StatusChanged;
        void Evaluate(Definition definition, BaseResponse response);
    }
}
