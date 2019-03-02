using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain;
using Gouda.Domain.Security;
using Gouda.Domain;
using Gouda.Application.Message.Command.Security.ViewModels;
using Curds;

namespace Gouda.Application.Message.Command.Security.Tests
{
    [TestClass]
    public class Login : TestTemplate
    {
        private MockOptions Options = new MockOptions();
        private GoudaApplication Application = null;

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
            Assert.IsInstanceOfType(Application.Commands.Login.ViewModel().AwaitResult(), typeof(CreateInitialUserViewModel));
        }

        [TestMethod]
        public void UsersViewModelLogin()
        {
            Assert.IsInstanceOfType(Application.Commands.Login.ViewModel().AwaitResult(), typeof(LoginViewModel));
        }

        [TestMethod]
        public void CreatesInitialUser()
        {
            Options.MockPersistence.EmptyUsers();
            CreateInitialUserViewModel viewModel = Application.Commands.Login.ViewModel().AwaitResult() as CreateInitialUserViewModel;
            viewModel.UserName = nameof(CreatesInitialUser);
            viewModel.UserCredentials.Email = Testing.TestEmail;
            viewModel.UserCredentials.Password = Curds.Testing.TestPassword;

            User newUser = Application.Commands.CreateInitialUser.Execute(viewModel).AwaitResult();
            Assert.AreEqual(1, newUser.ID);
            Assert.AreEqual(Testing.TestEmail, newUser.Email);
            Assert.AreNotEqual(Curds.Testing.TestPassword, newUser.Password);
            Assert.AreEqual(1, Application.Persistence.Users.Count.AwaitResult());
        }
    }
}
