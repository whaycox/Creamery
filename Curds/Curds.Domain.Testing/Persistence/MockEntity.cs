namespace Curds.Domain.Persistence
{
    public class MockEntity : Entity
    {
        public override Entity Clone()
        {
            MockEntity clone = new MockEntity();
            clone.ID = ID;

            return clone;
        }
    }
}
