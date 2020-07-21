using System;
using System.Text;

namespace Curds.Text.Implementation
{
    using Abstraction;

    internal class IndentStringBuilder : IIndentStringBuilder
    {
        public const string DefaultIndent = "\t";
        private static readonly string[] CleanSplitStrings = new string[] { Environment.NewLine };

        private StringBuilder Builder { get; } = new StringBuilder();
        private bool IndentsNeeded { get; set; } = true;

        public string Indent { get; set; } = DefaultIndent;

        private int _indents;
        public int Indents
        {
            get => _indents;
            set
            {
                if (value < 0)
                    _indents = 0;
                else
                    _indents = value;
            }
        }

        public void Append(string text) => CleanAndAppendText(text);
        private void CleanAndAppendText(string text)
        {
            string[] sections = text.Split(CleanSplitStrings, StringSplitOptions.None);

            for (int i = 0; i < sections.Length; i++)
            {
                if (i > 0)
                    SetNewLine();
                AppendToBuilder(sections[i]);
            }
        }
        private void AppendToBuilder(string cleanedText)
        {
            if (IndentsNeeded)
                AppendIndents();
            Builder.Append(cleanedText);
        }
        private void AppendIndents()
        {
            for (int i = 0; i < Indents; i++)
                Builder.Append(Indent);
            IndentsNeeded = false;
        }

        public void AppendLine(string text)
        {
            Append(text);
            SetNewLine();
        }

        public void SetNewLine()
        {
            if (!IndentsNeeded)
            {
                Builder.Append(Environment.NewLine);
                IndentsNeeded = true;
            }
        }

        public string Flush()
        {
            string built = Builder.ToString();
            Builder.Clear();
            return built;
        }

        public IDisposable CreateIndentScope() => new IndentScope(this);

        private class IndentScope : IDisposable
        {
            private IndentStringBuilder Parent { get; }

            public IndentScope(IndentStringBuilder parent)
            {
                Parent = parent;
                Parent.Indents++;
            }

            public void Dispose()
            {
                Parent.Indents--;
            }
        }
    }
}
