using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gouda.Check.Data.Tests
{
    using Communication.Abstraction;
    using Communication.Enumerations;
    using Domain;

    [TestClass]
    public class IntSeries : Communication.Template.ICommunicableObject<Domain.IntSeries>
    {
        protected override Domain.IntSeries TestObject { get; } = Mock.IntSeries.Sample;

        protected override int ExpectedByteLength => 22;
        protected override string ExpectedShaHash => "uJq0Z1zTcMx7O+wRUpdV5RRx/zg=";
        protected override CommunicableType ExpectedType => CommunicableType.DataSeries;
        protected override IParser Parser => new SeriesParser();
        protected override void VerifyParsedObject(Domain.IntSeries parsed)
        {
            Assert.AreEqual(TestObject.Name, parsed.Name);
            Assert.AreEqual(TestObject.Value, parsed.Value);
        }
    }
}
