using Curds;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Feta.OpenType.Tables.Template
{
    using Abstraction;
    using Domain;
    using OpenType.Domain;
    using OpenType.Mock;

    public abstract class ITablePersistor<T, U> : Test<T>
        where T : ITablePersistor<U>
        where U : BaseTable
    {
        protected MemoryStream TestStream = null;
        protected FontReader Reader = null;
        protected FontWriter Writer = null;

        protected IParsedTables MockParsedTables = new IParsedTables();

        protected abstract U[] Samples { get; }

        protected abstract void VerifyTablesAreEqual(U expected, U actual);

        private void RefreshStream()
        {
            DisposeObjects();
            BuildReaderWriter();
        }

        [TestInitialize]
        public void BuildReaderWriter()
        {
            TestStream = new MemoryStream();
            Reader = new FontReader(TestStream);
            Writer = new FontWriter(TestStream);
            MockParsedTables = new IParsedTables();
        }
        [TestCleanup]
        public void DisposeObjects()
        {
            TestStream?.Dispose();
            Reader?.Dispose();
            Writer?.Dispose();
        }

        [TestMethod]
        public void CanReadWriterOutput()
        {
            foreach (U sample in Samples)
            {
                TestObject.Write(Writer, sample);
                TestStream.Seek(0, SeekOrigin.Begin);
                TestObject.Read(Reader, MockParsedTables);
                Assert.AreEqual(1, MockParsedTables.Added.Count);
                VerifyTablesAreEqual(sample, MockParsedTables.Added[0] as U);

                RefreshStream();
            }
        }

        [TestMethod]
        public void TableOutputIsMultipleOfFour()
        {
            foreach (U sample in Samples)
            {
                TestObject.Write(Writer, sample);
                Assert.IsTrue(TestStream.Length % 4 == 0);

                RefreshStream();
            }
        }
    }
}
