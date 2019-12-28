using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace Gouda.Application.Queries.DisplaySatellite.Tests
{
    using Implementation;
    using Template;

    [TestClass]
    public class DisplaySatelliteValidatorTest : DisplaySatelliteTemplate
    {
        private DisplaySatelliteValidator TestObject = new DisplaySatelliteValidator();

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
