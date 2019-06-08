namespace Curds.CLI.Formatting.Token.Implementation
{
    using Abstraction;
    using CLI.Abstraction;

    public class PlainText : IToken
    {
        private string Value { get; }

        public PlainText(string value)
        {
            Value = value;
        }

        public virtual void Write(IConsole writer)
        {
            if (!string.IsNullOrEmpty(Value))
                writer.Write(Value);
        }
    }
}
