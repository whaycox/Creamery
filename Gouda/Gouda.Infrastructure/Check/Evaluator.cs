using Gouda.Application.Check;
using Gouda.Domain.Check;
using Gouda.Domain.Enumerations;
using System;
using System.Collections.Generic;
using Gouda.Domain.Check.Responses;
using Gouda.Application.Communication;

namespace Gouda.Infrastructure.Check
{
    public class Evaluator : ReflectionLoader<Guid, IResponseHandler>, IEvaluator
    {
        public INotifier Notifier { get; set; }

        protected override IEnumerable<string> NamespacesToSearch => LoadableItems.CheckNamespaces;

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
        private Status EvaluateInternal(IResponseHandler handler, Success response)
        {
            throw new NotImplementedException();
        }

        protected override Guid KeySelector(IResponseHandler instance) => instance.ID;
    }
}
