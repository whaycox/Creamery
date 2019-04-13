using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Curds.Application.Message.Command;
using Curds.Application.Message;

namespace Queso.Application.Message.Command.Character
{
    using Query.Character;
    using Queso.Application.Message.ViewModels;

    public class ResurrectCommand : BaseCommand
    {
        public string CharacterPath { get; }

        public ResurrectCommand(string characterPath)
        {
            CharacterPath = characterPath;
        }
    }

    public class ResurrectDefinition : BaseCommandDefinition<QuesoApplication, CharacterPath, ResurrectCommand, ScanQuery>
    {
        public ResurrectDefinition(QuesoApplication application)
            : base(application)
        { }

        public override CharacterPath ViewModel => new CharacterPath();
        public override Task<ScanQuery> Execute(ResurrectCommand message) => Task.Factory.StartNew(() => ExecuteAndReturn(message));
        private ScanQuery ExecuteAndReturn(ResurrectCommand command)
        {
            Application.Character.Resurrect(command.CharacterPath);
            return new ScanQuery(command.CharacterPath);
        }
    }
}
