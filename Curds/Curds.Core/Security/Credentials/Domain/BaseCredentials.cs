namespace Curds.Security.Credentials.Domain
{
    public abstract class BaseCredentials
    {
        public string DeviceIdentifier { get; set; }
        public bool RememberMe { get; set; }
    }
}
