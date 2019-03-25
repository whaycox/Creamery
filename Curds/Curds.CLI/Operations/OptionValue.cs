namespace Curds.CLI.Operations
{
    using Formatting;

    public abstract class OptionValue
    {
        public const string Indent = "\t";

        public abstract string Syntax { get; }
        public abstract string Name { get; }
        public abstract string Description { get; }

        public abstract FormattedText Usage { get; }
    }
}
