using System;
using System.Collections.Generic;
using System.Text;
using Curds.Domain.Persistence;
using Curds.Domain;

namespace Gouda.Domain.Communication
{
    public class UserRegistration : Entity, ICronEntity
    {
        public int DefinitionID { get; set; }
        public int UserID { get; set; }
        public string CronString { get; set; }
    }
}
