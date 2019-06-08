namespace Curds.Application.Security.Command.Domain
{
    using Application.Domain;

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
