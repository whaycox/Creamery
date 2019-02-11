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
            TestChange(modifier, int.MinValue);
            TestChange(modifier, int.MaxValue);
            TestChange(modifier, default);
        }
        protected void TestStringChange(Action<T, string> modifier)
        {
            TestChange(modifier, null);
            TestChange(modifier, string.Empty);
            TestChange(modifier, nameof(TestStringChange));
            TestChangeOnBoth(modifier, null, string.Empty);
            TestChangeOnBoth(modifier, string.Empty, null);
        }
        protected void TestGuidChange(Action<T, Guid> modifier)
        {
            TestChange(modifier, Guid.Empty);
            TestChange(modifier, Guid.NewGuid());
        }
        protected void TestTimeSpanChange(Action<T, TimeSpan> modifier)
        {
            TestChange(modifier, TimeSpan.MinValue);
            TestChange(modifier, TimeSpan.MaxValue);
            TestChange(modifier, TimeSpan.FromMilliseconds(1));
        }

        private void TestChange<U>(Action<T, U> modifier, U suppliedValue)
        {
            var samples = Samples;
            modifier(samples.right, suppliedValue);
            TestEquality(samples);
        }
        private void TestChangeOnBoth<U>(Action<T, U> modifier, U leftValue, U rightValue)
        {
            var samples = Samples;
            modifier(samples.left, leftValue);
            modifier(samples.right, rightValue);
            TestEquality(samples);
        }
    }
}
