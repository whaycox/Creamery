using Curds.Persistence.Domain;

namespace Parmesan.Domain
{
    public class ClientRedirectionUri : BaseSimpleEntity
    {
        public int ClientID { get; set; }
        public string RedirectionUri { get; set; }
    }
}
