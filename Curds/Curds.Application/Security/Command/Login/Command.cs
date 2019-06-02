using System;
using System.Threading.Tasks;

namespace Curds.Application.Security.Command.Login
{
    using Application.Command.Domain;
    using Domain;

    public class Command : BaseCommand
    {
        public string DeviceIdentifier { get; }
        public string Email { get; }
        public string Password { get; }
        public bool RememberMe { get; }

        public Command(ViewModel viewModel)
        {
            Credentials credentials = viewModel.LoginCredentials;

            DeviceIdentifier = credentials.DeviceIdentifier;
            Email = credentials.Email;
            Password = credentials.Password;
            RememberMe = credentials.RememberMe;
        }

        public Command(CreateInitialUser.Command createInitialLogin)
        {
            DeviceIdentifier = createInitialLogin.DeviceIdentifier;
            Email = createInitialLogin.Email;
            Password = createInitialLogin.Password;
            RememberMe = createInitialLogin.RememberMe;
        }
    }
}
