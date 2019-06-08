namespace Curds.CLI.Formatting.Token.Mock
{
    using CLI.Abstraction;

    public class IToken : Abstraction.IToken
    {
        public int Writes = 0;
        public void Write(IConsole writer)
        {
            Writes++;
            writer.Write(nameof(IToken));
        }
    }
}
