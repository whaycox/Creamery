namespace Parmesan.Security.Abstraction
{
    using Parmesan.Domain;

    public interface IAuthenticationVerifier
    {
        bool VerifyPassword(PasswordAuthentication storedPassword, string suppliedPassword);
    }
}
