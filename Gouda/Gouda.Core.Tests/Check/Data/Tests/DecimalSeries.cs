using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Check.Data.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gouda.Check.Data.Tests
{
    using Domain;
    using Communication.Abstraction;
    using Communication.Enumerations;

    [TestClass]
    public class DecimalSeries : Communication.Template.ICommunicableObject<Domain.DecimalSeries>
    {
        protected override Domain.DecimalSeries TestObject { get; } = Mock.DecimalSeries.Sample;

        protected override int ExpectedByteLength => 38;
        protected override string ExpectedShaHash => "WZyDVNPVA/zdZt/H5n6iGsAfD64=";
        protected override CommunicableType ExpectedType => CommunicableType.DataSeries;
        protected override IParser Parser => new SeriesParser();
        protected override void VerifyParsedObject(Domain.DecimalSeries parsed)
        {
            Assert.AreEqual(TestObject.Name, parsed.Name);
            Assert.AreEqual(TestObject.Value, parsed.Value);
        }
    }
}
