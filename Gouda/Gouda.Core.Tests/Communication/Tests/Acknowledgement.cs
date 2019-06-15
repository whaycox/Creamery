using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Gouda.Communication.Tests
{
    using Abstraction;
    using Domain;
    using Enumerations;

    [TestClass]
    public class Acknowledgement : Template.ICommunicableObject<Domain.Acknowledgement>
    {
        private Domain.Acknowledgement _obj = null;
        protected override Domain.Acknowledgement TestObject => _obj;

        [TestInitialize]
        public void BuildObj()
        {
            MockTime.SetPointInTime(new DateTimeOffset(2001, 1, 1, 1, 1, 1, TimeSpan.FromMinutes(23)));
            _obj = new Domain.Acknowledgement(MockTime.Fetch);
        }

        protected override int ExpectedByteLength => 20;
        protected override string ExpectedShaHash => "ouDGsLJZ69Okq4lNvfhxkevFddc=";
        protected override CommunicableType ExpectedType => CommunicableType.Acknowledgement;
        protected override IParser Parser => new AcknowledgementParser();
        protected override void VerifyParsedObject(Domain.Acknowledgement parsed)
        {
            Assert.AreEqual(MockTime.Fetch, parsed.Time);
        }
    }
}
