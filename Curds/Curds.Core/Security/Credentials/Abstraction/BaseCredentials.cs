namespace Curds.Security.Credentials.Abstraction
{
    public abstract class BaseCredentials
    {
        public string DeviceIdentifier { get; set; }
        public bool RememberMe { get; set; }
    }
}
