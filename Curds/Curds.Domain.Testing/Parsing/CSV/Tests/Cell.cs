using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Domain.Parsing.CSV.Tests
{
    [TestClass]
    public class Cell
    {

        [TestMethod]
        public void EmptyOrNullValueIsEmpty()
        {
            EmptyOrNullValueIsEmptyHelper(null);
            EmptyOrNullValueIsEmptyHelper(string.Empty);
        }
        private void EmptyOrNullValueIsEmptyHelper(string suppliedValue)
        {
            CSV.Cell toTest = new CSV.Cell(suppliedValue);
            Assert.AreEqual(string.Empty, toTest.Value);
            Assert.AreEqual(string.Empty, toTest.ToString());

        }


    }
}
