using Curds.Domain.Persistence;
using Curds.Domain;

namespace Gouda.Domain.Communication
{
    public abstract class Contact : NamedEntity, ICronEntity
    {
        public int UserID { get; set; }
        public string CronString { get; set; }
    }
}
