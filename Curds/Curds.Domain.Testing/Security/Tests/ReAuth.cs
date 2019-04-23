using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Curds.Domain.Security.Tests
{
    [TestClass]
    public class ReAuth : SecureObjectTemplate
    {
        [TestMethod]
        public void SeriesIsBase64()
        {
            VerifyBase64(Security.ReAuth.NewSeries, Security.ReAuth.SeriesLengthInBytes);
        }

        [TestMethod]
        public void DifferentSeriesEachTime()
        {
            Assert.AreNotEqual(Security.ReAuth.NewSeries, Security.ReAuth.NewSeries);
        }

        [TestMethod]
        public void TokenIsBase64()
        {
            VerifyBase64(Security.ReAuth.NewToken, Security.ReAuth.TokenLengthInBytes);
        }

        [TestMethod]
        public void DifferentTokenEachTime()
        {
            Assert.AreNotEqual(Security.ReAuth.NewToken, Security.ReAuth.NewToken);
        }
    }
}
