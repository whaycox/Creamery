using System;
using System.Collections.Generic;
using System.Text;
using Gouda.Application.Persistence;
using Gouda.Application.Security;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Linq;

namespace Gouda.Infrastructure.Security
{
    public class SecurityProvider : ISecurity
    {
        private const string FormatString = "X2";
        private const int IterationCount = 10000;

        public IPersistence Persistence { get; set; }

        private const int SaltLengthInBits = 128;
        private int SaltLengthInBytes => SaltLengthInBits / 8;

        private const int PasswordLengthInBits = 256;
        private int PasswordLengthInBytes => PasswordLengthInBits / 8;

        private string FormatBytes(byte[] bytes)
        {
            StringBuilder builder = new StringBuilder();
            foreach (byte single in bytes)
                builder.Append(single.ToString(FormatString));
            return builder.ToString();
        }
        private byte[] DeconstructSalt(string salt)
        {
            List<byte> toReturn = new List<byte>();
            for (int i = 0; i < salt.Length; i += 2)
            {
                string pair = salt.Substring(i, 2);
                toReturn.Add(Convert.ToByte(pair, 16));
            }
            return toReturn.ToArray();
        }

        public string EncryptPassword(string salt, string password)
        {
            if (string.IsNullOrWhiteSpace(salt))
                throw new ArgumentNullException(nameof(salt));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            byte[] encryptedPassword = KeyDerivation.Pbkdf2(
                password,
                DeconstructSalt(salt),
                KeyDerivationPrf.HMACSHA1,
                IterationCount,
                PasswordLengthInBytes
                );
            return FormatBytes(encryptedPassword);
        }

        public string GenerateSalt()
        {
            byte[] generated = new byte[SaltLengthInBytes];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(generated);
            return FormatBytes(generated);
        }
    }
}
