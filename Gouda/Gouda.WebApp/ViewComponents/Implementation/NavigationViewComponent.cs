namespace Gouda.WebApp.ViewComponents.Implementation
{
    using Application.ViewModels.Navigation.Abstraction;

    public class NavigationViewComponent : BaseViewModelViewComponent<INavigationObject>
    {
        public const string Name = nameof(Application.ViewModels.Navigation);
    }
}
