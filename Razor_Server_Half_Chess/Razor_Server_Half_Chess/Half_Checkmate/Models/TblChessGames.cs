using System.ComponentModel.DataAnnotations;

namespace Razor_Server_Half_Chess.Models
{
    public class TblChessGames
    {
        [Key]
        public int GameID { get; set; }
        public int UserID { get; set; }
        public DateTime StartTime { get; set; }
        public string? GameLength { get; set; }
        public string? Winner { get; set; }
    }
}
