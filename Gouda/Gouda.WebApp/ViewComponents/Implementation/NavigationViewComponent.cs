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

        public IViewComponentResult Invoke(INavigationObject navigationObject) => View(navigationObject.ViewName, navigationObject);
    }
}
