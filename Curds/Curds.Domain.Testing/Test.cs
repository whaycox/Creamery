using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Domain
{
    public abstract class Test
    {
        protected void TestAcceptanceCases(IEnumerable<AcceptanceCase> testCases)
        {
            foreach (AcceptanceCase testCase in testCases)
                TestAcceptanceCase(testCase);
        }
        protected void TestAcceptanceCase(AcceptanceCase testCase)
        {
            if (testCase.ShouldSucceed)
                testCase.Delegate();
            else
                TestForException<Exception>(testCase.Delegate);
        }

        protected void TestForException(Action action) => TestForException<Exception>(action);
        protected void TestForException<U>(Action action) where U : Exception => TestForException<U>(action, (u) => { });
        protected void TestForException<U>(Action action, Action<U> exceptionHandler) where U : Exception
        {
            try
            {
                action();
                Assert.Fail("Expected an Exception to be thrown but one was not");
            }
            catch (AssertFailedException assertException)
            {
                throw assertException;
            }
            catch (Exception generatedException)
            {
                if (generatedException is U)
                    exceptionHandler(generatedException as U);
                else
                    Assert.Fail($"{nameof(generatedException)} is expected to be {typeof(U).FullName} but is instead {generatedException.GetType().FullName}");
            }
        }
    }
}
