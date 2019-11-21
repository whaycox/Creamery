using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Gouda.WebApp.ViewComponents.Implementation
{
    using Navigation.Domain;

    public class NavigationNodeViewComponent : ViewComponent
    {
        public const string Name = nameof(NavigationNode);

        public IViewComponentResult Invoke(NavigationNode navigationNode)
        {
            switch (navigationNode)
            {
                case NavigationSection navigationSection:
                    return View(nameof(NavigationSection), navigationSection);
                case NavigationGroup navigationGroup:
                    return View(nameof(NavigationGroup), navigationGroup);
                case NavigationLeaf navigationLeaf:
                    return View(nameof(NavigationLeaf), navigationLeaf);
                default:
                    throw new InvalidOperationException($"Unsupported node {navigationNode.GetType().FullName}");
            }
        }
    }
}
