using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Whey
{
    public static class SetupExtensions
    {
        public static TReturn SetupMock<TMock, TReturn>(this Mock<TMock> mock, Expression<Func<TMock, TReturn>> setupExpression)
            where TMock : class
            where TReturn : class
        {
            TReturn mockToken = Mock.Of<TReturn>();
            mock
                .Setup(setupExpression)
                .Returns(mockToken);
            return mockToken;
        }

        public static List<TReturn> SetupMock<TMock, TReturn>(this Mock<TMock> mock, Expression<Func<TMock, IEnumerable<TReturn>>> setupExpression, int elements)
            where TMock : class
            where TReturn : class
        {
            List<TReturn> mockElements = new List<TReturn>();
            for (int i = 0; i < elements; i++)
                mockElements.Add(Mock.Of<TReturn>());
            mock
                .Setup(setupExpression)
                .Returns(mockElements);
            return mockElements;
        }
    }
}
