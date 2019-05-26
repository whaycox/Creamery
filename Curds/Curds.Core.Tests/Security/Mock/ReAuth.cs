namespace Curds.Security.Mock
{
    public class ReAuth : Domain.ReAuth
    {
        public static Domain.ReAuth One => new ReAuth(Mock.User.One.ID);
        public static Domain.ReAuth Two => new ReAuth(Mock.User.Two.ID);
        public static Domain.ReAuth Three => new ReAuth(Mock.User.Three.ID);

        public ReAuth(int userID)
        {
            DeviceIdentifier = nameof(DeviceIdentifier);
            Series = nameof(Series);
            Token = nameof(Token);
            UserID = userID;
        }
    }
}
