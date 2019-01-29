using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Check;
using Gouda.Domain.Check;
using Gouda.Domain.EventArgs;
using Gouda.Domain.Enumerations;

namespace Gouda.Infrastructure.Check
{
    public class Evaluator : IEvaluator
    {
        public event EventHandler<StatusChanged> StatusChanged;
        protected void OnStatusChanged(Definition definition, Status oldStatus, Status newStatus) => StatusChanged?.Invoke(this, new StatusChanged(definition, oldStatus, newStatus));

        public void Evaluate(Definition definition, Response response)
        {
            Status evaluated = definition.Evaluate(response);
            if(definition.Status != evaluated)
            {
                Status old = definition.Status;
                definition.Update(evaluated);
                OnStatusChanged(definition, old, evaluated);
            }
        }
    }
}
