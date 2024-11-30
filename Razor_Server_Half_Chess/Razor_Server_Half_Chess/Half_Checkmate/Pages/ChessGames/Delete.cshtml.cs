using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor_Server_Half_Chess.Models;

namespace Razor_Server_Half_Chess.Pages.ChassGames
{
    public class DeleteModel : PageModel
    {
        private readonly Half_Checkmate.Data.Half_CheckmateContext _context;

        public DeleteModel(Half_Checkmate.Data.Half_CheckmateContext context)
        {
            _context = context;
        }

        [BindProperty]
      public TblChessGames TblChessGames { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TblChessGames == null)
            {
                return NotFound();
            }

            var tblchessgames = await _context.TblChessGames.FirstOrDefaultAsync(m => m.GameID == id);

            if (tblchessgames == null)
            {
                return NotFound();
            }
            else 
            {
                TblChessGames = tblchessgames;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.TblChessGames == null || _context.TblUsers == null)
            {
                return NotFound();
            }

            // Find the game by ID
            var tblchessgames = await _context.TblChessGames.FindAsync(id);

            // Ensure that the game exists
            if (tblchessgames == null)
            {
                return NotFound(); // If the game is not found, return a 404
            }

            // Find the user associated with the game
            var user = await _context.TblUsers.FindAsync(tblchessgames.UserID);

            // If the user is found, update their number of games
            if (user != null)
            {
                user.NumberOfGames -= 1;
                _context.TblUsers.Update(user);
            }

            // Delete the game from the database
            _context.TblChessGames.Remove(tblchessgames);
            await _context.SaveChangesAsync();

            // Redirect to the Index page after the delete
            return RedirectToPage("./Index");
        }


    }
}
