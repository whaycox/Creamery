using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Curds.Infrastructure.System.Linq.Tests
{
    [TestClass]
    public class Extensions
    {
        [TestMethod]
        public void StitchBehavior()
        {
            foreach (StitchCase testCase in StitchCase.Examples)
                StitchHelper(testCase);
        }
        private void StitchHelper(StitchCase testCase)
        {
            string actual = testCase.ContentToStitch.Stitch(testCase.Delimiter);
            string expected = null;

            switch (testCase.CaseName)
            {
                case nameof(StitchCase.Mixed):
                    expected = MixedExpected(testCase.DelimiterName);
                    break;
                case nameof(StitchCase.Strings):
                    expected = StringsExpected(testCase.DelimiterName);
                    break;
                default:
                    throw new InvalidOperationException("Unrecognized content");
            }
            Assert.AreEqual(expected, actual);
        }

        private string MixedExpected(string delimiterName)
        {
            switch (delimiterName)
            {
                case nameof(StitchCase.CommaDelimiter):
                    return "True,Hi,17";
                case nameof(StitchCase.NewLineDelimiter):
                    return "True\r\nHi\r\n17";
                case nameof(StitchCase.SpaceDelimiter):
                    return "True Hi 17";
                case nameof(StitchCase.CommandAndSpaceDelimiter):
                    return "True, Hi, 17";
                default:
                    throw new InvalidOperationException("Unrecognized delimiter");
            }
        }
        private string StringsExpected(string delimiterName)
        {
            switch (delimiterName)
            {
                case nameof(StitchCase.CommaDelimiter):
                    return "One,Two,Three";
                case nameof(StitchCase.NewLineDelimiter):
                    return $"One{Environment.NewLine}Two{Environment.NewLine}Three";
                case nameof(StitchCase.SpaceDelimiter):
                    return "One Two Three";
                case nameof(StitchCase.CommandAndSpaceDelimiter):
                    return "One, Two, Three";
                default:
                    throw new InvalidOperationException("Unrecognized delimiter");
            }
        }

        [TestMethod]
        public void StitchNull()
        {
            StitchNullHelper(new StitchCase() { ContentToStitch = new object[] { 0, null, "oeu" }, Delimiter = StitchCase.CommaDelimiter }, "0,,oeu");
        }
        private void StitchNullHelper(StitchCase testCase, string expected)
        {
            string stitched = testCase.ContentToStitch.Stitch(testCase.Delimiter);
            Assert.AreEqual(expected, stitched);
        }
    }
}
