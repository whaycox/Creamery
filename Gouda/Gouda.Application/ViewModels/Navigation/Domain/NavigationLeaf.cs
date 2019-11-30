using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Navigation.Domain
{
    using DeferredValues.Domain;
    using Glyph.Abstraction;

    public class NavigationLeaf : BaseNavigationViewModel
    {
        public override string ViewName => nameof(NavigationLeaf);

        public IGlyphViewModel Glyph { get; set; }
        public DestinationDeferredKey Destination { get; set; }
    }
}
