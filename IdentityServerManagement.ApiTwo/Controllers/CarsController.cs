using IdentityServerManagement.ApiTwo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerManagement.ApiTwo.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = "ReadPolicy")]
        public IActionResult GetCars()
        {
            var cars = new List<Car>()
            {
                new Car
                {
                    Id = 1,
                    Brand = "Mercedes",
                    Model = "C180"
                },
                new Car
                {
                    Id = 2,
                    Brand = "Bmw",
                    Model = "XDrive"
                },
                new Car
                {
                    Id = 3,
                    Brand = "Audi",
                    Model = "A5 Sport"
                },
            };

            return Ok(cars);
        }

        [HttpPost]
        [Authorize(Policy = "UpdatePolicy")]
        public IActionResult UpdateCar(int id)
        {
            return Ok($"Car {id} was updated");
        }

        [HttpPost]
        [Authorize(Policy = "AddPolicy")]
        public IActionResult AddCar(Car car)
        {
            return Ok($"car {car.Brand} was added");
        }
    }
}
