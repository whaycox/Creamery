namespace Curds.Persistence.Domain
{
    public class TestEnumEntity : NamedEntity
    {
        public TestByteEnum ByteEnum { get; set; }
        public TestShortEnum ShortEnum { get; set; }
        public TestIntEnum IntEnum { get; set; }
        public TestLongEnum LongEnum { get; set; }

        public TestByteEnum? NullableByteEnum { get; set; }
        public TestShortEnum? NullableShortEnum { get; set; }
        public TestIntEnum? NullableIntEnum { get; set; }
        public TestLongEnum? NullableLongEnum { get; set; }

        public TestEnumEntity()
        {
            Name = nameof(TestEnumEntity);
        }
    }
}
