namespace Curds.Domain.Persistence
{
    public class MockEntity : Entity
    {
        public const int SampleID = 1;

        public static MockEntity Sample => new MockEntity() { ID = SampleID };

        public override bool Equals(object obj)
        {
            MockEntity toTest = obj as MockEntity;
            if (toTest == null)
                return false;
            if (toTest.ID != ID)
                return false;
            return true;
        }
    }
}
