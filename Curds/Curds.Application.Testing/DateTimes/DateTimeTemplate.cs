using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain;

namespace Curds.Application.DateTimes
{
    public abstract class DateTimeTemplate<T> : TestTemplate<T> where T : IDateTime
    {

        [TestMethod]
        public void CanFetchTime() => Assert.IsNotNull(TestObject.Fetch);
    }
}
