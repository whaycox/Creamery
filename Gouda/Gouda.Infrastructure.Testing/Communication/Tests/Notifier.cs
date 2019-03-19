using Curds;
using Curds.Domain;
using Gouda.Domain.Check;
using Gouda.Domain.Communication;
using Gouda.Domain.Communication.ContactAdapters;
using Gouda.Domain.Enumerations;
using Gouda.Domain.Persistence;
using Gouda.Domain.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Gouda.Infrastructure.Communication.Tests
{
    [TestClass]
    public class Notifier : CronTemplate<Communication.Notifier>
    {
        private static TimeSpan LocalOffset => DateTimeOffset.Now.Offset;
        private static DateTimeOffset FourthSaturdayInMarchTime => new DateTimeOffset(2019, 3, 23, 0, 0, 0, LocalOffset);
        private static DateTimeOffset AMonday => new DateTimeOffset(2019, 3, 18, 0, 0, 0, LocalOffset);
        private static DateTimeOffset ATuesday => new DateTimeOffset(2019, 3, 19, 0, 0, 0, LocalOffset);
        private static DateTimeOffset AWednesday => new DateTimeOffset(2019, 3, 20, 0, 0, 0, LocalOffset);

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
            MockContactThreeAdapter.Notifications.Clear();
        }

        [TestMethod]
        public void NotifiesOnStatusChange()
        {
            Evaluator.FireEvent(Definition);
            Assert.AreEqual(3, MockContactOneAdapter.Notifications.Count);
            Assert.AreEqual(MockUser.One.ID, MockContactOneAdapter.Notifications[0].userNotified);
            Assert.AreEqual(MockUser.Two.ID, MockContactOneAdapter.Notifications[1].userNotified);
            Assert.AreEqual(MockUser.Three.ID, MockContactOneAdapter.Notifications[2].userNotified);
            Assert.AreEqual(3, MockContactTwoAdapter.Notifications.Count);
            Assert.AreEqual(MockUser.One.ID, MockContactTwoAdapter.Notifications[0].userNotified);
            Assert.AreEqual(MockUser.Two.ID, MockContactTwoAdapter.Notifications[1].userNotified);
            Assert.AreEqual(MockUser.Three.ID, MockContactTwoAdapter.Notifications[2].userNotified);
            Assert.AreEqual(3, MockContactThreeAdapter.Notifications.Count);
            Assert.AreEqual(MockUser.One.ID, MockContactThreeAdapter.Notifications[0].userNotified);
            Assert.AreEqual(MockUser.Two.ID, MockContactThreeAdapter.Notifications[1].userNotified);
            Assert.AreEqual(MockUser.Three.ID, MockContactThreeAdapter.Notifications[2].userNotified);
        }

        [TestMethod]
        public void NotifiesDefinitionsByRegistration()
        {
            Persistence.EmptyDefinitionRegistrations();
            Persistence.DefinitionRegistrations.Insert(MonWedsFri);
            Persistence.DefinitionRegistrations.Insert(TuesThurs);

            Time.SetPointInTime(AMonday);
            Evaluator.FireEvent(Definition);
            Assert.AreEqual(1, MockContactOneAdapter.Notifications.Count);
            Assert.AreEqual(MockUser.One.ID, MockContactOneAdapter.Notifications[0].userNotified);
            Assert.AreEqual(1, MockContactTwoAdapter.Notifications.Count);
            Assert.AreEqual(MockUser.One.ID, MockContactTwoAdapter.Notifications[0].userNotified);
            Assert.AreEqual(1, MockContactThreeAdapter.Notifications.Count);
            Assert.AreEqual(MockUser.One.ID, MockContactThreeAdapter.Notifications[0].userNotified);

            Time.SetPointInTime(ATuesday);
            Evaluator.FireEvent(Definition);
            Assert.AreEqual(2, MockContactOneAdapter.Notifications.Count);
            Assert.AreEqual(MockUser.Two.ID, MockContactOneAdapter.Notifications[1].userNotified);
            Assert.AreEqual(2, MockContactTwoAdapter.Notifications.Count);
            Assert.AreEqual(MockUser.Two.ID, MockContactTwoAdapter.Notifications[1].userNotified);
            Assert.AreEqual(2, MockContactThreeAdapter.Notifications.Count);
            Assert.AreEqual(MockUser.Two.ID, MockContactTwoAdapter.Notifications[1].userNotified);
        }
        private Definition ResetDefinitionStatus(Definition definition)
        {
            definition.Status = Status.Unknown;
            return definition;
        }

        private DefinitionRegistration MonWedsFri => new DefinitionRegistration
        {
            DefinitionID = MockDefinition.SampleID,
            UserID = MockUser.One.ID,
            CronString = "* * * * 1,3,5",
        };

        private DefinitionRegistration TuesThurs => new DefinitionRegistration
        {
            DefinitionID = MockDefinition.SampleID,
            UserID = MockUser.Two.ID,
            CronString = "* * * * 2,4",
        };

        [TestMethod]
        public void NotifiesContactsByRegistration()
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
        
        private ContactRegistration OnlyOnWeekdays => new ContactRegistration
        {
            ContactID = 1,
            UserID = MockUser.One.ID,
            CronString = "* * * * 1-5",
        };
        private ContactRegistration WeekendsInEvenMonths => new ContactRegistration
        {
            ContactID = 5,
            UserID = MockUser.Two.ID,
            CronString = "* * * 2,4,6,8,10,12 0,6",
        };
        private ContactRegistration FourthSaturdayInMarch => new ContactRegistration
        {
            ContactID = 9,
            UserID = MockUser.Three.ID,
            CronString = "* * * 3 6#4",
        };

        [TestMethod]
        public void ComplexNotificationScenario()
        {
            Persistence.EmptyContactRegistrations();
            Persistence.EmptyDefinitionRegistrations();

            Persistence.DefinitionRegistrations.Insert(TuesWeds);
            Persistence.DefinitionRegistrations.Insert(MonWeds);
            Persistence.DefinitionRegistrations.Insert(MonTues);

            Persistence.ContactRegistrations.Insert(OneOnMondays(MockUser.Two.ID));
            Persistence.ContactRegistrations.Insert(OneOnMondays(MockUser.Three.ID));
            Persistence.ContactRegistrations.Insert(TwoOnTuesdays(MockUser.One.ID));
            Persistence.ContactRegistrations.Insert(TwoOnTuesdays(MockUser.Three.ID));
            Persistence.ContactRegistrations.Insert(ThreeOnWednesdays(MockUser.One.ID));
            Persistence.ContactRegistrations.Insert(ThreeOnWednesdays(MockUser.Two.ID));

            Time.SetPointInTime(AMonday);
            Evaluator.FireEvent(Definition);
            Assert.AreEqual(2, MockContactOneAdapter.Notifications.Count);
            Assert.AreEqual(0, MockContactTwoAdapter.Notifications.Count);
            Assert.AreEqual(0, MockContactThreeAdapter.Notifications.Count);
            Assert.IsTrue(UserWasNotifiedBy(2, 1));
            Assert.IsTrue(UserWasNotifiedBy(3, 1));

            Time.SetPointInTime(ATuesday);
            Evaluator.FireEvent(Definition);
            Assert.AreEqual(2, MockContactOneAdapter.Notifications.Count);
            Assert.AreEqual(2, MockContactTwoAdapter.Notifications.Count);
            Assert.AreEqual(0, MockContactThreeAdapter.Notifications.Count);
            Assert.IsTrue(UserWasNotifiedBy(1, 2));
            Assert.IsTrue(UserWasNotifiedBy(3, 2));

            Time.SetPointInTime(AWednesday);
            Evaluator.FireEvent(Definition);
            Assert.AreEqual(2, MockContactOneAdapter.Notifications.Count);
            Assert.AreEqual(2, MockContactTwoAdapter.Notifications.Count);
            Assert.AreEqual(2, MockContactThreeAdapter.Notifications.Count);
            Assert.IsTrue(UserWasNotifiedBy(1, 3));
            Assert.IsTrue(UserWasNotifiedBy(2, 3));
        }
        private bool UserWasNotifiedBy(int userID, int contactMethod)
        {
            switch(contactMethod)
            {
                case 1:
                    return MockContactOneAdapter.Notifications.Where(n => n.userNotified == userID).Any();
                case 2:
                    return MockContactTwoAdapter.Notifications.Where(n => n.userNotified == userID).Any();
                case 3:
                    return MockContactThreeAdapter.Notifications.Where(n => n.userNotified == userID).Any();
                default:
                    throw new NotImplementedException();
            }
        }

        private DefinitionRegistration TuesWeds => new DefinitionRegistration
        {
            DefinitionID = MockDefinition.SampleID,
            UserID = MockUser.One.ID,
            CronString = "* * * * 2,3",
        };
        private DefinitionRegistration MonWeds => new DefinitionRegistration
        {
            DefinitionID = MockDefinition.SampleID,
            UserID = MockUser.Two.ID,
            CronString = "* * * * 1,3",
        };
        private DefinitionRegistration MonTues => new DefinitionRegistration
        {
            DefinitionID = MockDefinition.SampleID,
            UserID = MockUser.Three.ID,
            CronString = "* * * * 1,2",
        };

        private ContactRegistration OneOnMondays(int userID) => new ContactRegistration
        {
            ContactID = userID == 2 ? 4 : 7,
            UserID = userID,
            CronString = "* * * * 1",
        };
        private ContactRegistration TwoOnTuesdays(int userID) => new ContactRegistration
        {
            ContactID = userID == 1 ? 2 : 8,
            UserID = userID,
            CronString = "* * * * 2",
        };
        private ContactRegistration ThreeOnWednesdays(int userID) => new ContactRegistration
        {
            ContactID = userID == 1 ? 3 : 6,
            UserID = userID,
            CronString = "* * * * 3",
        };
    }
}
