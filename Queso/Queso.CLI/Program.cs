using System;
using Queso.Application.Character;
using Queso.Infrastructure.Character;
using Queso.Application;
using Queso.Infrastructure;
using Curds;
using Queso.Application.Message.Command.Character;

namespace Queso.CLI
{
    class Program
    {
        public static QuesoApplication Application = new QuesoApplication(new DefaultOptions());

        private static void Usage(int exitCode, string message, params object[] args) => Usage(exitCode, string.Format(message, args));
        private static void Usage(int exitCode, string message)
        {
            Console.WriteLine(message);
            Console.Write(Operations.QuesoOperations.OperationDescriptions);
            Environment.Exit(exitCode);
        }

        static void Main(string[] args)
        {
            try
            {
                var operations = Operations.QuesoOperations.Parse(args);
                foreach(var pair in operations)
                {
                    switch(pair.Key)
                    {
                        case Operations.ResurrectOperation rez:
                            string pathToChar = pair.Value[Operation.ArgumentlessOperation.Argumentless][0].RawValue;
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
