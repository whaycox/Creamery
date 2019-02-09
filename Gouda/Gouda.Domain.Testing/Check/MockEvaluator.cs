using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Infrastructure.Check;

namespace Gouda.Domain.Check
{
    using Enumerations;

    public class MockEvaluator : Evaluator
    {
        public void FireEvent(Definition definition) => Notifier.NotifyUsers(MockChange(definition));

        private StatusChange MockChange(Definition definition) => new StatusChange()
        {
            Definition = definition,
            Old = Status.Unknown,
            New = Status.Good,
            Response = new MockResponse()
        };
    }
}
