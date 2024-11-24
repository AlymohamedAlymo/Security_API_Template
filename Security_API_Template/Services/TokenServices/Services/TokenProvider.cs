using API_Template.Api.Services.TokenServices.Models;
using IdentityModel;
using IdentityServer4.ResponseHandling;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace API_Template.Api.Services.TokenServices.Services
{
    public class TokenProvider : Interfaces.ITokenProvider
    {
        private readonly ITokenRequestValidator _requestValidator;
        private readonly IClientSecretValidator _clientValidator;
        private readonly ITokenResponseGenerator _responseGenerator;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public TokenProvider(ITokenRequestValidator requestValidator, IClientSecretValidator clientValidator, ITokenResponseGenerator responseGenerator, IHttpContextAccessor httpContextAccessor)
        {
            _requestValidator = requestValidator;
            _clientValidator = clientValidator;
            _responseGenerator = responseGenerator;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<Models.TokenResponse> GetToken(TokenRequest request, string Scope)
        {
            var parameters = new NameValueCollection
            {
                { "username", request.Username },
                { "password", request.Password },
                { "grant_type", request.GrantType },
                { "scope", Scope },
                { "refresh_token", request.RefreshToken },
                { "response_type", OidcConstants.ResponseTypes.Token }
            };

            var response = await GetIdpToken(parameters);

            return GetTokenResponse(response);
        }

        private async Task<IdentityServer4.ResponseHandling.TokenResponse> GetIdpToken(NameValueCollection parameters)
        {
            var clientResult = await _clientValidator.ValidateAsync(_httpContextAccessor.HttpContext);

            if (clientResult.IsError)
            {
                return new IdentityServer4.ResponseHandling.TokenResponse
                {
                    Custom = new Dictionary<string, object>
                    {
                    { "Error", "invalid_client" },
                    { "ErrorDescription", "Invalid client/secret combination" }
                    }
                };
            }

            var validationResult = await _requestValidator.ValidateRequestAsync(parameters, clientResult);

            if (validationResult.IsError)
            {
                return new IdentityServer4.ResponseHandling.TokenResponse
                {
                    Custom = new Dictionary<string, object>
                    {
                    { "Error", validationResult.Error },
                    { "ErrorDescription", validationResult.ErrorDescription }
                    }
                };
            }

            return await _responseGenerator.ProcessAsync(validationResult);
        }


        private static Models.TokenResponse GetTokenResponse(IdentityServer4.ResponseHandling.TokenResponse response)
        {
            if (response.Custom != null && response.Custom.ContainsKey("Error"))
            {
                return new Models.TokenResponse
                {
                    Error = response.Custom["Error"].ToString(),
                    ErrorDescription = response.Custom["ErrorDescription"]?.ToString()
                };
            }

            return new Models.TokenResponse
            {
                AccessToken = response.AccessToken,
                RefreshToken = response.RefreshToken,
                ExpiresIn = DateTime.Now.AddSeconds(response.AccessTokenLifetime),
                TokenType = "Bearer"
            };
        }

    }
}
