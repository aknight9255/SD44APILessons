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
    public class TransactionController : ApiController
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> Post(Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.DateOfTransaction = DateTime.Now;
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Transaction> transactions = await _context.Transactions.ToListAsync();
            return Ok(transactions);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri]int id)
        {
            Transaction transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put([FromUri]int id, [FromBody]Transaction model)
        {
            if (ModelState.IsValid)
            {
                if(id != model.Id)
                {
                    return BadRequest("Transaction Id mismatch. SHAME ");
                }
                _context.Entry(model).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete([FromUri]int id)
        {
            Transaction transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}
