using System;
using Queso.Application.Character;
using Queso.Infrastructure.Character;
using Queso.Application;
using Queso.Infrastructure;
using Curds;
using Queso.Application.Message.Command.Character;
using System.Collections.Generic;
using Curds.CLI.Operations;
using Curds.CLI;
using Curds.CLI.Formatting;

namespace Queso.CLI
{
    using Operations;

    class Program
    {
        private static QuesoApplication Application = new QuesoApplication(new DefaultOptions());

        private static OperationParser<QuesoApplication> Parser = new OperationParser<QuesoApplication>();
        private static IEnumerable<Operation<QuesoApplication>> Operations = null;

        private static string ImplicitArgument = ArgumentlessOperation<QuesoApplication>.Argumentless;

        private static void Usage(int exitCode, string message, params object[] args) => Usage(exitCode, string.Format(message, args));
        private static void Usage(int exitCode, string message)
        {
            ConsoleWriter writer = new ConsoleWriter();
            FormattedText usage = new FormattedText();
            usage.AddLine(PlainTextToken.Create(message));
            usage.AddLine(PlainTextToken.Create($"{AppDomain.CurrentDomain.FriendlyName} | {Application.Description}"));
            usage.Add(Parser.OperationUsages(Operations));
            usage.Write(writer);
            Environment.Exit(exitCode);
        }

        static void Main(string[] args)
        {
            Operations = QuesoOperations.Operations(Application);
            try
            {
                var parsed = Parser.Parse(Operations, args);
                foreach (var pair in parsed)
                {
                    switch (pair.Operation)
                    {
                        case ResurrectOperation rez:
                            string pathToChar = pair.Arguments[ImplicitArgument][0].RawValue;
                            ViewModel model = Application.Commands.Resurrect.ViewModel().AwaitResult() as ViewModel;
                            model.CharacterPath = pathToChar;
                            Application.Commands.Resurrect.Execute(model);
                            break;
                        default:
                            throw new InvalidOperationException("Unexpected operation");
                    }
                }
            }
            catch(Exception ex)
            {
                Usage(1, "Failed to operate: {0}", ex);
            }
        }
    }
}
