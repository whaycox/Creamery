namespace Curds.Persistence.Mock
{
    public class Entity : Domain.Entity
    {
        public static Domain.Entity Ten => new Entity(10);
        public static Domain.Entity Twenty => new Entity(20);
        public static Domain.Entity Thirty => new Entity(30);

        public static Domain.Entity[] Samples => new Domain.Entity[]
        {
            Ten,
            Twenty,
            Thirty,
        };

        public int Other { get; set; }

        public Entity()
        { }

        public Entity(int id)
        {
            ID = id;
        }
    }
}
