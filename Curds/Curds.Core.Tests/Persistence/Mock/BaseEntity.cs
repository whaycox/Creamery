namespace Curds.Persistence.Mock
{
    public class BaseEntity : Domain.BaseEntity
    {
        public int MyValue { get; }

        public BaseEntity(int value)
        {
            MyValue = value;
        }
    }
}
