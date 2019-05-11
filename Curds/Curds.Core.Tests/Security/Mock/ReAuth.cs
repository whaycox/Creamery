namespace Curds.Security.Mock
{
    public class ReAuth : Domain.ReAuth
    {
        public ReAuth(int userID)
        {
            DeviceIdentifier = nameof(DeviceIdentifier);
            Series = nameof(Series);
            Token = nameof(Token);
            UserID = userID;
        }
    }
}
