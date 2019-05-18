namespace Curds.Security.Credentials.Mock
{
    public class Password : Domain.Password
    {
        public Password(int userID)
        {
            DeviceIdentifier = Testing.DeviceIdentifier;
            Email = $"{userID}{Testing.Email}";
            RawPassword = Testing.Password;
            RememberMe = true;
        }
    }
}
