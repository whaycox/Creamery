using System.Collections.Generic;
using System.Linq;

namespace Curds.CLI.Formatting.Text.Domain
{
    using CLI.Abstraction;
    using Token.Abstraction;
    using Token.Implementation;

    public class Formatted : IToken
    {
        protected List<IToken> Buffer { get; }

        public IEnumerable<IToken> Output => Buffer.ToList();

        public Formatted()
        {
            Buffer = new List<IToken>();
        }

        public Formatted(Formatted text)
        {
            text = text ?? new Formatted();
            Buffer = text.Output.ToList();
        }

        protected virtual void AddToBuffer(IToken token) => Buffer.Add(token);

        public void Add(IToken token) => AddToBuffer(token);
        public void AddLine(IToken token)
        {
            Add(token);
            Add(new NewLine());
        }

        public void Add(Formatted tokens)
        {
            foreach (IToken token in tokens.Output)
                AddToBuffer(token);
        }
        public void AddLine(Formatted tokens)
        {
            Add(tokens);
            Add(new NewLine());
        }

        public void Write(IConsole writer)
        {
            foreach (IToken token in Buffer)
                token.Write(writer);
        }
    }
}
