using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Navigation.Domain
{
    using Abstraction;

    public class NavigationTree : BaseNavigationViewModel
    {
        public override string ViewName => nameof(NavigationTree);

        public List<NavigationSection> Sections { get; set; } = new List<NavigationSection>();
    }
}
