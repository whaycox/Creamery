using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Check;
using Gouda.Domain;
using Gouda.Domain.Check;
using Gouda.Domain.Check.Responses;

namespace Gouda.Infrastructure.Check
{
    public class Executor : IExecutor
    {
        private Dictionary<Guid, IRequestHandler> Handlers = new Dictionary<Guid, IRequestHandler>();

        public Executor()
        {
            LoadHandlers();
        }
        private void LoadHandlers()
        {
            foreach (string nameSpace in LoadableItems.IRequestHandlerNamespaces)
                foreach (var handlerPair in AppDomain.CurrentDomain.LoadKeyInstancePairs<Guid, IRequestHandler>(nameSpace, (t) => t.ID))
                    Handlers.Add(handlerPair.key, handlerPair.instance);
        }

        public BaseResponse Perform(Request request)
        {
            try
            {
                return Handlers[request.ID].Perform(request);
            }
            catch(Exception ex)
            {
                return new Failure(ex);
            }
        }
    }
}
