using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Security.Tests
{
    [TestClass]
    public class ReAuth : Template.SecureObject
    {
        [TestMethod]
        public void SeriesIsBase64()
        {
            VerifyBase64(Domain.ReAuth.NewSeries, Domain.ReAuth.SeriesLengthInBytes);
        }

        [TestMethod]
        public void DifferentSeriesEachTime()
        {
            Assert.AreNotEqual(Domain.ReAuth.NewSeries, Domain.ReAuth.NewSeries);
        }

        [TestMethod]
        public void TokenIsBase64()
        {
            VerifyBase64(Domain.ReAuth.NewToken, Domain.ReAuth.TokenLengthInBytes);
        }

        [TestMethod]
        public void DifferentTokenEachTime()
        {
            Assert.AreNotEqual(Domain.ReAuth.NewToken, Domain.ReAuth.NewToken);
        }
    }
}
