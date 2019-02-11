using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Domain
{
    public abstract class AcceptanceCase
    {
        public Action Delegate { get; }

        public AcceptanceCase(Action testDelegate)
        {
            Delegate = testDelegate;
        }

        public abstract void Test();
    }

    public class SuccessCase : AcceptanceCase
    {
        public SuccessCase(Action testDelegate)
            : base(testDelegate)
        { }

        public override void Test() => Delegate();
    }

    public class FailureCase<T> : AcceptanceCase where T : Exception
    {
        public FailureCase(Action testDelegate)
            : base(testDelegate)
        { }

        public override void Test() => Assert.ThrowsException<T>(Delegate);
    }
}
