using Half_Checkmate.Data;
using Half_Checkmate.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Razor_Server_Half_Chess.Pages.GamesResultsAndPlayers
{
    public class PlayersGroupedbyGamesPlayedModel : PageModel
    {
        private readonly Half_CheckmateContext _context;
        public IList<IGrouping<int, TblUsers>> GroupedTblUsers { get; set; } = default!;

        public PlayersGroupedbyGamesPlayedModel(Half_CheckmateContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            if (_context.TblUsers != null)
            {
                var allUsers = await _context.TblUsers.ToListAsync();

                // Grouping by number of games with descending order sorting
                GroupedTblUsers = allUsers
                    .GroupBy(u => u.NumberOfGames).OrderByDescending(g => g.Key).ToList();
            }
        }
    }
}
