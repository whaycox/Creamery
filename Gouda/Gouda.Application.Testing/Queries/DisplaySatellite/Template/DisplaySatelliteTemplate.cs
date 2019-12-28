using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gouda.Application.Queries.DisplaySatellite.Template
{
    using Domain;

    public abstract class DisplaySatelliteTemplate
    {
        protected DisplaySatelliteQuery TestQuery = new DisplaySatelliteQuery();
        protected int TestSatelliteID = 7;

        [TestInitialize]
        public void SetupQuery()
        {
            TestQuery.SatelliteID = TestSatelliteID;
        }
    }
}
