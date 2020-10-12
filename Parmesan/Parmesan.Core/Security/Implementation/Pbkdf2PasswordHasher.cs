using System;
using System.Security.Cryptography;

namespace Parmesan.Security.Implementation
{
    using Abstraction;

    internal class Pbkdf2PasswordHasher : IPasswordHasher
    {
        private const int IterationCount = 2000;
        public const int HashSizeInBytes = 48;

        public string Compute(string password, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            using (Rfc2898DeriveBytes hasher = new Rfc2898DeriveBytes(password, saltBytes) { IterationCount = IterationCount })
                return Convert.ToBase64String(hasher.GetBytes(HashSizeInBytes));
        }
    }
}
