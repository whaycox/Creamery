﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queso.Domain.TestCharacters;
using System.IO;
using Queso.Application.Character;
using Queso.Domain;
using Queso.Domain.Enumerations;
using Queso.Infrastructure.Character;

namespace Queso.Domain.Tests
{
    [TestClass]
    public class DisposableCharacter
    {
        private ICharacter Provider = new CharacterProvider();

        [TestMethod]
        public void MakesATemporaryCopy()
        {
            string temporaryPath = null;
            using (Domain.DisposableCharacter temporary = new Domain.DisposableCharacter(Files.StartingCharacters.Amazon))
            {
                temporaryPath = temporary.Path;
                Character tempChar = Provider.Load(temporaryPath);
                Assert.IsTrue(tempChar.Class == Class.Amazon);
            }
            Assert.IsFalse(File.Exists(temporaryPath));
        }


    }
}