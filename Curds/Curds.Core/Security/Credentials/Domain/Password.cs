namespace Curds.Security.Credentials.Domain
{
    public class Password : BaseCredentials
    {
        public string Email { get; set; }
        public string RawPassword { get; set; }
    }
}
