using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Persistence.Model.Tests
{
    using Implementation;
    using Persistence.Domain;
    using Abstraction;
    using Persistence.Abstraction;

    [TestClass]
    public class DelegateMapperTest
    {
        private Mock<ITypeMapper> MockTypeMapper = new Mock<ITypeMapper>();

        private DelegateMapper TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new DelegateMapper(MockTypeMapper.Object);
        }

        [TestMethod]
        public void CanMapValueEntityDelegate()
        {
            ValueEntityDelegate valueEntityDelegate = TestObject.MapValueEntityDelegate<ITestDataModel>(typeof(TestEntity));
        }
    }
}
