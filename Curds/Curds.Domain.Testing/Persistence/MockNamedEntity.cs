namespace Curds.Domain.Persistence
{
    public class MockNamedEntity : NamedEntity
    {
        public MockNamedEntity()
        {
            Name = nameof(MockNamedEntity);
        }

        public override Entity Clone()
        {
            MockNamedEntity clone = new MockNamedEntity();
            clone.ID = ID;
            clone.Name = Name;

            return clone;
        }
    }
}
