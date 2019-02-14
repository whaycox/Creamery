using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message;

namespace Gouda.Application.Message.Command.Security.ViewModels
{
    public class CreateInitialUserViewModel : BaseViewModel
    {
        public CredentialsViewModel UserCredentials { get; set; }

        public string UserNameLabel { get; set; }
        public string UserName { get; set; }

        public string CreateUserButtonLabel { get; set; }
        public string CreateUserButton { get; set; }

        public CreateInitialUserViewModel()
        {
            UserCredentials = new CredentialsViewModel();
        }
    }
}
