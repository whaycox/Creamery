using Curds.Application.Message;
using System;
using Curds.Application.Message.Command;
using System.Threading.Tasks;
using Curds.Application.Security;
using Curds.Domain.Security;

namespace Curds.Application.Security.Command
{
    using ViewModel;

    public class CreateInitialUser : BaseCommand
    {
        public string UserName { get; }
        public string DeviceIdentifier { get; }
        public string Email { get; }
        public string Password { get; }
        public bool RememberMe { get; }

        public CreateInitialUser(ViewModel.CreateInitialUser viewModel)
        {
            Credentials credentials = viewModel.UserCredentials;

            UserName = viewModel.UserName;
            DeviceIdentifier = credentials.DeviceIdentifier;
            Email = credentials.Email;
            Password = credentials.Password;
            RememberMe = credentials.RememberMe;
        }
    }
}
