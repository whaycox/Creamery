namespace Curds.Security.Mock
{
    public class Authentication : Domain.Authentication
    {
        public Authentication(int userID)
        {
            Session = new Session(userID);
            ReAuthentication = new ReAuth(userID);
        }
    }
}
