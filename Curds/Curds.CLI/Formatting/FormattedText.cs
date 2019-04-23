using System.Collections.Generic;
using System.Linq;

namespace Curds.CLI.Formatting
{
    using Tokens;

    public class FormattedText : BaseTextToken
    {
        protected List<BaseTextToken> Buffer { get; }

        public IEnumerable<BaseTextToken> Output => Buffer.ToList();

        public FormattedText()
        {
            Buffer = new List<BaseTextToken>();
        }

        public FormattedText(FormattedText text)
        {
            text = text ?? New;
            Buffer = text.Output.ToList();
        }

        protected virtual void AddToBuffer(BaseTextToken token) => Buffer.Add(token);

        public void Add(BaseTextToken token) => AddToBuffer(token);
        public void AddLine(BaseTextToken token)
        {
            Add(token);
            Add(NewLineToken.New);
        }

        public void Add(FormattedText tokens)
        {
            foreach (BaseTextToken token in tokens.Output)
                AddToBuffer(token);
        }
        public void AddLine(FormattedText tokens)
        {
            Add(tokens);
            Add(NewLineToken.New);
        }

        public override void Write(IConsoleWriter writer)
        {
            foreach (BaseTextToken token in Buffer)
                token.Write(writer);
        }

        public static FormattedText New => new FormattedText();
    }
}
