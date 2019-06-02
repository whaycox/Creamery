namespace Curds.CLI.Formatting.Text.Mock
{
    public class Formatted : Domain.Formatted
    {
        public Formatted(int mockTokens)
        {
            for (int i = 0; i < mockTokens; i++)
                Add(new Token.Mock.IToken());
        }
    }
}
