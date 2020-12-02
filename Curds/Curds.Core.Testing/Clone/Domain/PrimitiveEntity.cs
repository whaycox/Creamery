using System;

namespace Curds.Clone.Domain
{
    public class PrimitiveEntity
    {
        public const byte DefaultByte = 4;
        public const short DefaultShort = 20001;
        public const int DefaultInt = 123456789;
        public const long DefaultLong = 1234567899876543210;

        public byte TestByte { get; set; } = DefaultByte;
        public short TestShort { get; set; } = DefaultShort;
        public int TestInt { get; set; } = DefaultInt;
        public long TestLong { get; set; } = DefaultLong;
        public DateTime TestDateTime { get; set; } = DateTime.Now;
        public DateTimeOffset TestDateTimeOffset { get; set; } = DateTimeOffset.UtcNow;
        public string TestString { get; set; } = nameof(TestString);
    }
}
