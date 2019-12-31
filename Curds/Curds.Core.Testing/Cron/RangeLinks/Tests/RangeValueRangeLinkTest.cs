using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Cron.RangeLinks.Tests
{
    using Cron.Abstraction;
    using Template;
    using Implementation;
    using Ranges.Implementation;

    [TestClass]
    public class RangeValueRangeLinkTest : BaseRangeLinkTemplate
    {
        private RangeValueRangeLink<ICronFieldDefinition> TestObject = null;
        protected override ICronRangeLink InterfaceTestObject => TestObject;

        private string TestRange(int low, int high) => $"{low}-{high}";

        [TestInitialize]
        public void Init()
        {
            TestObject = new RangeValueRangeLink<ICronFieldDefinition>(MockFieldDefinition.Object, MockRangeLink.Object);
        }

        [TestMethod]
        public void ReturnsExpectedRange()
        {
            ICronRange actual = TestObject.HandleParse(TestRange(TestAbsoluteMin, TestAbsoluteMax));

            Assert.IsInstanceOfType(actual, typeof(RangeValueRange<ICronFieldDefinition>));
        }

        [TestMethod]
        public void ReturnedRangeHasLowPopulated()
        {
            ICronRange actual = TestObject.HandleParse(TestRange(TestAbsoluteMin, TestAbsoluteMax));

            RangeValueRange<ICronFieldDefinition> range = (RangeValueRange<ICronFieldDefinition>)actual;
            Assert.AreEqual(TestAbsoluteMin, range.Low);
        }

        [TestMethod]
        public void ReturnedRangeHasHighPopulated()
        {
            ICronRange actual = TestObject.HandleParse(TestRange(TestAbsoluteMin, TestAbsoluteMax));

            RangeValueRange<ICronFieldDefinition> range = (RangeValueRange<ICronFieldDefinition>)actual;
            Assert.AreEqual(TestAbsoluteMax, range.High);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("  ")]
        [DataRow("5")]
        [DataRow("-5")]
        [DataRow("5-")]
        [DataRow("5--5")]
        [DataRow("Test-Test")]
        [DataRow("Test")]
        public void ReturnsNullOnNonRange(string testRange)
        {
            Assert.IsNull(TestObject.HandleParse(testRange));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ThrowsIfRangeIsInverted()
        {
            TestObject.HandleParse(TestRange(TestAbsoluteMax, TestAbsoluteMin));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ThrowsIfLowEndIsLessThanAbsoluteMin()
        {
            TestObject.HandleParse(TestRange(TestAbsoluteMin - 1, TestAbsoluteMax));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ThrowsIfHighEndIsLessThanAbsoluteMin()
        {
            TestObject.HandleParse(TestRange(TestAbsoluteMin, TestAbsoluteMin - 1));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ThrowsIfLowEndIsMoreThanAbsoluteMax()
        {
            TestObject.HandleParse(TestRange(TestAbsoluteMax + 1, TestAbsoluteMax));
        }

        [TestMethod]
        [ExpectedException(typeof(FormatException))]
        public void ThrowsIfHighEndIsMoreThanAbsoluteMax()
        {
            TestObject.HandleParse(TestRange(TestAbsoluteMin, TestAbsoluteMax + 1));
        }
    }
}
