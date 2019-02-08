using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using System.Diagnostics;

namespace Gouda.Infrastructure
{
    public static class ReflectionExtensions
    {
        public static IEnumerable<(T key, U instance)> LoadBasicConstructors<T, U>(this AppDomain appDomain, string nameSpace, Func<U, T> keySelector) where U : class
        {
            foreach (Type type in appDomain.LoadTypes<U>(nameSpace))
            {
                U basicInstance = InvokeBasicConstructor<U>(type);
                yield return (keySelector(basicInstance), basicInstance);
            }
        }
        private static T InvokeBasicConstructor<T>(Type type) where T : class
        {
            var ctor = type.GetConstructor(new Type[0]);
            return ctor.Invoke(null) as T;
        }

        public static IEnumerable<Type> LoadTypes<T>(this AppDomain appDomain, string nameSpace)
        {
            foreach (Assembly assembly in appDomain.GetAssemblies())
                foreach (Type type in assembly.LoadTypes<T>(nameSpace))
                    yield return type;
        }
        private static IEnumerable<Type> LoadTypes<T>(this Assembly assembly, string nameSpace)
        {
            foreach (Type t in assembly.GetTypes())
            {
                if (typeof(T).IsAssignableFrom(t) && t.Namespace.StartsWith(nameSpace))
                    yield return t;
            }
        }
    }
}
