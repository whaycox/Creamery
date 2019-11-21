using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Gouda.WebApp.Navigation.Implementation
{
    using Abstraction;
    using ViewComponents;
    using Glyphs.Implementation;

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
            NavigationTreeBuilder.AddSection("TestSection");
            NavigationTreeBuilder.AddLeaf("TestSection", MaterialIconGlyph.Glyph("warning"), "TestLeaf", "TestLeafDestination");
            NavigationTreeBuilder.AddGroup("TestSection", MaterialIconGlyph.Glyph("done"), "TestGroup");
            NavigationTreeBuilder.AddLeaf("TestSection", "TestGroup", "TestLeaf", "TestLeafDestination");
        }
    }
}
