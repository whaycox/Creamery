namespace Curds.Persistence.Mock
{
    public class NamedEntity : Domain.NamedEntity
    {
        public NamedEntity()
        { }

        public NamedEntity(int id)
        {
            ID = id;
            Name = nameof(NamedEntity);
        }
    }
}
