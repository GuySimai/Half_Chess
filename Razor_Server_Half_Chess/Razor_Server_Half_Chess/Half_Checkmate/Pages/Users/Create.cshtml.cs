using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Half_Checkmate.Models;
using Microsoft.EntityFrameworkCore;
using Half_Checkmate.Data;

namespace Half_Checkmate.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly Half_CheckmateContext _context;
        public List<string?> Countries { get; set; }
        [BindProperty]
        public TblUsers TblUsers { get; set; } = default!;
        public CreateModel(Half_CheckmateContext context)
        {
            _context = context;
            Countries = _context.TblCountries.Select(p => p.Country).ToList();
        }

        public IActionResult OnGet()
        {
            return Page();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.TblUsers == null || TblUsers == null)
            {
                return Page();
            }

            var userExists = await _context.TblUsers.AnyAsync(tu => tu.UserID == TblUsers.UserID);
            if (userExists)
            {
                ModelState.AddModelError("TblUsers.UserID", "User ID already exists.");
                return Page();
            }
            TblUsers.NumberOfGames = 0;

            _context.TblUsers.Add(TblUsers);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "User added successfully!";
            TblUsers = default!;

            return Page();
        }
    }
}
