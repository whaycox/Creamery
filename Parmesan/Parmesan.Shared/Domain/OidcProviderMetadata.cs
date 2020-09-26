using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Parmesan.Domain
{
    public class OidcProviderMetadata
    {
        public const string ConfigurationRoute = "/.well-known/openid-configuration";

        public const string IssuerName = "issuer";
        [JsonPropertyName(IssuerName)]
        public string Issuer { get; set; }

        public const string AuthorizationEndpointName = "authorization_endpoint";
        [JsonPropertyName(AuthorizationEndpointName)]
        public string AuthorizationEndpoint { get; set; }

        public const string TokenEndpointName = "token_endpoint";
        [JsonPropertyName(TokenEndpointName)]
        public string TokenEndpoint { get; set; }

        public const string JwksUriName = "jwks_uri";
        [JsonPropertyName(JwksUriName)]
        public string JwksUri { get; set; }

        public const string ResponseTypesSupportedName = "response_types_supported";
        [JsonPropertyName(ResponseTypesSupportedName)]
        public ResponseType[] ResponseTypesSupported { get; set; }

        public const string SubjectTypesSupportedName = "subject_types_supported";
        [JsonPropertyName(SubjectTypesSupportedName)]
        public string SubjectTypesSupported { get; set; }

        public const string IDTokenSigningAlgValuesSupportedName = "id_token_signing_alg_values_supported";
        [JsonPropertyName(IDTokenSigningAlgValuesSupportedName)]
        public string IDTokenSigningAlgValuesSupported { get; set; }
    }
}
