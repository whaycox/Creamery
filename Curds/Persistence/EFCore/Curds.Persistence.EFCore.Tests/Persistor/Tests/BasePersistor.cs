using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Curds.Persistence.EFCore.Persistor.Tests
{
    using EFCore.Mock;
    using Persistence.Mock;

    [TestClass]
    public class BasePersistor : Template.MockContextTemplate<Mock.BasePersistor>
    {
        protected override Mock.BasePersistor TestObject { get; } = new Mock.BasePersistor();

        [TestMethod]
        public async Task CanUseContextImmediately()
        {
            using (Context context = TestObject.ExposedContext)
                Assert.AreEqual(Persistence.Mock.BaseEntity.Samples.Length, await context.Entities.CountAsync());
        }

        [TestMethod]
        public async Task CanUseSetToAccesData()
        {
            using (Context context = TestObject.ExposedContext)
            {
                DbSet<Persistence.Mock.BaseEntity> set = TestObject.ExposedSet(context);
                Assert.AreEqual(Persistence.Mock.BaseEntity.Samples.Length, await set.CountAsync());
            }
        }

    }
}
