using Half_Checkmate.Data;
using Half_Checkmate.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Razor_Server_Half_Chess.Pages.GamesResultsAndPlayers
{
    public class GamesCountPerPlayerModel : PageModel
    {
        private readonly Half_CheckmateContext _context;
        public IList<TblUsers> TblUsers { get; set; } = default!;

        public GamesCountPerPlayerModel(Half_CheckmateContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            if (_context.TblUsers != null)
            {
                TblUsers = await _context.TblUsers.ToListAsync();
            }
        }
    }
}
