using Curds.Application.Cron;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Infrastructure.Cron
{
    using Tokens;

    public class Expression : ICronObject
    {
        private enum ExpressionPlacement
        {
            Minute = 0,
            Hour = 1,
            DayOfMonth = 2,
            Month = 3,
            DayOfWeek = 4,
        }

        private static readonly string Separator = " ";
        private static readonly string[] SeparatorArray = new string[] { Separator };
        private const int ExpectedParts = 5;

        private List<ICronObject> Children = new List<ICronObject>();

        private string OriginalExpression { get; }

        public Expression(string expression)
        {
            ParseExpression(expression.Split(SeparatorArray, StringSplitOptions.RemoveEmptyEntries));
            OriginalExpression = expression;
        }
        private void ParseExpression(string[] expressionParts)
        {
            if (expressionParts.Length != ExpectedParts)
                throw new FormatException($"Invalid format for {nameof(Expression)}: {expressionParts.Stitch(Separator)}");

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

        public bool Test(DateTime testTime) => !AreAnyFails(testTime);
        private bool AreAnyFails(DateTime testTime) => Children.Where(c => !c.Test(testTime)).Any();

        public override string ToString() => OriginalExpression;
    }
}
