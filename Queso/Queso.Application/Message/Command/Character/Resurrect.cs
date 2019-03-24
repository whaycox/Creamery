using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Curds.Application.Message;

namespace Queso.Application.Message.Command.Character
{
    public class ResurrectCommand : BaseCommand<ViewModel>
    {
        public string CharacterPath { get; }

        public ResurrectCommand(ViewModel viewModel)
            : base(viewModel)
        {
            CharacterPath = viewModel.CharacterPath;
        }
    }

    public class ResurrectHandler : CommandHandler<ResurrectCommand, ViewModel>
    {
        public ResurrectHandler(QuesoApplication application)
            : base(application)
        { }

        public void Handle(ResurrectCommand command) => Application.Character.Resurrect(command.CharacterPath);
    }

    public class ResurrectDefinition : CommandDefinition<ResurrectCommand, ResurrectHandler, ViewModel>
    {
        public override string Name => "Resurrect";
        public override string Description => "Bring a dead hardcore character back to life.";

        public ResurrectDefinition(QuesoApplication application)
            : base(application)
        { }

        protected override ResurrectHandler Handler => new ResurrectHandler(Application);

        public void Execute(ViewModel viewModel) => Handler.Handle(BuildMessage(viewModel));

        public override Task<BaseViewModel> ViewModel() => Task.Factory.StartNew<BaseViewModel>(() => new ViewModel());

        protected override ResurrectCommand BuildMessage(ViewModel viewModel) => new ResurrectCommand(viewModel);
    }

}
