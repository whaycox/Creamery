using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace Gouda.WebApp.Navigation.Tests
{
    using Domain;
    using Implementation;
    using Application.ViewModels.Glyphs.Abstraction;

    [TestClass]
    public class NavigationTreeBuilderTest
    {
        private string TestSectionName = nameof(TestSectionName);
        private string TestGroupName = nameof(TestGroupName);
        private string TestLeafName = nameof(TestLeafName);
        private string TestLeafDestination = nameof(TestLeafDestination);

        private Mock<IGlyph> MockGlyph = new Mock<IGlyph>();

        private NavigationTreeBuilder TestObject = new NavigationTreeBuilder();

        private NavigationSection VerifyTestSectionWasBuilt(NavigationTree builtTree)
        {
            Assert.AreEqual(1, builtTree.Sections.Count);
            NavigationSection builtSection = builtTree.Sections.First();
            Assert.AreEqual(TestSectionName, builtSection.Name);
            return builtSection;
        }

        private T VerifyTestNodeWasBuilt<T>(NavigationSection builtSection)
            where T : NavigationNode
        {
            Assert.AreEqual(1, builtSection.Nodes.Count);
            NavigationNode builtNode = builtSection.Nodes.First();
            Assert.IsInstanceOfType(builtNode, typeof(T));
            T builtT = (T)builtNode;
            return builtT;
        }

        private void VerifyLeafIsTestLeaf(NavigationLeaf builtLeaf, bool shouldHaveGlyph)
        {
            if (shouldHaveGlyph)
                Assert.AreSame(MockGlyph.Object, builtLeaf.Glyph);
            else
                Assert.IsNull(builtLeaf.Glyph);

            Assert.AreEqual(TestLeafName, builtLeaf.Name);
            Assert.AreEqual(TestLeafDestination, builtLeaf.Destination);
        }

        [TestMethod]
        public void CanAddSection()
        {
            TestObject.AddSection(TestSectionName);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("  ")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidSectionNameThrows(string sectionName)
        {
            TestObject.AddSection(sectionName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DuplicateSectionThrows()
        {
            TestObject.AddSection(TestSectionName);

            TestObject.AddSection(TestSectionName);
        }

        [TestMethod]
        public void BuildsAddedSection()
        {
            TestObject.AddSection(TestSectionName);

            NavigationTree built = TestObject.Build();

            NavigationSection builtSection = VerifyTestSectionWasBuilt(built);
            Assert.AreEqual(0, builtSection.Nodes.Count);
        }

        [TestMethod]
        public void CanAddGroupToSection()
        {
            TestObject.AddSection(TestSectionName);

            TestObject.AddGroup(TestSectionName, MockGlyph.Object, TestGroupName);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("  ")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidGroupNameThrows(string groupName)
        {
            TestObject.AddSection(TestSectionName);

            TestObject.AddGroup(TestSectionName, MockGlyph.Object, groupName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DuplicateGroupThrows()
        {
            TestObject.AddSection(TestSectionName);
            TestObject.AddGroup(TestSectionName, MockGlyph.Object, TestGroupName);

            TestObject.AddGroup(TestSectionName, MockGlyph.Object, TestGroupName);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void AddGroupWithMissingSectionThrows()
        {
            TestObject.AddGroup(TestSectionName, MockGlyph.Object, TestGroupName);
        }

        [TestMethod]
        public void BuildsAddedGroup()
        {
            TestObject.AddSection(TestSectionName);
            TestObject.AddGroup(TestSectionName, MockGlyph.Object, TestGroupName);

            NavigationTree built = TestObject.Build();

            NavigationSection builtSection = VerifyTestSectionWasBuilt(built);
            NavigationGroup builtGroup = VerifyTestNodeWasBuilt<NavigationGroup>(builtSection);
            Assert.AreSame(MockGlyph.Object, builtGroup.Glyph);
            Assert.AreEqual(TestGroupName, builtGroup.Name);
            Assert.AreEqual(0, builtGroup.Leaves.Count);
        }

        [TestMethod]
        public void CanAddLeafToSection()
        {
            TestObject.AddSection(TestSectionName);

            TestObject.AddLeaf(TestSectionName, MockGlyph.Object, TestLeafName, TestLeafDestination);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("  ")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidLeafNameThrowsAddingToSection(string leafName)
        {
            TestObject.AddSection(TestSectionName);

            TestObject.AddLeaf(TestSectionName, MockGlyph.Object, leafName, TestLeafDestination);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DuplicateLeafNameThrowsAddingToSection()
        {
            TestObject.AddSection(TestSectionName);
            TestObject.AddLeaf(TestSectionName, MockGlyph.Object, TestLeafName, TestLeafDestination);

            TestObject.AddLeaf(TestSectionName, MockGlyph.Object, TestLeafName, TestLeafDestination);
        }

        [TestMethod]
        public void BuildsLeafAddedToSection()
        {
            TestObject.AddSection(TestSectionName);
            TestObject.AddLeaf(TestSectionName, MockGlyph.Object, TestLeafName, TestLeafDestination);

            NavigationTree built = TestObject.Build();

            NavigationSection builtSection = VerifyTestSectionWasBuilt(built);
            NavigationLeaf builtLeaf = VerifyTestNodeWasBuilt<NavigationLeaf>(builtSection);
            VerifyLeafIsTestLeaf(builtLeaf, true);
        }

        [TestMethod]
        public void CanAddLeafToGroup()
        {
            TestObject.AddSection(TestSectionName);
            TestObject.AddGroup(TestSectionName, MockGlyph.Object, TestGroupName);

            TestObject.AddLeaf(TestSectionName, TestGroupName, TestLeafName, TestLeafDestination);
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("  ")]
        [ExpectedException(typeof(ArgumentNullException))]
        public void InvalidLeafNameThrowsAddingToGroup(string leafName)
        {
            TestObject.AddSection(TestSectionName);
            TestObject.AddGroup(TestSectionName, MockGlyph.Object, TestGroupName);

            TestObject.AddLeaf(TestSectionName, TestGroupName, leafName, TestLeafDestination);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DuplicateLeafNameThrowsAddingToGroup()
        {
            TestObject.AddSection(TestSectionName);
            TestObject.AddGroup(TestSectionName, MockGlyph.Object, TestGroupName);
            TestObject.AddLeaf(TestSectionName, TestGroupName, TestLeafName, TestLeafDestination);

            TestObject.AddLeaf(TestSectionName, TestGroupName, TestLeafName, TestLeafDestination);
        }

        [TestMethod]
        public void BuildsLeafAddedToGroup()
        {
            TestObject.AddSection(TestSectionName);
            TestObject.AddGroup(TestSectionName, MockGlyph.Object, TestGroupName);
            TestObject.AddLeaf(TestSectionName, TestGroupName, TestLeafName, TestLeafDestination);

            NavigationTree built = TestObject.Build();

            NavigationSection builtSection = VerifyTestSectionWasBuilt(built);
            NavigationGroup builtGroup = VerifyTestNodeWasBuilt<NavigationGroup>(builtSection);
            Assert.AreEqual(1, builtGroup.Leaves.Count);
            NavigationLeaf builtLeaf = builtGroup.Leaves.First();
            VerifyLeafIsTestLeaf(builtLeaf, false);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotAddLeafWithSameNameAsGroupToSection()
        {
            TestObject.AddSection(TestSectionName);
            TestObject.AddGroup(TestSectionName, MockGlyph.Object, TestGroupName);

            TestObject.AddLeaf(TestSectionName, MockGlyph.Object, TestGroupName, TestLeafDestination);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotAddGroupWithSameNameAsLeaf()
        {
            TestObject.AddSection(TestSectionName);
            TestObject.AddLeaf(TestSectionName, MockGlyph.Object, TestLeafName, TestLeafDestination);

            TestObject.AddGroup(TestSectionName, MockGlyph.Object, TestLeafName);
        }
    }
}
