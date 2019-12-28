using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Satellite.Tests
{
    using Implementation;
    using Domain;
    using Abstraction;
    using Gouda.Domain;

    [TestClass]
    public class CheckDefinitionMapperTest
    {
        private CheckDefinition TestCheckDefinition = new CheckDefinition();
        private string TestName = nameof(TestName);
        private Guid TestCheckID = Guid.NewGuid();

        private CheckDefinitionMapper TestObject = new CheckDefinitionMapper();

        [TestInitialize]
        public void Init()
        {
            TestCheckDefinition.Name = TestName;
            TestCheckDefinition.CheckID = TestCheckID;
        }

        [TestMethod]
        public void MapsName()
        {
            CheckViewModel viewModel = TestObject.Map(TestCheckDefinition);

            Assert.AreEqual(TestName, viewModel.Name);
        }

        [TestMethod]
        public void MapsCheckID()
        {
            CheckViewModel viewModel = TestObject.Map(TestCheckDefinition);

            Assert.AreEqual(TestCheckID, viewModel.CheckID);
        }
    }
}
