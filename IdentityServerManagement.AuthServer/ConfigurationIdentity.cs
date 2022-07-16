using IdentityServer4.Models;

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
                }
            };
        }
    }
}
