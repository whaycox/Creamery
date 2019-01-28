using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Domain.Persistence
{
    public abstract class EntityTemplate<T> where T : Entity
    {
        protected abstract T Sample { get; }

        [TestMethod]
        public void ClonesAreNotSame()
        {
            T left = Sample;
            T right = left.Clone() as T;
            Assert.AreNotSame(left, right);
        }

        [TestMethod]
        public void ClonesHaveSameHashCode()
        {
            T left = Sample;
            T right = left.Clone() as T;
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        [TestMethod]
        public void ClonesAreEqual()
        {
            T left = Sample;
            T right = left.Clone() as T;
            Assert.AreEqual(left, right);
        }

        [TestMethod]
        public void SamplesHaveSameCode()
        {
            T left = Sample;
            T right = Sample;
            Assert.AreNotSame(left, right);
            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
        }

        [TestMethod]
        public void IDChangesCode()
        {
            T left = Sample;
            T right = Sample;
            right.ID++;
            Assert.AreNotEqual(left.GetHashCode(), right.GetHashCode());
        }

        [TestMethod]
        public void SamplesAreEqual()
        {
            T left = Sample;
            T right = Sample;
            Assert.AreNotSame(left, right);
            Assert.AreEqual(left, right);
        }

        [TestMethod]
        public void SampleNotEqualToNull()
        {
            T left = Sample;
            T right = null;
            Assert.AreNotEqual(left, right);
        }

        [TestMethod]
        public void DifferentIDNotEquals()
        {
            T left = Sample;
            T right = Sample;
            right.ID++;
            Assert.AreNotEqual(left, right);
        }
    }
}
