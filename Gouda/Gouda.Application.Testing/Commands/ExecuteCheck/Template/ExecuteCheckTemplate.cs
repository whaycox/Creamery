using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Gouda.Application.Commands.ExecuteCheck.Template
{
    using Domain;
    using Implementation;
    using Gouda.Domain;

    public abstract class ExecuteCheckTemplate
    {
        protected ExecuteCheckCommand TestCommand = new ExecuteCheckCommand();
        protected CheckDefinition TestCheck = new CheckDefinition();
        protected Satellite TestSatellite = new Satellite();

        [TestInitialize]
        public void SetupCommand()
        {
            TestCheck.Satellite = TestSatellite;
            TestCommand.Check = TestCheck;
        }
    }
}
