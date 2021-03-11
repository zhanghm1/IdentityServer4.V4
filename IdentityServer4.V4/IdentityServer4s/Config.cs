// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer4.V4
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> Apis =>
            new List<ApiScope>
            {
                new ApiScope("api1", "用户数据"),
            };

        public static IEnumerable<Client> Clients(IConfiguration Configuration)
        {
            var MVC = new Client
            {
                ClientId = "mvc",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RequireConsent = false,
                RequirePkce = true,
                RedirectUris = { "https://localhost:5005/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:5005/signout-callback-oidc" },
                AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                AllowOfflineAccess = true
            };
            var MVCOrder = new Client
            {
                ClientId = "MVCOrder",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RequireConsent = false,
                RequirePkce = true,
                RedirectUris = { "https://localhost:5007/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:5007/signout-callback-oidc" },
                AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                AllowOfflineAccess = true
            };

            var JsVue = new Client
            {
                ClientId = "jsvue",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "api1"
                    },
                AllowOfflineAccess = true
            };
            var list = new List<Client>() { MVC, JsVue, MVCOrder };
            return list;
        }
    }
}