namespace Curds.Application.Domain
{
    using Abstraction;
    using Time.Abstraction;

    public abstract class CurdsApplication
    {
        protected ITime Time { get; }

        public abstract string Description { get; }

        public CurdsApplication(ICurdsOptions options)
        {
            Time = options.Time;
        }
    }
}
