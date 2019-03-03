using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message;

namespace Gouda.Application.Message.Command.Security.ViewModels
{
    public class CredentialsViewModel : BaseViewModel
    {
        public string EmailLabel { get; set; }
        public string Email { get; set; }

        public string PasswordLabel { get; set; }
        public string Password { get; set; }
    }
}
