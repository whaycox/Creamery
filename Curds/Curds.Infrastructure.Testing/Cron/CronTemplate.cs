using Curds.Application.Cron;
using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.Infrastructure.Cron
{
    public abstract class CronTemplate<T> : Test where T : ICronObject
    {
        protected abstract IEnumerable<AcceptanceCase> AcceptanceCases { get; }
        protected abstract IEnumerable<CronCase<T>> TestCases { get; }

        [TestMethod]
        public void Acceptance() => TestAcceptanceCases(AcceptanceCases);

        [TestMethod]
        public void CronTests()
        {
            foreach (CronCase<T> testCase in TestCases)
                TestCronCase(testCase);                
        }
        private void TestCronCase(CronCase<T> testCase)
        {
            foreach(T sample in testCase.Samples)
            {
                foreach (DateTime trueTime in testCase.TrueTimes)
                    Assert.IsTrue(sample.Test(trueTime));
                foreach (DateTime falseTime in testCase.FalseTimes)
                    Assert.IsFalse(sample.Test(falseTime));
            }
        }
    }
}
