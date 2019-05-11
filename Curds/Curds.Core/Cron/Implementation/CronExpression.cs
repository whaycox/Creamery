using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Cron.Implementation
{
    using Abstraction;
    using Enumeration;
    using Token.Implementation;

    public class CronExpression : Domain.CronExpression
    {
        private const string Separator = " ";
        private static readonly string[] SeparatorArray = new string[] { Separator };
        private const int ExpectedParts = 5;

        private List<ICronObject> Children = new List<ICronObject>();

        public override string Expression { get; }

        public CronExpression(string expression)
        {
            ParseExpression(expression.Split(SeparatorArray, StringSplitOptions.RemoveEmptyEntries));
            Expression = expression;
        }
        private void ParseExpression(string[] expressionParts)
        {
            if (expressionParts.Length != ExpectedParts)
                throw new FormatException($"Invalid format for {nameof(Domain.CronExpression.Expression)}: {expressionParts.Stitch(Separator)}");

            for (int i = 0; i < ExpectedParts; i++)
                Children.Add(Parse(expressionParts[i], (Token)i));
        }
        private ICronObject Parse(string tokenRanges, Token tokenType)
        {
            switch (tokenType)
            {
                case Token.Minute:
                    return new Minute(new Parser.Domain.Basic().ParseRanges(tokenRanges));
                case Token.Hour:
                    return new Hour(new Parser.Domain.Basic().ParseRanges(tokenRanges));
                case Token.DayOfMonth:
                    return new DayOfMonth(new Parser.Implementation.DayOfMonth().ParseRanges(tokenRanges));
                case Token.Month:
                    return new Month(new Parser.Implementation.Month().ParseRanges(tokenRanges));
                case Token.DayOfWeek:
                    return new DayOfWeek(new Parser.Implementation.DayOfWeek().ParseRanges(tokenRanges));
                default:
                    throw new InvalidOperationException($"Unexpected {nameof(tokenType)}: {tokenType}");
            }
        }

        public override bool Test(DateTime testTime) => !AreAnyFails(testTime);
        private bool AreAnyFails(DateTime testTime) => Children.Where(c => !c.Test(testTime)).Any();

        public override string ToString() => Expression;
    }
}
