using Gouda.Application.Check;
using Gouda.Domain.Check;
using Gouda.Domain.Enumerations;
using System;
using System.Collections.Generic;
using Gouda.Domain.Check.Responses;
using Gouda.Application.Communication;
using Gouda.Application.Persistence;

namespace Gouda.Infrastructure.Check
{
    public class Evaluator : ReflectionLoader<Guid, IResponseHandler>, IEvaluator
    {
        public INotifier Notifier { get; set; }
        public IPersistence Persistence { get; set; }

        protected override IEnumerable<string> NamespacesToSearch => LoadableItems.CheckNamespaces;

        public Evaluator(INotifier notifier, IPersistence persistence)
        {
            Notifier = notifier;
            Persistence = persistence;
        }

        public void Evaluate(Definition definition, BaseResponse response)
        {
            Status evaluated = EvaluateResponse(definition, response);
            if(definition.Status != evaluated)
            {
                Status old = definition.Status;
                Persistence.Definitions.Update(definition.ID, (d) => UpdateStatus(d, evaluated));
                StatusChange changeInformation = new StatusChange()
                {
                    Definition = definition,
                    Old = old,
                    New = evaluated,
                    Response = response,
                };
                Notifier.NotifyUsers(changeInformation);
            }
        }
        private Status EvaluateResponse(Definition definition, BaseResponse response)
        {
            Status evaluated = Status.Unknown;
            try
            {
                evaluated = EvaluateInternal(Loaded[definition.CheckID], response);
            }
            catch (Exception ex)
            {
                evaluated = Status.Critical;
            }
            return evaluated;
        }
        private Status EvaluateInternal(IResponseHandler handler, BaseResponse response)
        {
            if (response is Failure)
                return Status.Critical;
            else
                return handler.Evaluate((Success)response);
        }
        private Definition UpdateStatus(Definition definition, Status newStatus)
        {
            definition.Status = newStatus;
            return definition;
        }

        protected override Guid KeySelector(IResponseHandler instance) => instance.ID;
    }
}
