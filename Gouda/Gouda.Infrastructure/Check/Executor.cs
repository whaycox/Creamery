using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Check;
using Gouda.Domain;
using Gouda.Domain.Check;

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
                foreach (var handlerPair in AppDomain.CurrentDomain.LoadBasicConstructors<Guid, IRequestHandler>(nameSpace, (t) => t.ID))
                    Handlers.Add(handlerPair.key, handlerPair.instance);
        }

        public BaseResponse Perform(Request request) => Handlers[request.ID].Perform(request);
    }
}
