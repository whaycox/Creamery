using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Navigation.Domain
{
    using Abstraction;

    public class NavigationSection : BaseNavigationViewModel
    {
        public override string ViewName => nameof(NavigationSection);

        public List<INavigationViewModel> ViewModels { get; set; } = new List<INavigationViewModel>();
    }
}
