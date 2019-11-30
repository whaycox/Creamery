using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Gouda.Application.ViewModels.Satellite.Template
{
    using Gouda.Domain;
    using Enumerations;
    
    public abstract class SatelliteMapperTemplate
    {
        protected Satellite TestSatellite = new Satellite();
        protected int TestSatelliteID = 3;
        protected string TestSatelliteName = nameof(TestSatelliteName);
        protected IPAddress TestSatelliteIP = IPAddress.Parse("7.6.5.4");
        protected SatelliteStatus TestSatelliteStatus = SatelliteStatus.Good;

        [TestInitialize]
        public void Init()
        {
            TestSatellite.ID = TestSatelliteID;
            TestSatellite.Name = TestSatelliteName;
            TestSatellite.IPAddress = TestSatelliteIP;
            TestSatellite.Status = TestSatelliteStatus;
        }
    }
}
