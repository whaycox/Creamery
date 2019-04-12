using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Curds.Application.Message;
using Curds.Application.Message.Query;

namespace Curds.Domain.Application.Message.Query
{
    public class MockQuery : BaseQuery
    {
        public string Message { get; }

        public MockQuery(string message)
        {
            Message = message;
        }
    }
}
