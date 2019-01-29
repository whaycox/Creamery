namespace Curds.Domain.Persistence
{
    public class MockEntity : Entity
    {
        public MockEntity()
        {
            ID = 12;
        }

        public override Entity Clone()
        {
            MockEntity clone = new MockEntity();
            clone.ID = ID;

            return clone;
        }
    }
}
