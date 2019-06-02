namespace Curds.CLI.Formatting.Token.Abstraction
{
    using CLI.Abstraction;

    public interface IToken
    {
        void Write(IConsole console);
    }
}
