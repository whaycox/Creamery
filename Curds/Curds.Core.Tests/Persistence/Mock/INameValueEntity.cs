namespace Curds.Persistence.Mock
{
    public class INameValueEntity : Abstraction.INameValueEntity, Abstraction.IEntity
    {
        public static INameValueEntity Ten => new INameValueEntity() { ID = 10, Name = nameof(Ten), Value = nameof(Value) };
        public static INameValueEntity Twenty => new INameValueEntity() { ID = 20, Name = nameof(Twenty), Value = nameof(Value) };
        public static INameValueEntity Thirty => new INameValueEntity() { ID = 30, Name = nameof(Thirty), Value = nameof(Value) };

        public static INameValueEntity[] Samples => new INameValueEntity[]
        {
            Ten,
            Twenty,
            Thirty,
        };

        public int ID { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
    }
}
