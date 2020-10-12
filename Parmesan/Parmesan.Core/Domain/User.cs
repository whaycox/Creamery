using Curds.Persistence.Domain;
using System;

namespace Parmesan.Domain
{
    public class User : SimpleEntity
    {
        public string UserName { get; set; }
    }
}
