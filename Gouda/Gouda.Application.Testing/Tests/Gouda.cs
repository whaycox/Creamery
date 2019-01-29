using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using Gouda.Domain.Enumerations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using Curds.Domain.DateTimes;
using Gouda.Domain;

namespace Gouda.Application.Tests
{
    [TestClass]
    public class Gouda
    {
        private MockOptions MockOptions = new MockOptions();

        private Definition MockDefinition(Application.Gouda app) => app.Entities.Definitions.First();
        private void StartScheduling(Application.Gouda app) => app.Commands.StartScheduling(new Message.Command.Scheduling.Start());

        private void ApplicationTestHelper(Action<Application.Gouda> testDelegate)
        {
            using (Application.Gouda app = new Application.Gouda(MockOptions))
                testDelegate(app);
        }

        [TestCleanup]
        public void Cleanup()
        {
            MockDateTime.Reset();
        }

        [TestMethod]
        public void BuildsRequestFromDefinitionTest() => ApplicationTestHelper(BuildsRequestFromDefinitionHelper);
        private void BuildsRequestFromDefinitionHelper(Application.Gouda app)
        {
            Definition sample = MockDefinition(app);
            Request request = sample.Request;
            Assert.AreEqual(nameof(Domain.Check.MockDefinition), request.Name);
            foreach (var argPair in request.Arguments)
            {
                Assert.IsTrue(Domain.Check.MockDefinition.MockArguments.ContainsKey(argPair.Key));
                Assert.AreEqual(Domain.Check.MockDefinition.MockArguments[argPair.Key], argPair.Value);
            }
        }

        [TestMethod]
        public void DeterminesWhetherDefinitionStatusChangesTest() => ApplicationTestHelper(DeterminesWhetherDefinitionStatusChangesHelper);
        private void DeterminesWhetherDefinitionStatusChangesHelper(Application.Gouda app)
        {
            Assert.AreEqual(Status.Unknown, MockDefinition(app).Status);
            StartScheduling(app);
            Assert.AreEqual(Status.Good, MockDefinition(app).Status);
        }

        [TestMethod]
        public void DeterminesWhoIsNotifiedTest() => ApplicationTestHelper(DeterminesWhoIsNotifiedHelper);
        private void DeterminesWhoIsNotifiedHelper(Application.Gouda app)
        {
            StartScheduling(app);
            Assert.AreEqual(2, MockOptions.MockNotifier.UsersNotified.Count);
            Assert.AreEqual(2, MockOptions.MockNotifier.UsersNotified[0]);
            Assert.AreEqual(3, MockOptions.MockNotifier.UsersNotified[1]);
        }

        [TestMethod]
        public void DeterminesHowToContactTest() => ApplicationTestHelper(DeterminesHowToContactHelper);
        private void DeterminesHowToContactHelper(Application.Gouda app)
        {
            StartScheduling(app);
            Assert.AreEqual(1, MockOptions.MockNotifier.UsersContactedByOne.Count);
            Assert.AreEqual(3, MockOptions.MockNotifier.UsersContactedByOne[0]);
            Assert.AreEqual(1, MockOptions.MockNotifier.UsersContactedByTwo.Count);
            Assert.AreEqual(2, MockOptions.MockNotifier.UsersContactedByTwo[0]);
        }
    }
}
