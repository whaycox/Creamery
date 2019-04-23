using Curds.Domain;
using Curds.Domain.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security.Authentication;

namespace Curds.Application.Security
{
    public abstract class ISecurityTemplate<T> : CronTemplate<T> where T : ISecurity
    {
        private ViewModel.Credentials TestingCredentials => new ViewModel.Credentials
        {
            DeviceIdentifier = Testing.DeviceIdentifier,
            Email = MockUser.Two.Email,
            Password = Testing.Password,
            RememberMe = true,
        };
        private ViewModel.Login LoginViewModel => new ViewModel.Login() { LoginCredentials = TestingCredentials };
        private Command.Login LoginCommand => new Command.Login(LoginViewModel);
        private ViewModel.CreateInitialUser InitialUserViewModel => new ViewModel.CreateInitialUser() { UserName = nameof(InitialUserViewModel), UserCredentials = TestingCredentials };
        private Command.CreateInitialUser InitialUserCommand => new Command.CreateInitialUser(InitialUserViewModel);

        [TestMethod]
        public void RememberMeGeneratesSessionAndReAuth()
        {
            Authentication auth = TestObject.Login(LoginCommand).AwaitResult();
            Session generated = auth.Session;
            Assert.IsFalse(string.IsNullOrWhiteSpace(generated.Identifier));
            Assert.AreEqual(MockUser.Two.ID, generated.UserID);
            ReAuth reAuth = auth.ReAuthentication;
            Assert.AreEqual(Testing.DeviceIdentifier, reAuth.DeviceIdentifier);
            Assert.IsFalse(string.IsNullOrWhiteSpace(reAuth.Series));
            Assert.IsFalse(string.IsNullOrWhiteSpace(reAuth.Token));
            Assert.AreEqual(MockUser.Two.ID, reAuth.UserID);
        }

        [TestMethod]
        public void NoRememberMeGeneratesSessionOnly()
        {
            ViewModel.Login viewModel = LoginViewModel;
            viewModel.LoginCredentials.RememberMe = false;
            Authentication auth = TestObject.Login(new Command.Login(viewModel)).AwaitResult();
            Session generated = auth.Session;
            Assert.IsFalse(string.IsNullOrWhiteSpace(generated.Identifier));
            Assert.AreEqual(MockUser.Two.ID, generated.UserID);
            Assert.IsTrue(auth.ReAuthentication == null);
        }

        [TestMethod]
        public void CanValidateGeneratedSession()
        {
            Authentication auth = TestObject.Login(LoginCommand).AwaitResult();
            Assert.IsTrue(TestObject.Validate(new Command.ValidateSession(auth.Session.Identifier)).AwaitResult());
        }

        [TestMethod]
        public void InvalidSessionDoesntValidate()
        {
            Authentication auth = TestObject.Login(LoginCommand).AwaitResult();
            Assert.IsFalse(TestObject.Validate(new Command.ValidateSession(nameof(InvalidSessionDoesntValidate))).AwaitResult());
        }

        [TestMethod]
        public void DoesntValidateIfOnOrAfterExpiration()
        {
            Authentication login = TestObject.Login(LoginCommand).AwaitResult();
            Time.SetPointInTime(login.Session.Expiration);
            Assert.IsFalse(TestObject.Validate(new Command.ValidateSession(login.Session.Identifier)).AwaitResult());
        }

        [TestMethod]
        public void ValidateExtendsExpiration()
        {
            Authentication login = TestObject.Login(LoginCommand).AwaitResult();
            DateTimeOffset justBeforeExpiration = login.Session.Expiration.AddTicks(-5);
            login.Session.IncrementExpiration();
            DateTimeOffset expected = login.Session.Expiration;
            Time.SetPointInTime(justBeforeExpiration);
            Assert.IsTrue(TestObject.Validate(new Command.ValidateSession(login.Session.Identifier)).AwaitResult());
            Session stored = TestObject.Persistence.Sessions.Lookup(login.Session.Identifier).AwaitResult();
            Assert.AreEqual(expected, stored.Expiration);
        }

        [TestMethod]
        public void ReAuthGeneratesNewSession()
        {
            Authentication auth = TestObject.Login(LoginCommand).AwaitResult();
            Command.ReAuthenticate reAuthCommand = new Command.ReAuthenticate(auth.ReAuthentication.Series, auth.ReAuthentication.Token);
            Authentication reAuth = TestObject.ReAuthenticate(reAuthCommand).AwaitResult();
            Assert.IsFalse(TestObject.Validate(new Command.ValidateSession(auth.Session.Identifier)).AwaitResult());
            Assert.IsTrue(TestObject.Validate(new Command.ValidateSession(reAuth.Session.Identifier)).AwaitResult());
        }

        [TestMethod]
        public void SameUserMultipleSessionsValidate()
        {
            Authentication first = TestObject.Login(LoginCommand).AwaitResult();
            Authentication second = TestObject.Login(LoginCommand).AwaitResult();
            Authentication third = TestObject.Login(LoginCommand).AwaitResult();

            Assert.IsTrue(TestObject.Validate(new Command.ValidateSession(first.Session.Identifier)).AwaitResult());
            Assert.IsTrue(TestObject.Validate(new Command.ValidateSession(second.Session.Identifier)).AwaitResult());
            Assert.IsTrue(TestObject.Validate(new Command.ValidateSession(third.Session.Identifier)).AwaitResult());
        }

        [TestMethod]
        public void LogoutSeriesInvalidatesSession()
        {
            Authentication login = TestObject.Login(LoginCommand).AwaitResult();
            TestObject.Logout(new Command.LogoutSeries(login.ReAuthentication.Series)).AwaitResult();
            Assert.IsFalse(TestObject.Validate(new Command.ValidateSession(login.Session.Identifier)).AwaitResult());
        }

        [TestMethod]
        public void LogoutSeriesPreventsReAuth()
        {
            Authentication login = TestObject.Login(LoginCommand).AwaitResult();
            TestObject.Logout(new Command.LogoutSeries(login.ReAuthentication.Series)).AwaitResult();
            Command.ReAuthenticate reAuthCommand = new Command.ReAuthenticate(login.ReAuthentication.Series, login.ReAuthentication.Token);
            Assert.ThrowsException<AuthenticationException>(() => TestObject.ReAuthenticate(reAuthCommand).AwaitResult());
        }

        [TestMethod]
        public void LogoutSeriesADoesntInvalidateSeriesB()
        {
            Authentication first = TestObject.Login(LoginCommand).AwaitResult();
            Authentication second = TestObject.Login(LoginCommand).AwaitResult();

            TestObject.Logout(new Command.LogoutSeries(first.ReAuthentication.Series)).AwaitResult();

            Assert.IsFalse(TestObject.Validate(new Command.ValidateSession(first.Session.Identifier)).AwaitResult());
            Assert.IsTrue(TestObject.Validate(new Command.ValidateSession(second.Session.Identifier)).AwaitResult());
        }

        [TestMethod]
        public void LogoutUserInvalidatesAllSessions()
        {
            Authentication first = TestObject.Login(LoginCommand).AwaitResult();
            Authentication second = TestObject.Login(LoginCommand).AwaitResult();
            Authentication third = TestObject.Login(LoginCommand).AwaitResult();

            TestObject.Logout(new Command.LogoutUser(MockUser.Two.ID)).AwaitResult();

            Assert.IsFalse(TestObject.Validate(new Command.ValidateSession(first.Session.Identifier)).AwaitResult());
            Assert.IsFalse(TestObject.Validate(new Command.ValidateSession(second.Session.Identifier)).AwaitResult());
            Assert.IsFalse(TestObject.Validate(new Command.ValidateSession(third.Session.Identifier)).AwaitResult());
        }

        [TestMethod]
        public void LogoutUserADoesntInvalidateUserB()
        {
            ViewModel.Login viewModel = LoginViewModel;
            Authentication first = TestObject.Login(new Command.Login(viewModel)).AwaitResult();
            viewModel.LoginCredentials.Email = MockUser.Three.Email;
            Authentication second = TestObject.Login(new Command.Login(viewModel)).AwaitResult();

            TestObject.Logout(new Command.LogoutUser(MockUser.Two.ID)).AwaitResult();

            Assert.IsFalse(TestObject.Validate(new Command.ValidateSession(first.Session.Identifier)).AwaitResult());
            Assert.IsTrue(TestObject.Validate(new Command.ValidateSession(second.Session.Identifier)).AwaitResult());
        }

        [TestMethod]
        public void GoodSeriesBadTokenLogsOutUser()
        {
            Authentication login = TestObject.Login(LoginCommand).AwaitResult();
            Command.ReAuthenticate reAuthCommand = new Command.ReAuthenticate(login.ReAuthentication.Series, nameof(GoodSeriesBadTokenLogsOutUser));
            Assert.ThrowsException<AuthenticationException>(() => TestObject.ReAuthenticate(reAuthCommand).AwaitResult());

            Assert.IsFalse(TestObject.Validate(new Command.ValidateSession(login.Session.Identifier)).AwaitResult());
            reAuthCommand = new Command.ReAuthenticate(login.ReAuthentication.Series, login.ReAuthentication.Token);
            Assert.ThrowsException<AuthenticationException>(() => TestObject.ReAuthenticate(reAuthCommand).AwaitResult());
        }

        [TestMethod]
        public void CantCreateInitialUserIfUsersExist()
        {
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.CreateInitialUser(InitialUserCommand).AwaitResult());
        }

        [TestMethod]
        public void CreatesInitialUserIfNoUsers()
        {
            EmptyUsers();
            Authentication initialLogin = TestObject.CreateInitialUser(InitialUserCommand).AwaitResult();
            Assert.IsTrue(TestObject.Validate(new Command.ValidateSession(initialLogin.Session.Identifier)).AwaitResult());
        }
        protected abstract void EmptyUsers();
    }
}
