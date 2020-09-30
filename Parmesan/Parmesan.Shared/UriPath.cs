using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Parmesan
{
    public static class UriPath
    {
        private const string Separator = "/";

        private static Regex FirstUriPart { get; } = new Regex("^(.*?)/?$", RegexOptions.Compiled);
        private static Regex MiddleUriPart { get; } = new Regex("^/*(.*?)/*$", RegexOptions.Compiled);
        private static Regex LastUriPart { get; } = new Regex("^/?(.*?)$", RegexOptions.Compiled);

        public static string Combine(params string[] parts)
        {
            if (parts == null || parts.Length == 0)
                throw new ArgumentNullException(nameof(parts));

            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < parts.Length; i++)
            {
                if (i > 0)
                    stringBuilder.Append(Separator);

                Match partMatch = MatchPart(i, parts.Length, parts[i]);
                stringBuilder.Append(partMatch.Groups[1].Value);
            }

            return stringBuilder.ToString();
        }
        private static Match MatchPart(int index, int length, string part)
        {
            if (index == 0)
                return FirstUriPart.Match(part);
            else if (index == length - 1)
                return LastUriPart.Match(part);
            else
                return MiddleUriPart.Match(part);
        }
    }
}
