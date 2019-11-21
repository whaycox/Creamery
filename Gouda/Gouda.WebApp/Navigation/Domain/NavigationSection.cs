using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.WebApp.Navigation.Domain
{
    public class NavigationSection : NavigationNode
    {
        public override string ViewName => nameof(NavigationSection);

        public List<NavigationNode> Nodes { get; set; } = new List<NavigationNode>();
    }
}
