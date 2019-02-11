using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.Domain.Persistence.Tests
{
    [TestClass]
    public class CompareExtensions
    {
        private delegate T AlterDelegate<T>(T input);

        private static List<T> Append<T>(List<T> input, AlterDelegate<T> alterDelegate)
        {
            if (input == null)
                input = new List<T>();
            input.Add(alterDelegate(default));
            return input;
        }

        private static int AlterInt(int input) => input + 1;
        private static long AlterLong(long input) => input + 1;
        private static decimal AlterDecimal(decimal input) => input + 0.001m;
        private static string AlterString(string input) => $"{(input ?? string.Empty)}{nameof(AlterString)}";
        private static DateTime AlterDateTime(DateTime input) => input.AddMilliseconds(1);
        private static MockEntity AlterMockEntity(MockEntity input)
        {
            if (input == null)
                input = new MockEntity();
            input.ID++;
            return input;
        }

        [TestMethod]
        public void CompareWithNullTests()
        {
        }
        private void CompareWithNullValueTests()
        {
            CompareWithNullByType<int>(AlterInt);
            CompareWithNullByType<long>(AlterLong);
            CompareWithNullByType<decimal>(AlterDecimal);
            CompareWithNullByType<DateTime>(AlterDateTime);
        }
        private void CompareWithNullClassTests()
        {
            CompareWithNullByType<string>(AlterString);
            CompareWithNullByType<MockEntity>(AlterMockEntity);
        }

        private void CompareWithNullByNullableType<T>(AlterDelegate<T> alterDelegate) where T : class
        {
            CompareWithNullByType(alterDelegate);
            BothNullsAreTrue(alterDelegate);
            DefaultAndNullAreFalse(alterDelegate);
        }
        private void CompareWithNullByType<T>(AlterDelegate<T> alterDelegate)
        {
            DefaultValuesAreTrue<T>();
            SeparateAltersAreTrue(alterDelegate);
            DefaultAndAlteredAreFalse(alterDelegate);
            MultipleAltersAreFalse(alterDelegate);
        }

        private void DefaultValuesAreTrue<T>()
        {
            T left = default;
            T right = default;
            Assert.IsTrue(left.CompareWithNull(right));
            Assert.IsTrue(right.CompareWithNull(left));
        }
        private void SeparateAltersAreTrue<T>(AlterDelegate<T> alterDelegate)
        {
            T left = alterDelegate(default);
            T right = alterDelegate(default);
            Assert.IsTrue(left.CompareWithNull(right));
            Assert.IsTrue(right.CompareWithNull(left));
        }
        private void DefaultAndAlteredAreFalse<T>(AlterDelegate<T> alterDelegate)
        {
            T left = default;
            T right = alterDelegate(default);
            Assert.IsFalse(left.CompareWithNull(right));
            Assert.IsFalse(right.CompareWithNull(left));
        }
        private void MultipleAltersAreFalse<T>(AlterDelegate<T> alterDelegate)
        {
            T once = alterDelegate(default);
            T twice = alterDelegate(alterDelegate(default));
            Assert.IsFalse(once.CompareWithNull(twice));
            Assert.IsFalse(twice.CompareWithNull(once));
        }
        private void BothNullsAreTrue<T>(AlterDelegate<T> alterDelegate) where T : class
        {
            T left = null;
            T right = null;
            Assert.IsTrue(left.CompareWithNull(right));
            Assert.IsTrue(right.CompareWithNull(left));
        }
        private void DefaultAndNullAreFalse<T>(AlterDelegate<T> alterDelegate) where T : class
        {
            T left = alterDelegate(default);
            T right = null;
            Assert.IsFalse(left.CompareWithNull(right));
            Assert.IsFalse(right.CompareWithNull(left));
        }

        [TestMethod]
        public void CompareTwoListsTests()
        {
            CompareTwoListsByType<int>(AlterInt);
            CompareTwoListsByType<long>(AlterLong);
            CompareTwoListsByType<decimal>(AlterDecimal);
            CompareTwoListsByType<string>(AlterString);
            CompareTwoListsByType<DateTime>(AlterDateTime);
            CompareTwoListsByType<MockEntity>(AlterMockEntity);
        }

        private void CompareTwoListsByType<T>(AlterDelegate<T> alterDelegate)
        {
            NullsAreTrue<T>();
            NullAndEmptyAreFalse<T>();
            SameCountAreTrue(alterDelegate);
            DifferentCountAreFalse(alterDelegate);
            SameCountDifferentContentAreFalse(alterDelegate);
        }
        private void NullsAreTrue<T>()
        {
            List<T> left = null;
            List<T> right = null;
            Assert.IsTrue(left.CompareTwoLists(right));
            Assert.IsTrue(right.CompareTwoLists(left));
        }
        private void NullAndEmptyAreFalse<T>()
        {
            List<T> left = null;
            List<T> right = new List<T>();
            Assert.IsFalse(left.CompareTwoLists(right));
            Assert.IsFalse(right.CompareTwoLists(left));
        }
        private void SameCountAreTrue<T>(AlterDelegate<T> alterDelegate)
        {
            List<T> left = Append(null, alterDelegate);
            List<T> right = Append(null, alterDelegate);
            Assert.IsTrue(left.CompareTwoLists(right));
            Assert.IsTrue(right.CompareTwoLists(left));
        }
        private void DifferentCountAreFalse<T>(AlterDelegate<T> alterDelegate)
        {
            List<T> left = Append(null, alterDelegate);
            List<T> right = Append(null, alterDelegate);
            right = Append(right, alterDelegate);
            Assert.IsFalse(left.CompareTwoLists(right));
            Assert.IsFalse(right.CompareTwoLists(left));
        }
        private void SameCountDifferentContentAreFalse<T>(AlterDelegate<T> alterDelegate)
        {
            List<T> left = Append(null, alterDelegate);
            List<T> right = Append(null, alterDelegate);
            right[0] = alterDelegate(right[0]);
            Assert.IsFalse(left.CompareTwoLists(right));
            Assert.IsFalse(right.CompareTwoLists(left));
        }
    }
}
