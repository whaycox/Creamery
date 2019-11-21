using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.WebApp.Navigation.Domain
{
    using WebApp.Abstraction;

    public class NavigationGroup : NavigationGlyphNode
    {
        public List<NavigationLeaf> Leaves { get; set; } = new List<NavigationLeaf>();
    }
}
