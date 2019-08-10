namespace Curds.Persistence.Mock
{
    public class IEntity : Abstraction.IEntity
    {
        public static IEntity Ten => new IEntity() { ID = 10, Other = 5 };
        public static IEntity Twenty => new IEntity() { ID = 20, Other = 7 };
        public static IEntity Thirty => new IEntity() { ID = 30, Other = 3 };

        public static IEntity[] Samples => new IEntity[]
        {
            Ten,
            Twenty,
            Thirty,
        };

        public int ID { get; set; }
        public int Other { get; set; }
    }
}
