using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Check;
using Gouda.Domain.Check;
using Gouda.Domain.Check.Responses;
using Gouda.Domain.Enumerations;

namespace Gouda.Check
{
    public abstract class BaseCheck : IRequestHandler, IResponseHandler
    {
        public abstract Guid ID { get; }

        public abstract Success Perform(Request request);
        public abstract Status Evaluate(Success response);
    }

    public abstract class BaseCheck<T, U> : BaseCheck where T : Request where U : Success
    {
        public override Success Perform(Request request) => PerformInternal(BuildRequest(request));
        protected abstract Success PerformInternal(T request);
        protected abstract T BuildRequest(Request request);

        public override Status Evaluate(Success response) => EvaluateInternal(BuildResponse(response));
        protected abstract Status EvaluateInternal(U response);
        protected abstract U BuildResponse(Success response);
    }
}
