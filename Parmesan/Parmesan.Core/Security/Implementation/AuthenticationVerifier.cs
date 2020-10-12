using System;

namespace Parmesan.Security.Implementation
{
    using Abstraction;
    using Parmesan.Domain;

    internal class AuthenticationVerifier : IAuthenticationVerifier
    {
        private IPasswordHasher PasswordHasher { get; }

        public AuthenticationVerifier(IPasswordHasher passwordHasher)
        {
            PasswordHasher = passwordHasher;
        }

        public bool VerifyPassword(PasswordAuthentication storedPassword, string suppliedPassword)
        {
            string computedHash = PasswordHasher.Compute(suppliedPassword, storedPassword.Salt);
            return computedHash.Equals(storedPassword.Hash, StringComparison.InvariantCulture);
        }
    }
}
