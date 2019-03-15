using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Gouda.Domain.Check;
using Gouda.Domain.Persistence;
using Curds.Domain.DateTimes;
using Curds.Domain;
using Gouda.Domain.Communication.ContactAdapters;
using Curds;
using Gouda.Domain.Security;
using Gouda.Domain.Communication;

namespace Gouda.Infrastructure.Communication.Tests
{
    [TestClass]
    public class Notifier : CronTemplate<Communication.Notifier>
    {
        private static TimeSpan LocalOffset => DateTimeOffset.Now.Offset;
        private static DateTimeOffset FourthSaturdayInMarchTime => new DateTimeOffset(2019, 3, 23, 0, 0, 0, LocalOffset);

        private MockEvaluator Evaluator = null;
        private MockPersistence Persistence = null;

        private Definition Definition => Persistence.Definitions.Lookup(MockDefinition.SampleID).AwaitResult();

        private Communication.Notifier _obj = null;
        protected override Communication.Notifier TestObject => _obj;

        [TestInitialize]
        public void Init()
        {
            Persistence = new MockPersistence(Cron);
            Persistence.Reset();
            Persistence.Initialize();

            _obj = new Communication.Notifier(Time, Persistence);

            Evaluator = new MockEvaluator(TestObject, Persistence);
        }

        [TestCleanup]
        public void Clean()
        {
            MockContactOneAdapter.Notifications.Clear();
            MockContactTwoAdapter.Notifications.Clear();
        }

        [TestMethod]
        public void NotifiesOnStatusChange()
        {
            Evaluator.FireEvent(Definition);
            Assert.AreEqual(1, MockContactOneAdapter.Notifications.Count);
            Assert.AreEqual(MockUser.One.ID, MockContactOneAdapter.Notifications[0].userNotified);
            Assert.AreEqual(1, MockContactTwoAdapter.Notifications.Count);
            Assert.AreEqual(MockUser.Two.ID, MockContactTwoAdapter.Notifications[0].userNotified);
        }

        [TestMethod]
        public void NotifiesByRegistrations()
        {
            Persistence.EmptyContactRegistrations();

            Persistence.ContactRegistrations.Insert(OnlyOnWeekdays);
            Persistence.ContactRegistrations.Insert(WeekendsInEvenMonths);
            Persistence.ContactRegistrations.Insert(FourthSaturdayInMarch);
            
            Time.SetPointInTime(FourthSaturdayInMarchTime);

            Evaluator.FireEvent(Definition);
            Assert.AreEqual(0, MockContactOneAdapter.Notifications.Count);
            Assert.AreEqual(0, MockContactTwoAdapter.Notifications.Count);
            Assert.AreEqual(1, MockContactThreeAdapter.Notifications.Count);
            Assert.AreEqual(MockUser.Three.ID, MockContactThreeAdapter.Notifications[0].userNotified);
        }

        //One only listens on weekdays
        private ContactRegistration OnlyOnWeekdays => new ContactRegistration
        {
            ContactID = MockContact.One.ID,
            UserID = MockUser.One.ID,
            CronString = "* * * * 1-5",
        };
        //Two only on weekends in even months
        private ContactRegistration WeekendsInEvenMonths => new ContactRegistration
        {
            ContactID = MockContact.Two.ID,
            UserID = MockUser.Two.ID,
            CronString = "* * * 2,4,6,8,10,12 0,6",
        };
        //Three listens only on the fourth Saturday in March
        private ContactRegistration FourthSaturdayInMarch => new ContactRegistration
        {
            ContactID = MockContact.Three.ID,
            UserID = MockUser.Three.ID,
            CronString = "* * 23 3 6#4",
        };

    }
}
