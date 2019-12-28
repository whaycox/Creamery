using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Gouda.Application.Commands.AddCheck.Template
{
    using Domain;

    public abstract class AddCheckTemplate
    {
        protected AddCheckCommand TestCommand = new AddCheckCommand();
        protected int TestSatelliteID = 10;
        protected string TestName = nameof(TestName);
        protected Guid TestCheckID = Guid.NewGuid();

        [TestInitialize]
        public void SetupCommand()
        {
            TestCommand.SatelliteID = TestSatelliteID;
            TestCommand.Name = TestName;
            TestCommand.CheckID = TestCheckID;
        }
    }
}
