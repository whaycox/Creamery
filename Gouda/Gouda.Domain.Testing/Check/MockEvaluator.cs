using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Infrastructure.Check;
using Gouda.Application.Communication;
using Gouda.Application.Persistence;

namespace Gouda.Domain.Check
{
    using Enumerations;

    public class MockEvaluator : Evaluator
    {
        public MockEvaluator(INotifier notifier, IPersistence persistence)
            : base(notifier, persistence)
        { }

        public void FireEvent(Definition definition) => Notifier.NotifyUsers(MockChange(definition)).GetAwaiter().GetResult();

        private StatusChange MockChange(Definition definition) => new StatusChange()
        {
            Definition = definition,
            Old = Status.Unknown,
            New = Status.Good,
            Response = new MockResponse()
        };
    }
}
