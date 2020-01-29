using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Options;

namespace Curds.Persistence.Tests
{
    using Implementation;
    using Abstraction;
    using Domain;
    using Template;

    [TestClass]
    public class SqlDatabaseTest : SqlTemplate
    {
        private Mock<ISqlConnectionContext> MockConnectionContext = new Mock<ISqlConnectionContext>();

        private SqlDatabase TestObject = null;

        [TestInitialize]
        public void Init()
        {
            TestObject = new SqlDatabase(MockConnectionContext.Object);

            throw new NotImplementedException();
        }
    }
}
