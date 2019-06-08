using System.Collections.Generic;

namespace Curds.CLI.Template
{
    using Operations.Implementation;

    public abstract class Console : Test
    {
        protected const string One = nameof(One);
        protected const string Two = nameof(Two);
        protected const string Three = nameof(Three);

        protected Mock.IConsole MockConsole = new Mock.IConsole();
        protected Mock.IArgumentCrawler MockArgumentCrawler = new Mock.IArgumentCrawler();

        protected string[] ValueScript(int values)
        {
            List<string> script = new List<string>();
            for (int i = 0; i < values; i++)
                script.Add(nameof(ValueScript));
            return script.ToArray();
        }

        protected List<Operation> BuildTestOperations(bool argIsRequired, bool boolArgIsRequired)
        {
            List<Operation> toReturn = new List<Operation>();
            BuildMockOperation(toReturn, argIsRequired, boolArgIsRequired);
            BuildMockArgumentlessOperation(toReturn);
            BuildMockBooleanOperation(toReturn);
            return toReturn;
        }
        protected void BuildMockOperation(List<Operation> operations, bool argIsRequired, bool boolArgIsRequired)
        {
            CLI.Operations.Mock.Operation mockOperation = new CLI.Operations.Mock.Operation();
            mockOperation.ArgumentIsRequired = argIsRequired;
            mockOperation.BooleanArgumentIsRequired = boolArgIsRequired;

            operations.Add(mockOperation);
        }
        protected void BuildMockArgumentlessOperation(List<Operation> operations)
        {
            CLI.Operations.Mock.ArgumentlessOperation mockArgumentlessOperation = new CLI.Operations.Mock.ArgumentlessOperation();
            operations.Add(mockArgumentlessOperation);
        }
        protected void BuildMockBooleanOperation(List<Operation> operations)
        {
            CLI.Operations.Mock.BooleanOperation mockBooleanOperation = new CLI.Operations.Mock.BooleanOperation();
            operations.Add(mockBooleanOperation);
        }
    }

    public abstract class Console<T> : Console
    {
        protected abstract T TestObject { get; }
    }
}
