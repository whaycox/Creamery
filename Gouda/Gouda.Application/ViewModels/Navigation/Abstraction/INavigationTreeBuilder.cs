using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Navigation.Abstraction
{
    using Domain;
    using Application.ViewModels.Glyphs.Abstraction;

    public interface INavigationTreeBuilder
    {
        void AddSection(string sectionName);
        void AddGroup(string sectionName, IGlyph groupGlyph, string groupName);
        void AddLeaf(string sectionName, IGlyph leafGlyph, string leafName, string leafDestination);
        void AddLeaf(string sectionName, string groupName, string leafName, string leafDestination);

        NavigationTree Build();
    }
}
