using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServerManagement.AuthServer
{
    public static class ConfigurationIdentity
    {

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>()
            {
                new ApiScope("api_one_read","Allowance for reading from the source"),
                new ApiScope("api_one_write","Allowance for wrting to the source"),
                new ApiScope("api_one_update","Allowance for modifing the source"),
                new ApiScope("api_two_read","Allowance for reading from the source"),
                new ApiScope("api_two_write","Allowance for wrting to the source"),
                new ApiScope("api_two_update","Allowance for modifing the source")
            };
        }
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>() {
                new ApiResource("ApiOne")
                {
                    Scopes = GetApiScopes().Where(s => s.Name.StartsWith("api_one")).Select(s => s.Name).ToList(),
                    ApiSecrets = new [] { new Secret("secretOne".Sha256()) }
                },
                new ApiResource("ApiTwo")
                {
                    Scopes = GetApiScopes().Where(s => s.Name.StartsWith("api_two")).Select(s => s.Name).ToList(),
                    ApiSecrets = new [] { new Secret("secretTwo".Sha256()) }
                },
            };
        }
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {

            var identityResources = new List<IdentityResource>()
            {
                new IdentityResources.Email(),
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource(){ Name="CountryAndCity", DisplayName="Country and City",Description="Country And City Information of the User", UserClaims= new [] {"country","city"}},
                new IdentityResource(){ Name="Roles",DisplayName="Roles", Description="User Roles", UserClaims=new [] { "role"} }
            };

            return identityResources;
        }
        public static IEnumerable<TestUser> GetTestUsers()
        {
            var testUsers = new List<TestUser>()
            {
                new TestUser()
                {
                    SubjectId = "1",
                    Username = "sncrbl",
                    Password = "psw",
                    Claims = new List<Claim>()
                    {
                        new Claim("given_name","Sancar"),
                        new Claim("family_name", "Bal"),
                        new Claim("country","Turkey"),
                        new Claim("city","Ankara"),
                        new Claim("role","admin")
                    }
                },
                new TestUser()
                {
                    SubjectId = "2",
                    Username = "smlbl",
                    Password = "psw",
                    Claims = new List<Claim>()
                    {
                        new Claim("given_name","Ismail"),
                        new Claim("family_name", "Bal"),
                        new Claim("country","Turkey"),
                        new Claim("city","Istanbul"),
                        new Claim("role","customer")
                    }
                }
            };

            return testUsers;
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>()
            {
                new Client()
                {
                    ClientId = "ClientIdOne",
                    ClientName = "ClientOne Application",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = new [] { "api_one_read", "api_one_write", "api_one_update", "api_two_read" }
                },
                new Client()
                {
                    ClientId = "ClientIdTwo",
                    ClientName = "ClientTwo Application",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = new [] { "api_one_read", "api_one_update", "api_two_write" }
                },
                new Client()
                {
                    ClientId = "ClientOne-Mvc",
                    RequirePkce = false,
                    ClientName = "ClientOne MVC Application",
                    ClientSecrets = new [] { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = new List<string>(){ "https://localhost:7208/signin-oidc" },
                    PostLogoutRedirectUris=new List<string>{ "https://localhost:7208/signout-callback-oidc" },
                    AllowedScopes = new List<string>(){IdentityServerConstants.StandardScopes.OpenId,IdentityServerConstants.StandardScopes.Profile,"api_one_read", IdentityServerConstants.StandardScopes.Email,  IdentityServerConstants.StandardScopes.OfflineAccess,"CountryAndCity","Roles"},
                    AllowOfflineAccess = true,
                    AccessTokenLifetime=2*60*60,
                    RefreshTokenUsage=TokenUsage.ReUse,
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime=(int) (DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                    RequireConsent = true,
                },
                new Client()
                 {
                    ClientId = "Client1-ResourceOwner-Mvc",

                    ClientName="Client 1 app  mvc uygulaması",
                    ClientSecrets=new[] {new Secret("secret".Sha256())},
                    AllowedGrantTypes= GrantTypes.ResourceOwnerPassword,
                    AllowedScopes = { IdentityServerConstants.StandardScopes.Email, IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "api1.read",IdentityServerConstants.StandardScopes.OfflineAccess,"CountryAndCity","Roles"},
                    AccessTokenLifetime=2*60*60,
                    AllowOfflineAccess=true,
                    RefreshTokenUsage=TokenUsage.ReUse,
                    RefreshTokenExpiration=TokenExpiration.Absolute,
                    AbsoluteRefreshTokenLifetime=(int) (DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds,
                }
            };
        }
    }
}
