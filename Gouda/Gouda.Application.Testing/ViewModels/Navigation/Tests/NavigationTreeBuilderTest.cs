using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Moq;

namespace Gouda.Application.ViewModels.Navigation.Tests
{
    using Domain;
    using Implementation;
    using Application.ViewModels.Glyph.Abstraction;
    using DeferredValues.Domain;
    using Abstraction;

    [TestClass]
    public class NavigationTreeBuilderTest
    {
        private LabelDeferredKey TestSectionLabel = LabelDeferredKey.SatelliteName;
        private LabelDeferredKey TestGroupLabel = LabelDeferredKey.SatelliteIP;
        private LabelDeferredKey TestLeafLabel = LabelDeferredKey.AddSatelliteForm;
        private DestinationDeferredKey TestLeafDestination = DestinationDeferredKey.ListSatellites;

        private Mock<IGlyphViewModel> MockGlyph = new Mock<IGlyphViewModel>();

        private NavigationTreeBuilder TestObject = new NavigationTreeBuilder();

        private NavigationSection VerifyTestSectionWasBuilt(NavigationTree builtTree)
        {
            Assert.AreEqual(1, builtTree.Sections.Count);
            NavigationSection builtSection = builtTree.Sections.First();
            Assert.AreEqual(TestSectionLabel, builtSection.Label);
            return builtSection;
        }

        private T VerifyTestNodeWasBuilt<T>(NavigationSection builtSection)
            where T : INavigationViewModel
        {
            Assert.AreEqual(1, builtSection.ViewModels.Count);
            INavigationViewModel builtViewModel = builtSection.ViewModels.First();
            Assert.IsInstanceOfType(builtViewModel, typeof(T));
            T builtT = (T)builtViewModel;
            return builtT;
        }

        private void VerifyLeafIsTestLeaf(NavigationLeaf builtLeaf, bool shouldHaveGlyph)
        {
            if (shouldHaveGlyph)
                Assert.AreSame(MockGlyph.Object, builtLeaf.Glyph);
            else
                Assert.IsNull(builtLeaf.Glyph);

            Assert.AreEqual(TestLeafLabel, builtLeaf.Label);
            Assert.AreEqual(TestLeafDestination, builtLeaf.Destination);
        }

        [TestMethod]
        public void CanAddSection()
        {
            TestObject.AddSection(TestSectionLabel);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DuplicateSectionThrows()
        {
            TestObject.AddSection(TestSectionLabel);

            TestObject.AddSection(TestSectionLabel);
        }

        [TestMethod]
        public void BuildsAddedSection()
        {
            TestObject.AddSection(TestSectionLabel);

            NavigationTree built = TestObject.Build();

            NavigationSection builtSection = VerifyTestSectionWasBuilt(built);
            Assert.AreEqual(0, builtSection.ViewModels.Count);
        }

        [TestMethod]
        public void CanAddGroupToSection()
        {
            TestObject.AddSection(TestSectionLabel);

            TestObject.AddGroup(TestSectionLabel, MockGlyph.Object, TestGroupLabel);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DuplicateGroupThrows()
        {
            TestObject.AddSection(TestSectionLabel);
            TestObject.AddGroup(TestSectionLabel, MockGlyph.Object, TestGroupLabel);

            TestObject.AddGroup(TestSectionLabel, MockGlyph.Object, TestGroupLabel);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void AddGroupWithMissingSectionThrows()
        {
            TestObject.AddGroup(TestSectionLabel, MockGlyph.Object, TestGroupLabel);
        }

        [TestMethod]
        public void BuildsAddedGroup()
        {
            TestObject.AddSection(TestSectionLabel);
            TestObject.AddGroup(TestSectionLabel, MockGlyph.Object, TestGroupLabel);

            NavigationTree built = TestObject.Build();

            NavigationSection builtSection = VerifyTestSectionWasBuilt(built);
            NavigationGroup builtGroup = VerifyTestNodeWasBuilt<NavigationGroup>(builtSection);
            Assert.AreSame(MockGlyph.Object, builtGroup.Glyph);
            Assert.AreEqual(TestGroupLabel, builtGroup.Label);
            Assert.AreEqual(0, builtGroup.Leaves.Count);
        }

        [TestMethod]
        public void CanAddLeafToSection()
        {
            TestObject.AddSection(TestSectionLabel);

            TestObject.AddLeaf(TestSectionLabel, MockGlyph.Object, TestLeafLabel, TestLeafDestination);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DuplicateLeafNameThrowsAddingToSection()
        {
            TestObject.AddSection(TestSectionLabel);
            TestObject.AddLeaf(TestSectionLabel, MockGlyph.Object, TestLeafLabel, TestLeafDestination);

            TestObject.AddLeaf(TestSectionLabel, MockGlyph.Object, TestLeafLabel, TestLeafDestination);
        }

        [TestMethod]
        public void BuildsLeafAddedToSection()
        {
            TestObject.AddSection(TestSectionLabel);
            TestObject.AddLeaf(TestSectionLabel, MockGlyph.Object, TestLeafLabel, TestLeafDestination);

            NavigationTree built = TestObject.Build();

            NavigationSection builtSection = VerifyTestSectionWasBuilt(built);
            NavigationLeaf builtLeaf = VerifyTestNodeWasBuilt<NavigationLeaf>(builtSection);
            VerifyLeafIsTestLeaf(builtLeaf, true);
        }

        [TestMethod]
        public void CanAddLeafToGroup()
        {
            TestObject.AddSection(TestSectionLabel);
            TestObject.AddGroup(TestSectionLabel, MockGlyph.Object, TestGroupLabel);

            TestObject.AddLeaf(TestSectionLabel, TestGroupLabel, TestLeafLabel, TestLeafDestination);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DuplicateLeafNameThrowsAddingToGroup()
        {
            TestObject.AddSection(TestSectionLabel);
            TestObject.AddGroup(TestSectionLabel, MockGlyph.Object, TestGroupLabel);
            TestObject.AddLeaf(TestSectionLabel, TestGroupLabel, TestLeafLabel, TestLeafDestination);

            TestObject.AddLeaf(TestSectionLabel, TestGroupLabel, TestLeafLabel, TestLeafDestination);
        }

        [TestMethod]
        public void BuildsLeafAddedToGroup()
        {
            TestObject.AddSection(TestSectionLabel);
            TestObject.AddGroup(TestSectionLabel, MockGlyph.Object, TestGroupLabel);
            TestObject.AddLeaf(TestSectionLabel, TestGroupLabel, TestLeafLabel, TestLeafDestination);

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
            TestObject.AddSection(TestSectionLabel);
            TestObject.AddGroup(TestSectionLabel, MockGlyph.Object, TestGroupLabel);

            TestObject.AddLeaf(TestSectionLabel, MockGlyph.Object, TestGroupLabel, TestLeafDestination);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CannotAddGroupWithSameNameAsLeaf()
        {
            TestObject.AddSection(TestSectionLabel);
            TestObject.AddLeaf(TestSectionLabel, MockGlyph.Object, TestLeafLabel, TestLeafDestination);

            TestObject.AddGroup(TestSectionLabel, MockGlyph.Object, TestLeafLabel);
        }
    }
}
