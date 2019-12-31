using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace Curds.Cron.Fields.Template
{
    using Cron.Abstraction;
    using Implementation;

    public abstract class BaseFieldTemplate
    {
        protected DateTime TestTime = new DateTime(10, 5, 5);

        protected Mock<ICronRange> MockFirstRange = new Mock<ICronRange>();
        protected Mock<ICronRange> MockSecondRange = new Mock<ICronRange>();

        internal abstract BaseField BaseFieldTestObject { get; }

        protected abstract void SetTestObject(IEnumerable<ICronRange> ranges);

        [TestInitialize]
        public void SetupBaseFieldTemplate()
        {
            List<ICronRange> mockRanges = new List<ICronRange> { MockFirstRange.Object, MockSecondRange.Object };
            SetTestObject(mockRanges);
        }

        [TestMethod]
        public void BuildsFieldFromRanges()
        {
            Assert.AreEqual(ExpectedRangeCount, BaseFieldTestObject.Ranges.Count);
            Assert.AreSame(MockFirstRange.Object, BaseFieldTestObject.Ranges[0]);
            Assert.AreSame(MockSecondRange.Object, BaseFieldTestObject.Ranges[1]);
        }
        private int ExpectedRangeCount = 2;

        [DataTestMethod]
        [DataRow(false, true)]
        [DataRow(true, false)]
        [DataRow(true, true)]
        public void IsActiveTrueIfAnyRangeTrue(bool firstIsActive, bool secondIsActive)
        {
            MockFirstRange
                .Setup(range => range.IsActive(It.IsAny<DateTime>()))
                .Returns(firstIsActive);
            MockSecondRange
                .Setup(range => range.IsActive(It.IsAny<DateTime>()))
                .Returns(secondIsActive);

            Assert.IsTrue(BaseFieldTestObject.IsActive(TestTime));
        }

        [TestMethod]
        public void IsActiveFalseIfAllRangesFalse()
        {
            MockFirstRange
                .Setup(range => range.IsActive(It.IsAny<DateTime>()))
                .Returns(false);
            MockSecondRange
                .Setup(range => range.IsActive(It.IsAny<DateTime>()))
                .Returns(false);

            Assert.IsFalse(BaseFieldTestObject.IsActive(TestTime));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ThrowsOnNullRanges()
        {
            SetTestObject(null);
        }
    }
}
