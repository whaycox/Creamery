using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Cron.Implementation
{
    using Abstraction;
    using Enumeration;
    using Token.Domain;

    public class CronExpression : ICronExpression
    {
        private const string Separator = " ";
        private static readonly string[] SeparatorArray = new string[] { Separator };
        private const int ExpectedParts = 5;

        private List<ICronObject> Children = new List<ICronObject>();

        public string Expression { get; }

        public CronExpression(string expression)
        {
            Expression = expression;
            ParseExpression(expression.Split(SeparatorArray, StringSplitOptions.RemoveEmptyEntries));
        }

        private void ParseExpression(string[] expressionParts)
        {
            if (expressionParts.Length != ExpectedParts)
                throw new FormatException($"Invalid format for {nameof(ICronExpression)}: {Expression}");

            for (int i = 0; i < ExpectedParts; i++)
                Children.Add(Parse(expressionParts[i], (ExpressionPart)i));
        }
        private ICronObject Parse(string tokenRanges, ExpressionPart part)
        {
            switch (part)
            {
                case ExpressionPart.Minute:
                    return new Minute(new Parser.Domain.Basic().ParseRanges(tokenRanges));
                case ExpressionPart.Hour:
                    return new Hour(new Parser.Domain.Basic().ParseRanges(tokenRanges));
                case ExpressionPart.DayOfMonth:
                    return new DayOfMonth(new Parser.Implementation.DayOfMonth().ParseRanges(tokenRanges));
                case ExpressionPart.Month:
                    return new Month(new Parser.Implementation.Month().ParseRanges(tokenRanges));
                case ExpressionPart.DayOfWeek:
                    return new DayOfWeek(new Parser.Implementation.DayOfWeek().ParseRanges(tokenRanges));
                default:
                    throw new InvalidOperationException($"Unexpected {nameof(part)}: {part}");
            }
        }

        public bool Test(DateTime testTime) => !AreAnyFails(testTime);
        private bool AreAnyFails(DateTime testTime) => Children.Where(c => !c.Test(testTime)).Any();

        public override string ToString() => Expression;
    }
}
