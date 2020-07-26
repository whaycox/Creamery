using Moq;
using System;
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
    }
}
