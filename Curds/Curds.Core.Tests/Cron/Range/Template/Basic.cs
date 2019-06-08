using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.Cron.Range.Template
{
    using Enumeration;

    public abstract class Basic<T> : Test<T> where T : Domain.Basic
    {
        protected Mock.Basic MockRange = new Mock.Basic(true);
        protected Token.Mock.Basic MockToken = null;

        protected abstract ExpressionPart TestingPart { get; }

        protected Token.Domain.Basic TestToken => BuildTestToken(TestingPart, TestObject);
        protected Token.Domain.Basic BuildTestToken(ExpressionPart part, Domain.Basic range)
        {
            IEnumerable<Domain.Basic> ranges = new Domain.Basic[] { range };
            switch (part)
            {
                case ExpressionPart.Minute:
                    return new Token.Domain.Minute(ranges);
                case ExpressionPart.Hour:
                    return new Token.Domain.Hour(ranges);
                case ExpressionPart.DayOfMonth:
                    return new Token.Domain.DayOfMonth(ranges);
                case ExpressionPart.Month:
                    return new Token.Domain.Month(ranges);
                case ExpressionPart.DayOfWeek:
                    return new Token.Domain.DayOfWeek(ranges);
                default:
                    throw new NotImplementedException();
            }
        }

        [TestInitialize]
        public void SetTokenType()
        {
            MockToken = new Token.Mock.Basic(MockRange);
            MockToken.SetExpressionPart(TestingPart);
        }
    }
}
