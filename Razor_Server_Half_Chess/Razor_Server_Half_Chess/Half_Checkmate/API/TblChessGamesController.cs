using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Half_Checkmate.Data;
using Razor_Server_Half_Chess.Models;

namespace Razor_Server_Half_Chess.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblChessGamesController : ControllerBase
    {
        private readonly Half_CheckmateContext _context;

        public TblChessGamesController(Half_CheckmateContext context)
        {
            _context = context;
        }

        // GET: api/TblChessGames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblChessGames>>> GetTblChessGames()
        {
            if (_context.TblChessGames == null)
            {
                return NotFound();
            }
            return await _context.TblChessGames.ToListAsync();
        }

        // GET: api/TblChessGames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblChessGames>> GetTblChessGames(int id)
        {
            if (_context.TblChessGames == null)
            {
                return NotFound();
            }
            var tblChessGames = await _context.TblChessGames.FindAsync(id);

            if (tblChessGames == null)
            {
                return NotFound();
            }

            return tblChessGames;
        }

        // PUT: api/TblChessGames/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblChessGames(int id, TblChessGames tblChessGames)
        {
            if (id != tblChessGames.GameID)
            {
                return BadRequest();
            }

            _context.Entry(tblChessGames).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblChessGamesExists(id))
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

        // POST: api/TblChessGames
        [HttpPost]
        public async Task<ActionResult<TblChessGames>> PostTblChessGames(TblChessGames tblChessGames)
        {
            if (_context.TblChessGames == null)
            {
                return Problem("Entity set 'Half_CheckmateContext.TblChessGames'  is null.");
            }
            _context.TblChessGames.Add(tblChessGames);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblChessGames", new { id = tblChessGames.GameID }, tblChessGames);
        }

        // DELETE: api/TblChessGames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblChessGames(int id)
        {
            if (_context.TblChessGames == null)
            {
                return NotFound();
            }
            var tblChessGames = await _context.TblChessGames.FindAsync(id);
            if (tblChessGames == null)
            {
                return NotFound();
            }

            _context.TblChessGames.Remove(tblChessGames);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblChessGamesExists(int id)
        {
            return (_context.TblChessGames?.Any(e => e.GameID == id)).GetValueOrDefault();
        }
    }
}
