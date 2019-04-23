using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message.ViewModel;
using Curds.Application.Security.ViewModel;

namespace Curds.Application.Security.ViewModel
{
    public class CreateInitialUser : BaseViewModel
    {
        public Credentials UserCredentials { get; set; }

        public string UserNameLabel { get; set; }
        public string UserName { get; set; }

        public string CreateUserButtonLabel { get; set; }
        public string CreateUserButton { get; set; }

        public CreateInitialUser()
        {
            UserCredentials = new Credentials();
        }
    }
}
