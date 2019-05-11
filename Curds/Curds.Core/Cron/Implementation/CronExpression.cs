using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Cron.Implementation
{
    using Abstraction;
    using Token.Implementation;

    public class CronExpression : Domain.CronExpression
    {
        private enum ExpressionPlacement
        {
            Minute = 0,
            Hour = 1,
            DayOfMonth = 2,
            Month = 3,
            DayOfWeek = 4,
        }

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

            ParseMinute(expressionParts[(int)ExpressionPlacement.Minute]);
            ParseHour(expressionParts[(int)ExpressionPlacement.Hour]);
            ParseDayOfMonth(expressionParts[(int)ExpressionPlacement.DayOfMonth]);
            ParseMonth(expressionParts[(int)ExpressionPlacement.Month]);
            ParseDayOfWeek(expressionParts[(int)ExpressionPlacement.DayOfWeek]);
        }
        private void ParseMinute(string minuteRange) => Children.Add(new Minute(minuteRange));
        private void ParseHour(string hourRange) => Children.Add(new Hour(hourRange));
        private void ParseDayOfMonth(string dayOfMonthRange) => Children.Add(new DayOfMonth(dayOfMonthRange));
        private void ParseMonth(string monthRange) => Children.Add(new Month(monthRange));
        private void ParseDayOfWeek(string dayOfWeekRange) => Children.Add(new DayOfWeek(dayOfWeekRange));

        public override bool Test(DateTime testTime) => !AreAnyFails(testTime);
        private bool AreAnyFails(DateTime testTime) => Children.Where(c => !c.Test(testTime)).Any();

        public override string ToString() => Expression;
    }
}
