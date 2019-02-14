using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message;

namespace Gouda.Application.Message.Command.Security.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public CredentialsViewModel LoginCredentials { get; set; }

        public string LoginButton { get; set; }
        public string LoginButtonLabel { get; set; }
        
        public string ForgotPasswordButtonLabel { get; set; }
        public string ForgotPasswordButtonValue { get; set; }

        public LoginViewModel()
        {
            LoginCredentials = new CredentialsViewModel();
        }
    }
}
