using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message.ViewModel;

namespace Curds.Application.Security.ViewModel
{
    public class Credentials : BaseViewModel
    {
        public string DeviceIdentifier { get; set; }

        public string EmailLabel { get; set; }
        public string Email { get; set; }

        public string PasswordLabel { get; set; }
        public string Password { get; set; }

        public string RememberMeLabel { get; set; }
        public bool RememberMe { get; set; }
    }
}
