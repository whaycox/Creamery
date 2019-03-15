using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queso.Domain.TestCharacters;
using System.IO;
using Queso.Application.Character;
using Queso.Domain;
using Queso.Domain.Enumerations;

namespace Queso.Infrastructure.Character.Tests
{
    [TestClass]
    public class DisposableCharacter
    {
        private ICharacter Provider = new Character.CharacterProvider();

        [TestMethod]
        public void MakesATemporaryCopy()
        {
            string temporaryPath = null;
            using (Character.DisposableCharacter temporary = new Character.DisposableCharacter(Files.StartingCharacters.Amazon))
            {
                temporaryPath = temporary.Path;
                Domain.Character tempChar = Provider.Load(temporaryPath);
                Assert.IsTrue(tempChar.Class == Class.Amazon);
            }
            Assert.IsFalse(File.Exists(temporaryPath));
        }


    }
}
