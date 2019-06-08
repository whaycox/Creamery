namespace Curds.CLI.Formatting.Token.Implementation
{
    using Abstraction;
    using Curds.CLI.Abstraction;

    public class ResetTextColor : IToken
    {
        public void Write(IConsole console) => console.RemoveColor();
    }
}
