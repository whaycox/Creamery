using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Routing;

namespace Gouda.WebApp.Filters.Implementation
{
    using ViewComponents;
    using Application.ViewModels.Glyphs.Implementation;
    using Controllers.Implementation;
    using Application.ViewModels.Navigation.Abstraction;

    public class NavigationFilter : IActionFilter
    {
        private INavigationTreeBuilder NavigationTreeBuilder { get; }
        private IUrlHelperFactory UrlHelperFactory { get; }

        public NavigationFilter(INavigationTreeBuilder navigationTreeBuilder, IUrlHelperFactory urlHelperFactory)
        {
            NavigationTreeBuilder = navigationTreeBuilder;
            UrlHelperFactory = urlHelperFactory;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            IUrlHelper urlHelper = UrlHelperFactory.GetUrlHelper(context);

            NavigationTreeBuilder.AddSection(SatelliteController.Name);
            NavigationTreeBuilder.AddLeaf(SatelliteController.Name, MaterialIconGlyph.cloud, "Satellites", urlHelper.Action(nameof(SatelliteController.Index), SatelliteController.Name));
        }
    }
}
