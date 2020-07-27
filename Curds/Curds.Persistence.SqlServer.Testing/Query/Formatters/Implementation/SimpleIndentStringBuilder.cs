using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.Persistence.Query.Formatters.Implementation
{
    using Text.Abstraction;

    public class SimpleIndentStringBuilder : IIndentStringBuilder
    {
        private int CurrentVerifyIndex = 0;

        public List<string> Operations { get; } = new List<string>();
        public void VerifyOperationCount() => Assert.AreEqual(CurrentVerifyIndex, Operations.Count);

        private SimpleIndentStringBuilder VerifyOperation(string expectedText)
        {
            Assert.AreEqual(expectedText, Operations[CurrentVerifyIndex++]);
            return this;
        }

        public int Indents { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private string FormatAppend(string text) => $"{nameof(Append)}({text})";
        public void Append(string text) => Operations.Add(FormatAppend(text));
        public SimpleIndentStringBuilder VerifyAppend(string text) => VerifyOperation(FormatAppend(text));

        private string FormatAppendLine(string text) => $"{nameof(AppendLine)}({text})";
        public void AppendLine(string text) => Operations.Add(FormatAppendLine(text));
        public SimpleIndentStringBuilder VerifyAppendLine(string text) => VerifyOperation(FormatAppendLine(text));

        public IDisposable CreateIndentScope()
        {
            Operations.Add(nameof(CreateIndentScope));
            return new SimpleScope(Operations);
        }
        public SimpleIndentStringBuilder VerifyCreateScope() => VerifyOperation(nameof(CreateIndentScope));
        public SimpleIndentStringBuilder VerifyDisposeScope() => VerifyOperation(nameof(SimpleScope.Dispose));

        public string Flush()
        {
            Operations.Add(nameof(Flush));
            return nameof(Flush);
        }
        public SimpleIndentStringBuilder VerifyFlush() => VerifyOperation(nameof(Flush));

        public void SetNewLine() => Operations.Add(nameof(SetNewLine));
        public SimpleIndentStringBuilder VerifySetNewLine() => VerifyOperation(nameof(SetNewLine));

        private class SimpleScope : IDisposable
        {
            private List<string> Operations { get; }

            public SimpleScope(List<string> operations)
            {
                Operations = operations;
            }

            public void Dispose() => Operations.Add(nameof(Dispose));
        }
    }
}
