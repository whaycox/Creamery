using System;
using System.Collections.Generic;
using System.Text;

namespace Parmesan.Domain
{
    public class AuthorizationRequest
    {
        public const string ResponseTypeName = "response_type";
        public ResponseType ResponseType { get; set; }

        public const string ClientIDName = "client_id";
        public string ClientID { get; set; }

        public const string RedirectUriName = "redirect_uri";
        public string RedirectUri { get; set; }

        public const string ScopeName = "scope";
        public string Scope { get; set; }

        public const string StateName = "state";
        public string State { get; set; }

        public const string CodeChallengeName = "code_challenge";
        public string CodeChallenge { get; set; }

        public const string CodeChallengeMethodName = "code_challenge_method";
        public CodeChallengeMethod CodeChallengeMethod { get; set; }
        
        public string ResponseMode { get; set; }
        public string Nonce { get; set; }
        public string Display { get; set; }
        public string Prompt { get; set; }
        public string MaxAge { get; set; }
        public string UILocales { get; set; }
        public string IDTokenHint { get; set; }
        public string LoginHint { get; set; }
        public string AcrValues { get; set; }
    }
}
