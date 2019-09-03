using System;
using System.Collections.Generic;
using System.Text;
using Curds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Feta.OpenType.Tables.ClassDefinition.Tests
{
    using Implementation;
    using Mock;
    using Domain;
    using Exceptions;

    [TestClass]
    public class ClassRangeRecordValidatorTest : Test<ClassRangeRecordValidator>
    {
        protected override ClassRangeRecordValidator TestObject { get; } = new ClassRangeRecordValidator();

        [TestMethod]
        public void CanAddFirstRecord()
        {
            TestObject.Add(MockClassRangeRecord.One);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullRecordThrowsOnAdd()
        {
            TestObject.Add(null);
        }

        [TestMethod]
        public void CanAddManyGoodRecords()
        {
            foreach (ClassRangeRecord classRangeRecord in MockClassRangeRecord.AllSamples)
                TestObject.Add(classRangeRecord);
        }

        [TestMethod]
        public void MisorderedRangeRecordsThrowOnAdd()
        {
            ClassRangeRecord[] misordered = MockClassRangeRecord.MisorderedSamples;
            TestObject.Add(misordered[0]);
            Assert.ThrowsException<RangeFormatException>(() => TestObject.Add(misordered[1]));
        }

        [TestMethod]
        public void OverlappingRangeRecordsThrowOnAdd()
        {
            ClassRangeRecord[] overlapping = MockClassRangeRecord.OverlappingSamples;
            TestObject.Add(overlapping[0]);
            Assert.ThrowsException<RangeFormatException>(() => TestObject.Add(overlapping[1]));
        }
    }
}
