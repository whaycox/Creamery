using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.WebApp.Navigation.Domain
{
    public class NavigationLeaf : NavigationGlyphNode
    {
        public string Destination { get; set; }
    }
}
