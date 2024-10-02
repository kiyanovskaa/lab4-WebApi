using kpz_test4.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace kpz_test4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class customersController : ControllerBase
    {
        private readonly Context _context;
        public customersController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            if(_context.customers == null)
            {
                return NotFound();
            }
            return await _context.customers.ToListAsync();  
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            if(_context == null)
            {
                return NotFound();
            }
            var customer =await _context.customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.customers.Add(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCustomers), new { id = customer.id }, customer);
        }

        [HttpPut]
        public async Task<ActionResult<Customer>> PutCustomer(int id, Customer customer)
        {
            if (id != customer.id)
            {
                return BadRequest();
            }
            _context.Entry(customer).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!CustomerAvailable(id))
                {
                    return NotFound();
                }
                else throw;
            }
            return Ok();
        }
        private bool CustomerAvailable(int id)
        {
            return (_context.customers?.Any(x => x.id == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            if (_context.customers == null)
            {
                return NotFound();
            }
            var customer =await _context.customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            _context.customers.Remove(customer);
            await _context.SaveChangesAsync();
            return Ok();    

        }
    }
}
