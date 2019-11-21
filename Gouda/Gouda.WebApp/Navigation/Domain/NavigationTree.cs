using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.WebApp.Navigation.Domain
{
    using Abstraction;

    public class NavigationTree : INavigationObject
    {
        public string ViewName => nameof(NavigationTree);

        public List<NavigationSection> Sections { get; set; } = new List<NavigationSection>();
    }
}
