using System;

namespace Client_Half_Chess.Models
{
    public class Game
    {
        public int GameID { get; set; }
        public int UserID { get; set; }
        public DateTime StartTime { get; set; }
        public string GameLength { get; set; }
        public string Winner { get; set; }
    }
}
