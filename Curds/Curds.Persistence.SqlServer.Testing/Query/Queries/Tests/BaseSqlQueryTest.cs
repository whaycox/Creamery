using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Queries.Tests
{
    using Implementation;
    using Query.Abstraction;
    using Template;

    [TestClass]
    public class BaseSqlQueryTest : BaseSqlQueryTemplate
    {
        private SimpleSqlQuery TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new SimpleSqlQuery(MockQueryContext.Object);
        }

        [TestMethod]
        public void ExposedParameterBuilderComesFromContext()
        {
            Assert.AreSame(MockParameterBuilder.Object, TestObject.ExposedParameterBuilder);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        public void GenerateCommandFormatsGeneratedTokens(int tokensToGenerate)
        {
            for (int i = 0; i < tokensToGenerate; i++)
                TestObject.TokensToGenerate.Add(Mock.Of<ISqlQueryToken>());

            TestObject.GenerateCommand();

            MockQueryContext.Verify(context => context.Formatter.FormatTokens(TestObject.TokensToGenerate), Times.Once);
            CollectionAssert.AreEqual(TestObject.TokensToGenerate, FormattedTokens);
        }

        [TestMethod]
        public void GenerateCommandFlushesParameterBuilder()
        {
            TestObject.GenerateCommand();

            MockQueryContext.Verify(context => context.ParameterBuilder.Flush(), Times.Once);
        }

        [TestMethod]
        public void GeneratedCommandHasFormattedText()
        {
            MockQueryContext
                .Setup(context => context.Formatter.FormatTokens(It.IsAny<IEnumerable<ISqlQueryToken>>()))
                .Returns(nameof(GeneratedCommandHasFormattedText));

            SqlCommand actual = TestObject.GenerateCommand();

            Assert.AreEqual(nameof(GeneratedCommandHasFormattedText), actual.CommandText);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(5)]
        [DataRow(7)]
        [DataRow(10)]
        public void GeneratedCommandHasFlushedParameters(int parametersToGenerate)
        {
            for (int i = 0; i < parametersToGenerate; i++)
                TestFlushedParameters.Add(new SqlParameter(i.ToString(), i));

            SqlCommand actual = TestObject.GenerateCommand();

            CollectionAssert.AreEqual(TestFlushedParameters, actual.Parameters);
        }

        [TestMethod]
        public void ProcessResultDoesNothing()
        {
            Assert.AreEqual(Task.CompletedTask, TestObject.ProcessResult(MockQueryReader.Object));
        }

        [TestMethod]
        public async Task ExecuteSuppliesQueryToConnectionContext()
        {
            MockQueryContext
                .Setup(context => context.ConnectionContext.Execute(It.IsAny<ISqlQuery>()));

            await TestObject.Execute();

            MockQueryContext.Verify(context => context.ConnectionContext.Execute(TestObject), Times.Once);
        }
    }
}
