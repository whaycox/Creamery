using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Tests
{
    using Implementation;
    using Persistence.Abstraction;
    using Abstraction;
    using Domain;

    [TestClass]
    public class ModelMapTest
    {
        private Mock<IModelMapper> MockModelMapper = new Mock<IModelMapper>();
        private Dictionary<string, Table> TestTablesByName = new Dictionary<string, Table>();
        private Dictionary<Type, Table> TestTablesByType = new Dictionary<Type, Table>();
        private Dictionary<Type, ValueEntityDelegate> TestValueEntityDelegates = new Dictionary<Type, ValueEntityDelegate>();

        private ModelMap<ITestDataModel> TestObject = null;

        [TestInitialize]
        public void Init()
        {
            MockModelMapper
                .Setup(mapper => mapper.MapTablesByName<ITestDataModel>())
                .Returns(TestTablesByName);
            MockModelMapper
                .Setup(mapper => mapper.MapTablesByType<ITestDataModel>())
                .Returns(TestTablesByType);
            MockModelMapper
                .Setup(mapper => mapper.MapValueEntityDelegates<ITestDataModel>())
                .Returns(TestValueEntityDelegates);

            TestObject = new ModelMap<ITestDataModel>(MockModelMapper.Object);
        }

        [TestMethod]
        public void BuildsTablesByNameFromMapper()
        {
            MockModelMapper.Verify(mapper => mapper.MapTablesByName<ITestDataModel>(), Times.Once);
            Assert.AreSame(TestTablesByName, TestObject.TablesByName);
        }

        [TestMethod]
        public void BuildsTablesByTypeFromMapper()
        {
            MockModelMapper.Verify(mapper => mapper.MapTablesByType<ITestDataModel>(), Times.Once);
            Assert.AreSame(TestTablesByType, TestObject.TablesByType);
        }

        [TestMethod]
        public void BuildsValueEntityDelegatesFromMapper()
        {
            MockModelMapper.Verify(mapper => mapper.MapValueEntityDelegates<ITestDataModel>(), Times.Once);
            Assert.AreSame(TestValueEntityDelegates, TestObject.ValueEntityBuilders);
        }

    }
}
