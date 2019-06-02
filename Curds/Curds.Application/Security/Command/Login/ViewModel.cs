namespace Curds.Application.Security.Command.Login
{
    using Application.Domain;
    using Domain;

    public class ViewModel : BaseViewModel
    {
        public Credentials LoginCredentials { get; set; }

        public string LoginButton { get; set; }
        public string LoginButtonLabel { get; set; }

        public string ForgotPasswordButtonLabel { get; set; }
        public string ForgotPasswordButtonValue { get; set; }

        public ViewModel()
        {
            LoginCredentials = new Credentials();
        }
    }
}
