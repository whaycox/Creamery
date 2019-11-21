using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.WebApp.Navigation.Domain
{
    using Abstraction;

    public abstract class NavigationNode : INavigationObject
    {
        public string Name { get; set; }

        public abstract string ViewName { get; }
    }
}
