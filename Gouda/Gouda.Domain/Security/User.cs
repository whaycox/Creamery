using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;
using Curds.Domain;

namespace Gouda.Domain.Security
{
    public class User : NamedEntity
    {
        public string Email { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }
    }
}
