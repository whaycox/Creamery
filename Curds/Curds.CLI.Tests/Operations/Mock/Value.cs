namespace Curds.CLI.Operations.Mock
{
    using CLI.Domain;

    public class Value : Implementation.Value
    {
        public override string Name => nameof(Name);
        public override string Description => nameof(Description);
    }
}
