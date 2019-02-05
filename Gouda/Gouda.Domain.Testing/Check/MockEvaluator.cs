using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Infrastructure.Check;

namespace Gouda.Domain.Check
{
    public class MockEvaluator : Evaluator
    {
        public void FireEvent(Definition definition) => OnStatusChanged(definition, Enumerations.Status.Unknown, Enumerations.Status.Good);
    }
}
