namespace Gouda.Application.DeferredValues.Abstraction
{
    using Application.Abstraction;
    using DeferredValues.Domain;

    public interface ILabelDeferredValue : IDeferredValue<LabelDeferredKey, string>
    { }
}
