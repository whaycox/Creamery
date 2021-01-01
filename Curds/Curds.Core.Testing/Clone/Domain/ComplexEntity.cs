using System;

namespace Curds.Clone.Domain
{
    public class ComplexEntity
    {
        public const int DefaultInt = 654;

        public int TestInt { get; set; } = DefaultInt;
        public PrimitiveEntity TestPrimitiveEntity { get; set; }

        public override int GetHashCode() => HashCode.Combine(
            TestInt,
            TestPrimitiveEntity);

        public override bool Equals(object obj)
        {
            ComplexEntity testObject = obj as ComplexEntity;
            return testObject != null &&
                EqualsInternal(testObject);
        }
        private bool EqualsInternal(ComplexEntity testObject)
        {
            if (TestInt != testObject.TestInt)
                return false;
            if (!TestPrimitiveEntity.Equals(testObject.TestPrimitiveEntity))
                return false;

            return true;
        }
    }
}
