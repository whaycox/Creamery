using System;

namespace Curds.Persistence.Domain
{
    public class GenericToken : BaseEntity
    {
        public string ID { get; set; } = Guid.NewGuid().ToString();
        public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.Now;

        public override object[] Keys => new object[] { ID };
    }
}
