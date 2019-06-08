namespace Curds.Persistence.Domain
{
    public abstract class NamedEntity : Entity
    {
        public string Name { get; set; }
    }
}
