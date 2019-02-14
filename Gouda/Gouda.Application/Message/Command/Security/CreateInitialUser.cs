using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Message;
using Gouda.Domain;
using Gouda.Application.Security;
using Gouda.Domain.Security;

namespace Gouda.Application.Message.Command.Security
{
    using ViewModels;

    public class CreateInitialUserCommand : BaseCommand<CreateInitialUserViewModel>
    {
        public string UserName { get; }
        public string Email { get; }
        public string Salt { get; }
        public string Password { get; }

        public CreateInitialUserCommand(ISecurity security, CreateInitialUserViewModel viewModel)
            : base(viewModel)
        { }

        public User Entity => new User
        {
            Name = UserName,
        };
    }

    public class CreateInitialUserHandler : CommandHandler<CreateInitialUserCommand, CreateInitialUserViewModel>
    {
        public CreateInitialUserHandler(GoudaApplication application)
            : base(application)
        { }

        public User Handle(CreateInitialUserCommand message) => Application.Persistence.Users.Insert(message.Entity);
    }

    public class CreateInitialUserDefinition : CommandDefinition<CreateInitialUserCommand, CreateInitialUserHandler, CreateInitialUserViewModel>
    {
        public override BaseViewModel ViewModel => throw new NotImplementedException();

        protected override CreateInitialUserHandler Handler => new CreateInitialUserHandler(Application);

        public CreateInitialUserDefinition(GoudaApplication application)
            : base(application)
        { }

        public User Execute(CreateInitialUserViewModel viewModel) => new CreateInitialUserHandler(Application).Handle(BuildMessage(viewModel));

        protected override CreateInitialUserCommand BuildMessage(CreateInitialUserViewModel viewModel) => new CreateInitialUserCommand(Application.Security, viewModel);
    }
}
