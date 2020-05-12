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
    using Domain;
    using Abstraction;

    [TestClass]
    public class EntityModelTest
    {
        private Type TestType = typeof(EntityModelTest);

        private Mock<IValueModel> MockValueModel = new Mock<IValueModel>();

        private EntityModel TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new EntityModel(TestType);
        }

        [TestMethod]
        public void EntityTypeIsPassedInConstructor()
        {
            Assert.AreEqual(TestType, TestObject.EntityType);
        }

        [TestMethod]
        public void ValuesAreExposedByCollection()
        {
            TestObject.ValueModels.Add(Mock.Of<IValueModel>());
            TestObject.ValueModels.Add(Mock.Of<IValueModel>());
            TestObject.ValueModels.Add(Mock.Of<IValueModel>());

            CollectionAssert.AreEqual(TestObject.ValueModels, TestObject.Values.ToList());
        }

        [TestMethod]
        public void KeysAreExposedByCollection()
        {
            TestObject.KeyDefinition.Add(Mock.Of<IValueModel>());
            TestObject.KeyDefinition.Add(Mock.Of<IValueModel>());
            TestObject.KeyDefinition.Add(Mock.Of<IValueModel>());

            CollectionAssert.AreEqual(TestObject.KeyDefinition, TestObject.Keys.ToList());
        }

        [TestMethod]
        public void IdentityReturnsFirstIdentityValue()
        {
            MockValueModel
                .Setup(model => model.IsIdentity)
                .Returns(true);
            TestObject.ValueModels.Add(Mock.Of<IValueModel>());
            TestObject.ValueModels.Add(MockValueModel.Object);
            TestObject.ValueModels.Add(Mock.Of<IValueModel>());

            Assert.AreSame(MockValueModel.Object, TestObject.Identity);
        }

        [TestMethod]
        public void NoIdentityValuesIsNull()
        {
            Assert.IsNull(TestObject.Identity);
        }

        [TestMethod]
        public void NonIdentitiesDoesntReturnIdentityValue()
        {
            MockValueModel
                .Setup(model => model.IsIdentity)
                .Returns(true);
            TestObject.ValueModels.Add(Mock.Of<IValueModel>());
            TestObject.ValueModels.Add(MockValueModel.Object);
            TestObject.ValueModels.Add(Mock.Of<IValueModel>());

            IEnumerable<IValueModel> actual = TestObject.NonIdentities;

            Assert.AreEqual(2, actual.Count());
            foreach (IValueModel model in actual)
                Assert.AreNotSame(MockValueModel.Object, model);
        }

    }
}
