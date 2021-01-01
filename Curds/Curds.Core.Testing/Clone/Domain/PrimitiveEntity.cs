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

        public override int GetHashCode() => HashCode.Combine(
            TestString,
            TestString,
            TestInt,
            TestLong,
            TestDateTime,
            TestDateTimeOffset,
            TestString);

        public override bool Equals(object obj)
        {
            PrimitiveEntity testObject = obj as PrimitiveEntity;
            return testObject != null &&
                EqualsInternal(testObject);
        }
        private bool EqualsInternal(PrimitiveEntity testObject)
        {
            if (TestByte != testObject.TestByte)
                return false;
            if (TestShort != testObject.TestShort)
                return false;
            if (TestInt != testObject.TestInt)
                return false;
            if (TestLong != testObject.TestLong)
                return false;
            if (TestDateTime != testObject.TestDateTime)
                return false;
            if (TestDateTimeOffset != testObject.TestDateTimeOffset)
                return false;
            if (TestString != testObject.TestString)
                return false;

            return true;
        }
    }
}
