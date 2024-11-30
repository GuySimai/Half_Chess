using System;
using System.Drawing;
using System.IO;

namespace Client_Half_Chess.Models
{
    public class Rook : ChessPiece
    {
        public Rook(Color color)
        {
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..")); // (bin\Debug)
            string imagesDirectory = Path.Combine(projectDirectory, "Images");
            string imagePath = Path.Combine(imagesDirectory, $"{(color.Name == "White" ? "wR" : "bR")}.png");
            pieceBitmap = new Bitmap(imagePath);
        }

        public override Boolean Move(int row, int col)
        {
            int startRow = position.X;
            int startCol = position.Y;

            if (row == startRow || col == startCol)
            {
                return true;
            }

            return false;
        }

        public override Boolean Attack(int row, int col)
        {
            return Move(row, col);
        }
    }
}
