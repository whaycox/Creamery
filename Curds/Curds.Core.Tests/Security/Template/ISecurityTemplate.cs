using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Authentication;
using System.Threading.Tasks;

namespace Curds.Security.Template
{
    using Abstraction;
    using Credentials.Domain;
    using Domain;
    using Persistence.Mock;

    public abstract class ISecurityTemplate<T> : Test<T> where T : ISecurity
    {
        private const int MockUserID = 1;

        private Mock.Session MockSession = new Mock.Session(1);
        private Password MockPassword = new Credentials.Mock.Password(MockUserID);
        private User MockUser = new Mock.User(MockUserID);

        protected ISecurityPersistence MockPersistence = new ISecurityPersistence();

        [TestMethod]
        public async Task RememberMeGeneratesSessionAndReAuth()
        {
            Authentication auth = await TestObject.Login(MockPassword);
            Session generated = auth.Session;
            Assert.IsFalse(string.IsNullOrWhiteSpace(generated.Identifier));
            Assert.AreEqual(MockUser.ID, generated.UserID);
            ReAuth reAuth = auth.ReAuthentication;
            Assert.AreEqual(Testing.DeviceIdentifier, reAuth.DeviceIdentifier);
            Assert.IsFalse(string.IsNullOrWhiteSpace(reAuth.Series));
            Assert.IsFalse(string.IsNullOrWhiteSpace(reAuth.Token));
            Assert.AreEqual(MockUser.ID, reAuth.UserID);
        }

        [TestMethod]
        public async Task NoRememberMeGeneratesSessionOnly()
        {
            MockPassword.RememberMe = false;
            Authentication auth = await TestObject.Login(MockPassword);
            Session generated = auth.Session;
            Assert.IsFalse(string.IsNullOrWhiteSpace(generated.Identifier));
            Assert.AreEqual(MockUser.ID, generated.UserID);
            Assert.IsTrue(auth.ReAuthentication == null);
        }

        [TestMethod]
        public async Task CanValidateExistingSession()
        {
            Assert.IsTrue(await TestObject.Validate(nameof(Session.Identifier)));
        }

        [TestMethod]
        public async Task InvalidSessionDoesntValidate()
        {
            Assert.IsFalse(await TestObject.Validate(nameof(InvalidSessionDoesntValidate)));
        }

        [TestMethod]
        public async Task DoesntValidateIfOnOrAfterExpiration()
        {
            Time.SetPointInTime(MockSession.Expiration);
            Assert.IsFalse(await TestObject.Validate(nameof(Session.Identifier)));
        }

        [TestMethod]
        public async Task ValidateUpdatesExpiration()
        {
            DateTimeOffset justBeforeExpiration = MockSession.Expiration.AddTicks(-5);
            MockSession.ExtendExpiration(MockSession.Expiration);
            DateTimeOffset expected = MockSession.Expiration.AddTicks(-5);
            Time.SetPointInTime(justBeforeExpiration);
            Assert.IsTrue(await TestObject.Validate(nameof(Session.Identifier)));
            Assert.AreEqual(1, MockPersistence.MockSessions.UpdatedSessions.Count);
            Assert.AreEqual(expected, MockPersistence.MockSessions.UpdatedSessions[0].expiration);
        }

        [TestMethod]
        public async Task ReAuthUpdatesToken()
        {
            Authentication reAuth = await TestObject.ReAuthenticate(nameof(ReAuth.Series), nameof(ReAuth.Token));
            Assert.AreEqual(1, MockPersistence.MockReAuths.UpdatedReAuths.Count);
            Assert.AreEqual(nameof(ReAuth.Series), MockPersistence.MockReAuths.UpdatedReAuths[0].series);
            Assert.IsFalse(string.IsNullOrWhiteSpace(MockPersistence.MockReAuths.UpdatedReAuths[0].token));
        }

        [TestMethod]
        public async Task ReAuthGeneratesNewSessionForSameSeries()
        {
            Authentication reAuth = await TestObject.ReAuthenticate(nameof(ReAuth.Series), nameof(ReAuth.Token));
            Assert.AreEqual(1, MockPersistence.MockSessions.DeletedSeries.Count);
            Assert.AreEqual(nameof(ReAuth.Series), MockPersistence.MockSessions.DeletedSeries[0]);
            Assert.AreEqual(1, MockPersistence.MockSessions.InsertedSessions.Count);
            Assert.AreEqual(nameof(ReAuth.Series), MockPersistence.MockSessions.InsertedSessions[0].Series);
            Assert.AreNotEqual(nameof(ReAuth.Token), MockPersistence.MockSessions.InsertedSessions[0].Identifier);
        }

        [TestMethod]
        public async Task SameUserCanLoginMultipleTimes()
        {
            Authentication first = await TestObject.Login(MockPassword);
            Authentication second = await TestObject.Login(MockPassword);
            Authentication third = await TestObject.Login(MockPassword);

            Assert.AreEqual(3, MockPersistence.MockReAuths.InsertedReAuths.Count);
            Assert.AreEqual(0, MockPersistence.MockReAuths.DeletedSeries.Count);
            Assert.AreEqual(0, MockPersistence.MockReAuths.DeletedUsers.Count);

            Assert.AreEqual(3, MockPersistence.MockSessions.InsertedSessions.Count);
            Assert.AreEqual(0, MockPersistence.MockSessions.DeletedDateTimes.Count);
            Assert.AreEqual(0, MockPersistence.MockSessions.DeletedSeries.Count);
            Assert.AreEqual(0, MockPersistence.MockSessions.DeletedUsers.Count);
        }

        [TestMethod]
        public async Task LogoutSeriesDeletesSessionAndReAuth()
        {
            await TestObject.Logout(nameof(Session.Series));

            Assert.AreEqual(1, MockPersistence.MockReAuths.DeletedSeries.Count);
            Assert.AreEqual(nameof(Session.Series), MockPersistence.MockReAuths.DeletedSeries[0]);

            Assert.AreEqual(1, MockPersistence.MockSessions.DeletedSeries.Count);
            Assert.AreEqual(nameof(Session.Series), MockPersistence.MockSessions.DeletedSeries[0]);
        }

        [TestMethod]
        public async Task LogoutUserDeletesUserSessionAndReAuth()
        {
            await TestObject.Logout(MockUser.ID);

            Assert.AreEqual(1, MockPersistence.MockReAuths.DeletedUsers.Count);
            Assert.AreEqual(MockUser.ID, MockPersistence.MockReAuths.DeletedUsers[0]);

            Assert.AreEqual(1, MockPersistence.MockSessions.DeletedUsers.Count);
            Assert.AreEqual(MockUser.ID, MockPersistence.MockSessions.DeletedUsers[0]);
        }

        [TestMethod]
        public async Task GoodSeriesBadTokenLogsOutUser()
        {
            await Assert.ThrowsExceptionAsync<AuthenticationException>(() => TestObject.ReAuthenticate(nameof(ReAuth.Series), nameof(GoodSeriesBadTokenLogsOutUser)));

            Assert.AreEqual(1, MockPersistence.MockReAuths.DeletedUsers.Count);
            Assert.AreEqual(MockUser.ID, MockPersistence.MockReAuths.DeletedUsers[0]);

            Assert.AreEqual(1, MockPersistence.MockSessions.DeletedUsers.Count);
            Assert.AreEqual(MockUser.ID, MockPersistence.MockSessions.DeletedUsers[0]);
        }
    }
}
