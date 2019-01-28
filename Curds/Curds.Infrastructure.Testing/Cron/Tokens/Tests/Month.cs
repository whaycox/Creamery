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
        protected override IEnumerable<AcceptanceCase> AcceptanceCases
        {
            get
            {
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Month("*,*/5,12"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Month("JAN-MAR"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Month("MAR-JAN"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Month("FEB,MaR,Apr,mAY,jUn"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Month("1-OCT"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Month("12-OCT"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Month("DEC-NOV"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Month("13"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Month("Jun-15"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Month("0-May"), ShouldSucceed = false };
            }
        }

        protected override IEnumerable<CronCase<Tokens.Month>> TestCases
        {
            get
            {
                yield return new Spring();
                yield return new Summer();
                yield return new Winter();
            }
        }
    }
}
