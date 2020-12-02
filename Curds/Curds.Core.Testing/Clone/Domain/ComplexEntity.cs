namespace Curds.Clone.Domain
{
    public class ComplexEntity
    {
        public const int DefaultInt = 654;

        public int TestInt { get; set; } = DefaultInt;
        public PrimitiveEntity TestPrimitiveEntity { get; set; }
    }
}
