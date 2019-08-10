namespace Curds.Persistence.Abstraction
{
    public interface INameValueEntity : INamedEntity
    {
        string Value { get; set; }
    }
}
