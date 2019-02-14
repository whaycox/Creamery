using Curds.Domain.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gouda.Domain.Check.Tests
{
    [TestClass]
    public class Definition : NamedEntityTemplate<Check.Definition>
    {
        protected override Check.Definition TestObject => MockDefinition.Sample;

        [TestMethod]
        public void SatelliteIDEquality() => TestIntChange((e, v) => e.SatelliteID = v);

        [TestMethod]
        public void ArgumentsEquality()
        {
            NullCollectionNotEqualsSample();
            EmptyCollectionNotEqualsSample();
            NullCollectionNotEqualsEmpty();
            ContentChangesNotEqual();
            QuantityChangesNotEqual();
        }
        private void NullCollectionNotEqualsSample()
        {
            var samples = Samples;
            samples.right.ArgumentIDs = null;
            TestEquality(samples);
        }
        private void EmptyCollectionNotEqualsSample()
        {
            var samples = Samples;
            samples.right.ArgumentIDs.Clear();
            TestEquality(samples);
        }
        private void NullCollectionNotEqualsEmpty()
        {
            var samples = Samples;
            samples.left.ArgumentIDs = null;
            samples.right.ArgumentIDs.Clear();
            TestEquality(samples);
        }
        private void ContentChangesNotEqual()
        {
            var samples = Samples;
            samples.right.ArgumentIDs[0] = int.MaxValue;
            TestEquality(samples);
        }
        private void QuantityChangesNotEqual()
        {
            var samples = Samples;
            samples.right.ArgumentIDs.Add(MockArgument.Four.ID);
            TestEquality(samples);
        }

        [TestMethod]
        public void CheckIDEquality() => TestGuidChange((e, v) => e.CheckID = v);

        [TestMethod]
        public void RescheduleSpanEquality() => TestTimeSpanChange((e, v) => e.RescheduleSpan = v);
    }
}
