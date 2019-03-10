using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Queso.Application.Character;

namespace Queso.Infrastructure.Character.Tests
{
    [TestClass]
    public class CharacterProvider : ICharacterTemplate<Character.CharacterProvider>
    {
        private Character.CharacterProvider _obj = new Character.CharacterProvider();
        protected override Character.CharacterProvider TestObject => _obj;
    }
}
