using Microsoft.AspNetCore.Mvc;
using MovieRental.Customer;
using MovieRental.Movie;
using MovieRental.Rental;

namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {

        private readonly ICustomerFeatures _features;

        public CustomerController(ICustomerFeatures features)
        {
            _features = features;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Customer.Customer customer)
        {
	        return Ok(await _features.Save(customer));
        }

        [HttpGet]
        public async 
            
            Task<IActionResult> GetCustomer()
        {
            return Ok(await _features.GetCustomers());
        }

    }
}
