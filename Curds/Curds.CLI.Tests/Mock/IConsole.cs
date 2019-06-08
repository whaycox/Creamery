using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Curds.CLI.Mock
{
    using Domain;
    using Enumerations;

    public class IConsole : Abstraction.IConsole
    {
        public List<OperationEvent> Operations = new List<OperationEvent>();
        public VerificationChain VerifyOperations(int expectedOperations)
        {
            Assert.AreEqual(expectedOperations, Operations.Count);
            return new VerificationChain(this);
        }

        public string Indentation { get; set; } = CLIEnvironment.DefaultIndentation;
        public ConsoleColor CurrentColor { get; private set; } = CLIEnvironment.DefaultTextColor;

        public void Exit(int exitCode) => Operations.Add(new OperationEvent(ConsoleOperation.EnvironmentExited, exitCode));

        public void ApplyColor(ConsoleColor color) => Operations.Add(new OperationEvent(ConsoleOperation.TextColorApplied, color));
        public void RemoveColor() => Operations.Add(new OperationEvent(ConsoleOperation.TextColorRemoved, null));

        public void IncreaseIndent() => Operations.Add(new OperationEvent(ConsoleOperation.IndentsIncreased, null));
        public void DecreaseIndent() => Operations.Add(new OperationEvent(ConsoleOperation.IndentsDecreased, null));

        public void Write(string message) => Operations.Add(new OperationEvent(ConsoleOperation.TextWritten, message));
        public void ResetNewLine() => Operations.Add(new OperationEvent(ConsoleOperation.NewLineReset, null));
    }
}
