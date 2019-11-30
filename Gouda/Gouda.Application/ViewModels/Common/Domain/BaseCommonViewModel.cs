namespace Gouda.Application.ViewModels.Common.Domain
{
    using Abstraction;

    public abstract class BaseCommonViewModel : ICommonViewModel
    {
        public string ViewConcept => nameof(Common);

        public abstract string ViewName { get; }
    }
}
