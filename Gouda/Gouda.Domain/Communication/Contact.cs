using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;

namespace Gouda.Domain.Communication
{
    public abstract class Contact : NamedEntity, ICronEntity
    {
        public int UserID { get; set; }
        public string CronString { get; set; }
    }
}
