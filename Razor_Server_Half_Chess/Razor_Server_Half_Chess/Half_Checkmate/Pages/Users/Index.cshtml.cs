using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Half_Checkmate.Data;
using Half_Checkmate.Models;

namespace Half_Checkmate.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly Half_CheckmateContext _context;
        public bool ShowCaseSensitiveButton { get; set; }
        public bool ShowWithoutCaseSensitiveButton { get; set; }
        public IList<TblUsers> TblUsers { get; set; } = default!;

        public IndexModel(Half_CheckmateContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            if (_context.TblUsers != null)
            {
                await OnPostAllPlayersAsync();
            }
        }

        public async Task OnPostAllPlayersAsync()
        {
            if (_context.TblUsers != null)
            {
                var players = await _context.TblUsers.ToListAsync();
                TblUsers = players.OrderBy(p => p.Name, StringComparer.Ordinal).ToList();
                ShowCaseSensitiveButton = false;
                ShowWithoutCaseSensitiveButton = true;
            }
        }

        public async Task OnPostWithoutCaseSensitiveAsync()
        {
            if (_context.TblUsers != null)
            {
                TblUsers = await _context.TblUsers.OrderBy(p => p.Name).ToListAsync();
                ShowCaseSensitiveButton = true;
                ShowWithoutCaseSensitiveButton = false;
            }
        }
    }
}
