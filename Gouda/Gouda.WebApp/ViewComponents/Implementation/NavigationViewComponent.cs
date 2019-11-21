using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.WebApp.ViewComponents.Implementation
{
    using Navigation.Abstraction;
    using Navigation.Domain;

    public class NavigationViewComponent : ViewComponent
    {
        public const string Name = nameof(Navigation);

        private INavigationTreeBuilder NavigationTreeBuilder { get; }

        public NavigationViewComponent(INavigationTreeBuilder navigationTreeBuilder)
        {
            NavigationTreeBuilder = navigationTreeBuilder;
        }

        public IViewComponentResult Invoke()
        {
            return View(NavigationTreeBuilder.Build());
        }
    }
}
