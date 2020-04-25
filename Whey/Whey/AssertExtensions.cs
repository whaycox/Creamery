using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Whey
{
    public static class AssertExtensions
    {
        public static TTarget VerifyIsActually<TTarget>(this object source)
        {
            Assert.IsInstanceOfType(source, typeof(TTarget));
            return (TTarget)source;
        }

    }
}
