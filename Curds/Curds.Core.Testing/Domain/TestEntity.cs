using System;

namespace Curds.Domain
{
    using Persistence.Domain;

    public class TestEntity : BaseSimpleEntity
    {
        public int TestField = default;

        public bool BoolValue { get; set; } = true;
        public bool? NullableBoolValue { get; set; }
        public byte ByteValue { get; set; } = 255;
        public byte? NullableByteValue { get; set; }
        public short ShortValue { get; set; } = 20000;
        public short? NullableShortValue { get; set; }
        public int IntValue { get; set; } = 157;
        public int? NullableIntValue { get; set; }
        public long LongValue { get; set; } = long.MaxValue;
        public long? NullableLongValue { get; set; }
        public DateTime DateTimeValue { get; set; } = DateTime.UtcNow;
        public DateTime? NullableDateTimeValue { get; set; }
        public DateTimeOffset DateTimeOffsetValue { get; set; } = DateTimeOffset.Now;
        public DateTimeOffset? NullableDateTimeOffsetValue { get; set; }
        public decimal DecimalValue { get; set; } = 123.456m;
        public decimal? NullableDecimalValue { get; set; }
        public double DoubleValue { get; set; } = 3e8;
        public double? NullableDoubleValue { get; set; }

        public TestEntity()
        { }
        public TestEntity(int intValue)
        {
            IntValue = intValue;
        }
    }
}
