using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FollowAPI.Models;

namespace FollowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private readonly FollowContext _context;

        public FollowController(FollowContext context)
        {
            _context = context;
        }

        // GET: api/Follow
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Follow>>> GetFollow()
        {
          if (_context.Follow == null)
          {
              return NotFound();
          }
            return await _context.Follow.ToListAsync();
        }

        // GET: api/Follow/Followers/5
        [HttpGet("Followers/{id}")]
        public async Task<ActionResult<IEnumerable<Follow>>> GetFollowersSpecific(int id)
        {
            if (_context.Follow == null)
            {
                return NotFound();
            }

            var followers = await _context.Follow.ToListAsync();

            if (followers == null)
            {
                return NotFound();
            }

            var filteredFollowers = followers.Where(f => f.FollowedId == id).ToList();

            return filteredFollowers;
        }
        
        // GET: api/Follow/Followed/5
        [HttpGet("Followed/{id}")]
        public async Task<ActionResult<IEnumerable<Follow>>> GetFollowersSpecific2(int id)
        {
            if (_context.Follow == null)
            {
                return NotFound();
            }

            var followers = await _context.Follow.ToListAsync();

            if (followers == null)
            {
                return NotFound();
            }

            var filteredFollowers = followers.Where(f => f.FollowerId == id).ToList();

            return filteredFollowers;
        }


        // GET: api/Follow/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Follow>> GetFollow(int id)
        {
          if (_context.Follow == null)
          {
              return NotFound();
          }
            var follow = await _context.Follow.FindAsync(id);

            if (follow == null)
            {
                return NotFound();
            }

            return follow;
        }

        // PUT: api/Follow/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFollow(int id, Follow follow)
        {
            if (id != follow.FollowId)
            {
                return BadRequest();
            }

            _context.Entry(follow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowExists(id))
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

        // POST: api/Follow
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Follow>> PostFollow(Follow follow)
        {
          if (_context.Follow == null)
          {
              return Problem("Entity set 'FollowContext.Follow'  is null.");
          }
            _context.Follow.Add(follow);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFollow", new { id = follow.FollowId }, follow);
        }

        // DELETE: api/Follow/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFollow(int id)
        {
            if (_context.Follow == null)
            {
                return NotFound();
            }
            var follow = await _context.Follow.FindAsync(id);
            if (follow == null)
            {
                return NotFound();
            }

            _context.Follow.Remove(follow);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FollowExists(int id)
        {
            return (_context.Follow?.Any(e => e.FollowId == id)).GetValueOrDefault();
        }
    }
}
