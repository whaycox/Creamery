using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Parsing.CSV.Tests
{
    [TestClass]
    public class Cell : Test
    {
        [TestMethod]
        public void EmptyOrNullValueIsEmpty()
        {
            EmptyOrNullValueIsEmptyHelper(null);
            EmptyOrNullValueIsEmptyHelper(string.Empty);
        }
        private void EmptyOrNullValueIsEmptyHelper(string suppliedValue)
        {
            Domain.Cell toTest = new Domain.Cell(suppliedValue);
            Assert.AreEqual(string.Empty, toTest.Value);
            Assert.AreEqual(string.Empty, toTest.ToString());
        }
    }
}
