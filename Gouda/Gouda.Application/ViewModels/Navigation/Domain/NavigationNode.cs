using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Navigation.Domain
{
    using Abstraction;

    public abstract class NavigationNode : INavigationObject
    {
        public string Name { get; set; }

        public abstract string ViewName { get; }
    }
}
