using System;

namespace Curds.CLI
{
    using Domain;
    using Enumerations;
    using Operations.Domain;
    using Operations.Implementation;

    public static class Verifications
    {
        public static VerificationChain VerifyNewLine(this VerificationChain verification) =>
            verification
                .Test(ConsoleOperation.TextWritten, Environment.NewLine)
                .Test(ConsoleOperation.NewLineReset, null);

        public static VerificationChain VerifyValueSyntax(this VerificationChain verification) =>
            verification
                .Test(ConsoleOperation.TextWritten, Value.SyntaxStart)
                .Test(ConsoleOperation.TextWritten, nameof(Value.Name))
                .Test(ConsoleOperation.TextWritten, Value.SyntaxEnd);

        public static VerificationChain VerifyValueUsage(this VerificationChain verification) =>
            verification
                .Test(ConsoleOperation.TextColorApplied, CLIEnvironment.Value)
                .Test(ConsoleOperation.TextWritten, nameof(Value.Name))
                .Test(ConsoleOperation.TextColorRemoved, null)
                .Test(ConsoleOperation.TextWritten, $": {nameof(Value.Description)}");

        public static VerificationChain VerifyThreeValuesUsage(this VerificationChain verification) =>
            verification
                .Test(ConsoleOperation.TextColorApplied, CLIEnvironment.Value)
                .Test(ConsoleOperation.TextWritten, "Values:")
                .VerifyNewLine()
                .Test(ConsoleOperation.TextColorRemoved, null)
                .Test(ConsoleOperation.IndentsIncreased, null)
                .VerifyValueUsage()
                .VerifyNewLine()
                .VerifyValueUsage()
                .VerifyNewLine()
                .VerifyValueUsage()
                .Test(ConsoleOperation.IndentsDecreased, null);

        public static VerificationChain VerifyArgumentAliases(this VerificationChain verification, string baseName) =>
            verification
                .Test(ConsoleOperation.TextColorApplied, CLIEnvironment.Argument)
                .Test(ConsoleOperation.TextWritten, AliasedOptionValue.AliasStart)
                .Test(ConsoleOperation.TextWritten, Argument.PrependIdentifier(baseName))
                .Test(ConsoleOperation.TextWritten, AliasedOptionValue.AliasSeparator)
                .Test(ConsoleOperation.TextWritten, Argument.PrependIdentifier($"{baseName}{nameof(Argument.Aliases)}"))
                .Test(ConsoleOperation.TextWritten, AliasedOptionValue.AliasEnd)
                .Test(ConsoleOperation.TextColorRemoved, null);

        public static VerificationChain VerifyArgumentSyntax(this VerificationChain verification, bool isRequired)
        {
            if (!isRequired)
                verification.Test(ConsoleOperation.TextWritten, Argument.OptionalStart);

            verification
                .VerifyArgumentAliases(nameof(Argument))
                .Test(ConsoleOperation.TextWritten, " ")
                .Test(ConsoleOperation.TextColorApplied, CLIEnvironment.Value)
                .VerifyValueSyntax()
                .Test(ConsoleOperation.TextWritten, " ")
                .VerifyValueSyntax()
                .Test(ConsoleOperation.TextWritten, " ")
                .VerifyValueSyntax()
                .Test(ConsoleOperation.TextColorRemoved, null);

            if (!isRequired)
                verification.Test(ConsoleOperation.TextWritten, Argument.OptionalEnd);

            return verification;
        }

        public static VerificationChain VerifyArgumentUsage(this VerificationChain verification, bool isRequired)
        {
            verification
                .VerifyArgumentSyntax(isRequired)
                .VerifyNewLine()
                .Test(ConsoleOperation.TextColorApplied, CLIEnvironment.Argument);

            if (isRequired)
                verification.Test(ConsoleOperation.TextWritten, nameof(Argument));
            else
                verification.Test(ConsoleOperation.TextWritten, $"{nameof(Argument)} (Optional)");

            return verification
                .Test(ConsoleOperation.TextColorRemoved, null)
                .Test(ConsoleOperation.TextWritten, $": {nameof(Argument.Description)}")
                .VerifyNewLine()
                .VerifyThreeValuesUsage();
        }

        public static VerificationChain VerifyBooleanArgumentSyntax(this VerificationChain verification, bool isRequired)
        {
            if (!isRequired)
                verification.Test(ConsoleOperation.TextWritten, Argument.OptionalStart);

            verification.VerifyArgumentAliases(nameof(BooleanArgument));

            if (!isRequired)
                verification.Test(ConsoleOperation.TextWritten, Argument.OptionalEnd);

            return verification;
        }

        public static VerificationChain VerifyBooleanArgumentUsage(this VerificationChain verification, bool isRequired)
        {
            verification
                .VerifyBooleanArgumentSyntax(isRequired)
                .VerifyNewLine()
                .Test(ConsoleOperation.TextColorApplied, CLIEnvironment.Argument);

            if (isRequired)
                verification.Test(ConsoleOperation.TextWritten, nameof(BooleanArgument));
            else
                verification.Test(ConsoleOperation.TextWritten, $"{nameof(BooleanArgument)} (Optional)");

            return verification
                .Test(ConsoleOperation.TextColorRemoved, null)
                .Test(ConsoleOperation.TextWritten, $": {nameof(Argument.Description)}");
        }

        public static VerificationChain VerifyBothArgumentsUsage(this VerificationChain verification, bool argIsRequired, bool boolArgIsRequired) =>
            verification
                .Test(ConsoleOperation.TextColorApplied, CLIEnvironment.Argument)
                .Test(ConsoleOperation.TextWritten, "Arguments:")
                .VerifyNewLine()
                .Test(ConsoleOperation.TextColorRemoved, null)
                .Test(ConsoleOperation.IndentsIncreased, null)
                .VerifyArgumentUsage(argIsRequired)
                .VerifyNewLine()
                .VerifyBooleanArgumentUsage(boolArgIsRequired)
                .Test(ConsoleOperation.IndentsDecreased, null);

        public static VerificationChain VerifyOperationAliases(this VerificationChain verification, string baseName) =>
            verification
                .Test(ConsoleOperation.TextColorApplied, CLIEnvironment.Operation)
                .Test(ConsoleOperation.TextWritten, AliasedOptionValue.AliasStart)
                .Test(ConsoleOperation.TextWritten, Operation.PrependIdentifier(baseName))
                .Test(ConsoleOperation.TextWritten, AliasedOptionValue.AliasSeparator)
                .Test(ConsoleOperation.TextWritten, Operation.PrependIdentifier($"{baseName}{nameof(Operation.Aliases)}"))
                .Test(ConsoleOperation.TextWritten, AliasedOptionValue.AliasEnd)
                .Test(ConsoleOperation.TextColorRemoved, null);

        public static VerificationChain VerifyOperationSyntax(this VerificationChain verification) =>
            verification.VerifyOperationAliases(nameof(Operation));

        public static VerificationChain VerifyOperationUsage(this VerificationChain verification, bool argIsRequired, bool boolArgIsRequired) =>
            verification
                .VerifyOperationSyntax()
                .VerifyNewLine()
                .Test(ConsoleOperation.TextColorApplied, CLIEnvironment.Operation)
                .Test(ConsoleOperation.TextWritten, nameof(Operation))
                .Test(ConsoleOperation.TextColorRemoved, null)
                .Test(ConsoleOperation.TextWritten, $": {nameof(Operation.Description)}")
                .VerifyNewLine()
                .VerifyBothArgumentsUsage(argIsRequired, boolArgIsRequired);

        public static VerificationChain VerifyArgumentlessOperationSyntax(this VerificationChain verification) =>
            verification
                .VerifyOperationAliases(nameof(ArgumentlessOperation))
                .Test(ConsoleOperation.TextWritten, " ")
                .Test(ConsoleOperation.TextColorApplied, CLIEnvironment.Value)
                .VerifyValueSyntax()
                .Test(ConsoleOperation.TextWritten, " ")
                .VerifyValueSyntax()
                .Test(ConsoleOperation.TextWritten, " ")
                .VerifyValueSyntax()
                .Test(ConsoleOperation.TextColorRemoved, null);

        public static VerificationChain VerifyArgumentlessOperationUsage(this VerificationChain verification) =>
            verification
                .VerifyArgumentlessOperationSyntax()
                .VerifyNewLine()
                .Test(ConsoleOperation.TextColorApplied, CLIEnvironment.Operation)
                .Test(ConsoleOperation.TextWritten, nameof(ArgumentlessOperation))
                .Test(ConsoleOperation.TextColorRemoved, null)
                .Test(ConsoleOperation.TextWritten, $": {nameof(ArgumentlessOperation.Description)}")
                .VerifyNewLine()
                .VerifyThreeValuesUsage();

        public static VerificationChain VerifyBooleanOperationSyntax(this VerificationChain verification) =>
            verification.VerifyOperationAliases(nameof(BooleanOperation));

        public static VerificationChain VerifyBooleanOperationUsage(this VerificationChain verification) =>
            verification
                .VerifyBooleanOperationSyntax()
                .VerifyNewLine()
                .Test(ConsoleOperation.TextColorApplied, CLIEnvironment.Operation)
                .Test(ConsoleOperation.TextWritten, nameof(BooleanOperation))
                .Test(ConsoleOperation.TextColorRemoved, null)
                .Test(ConsoleOperation.TextWritten, $": {nameof(BooleanOperation.Description)}");

        public static VerificationChain VerifyUsageText(this VerificationChain verification, string messageStartsWith, string expectedDescription)
        {
            verification.Test(ConsoleOperation.TextColorApplied, CLIEnvironment.Error);

            if (messageStartsWith != null)
                verification.TestTextStartsWith(messageStartsWith);

            verification
                .VerifyNewLine()
                .Test(ConsoleOperation.TextColorRemoved, null)
                .Test(ConsoleOperation.TextColorApplied, CLIEnvironment.Application)
                .Test(ConsoleOperation.TextWritten, Testing.ApplicationName)
                .Test(ConsoleOperation.TextColorRemoved, null)
                .Test(ConsoleOperation.TextWritten, " | ");

            if (expectedDescription != null)
                verification.Test(ConsoleOperation.TextWritten, expectedDescription);

            return verification;
        }

        public static VerificationChain VerifyHelpUsage(this VerificationChain verification) =>
            verification
                .Test(ConsoleOperation.TextColorApplied, CLIEnvironment.Operation)
                .Test(ConsoleOperation.TextWritten, AliasedOptionValue.AliasStart)
                .Test(ConsoleOperation.TextWritten, Operation.PrependIdentifier("Help"))
                .Test(ConsoleOperation.TextWritten, AliasedOptionValue.AliasSeparator)
                .Test(ConsoleOperation.TextWritten, Operation.PrependIdentifier($"?"))
                .Test(ConsoleOperation.TextWritten, AliasedOptionValue.AliasEnd)
                .Test(ConsoleOperation.TextColorRemoved, null)
                .VerifyNewLine()
                .Test(ConsoleOperation.TextColorApplied, CLIEnvironment.Operation)
                .Test(ConsoleOperation.TextWritten, "Help")
                .Test(ConsoleOperation.TextColorRemoved, null)
                .Test(ConsoleOperation.TextWritten, $": Print Usage");

        public static VerificationChain VerifyUsageOperations(this VerificationChain verification, bool help, bool operation, bool argIsRequired, bool boolArgIsRequired, bool argumentlessOperation, bool booleanOperation)
        {
            verification
                .Test(ConsoleOperation.TextColorApplied, CLIEnvironment.Operation)
                .Test(ConsoleOperation.TextWritten, "Operations:")
                .VerifyNewLine()
                .Test(ConsoleOperation.TextColorRemoved, null)
                .Test(ConsoleOperation.IndentsIncreased, null);

            if(help)
            {
                verification.VerifyHelpUsage();
                if (operation || argumentlessOperation || booleanOperation)
                    verification.VerifyNewLine();
            }

            if (operation)
            {
                verification.VerifyOperationUsage(argIsRequired, boolArgIsRequired);
                if (argumentlessOperation || booleanOperation)
                    verification.VerifyNewLine();
            }

            if (argumentlessOperation)
            {
                verification.VerifyArgumentlessOperationUsage();
                if (booleanOperation)
                    verification.VerifyNewLine();
            }

            if (booleanOperation)
                verification.VerifyBooleanOperationUsage();

            return verification.Test(ConsoleOperation.IndentsDecreased, null);
        }
    }
}
