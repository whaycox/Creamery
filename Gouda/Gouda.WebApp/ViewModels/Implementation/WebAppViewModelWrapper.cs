using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Gouda.WebApp.ViewModels.Implementation
{
    using Abstraction;
    using Application.Abstraction;
    using Domain;

    public class WebAppViewModelWrapper : IWebAppViewModelWrapper
    {
        private delegate IWebAppViewModel WrapperDelegate(IViewModel viewModel);

        private object Locker { get; } = new object();
        private Dictionary<Type, WrapperDelegate> Wrappers { get; } = new Dictionary<Type, WrapperDelegate>();

        public IWebAppViewModel Wrap(IViewModel viewModel)
        {
            WrapperDelegate wrapper = FetchDelegate(viewModel.GetType());
            return wrapper(viewModel);
        }
        private WrapperDelegate FetchDelegate(Type wrappedType)
        {
            if (!Wrappers.ContainsKey(wrappedType))
                AddWrapper(wrappedType);
            return Wrappers[wrappedType];
        }
        private void AddWrapper(Type wrappedType)
        {
            lock (Locker)
            {
                if (!Wrappers.ContainsKey(wrappedType))
                    Wrappers.Add(wrappedType, BuildDelegate(wrappedType));
            }
        }
        private WrapperDelegate BuildDelegate(Type wrappedType)
        {
            Type[] genericType = new Type[] { wrappedType };
            Type openModelType = typeof(WebAppViewModel<>);
            Type specificModelType = openModelType.MakeGenericType(genericType);

            ParameterExpression delegateParam = Expression.Parameter(typeof(IViewModel), nameof(IViewModel));
            UnaryExpression constructorParam = Expression.Convert(delegateParam, wrappedType);
            ConstructorInfo constructor = specificModelType.GetConstructor(genericType);
            NewExpression constructorExpression = Expression.New(constructor, new Expression[] { constructorParam });
            return Expression.Lambda<WrapperDelegate>(constructorExpression, new ParameterExpression[] { delegateParam }).Compile();
        }
    }
}
