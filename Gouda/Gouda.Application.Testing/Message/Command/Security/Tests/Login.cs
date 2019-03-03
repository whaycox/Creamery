using Curds;
using Curds.Domain;
using Gouda.Application.Message.Command.Security.ViewModels;
using Gouda.Domain;
using Gouda.Domain.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Gouda.Application.Message.Command.Security.Tests
{
    [TestClass]
    public class Login : TestTemplate
    {
        private MockOptions Options = new MockOptions();
        private GoudaApplication Application = null;

        private CreateInitialUserViewModel InitialUserModel => Application.Commands.Login.ViewModel().AwaitResult() as CreateInitialUserViewModel;
        private LoginViewModel LoginModel => Application.Commands.Login.ViewModel().AwaitResult() as LoginViewModel;

        [TestInitialize]
        public void Init()
        {
            Options.MockPersistence.Reset();
            Application = new GoudaApplication(Options);
        }

        [TestCleanup]
        public void Clean()
        {
            Application.Dispose();
        }

        [TestMethod]
        public void NoUsersViewModelCreatesInitialUser()
        {
            Options.MockPersistence.EmptyUsers();
            Assert.IsNotNull(InitialUserModel);
        }

        [TestMethod]
        public void UsersViewModelLogin()
        {
            Assert.IsNotNull(LoginModel);
        }

        [TestMethod]
        public void CreatesInitialUser()
        {
            Options.MockPersistence.EmptyUsers();
            CreateInitialUserViewModel viewModel = InitialUserModel;
            viewModel.UserName = nameof(CreatesInitialUser);
            viewModel.UserCredentials.Email = Testing.TestEmail;
            viewModel.UserCredentials.Password = Curds.Testing.TestPassword;

            User newUser = Application.Commands.CreateInitialUser.Execute(viewModel).AwaitResult();
            Assert.AreEqual(1, newUser.ID);
            Assert.AreEqual(nameof(CreatesInitialUser), newUser.Name);
            Assert.AreEqual(Testing.TestEmail, newUser.Email);
            Assert.AreNotEqual(Curds.Testing.TestPassword, newUser.Password);
            Assert.AreEqual(1, Application.Persistence.Users.Count.AwaitResult());
        }

        [TestMethod]
        public void GenerateSessionWithCorrectPassword()
        {
            LoginViewModel viewModel = LoginModel;
            viewModel.LoginCredentials.Email = Testing.TestEmail;
            viewModel.LoginCredentials.Password = Curds.Testing.TestPassword;
            viewModel.DeviceIdentifier = nameof(GenerateSessionWithCorrectPassword);

            Session generated = Application.Commands.Login.Execute(viewModel).AwaitResult();
            Assert.IsFalse(string.IsNullOrWhiteSpace(generated.Identifier));
        }

        [TestMethod]
        public void ErrorGenerateSessionWithBadPassword()
        {
            LoginViewModel viewModel = LoginModel;
            viewModel.LoginCredentials.Email = Testing.TestEmail;
            viewModel.LoginCredentials.Password = nameof(ErrorGenerateSessionWithBadPassword);
            viewModel.DeviceIdentifier = nameof(ErrorGenerateSessionWithBadPassword);

            Assert.ThrowsExceptionAsync<InvalidOperationException>(() => Application.Commands.Login.Execute(viewModel));
        }
    }
}
