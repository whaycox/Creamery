using Gouda.Application.Check;
using Gouda.Domain.Check;
using Gouda.Domain.Enumerations;
using Gouda.Domain.EventArgs;
using System;
using System.Collections.Generic;

namespace Gouda.Infrastructure.Check
{
    public class Evaluator : ReflectionLoader<Guid, IResponseHandler>, IEvaluator
    {
        protected override IEnumerable<string> NamespacesToSearch => LoadableItems.CheckNamespaces;

        public event EventHandler<StatusChanged> StatusChanged;
        protected void OnStatusChanged(Definition definition, Status oldStatus, Status newStatus) => StatusChanged?.Invoke(this, new StatusChanged(definition, oldStatus, newStatus));

        public void Evaluate(Definition definition, BaseResponse response)
        {
            throw new NotImplementedException();
            //Status evaluated = definition.Evaluate(response);
            //if (definition.Status != evaluated)
            //{
            //    Status old = definition.Status;
            //    definition.Update(evaluated);
            //    OnStatusChanged(definition, old, evaluated);
            //}
        }

        protected override Guid KeySelector(IResponseHandler instance) => instance.ID;
    }
}
