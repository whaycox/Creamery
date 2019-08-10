namespace Curds.Persistence.Mock
{
    public class INamedEntity : Abstraction.INamedEntity
    {
        public static INamedEntity Ten => new INamedEntity() { Name = nameof(Ten) };
        public static INamedEntity Twenty => new INamedEntity() { Name = nameof(Twenty) };
        public static INamedEntity Thirty => new INamedEntity() { Name = nameof(Thirty) };

        public static INamedEntity[] Samples => new INamedEntity[]
        {
            Ten,
            Twenty,
            Thirty,
        };

        public string Name { get; set; }
    }
}
