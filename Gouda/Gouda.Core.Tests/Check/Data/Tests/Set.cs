using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Check.Data.Domain;
using Gouda.Communication.Abstraction;
using Gouda.Communication.Enumerations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gouda.Check.Data.Tests
{
    using Domain;
    using Communication.Abstraction;
    using Communication.Enumerations;
    using Enumerations;

    [TestClass]
    public class Set : Communication.Template.ICommunicableObject<Domain.Set>
    {
        protected override Domain.Set TestObject { get; } = Mock.Set.Sample;

        protected override int ExpectedByteLength => 95;
        protected override string ExpectedShaHash => "Z6ymd2UoORSNyrpSvaose7l/Nps=";
        protected override CommunicableType ExpectedType => CommunicableType.DataSet;
        protected override IParser Parser => new SetParser();
        protected override void VerifyParsedObject(Domain.Set parsed)
        {
            Assert.AreEqual(TestObject.Data.Count, parsed.Data.Count);
            for (int i = 0; i < TestObject.Data.Count; i++)
                VerifyParsedSeries(TestObject.Data[i], parsed.Data[i]);
        }
        private void VerifyParsedSeries(Series expected, Series actual)
        {
            Assert.AreEqual(expected.SeriesType, actual.SeriesType);
            Assert.AreEqual(expected.Name, actual.Name);
            switch (actual.SeriesType)
            {
                case SeriesType.Int:
                    Domain.IntSeries expectedInt = expected as Domain.IntSeries;
                    Domain.IntSeries actualInt = actual as Domain.IntSeries;
                    Assert.AreEqual(expectedInt.Value, actualInt.Value);
                    break;
                case SeriesType.Long:
                    Domain.LongSeries expectedLong = expected as Domain.LongSeries;
                    Domain.LongSeries actualLong = actual as Domain.LongSeries;
                    Assert.AreEqual(expectedLong.Value, actualLong.Value);
                    break;
                case SeriesType.Decimal:
                    Domain.DecimalSeries expectedDecimal = expected as Domain.DecimalSeries;
                    Domain.DecimalSeries actualDecimal = actual as Domain.DecimalSeries;
                    Assert.AreEqual(expectedDecimal.Value, actualDecimal.Value);
                    break;
                default:
                    throw new NotImplementedException();
            }

        }
    }
}
