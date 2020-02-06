using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System;

namespace Curds.Persistence.Tests
{
    using Abstraction;
    using Domain;
    using Implementation;
    using Query.Domain;

    [TestClass]
    public class SqlQueryWriterTest
    {
        private Table TestTable = new Table();
        private string TestSchema = nameof(TestSchema);
        private string TestTableName = nameof(TestTableName);
        private Column TestColumnOne = new Column { Name = nameof(TestColumnOne) };
        private Column TestColumnTwo = new Column { Name = nameof(TestColumnTwo) };
        private List<ValueEntity> TestValueEntities = new List<ValueEntity>();
        private ValueEntity TestValueEntity = new ValueEntity();
        private IntValue TestIntValue = new IntValue { Name = nameof(TestIntValue) };
        private int TestInt = 10;
        private string TestParameterName = nameof(TestParameterName);

        private Mock<ISqlQueryParameterBuilder> MockParameterBuilder = new Mock<ISqlQueryParameterBuilder>();

        private SqlQueryWriter TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestTable.Schema = TestSchema;
            TestTable.Name = TestTableName;
            TestValueEntities.Add(TestValueEntity);
            TestIntValue.Int = TestInt;

            MockParameterBuilder
                .Setup(builder => builder.RegisterNewParamater(It.IsAny<Value>()))
                .Returns(TestParameterName);

            TestObject = new SqlQueryWriter(MockParameterBuilder.Object);
        }

        [TestMethod]
        public void FlushWithNothingIsCommandWithEmptyString()
        {
            SqlCommand actual = TestObject.Flush();

            Assert.AreEqual(string.Empty, actual.CommandText);
        }

        [TestMethod]
        public void FlushProducesCommandOfCorrectType()
        {
            SqlCommand actual = TestObject.Flush();

            Assert.AreEqual(CommandType.Text, actual.CommandType);
        }

        [TestMethod]
        public void FlushProducesCommandWithNoConnection()
        {
            SqlCommand actual = TestObject.Flush();

            Assert.IsNull(actual.Connection);
        }

        [TestMethod]
        public void FlushProducesCommandWithNoTransaction()
        {
            SqlCommand actual = TestObject.Flush();

            Assert.IsNull(actual.Transaction);
        }

        [TestMethod]
        public void FlushFlushesRegisteredParameters()
        {
            SqlCommand actual = TestObject.Flush();

            MockParameterBuilder.Verify(builder => builder.Flush(), Times.Once);
        }

        [TestMethod]
        public void FlushAttachesParametersToCommand()
        {
            SqlParameter testParameter = new SqlParameter();
            MockParameterBuilder
                .Setup(builder => builder.Flush())
                .Returns(new SqlParameter[] { testParameter });

            SqlCommand actual = TestObject.Flush();

            Assert.AreEqual(1, actual.Parameters.Count);
            Assert.AreSame(testParameter, actual.Parameters[0]);
        }

        [TestMethod]
        public void OneColumnInsertWritesTableDefinitionToCommand()
        {
            TestTable.Columns.Add(TestColumnOne);
            TestObject.Insert(TestTable);

            SqlCommand actual = TestObject.Flush();

            Assert.AreEqual(ExpectedOneColumnInsert, actual.CommandText);
        }
        private string ExpectedOneColumnInsert => @$"INSERT [{TestSchema}].[{TestTableName}] ([{nameof(TestColumnOne)}])
";

        [TestMethod]
        public void MultiColumnInsertWritesTableDefinitionToCommand()
        {
            TestTable.Columns.Add(TestColumnOne);
            TestTable.Columns.Add(TestColumnTwo);
            TestObject.Insert(TestTable);

            SqlCommand actual = TestObject.Flush();

            Assert.AreEqual(ExpectedMultiColumnInsert, actual.CommandText);
        }
        private string ExpectedMultiColumnInsert => @$"INSERT [{TestSchema}].[{TestTableName}]
(
{"\t"} [{nameof(TestColumnOne)}]
{"\t"},[{nameof(TestColumnTwo)}]
)
";

        [TestMethod]
        public void InsertDoesntIncludeIdentityColumn()
        {
            TestColumnTwo.IsIdentity = true;
            TestTable.Columns.Add(TestColumnOne);
            TestTable.Columns.Add(TestColumnTwo);
            TestObject.Insert(TestTable);

            SqlCommand actual = TestObject.Flush();

            Assert.AreEqual(ExpectedOneColumnInsert, actual.CommandText);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(3)]
        [DataRow(5)]
        [DataRow(10)]
        [DataRow(15)]
        public void ValueEntityRegistersParameterForEachValue(int valueNumber)
        {
            for (int i = 0; i < valueNumber; i++)
                TestValueEntity.Values.Add(TestIntValue);

            TestObject.ValueEntities(TestValueEntities);

            MockParameterBuilder.Verify(builder => builder.RegisterNewParamater(TestIntValue), Times.Exactly(valueNumber));
        }

        [TestMethod]
        public void SingleValueValueEntityWritesParameterNameToCommand()
        {
            TestValueEntity.Values.Add(TestIntValue);
            TestObject.ValueEntities(TestValueEntities);

            SqlCommand actual = TestObject.Flush();

            Assert.AreEqual(ExpectedSingleValueValueEntities, actual.CommandText);
        }
        private string ExpectedSingleValueValueEntities => $@"VALUES
(@{nameof(TestParameterName)})";

        [TestMethod]
        public void MultiValueValueEntityWritesParameterNameToCommand()
        {
            TestValueEntity.Values.Add(TestIntValue);
            TestValueEntity.Values.Add(TestIntValue);
            TestObject.ValueEntities(TestValueEntities);

            SqlCommand actual = TestObject.Flush();

            Assert.AreEqual(ExpectedMultiValueValueEntities, actual.CommandText);
        }
        private string ExpectedMultiValueValueEntities => $@"VALUES
(@{nameof(TestParameterName)}, @{nameof(TestParameterName)})";

        [DataTestMethod]
        [DynamicData(nameof(MultipleEntitiesMultipleValuesData), DynamicDataSourceType.Method)]
        public void MultipleValueEntitiesRegistersParameterForEachEntitiesValue(int entityNumber, int valueNumber)
        {
            TestValueEntities.Clear();
            for (int i = 0; i < entityNumber; i++)
                TestValueEntities.Add(TestValueEntity);
            for (int i = 0; i < valueNumber; i++)
                TestValueEntity.Values.Add(TestIntValue);

            TestObject.ValueEntities(TestValueEntities);

            MockParameterBuilder.Verify(builder => builder.RegisterNewParamater(TestIntValue), Times.Exactly(entityNumber * valueNumber));
        }
        private static IEnumerable<object[]> MultipleEntitiesMultipleValuesData()
        {
            foreach (int entityNumber in MultipleEntitiesNumbers)
                foreach (int valueNumber in MultipleValuesNumbers)
                    yield return new object[] { entityNumber, valueNumber };
        }
        private static readonly int[] MultipleEntitiesNumbers = new int[]
        {
            1, 3, 5, 7, 10,
        };
        private static readonly int[] MultipleValuesNumbers = new int[]
        {
            1, 5, 10, 15,
        };

        [TestMethod]
        public void MultipleValueEntitiesWritesToCommand()
        {
            TestValueEntity.Values.Add(TestIntValue);
            TestValueEntities.Add(TestValueEntity);
            TestObject.ValueEntities(TestValueEntities);

            SqlCommand actual = TestObject.Flush();

            Assert.AreEqual(ExpectedMultipleValueEntities, actual.CommandText);
        }
        private string ExpectedMultipleValueEntities => $@"VALUES
(@{nameof(TestParameterName)}),
(@{nameof(TestParameterName)})";
    }
}
