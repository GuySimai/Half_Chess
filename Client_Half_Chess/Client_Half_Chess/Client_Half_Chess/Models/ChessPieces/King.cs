using System;
using System.Drawing;
using System.IO;

namespace Client_Half_Chess.Models
{
    public class King : ChessPiece
    {
        public King(Color color)
        {
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..")); // (bin\Debug)
            string imagesDirectory = Path.Combine(projectDirectory, "Images");
            string imagePath = Path.Combine(imagesDirectory, $"{(color.Name == "White" ? "wK" : "bK")}.png");
            pieceBitmap = new Bitmap(imagePath);
        }

        public override Boolean Move(int row, int col)
        {
            int startRow = position.X;
            int startCol = position.Y;

            if (Math.Abs(row - startRow) <= 1 && Math.Abs(col - startCol) <= 1)
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
