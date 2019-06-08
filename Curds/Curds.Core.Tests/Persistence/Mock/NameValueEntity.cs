using System;

namespace Curds.Persistence.Mock
{
    public class NameValueEntity : Domain.NameValueEntity
    {
        public static Domain.NameValueEntity Ten => new NameValueEntity(10);
        public static Domain.NameValueEntity Twenty => new NameValueEntity(20);
        public static Domain.NameValueEntity Thirty => new NameValueEntity(30);

        public static Domain.NameValueEntity[] Samples => new Domain.NameValueEntity[]
        {
            Ten,
            Twenty,
            Thirty,
        };

        public NameValueEntity()
        { }

        public NameValueEntity(int id)
        {
            ID = id;
            Name = nameof(NameValueEntity);
            Value = nameof(Value);
        }
    }
}
