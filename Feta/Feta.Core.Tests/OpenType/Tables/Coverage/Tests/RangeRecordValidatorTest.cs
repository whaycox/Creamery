using Curds;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Feta.OpenType.Tables.Coverage.Tests
{
    using Domain;
    using Exceptions;
    using Implementation;
    using Mock;

    [TestClass]
    public class RangeRecordValidatorTest : Test<RangeRecordValidator>
    {
        protected override RangeRecordValidator TestObject { get; } = new RangeRecordValidator();

        [TestMethod]
        public void CanAddGoodRanges()
        {
            foreach (RangeRecord range in MockRangeRecord.Samples)
                TestObject.Add(range);
        }

        [TestMethod]
        [ExpectedException(typeof(RangeFormatException))]
        public void MisorderedRangesThrowOnAdd()
        {
            foreach (RangeRecord range in MockRangeRecord.Misordered)
                TestObject.Add(range);
        }

        [TestMethod]
        [ExpectedException(typeof(RangeFormatException))]
        public void OverlappingRangesThrowOnAdd()
        {
            foreach (RangeRecord range in MockRangeRecord.Overlapping)
                TestObject.Add(range);
        }

        [TestMethod]
        [ExpectedException(typeof(RangeFormatException))]
        public void BadStartCoverageIndexThrowsOnAdd()
        {
            foreach (RangeRecord range in MockRangeRecord.BadStartCoverageIndex)
                TestObject.Add(range);
        }
    }
}
