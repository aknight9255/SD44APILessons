using GeneralStoreAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace GeneralStoreAPI.Controllers
{
    public class CustomerController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext(); 
        [HttpPost]
        public async Task<IHttpActionResult> Post(Customer customer)
        {
            if(ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        //GETALL
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Customer> customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        //GET BY ID
        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri]int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            if(customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        //UPDATE
        [HttpPut]
        public async Task<IHttpActionResult> Put([FromUri]int id, [FromBody] Customer model)
        {
            if (ModelState.IsValid)
            {
                if(id != model.Id)
                {
                    return BadRequest("Customer Id mismatch");
                }
                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        //DELETE
        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromUri] int id)
        {
            Customer customer = await _context.Customers.FindAsync(id);
            if(customer == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
