using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Curds.Cron.Tests
{
    using Abstraction;
    using FieldFactories.Abstraction;
    using Implementation;

    [TestClass]
    public class CronExpressionFactoryTest
    {
        private string TestMinuteField = nameof(TestMinuteField);
        private string TestHourField = nameof(TestHourField);
        private string TestDayOfMonthField = nameof(TestDayOfMonthField);
        private string TestMonthField = nameof(TestMonthField);
        private string TestDayOfWeekField = nameof(TestDayOfWeekField);
        private string TestExpression = null;

        private Mock<IMinuteFieldFactory> MockMinuteFactory = new Mock<IMinuteFieldFactory>();
        private Mock<ICronField> MockMinuteField = new Mock<ICronField>();
        private Mock<IHourFieldFactory> MockHourFactory = new Mock<IHourFieldFactory>();
        private Mock<ICronField> MockHourField = new Mock<ICronField>();
        private Mock<IDayOfMonthFieldFactory> MockDayOfMonthFactory = new Mock<IDayOfMonthFieldFactory>();
        private Mock<ICronField> MockDayOfMonthField = new Mock<ICronField>();
        private Mock<IMonthFieldFactory> MockMonthFactory = new Mock<IMonthFieldFactory>();
        private Mock<ICronField> MockMonthField = new Mock<ICronField>();
        private Mock<IDayOfWeekFieldFactory> MockDayOfWeekFactory = new Mock<IDayOfWeekFieldFactory>();
        private Mock<ICronField> MockDayOfWeekField = new Mock<ICronField>();

        private CronExpressionFactory TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestExpression = $"{TestMinuteField} {TestHourField} {TestDayOfMonthField} {TestMonthField} {TestDayOfWeekField}";

            MockMinuteFactory
                .Setup(factory => factory.Parse(It.IsAny<string>()))
                .Returns(MockMinuteField.Object);
            MockHourFactory
                .Setup(factory => factory.Parse(It.IsAny<string>()))
                .Returns(MockHourField.Object);
            MockDayOfMonthFactory
                .Setup(factory => factory.Parse(It.IsAny<string>()))
                .Returns(MockDayOfMonthField.Object);
            MockMonthFactory
                .Setup(factory => factory.Parse(It.IsAny<string>()))
                .Returns(MockMonthField.Object);
            MockDayOfWeekFactory
                .Setup(factory => factory.Parse(It.IsAny<string>()))
                .Returns(MockDayOfWeekField.Object);

            TestObject = new CronExpressionFactory(
                MockMinuteFactory.Object,
                MockHourFactory.Object,
                MockDayOfMonthFactory.Object,
                MockMonthFactory.Object,
                MockDayOfWeekFactory.Object);
        }

        [TestMethod]
        public void ParsesMinuteField()
        {
            TestObject.Parse(TestExpression);

            MockMinuteFactory.Verify(factory => factory.Parse(TestMinuteField), Times.Once);
        }

        [TestMethod]
        public void ParsesHourField()
        {
            TestObject.Parse(TestExpression);

            MockHourFactory.Verify(factory => factory.Parse(TestHourField), Times.Once);
        }

        [TestMethod]
        public void ParsesDayOfMonthField()
        {
            TestObject.Parse(TestExpression);

            MockDayOfMonthFactory.Verify(factory => factory.Parse(TestDayOfMonthField), Times.Once);
        }

        [TestMethod]
        public void ParsesMonthField()
        {
            TestObject.Parse(TestExpression);

            MockMonthFactory.Verify(factory => factory.Parse(TestMonthField), Times.Once);
        }

        [TestMethod]
        public void ParsesDayOfWeekField()
        {
            TestObject.Parse(TestExpression);

            MockDayOfWeekFactory.Verify(factory => factory.Parse(TestDayOfWeekField), Times.Once);
        }

        [TestMethod]
        public void BuildsCronExpressionFromParsedFields()
        {
            ICronExpression actual = TestObject.Parse(TestExpression);

            Assert.IsInstanceOfType(actual, typeof(CronExpression));
            CronExpression expression = (CronExpression)actual;
            Assert.AreEqual(ExpectedFieldCount, expression.Fields.Count);
            Assert.AreSame(MockMinuteField.Object, expression.Fields[0]);
            Assert.AreSame(MockHourField.Object, expression.Fields[1]);
            Assert.AreSame(MockDayOfMonthField.Object, expression.Fields[2]);
            Assert.AreSame(MockMonthField.Object, expression.Fields[3]);
            Assert.AreSame(MockDayOfWeekField.Object, expression.Fields[4]);
        }
        private int ExpectedFieldCount = 5;

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("  ")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidStringThrows(string testExpression)
        {
            TestObject.Parse(testExpression);
        }

        [DataTestMethod]
        [DataRow("0 0 0 0")]
        [DataRow("0 0 0 0 0 0")]
        [ExpectedException(typeof(FormatException))]
        public void InvalidFieldsThrows(string testExpression)
        {
            TestObject.Parse(testExpression);
        }
    }
}
