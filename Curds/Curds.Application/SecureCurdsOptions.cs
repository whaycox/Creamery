namespace Curds.Application
{
    using Security;

    public abstract class SecureCurdsOptions : CurdsOptions
    {
        public abstract ISecurity Security { get; }
    }
}
