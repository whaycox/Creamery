using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Cron.Tokens.Tests
{
    using Cases.Minute;

    [TestClass]
    public class Minute : CronTemplate<Tokens.Minute>
    {
        protected override IEnumerable<AcceptanceCase> AcceptanceCases
        {
            get
            {
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Minute("0-60"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Minute("*,*/5,20,55"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Minute("30-29"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Minute("60"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Minute("50-100"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Minute("-10-50"), ShouldSucceed = false };
            }
        }

        protected override IEnumerable<CronCase<Tokens.Minute>> TestCases
        {
            get
            {
                yield return new BackHalfHour();
                yield return new EverySixMinutes();
            }
        }
    }
}
