using System;
using System.Security.Cryptography;

namespace Curds.Domain.Security
{
    using Persistence;

    public class Session : BaseEntity
    {
        private const int SessionIdentifierLengthInBits = 512;
        public static int SessionIdentifierLengthInBytes => SessionIdentifierLengthInBits / 8;

        public static readonly TimeSpan ExpirationDuration = TimeSpan.FromMinutes(5);

        public string Identifier { get; set; }
        public string Series { get; set; }
        public int UserID { get; set; }
        public DateTimeOffset Expiration { get; set; }

        public void IncrementExpiration() => Expiration = Expiration.Add(ExpirationDuration);

        public static string NewSessionID
        {
            get
            {
                byte[] generated = new byte[SessionIdentifierLengthInBytes];
                using (var rng = RandomNumberGenerator.Create())
                    rng.GetBytes(generated);
                return Convert.ToBase64String(generated);
            }
        }
    }
}
