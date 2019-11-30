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
    using Application.ViewModels.Glyph.Domain;
    using Controllers.Implementation;
    using Application.ViewModels.Navigation.Abstraction;
    using Adapters.Abstraction;
    using Application.DeferredValues.Domain;

    public class NavigationFilter : IActionFilter
    {
        private INavigationTreeBuilder NavigationTreeBuilder { get; }

        public NavigationFilter(INavigationTreeBuilder navigationTreeBuilder)
        {
            NavigationTreeBuilder = navigationTreeBuilder;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            NavigationTreeBuilder.AddSection(LabelDeferredKey.Satellite);
            NavigationTreeBuilder.AddLeaf(LabelDeferredKey.Satellite, MaterialIconGlyphViewModel.cloud, LabelDeferredKey.Satellites, DestinationDeferredKey.ListSatellites);
        }
    }
}
