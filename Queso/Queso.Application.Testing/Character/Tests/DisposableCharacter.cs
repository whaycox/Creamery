using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queso.Domain.TestCharacters;
using System.IO;
using Queso.Application.Character;
using Queso.Domain;
using Queso.Domain.Enumerations;

namespace Queso.Application.Character.Tests
{
    [TestClass]
    public class DisposableCharacter
    {
        [TestMethod]
        public void MakesATemporaryCopy()
        {
            string temporaryPath = null;
            using (Character.DisposableCharacter temporary = new Character.DisposableCharacter(Files.StartingCharacters.Amazon))
            {
                temporaryPath = temporary.Path;
                Assert.IsTrue(File.Exists(temporaryPath));
            }
            Assert.IsFalse(File.Exists(temporaryPath));
        }


    }
}
