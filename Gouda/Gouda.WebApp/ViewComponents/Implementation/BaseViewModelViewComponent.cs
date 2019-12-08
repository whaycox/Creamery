using Microsoft.AspNetCore.Mvc;

namespace Gouda.WebApp.ViewComponents.Implementation
{
    using Application.Abstraction;
    using WebApp.Domain;
    using ViewModels.Domain;

    public abstract class BaseViewModelViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(WebAppViewModel viewModel) => View(viewModel.ViewName, viewModel);
    }
}
