namespace Curds.Security.Credentials.Domain
{
    using Abstraction;

    public class Password : BaseCredentials
    {
        public string Email { get; set; }
        public string RawPassword { get; set; }
    }
}
