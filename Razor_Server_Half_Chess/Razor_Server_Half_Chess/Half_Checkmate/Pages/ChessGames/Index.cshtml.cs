using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Half_Checkmate.Data;
using Razor_Server_Half_Chess.Models;

namespace Razor_Server_Half_Chess.Pages.ChassGames
{
    public class IndexModel : PageModel
    {
        private readonly Half_CheckmateContext _context;

        public IndexModel(Half_CheckmateContext context)
        {
            _context = context;
        }

        public IList<TblChessGames> TblChessGames { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.TblChessGames != null)
            {
                TblChessGames = await _context.TblChessGames.ToListAsync();
            }
        }
    }
}
