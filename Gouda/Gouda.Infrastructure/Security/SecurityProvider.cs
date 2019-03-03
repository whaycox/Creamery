using Curds.Application.DateTimes;
using Gouda.Application.Persistence;
using Gouda.Application.Security;
using Gouda.Domain.Security;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Gouda.Infrastructure.Security
{
    public class SecurityProvider : ISecurity
    {
        private const string FormatString = "X2";
        private const int IterationCount = 10000;

        private const int SaltLengthInBits = 128;
        private int SaltLengthInBytes => SaltLengthInBits / 8;

        private const int PasswordLengthInBits = 256;
        private int PasswordLengthInBytes => PasswordLengthInBits / 8;

        private const int SessionIdentifierLengthInBits = 512;
        private int SessionIdentifierLengthInBytes => SessionIdentifierLengthInBits / 8;

        private static TimeSpan SessionExpiration = TimeSpan.FromHours(4);

        public IDateTime Time { get; }
        public IPersistence Persistence { get; }

        public SecurityProvider(IDateTime time, IPersistence persistence)
        {
            Time = time;
            Persistence = persistence;
        }

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

        public string GenerateSessionIdentifier()
        {
            byte[] generated = new byte[SessionIdentifierLengthInBytes];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(generated);
            return FormatBytes(generated);
        }

        public async Task<Session> GenerateSession(string deviceIdentifier, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(deviceIdentifier))
                throw new InvalidOperationException($"Must provide a valid {nameof(deviceIdentifier)}");

            try
            {
                User user = await Persistence.FindByEmail(email);
                string encryptedPassword = EncryptPassword(user.Salt, password);
                if (!user.Password.Equals(encryptedPassword))
                    throw new Exception();

                DateTimeOffset createdTime = Time.Fetch;
                Session newSession = new Session
                {
                    Identifier = GenerateSessionIdentifier(),
                    Created = createdTime,
                    DeviceIdentifier = deviceIdentifier,
                    UserID = user.ID,
                    WouldExpire = createdTime.Add(SessionExpiration),
                };
                await Persistence.AddSession(newSession);
                return newSession;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Invalid authentication information provided");
            }
        }
    }
}
