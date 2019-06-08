namespace Curds.Application.Abstraction
{
    using Time.Abstraction;

    public interface ICurdsOptions
    {
        ITime Time { get; }
    }
}
