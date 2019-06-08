namespace Curds.Application.Domain
{
    using Abstraction;
    using Time.Abstraction;

    public class CurdsOptions : ICurdsOptions
    {
        public ITime Time { get; set; }
    }
}
