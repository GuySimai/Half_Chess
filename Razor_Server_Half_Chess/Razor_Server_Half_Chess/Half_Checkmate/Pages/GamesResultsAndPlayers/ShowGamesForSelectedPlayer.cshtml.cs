using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Razor_Server_Half_Chess.Models;

namespace Razor_Server_Half_Chess.Pages.GamesResultsAndPlayers
{
    public class ShowGamesForSelectedPlayerModel : PageModel
    {
        private readonly Half_Checkmate.Data.Half_CheckmateContext _context;
        public List<string?>? UsersNames { get; set; }
        public IList<TblChessGames> PlayerGames { get; set; } = default!;

        [BindProperty]
        public string? SelectedPlayerName { get; set; }


        public ShowGamesForSelectedPlayerModel(Half_Checkmate.Data.Half_CheckmateContext context)
        {
            _context = context;
            if (_context.TblUsers != null)
            {
                UsersNames = _context.TblUsers.Select(p => p.Name).OrderBy(name => name).Distinct().ToList();
            }
        }

        public async Task OnGetAsync()
        {
            if (_context.TblChessGames != null)
            {
                PlayerGames = await _context.TblChessGames.ToListAsync();
            }
        }

        public async Task OnPostByNamesAsync()
        {
            // Check if the context and selected player name are not null or empty
            if (_context.TblChessGames != null && _context.TblUsers != null && !string.IsNullOrEmpty(SelectedPlayerName))
            {
                // Finding the UserID of the player by their name (case-insensitive comparison)
                var selectedPlayer = await _context.TblUsers
                    .Where(p => p.Name != null && p.Name.ToLower() == SelectedPlayerName.ToLower()) // Added null check for Name
                    .FirstOrDefaultAsync();

                // If we found the player
                if (selectedPlayer != null)
                {
                    // Retrieve all the player's games by UserID
                    PlayerGames = await _context.TblChessGames
                        .Where(p => p.UserID == selectedPlayer.UserID) // Search by UserID
                        .ToListAsync();
                }
            }
        }




    }
}
