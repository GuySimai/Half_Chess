using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Half_Checkmate.Models;
using Half_Checkmate.Data;

namespace Half_Checkmate.Pages.Users
{
	public class EditModel : PageModel
	{
		[BindProperty]
		public TblUsers TblUsers { get; set; } = default!;
		public List<string?> Countries { get; set; }
		private readonly Half_CheckmateContext _context;

		public EditModel(Half_CheckmateContext context)
		{
			_context = context;
			Countries = _context.TblCountries.Select(p => p.Country).ToList();
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
			TblUsers = tblusers;
			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid || _context.TblUsers == null || TblUsers == null)
			{
				return Page();
			}

			_context.Attach(TblUsers).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!TblUsersExists(TblUsers.UserID))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return RedirectToPage("./Index");
		}

		private bool TblUsersExists(int id)
		{
			return (_context.TblUsers?.Any(e => e.UserID == id)).GetValueOrDefault();
		}
	}
}
