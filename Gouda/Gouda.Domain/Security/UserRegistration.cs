using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;
using Curds.Domain.Security;

namespace Gouda.Domain.Security
{
    public abstract class UserRegistration : Entity, ICronEntity
    {
        public string CronString { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }
    }
}
