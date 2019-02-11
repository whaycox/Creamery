using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Cron.Tokens.Tests
{
    using Cases.Month;

    [TestClass]
    public class Month : CronTemplate<Tokens.Month>
    {
        protected override IEnumerable<AcceptanceCase> AcceptanceCases => new List<AcceptanceCase>
        {
            { new SuccessCase(() => new Tokens.Month("*,*/5,12")) },
            { new SuccessCase(() => new Tokens.Month("JAN-MAR")) },
            { new FailureCase<InvalidOperationException>(() => new Tokens.Month("MAR-JAN")) },
            { new SuccessCase(() => new Tokens.Month("FEB,MaR,Apr,mAY,jUn")) },
            { new SuccessCase(() => new Tokens.Month("1-OCT")) },
            { new FailureCase<InvalidOperationException>(() => new Tokens.Month("12-OCT")) },
            { new FailureCase<InvalidOperationException>(() => new Tokens.Month("DEC-NOV")) },
            { new FailureCase<FormatException>(() => new Tokens.Month("13")) },
            { new FailureCase<FormatException>(() => new Tokens.Month("Jun-15")) },
            { new FailureCase<FormatException>(() => new Tokens.Month("0-May")) },
        };

        protected override IEnumerable<CronCase<Tokens.Month>> TestCases => new List<CronCase<Tokens.Month>>
        {
            { new Spring() },
            { new Summer() },
            { new Winter() },
        };
    }
}
