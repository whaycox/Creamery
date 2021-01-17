using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Curds.Expressions.Tests
{
    using Abstraction;
    using Curds.Domain;
    using Curds.Template;

    [TestClass]
    public class ExpressionParserIntegrationTest : IntegrationTemplate
    {
        [TestMethod]
        public void ParsePropertyExpressionReturnsExpected()
        {
            RegisterServices();
            BuildServiceProvider();

            ParseTestPropertyExpression(testEntity => testEntity.BoolValue, typeof(TestEntity).GetProperty(nameof(TestEntity.BoolValue)));
            ParseTestPropertyExpression(testEntity => testEntity.NullableBoolValue, typeof(TestEntity).GetProperty(nameof(TestEntity.NullableBoolValue)));
            ParseTestPropertyExpression(testEntity => testEntity.ByteValue, typeof(TestEntity).GetProperty(nameof(TestEntity.ByteValue)));
            ParseTestPropertyExpression(testEntity => testEntity.NullableByteValue, typeof(TestEntity).GetProperty(nameof(TestEntity.NullableByteValue)));
            ParseTestPropertyExpression(testEntity => testEntity.ShortValue, typeof(TestEntity).GetProperty(nameof(TestEntity.ShortValue)));
            ParseTestPropertyExpression(testEntity => testEntity.NullableShortValue, typeof(TestEntity).GetProperty(nameof(TestEntity.NullableShortValue)));
            ParseTestPropertyExpression(testEntity => testEntity.IntValue, typeof(TestEntity).GetProperty(nameof(TestEntity.IntValue)));
            ParseTestPropertyExpression(testEntity => testEntity.NullableIntValue, typeof(TestEntity).GetProperty(nameof(TestEntity.NullableIntValue)));
            ParseTestPropertyExpression(testEntity => testEntity.LongValue, typeof(TestEntity).GetProperty(nameof(TestEntity.LongValue)));
            ParseTestPropertyExpression(testEntity => testEntity.NullableLongValue, typeof(TestEntity).GetProperty(nameof(TestEntity.NullableLongValue)));
            ParseTestPropertyExpression(testEntity => testEntity.DateTimeValue, typeof(TestEntity).GetProperty(nameof(TestEntity.DateTimeValue)));
            ParseTestPropertyExpression(testEntity => testEntity.NullableDateTimeValue, typeof(TestEntity).GetProperty(nameof(TestEntity.NullableDateTimeValue)));
            ParseTestPropertyExpression(testEntity => testEntity.DateTimeOffsetValue, typeof(TestEntity).GetProperty(nameof(TestEntity.DateTimeOffsetValue)));
            ParseTestPropertyExpression(testEntity => testEntity.NullableDateTimeOffsetValue, typeof(TestEntity).GetProperty(nameof(TestEntity.NullableDateTimeOffsetValue)));
            ParseTestPropertyExpression(testEntity => testEntity.DecimalValue, typeof(TestEntity).GetProperty(nameof(TestEntity.DecimalValue)));
            ParseTestPropertyExpression(testEntity => testEntity.NullableDecimalValue, typeof(TestEntity).GetProperty(nameof(TestEntity.NullableDecimalValue)));
            ParseTestPropertyExpression(testEntity => testEntity.DoubleValue, typeof(TestEntity).GetProperty(nameof(TestEntity.DoubleValue)));
            ParseTestPropertyExpression(testEntity => testEntity.NullableDoubleValue, typeof(TestEntity).GetProperty(nameof(TestEntity.NullableDoubleValue)));
        }
        private void ParseTestPropertyExpression<TValue>(Expression<Func<TestEntity, TValue>> propertyExpression, PropertyInfo expectedProperty)
        {
            IExpressionParser testObject = TestServiceProvider.GetRequiredService<IExpressionParser>();

            PropertyInfo actual = testObject.ParsePropertyExpression(propertyExpression);

            Assert.AreEqual(expectedProperty, actual);
        }
    }
}
