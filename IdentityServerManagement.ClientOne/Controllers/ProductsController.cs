using IdentityModel.Client;
using IdentityServerManagement.ClientOne.Models;
using IdentityServerManagement.ClientOne.Utils.Helper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IdentityServerManagement.ClientOne.Controllers
{
    public class ProductsController : Controller
    {

        private readonly IConfiguration _configuration;
        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();

            var clientCredentialsTokenRequest = await ClientCredentialsTokenHelper.GetClientCredentialsTokenRequest(client, _configuration);
            var token = await client.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

            client.SetBearerToken(token.AccessToken);

            var response = await client.GetAsync("https://localhost:7242/api/Products/GetProducts");

            var products = new List<Product>();

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<List<Product>>(content);
            }
            return View(products);
        }
    }
}
