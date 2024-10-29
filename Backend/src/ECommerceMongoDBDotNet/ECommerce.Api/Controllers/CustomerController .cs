using ECommerce.Api.Domain;
using ECommerce.Api.Domain.Entitys;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerCacheRepository _customerCacheRepository;

        public CustomerController(ICustomerCacheRepository customerCacheRepository)
        {
            _customerCacheRepository = customerCacheRepository;
        }

        // GET: api/customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            var customers = await _customerCacheRepository.GetAllCustomersAsync();
            return Ok(customers);
        }


        // GET: api/customer/{id}
        [HttpGet("{customerId}")]
        public async Task<ActionResult<Customer>> GetCustomer(string customerId)
        {
            var customer = await _customerCacheRepository.GetCustomerAsync(customerId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // POST: api/customer
        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody] Customer customer)
        {
            await _customerCacheRepository.CreateAsync(customer);
            return CreatedAtAction(nameof(GetCustomer), new { customerId = customer.CustomerId }, customer);
        }

        // PUT: api/customer/{id}
        [HttpPut("{customerId}")]
        public async Task<IActionResult> UpdateCustomer(int customerId, Customer customer)
        {
            // Verifica que el ID del cliente en el objeto de actualización sea el mismo que el del parámetro
            if (customer.CustomerId != customerId)
            {
                return BadRequest("El ID del cliente no coincide con el ID proporcionado.");
            }

            // Realiza la actualización sin cambiar el campo `_id`
            var result = await _customerCacheRepository.UpdateAsync(customer);

            if (result == null)
            {
                return NotFound();
            }

            return NoContent();
        }


        // DELETE: api/customer/{id}
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> DeleteCustomer(int customerId)
        {
            await _customerCacheRepository.RemoveAsync(customerId);
            return NoContent();
        }
    }
}
