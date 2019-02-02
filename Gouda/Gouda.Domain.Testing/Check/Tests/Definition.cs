using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain.Persistence;

namespace Gouda.Domain.Check.Tests
{
    using Communication;

    [TestClass]
    public class Definition : NamedEntityTemplate<Check.Definition>
    {
        protected override Check.Definition Sample => new MockDefinition() { ID = 5, Name = nameof(Sample), SatelliteID = 5, Arguments = MockArgument.MockArguments };

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
            samples.right.Arguments = null;
            TestEquality(samples);
        }
        private void EmptyCollectionNotEqualsSample()
        {
            var samples = Samples;
            samples.right.Arguments.Clear();
            TestEquality(samples);
        }
        private void NullCollectionNotEqualsEmpty()
        {
            var samples = Samples;
            samples.left.Arguments = null;
            samples.right.Arguments.Clear();
            TestEquality(samples);
        }
        private void ContentChangesNotEqual()
        {
            var samples = Samples;
            samples.right.Arguments[0].Value = nameof(ContentChangesNotEqual);
            TestEquality(samples);
        }
        private void QuantityChangesNotEqual()
        {
            var samples = Samples;
            samples.right.Arguments.Add(MockArgument.Four);
            TestEquality(samples);
        }
    }
}
