using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.WebApp.ViewModels.Domain
{
    using Application.Abstraction;

    public class WebAppViewModel : IViewModel
    {
        public string ViewConcept => ViewModel.ViewConcept;
        public string ViewName => ViewModel.ViewName;

        protected IViewModel ViewModel { get; }

        public string ID { get; set; }
        public string Class { get; set; }

        public WebAppViewModel(IViewModel viewModel)
        {
            ViewModel = viewModel;
        }
    }

    public class WebAppViewModel<TViewModel> : WebAppViewModel 
        where TViewModel : class, IViewModel
    {
        public TViewModel Wrapped => ViewModel as TViewModel;

        public WebAppViewModel(TViewModel viewModel)
            : base(viewModel)
        { }
    }
}
