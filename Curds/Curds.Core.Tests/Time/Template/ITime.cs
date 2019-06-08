using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Time.Template
{
    public abstract class ITime<T> : Test<T> where T : Abstraction.ITime
    {
        [TestMethod]
        public void CanFetchTime() => Assert.IsNotNull(TestObject.Fetch);
    }
}
