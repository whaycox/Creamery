namespace Curds.CLI.Domain
{
    using Enumerations;

    public class ExpectedEvent : OperationEvent
    {
        public ExpectedEvent(ConsoleOperation operation, object value)
            : base(operation, value)
        { }
    }
}
