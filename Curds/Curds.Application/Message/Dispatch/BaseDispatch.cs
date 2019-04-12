using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Message.Dispatch
{
    public abstract class BaseDispatch<T> : ReferencingObject<T> where T : CurdsApplication
    {
        private Dictionary<Type, BaseMessageDefinition<T>> Map { get; }

        public BaseDispatch(T application)
            : base(application)
        {
            Map = BuildMapping(new Dictionary<Type, BaseMessageDefinition<T>>());
        }

        protected abstract Dictionary<Type, BaseMessageDefinition<T>> BuildMapping(Dictionary<Type, BaseMessageDefinition<T>> requestMap);

        protected U Lookup<U>() where U : BaseMessageDefinition<T>
        {
            Type handlerType = typeof(U);
            if (!Map.TryGetValue(handlerType, out BaseMessageDefinition<T> handler))
                throw new KeyNotFoundException($"No handler found for {handlerType.FullName}");
            return handler as U;
        }
    }
}
