using Curds;
using Curds.CLI;
using Curds.CLI.Operations;
using Queso.Application;
using Queso.Application.Message.Command.Character;
using Queso.Application.Message.ViewModels;
using Queso.Application.Message.Query.Character;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Curds.CLI.Formatting;
using Curds.CLI.Formatting.Tokens;

namespace Queso.CLI
{
    using Operations;

    public class QuesoCommandLine : CommandLineApplication<QuesoApplication>
    {
        protected override IEnumerable<Operation> Operations => QuesoOperations.Operations(Application);

        public QuesoCommandLine(QuesoApplication application, IConsoleWriter writer)
            : base(application, writer)
        { }

        protected async override Task ExecuteOperation(OperationParser<QuesoApplication>.ParsedPair parsedPair)
        {
            switch (parsedPair.Operation)
            {
                case ResurrectOperation rez:
                    string pathToChar = parsedPair.Arguments[ImplicitArgument][0].RawValue;
                    ResurrectCommand command = new ResurrectCommand(pathToChar);
                    ScanQuery query = await Application.Commands.Resurrect.Execute(command);
                    RenderCharacter(await Application.Queries.Scan.Execute(query)).Write(Writer);
                    break;
                default:
                    throw new InvalidOperationException("Unexpected operation");
            }
        }

        private FormattedText RenderCharacter(Character viewModel) => FormattedText.New
            .AppendLine(PlainTextToken.Create($"Name: {viewModel.Name}"))
            .AppendLine(PlainTextToken.Create($"Class: {viewModel.Class}"))
            .AppendLine(PlainTextToken.Create($"IsAlive? {viewModel.Alive}"));
    }
}
