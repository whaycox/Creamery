using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Infrastructure.Cron.Tokens.Tests
{
    using Cases.Hour;

    [TestClass]
    public class Hour : CronTemplate<Tokens.Hour>
    {
        protected override IEnumerable<AcceptanceCase> AcceptanceCases
        {
            get
            {
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Hour("*,*/2,5-20"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Hour("*-2"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Hour("5-24"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Hour("0,1,2,3,4,23"), ShouldSucceed = true };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Hour("0,1,2,3,4,25"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Hour("24"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Hour("-10-12"), ShouldSucceed = false };
                yield return new AcceptanceCase() { Delegate = () => new Tokens.Hour("12-30"), ShouldSucceed = false };
            }
        }

        protected override IEnumerable<CronCase<Tokens.Hour>> TestCases
        {
            get
            {
                yield return new BusinessHours();
                yield return new EverySixHours();
            }
        }
    }
}
