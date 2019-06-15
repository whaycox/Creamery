using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gouda.Check.Data.Tests
{
    using Communication.Abstraction;
    using Communication.Enumerations;
    using Domain;

    [TestClass]
    public class LongSeries : Communication.Template.ICommunicableObject<Domain.LongSeries>
    {
        protected override Domain.LongSeries TestObject { get; } = Mock.LongSeries.Sample;

        protected override int ExpectedByteLength => 27;
        protected override string ExpectedShaHash => "rbSQTQtjG3R+ExG2RQ+FmEp8Hik=";
        protected override CommunicableType ExpectedType => CommunicableType.DataSeries;
        protected override IParser Parser => new SeriesParser();
        protected override void VerifyParsedObject(Domain.LongSeries parsed)
        {
            Assert.AreEqual(TestObject.Name, parsed.Name);
            Assert.AreEqual(TestObject.Value, parsed.Value);
        }
    }
}
