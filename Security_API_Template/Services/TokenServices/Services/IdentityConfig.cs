using IdentityServer4;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Template.Api.Services.TokenServices.Services
{
    public class IdentityConfig
    {
        public static IEnumerable<ApiScope> ApiScopes => new[]
        {
            new ApiScope("electalexapi.read"),
            new ApiScope("electalexapi.write"),
        };

        public static IEnumerable<ApiResource> GetApiResources() => new List<ApiResource>
        {
            new ApiResource("all", "all"),
            new ApiResource("electalexapi")
            {
                Scopes = new List<string> { "electalexapi.read", "electalexapi.write"},
                ApiSecrets = new List<Secret> {new Secret("ScopeSecret".Sha256())},
                UserClaims = new List<string> {"role"}
            }
        };

        public static IEnumerable<Client> GetClients() => new List<Client>
        {
            new Client
            {
                ClientName = "InternalResources",
                ClientId = "InternalResources",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret("#dfewrfD85462584w5Srwfd@gdfDg2ZCZtdTgC$Y%wsrC12CBU4V3BVB54&HBd2NKOfgN34PM*~315#M$71de$".Sha256())
                },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "electalexapi.read", "electalexapi.write"
                },
                AllowOfflineAccess = true,
                RefreshTokenUsage = TokenUsage.OneTimeOnly,
                AccessTokenLifetime = 10800,
                RefreshTokenExpiration = TokenExpiration.Absolute,
                AbsoluteRefreshTokenLifetime = 200000,
                Claims = new List<ClientClaim>
                {
                    new ClientClaim(IdentityModel.JwtClaimTypes.Role, "Editor"),
                }
            },
        };

        public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    }
}
