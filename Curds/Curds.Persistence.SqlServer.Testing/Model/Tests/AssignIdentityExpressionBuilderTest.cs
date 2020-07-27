using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Reflection;

namespace Curds.Persistence.Model.Tests
{
    using Implementation;
    using Persistence.Domain;
    using Abstraction;
    using Query.Abstraction;

    [TestClass]
    public class AssignIdentityExpressionBuilderTest
    {
        private OtherEntity TestEntity = new OtherEntity();
        private byte TestByteIdentity = 156;
        private PropertyInfo TestByteProperty = typeof(OtherEntity).GetProperty(nameof(OtherEntity.ByteValue));
        private short TestShortIdentity = 1234;
        private PropertyInfo TestShortProperty = typeof(OtherEntity).GetProperty(nameof(OtherEntity.ShortValue));
        private int TestIntIdentity = 16;
        private PropertyInfo TestIntProperty = typeof(OtherEntity).GetProperty(nameof(OtherEntity.IntValue));
        private long TestLongIdentity = 12334546567;
        private PropertyInfo TestLongProperty = typeof(OtherEntity).GetProperty(nameof(OtherEntity.LongValue));

        private Mock<ISqlQueryReader> MockQueryReader = new Mock<ISqlQueryReader>();
        private Mock<IEntityModel> MockEntityModel = new Mock<IEntityModel>();
        private Mock<IValueModel> MockValueModel = new Mock<IValueModel>();

        private AssignIdentityExpressionBuilder TestObject = new AssignIdentityExpressionBuilder();

        [TestInitialize]
        public void Init()
        {
            MockEntityModel
                .Setup(model => model.EntityType)
                .Returns(typeof(OtherEntity));
            MockEntityModel
                .Setup(model => model.Identity)
                .Returns(MockValueModel.Object);
        }

        private void SetModelToIdentityProperty(PropertyInfo identityProperty)
        {
            MockValueModel
                .Setup(model => model.Property)
                .Returns(identityProperty);
        }

        [TestMethod]
        public void BuiltDelegateAssignsByteIdentity()
        {
            SetModelToIdentityProperty(TestByteProperty);
            MockQueryReader
                .Setup(reader => reader.ReadByte(It.IsAny<string>()))
                .Returns(TestByteIdentity);

            AssignIdentityDelegate actual = TestObject.BuildAssignIdentityDelegate(MockEntityModel.Object);
            actual(MockQueryReader.Object, TestEntity);

            Assert.AreEqual(TestByteIdentity, TestEntity.ByteValue);
        }

        [TestMethod]
        public void BuiltDelegateAssignsShortIdentity()
        {
            SetModelToIdentityProperty(TestShortProperty);
            MockQueryReader
                .Setup(reader => reader.ReadShort(It.IsAny<string>()))
                .Returns(TestShortIdentity);

            AssignIdentityDelegate actual = TestObject.BuildAssignIdentityDelegate(MockEntityModel.Object);
            actual(MockQueryReader.Object, TestEntity);

            Assert.AreEqual(TestShortIdentity, TestEntity.ShortValue);
        }

        [TestMethod]
        public void BuiltDelegateAssignsIntIdentity()
        {
            SetModelToIdentityProperty(TestIntProperty);
            MockQueryReader
                .Setup(reader => reader.ReadInt(It.IsAny<string>()))
                .Returns(TestIntIdentity);

            AssignIdentityDelegate actual = TestObject.BuildAssignIdentityDelegate(MockEntityModel.Object);
            actual(MockQueryReader.Object, TestEntity);

            Assert.AreEqual(TestIntIdentity, TestEntity.IntValue);
        }

        [TestMethod]
        public void BuiltDelegateAssignsLongIdentity()
        {
            SetModelToIdentityProperty(TestLongProperty);
            MockQueryReader
                .Setup(reader => reader.ReadLong(It.IsAny<string>()))
                .Returns(TestLongIdentity);

            AssignIdentityDelegate actual = TestObject.BuildAssignIdentityDelegate(MockEntityModel.Object);
            actual(MockQueryReader.Object, TestEntity);

            Assert.AreEqual(TestLongIdentity, TestEntity.LongValue);
        }
    }
}
