using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Cron;
using System.Linq;

namespace Curds.Infrastructure.Cron
{
    public class CronExpression : ICronObject
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

        public CronExpression(string expression)
        {
            ParseExpression(expression.Split(SeparatorArray, StringSplitOptions.RemoveEmptyEntries));
        }
        private void ParseExpression(string[] expressionParts)
        {
            if (expressionParts.Length != ExpectedParts)
                throw new FormatException($"Invalid format for {nameof(CronExpression)}: {expressionParts.Stitch(Separator)}");

            ParseMinute(expressionParts[(int)ExpressionPlacement.Minute]);
            ParseHour(expressionParts[(int)ExpressionPlacement.Hour]);
            ParseDayOfMonth(expressionParts[(int)ExpressionPlacement.DayOfMonth]);
            ParseMonth(expressionParts[(int)ExpressionPlacement.Month]);
            ParseDayOfWeek(expressionParts[(int)ExpressionPlacement.DayOfWeek]);
        }
        private void ParseMinute(string minuteRange) => Children.Add(new Tokens.Minute(minuteRange));
        private void ParseHour(string hourRange) => Children.Add(new Tokens.Hour(hourRange));
        private void ParseDayOfMonth(string dayOfMonthRange) => Children.Add(new Tokens.DayOfMonth(dayOfMonthRange));
        private void ParseMonth(string monthRange) => Children.Add(new Tokens.Month(monthRange));
        private void ParseDayOfWeek(string dayOfWeekRange) => Children.Add(new Tokens.DayOfWeek(dayOfWeekRange));

        public bool Test(DateTime testTime) => !AreAnyFails(testTime);
        private bool AreAnyFails(DateTime testTime) => Children.Where(c => !c.Test(testTime)).Any();
    }
}
