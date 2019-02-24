using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain;
using Gouda.Domain.Security;
using Gouda.Domain;
using Gouda.Application.Message.Command.Security.ViewModels;

namespace Gouda.Application.Message.Command.Security.Tests
{
    [TestClass]
    public class Login 
    {
        private MockOptions Options = new MockOptions();
        private GoudaApplication Application = null;

        [TestInitialize]
        public void Init()
        {
            throw new NotImplementedException();
            //Application = new GoudaApplication(Options);
        }

        [TestCleanup]
        public void Clean()
        {
            Application.Dispose();
        }

        [TestMethod]
        public void NoUsersViewModelCreatesInitialUser()
        {
            throw new NotImplementedException();
            //Assert.IsInstanceOfType(Application.Commands.Login.ViewModel, typeof(CreateInitialUserViewModel));
        }

        [TestMethod]
        public void UsersViewModelLogin()
        {
            Assert.IsInstanceOfType(Application.Commands.Login.ViewModel, typeof(LoginViewModel));
        }

        [TestMethod]
        public void CreatesInitialUser()
        {
            throw new NotImplementedException();
            //CreateInitialUserViewModel viewModel = Application.Commands.Login.ViewModel as CreateInitialUserViewModel;
            //viewModel.UserCredentials.Email = Testing.TestEmail;
            //viewModel.UserCredentials.Password = Curds.Testing.TestPassword;

            //User newUser = Application.Commands.CreateInitialUser.Execute(viewModel);
            //Assert.AreEqual(1, newUser.ID);
            //Assert.AreEqual(Testing.TestEmail, newUser.Email);
            //Assert.AreNotEqual(Curds.Testing.TestPassword, newUser.Password);
            //Assert.AreEqual(1, Application.Persistence.Users.Count);
        }
    }
}
