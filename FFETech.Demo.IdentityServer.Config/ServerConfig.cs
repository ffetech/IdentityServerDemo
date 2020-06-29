using System.Collections.Generic;

using IdentityServer4.Models;

namespace FFETech.Demo.IdentityServer.Config
{
    public static class ServerConfig
    {
        #region Properties

        public static IEnumerable<IdentityResource> Ids => new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiResource> Apis => new ApiResource[]
            {
                new ApiResource("api1", "My API #1")
            };

        public static IEnumerable<Client> Clients => new Client[]
            {
                // Razor client
                new Client
                {
                    ClientId = GlobalConfig.RazorClientId,
                    ClientName = "Razor UI",

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireConsent = false,

                    RequirePkce = true,
                    ClientSecrets = { new Secret(GlobalConfig.RazorClientSecret.Sha256()) },

                    RedirectUris = { $"http://localhost:{GlobalConfig.RazorPort}/{GlobalConfig.RazorClientId}/signin-oidc" },
                    FrontChannelLogoutUri = $"http://localhost:{GlobalConfig.RazorPort}/{GlobalConfig.RazorClientId}/signout-oidc",
                    PostLogoutRedirectUris = { $"http://localhost:{GlobalConfig.RazorPort}/{GlobalConfig.RazorClientId}/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "api1" }
                },

                // Blazor client
                new Client
                {
                    ClientId = GlobalConfig.BlazorClientId,
                    ClientName = "Blazor Client",

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RequireConsent = false,

                    RequirePkce = true,
                    ClientSecrets = { new Secret(GlobalConfig.BlazorClientSecret.Sha256()) },

                    RedirectUris = { $"http://localhost:{GlobalConfig.BlazorPort}/{GlobalConfig.BlazorClientId}/signin-oidc" },
                    FrontChannelLogoutUri = $"http://localhost:{GlobalConfig.BlazorPort}/{GlobalConfig.BlazorClientId}/signout-oidc",
                    PostLogoutRedirectUris = { $"http://localhost:{GlobalConfig.BlazorPort}/{GlobalConfig.BlazorClientId}/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "api1" }
                },

                // Api Client
                new Client
                {
                    ClientId = GlobalConfig.ApiClientId,
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret(GlobalConfig.ApiClientSecret.Sha256()) },

                    AllowedScopes = { "api1" }
                },
            };

        #endregion
    }
}