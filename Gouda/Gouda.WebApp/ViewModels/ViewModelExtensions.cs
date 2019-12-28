using Microsoft.AspNetCore.Mvc;

namespace Gouda.WebApp.ViewModels
{
    using Abstraction;
    using Application.Abstraction;
    using Domain;

    public static class ViewModelExtensions
    {
        public static IActionResult ReturnViewComponent<TViewModel>(this Controller controller, IWebAppViewModelWrapper viewModelWrapper, TViewModel viewModel)
            where TViewModel : IViewModel
        {
            IWebAppViewModel wrappedViewModel = viewModelWrapper.Wrap(viewModel);
            return new ViewComponentResult
            {
                ViewComponentName = wrappedViewModel.ViewConcept,
                Arguments = wrappedViewModel,
                ViewData = controller.ViewData,
                TempData = controller.TempData,
            };
        }
    }
}
