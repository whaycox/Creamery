using System;
using System.Collections.Generic;

namespace Gouda.Application.ViewModels.Navigation.Implementation
{
    using Abstraction;
    using Domain;
    using Application.ViewModels.Glyph.Abstraction;
    using DeferredValues.Domain;

    public class NavigationTreeBuilder : INavigationTreeBuilder
    {
        private Dictionary<LabelDeferredKey, NavigationSection> Sections { get; } = new Dictionary<LabelDeferredKey, NavigationSection>();
        private Dictionary<NavigationSection, Dictionary<LabelDeferredKey, INavigationViewModel>> SectionViewModels { get; } = new Dictionary<NavigationSection, Dictionary<LabelDeferredKey, INavigationViewModel>>();
        private Dictionary<NavigationGroup, Dictionary<LabelDeferredKey, NavigationLeaf>> GroupLeaves { get; } = new Dictionary<NavigationGroup, Dictionary<LabelDeferredKey, NavigationLeaf>>();

        private NavigationSection FindSection(LabelDeferredKey sectionLabel)
        {
            if (!Sections.TryGetValue(sectionLabel, out NavigationSection section))
                throw new KeyNotFoundException(nameof(sectionLabel));

            if (!SectionViewModels.ContainsKey(section))
                SectionViewModels.Add(section, new Dictionary<LabelDeferredKey, INavigationViewModel>());
            return section;
        }

        private NavigationGroup FindGroup(NavigationSection section, LabelDeferredKey groupLabel)
        {
            if (!SectionViewModels[section].TryGetValue(groupLabel, out INavigationViewModel viewModel))
                throw new KeyNotFoundException(nameof(groupLabel));

            NavigationGroup group = (NavigationGroup)viewModel;
            if (!GroupLeaves.ContainsKey(group))
                GroupLeaves.Add(group, new Dictionary<LabelDeferredKey, NavigationLeaf>());
            return group;
        }

        public void AddSection(LabelDeferredKey sectionLabel)
        {
            NavigationSection section = new NavigationSection
            {
                Label = sectionLabel,
            };
            Sections.Add(sectionLabel, section);
        }

        public void AddGroup(LabelDeferredKey sectionLabel, IGlyphViewModel groupGlyph, LabelDeferredKey groupLabel)
        {
            NavigationSection section = FindSection(sectionLabel);
            NavigationGroup group = new NavigationGroup
            {
                Glyph = groupGlyph,
                Label = groupLabel,
            };
            section.ViewModels.Add(group);
            SectionViewModels[section].Add(groupLabel, group);
        }

        public void AddLeaf(LabelDeferredKey sectionLabel, IGlyphViewModel leafGlyph, LabelDeferredKey leafLabel, DestinationDeferredKey leafDestination)
        {
            NavigationSection section = FindSection(sectionLabel);
            NavigationLeaf leaf = new NavigationLeaf
            {
                Glyph = leafGlyph,
                Label = leafLabel,
                Destination = leafDestination,
            };
            section.ViewModels.Add(leaf);
            SectionViewModels[section].Add(leafLabel, leaf);
        }

        public void AddLeaf(LabelDeferredKey sectionLabel, LabelDeferredKey groupLabel, LabelDeferredKey leafLabel, DestinationDeferredKey leafDestination)
        {
            NavigationSection section = FindSection(sectionLabel);
            NavigationGroup group = FindGroup(section, groupLabel);
            NavigationLeaf leaf = new NavigationLeaf
            {
                Label = leafLabel,
                Destination = leafDestination,
            };
            group.Leaves.Add(leaf);
            GroupLeaves[group].Add(leaf.Label, leaf);
        }

        public NavigationTree Build()
        {
            NavigationTree tree = new NavigationTree();
            foreach (var section in Sections)
                tree.Sections.Add(section.Value);
            return tree;
        }
    }
}
