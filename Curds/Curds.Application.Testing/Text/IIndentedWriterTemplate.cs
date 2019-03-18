using Curds.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace Curds.Application.Text
{
    public abstract class IIndentedWriterTemplate<T> : TestTemplate<T> where T : IIndentedWriter
    {
        [TestMethod]
        public void CannotSupplyNegativeIndents()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => TestObject.Indents = -1);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => TestObject.Indents--);
        }

        [TestMethod]
        public void CanWriteWithoutAddingAnything()
        {
            Assert.AreEqual(string.Empty, TestObject.Write);
        }

        [TestMethod]
        public void AddLineIsOutputWithWrite()
        {
            TestObject.AddLine(nameof(AddLineIsOutputWithWrite));
            Assert.AreEqual($"{nameof(AddLineIsOutputWithWrite)}{Environment.NewLine}", TestObject.Write);
        }

        [TestMethod]
        public void DefaultsToTabIndent()
        {
            TestObject.Indents++;
            TestObject.AddLine(nameof(DefaultsToTabIndent));
            Assert.AreEqual($"\t{nameof(DefaultsToTabIndent)}{Environment.NewLine}", TestObject.Write);
        }

        private const int IndentIterations = 3;
        [TestMethod]
        public void CanChangeIndentQuantity()
        {
            for (int i = 0; i < IndentIterations; i++)
            {
                TestObject.Indents++;
                TestObject.AddLine(nameof(CanChangeIndentQuantity));
            }

            Assert.AreEqual(CanChangeIndentQuantityHelper(), TestObject.Write);
        }
        private string CanChangeIndentQuantityHelper()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 1; i <= IndentIterations; i++)
            {
                for (int j = 0; j < i; j++)
                    builder.Append("\t");
                builder.AppendLine(nameof(CanChangeIndentQuantity));
            }
            return builder.ToString();
        }

        [TestMethod]
        public void ScopeIndents()
        {
            TestObject.AddLine(nameof(ScopeIndents));
            using (var scope = TestObject.Scope())
                TestObject.AddLine(nameof(ScopeIndents));
            TestObject.AddLine(nameof(ScopeIndents));
            Assert.AreEqual(ScopeIndentsHelper(), TestObject.Write);
        }
        private string ScopeIndentsHelper()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(nameof(ScopeIndents));
            builder.AppendLine($"\t{nameof(ScopeIndents)}");
            builder.AppendLine(nameof(ScopeIndents));
            return builder.ToString();
        }

        [TestMethod]
        public void CanChangeIndentation()
        {
            TestObject.Indents++;
            TestObject.Indentation = "  "; //2 spaces
            TestObject.AddLine(nameof(CanChangeIndentation));
            Assert.AreEqual($"  {nameof(CanChangeIndentation)}{Environment.NewLine}", TestObject.Write);
        }

        [TestMethod]
        public void CannotSupplyEmptyIndentation()
        {
            Assert.ThrowsException<ArgumentNullException>(() => TestObject.Indentation = string.Empty);
        }

        [TestMethod]
        public void WillFormatArguments()
        {
            TestObject.Indents++;
            TestObject.AddLine("{0}_{1}_{2}", 1, 2, 3);
            Assert.AreEqual($"\t1_2_3{Environment.NewLine}", TestObject.Write);
        }

        [TestMethod]
        public void ClearEmptiesBuffer()
        {
            TestObject.AddLine(nameof(ClearEmptiesBuffer));
            TestObject.Clear();
            Assert.AreEqual(string.Empty, TestObject.Write);
        }
    }
}
