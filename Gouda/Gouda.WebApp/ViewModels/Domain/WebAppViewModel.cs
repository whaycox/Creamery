namespace Gouda.WebApp.ViewModels.Domain
{
    using Abstraction;
    using Application.Abstraction;

    public class WebAppViewModel<TViewModel> : IWebAppViewModel
        where TViewModel : class, IViewModel
    {
        public string ViewConcept => Wrapped?.ViewConcept;
        public string ViewName => Wrapped?.ViewName;

        public string ID { get; set; }
        public string Class { get; set; }
        public TViewModel Wrapped { get; }

        public WebAppViewModel(TViewModel viewModel)
        {
            Wrapped = viewModel;
        }
    }
}
