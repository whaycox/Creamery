using System;
using System.Collections.Generic;
using System.Text;

namespace Curds.Application.Security.Command.CreateInitialUser
{
    using Application.Domain;
    using Domain;

    public class ViewModel : BaseViewModel
    {
        public Credentials UserCredentials { get; set; }

        public string UserNameLabel { get; set; }
        public string UserName { get; set; }

        public string CreateUserButtonLabel { get; set; }
        public string CreateUserButton { get; set; }

        public ViewModel()
        {
            UserCredentials = new Credentials();
        }
    }
}
