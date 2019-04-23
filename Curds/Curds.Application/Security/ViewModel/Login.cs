using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message.ViewModel;

namespace Curds.Application.Security.ViewModel
{
    public class Login : BaseViewModel
    {
        public Credentials LoginCredentials { get; set; }

        public string LoginButton { get; set; }
        public string LoginButtonLabel { get; set; }
        
        public string ForgotPasswordButtonLabel { get; set; }
        public string ForgotPasswordButtonValue { get; set; }

        public Login()
        {
            LoginCredentials = new Credentials();
        }
    }
}
