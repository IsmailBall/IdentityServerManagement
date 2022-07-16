using IdentityServerManagement.ApiOne.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerManagement.ApiOne.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = "ReadPolicy")]
        public IActionResult GetProducts()
        {
            var products = new List<Product>()
            {
                new Product
                {
                    Id = 1,
                    Name = "Pen",
                    UnitsInStock = 250
                },
                new Product
                {
                    Id = 2,
                    Name = "Book",
                    UnitsInStock = 750
                },
                new Product
                {
                    Id = 3,
                    Name = "Eraser",
                    UnitsInStock = 500
                }
            };

            return Ok(products);
        }

        [HttpPost]
        [Authorize(Policy = "UpdatePolicy")]
        public IActionResult UpdateProduct(int id)
        {
            return Ok($"product {id} was updated");
        }

        [HttpPost]
        [Authorize(Policy = "AddPolicy")]
        public IActionResult AddProduct(Product product)
        {
            return Ok($"product {product.Name} was added");
        }
    }
}
