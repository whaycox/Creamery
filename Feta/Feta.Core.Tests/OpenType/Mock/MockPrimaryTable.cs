namespace Feta.OpenType.Mock
{
    using Domain;

    public class MockPrimaryTable : PrimaryTable
    {
        public BaseTable Table { get; set; }

        public MockPrimaryTable()
            : this(nameof(Mock))
        { }

        public MockPrimaryTable(string tag)
            : base(tag)
        { }
    }
}
