namespace Curds.Persistence.Domain
{
    public class TestSimpleEntity : BaseSimpleEntity
    {
        public int ReadOnlyProperty => ID;
    }
}
