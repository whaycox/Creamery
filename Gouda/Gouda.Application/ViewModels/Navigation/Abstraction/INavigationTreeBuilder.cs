using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Navigation.Abstraction
{
    using Domain;
    using Application.ViewModels.Glyph.Abstraction;
    using DeferredValues.Domain;

    public interface INavigationTreeBuilder
    {
        void AddSection(LabelDeferredKey sectionLabel);
        void AddGroup(LabelDeferredKey sectionLabel, IGlyphViewModel groupGlyph, LabelDeferredKey groupLabel);
        void AddLeaf(LabelDeferredKey sectionLabel, IGlyphViewModel leafGlyph, LabelDeferredKey leafLabel, DestinationDeferredKey leafDestination);
        void AddLeaf(LabelDeferredKey sectionLabel, LabelDeferredKey groupLabel, LabelDeferredKey leafLabel, DestinationDeferredKey leafDestination);

        NavigationTree Build();
    }
}
