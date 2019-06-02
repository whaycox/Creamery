namespace Curds.Application.Security.Command.CreateInitialUser
{
    using Application.Command.Domain;
    using Security.Command.Domain;

    public class Command : BaseCommand
    {
        public string UserName { get; }
        public string DeviceIdentifier { get; }
        public string Email { get; }
        public string Password { get; }
        public bool RememberMe { get; }

        public Command(ViewModel viewModel)
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
