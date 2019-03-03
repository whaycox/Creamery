using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Communication
{
    using Security;

    public class ContactRegistration : UserRegistration
    {
        public int ContactID { get; set; }
        public Contact Contact { get; set; }
    }
}
