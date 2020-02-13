using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Curds.Persistence.Query.Tests
{
    using Domain;
    using Implementation;

    [TestClass]
    public class SqlQueryParameterBuilderTest
    {
        private IntValue TestIntValue = new IntValue { Name = nameof(TestIntValue) };
        private int TestInt = 7;
        private StringValue TestStringValue = new StringValue { Name = nameof(TestStringValue) };
        private string TestString = nameof(TestString);

        private SqlQueryParameterBuilder TestObject = new SqlQueryParameterBuilder();

        [TestInitialize]
        public void Init()
        {
            TestIntValue.Int = TestInt;
            TestStringValue.String = TestString;
        }

        [TestMethod]
        public void CanRegisterNewIntValue()
        {
            TestObject.RegisterNewParamater(TestIntValue);
        }

        [TestMethod]
        public void CanRegisterNewStringValue()
        {
            TestObject.RegisterNewParamater(TestStringValue);
        }

        [TestMethod]
        public void RegisterNewValueReturnsValueName()
        {
            string actual = TestObject.RegisterNewParamater(TestIntValue);

            Assert.AreEqual(nameof(TestIntValue), actual);
        }

        [DataTestMethod]
        [DataRow(2)]
        [DataRow(5)]
        [DataRow(8)]
        [DataRow(10)]
        public void RegisteringValueWithSameNameGetsDifferentParameterName(int parameters)
        {
            List<string> parameterNames = new List<string>();
            for (int i = 0; i < parameters; i++)
                parameterNames.Add(TestObject.RegisterNewParamater(TestIntValue));

            Assert.AreEqual(parameters, parameterNames.Count);
            Assert.AreEqual(parameters, parameterNames.GroupBy(name => name).Count());
        }

        [TestMethod]
        public void FlushReturnsRegisteredParameters()
        {
            TestObject.RegisterNewParamater(TestIntValue);
            TestObject.RegisterNewParamater(TestStringValue);

            SqlParameter[] actual = TestObject.Flush();

            Assert.AreEqual(2, actual.Length);
        }

        [TestMethod]
        public void FlushedParameterHasRegisteredName()
        {
            TestObject.RegisterNewParamater(TestIntValue);

            SqlParameter[] actual = TestObject.Flush();

            Assert.AreEqual(nameof(TestIntValue), actual[0].ParameterName);
        }

        [TestMethod]
        public void FlushedParameterHasRegisteredValue()
        {
            TestObject.RegisterNewParamater(TestIntValue);

            SqlParameter[] actual = TestObject.Flush();

            Assert.AreEqual(TestInt, actual[0].Value);
        }

        [TestMethod]
        public void FlushedParametersWithNullValueAreDBNull()
        {
            TestStringValue.String = null;
            TestObject.RegisterNewParamater(TestStringValue);

            SqlParameter[] actual = TestObject.Flush();

            Assert.AreEqual(DBNull.Value, actual[0].Value);
        }
    }
}
