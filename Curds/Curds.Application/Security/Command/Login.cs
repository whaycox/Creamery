using System;
using System.Threading.Tasks;
using Curds.Domain.Security;

namespace Curds.Application.Security.Command
{
    using Message.Command;

    public class Login : BaseCommand
    {
        public string DeviceIdentifier { get; }
        public string Email { get; }
        public string Password { get; }
        public bool RememberMe { get; }

        public Login(ViewModel.Login viewModel)
        {
            ViewModel.Credentials credentials = viewModel.LoginCredentials;

            DeviceIdentifier = credentials.DeviceIdentifier;
            Email = credentials.Email;
            Password = credentials.Password;
            RememberMe = credentials.RememberMe;
        }

        public Login(CreateInitialUser createInitialLogin)
        {
            DeviceIdentifier = createInitialLogin.DeviceIdentifier;
            Email = createInitialLogin.Email;
            Password = createInitialLogin.Password;
            RememberMe = createInitialLogin.RememberMe;
        }
    }
}
