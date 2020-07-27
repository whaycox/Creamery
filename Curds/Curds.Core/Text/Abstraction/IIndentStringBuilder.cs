using System;

namespace Curds.Text.Abstraction
{
    public interface IIndentStringBuilder
    {
        int Indents { get; set; }

        void Append(string text);
        void AppendLine(string text);
        void SetNewLine();

        IDisposable CreateIndentScope();

        string Flush();
    }
}
