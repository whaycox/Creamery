using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Persistence.Tests
{
    [TestClass]
    public class SeedGenerator : Template.ISeedGenerator<Implementation.SeedGenerator>
    {
        private Implementation.SeedGenerator _obj = new Implementation.SeedGenerator();
        protected override Implementation.SeedGenerator TestObject => _obj;
    }
}
