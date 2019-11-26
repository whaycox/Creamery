using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Navigation.Domain
{
    public class NavigationLeaf : NavigationGlyphNode
    {
        public override string ViewName => nameof(NavigationLeaf);

        public string Destination { get; set; }
    }
}
