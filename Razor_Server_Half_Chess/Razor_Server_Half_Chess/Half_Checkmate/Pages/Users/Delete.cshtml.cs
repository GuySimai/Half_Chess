using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Half_Checkmate.Models;
using Half_Checkmate.Data;

namespace Half_Checkmate.Pages.Users
{
	public class DeleteModel : PageModel
	{
		[BindProperty]
		public TblUsers TblUsers { get; set; } = default!;
		private readonly Half_CheckmateContext _context;
		public DeleteModel(Half_CheckmateContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> OnGetAsync(int? id)
		{
			if (id == null || _context.TblUsers == null)
			{
				return NotFound();
			}

			var tblusers = await _context.TblUsers.FirstOrDefaultAsync(m => m.UserID == id);

			if (tblusers == null)
			{
				return NotFound();
			}
			else
			{
				TblUsers = tblusers;
			}
			return Page();
		}

		public async Task<IActionResult> OnPostAsync(int? id)
		{
			if (id == null || _context.TblUsers == null || _context.TblChessGames == null)
			{
				return NotFound();
			}

			// Searching for the user by ID
			var tblusers = await _context.TblUsers.FindAsync(id);

			if (tblusers != null)
			{
				// Deleting all games related to the user
				var userGames = _context.TblChessGames.Where(g => g.UserID == tblusers.UserID);
				_context.TblChessGames.RemoveRange(userGames);

				// Deleting the user
				_context.TblUsers.Remove(tblusers);

				await _context.SaveChangesAsync();
			}

			// Redirecting back to the Index page after deletion
			return RedirectToPage("./Index");
		}

	}
}
