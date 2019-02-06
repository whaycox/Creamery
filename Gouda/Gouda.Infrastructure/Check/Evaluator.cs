using Gouda.Application.Check;
using Gouda.Domain.Check;
using Gouda.Domain.Enumerations;
using Gouda.Domain.EventArgs;
using System;

namespace Gouda.Infrastructure.Check
{
    public class Evaluator : IEvaluator
    {
        public event EventHandler<StatusChanged> StatusChanged;
        protected void OnStatusChanged(Definition definition, Status oldStatus, Status newStatus) => StatusChanged?.Invoke(this, new StatusChanged(definition, oldStatus, newStatus));

        public void Evaluate(Definition definition, BaseResponse response)
        {
            Status evaluated = definition.Evaluate(response);
            if (definition.Status != evaluated)
            {
                Status old = definition.Status;
                definition.Update(evaluated);
                OnStatusChanged(definition, old, evaluated);
            }
        }
    }
}
