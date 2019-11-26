using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Navigation.Domain
{
    public class NavigationGroup : NavigationGlyphNode
    {
        public override string ViewName => nameof(NavigationGroup);

        public List<NavigationLeaf> Leaves { get; set; } = new List<NavigationLeaf>();
    }
}
