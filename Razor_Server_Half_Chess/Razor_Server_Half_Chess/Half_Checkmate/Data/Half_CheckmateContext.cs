using Microsoft.EntityFrameworkCore;

namespace Half_Checkmate.Data
{
    public class Half_CheckmateContext : DbContext
    {
        public Half_CheckmateContext(DbContextOptions<Half_CheckmateContext> options)
            : base(options)
        {
        }

        public DbSet<Half_Checkmate.Models.TblCountries> TblCountries { get; set; } = default!;

        public DbSet<Half_Checkmate.Models.TblUsers>? TblUsers { get; set; }

        public DbSet<Razor_Server_Half_Chess.Models.TblChessGames>? TblChessGames { get; set; }
    }
}
