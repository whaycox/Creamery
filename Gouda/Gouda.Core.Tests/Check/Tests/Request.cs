using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Check.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gouda.Check.Tests
{
    using Domain;
    using Communication.Enumerations;
    using Communication.Abstraction;

    [TestClass]
    public class Request : Communication.Template.ICommunicableObject<Domain.Request>
    {
        private Guid TestGuid = Guid.Parse("7d41284e-01b4-4a9c-8585-08eab4586ef6");

        private Domain.Request _obj = null;
        protected override Domain.Request TestObject => _obj;

        [TestInitialize]
        public void BuildObj()
        {
            _obj = new Domain.Request(TestGuid, TestArguments);
        }
        private Dictionary<string, string> TestArguments => new Dictionary<string, string>
        {
            { nameof(TestArguments), nameof(TestArguments) },
        };

        protected override int ExpectedByteLength => 58;
        protected override string ExpectedShaHash => "rlDtUyOvMQW2lQtDdwIvtFDhudY=";
        protected override CommunicableType ExpectedType => CommunicableType.Request;
        protected override IParser Parser => new RequestParser();
        protected override void VerifyParsedObject(Domain.Request parsed)
        {
            Assert.AreEqual(TestGuid, parsed.ID);
            Assert.AreEqual(TestArguments.Count, parsed.Arguments.Count);
            foreach (var arg in TestArguments)
            {
                Assert.IsTrue(parsed.Arguments.ContainsKey(arg.Key));
                Assert.AreEqual(arg.Value, parsed.Arguments[arg.Key]);
            }
        }


    }
}
