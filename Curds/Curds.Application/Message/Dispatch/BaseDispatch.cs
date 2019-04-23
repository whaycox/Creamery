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
            Type definitionType = typeof(U);
            if (!Map.TryGetValue(definitionType, out BaseMessageDefinition<T> definition))
                throw new KeyNotFoundException($"No {nameof(definition)} found for {definitionType.FullName}");
            return definition as U;
        }
    }
}
