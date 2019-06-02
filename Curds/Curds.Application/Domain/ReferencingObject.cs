namespace Curds.Application.Domain
{
    public abstract class ReferencingObject<T> where T : CurdsApplication
    {
        protected T Application { get; }

        public ReferencingObject(T application)
        {
            Application = application;
        }
    }
}
