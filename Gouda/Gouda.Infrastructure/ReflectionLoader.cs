using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Infrastructure
{
    public abstract class ReflectionLoader<T, U> where U : class
    {
        protected Dictionary<T, U> Loaded = new Dictionary<T, U>();

        public ReflectionLoader()
        {
            LoadHandlers();
        }

        private void LoadHandlers()
        {
            foreach (string nameSpace in NamespacesToSearch)
                foreach (var pair in AppDomain.CurrentDomain.LoadKeyInstancePairs<T, U>(nameSpace, KeySelector))
                    Loaded.Add(pair.key, pair.instance);
        }
        protected abstract IEnumerable<string> NamespacesToSearch { get; }
        protected abstract T KeySelector(U instance);

    }
}
