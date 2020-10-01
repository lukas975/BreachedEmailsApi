using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BreachedEmailsApi.Models;
using BreachedEmailsApi.Service;
using System.Security.Cryptography.X509Certificates;

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

        // GET: api/BreachedEmails/5
        [HttpGet("{id}")]
        public ActionResult<BreachedEmail> GetBreachedEmail(string id)
        {
            var cachedEmails = GetCachedEmails();

            var breachedEmail = cachedEmails.SingleOrDefault(x => x.Email == id); //await _context.BreachedEmails.FindAsync(id);

            if (breachedEmail == null)
            {
                return NotFound();
            }

            return breachedEmail;
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
                UpdateCachedEmails();
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
            var cachedEmails = GetCachedEmails();

            var breachedEmail = cachedEmails.SingleOrDefault(x => x.Email == id); //await _context.BreachedEmails.FindAsync(id);

            if (breachedEmail == null)
            {
                return NotFound();
            }

            _context.BreachedEmails.Remove(breachedEmail);
            await _context.SaveChangesAsync();

            UpdateCachedEmails();

            return breachedEmail;
        }

        private bool BreachedEmailExists(string id)
        {
            return _context.BreachedEmails.Any(e => e.Email == id);
        }

        private List<BreachedEmail> GetCachedEmails()
        {
            List<BreachedEmail> mcBreachedEmails = _memoryCache.GetCache<List<BreachedEmail>>("breachedemails");

            if (mcBreachedEmails == null)
            {
                List<BreachedEmail> breachedEmails = _context.BreachedEmails.ToList();
                _memoryCache.SetCache<List<BreachedEmail>>(breachedEmails, "breachedemails");

                return breachedEmails;
            }

            return mcBreachedEmails;
        }

        private void UpdateCachedEmails()
        {
            List<BreachedEmail> breachedEmails = _context.BreachedEmails.ToList();
            _memoryCache.SetCache<List<BreachedEmail>>(breachedEmails, "breachedemails");
        }
    }
}
