namespace Parmesan.Server.Controllers.Domain
{
    using Security.Domain;

    public class PasswordAuthenticationSubmission : PasswordAuthenticationData
    {
        public string ReturnUrl { get; set; }
    }
}
