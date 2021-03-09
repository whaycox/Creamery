namespace Parmesan.Domain
{
    public class AccessTokenRequest
    {
        public const string GrantTypeName = "grant_type";
        public GrantType GrantType { get; set; }

        public const string CodeName = "code";
        public string Code { get; set; }

        public const string ClientIDName = "client_id";
        public string ClientID { get; set; }

        public const string RedirectUriName = "redirect_uri";
        public string RedirectUri { get; set; }
    }
}
