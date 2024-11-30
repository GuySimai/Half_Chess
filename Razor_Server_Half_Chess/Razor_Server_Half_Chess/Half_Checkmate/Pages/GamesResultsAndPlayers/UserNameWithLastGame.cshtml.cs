using Half_Checkmate.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Razor_Server_Half_Chess.Pages.GamesResultsAndPlayers
{
    public class UserNameWithLastGameModel : PageModel
    {
        private readonly Half_CheckmateContext _context;
        public IList<UserNameWithLastGame> userNameWithLastGame { get; set; } = default!;

        public UserNameWithLastGameModel(Half_CheckmateContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            if (_context.TblUsers != null && _context.TblChessGames != null)
            {
                userNameWithLastGame = await (from user in _context.TblUsers
                                              join game in _context.TblChessGames
                                              on user.UserID equals game.UserID into gameGroup
                                              from game in gameGroup.DefaultIfEmpty() // LastGameDate == NULL ,Left Join
                                              group game by new { user.UserID, user.Name } into gameGroup // Group the games by UserID and Name of the user
                                              select new UserNameWithLastGame
                                              {
                                                  UserID = gameGroup.Key.UserID,
                                                  Name = gameGroup.Key.Name,
                                                  LastGameDate = gameGroup.Max(g => g.StartTime)
                                              })
                                              .ToListAsync();

                userNameWithLastGame = userNameWithLastGame
                    .OrderBy(u => u.Name, StringComparer.Ordinal) // Order by name
                    .ToList();
            }
        }

    }

    public class UserNameWithLastGame
    {
        public int UserID { get; set; }
        public string? Name { get; set; }
        public DateTime? LastGameDate { get; set; }
    }
}
