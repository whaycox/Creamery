namespace Feta.OpenType.Mock
{
    using Domain;

    public class PrimaryTable : Domain.PrimaryTable
    {
        public string CurrentTag = nameof(Mock);
        public override string Tag => CurrentTag;

        public BaseTable Table { get; set; }
    }
}
