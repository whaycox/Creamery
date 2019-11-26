using System;
using System.Collections.Generic;

namespace Gouda.WebApp.Navigation.Implementation
{
    using Abstraction;
    using Domain;
    using Application.ViewModels.Glyphs.Abstraction;

    public class NavigationTreeBuilder : INavigationTreeBuilder
    {
        private Dictionary<string, NavigationSection> Sections { get; } = new Dictionary<string, NavigationSection>();
        private Dictionary<NavigationSection, Dictionary<string, NavigationNode>> SectionNodes { get; } = new Dictionary<NavigationSection, Dictionary<string, NavigationNode>>();
        private Dictionary<NavigationGroup, Dictionary<string, NavigationLeaf>> GroupLeaves { get; } = new Dictionary<NavigationGroup, Dictionary<string, NavigationLeaf>>();

        private NavigationSection FindSection(string sectionName)
        {
            if (!Sections.TryGetValue(sectionName, out NavigationSection section))
                throw new KeyNotFoundException(nameof(sectionName));

            if (!SectionNodes.ContainsKey(section))
                SectionNodes.Add(section, new Dictionary<string, NavigationNode>());
            return section;
        }

        private NavigationGroup FindGroup(NavigationSection section, string groupName)
        {
            if (!SectionNodes[section].TryGetValue(groupName, out NavigationNode node))
                throw new KeyNotFoundException(nameof(groupName));

            NavigationGroup group = (NavigationGroup)node;
            if (!GroupLeaves.ContainsKey(group))
                GroupLeaves.Add(group, new Dictionary<string, NavigationLeaf>());
            return group;
        }

        public void AddSection(string sectionName)
        {
            if (string.IsNullOrWhiteSpace(sectionName))
                throw new ArgumentNullException(nameof(sectionName));

            NavigationSection section = new NavigationSection
            {
                Name = sectionName,
            };
            Sections.Add(sectionName, section);
        }

        public void AddGroup(string sectionName, IGlyph groupGlyph, string groupName)
        {
            if (string.IsNullOrWhiteSpace(groupName))
                throw new ArgumentNullException(nameof(groupName));

            NavigationSection section = FindSection(sectionName);
            NavigationGroup group = new NavigationGroup
            {
                Glyph = groupGlyph,
                Name = groupName,
            };
            section.Nodes.Add(group);
            SectionNodes[section].Add(groupName, group);
        }

        public void AddLeaf(string sectionName, IGlyph leafGlyph, string leafName, string leafDestination)
        {
            if (string.IsNullOrWhiteSpace(leafName))
                throw new ArgumentNullException(nameof(leafName));

            NavigationSection section = FindSection(sectionName);
            NavigationLeaf leaf = new NavigationLeaf
            {
                Glyph = leafGlyph,
                Name = leafName,
                Destination = leafDestination,
            };
            section.Nodes.Add(leaf);
            SectionNodes[section].Add(leafName, leaf);
        }

        public void AddLeaf(string sectionName, string groupName, string leafName, string leafDestination)
        {
            if (string.IsNullOrWhiteSpace(leafName))
                throw new ArgumentNullException(nameof(leafName));

            NavigationSection section = FindSection(sectionName);
            NavigationGroup group = FindGroup(section, groupName);
            NavigationLeaf leaf = new NavigationLeaf
            {
                Name = leafName,
                Destination = leafDestination,
            };
            group.Leaves.Add(leaf);
            GroupLeaves[group].Add(leaf.Name, leaf);
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
