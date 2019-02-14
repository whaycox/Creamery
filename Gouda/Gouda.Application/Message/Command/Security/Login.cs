using Curds.Application.Message;
using Gouda.Domain.Security;
using System;

namespace Gouda.Application.Message.Command.Security
{
    using ViewModels;

    public class LoginCommand : BaseCommand<LoginViewModel>
    {
        public string Email { get; }
        public string Password { get; }

        public LoginCommand(LoginViewModel viewModel)
            : base(viewModel)
        {
            Email = viewModel.LoginCredentials.Email;
            Password = viewModel.LoginCredentials.Password;
        }
    }

    public class LoginHandler : CommandHandler<LoginCommand, LoginViewModel>
    {
        public LoginHandler(GoudaApplication application)
            : base(application)
        { }

        public AuthenticationToken Handle(LoginCommand command)
        {
            throw new NotImplementedException();
        }
    }

    public class LoginDefinition : CommandDefinition<LoginCommand, LoginHandler, LoginViewModel>
    {
        public override BaseViewModel ViewModel
        {
            get
            {
                if (Application.Persistence.Users.Count == 0)
                    return new CreateInitialUserViewModel();
                else
                    return new LoginViewModel();
            }
        }

        protected override LoginHandler Handler => new LoginHandler(Application);

        public LoginDefinition(GoudaApplication application)
            : base(application)
        { }

        protected override LoginCommand BuildMessage(LoginViewModel viewModel) => new LoginCommand(viewModel);
    }
}
