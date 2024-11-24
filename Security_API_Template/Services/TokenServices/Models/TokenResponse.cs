using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Template.Api.Services.TokenServices.Models
{
    public class TokenResponse
    {
        [JsonProperty("access_token", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string AccessToken { get; set; }
        [JsonProperty("refresh_token", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string RefreshToken { get; set; }
        [JsonProperty("token_type", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string TokenType { get; set; }
        [JsonProperty("expires_in", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DateTime ExpiresIn { get; set; }
        [JsonProperty("error", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Error { get; set; }
        [JsonProperty("error_description", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ErrorDescription { get; set; }
    }
}
