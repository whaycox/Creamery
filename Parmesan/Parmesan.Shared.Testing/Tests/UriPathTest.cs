using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Parmesan.Tests
{
    [TestClass]
    public class UriPathTest
    {
        private string TestPartOne = nameof(TestPartOne);
        private string TestPartTwo = nameof(TestPartTwo);

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullParamsThrows()
        {
            string[] testArray = null;

            UriPath.Combine(testArray);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void EmptyParamsThrows()
        {
            string[] testArray = new string[0];

            UriPath.Combine(testArray);
        }

        [TestMethod]
        public void PlacesSeparatorBetweenParts()
        {
            string actual = UriPath.Combine(
                TestPartOne, 
                TestPartTwo);

            Assert.AreEqual($"{TestPartOne}/{TestPartTwo}", actual);
        }

        [TestMethod]
        public void PreservesLeadingSeparator()
        {
            string actual = UriPath.Combine(
                $"/{TestPartOne}",
                TestPartTwo);

            Assert.AreEqual($"/{TestPartOne}/{TestPartTwo}", actual);
        }

        [TestMethod]
        public void PreservesTrailingSeparator()
        {
            string actual = UriPath.Combine(
                TestPartOne,
                $"{TestPartTwo}/");

            Assert.AreEqual($"{TestPartOne}/{TestPartTwo}/", actual);
        }

        [TestMethod]
        public void CombinesConsecutiveSeparators()
        {
            string actual = UriPath.Combine(
                $"{TestPartOne}/",
                $"/{TestPartTwo}");

            Assert.AreEqual($"{TestPartOne}/{TestPartTwo}", actual);
        }

    }
}
