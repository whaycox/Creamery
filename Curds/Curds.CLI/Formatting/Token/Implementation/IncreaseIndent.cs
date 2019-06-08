namespace Curds.CLI.Formatting.Token.Implementation
{
    using Abstraction;
    using Curds.CLI.Abstraction;

    public class IncreaseIndent : IToken
    {
        public void Write(IConsole writer) => writer.IncreaseIndent();
    }
}
