using Curds;
using Curds.CLI;
using Curds.CLI.Operations;
using Queso.Application;
using Queso.Application.Message.Command.Character;
using System;
using System.Collections.Generic;

namespace Queso.CLI
{
    using Operations;

    public class QuesoCommandLine : CommandLineApplication<QuesoApplication>
    {
        protected override IEnumerable<Operation<QuesoApplication>> Operations => QuesoOperations.Operations(Application);

        public QuesoCommandLine(QuesoApplication application, IConsoleWriter writer)
            : base(application, writer)
        { }

        protected override void ExecuteOperation(OperationParser<QuesoApplication>.ParsedPair parsedPair)
        {
            switch (parsedPair.Operation)
            {
                case ResurrectOperation rez:
                    string pathToChar = parsedPair.Arguments[ImplicitArgument][0].RawValue;
                    ViewModel model = Application.Commands.Resurrect.ViewModel().AwaitResult() as ViewModel;
                    model.CharacterPath = pathToChar;
                    Application.Commands.Resurrect.Execute(model);
                    break;
                default:
                    throw new InvalidOperationException("Unexpected operation");
            }
        }
    }
}
