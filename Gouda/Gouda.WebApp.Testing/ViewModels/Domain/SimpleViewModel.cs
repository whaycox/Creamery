namespace Gouda.WebApp.ViewModels.Domain
{
    using Application.Abstraction;

    public class SimpleViewModel : IViewModel
    {
        public string ViewConcept => nameof(SimpleViewModel);
        public string ViewName => nameof(SimpleViewModel);
    }
}
