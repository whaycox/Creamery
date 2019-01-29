using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Curds.Domain.Persistence
{
    public abstract class EntityTemplate<T> where T : Entity
    {
        protected abstract T Sample { get; }

        protected (T left, T right) Samples => (Sample, Sample);
        
        protected void TestEquality((T left, T right) pair, bool expectedToBeEqual = false)
        {
            if (expectedToBeEqual)
            {
                Assert.AreEqual(pair.left.GetHashCode(), pair.right.GetHashCode());
                Assert.AreEqual(pair.left, pair.right);
            }
            else
            {
                Assert.AreNotEqual(pair.left.GetHashCode(), pair.right.GetHashCode());
                Assert.AreNotEqual(pair.left, pair.right);
            }
        }

        [TestMethod]
        public void CloneEquality()
        {
            T left = Sample;
            T right = left.Clone() as T;
            Assert.AreNotSame(left, right);
            TestEquality((left, right), true);
        }

        [TestMethod]
        public void SampleEquality()
        {
            var samples = Samples;
            Assert.AreNotSame(samples.left, samples.right);
            TestEquality(samples, true);
        }

        [TestMethod]
        public void SampleNotEqualToNull()
        {
            T left = Sample;
            T right = null;
            Assert.AreNotEqual(left, right);
        }

        [TestMethod]
        public void IDEquality() => TestIntChange((e, v) => e.ID = v);

        protected void TestIntChange(Action<T, int> modifier)
        {
            var samples = Samples;
            modifier(samples.right, default);
            TestEquality(samples);
        }

        protected void TestStringChange(Action<T, string> modifier)
        {
            TestStringChange(modifier, null);
            TestStringChange(modifier, string.Empty);
            TestStringChange(modifier, nameof(TestStringChange));
            TestStringChangeOnBoth(modifier, null, string.Empty);
            TestStringChangeOnBoth(modifier, string.Empty, null);
        }
        private void TestStringChange(Action<T, string> modifier, string suppliedValue)
        {
            var samples = Samples;
            modifier(samples.right, suppliedValue);
            TestEquality(samples);
        }
        private void TestStringChangeOnBoth(Action<T, string> modifier, string leftValue, string rightValue)
        {
            var samples = Samples;
            modifier(samples.left, leftValue);
            modifier(samples.right, rightValue);
            TestEquality(samples);
        }
    }
}
