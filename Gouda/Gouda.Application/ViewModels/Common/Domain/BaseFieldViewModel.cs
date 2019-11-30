namespace Gouda.Application.ViewModels.Common.Domain
{
    using Abstraction;
    using Gouda.Application.DeferredValues.Domain;

    public abstract class BaseFieldViewModel : BaseCommonViewModel, IFieldViewModel
    {
        public LabelDeferredKey Label { get; set; }
    }
}
