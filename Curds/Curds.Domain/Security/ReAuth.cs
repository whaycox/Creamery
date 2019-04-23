using System;
using System.Security.Cryptography;

namespace Curds.Domain.Security
{
    using Persistence;

    public class ReAuth : BaseEntity
    {
        private const int SeriesLengthInBits = 512;
        public static int SeriesLengthInBytes => SeriesLengthInBits / 8;

        private const int TokenLengthInBits = 512;
        public static int TokenLengthInBytes => TokenLengthInBits / 8;

        public string DeviceIdentifier { get; set; }

        public string Series { get; set; }
        public string Token { get; set; }

        public int UserID { get; set; }
        public User User { get; set; }

        public static string NewSeries
        {
            get
            {
                byte[] generated = new byte[SeriesLengthInBytes];
                using (var rng = RandomNumberGenerator.Create())
                    rng.GetBytes(generated);
                return Convert.ToBase64String(generated);
            }
        }

        public static string NewToken
        {
            get
            {
                byte[] generated = new byte[TokenLengthInBytes];
                using (var rng = RandomNumberGenerator.Create())
                    rng.GetBytes(generated);
                return Convert.ToBase64String(generated);
            }
        }
    }
}
