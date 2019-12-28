namespace Gouda.Application.ViewModels.Common.Abstraction
{
    using DeferredValues.Domain;

    public interface IFieldViewModel : ICommonViewModel
    {
        LabelDeferredKey Label { get; set; }
    }
}
