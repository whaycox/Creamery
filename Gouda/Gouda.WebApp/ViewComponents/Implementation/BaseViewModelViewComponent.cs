using Microsoft.AspNetCore.Mvc;

namespace Gouda.WebApp.ViewComponents.Implementation
{
    using Application.Abstraction;
    using WebApp.Domain;
    using ViewModels.Domain;
    using ViewModels.Abstraction;

    public abstract class BaseViewModelViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IWebAppViewModel viewModel) => View(viewModel.ViewName, viewModel);
    }
}
