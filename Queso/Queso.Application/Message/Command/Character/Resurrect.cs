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

    public class ResurrectHandler : QueryingCommandHandler<QuesoApplication, ResurrectCommand, Character>
    {
        public ResurrectHandler(QuesoApplication application)
            : base(application)
        { }

        public async override Task<Character> Execute(ResurrectCommand command)
        {
            Application.Character.Resurrect(command.CharacterPath);
            ScanQuery returnedQuery = new ScanQuery(command.CharacterPath);
            ScanHandler handler = Application.Queries.Scan.Handler();
            return await handler.Execute(returnedQuery);
        }
    }

    public class ResurrectDefinition : BaseCommandDefinition<QuesoApplication, ResurrectHandler, ResurrectCommand, CharacterPath>
    {
        public ResurrectDefinition(QuesoApplication application)
            : base(application)
        { }

        public override CharacterPath ViewModel => new CharacterPath();
        public override ResurrectHandler Handler() => new ResurrectHandler(Application);
    }
}
