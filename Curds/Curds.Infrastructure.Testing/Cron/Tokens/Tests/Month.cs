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
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.Month("*,*/5,12"), ShouldSucceed = true } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.Month("JAN-MAR"), ShouldSucceed = true } },
            { new AcceptanceCase<InvalidOperationException>() { Delegate = () => new Tokens.Month("MAR-JAN"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.Month("FEB,MaR,Apr,mAY,jUn"), ShouldSucceed = true } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.Month("1-OCT"), ShouldSucceed = true } },
            { new AcceptanceCase<InvalidOperationException>() { Delegate = () => new Tokens.Month("12-OCT"), ShouldSucceed = false } },
            { new AcceptanceCase<InvalidOperationException>() { Delegate = () => new Tokens.Month("DEC-NOV"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.Month("13"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.Month("Jun-15"), ShouldSucceed = false } },
            { new AcceptanceCase<FormatException>() { Delegate = () => new Tokens.Month("0-May"), ShouldSucceed = false } },
        };

        protected override IEnumerable<CronCase<Tokens.Month>> TestCases => new List<CronCase<Tokens.Month>>
        {
            { new Spring() },
            { new Summer() },
            { new Winter() },
        };
    }
}
