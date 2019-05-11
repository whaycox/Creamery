using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Cron.Token.Tests
{
    using Cases.Month;

    [TestClass]
    public class Month : CronTemplate<Token.Month>
    {
        protected override IEnumerable<AcceptanceCase> AcceptanceCases => new List<AcceptanceCase>
        {
            { new SuccessCase(() => new Token.Month("*,*/5,12")) },
            { new SuccessCase(() => new Token.Month("JAN-MAR")) },
            { new FailureCase<InvalidOperationException>(() => new Token.Month("MAR-JAN")) },
            { new SuccessCase(() => new Token.Month("FEB,MaR,Apr,mAY,jUn")) },
            { new SuccessCase(() => new Token.Month("1-OCT")) },
            { new FailureCase<InvalidOperationException>(() => new Token.Month("12-OCT")) },
            { new FailureCase<InvalidOperationException>(() => new Token.Month("DEC-NOV")) },
            { new FailureCase<FormatException>(() => new Token.Month("13")) },
            { new FailureCase<FormatException>(() => new Token.Month("Jun-15")) },
            { new FailureCase<FormatException>(() => new Token.Month("0-May")) },
        };

        protected override IEnumerable<CronCase<Token.Month>> TestCases => new List<CronCase<Token.Month>>
        {
            { new Spring() },
            { new Summer() },
            { new Winter() },
        };
    }
}
