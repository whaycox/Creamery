using Microsoft.AspNetCore.Mvc;

namespace Gouda.WebApp.ViewComponents.Implementation
{
    using Application.Abstraction;

    public abstract class BaseViewModelViewComponent<T> : ViewComponent
        where T : IViewModel
    {
        public IViewComponentResult Invoke(T viewModel) => View(viewModel.ViewName, viewModel);
    }
}
