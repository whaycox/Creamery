namespace Curds.Security.Mock
{
    public class User : Domain.User
    {
        public static Domain.User One => new User(1);
        public static Domain.User Two => new User(2);
        public static Domain.User Three => new User(3);

        public static Domain.User[] Samples => new Domain.User[]
        {
            One,
            Two,
            Three,
        };

        public User(int userID)
        {
            ID = userID;
            Email = $"{userID}{Testing.Email}";
            Salt = Testing.Salt;
            Password = Testing.EncryptedPassword;
        }
    }
}
