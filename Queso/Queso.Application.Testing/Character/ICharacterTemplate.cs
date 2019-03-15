using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queso.Domain.Enumerations;
using Queso.Domain.TestCharacters;
using System;
using System.Collections.Generic;
using System.IO;

namespace Queso.Application.Character
{
    public abstract class ICharacterTemplate<T> : TestTemplate<T> where T : ICharacter
    {
        private const string StartingCharacterName = "Testing";

        private delegate void StartingTest(Domain.Character character);

        private List<(string path, StartingTest validation)> StartingCharacters => new List<(string path, StartingTest validation)>
        {
            (Files.StartingCharacters.Amazon, ValidateAmazon),
            (Files.StartingCharacters.Sorceress, ValidateSorceress),
            (Files.StartingCharacters.Necromancer, ValidateNecromancer),
            (Files.StartingCharacters.Paladin, ValidatePaladin),
            (Files.StartingCharacters.Barbarian, ValidateBarbarian),
            (Files.StartingCharacters.Druid, ValidateDruid),
            (Files.StartingCharacters.Assassin, ValidateAssassin),
        };

        private void ValidateAmazon(Domain.Character amazon) => ValidateStartingCharacter(amazon, Class.Amazon);
        private void ValidateSorceress(Domain.Character sorceress) => ValidateStartingCharacter(sorceress, Class.Sorceress);
        private void ValidateNecromancer(Domain.Character necromancer) => ValidateStartingCharacter(necromancer, Class.Necromancer);
        private void ValidatePaladin(Domain.Character paladin) => ValidateStartingCharacter(paladin, Class.Paladin);
        private void ValidateBarbarian(Domain.Character barbarian) => ValidateStartingCharacter(barbarian, Class.Barbarian);
        private void ValidateDruid(Domain.Character druid) => ValidateStartingCharacter(druid, Class.Druid);
        private void ValidateAssassin(Domain.Character assassin) => ValidateStartingCharacter(assassin, Class.Assassin);

        private void ValidateStartingCharacter(Domain.Character character, Class characterClass)
        {
            ValidateFileInformation(character);
            Assert.AreEqual(StartingCharacterName, character.Name);
            Assert.AreEqual(characterClass, character.Class);
            Assert.IsTrue(character.Hardcore);
            Assert.IsTrue(character.Alive);

            switch (characterClass)
            {
                case Class.Amazon:
                    StartingAmazon(character);
                    break;
                case Class.Sorceress:
                    StartingSorceresss(character);
                    break;
                case Class.Necromancer:
                    StartingNecromancer(character);
                    break;
                case Class.Paladin:
                    StartingPaladin(character);
                    break;
                case Class.Barbarian:
                    StartingBarbarian(character);
                    break;
                case Class.Druid:
                    StartingDruid(character);
                    break;
                case Class.Assassin:
                    StartingAssassin(character);
                    break;
                default:
                    throw new InvalidOperationException("Unexpected class");
            }
        }
        private void ValidateFileInformation(Domain.Character character)
        {
            Assert.AreEqual(Domain.Character.Signature, character.File.Signature);
            Assert.AreEqual(VersionID.v1_10plus, character.File.VersionID);
        }

        private void StartingAmazon(Domain.Character amazon)
        {
            Assert.AreEqual(Files.StartingCharacters.AmazonSize, amazon.File.Size);
        }
        private void StartingSorceresss(Domain.Character sorceress)
        {
            Assert.AreEqual(Files.StartingCharacters.SorceressSize, sorceress.File.Size);
        }
        private void StartingNecromancer(Domain.Character necromancer)
        {
            Assert.AreEqual(Files.StartingCharacters.NecromancerSize, necromancer.File.Size);
        }
        private void StartingPaladin(Domain.Character paladin)
        {
            Assert.AreEqual(Files.StartingCharacters.PaladinSize, paladin.File.Size);
        }
        private void StartingBarbarian(Domain.Character barbarian)
        {
            Assert.AreEqual(Files.StartingCharacters.BarbarianSize, barbarian.File.Size);
        }
        private void StartingDruid(Domain.Character druid)
        {
            Assert.AreEqual(Files.StartingCharacters.DruidSize, druid.File.Size);
        }
        private void StartingAssassin(Domain.Character assassin)
        {
            Assert.AreEqual(Files.StartingCharacters.AssassinSize, assassin.File.Size);
        }

        [TestMethod]
        public void InvalidChecksumFails()
        {
            Assert.ThrowsException<FormatException>(() => TestObject.Load(Files.InvalidChecksum));
        }

        [TestMethod]
        public void LoadsStartingCharacters()
        {
            foreach(var starter in StartingCharacters)
                starter.validation(TestObject.Load(starter.path));
        }

        [TestMethod]
        public void LoadsDeadCharacter()
        {
            Domain.Character loaded = TestObject.Load(Files.Dead);
            Assert.IsTrue(loaded.Hardcore);
            Assert.IsFalse(loaded.Alive);
        }

        [TestMethod]
        public void CannotResurrectAliveCharacter()
        {
            Assert.ThrowsException<InvalidOperationException>(() => TestObject.Resurrect(Files.StartingCharacters.Amazon));
        }

        [TestMethod]
        public void ResurrectsDeadCharacter()
        {
            using (DisposableCharacter temporary = new DisposableCharacter(Files.Dead))
            {
                Domain.Character dead = TestObject.Load(temporary.Path);
                TestObject.Resurrect(temporary.Path);

                Domain.Character resurrected = TestObject.Load(temporary.Path);
                Assert.IsTrue(resurrected.Hardcore);
                Assert.IsTrue(resurrected.Alive);
            }
        }
    }
}
