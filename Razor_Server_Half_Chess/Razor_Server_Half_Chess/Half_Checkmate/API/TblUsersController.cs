using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Half_Checkmate.Data;
using Half_Checkmate.Models;

namespace Half_Checkmate.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblUsersController : ControllerBase
    {
        private readonly Half_CheckmateContext _context;

        public TblUsersController(Half_CheckmateContext context)
        {
            _context = context;
        }

        // GET: api/TblUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblUsers>>> GetTblUsers()
        {
            if (_context.TblUsers == null)
            {
                return NotFound();
            }
            return await _context.TblUsers.ToListAsync();
        }

        // GET: api/TblUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblUsers>> GetTblUsers(int id)
        {
            if (_context.TblUsers == null)
            {
                return NotFound();
            }
            var tblUsers = await _context.TblUsers.FindAsync(id);

            if (tblUsers == null)
            {
                return NotFound();
            }

            return tblUsers;
        }

        // PUT: api/TblUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblUsers(int id, TblUsers tblUsers)
        {
            if (id != tblUsers.UserID)
            {
                return BadRequest();
            }

            _context.Entry(tblUsers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblUsersExists(id))
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

        // POST: api/TblUsers
        [HttpPost]
        public async Task<ActionResult<TblUsers>> PostTblUsers(TblUsers tblUsers)
        {
            if (_context.TblUsers == null)
            {
                return Problem("Entity set 'Half_CheckmateContext.TblUsers'  is null.");
            }
            _context.TblUsers.Add(tblUsers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblUsers", new { id = tblUsers.UserID }, tblUsers);
        }

        // DELETE: api/TblUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblUsers(int id)
        {
            if (_context.TblUsers == null)
            {
                return NotFound();
            }
            var tblUsers = await _context.TblUsers.FindAsync(id);
            if (tblUsers == null)
            {
                return NotFound();
            }

            _context.TblUsers.Remove(tblUsers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblUsersExists(int id)
        {
            return (_context.TblUsers?.Any(e => e.UserID == id)).GetValueOrDefault();
        }
    }
}
