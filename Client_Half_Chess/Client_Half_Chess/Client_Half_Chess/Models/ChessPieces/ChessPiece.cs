using System;
using System.Drawing;


namespace Client_Half_Chess.Models
{
    public abstract class ChessPiece
    {
        public Bitmap pieceBitmap { get; set; }
        public Point position { get; set; }
        public abstract Boolean Move(int row, int col);
        public abstract Boolean Attack(int row, int col);
    }
}
