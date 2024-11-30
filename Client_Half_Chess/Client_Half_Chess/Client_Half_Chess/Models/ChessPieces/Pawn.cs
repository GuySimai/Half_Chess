using System;
using System.Drawing;
using System.IO;

namespace Client_Half_Chess.Models
{
    public class Pawn : ChessPiece
    {
        public int StapDirection { get; set; }

        public Pawn(Color color, int direction)
        {
            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..")); // (bin\Debug)
            string imagesDirectory = Path.Combine(projectDirectory, "Images");
            string imagePath = Path.Combine(imagesDirectory, $"{(color.Name == "White" ? "wP" : "bP")}.png");
            pieceBitmap = new Bitmap(imagePath);
            this.StapDirection = direction;
        }

        public override Boolean Move(int row, int col)
        {
            int startRow = position.X;
            int startCol = position.Y;

            if (col == startCol && row == startRow + StapDirection)
            {
                return true;
            }

            if ((col == startCol + StapDirection || col == startCol - StapDirection) && row == startRow)
            {
                return true;
            }

            if ((StapDirection == -1 && startRow == 6) || (StapDirection == 1 && startRow == 1))
            {
                if (col == startCol && row == startRow + 2 * StapDirection)
                {
                    return true;
                }
            }

            return false;
        }

        public override Boolean Attack(int row, int col)
        {
            if (Math.Abs(col - position.Y) == 1 && row == position.X + StapDirection)
            {
                return true;
            }

            return false;
        }

        public Boolean IsPromotion()
        {
            int Row = position.X;

            if ((StapDirection == -1 && Row == 0) || (StapDirection == 1 && Row == 7))
            {
                return true;
            }

            return false;
        }

    }
}

