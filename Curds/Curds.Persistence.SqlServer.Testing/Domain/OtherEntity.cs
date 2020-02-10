using System;

namespace Curds.Persistence.Domain
{
    public class OtherEntity : SimpleEntity
    {
        public string Name { get; set; } = Guid.NewGuid().ToString();
        public bool BoolValue { get; set; } = true;
        public bool? NullableBoolValue { get; set; }
        public int IntValue { get; set; } = 157;
        public int? NullableIntValue { get; set; }
        public DateTime DateTimeValue { get; set; } = DateTime.UtcNow;
        public DateTime? NullableDateTimeValue { get; set; }
        public DateTimeOffset DateTimeOffsetValue { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? NullableDateTimeOffsetValue { get; set; }
        public decimal DecimalValue { get; set; } = 123.456m;
        public decimal? NullableDecimalValue { get; set; }
        public double DoubleValue { get; set; } = 3e8;
        public double? NullableDoubleValue { get; set; }
    }
}
