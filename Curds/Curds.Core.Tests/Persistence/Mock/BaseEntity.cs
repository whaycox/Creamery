namespace Curds.Persistence.Mock
{
    public class BaseEntity : Domain.BaseEntity
    {
        public static BaseEntity One => new BaseEntity() { MyValue = 1 };
        public static BaseEntity Two => new BaseEntity() { MyValue = 2 };
        public static BaseEntity Three => new BaseEntity() { MyValue = 3 };

        public static BaseEntity[] Samples => new BaseEntity[]
        {
            One,
            Two,
            Three,
        };

        public int MyValue { get; set; }

        public BaseEntity()
        { }
    }
}
