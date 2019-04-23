namespace Curds.Application
{
    using Security;

    public abstract class SecureCurdsApplication : CurdsApplication
    {
        public ISecurity Security { get; }

        public SecureCurdsApplication(SecureCurdsOptions options)
            : base(options)
        {
            Security = options.Security;
        }
    }
}
