using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentValidation;
using System.Threading.Tasks;

namespace Gouda.Application.Queries.DisplaySatellite.Tests
{
    using Implementation;
    using Domain;

    [TestClass]
    public class DisplaySatelliteValidatorTest
    {
        private DisplaySatelliteQuery TestQuery = new DisplaySatelliteQuery();
        private int TestSatelliteID = 7;

        private DisplaySatelliteValidator TestObject = new DisplaySatelliteValidator();

        [TestInitialize]
        public void Init()
        {
            TestQuery.SatelliteID = TestSatelliteID;
        }

        [TestMethod]
        public async Task ValidatesTestQuery()
        {
            await TestObject.Process(TestQuery, default);
        }

        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(0)]
        [ExpectedException(typeof(ValidationException))]
        public async Task ValidatesSatelliteID(int testID)
        {
            TestQuery.SatelliteID = testID;

            await TestObject.Process(TestQuery, default);
        }

    }
}
