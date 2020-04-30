using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace Curds.Persistence.Query.Template
{
    using Model.Domain;

    public class SqlQueryTemplate
    {
        //protected Table TestTable = new Table();
        //protected string TestSchema = nameof(TestSchema);
        //protected string TestTableName = nameof(TestTableName);
        //protected Column TestColumnOne = new Column { Name = nameof(TestColumnOne) };
        //protected Column TestColumnTwo = new Column { Name = nameof(TestColumnTwo) };

        [TestInitialize]
        public void SetupSqlQueryTemplate()
        {
            Assert.Fail();
            //TestTable.Schema = TestSchema;
            //TestTable.Name = TestTableName;
        }
    }
}
