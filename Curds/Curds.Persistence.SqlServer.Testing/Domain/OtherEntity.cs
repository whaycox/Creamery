using System;

namespace Curds.Persistence.Domain
{
    public class OtherEntity : SimpleEntity
    {
        public string Name { get; set; } = Guid.NewGuid().ToString();
        public bool BoolValue { get; set; } = true;
        public int IntValue { get; set; } = 157;
        public DateTime DateTimeValue { get; set; } = DateTime.UtcNow;
        public DateTimeOffset DateTimeOffsetValue { get; set; } = DateTimeOffset.Now;
        public decimal DecimalValue { get; set; } = 123.456m;
        public double DoubleValue { get; set; } = 3e8;
    }
}
