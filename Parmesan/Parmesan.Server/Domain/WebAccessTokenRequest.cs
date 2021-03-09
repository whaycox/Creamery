using Microsoft.AspNetCore.Mvc;

namespace Parmesan.Server.Domain
{
    using Parmesan.Domain;

    public class WebAccessTokenRequest
    {
        [BindProperty(Name = AccessTokenRequest.GrantTypeName)]
        public string GrantType { get; set; }

        [BindProperty(Name = AccessTokenRequest.CodeName)]
        public string Code { get; set; }

        [BindProperty(Name = AccessTokenRequest.ClientIDName)]
        public string ClientID { get; set; }

        [BindProperty(Name = AccessTokenRequest.RedirectUriName)]
        public string RedirectUri { get; set; }
    }
}
