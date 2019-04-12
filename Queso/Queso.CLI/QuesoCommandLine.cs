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

namespace Queso.CLI
{
    using Operations;

    public class QuesoCommandLine : CommandLineApplication<QuesoApplication>
    {
        protected override IEnumerable<Operation<QuesoApplication>> Operations => QuesoOperations.Operations(Application);

        public QuesoCommandLine(QuesoApplication application, IConsoleWriter writer)
            : base(application, writer)
        { }

        protected async override Task ExecuteOperation(OperationParser<QuesoApplication>.ParsedPair parsedPair)
        {
            switch (parsedPair.Operation)
            {
                case ResurrectOperation rez:
                    string pathToChar = parsedPair.Arguments[ImplicitArgument][0].RawValue;
                    ResurrectHandler handler = Application.Commands.Resurrect.Handler();
                    await handler.Execute(new ResurrectCommand(pathToChar));
                    break;
                default:
                    throw new InvalidOperationException("Unexpected operation");
            }
        }
    }
}
