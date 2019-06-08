using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds;

namespace System.Linq.Tests
{
    [TestClass]
    public class Stitch : Test
    {
        #region Delimiters

        public const string CommaDelimiter = ",";
        public static string NewLineDelimiter => Environment.NewLine;
        public const string SpaceDelimiter = " ";
        public static string CommandAndSpaceDelimiter => CommaDelimiter + SpaceDelimiter;

        #endregion

        #region Content

        public static readonly object[] Mixed = new object[] { true, "Hi", 17 };
        public static readonly string[] Strings = new string[] { "One", "Two", "Three" };

        #endregion

        [TestMethod]
        public void StitchesSamplesProperly()
        {
            TestContent(Mixed, nameof(Mixed));
            TestContent(Strings, nameof(Strings));
        }
        private void TestContent(object[] content, string contentName)
        {
            TestCase(content, CommaDelimiter, ExpectedOutput(contentName, nameof(CommaDelimiter)));
            TestCase(content, NewLineDelimiter, ExpectedOutput(contentName, nameof(NewLineDelimiter)));
            TestCase(content, SpaceDelimiter, ExpectedOutput(contentName, nameof(SpaceDelimiter)));
            TestCase(content, CommandAndSpaceDelimiter, ExpectedOutput(contentName, nameof(CommandAndSpaceDelimiter)));
        }
        private string ExpectedOutput(string contentName, string delimiterName)
        {
            switch (contentName)
            {
                case nameof(Mixed):
                    return ExpectedMixed(delimiterName);
                case nameof(Strings):
                    return ExpectedStrings(delimiterName);
                default:
                    throw new Exception($"Unexpected content {contentName}");
            }
        }
        private string ExpectedMixed(string delimiterName)
        {
            switch (delimiterName)
            {
                case nameof(CommaDelimiter):
                    return "True,Hi,17";
                case nameof(NewLineDelimiter):
                    return "True\r\nHi\r\n17";
                case nameof(SpaceDelimiter):
                    return "True Hi 17";
                case nameof(CommandAndSpaceDelimiter):
                    return "True, Hi, 17";
                default:
                    throw new Exception($"Unexpected delimiter {delimiterName}");
            }
        }
        private string ExpectedStrings(string delimiterName)
        {
            switch (delimiterName)
            {
                case nameof(CommaDelimiter):
                    return "One,Two,Three";
                case nameof(NewLineDelimiter):
                    return $"One{Environment.NewLine}Two{Environment.NewLine}Three";
                case nameof(SpaceDelimiter):
                    return "One Two Three";
                case nameof(CommandAndSpaceDelimiter):
                    return "One, Two, Three";
                default:
                    throw new Exception($"Unexpected delimiter {delimiterName}");
            }
        }
        private void TestCase(object[] content, string delimiter, string expected) =>
            Assert.AreEqual(expected, content.Stitch(delimiter));

        [TestMethod]
        public void NullObjectsStitchEmpty()
        {
            TestCase(new object[] { 0, null, "oeu" }, CommaDelimiter, "0,,oeu");
        }
    }
}
