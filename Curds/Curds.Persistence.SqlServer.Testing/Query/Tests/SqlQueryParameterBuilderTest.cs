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
            throw new NotImplementedException();
            //TestObject.RegisterNewParamater(TestIntValue);
        }

        [TestMethod]
        public void CanRegisterNewStringValue()
        {
            throw new NotImplementedException();
            //TestObject.RegisterNewParamater(TestStringValue);
        }

        [TestMethod]
        public void RegisterNewValueReturnsValueName()
        {
            throw new NotImplementedException();
            //string actual = TestObject.RegisterNewParamater(TestIntValue);

            //Assert.AreEqual(nameof(TestIntValue), actual);
        }

        [DataTestMethod]
        [DataRow(2)]
        [DataRow(5)]
        [DataRow(8)]
        [DataRow(10)]
        public void RegisteringValueWithSameNameGetsDifferentParameterName(int parameters)
        {
            throw new NotImplementedException();
            //List<string> parameterNames = new List<string>();
            //for (int i = 0; i < parameters; i++)
            //    parameterNames.Add(TestObject.RegisterNewParamater(TestIntValue));

            //Assert.AreEqual(parameters, parameterNames.Count);
            //Assert.AreEqual(parameters, parameterNames.GroupBy(name => name).Count());
        }

        [TestMethod]
        public void FlushReturnsRegisteredParameters()
        {
            throw new NotImplementedException();
            //TestObject.RegisterNewParamater(TestIntValue);
            //TestObject.RegisterNewParamater(TestStringValue);

            //SqlParameter[] actual = TestObject.Flush();

            //Assert.AreEqual(2, actual.Length);
        }

        [TestMethod]
        public void FlushedParameterHasRegisteredName()
        {
            throw new NotImplementedException();
            //TestObject.RegisterNewParamater(TestIntValue);

            //SqlParameter[] actual = TestObject.Flush();

            //Assert.AreEqual(nameof(TestIntValue), actual[0].ParameterName);
        }

        [TestMethod]
        public void FlushedParameterHasRegisteredValue()
        {
            throw new NotImplementedException();
            //TestObject.RegisterNewParamater(TestIntValue);

            //SqlParameter[] actual = TestObject.Flush();

            //Assert.AreEqual(TestInt, actual[0].Value);
        }

        [TestMethod]
        public void FlushedParametersWithNullValueAreDBNull()
        {
            throw new NotImplementedException();
            //TestStringValue.String = null;
            //TestObject.RegisterNewParamater(TestStringValue);

            //SqlParameter[] actual = TestObject.Flush();

            //Assert.AreEqual(DBNull.Value, actual[0].Value);
        }
    }
}
