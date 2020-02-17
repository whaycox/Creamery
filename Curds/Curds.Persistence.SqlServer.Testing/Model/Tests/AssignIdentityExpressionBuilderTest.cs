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

        private AssignIdentityExpressionBuilder TestObject = new AssignIdentityExpressionBuilder();

        [TestMethod]
        public void BuiltDelegateAssignsByteIdentity()
        {
            MockQueryReader
                .Setup(reader => reader.ReadByte(It.IsAny<int>()))
                .Returns(TestByteIdentity);
            AssignIdentityDelegate actual = TestObject.BuildAssignIdentityDelegate(typeof(OtherEntity), TestByteProperty);
            actual(MockQueryReader.Object, TestEntity);

            Assert.AreEqual(TestByteIdentity, TestEntity.ByteValue);
        }

        [TestMethod]
        public void BuiltDelegateAssignsShortIdentity()
        {
            MockQueryReader
                .Setup(reader => reader.ReadShort(It.IsAny<int>()))
                .Returns(TestShortIdentity);
            AssignIdentityDelegate actual = TestObject.BuildAssignIdentityDelegate(typeof(OtherEntity), TestShortProperty);
            actual(MockQueryReader.Object, TestEntity);

            Assert.AreEqual(TestShortIdentity, TestEntity.ShortValue);
        }

        [TestMethod]
        public void BuiltDelegateAssignsIntIdentity()
        {
            MockQueryReader
                .Setup(reader => reader.ReadInt(It.IsAny<int>()))
                .Returns(TestIntIdentity);
            AssignIdentityDelegate actual = TestObject.BuildAssignIdentityDelegate(typeof(OtherEntity), TestIntProperty);
            actual(MockQueryReader.Object, TestEntity);

            Assert.AreEqual(TestIntIdentity, TestEntity.IntValue);
        }

        [TestMethod]
        public void BuiltDelegateAssignsLongIdentity()
        {
            MockQueryReader
                .Setup(reader => reader.ReadLong(It.IsAny<int>()))
                .Returns(TestLongIdentity);
            AssignIdentityDelegate actual = TestObject.BuildAssignIdentityDelegate(typeof(OtherEntity), TestLongProperty);
            actual(MockQueryReader.Object, TestEntity);

            Assert.AreEqual(TestLongIdentity, TestEntity.LongValue);
        }
    }
}
