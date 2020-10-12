using Curds.Persistence.Domain;

namespace Parmesan.Domain
{
    public class PasswordAuthentication : BaseEntity
    {
        public int UserID { get; set; }
        public string Salt { get; set; }
        public string Hash { get; set; }

        public override object[] Keys => new object[] { UserID };
    }
}
