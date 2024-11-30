using Half_Checkmate.Data;
using Half_Checkmate.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Razor_Server_Half_Chess.Pages.GamesResultsAndPlayers
{
    public class PlayersGroupedByCountryModel : PageModel
    {
        private readonly Half_CheckmateContext _context;
        public IList<IGrouping<string?, TblUsers>> GroupedTblUsers { get; set; } = default!;

        public PlayersGroupedByCountryModel(Half_CheckmateContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            if (_context.TblUsers != null)
            {
                var allUsers = await _context.TblUsers.ToListAsync();

                // Group by country and order countries alphabetically
                GroupedTblUsers = allUsers.GroupBy(u => u.Country).OrderBy(g => g.Key).ToList();    // Group by country, Order the groups alphabetically

            }
        }
    }
}
