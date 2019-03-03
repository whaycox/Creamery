using Curds.Application.Message;
using Gouda.Application.Security;
using Gouda.Domain.Security;
using System;

namespace Gouda.Application.Message.Command.Security
{
    using System.Threading.Tasks;
    using ViewModels;

    public class CreateInitialUserCommand : BaseCommand<CreateInitialUserViewModel>
    {
        public string UserName { get; }
        public string Email { get; }
        public string Salt { get; }
        public string Password { get; }

        public CreateInitialUserCommand(ISecurity security, CreateInitialUserViewModel viewModel)
            : base(viewModel)
        {
            UserName = viewModel.UserName;
            Email = viewModel.UserCredentials.Email;
            Salt = security.GenerateSalt();
            Password = security.EncryptPassword(Salt, viewModel.UserCredentials.Password);
        }

        public User Entity => new User
        {
            Name = UserName,
            Email = Email,
            Salt = Salt,
            Password = Password,
        };
    }

    public class CreateInitialUserHandler : CommandHandler<CreateInitialUserCommand, CreateInitialUserViewModel>
    {
        public CreateInitialUserHandler(GoudaApplication application)
            : base(application)
        { }

        public Task<User> Handle(CreateInitialUserCommand message) => Application.Persistence.Users.Insert(message.Entity);
    }

    public class CreateInitialUserDefinition : CommandDefinition<CreateInitialUserCommand, CreateInitialUserHandler, CreateInitialUserViewModel>
    {
        protected override CreateInitialUserHandler Handler => new CreateInitialUserHandler(Application);

        public CreateInitialUserDefinition(GoudaApplication application)
            : base(application)
        { }

        public async Task<User> Execute(CreateInitialUserViewModel viewModel) => await new CreateInitialUserHandler(Application).Handle(BuildMessage(viewModel));

        public override Task<BaseViewModel> ViewModel()
        {
            throw new NotImplementedException();
        }

        protected override CreateInitialUserCommand BuildMessage(CreateInitialUserViewModel viewModel) => new CreateInitialUserCommand(Application.Security, viewModel);
    }
}
