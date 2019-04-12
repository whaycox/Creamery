using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain;
using Curds.Domain.CLI;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.CLI.Formatting
{
    public abstract class FormattingTemplate<T> : TestTemplate<T>
    {
        protected MockConsoleWriter Writer = new MockConsoleWriter();

        protected string NewLine(bool newValue) => MockConsoleWriter.StartOfNewLineWrite(newValue);
        protected string TextColor(ConsoleColor textColor) => MockConsoleWriter.TextColorChangeWrite(textColor);
        protected string Indents(int indents) => MockConsoleWriter.IndentsWrite(indents);
        protected string EnvironmentExit(int exitCode) => MockConsoleWriter.EnvironmentExit(exitCode);
    }
}
