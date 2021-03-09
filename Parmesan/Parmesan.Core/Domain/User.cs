using Curds.Persistence.Domain;
using System;

namespace Parmesan.Domain
{
    public class User : BaseSimpleEntity
    {
        public string UserName { get; set; }
    }
}
