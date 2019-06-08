namespace Curds.Persistence.Mock
{
    public class NamedEntity : Domain.NamedEntity
    {
        public static Domain.NamedEntity Ten => new NamedEntity(10);
        public static Domain.NamedEntity Twenty => new NamedEntity(20);
        public static Domain.NamedEntity Thirty => new NamedEntity(30);

        public static Domain.NamedEntity[] Samples => new Domain.NamedEntity[]
        {
            Ten,
            Twenty,
            Thirty,
        };

        public NamedEntity()
        { }

        public NamedEntity(int id)
        {
            ID = id;
            Name = nameof(NamedEntity);
        }
    }
}
