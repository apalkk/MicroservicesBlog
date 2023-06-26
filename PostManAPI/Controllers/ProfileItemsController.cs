using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostManAPI.Models;

namespace PostManAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileItemsController : ControllerBase
    {
        private readonly ProfileContext _context;

        public ProfileItemsController(ProfileContext context)
        {
            _context = context;
        }

        // GET: api/ProfileItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfileItems()
        {
          if (_context.ProfileItems == null)
          {
              return NotFound();
          }
            return await _context.ProfileItems.ToListAsync();
        }

        [HttpGet("/specific/{username}")]
        public async Task<ActionResult<IEnumerable<Profile>>> GetProfileSpecific(string username)
        {
            if (_context.ProfileItems == null)
            {
                return NotFound();
            }
            var v = await _context.ProfileItems.ToListAsync();

            return new ActionResult<IEnumerable<Profile>>(v.Where(s=>s.UserName == username));
        }

        // GET: api/ProfileItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profile>> GetProfile(int id)
        {
          if (_context.ProfileItems == null)
          {
              return NotFound();
          }
            var profile = await _context.ProfileItems.FindAsync(id);

            if (profile == null)
            {
                return NotFound();
            }

            return profile;
        }

        // PUT: api/ProfileItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfile(int id, Profile profile)
        {
            if (id != profile.Id)
            {
                return BadRequest();
            }

            _context.Entry(profile).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfileExists(id))
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

        // POST: api/ProfileItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Profile>> PostProfile(Profile profile)
        {
          if (_context.ProfileItems == null)
          {
              return Problem("Entity set 'ProfileContext.ProfileItems'  is null.");
          }
            _context.ProfileItems.Add(profile);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProfile), new { id = profile.Id }, profile);
        }

        // DELETE: api/ProfileItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            if (_context.ProfileItems == null)
            {
                return NotFound();
            }
            var profile = await _context.ProfileItems.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            _context.ProfileItems.Remove(profile);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProfileExists(int id)
        {
            return (_context.ProfileItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
