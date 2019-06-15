using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Gouda.Communication.Tests
{
    using Abstraction;
    using Domain;
    using Enumerations;

    [TestClass]
    public class Error : Template.ICommunicableObject<Domain.Error>
    {
        private Domain.Error _obj = null;
        protected override Domain.Error TestObject => _obj;

        [TestInitialize]
        public void BuildObj()
        {
            _obj = new Domain.Error(new Exception(nameof(Exception)));
        }

        protected override int ExpectedByteLength => 35;
        protected override string ExpectedShaHash => "gOlam+dDp6T9ZK4H0R0xG7PYIPc=";
        protected override CommunicableType ExpectedType => CommunicableType.Error;
        protected override IParser Parser => new ErrorParser();
        protected override void VerifyParsedObject(Domain.Error parsed)
        {
            Assert.AreEqual("System.Exception: Exception", parsed.ExceptionText);
        }
    }
}
