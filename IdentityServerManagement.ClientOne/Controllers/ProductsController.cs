using IdentityServerManagement.ClientOne.Models;
using IdentityServerManagement.ClientOne.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IdentityServerManagement.ClientOne.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly IApiResourceHttpClient _apiResourceHttpClient;

        public ProductsController(IConfiguration configuration, IApiResourceHttpClient apiResourceHttpClient)
        {
            _configuration = configuration;
            _apiResourceHttpClient = apiResourceHttpClient;
        }


        public async Task<IActionResult> Index()
        {
            
            HttpClient client = await _apiResourceHttpClient.GetHttpClient();
            List<Product> products = new List<Product>();


            var response = await client.GetAsync("https://localhost:7242/api/Products/GetProducts");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                products = JsonConvert.DeserializeObject<List<Product>>(content)!;
            }
            else
            {
                //loglama yap
            }

            return View(products);
        }
    }
}
#region ClientCredential
//HttpClient client = new HttpClient();

//var clientCredentialsTokenRequest = await ClientCredentialsTokenHelper.GetClientCredentialsTokenRequest(client, _configuration);
//var token = await client.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

//client.SetBearerToken(token.AccessToken);

//var response = await client.GetAsync("https://localhost:7242/api/Products/GetProducts");

//var products = new List<Product>();

//if (response.IsSuccessStatusCode)
//{
//    var content = await response.Content.ReadAsStringAsync();
//    products = JsonConvert.DeserializeObject<List<Product>>(content);
//}
//return View(products); 
#endregion