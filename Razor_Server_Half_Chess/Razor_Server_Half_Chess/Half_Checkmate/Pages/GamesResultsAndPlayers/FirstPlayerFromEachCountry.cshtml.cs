using Half_Checkmate.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Razor_Server_Half_Chess.Pages.GamesResultsAndPlayers
{
    public class FirstPlayerFromEachCountryModel : PageModel
    {
        public IList<PlayerFromEachCountry> playerFromEachCountry { get; set; } = default!;

        private readonly Half_CheckmateContext _context;

        public FirstPlayerFromEachCountryModel(Half_CheckmateContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            if (_context.TblUsers != null && _context.TblChessGames != null)
            {
                // Retrieving all users and games
                var playersWithGames = await (from user in _context.TblUsers
                                              join game in _context.TblChessGames
                                              on user.UserID equals game.UserID
                                              select new { user.Country, user.UserID, user.Name, game.StartTime })
                                              .ToListAsync();


                playerFromEachCountry = playersWithGames
                    .GroupBy(pg => new { pg.Country, pg.UserID, pg.Name }) // Grouping by country
                    .Select(g => new PlayerFromEachCountry
                    {
                        Country = g.Key.Country,
                        UserID = g.Key.UserID,
                        Name = g.Key.Name,
                        // Selecting the minimum time of the first game
                        FirstGameTime = g.Min(pg => pg.StartTime)
                    })
                    .OrderBy(p => p.FirstGameTime) // Sorting by the first game time
                    .GroupBy(p => p.Country) // Grouping by country to get only the first player
                    .Select(g => g.First()) // Select the first player from each country
                    .ToList(); 
            }
        }



    }

    public class PlayerFromEachCountry
    {
        public string? Country { get; set; }
        public int UserID { get; set; }
        public string? Name { get; set; }
        public DateTime? FirstGameTime { get; set; }
    }

}
