using Gouda.Application.Check;
using Gouda.Domain.Check;
using Gouda.Domain.Check.Responses;
using System;
using System.Collections.Generic;

namespace Gouda.Infrastructure.Check
{
    public class Executor : ReflectionLoader<Guid, IRequestHandler>, IExecutor
    {
        protected override IEnumerable<string> NamespacesToSearch => LoadableItems.CheckNamespaces;

        public BaseResponse Perform(Request request)
        {
            try
            {
                return Loaded[request.ID].Perform(request);
            }
            catch(Exception ex)
            {
                return new Failure(ex);
            }
        }

        protected override Guid KeySelector(IRequestHandler instance) => instance.ID;
    }
}
