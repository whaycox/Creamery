using Curds.Domain.Persistence;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace Curds.Domain.Security
{
    public class User : NamedEntity
    {
        private const int IterationCount = 10000;

        private const int SaltLengthInBits = 128;
        public static int SaltLengthInBytes => SaltLengthInBits / 8;

        private const int PasswordLengthInBits = 256;
        public static int PasswordLengthInBytes => PasswordLengthInBits / 8;

        public string Email { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }

        public static string NewSalt
        {
            get
            {
                byte[] generated = new byte[SaltLengthInBytes];
                using (var rng = RandomNumberGenerator.Create())
                    rng.GetBytes(generated);
                return Convert.ToBase64String(generated);
            }
        }

        public static string EncryptPassword(string salt, string password)
        {
            if (string.IsNullOrWhiteSpace(salt))
                throw new ArgumentNullException(nameof(salt));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            byte[] encryptedPassword = KeyDerivation.Pbkdf2(
                password,
                Convert.FromBase64String(salt),
                KeyDerivationPrf.HMACSHA1,
                IterationCount,
                PasswordLengthInBytes
                );
            return Convert.ToBase64String(encryptedPassword);
        }
    }
}
