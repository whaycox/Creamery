using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Domain.Persistence
{
    public abstract class NamedEntityTemplate<T> : EntityTemplate<T> where T : NamedEntity
    {
        [TestMethod]
        public void NullNameNotEquals()
        {
            T left = Sample;
            T right = Sample;
            right.Name = null;
            Assert.AreNotEqual(left, right);
        }

        [TestMethod]
        public void EmptyNullNotEquals()
        {
            T left = Sample;
            T right = Sample;
            left.Name = null;
            right.Name = string.Empty;
            Assert.AreNotEqual(left, right);
        }

        [TestMethod]
        public void DifferentNameNotEquals()
        {
            T left = Sample;
            T right = Sample;
            right.Name = nameof(DifferentNameNotEquals);
            Assert.AreNotEqual(left, right);
        }

        [TestMethod]
        public void NullNameDifferentCode()
        {
            T left = Sample;
            T right = Sample;
            right.Name = null;
            Assert.AreNotEqual(left.GetHashCode(), right.GetHashCode());
        }

        [TestMethod]
        public void EmptyNullDifferentCode()
        {
            T left = Sample;
            T right = Sample;
            left.Name = null;
            right.Name = string.Empty;
            Assert.AreNotEqual(left.GetHashCode(), right.GetHashCode());
        }

        [TestMethod]
        public void DifferentNameDifferentCode()
        {
            T left = Sample;
            T right = Sample;
            right.Name = nameof(DifferentNameDifferentCode);
            Assert.AreNotEqual(left.GetHashCode(), right.GetHashCode());
        }
    }
}
