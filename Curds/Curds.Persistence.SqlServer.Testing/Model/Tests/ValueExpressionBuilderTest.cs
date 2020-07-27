using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Persistence.Model.Tests
{
    using Abstraction;
    using Implementation;
    using Model.Domain;
    using Persistence.Domain;
    using Query.Domain;
    using Query.Values.Domain;

    [TestClass]
    public class ValueExpressionBuilderTest
    {
        #region DynamicData
        private static IEnumerable<object[]> StringDynamicData => new List<object[]>
        {
            new object[] { null },
            new object[] { string.Empty },
            new object[] { "  " },
            new object[] { nameof(StringDynamicData) },
        };

        private static IEnumerable<object[]> BoolDynamicData => new List<object[]>
        {
            new object[] { true },
            new object[] { false },
        };
        private static IEnumerable<object[]> NullableBoolDynamicData => new List<object[]>
        {
            new object[] { null },
            new object[] { true },
            new object[] { false },
        };

        private static IEnumerable<object[]> ByteDynamicData => new List<object[]>
        {
            new object[] { (byte)0 },
            new object[] { (byte)2 },
            new object[] { (byte)4 },
            new object[] { (byte)8 },
            new object[] { (byte)16 },
            new object[] { (byte)32 },
            new object[] { (byte)64 },
            new object[] { (byte)128 },
            new object[] { (byte)255 },
        };
        private static IEnumerable<object[]> NullableByteDynamicData => new List<object[]>
        {
            new object[] { null },
            new object[] { (byte)0 },
            new object[] { (byte)2 },
            new object[] { (byte)4 },
            new object[] { (byte)8 },
            new object[] { (byte)16 },
            new object[] { (byte)32 },
            new object[] { (byte)64 },
            new object[] { (byte)128 },
            new object[] { (byte)255 },
        };

        private static IEnumerable<object[]> ShortDynamicData => new List<object[]>
        {
            new object[] { short.MinValue },
            new object[] { (short)0 },
            new object[] { (short)100 },
            new object[] { short.MaxValue },
        };
        private static IEnumerable<object[]> NullableShortDynamicData => new List<object[]>
        {
            new object[] { null },
            new object[] { short.MinValue },
            new object[] { (short)0 },
            new object[] { (short)100 },
            new object[] { short.MaxValue },
        };

        private static IEnumerable<object[]> NullableIntDynamicData => new List<object[]>
        {
            new object[] { null },
            new object[] { int.MinValue },
            new object[] { -10 },
            new object[] { 100 },
            new object[] { int.MaxValue },
        };
        private static IEnumerable<object[]> IntDynamicData => new List<object[]>
        {
            new object[] { int.MinValue },
            new object[] { -10 },
            new object[] { 100 },
            new object[] { int.MaxValue },
        };

        private static IEnumerable<object[]> NullableLongDynamicData => new List<object[]>
        {
            new object[] { null },
            new object[] { long.MinValue },
            new object[] { -10L },
            new object[] { 100L },
            new object[] { long.MaxValue },
        };
        private static IEnumerable<object[]> LongDynamicData => new List<object[]>
        {
            new object[] { long.MinValue },
            new object[] { -10L },
            new object[] { 100L },
            new object[] { long.MaxValue },
        };

        private static IEnumerable<object[]> DateTimeDynamicData => new List<object[]>
        {
            new object[] { DateTime.MinValue },
            new object[] { DateTime.Now },
            new object[] { DateTime.UtcNow },
            new object[] { DateTime.MaxValue },
        };
        private static IEnumerable<object[]> NullableDateTimeDynamicData => new List<object[]>
        {
            new object[] { null },
            new object[] { DateTime.MinValue },
            new object[] { DateTime.Now },
            new object[] { DateTime.UtcNow },
            new object[] { DateTime.MaxValue },
        };

        private static IEnumerable<object[]> DateTimeOffsetDynamicData => new List<object[]>
        {
            new object[] { DateTimeOffset.MinValue },
            new object[] { DateTimeOffset.Now },
            new object[] { DateTimeOffset.UtcNow },
            new object[] { DateTimeOffset.MaxValue },
        };
        private static IEnumerable<object[]> NullableDateTimeOffsetDynamicData => new List<object[]>
        {
            new object[] { null },
            new object[] { DateTimeOffset.MinValue },
            new object[] { DateTimeOffset.Now },
            new object[] { DateTimeOffset.UtcNow },
            new object[] { DateTimeOffset.MaxValue },
        };

        private static IEnumerable<object[]> DecimalDynamicData => new List<object[]>
        {
            new object[] { decimal.MinValue },
            new object[] { decimal.MinusOne },
            new object[] { decimal.Zero },
            new object[] { decimal.One },
            new object[] { 3e8m },
            new object[] { decimal.MaxValue },
        };
        private static IEnumerable<object[]> NullableDecimalDynamicData => new List<object[]>
        {
            new object[] { null },
            new object[] { decimal.MinValue },
            new object[] { decimal.MinusOne },
            new object[] { decimal.Zero },
            new object[] { decimal.One },
            new object[] { 3e8m },
            new object[] { decimal.MaxValue },
        };

        private static IEnumerable<object[]> DoubleDynamicData => new List<object[]>
        {
            new object[] { double.MinValue },
            new object[] { double.Epsilon },
            new object[] { double.PositiveInfinity },
            new object[] { double.NegativeInfinity },
            new object[] { double.NaN },
            new object[] { 3e8 },
            new object[] { double.MaxValue },
        };
        private static IEnumerable<object[]> NullableDoubleDynamicData => new List<object[]>
        {
            new object[] { null },
            new object[] { double.MinValue },
            new object[] { double.Epsilon },
            new object[] { double.PositiveInfinity },
            new object[] { double.NegativeInfinity },
            new object[] { double.NaN },
            new object[] { 3e8 },
            new object[] { double.MaxValue },
        };

        #endregion

        private delegate void OtherEntityAssignDelegate<TValueType>(TValueType valueType, OtherEntity entity)
            where TValueType : Value;
        private delegate void AddOtherEntityValueDelegate(OtherEntity entity, ValueEntity valueEntity);

        private OtherEntity TestOtherEntity = new OtherEntity();
        private ParameterExpression OtherEntityParameter = Expression.Parameter(typeof(OtherEntity), nameof(OtherEntityParameter));
        private List<ValueModel> OtherEntityModels = null;
        private ValueEntity TestValueEntity = new ValueEntity();
        private ParameterExpression ValueEntityParameter = Expression.Parameter(typeof(ValueEntity), nameof(ValueEntityParameter));

        private Mock<IEntityModel> MockEntityModel = new Mock<IEntityModel>();

        private ValueExpressionBuilder TestObject = new ValueExpressionBuilder();

        private PropertyInfo OtherEntityProperty(string propertyName) => typeof(OtherEntity).GetProperty(propertyName);

        [TestInitialize]
        public void Init()
        {
            OtherEntityModels = typeof(OtherEntity)
                .GetProperties()
                .Where(property => property.Name != nameof(OtherEntity.Keys))
                .OrderBy(property => property.Name)
                .Select(property => new ValueModel { Name = property.Name, Property = property })
                .ToList();

            MockEntityModel
                .Setup(model => model.EntityType)
                .Returns(typeof(OtherEntity));
            MockEntityModel
                .Setup(model => model.NonIdentities)
                .Returns(OtherEntityModels);
        }

        [TestMethod]
        public void ValueEntityDelegateReturnsEmptyValueEntity()
        {
            OtherEntityModels.Clear();
            ValueEntityDelegate valueEntityDelegate = TestObject.BuildValueEntityDelegate(MockEntityModel.Object);

            ValueEntity actual = valueEntityDelegate(TestOtherEntity);

            Assert.AreEqual(0, actual.Values.Count);
        }

        [TestMethod]
        public void ValueEntityDelegatePopulatesValues()
        {
            ValueEntityDelegate valueEntityDelegate = TestObject.BuildValueEntityDelegate(MockEntityModel.Object);

            ValueEntity actual = valueEntityDelegate(TestOtherEntity);

            Assert.AreEqual(20, actual.Values.Count);
            Assert.AreEqual(TestOtherEntity.BoolValue, actual.Values[0].Content);
            Assert.AreEqual(TestOtherEntity.ByteValue, actual.Values[1].Content);
            Assert.AreEqual(TestOtherEntity.DateTimeOffsetValue, actual.Values[2].Content);
            Assert.AreEqual(TestOtherEntity.DateTimeValue, actual.Values[3].Content);
            Assert.AreEqual(TestOtherEntity.DecimalValue, actual.Values[4].Content);
            Assert.AreEqual(TestOtherEntity.DoubleValue, actual.Values[5].Content);
            Assert.AreEqual(TestOtherEntity.ID, actual.Values[6].Content);
            Assert.AreEqual(TestOtherEntity.IntValue, actual.Values[7].Content);
            Assert.AreEqual(TestOtherEntity.LongValue, actual.Values[8].Content);
            Assert.AreEqual(TestOtherEntity.Name, actual.Values[9].Content);
            Assert.AreEqual(TestOtherEntity.NullableBoolValue, actual.Values[10].Content);
            Assert.AreEqual(TestOtherEntity.NullableByteValue, actual.Values[11].Content);
            Assert.AreEqual(TestOtherEntity.NullableDateTimeOffsetValue, actual.Values[12].Content);
            Assert.AreEqual(TestOtherEntity.NullableDateTimeValue, actual.Values[13].Content);
            Assert.AreEqual(TestOtherEntity.NullableDecimalValue, actual.Values[14].Content);
            Assert.AreEqual(TestOtherEntity.NullableDoubleValue, actual.Values[15].Content);
            Assert.AreEqual(TestOtherEntity.NullableIntValue, actual.Values[16].Content);
            Assert.AreEqual(TestOtherEntity.NullableLongValue, actual.Values[17].Content);
            Assert.AreEqual(TestOtherEntity.NullableShortValue, actual.Values[18].Content);
            Assert.AreEqual(TestOtherEntity.ShortValue, actual.Values[19].Content);
        }

        private void TestAddValueExpression(string propertyName, object expectedValue)
        {
            PropertyInfo testProperty = OtherEntityProperty(propertyName);
            Expression addValueExpression = TestObject.AddValueExpression(testProperty, OtherEntityParameter, ValueEntityParameter);
            AddOtherEntityValueDelegate addValueDelegate = Expression
                .Lambda<AddOtherEntityValueDelegate>(addValueExpression, new ParameterExpression[] { OtherEntityParameter, ValueEntityParameter })
                .Compile();
            addValueDelegate(TestOtherEntity, TestValueEntity);

            Assert.AreEqual(1, TestValueEntity.Values.Count);
            Value actual = TestValueEntity.Values.First();
            Assert.AreEqual(propertyName, actual.Name);
            Assert.AreEqual(expectedValue, actual.Content);
        }

        [DataTestMethod]
        [DynamicData(nameof(StringDynamicData))]
        public void AddStringValueExpressionBehavesProperly(string testString)
        {
            TestOtherEntity.Name = testString;

            TestAddValueExpression(nameof(OtherEntity.Name), testString);
        }

        [DataTestMethod]
        [DynamicData(nameof(BoolDynamicData))]
        public void AddBoolValueExpressionBehavesProperly(bool testBool)
        {
            TestOtherEntity.BoolValue = testBool;

            TestAddValueExpression(nameof(OtherEntity.BoolValue), testBool);
        }

        [DataTestMethod]
        [DynamicData(nameof(NullableBoolDynamicData))]
        public void AddNullableBoolValueExpressionBehavesProperly(bool? testBool)
        {
            TestOtherEntity.NullableBoolValue = testBool;

            TestAddValueExpression(nameof(OtherEntity.NullableBoolValue), testBool);
        }

        [DataTestMethod]
        [DynamicData(nameof(ByteDynamicData))]
        public void AddByteValueExpressionBehavesProperly(byte testByte)
        {
            TestOtherEntity.ByteValue = testByte;

            TestAddValueExpression(nameof(OtherEntity.ByteValue), testByte);
        }

        [DataTestMethod]
        [DynamicData(nameof(NullableByteDynamicData))]
        public void AddNullableByteValueExpressionBehavesProperly(byte? testByte)
        {
            TestOtherEntity.NullableByteValue = testByte;

            TestAddValueExpression(nameof(OtherEntity.NullableByteValue), testByte);
        }

        [DataTestMethod]
        [DynamicData(nameof(ShortDynamicData))]
        public void AddShortValueExpressionBehavesProperly(short testShort)
        {
            TestOtherEntity.ShortValue = testShort;

            TestAddValueExpression(nameof(OtherEntity.ShortValue), testShort);
        }

        [DataTestMethod]
        [DynamicData(nameof(NullableShortDynamicData))]
        public void AddNullableShortValueExpressionBehavesProperly(short? testShort)
        {
            TestOtherEntity.NullableShortValue = testShort;

            TestAddValueExpression(nameof(OtherEntity.NullableShortValue), testShort);
        }

        [DataTestMethod]
        [DynamicData(nameof(IntDynamicData))]
        public void AddIntValueExpressionBehavesProperly(int testInt)
        {
            TestOtherEntity.IntValue = testInt;

            TestAddValueExpression(nameof(OtherEntity.IntValue), testInt);
        }

        [DataTestMethod]
        [DynamicData(nameof(NullableIntDynamicData))]
        public void AddNullableIntValueExpressionBehavesProperly(int? testInt)
        {
            TestOtherEntity.NullableIntValue = testInt;

            TestAddValueExpression(nameof(OtherEntity.NullableIntValue), testInt);
        }

        [DataTestMethod]
        [DynamicData(nameof(LongDynamicData))]
        public void AddLongValueExpressionBehavesProperly(long testLong)
        {
            TestOtherEntity.LongValue = testLong;

            TestAddValueExpression(nameof(OtherEntity.LongValue), testLong);
        }

        [DataTestMethod]
        [DynamicData(nameof(NullableLongDynamicData))]
        public void AddNullableLongValueExpressionBehavesProperly(long? testLong)
        {
            TestOtherEntity.NullableLongValue = testLong;

            TestAddValueExpression(nameof(OtherEntity.NullableLongValue), testLong);
        }

        [DataTestMethod]
        [DynamicData(nameof(DateTimeDynamicData))]
        public void AddDateTimeValueExpressionBehavesProperly(DateTime testDateTime)
        {
            TestOtherEntity.DateTimeValue = testDateTime;

            TestAddValueExpression(nameof(OtherEntity.DateTimeValue), testDateTime);
        }

        [DataTestMethod]
        [DynamicData(nameof(NullableDateTimeDynamicData))]
        public void AddNullableDateTimeValueExpressionBehavesProperly(DateTime? testDateTime)
        {
            TestOtherEntity.NullableDateTimeValue = testDateTime;

            TestAddValueExpression(nameof(OtherEntity.NullableDateTimeValue), testDateTime);
        }

        [DataTestMethod]
        [DynamicData(nameof(DateTimeOffsetDynamicData))]
        public void AddDateTimeOffsetValueExpressionBehavesProperly(DateTimeOffset testDateTimeOffset)
        {
            TestOtherEntity.DateTimeOffsetValue = testDateTimeOffset;

            TestAddValueExpression(nameof(OtherEntity.DateTimeOffsetValue), testDateTimeOffset);
        }

        [DataTestMethod]
        [DynamicData(nameof(NullableDateTimeOffsetDynamicData))]
        public void AddNullableDateTimeOffsetValueExpressionBehavesProperly(DateTimeOffset? testDateTimeOffset)
        {
            TestOtherEntity.NullableDateTimeOffsetValue = testDateTimeOffset;

            TestAddValueExpression(nameof(OtherEntity.NullableDateTimeOffsetValue), testDateTimeOffset);
        }

        [DataTestMethod]
        [DynamicData(nameof(DecimalDynamicData))]
        public void AddDecimalValueExpressionBehavesProperly(decimal testDecimal)
        {
            TestOtherEntity.DecimalValue = testDecimal;

            TestAddValueExpression(nameof(OtherEntity.DecimalValue), testDecimal);
        }

        [DataTestMethod]
        [DynamicData(nameof(NullableDecimalDynamicData))]
        public void AddNullableDecimalValueExpressionBehavesProperly(decimal? testDecimal)
        {
            TestOtherEntity.NullableDecimalValue = testDecimal;

            TestAddValueExpression(nameof(OtherEntity.NullableDecimalValue), testDecimal);
        }

        [DataTestMethod]
        [DynamicData(nameof(DoubleDynamicData))]
        public void AddDoubleValueExpressionBehavesProperly(double testDouble)
        {
            TestOtherEntity.DoubleValue = testDouble;

            TestAddValueExpression(nameof(OtherEntity.DoubleValue), testDouble);
        }

        [DataTestMethod]
        [DynamicData(nameof(NullableDoubleDynamicData))]
        public void AddNullableDoubleExpressionBehavesProperly(double? testDouble)
        {
            TestOtherEntity.NullableDoubleValue = testDouble;

            TestAddValueExpression(nameof(OtherEntity.NullableDoubleValue), testDouble);
        }

        [DataTestMethod]
        [DataRow(typeof(string), typeof(StringValue))]
        [DataRow(typeof(bool), typeof(BoolValue))]
        [DataRow(typeof(bool?), typeof(NullableBoolValue))]
        [DataRow(typeof(byte), typeof(ByteValue))]
        [DataRow(typeof(byte?), typeof(NullableByteValue))]
        [DataRow(typeof(short), typeof(ShortValue))]
        [DataRow(typeof(short?), typeof(NullableShortValue))]
        [DataRow(typeof(int), typeof(IntValue))]
        [DataRow(typeof(int?), typeof(NullableIntValue))]
        [DataRow(typeof(long), typeof(LongValue))]
        [DataRow(typeof(long?), typeof(NullableLongValue))]
        [DataRow(typeof(DateTime), typeof(DateTimeValue))]
        [DataRow(typeof(DateTime?), typeof(NullableDateTimeValue))]
        [DataRow(typeof(DateTimeOffset), typeof(DateTimeOffsetValue))]
        [DataRow(typeof(DateTimeOffset?), typeof(NullableDateTimeOffsetValue))]
        [DataRow(typeof(decimal), typeof(DecimalValue))]
        [DataRow(typeof(decimal?), typeof(NullableDecimalValue))]
        [DataRow(typeof(double), typeof(DoubleValue))]
        [DataRow(typeof(double?), typeof(NullableDoubleValue))]
        public void RetrievesExpectedType(Type testValueType, Type expectedType)
        {
            Type actual = TestObject.ValueType(testValueType);

            Assert.AreEqual(expectedType, actual);
        }

        private TValue TestAssignValueDelegate<TValue>(PropertyInfo otherEntityProperty)
            where TValue : Value, new()
        {
            ParameterExpression testValueParameter = Expression.Parameter(typeof(TValue), nameof(testValueParameter));
            TValue testValue = new TValue();

            AssignValueDelegate assignValueDelegate = TestObject.AssignValue(typeof(TValue));
            Expression assignValueExpression = assignValueDelegate(testValueParameter, otherEntityProperty, OtherEntityParameter);
            OtherEntityAssignDelegate<TValue> actualDelegate = Expression
                .Lambda<OtherEntityAssignDelegate<TValue>>(assignValueExpression, new ParameterExpression[] { testValueParameter, OtherEntityParameter })
                .Compile();
            actualDelegate(testValue, TestOtherEntity);

            return testValue;
        }

        [DataTestMethod]
        [DynamicData(nameof(StringDynamicData))]
        public void AssignStringExpressionBehavesProperly(string testString)
        {
            TestOtherEntity.Name = testString;

            StringValue actual = TestAssignValueDelegate<StringValue>(typeof(OtherEntity).GetProperty(nameof(TestOtherEntity.Name)));

            Assert.AreEqual(testString, actual.String);
        }

        [DataTestMethod]
        [DynamicData(nameof(BoolDynamicData))]
        public void AssignBoolExpressionBehavesProperly(bool testBool)
        {
            TestOtherEntity.BoolValue = testBool;

            BoolValue actual = TestAssignValueDelegate<BoolValue>(typeof(OtherEntity).GetProperty(nameof(TestOtherEntity.BoolValue)));

            Assert.AreEqual(testBool, actual.Bool);
        }

        [DataTestMethod]
        [DynamicData(nameof(NullableBoolDynamicData))]
        public void AssignNullableBoolExpressionBehavesProperly(bool? testBool)
        {
            TestOtherEntity.NullableBoolValue = testBool;

            NullableBoolValue actual = TestAssignValueDelegate<NullableBoolValue>(typeof(OtherEntity).GetProperty(nameof(TestOtherEntity.NullableBoolValue)));

            Assert.AreEqual(testBool, actual.Bool);
        }

        [DataTestMethod]
        [DynamicData(nameof(IntDynamicData))]
        public void AssignIntExpressionBehavesProperly(int testInt)
        {
            TestOtherEntity.IntValue = testInt;

            IntValue actual = TestAssignValueDelegate<IntValue>(typeof(OtherEntity).GetProperty(nameof(TestOtherEntity.IntValue)));

            Assert.AreEqual(testInt, actual.Int);
        }

        [DataTestMethod]
        [DynamicData(nameof(NullableIntDynamicData))]
        public void AssignNullableIntExpressionBehavesProperly(int? testInt)
        {
            TestOtherEntity.NullableIntValue = testInt;

            NullableIntValue actual = TestAssignValueDelegate<NullableIntValue>(typeof(OtherEntity).GetProperty(nameof(TestOtherEntity.NullableIntValue)));

            Assert.AreEqual(testInt, actual.Int);
        }

        [DataTestMethod]
        [DynamicData(nameof(DateTimeDynamicData))]
        public void AssignDateTimeExpressionBehavesProperly(DateTime testDateTime)
        {
            TestOtherEntity.DateTimeValue = testDateTime;

            DateTimeValue actual = TestAssignValueDelegate<DateTimeValue>(typeof(OtherEntity).GetProperty(nameof(TestOtherEntity.DateTimeValue)));

            Assert.AreEqual(testDateTime, actual.DateTime);
        }

        [DataTestMethod]
        [DynamicData(nameof(NullableDateTimeDynamicData))]
        public void AssignNullableDateTimeExpressionBehavesProperly(DateTime? testDateTime)
        {
            TestOtherEntity.NullableDateTimeValue = testDateTime;

            NullableDateTimeValue actual = TestAssignValueDelegate<NullableDateTimeValue>(typeof(OtherEntity).GetProperty(nameof(TestOtherEntity.NullableDateTimeValue)));

            Assert.AreEqual(testDateTime, actual.DateTime);
        }

        [DataTestMethod]
        [DynamicData(nameof(DateTimeOffsetDynamicData))]
        public void AssignDateTimeOffsetExpressionBehavesProperly(DateTimeOffset testDateTimeOffset)
        {
            TestOtherEntity.DateTimeOffsetValue = testDateTimeOffset;

            DateTimeOffsetValue actual = TestAssignValueDelegate<DateTimeOffsetValue>(typeof(OtherEntity).GetProperty(nameof(TestOtherEntity.DateTimeOffsetValue)));

            Assert.AreEqual(testDateTimeOffset, actual.DateTimeOffset);
        }

        [DataTestMethod]
        [DynamicData(nameof(NullableDateTimeOffsetDynamicData))]
        public void AssignNullableDateTimeOffsetExpressionBehavesProperly(DateTimeOffset? testDateTimeOffset)
        {
            TestOtherEntity.NullableDateTimeOffsetValue = testDateTimeOffset;

            NullableDateTimeOffsetValue actual = TestAssignValueDelegate<NullableDateTimeOffsetValue>(typeof(OtherEntity).GetProperty(nameof(TestOtherEntity.NullableDateTimeOffsetValue)));

            Assert.AreEqual(testDateTimeOffset, actual.DateTimeOffset);
        }

        [DataTestMethod]
        [DynamicData(nameof(DecimalDynamicData))]
        public void AssignDecimalExpressionBehavesProperly(decimal testDecimal)
        {
            TestOtherEntity.DecimalValue = testDecimal;

            DecimalValue actual = TestAssignValueDelegate<DecimalValue>(typeof(OtherEntity).GetProperty(nameof(TestOtherEntity.DecimalValue)));

            Assert.AreEqual(testDecimal, actual.Decimal);
        }

        [DataTestMethod]
        [DynamicData(nameof(NullableDecimalDynamicData))]
        public void AssignNullableDecimalExpressionBehavesProperly(decimal? testDecimal)
        {
            TestOtherEntity.NullableDecimalValue = testDecimal;

            NullableDecimalValue actual = TestAssignValueDelegate<NullableDecimalValue>(typeof(OtherEntity).GetProperty(nameof(TestOtherEntity.NullableDecimalValue)));

            Assert.AreEqual(testDecimal, actual.Decimal);
        }

        [DataTestMethod]
        [DynamicData(nameof(DoubleDynamicData))]
        public void AssignDoubleExpressionBehavesProperly(double testDouble)
        {
            TestOtherEntity.DoubleValue = testDouble;

            DoubleValue actual = TestAssignValueDelegate<DoubleValue>(typeof(OtherEntity).GetProperty(nameof(TestOtherEntity.DoubleValue)));

            Assert.AreEqual(testDouble, actual.Double);
        }

        [DataTestMethod]
        [DynamicData(nameof(NullableDoubleDynamicData))]
        public void AssignNullableDoubleExpressionBehavesProperly(double? testDouble)
        {
            TestOtherEntity.NullableDoubleValue = testDouble;

            NullableDoubleValue actual = TestAssignValueDelegate<NullableDoubleValue>(typeof(OtherEntity).GetProperty(nameof(TestOtherEntity.NullableDoubleValue)));

            Assert.AreEqual(testDouble, actual.Double);
        }

        [TestMethod]
        [ExpectedException(typeof(ModelException))]
        public void InvalidValueTypeTypeThrows()
        {
            TestObject.ValueType(typeof(ValueExpressionBuilderTest));
        }

        [TestMethod]
        [ExpectedException(typeof(ModelException))]
        public void InvalidAssignValueTypeThrows()
        {
            TestObject.AssignValue(typeof(ValueExpressionBuilderTest));
        }
    }
}
