using Whey;

namespace Curds.Security.Mock
{
    public class User : Domain.User
    {
        public User(int userID)
        {
            ID = userID;
            Email = $"{userID}{Testing.Email}";
            Salt = Testing.Salt;
            Password = Testing.EncryptedPassword;
        }
    }
}
