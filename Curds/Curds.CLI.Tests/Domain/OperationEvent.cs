namespace Curds.CLI.Domain
{
    using Enumerations;

    public class OperationEvent
    {
        public ConsoleOperation Operation { get; }
        public object Value { get; }

        public OperationEvent(ConsoleOperation operation, object value)
        {
            Operation = operation;
            Value = value;
        }

        public override string ToString() => $"({Operation}){(Value == null ? string.Empty : $": {Value}")}";
    }
}
