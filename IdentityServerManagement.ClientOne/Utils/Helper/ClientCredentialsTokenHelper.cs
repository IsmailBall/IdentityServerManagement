using IdentityModel.Client;

namespace IdentityServerManagement.ClientOne.Utils.Helper
{
    public static class ClientCredentialsTokenHelper
    {
        public static async Task<ClientCredentialsTokenRequest> GetClientCredentialsTokenRequest(HttpClient httpClient, IConfiguration configuration)
        {
            var discovery = await httpClient.GetDiscoveryDocumentAsync("https://localhost:7179");

            ClientCredentialsTokenRequest clientCredentialsTokenRequest = new();
            clientCredentialsTokenRequest.ClientId = configuration["Client:client_id"];
            clientCredentialsTokenRequest.ClientSecret = configuration["Client:client_secret"];
            clientCredentialsTokenRequest.Address = discovery.TokenEndpoint;

            return clientCredentialsTokenRequest;
        }

    }
}
