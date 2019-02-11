using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Domain
{
    public class AcceptanceCase
    {
        public Action Delegate { get; set; }
        public bool ShouldSucceed { get; set; }

        public virtual void Test() => TestInternal<Exception>();

        protected void TestInternal<T>() where T : Exception
        {
            if (ShouldSucceed)
                Delegate();
            else
                Assert.ThrowsException<T>(Delegate);
        }
    }

    public class AcceptanceCase<T> : AcceptanceCase where T : Exception
    {
        public override void Test() => TestInternal<T>();
    }
}
