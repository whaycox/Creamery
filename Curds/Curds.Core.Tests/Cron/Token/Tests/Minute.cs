using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Cron.Token.Tests
{
    using Curds.Cron.Range.Domain;
    using Curds.Cron.Token.Domain;
    using Curds.Cron.Token.Implementation;
    using Template;

    [TestClass]
    public class Minute : Basic<Implementation.Minute>
    {
        protected override int AbsoluteMin => Implementation.Minute.MinMinute;
        protected override int AbsoluteMax => Implementation.Minute.MaxMinute;

        protected override Implementation.Minute Build(IEnumerable<Basic> ranges) => new Implementation.Minute(ranges);

        protected override IEnumerable<TestCase> TestCases => throw new NotImplementedException();

        //protected override IEnumerable<AcceptanceCase> AcceptanceCases => new List<AcceptanceCase>
        //{
        //    { new FailureCase<FormatException>(() => new Token.Minute("0-60")) },
        //    { new SuccessCase(() => new Token.Minute("*,*/5,20,55")) },
        //    { new FailureCase<InvalidOperationException>(() => new Token.Minute("30-29")) },
        //    { new FailureCase<FormatException>(() => new Token.Minute("60")) },
        //    { new FailureCase<FormatException>(() => new Token.Minute("50-100")) },
        //    { new FailureCase<FormatException>(() => new Token.Minute("-10-50")) },
        //};

        //protected override IEnumerable<CronCase<Token.Minute>> TestCases => new List<CronCase<Token.Minute>>
        //{
        //    { new BackHalfHour() },
        //    { new EverySixMinutes() },
        //};

        #region TestCases

        private class 

        #endregion

    }
}
