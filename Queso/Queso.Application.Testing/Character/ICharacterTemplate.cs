using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Curds.Domain;
using Queso.Domain.TestCharacters;
using Queso.Domain.Enumerations;

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
            Assert.AreEqual(StartingCharacterName, character.Name);
            Assert.AreEqual(characterClass, character.Class);
        }

        [TestMethod]
        public void LoadsStartingCharacters()
        {
            foreach(var starter in StartingCharacters)
                starter.validation(TestObject.Load(starter.path));
        }
    }
}
