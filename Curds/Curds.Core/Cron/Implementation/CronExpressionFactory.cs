using System;
using System.Collections.Generic;
using System.Linq;

namespace Curds.Cron.Implementation
{
    using Abstraction;
    using FieldFactories.Abstraction;

    internal class CronExpressionFactory : ICronExpressionFactory
    {
        private enum FieldLocation
        {
            Minute,
            Hour,
            DayOfMonth,
            Month,
            DayOfWeek,
        }

        private const char FieldSeparator = ' ';
        private const int AllowedFields = 5;

        private ICronFieldFactory MinuteFactory { get; }
        private ICronFieldFactory HourFactory { get; }
        private ICronFieldFactory DayOfMonthFactory { get; }
        private ICronFieldFactory MonthFactory { get; }
        private ICronFieldFactory DayOfWeekFactory { get; }

        public CronExpressionFactory(
            IMinuteFieldFactory minuteFactory,
            IHourFieldFactory hourFactory,
            IDayOfMonthFieldFactory dayOfMonthFactory,
            IMonthFieldFactory monthFactory,
            IDayOfWeekFieldFactory dayOfWeekFactory)
        {
            MinuteFactory = minuteFactory;
            HourFactory = hourFactory;
            DayOfMonthFactory = dayOfMonthFactory;
            MonthFactory = monthFactory;
            DayOfWeekFactory = dayOfWeekFactory;
        }

        public ICronExpression Parse(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                throw new ArgumentNullException(nameof(expression));

            Dictionary<FieldLocation, string> fields = SplitExpressionIntoFields(expression);
            List<ICronField> parsedFields = fields
                .Select(field => ParseField(field.Key, field.Value))
                .ToList();

            return new CronExpression(parsedFields);
        }
        private Dictionary<FieldLocation, string> SplitExpressionIntoFields(string expression)
        {
            string[] fields = expression.Split(FieldSeparator);
            if (fields.Length != AllowedFields)
                throw new FormatException($"Expected {AllowedFields} fields, you supplied {fields.Length}");

            Dictionary<FieldLocation, string> fieldMap = new Dictionary<FieldLocation, string>();
            for (int i = 0; i < fields.Length; i++)
                fieldMap.Add((FieldLocation)i, fields[i]);

            return fieldMap;
        }
        private ICronField ParseField(FieldLocation location, string field)
        {
            switch (location)
            {
                case FieldLocation.Minute:
                    return MinuteFactory.Parse(field);
                case FieldLocation.Hour:
                    return HourFactory.Parse(field);
                case FieldLocation.DayOfMonth:
                    return DayOfMonthFactory.Parse(field);
                case FieldLocation.Month:
                    return MonthFactory.Parse(field);
                case FieldLocation.DayOfWeek:
                    return DayOfWeekFactory.Parse(field);
                default:
                    throw new InvalidOperationException($"Unsupported field {location}");
            }
        }
    }
}
