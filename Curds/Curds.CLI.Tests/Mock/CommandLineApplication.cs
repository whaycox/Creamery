using System.Collections.Generic;
using System.Threading.Tasks;

namespace Curds.CLI.Mock
{
    using Operations.Implementation;

    public class CommandLineApplication : Implementation.CommandLineApplication<Application.Mock.CurdsApplication>
    {
        public CommandLineApplication(Application.Mock.CurdsApplication application, IConsole mockConsole)
            : base(application, mockConsole)
        { }

        public List<OperationParser.ParsedPair> ExecutedPairs = new List<OperationParser.ParsedPair>();
        protected override Task ExecuteOperation(OperationParser.ParsedPair parsedPair) => Task.Run(() => ExecutedPairs.Add(parsedPair));

        public bool IncludeOperation = true;
        public bool ArgIsRequired = true;
        public bool BoolArgIsRequired = true;
        public bool IncludeArgumentlessOperation = true;
        public bool IncludeBooleanOperation = true;
        protected override List<Operation> BuildOperations(List<Operation> operations)
        {
            operations = base.BuildOperations(operations);

            if (IncludeOperation)
            {
                Operations.Mock.Operation operation = new Operations.Mock.Operation();
                operation.ArgumentIsRequired = ArgIsRequired;
                operation.BooleanArgumentIsRequired = BoolArgIsRequired;
                operations.Add(operation);
            }
            if (IncludeArgumentlessOperation)
                operations.Add(new Operations.Mock.ArgumentlessOperation());
            if (IncludeBooleanOperation)
                operations.Add(new Operations.Mock.BooleanOperation());

            return operations;
        }
    }
}
