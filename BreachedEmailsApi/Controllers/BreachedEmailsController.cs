using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BreachedEmailsApi.Models;
using BreachedEmailsApi.Service;

namespace BreachedEmailsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreachedEmailsController : ControllerBase
    {
        private readonly BreachedEmailContext _context;
        private IMemoryCache _memoryCache;

        public BreachedEmailsController(BreachedEmailContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        // GET: api/BreachedEmails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BreachedEmail>>> GetBreachedEmails()
        {
            return await _context.BreachedEmails.ToListAsync();
        }

        // GET: api/BreachedEmails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BreachedEmail>> GetBreachedEmail(string id)
        {
            var breachedEmail = await _context.BreachedEmails.FindAsync(id);

            if (breachedEmail == null)
            {
                return NotFound();
            }

            return breachedEmail;
        }

        // PUT: api/BreachedEmails/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBreachedEmail(string id, BreachedEmail breachedEmail)
        {
            if (id != breachedEmail.Email)
            {
                return BadRequest();
            }

            _context.Entry(breachedEmail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BreachedEmailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BreachedEmails
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<BreachedEmail>> PostBreachedEmail(BreachedEmail breachedEmail)
        {
            _context.BreachedEmails.Add(breachedEmail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BreachedEmailExists(breachedEmail.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBreachedEmail", new { id = breachedEmail.Email }, breachedEmail);
        }

        // DELETE: api/BreachedEmails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BreachedEmail>> DeleteBreachedEmail(string id)
        {
            var breachedEmail = await _context.BreachedEmails.FindAsync(id);
            if (breachedEmail == null)
            {
                return NotFound();
            }

            _context.BreachedEmails.Remove(breachedEmail);
            await _context.SaveChangesAsync();

            return breachedEmail;
        }

        private bool BreachedEmailExists(string id)
        {
            return _context.BreachedEmails.Any(e => e.Email == id);
        }
    }
}
