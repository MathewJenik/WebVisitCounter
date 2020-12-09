using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVisitCounter.Data;
using Microsoft.AspNetCore.Cors;

namespace WebVisitCounter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class APIWebsitesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public APIWebsitesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/APIWebsites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Website>>> Getwebsites()
        {
            return await _context.websites.ToListAsync();
        }

        // GET: api/APIWebsites/5
        [EnableCors("PersonalPolicy")]
        [HttpGet("get/{id}")]
        public async Task<ActionResult<Website>> GetWebsite(int id)
        {
            var website = await _context.websites.FindAsync(id);

            if (website == null)
            {
                return NotFound();
            }

            return website;
        }

        [HttpGet("update/{id}")]
        public async Task<ActionResult<Website>> updateViewCount(int id)
        {
            var website = await _context.websites.FindAsync(id);

            if (website == null)
            {
                return NotFound();
            }
            website.totalVisits += 1;
            _context.SaveChanges();
            return website;
        }

        // PUT: api/APIWebsites/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("put/{id}")]
        public async Task<IActionResult> PutWebsite(int id, Website website)
        {
            if (id != website.websiteId)
            {
                return BadRequest();
            }

            _context.Entry(website).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WebsiteExists(id))
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

        // POST: api/APIWebsites
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Website>> PostWebsite(Website website)
        {
            _context.websites.Add(website);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetWebsite", new { id = website.websiteId }, website);
        }

        // DELETE: api/APIWebsites/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Website>> DeleteWebsite(int id)
        {
            var website = await _context.websites.FindAsync(id);
            if (website == null)
            {
                return NotFound();
            }

            _context.websites.Remove(website);
            await _context.SaveChangesAsync();

            return website;
        }

        private bool WebsiteExists(int id)
        {
            return _context.websites.Any(e => e.websiteId == id);
        }



        //

     
    }

}
