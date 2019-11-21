using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.WebApp.Navigation.Domain
{
    public class NavigationTree
    {
        public List<NavigationSection> Sections { get; set; } = new List<NavigationSection>();
    }
}
