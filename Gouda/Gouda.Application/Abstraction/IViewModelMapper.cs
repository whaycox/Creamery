namespace Gouda.Application.Abstraction
{
    using Gouda.Domain;

    public interface IViewModelMapper<T, U>
        where T : BaseEntity
        where U : IViewModel
    {
        U Map(T entity);
    }
}
