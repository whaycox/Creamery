using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Curds.Persistence.Query.Tests
{
    using Implementation;
    using Values.Domain;

    [TestClass]
    public class SqlQueryParameterBuilderTest
    {
        private string TestName = nameof(TestName);
        private int TestInt = 7;
        private string TestString = nameof(TestString);

        private SqlQueryParameterBuilder TestObject = new SqlQueryParameterBuilder();

        [TestMethod]
        public void CanRegisterNewIntValue()
        {
            TestObject.RegisterNewParamater(TestName, TestInt);
        }

        [TestMethod]
        public void CanRegisterNewStringValue()
        {
            TestObject.RegisterNewParamater(TestName, TestString);
        }

        [TestMethod]
        public void RegisterNewValueReturnsValueName()
        {
            string actual = TestObject.RegisterNewParamater(TestName, TestInt);

            Assert.AreEqual(nameof(TestName), actual);
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
                parameterNames.Add(TestObject.RegisterNewParamater(TestName, TestInt));

            Assert.AreEqual(parameters, parameterNames.Count);
            Assert.AreEqual(parameters, parameterNames.GroupBy(name => name).Count());
        }

        [TestMethod]
        public void FlushReturnsRegisteredParameters()
        {
            TestObject.RegisterNewParamater(TestName, TestInt);
            TestObject.RegisterNewParamater(TestName, TestInt);

            SqlParameter[] actual = TestObject.Flush();

            Assert.AreEqual(2, actual.Length);
        }

        [TestMethod]
        public void FlushedParameterHasRegisteredName()
        {
            TestObject.RegisterNewParamater(TestName, TestInt);

            SqlParameter[] actual = TestObject.Flush();

            Assert.AreEqual(TestName, actual[0].ParameterName);
        }

        [TestMethod]
        public void FlushedParameterHasRegisteredValue()
        {
            TestObject.RegisterNewParamater(TestName, TestInt);

            SqlParameter[] actual = TestObject.Flush();

            Assert.AreEqual(TestInt, actual[0].Value);
        }

        [TestMethod]
        public void FlushedParametersWithNullValueAreDBNull()
        {
            TestObject.RegisterNewParamater(TestName, null);

            SqlParameter[] actual = TestObject.Flush();

            Assert.AreEqual(DBNull.Value, actual[0].Value);
        }

        [TestMethod]
        public void UnregisterParameterRemovesFromFlush()
        {
            TestObject.RegisterNewParamater(TestName, TestInt);

            TestObject.UnregisterParameter(TestName);

            Assert.AreEqual(0, TestObject.Flush().Length);
        }

        [TestMethod]
        public void UnregisterAllowsNameToBeReused()
        {
            string first = TestObject.RegisterNewParamater(TestName, TestInt);
            string second = TestObject.RegisterNewParamater(TestName, TestInt);
            string third = TestObject.RegisterNewParamater(TestName, TestInt);
            TestObject.UnregisterParameter(second);

            Assert.AreEqual(second, TestObject.RegisterNewParamater(TestName, TestInt));
        }

        [TestMethod]
        public void UnregisterReturnsRegisteredValue()
        {
            TestObject.RegisterNewParamater(TestName, TestInt);

            Assert.AreEqual(TestInt, TestObject.UnregisterParameter(TestName));
        }
    }
}
