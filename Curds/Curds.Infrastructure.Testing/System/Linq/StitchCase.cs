using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Infrastructure.System.Linq
{
    public class StitchCase
    {
        public const string CommaDelimiter = ",";
        public static string NewLineDelimiter => Environment.NewLine;
        public const string SpaceDelimiter = " ";
        public static string CommandAndSpaceDelimiter => CommaDelimiter + SpaceDelimiter;

        public static readonly object[] Mixed = new object[] { true, "Hi", 17 };
        public static readonly string[] Strings = new string[] { "One", "Two", "Three" };

        public static IEnumerable<StitchCase> Examples
        {
            get
            {
                foreach (StitchCase testCase in AllDelimiterCases(Mixed, nameof(Mixed)))
                    yield return testCase;
                foreach (StitchCase testCase in AllDelimiterCases(Strings, nameof(Strings)))
                    yield return testCase;
            }
        }
        private static IEnumerable<StitchCase> AllDelimiterCases(object[] content, string contentName)
        {
            yield return new StitchCase() { ContentToStitch = content, CaseName = contentName, Delimiter = CommaDelimiter, DelimiterName = nameof(CommaDelimiter) };
            yield return new StitchCase() { ContentToStitch = content, CaseName = contentName, Delimiter = NewLineDelimiter, DelimiterName = nameof(NewLineDelimiter) };
            yield return new StitchCase() { ContentToStitch = content, CaseName = contentName, Delimiter = SpaceDelimiter, DelimiterName = nameof(SpaceDelimiter) };
            yield return new StitchCase() { ContentToStitch = content, CaseName = contentName, Delimiter = CommandAndSpaceDelimiter, DelimiterName = nameof(CommandAndSpaceDelimiter) };
        }

        public object[] ContentToStitch { get; set; }
        public string CaseName { get; set; }
        public string Delimiter { get; set; }
        public string DelimiterName { get; set; }
    }
}
