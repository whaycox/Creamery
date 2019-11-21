using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.WebApp.Navigation.Domain
{
    public class NavigationSection : NavigationNode
    {
        public List<NavigationNode> Nodes { get; set; } = new List<NavigationNode>();
    }
}
