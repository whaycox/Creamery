using System.Collections.Generic;
using System.Linq;

namespace Curds.CLI.Formatting
{
    public class FormattedText : BaseTextToken
    {
        private List<BaseTextToken> Buffer { get; }

        public IEnumerable<BaseTextToken> Output => Buffer.ToList();

        public FormattedText()
            : this(new List<BaseTextToken>())
        { }

        public FormattedText(IEnumerable<BaseTextToken> tokens)
        {
            Buffer = tokens.ToList();
        }

        public void Add(BaseTextToken token) => Buffer.Add(token);
        public void AddLine(BaseTextToken token)
        {
            Add(token);
            Add(NewLineToken.New);
        }

        public void Add(FormattedText tokens) => Buffer.AddRange(tokens.Output);
        public void AddLine(FormattedText tokens)
        {
            Add(tokens);
            Add(NewLineToken.New);
        }

        public override void Write(ConsoleWriter writer)
        {
            foreach (BaseTextToken token in Buffer)
                token.Write(writer);
        }
    }
}
