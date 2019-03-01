using System;
using System.Collections.Generic;
using System.Text;

namespace Gouda.Domain.Check
{
    using Security;

    public class DefinitionRegistration : UserRegistration
    {
        public int DefinitionID { get; set; }
        public Definition Definition { get; set; }
    }
}
