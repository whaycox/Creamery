using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gouda.Application.ViewModels.Glyph.Abstraction;

namespace Gouda.Application.ViewModels.Navigation.Domain
{
    public class NavigationGroup : BaseNavigationViewModel
    {
        public override string ViewName => nameof(NavigationGroup);

        public IGlyphViewModel Glyph { get; set; }
        public List<NavigationLeaf> Leaves { get; set; } = new List<NavigationLeaf>();
    }
}
