using System;
using System.Collections.Generic;
using System.Text;
using Curds.Application.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.Tests
{
    [TestClass]
    public class SeedGenerator : ISeedGeneratorTemplate<Persistence.SeedGenerator>
    {
        private Persistence.SeedGenerator _obj = new Persistence.SeedGenerator();
        protected override Persistence.SeedGenerator TestObject => _obj;
    }
}
