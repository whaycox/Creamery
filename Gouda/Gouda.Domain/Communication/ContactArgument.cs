using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;

namespace Gouda.Domain.Communication
{
    public class ContactArgument : NameValueEntity
    {
        public int ContactID { get; set; }
    }
}
