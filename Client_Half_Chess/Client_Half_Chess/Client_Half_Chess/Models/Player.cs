using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Client_Half_Chess.Models
{
    public enum UserType { User, Server }

    public class Player
    {
        // Variables
        public UserType UserType { get; set; }
        public Color Color { get; set; }
        public Color MoveColor { get; set; }
        public Color SelectedPieceColor { get; set; }
        public ChessPiece[,] pieces;

        public Player(UserType userType, Color color, Color moveColor, Color SelectedPieceColor)
        {
            this.UserType = userType;
            this.Color = color;
            this.MoveColor = moveColor;
            this.SelectedPieceColor = SelectedPieceColor;
            pieces = new ChessPiece[GameUserServer.ROWS, GameUserServer.COLUMNS];
            PlacePieces();
        }

        private void PlacePieces()
        {
            if (UserType == UserType.Server)
            {
                pieces[0, 0] = new Rook(Color) { position = new Point(0, 0) };
                pieces[0, 1] = new Knight(Color) { position = new Point(0, 1) };
                pieces[0, 2] = new Bishop(Color) { position = new Point(0, 2) };
                pieces[0, 3] = new King(Color) { position = new Point(0, 3) };
                pieces[1, 0] = new Pawn(Color, 1) { position = new Point(1, 0) };
                pieces[1, 1] = new Pawn(Color, 1) { position = new Point(1, 1) };
                pieces[1, 2] = new Pawn(Color, 1) { position = new Point(1, 2) };
                pieces[1, 3] = new Pawn(Color, 1) { position = new Point(1, 3) };
            }
            else
            {
                pieces[7, 0] = new Rook(Color) { position = new Point(7, 0) };
                pieces[7, 1] = new Knight(Color) { position = new Point(7, 1) };
                pieces[7, 2] = new Bishop(Color) { position = new Point(7, 2) };
                pieces[7, 3] = new King(Color) { position = new Point(7, 3) };
                pieces[6, 0] = new Pawn(Color, -1) { position = new Point(6, 0) };
                pieces[6, 1] = new Pawn(Color, -1) { position = new Point(6, 1) };
                pieces[6, 2] = new Pawn(Color, -1) { position = new Point(6, 2) };
                pieces[6, 3] = new Pawn(Color, -1) { position = new Point(6, 3) };
            }
        }

        // Retuen possibilities Buttons
        public List<Button> ThePossibilities(Player OtherGroup, int row, int col, ChessPiece selectedPiece, Button[,] boardButtons)
        {
            List<Button> possibilities = new List<Button>();
            Button button;

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    // Moving Possibilities
                    button = ThePossibilitiesMoving(OtherGroup, row, col, selectedPiece, boardButtons, r, c);
                    if (button != null)
                    {
                        possibilities.Add(button);
                    }
                    // Attacking Possibilities
                    button = ThePossibilitiesAttacking(OtherGroup, row, col, selectedPiece, boardButtons, r, c);
                    if (button != null)
                    {
                        possibilities.Add(button);
                    }
                }
            }
            return possibilities;
        }

        private Button ThePossibilitiesMoving(Player OtherGroup, int row, int col, ChessPiece selectedPiece, Button[,] boardButtons, int r, int c)
        {
            // Ensuring the move is valid for the piece, and the target square is empty
            if (selectedPiece.Move(r, c) && OtherGroup.pieces[r, c] == null && this.pieces[r, c] == null && IsValidMove(row, col, r, c, OtherGroup))
            {
                if (selectedPiece is Rook && RookCheck(row, col, r, c, OtherGroup))
                {

                    return boardButtons[r, c];

                }
                else if (selectedPiece is Bishop && BishopCheck(row, col, r, c, OtherGroup))
                {

                    return boardButtons[r, c];

                }
                else if (selectedPiece is Pawn && PawnCheck(row, col, r, c, OtherGroup))
                {

                    return boardButtons[r, c];

                }
                else if (selectedPiece is Knight || selectedPiece is King)
                {

                    return boardButtons[r, c];

                }
            }
            return null;
        }

        private Button ThePossibilitiesAttacking(Player OtherGroup, int row, int col, ChessPiece selectedPiece, Button[,] boardButtons, int r, int c)
        {
            if (selectedPiece.Attack(r, c) && OtherGroup.pieces[r, c] != null && IsValidMove(row, col, r, c, OtherGroup))
            {
                if (selectedPiece is Rook && RookCheck(row, col, r, c, OtherGroup))
                {
                    return boardButtons[r, c];

                }
                else if (selectedPiece is Bishop && BishopCheck(row, col, r, c, OtherGroup))
                {
                    return boardButtons[r, c];

                }
                else if (!(selectedPiece is Rook) && !(selectedPiece is Bishop))
                {

                    return boardButtons[r, c];

                }
            }
            return null;
        }

        // Checking if chess after the move
        private bool IsValidMove(int startRow, int startCol, int endRow, int endCol, Player otherGroup)
        {
            ChessPiece piece = this.pieces[startRow, startCol];
            ChessPiece capturedPiece = otherGroup.pieces[endRow, endCol];

            // Move the piece to the new location
            this.pieces[startRow, startCol] = null;
            this.pieces[endRow, endCol] = piece;
            piece.position = new Point(endRow, endCol);

            // If it's attacking, temporarily remove the opponent piece
            if (capturedPiece != null)
            {
                otherGroup.pieces[endRow, endCol] = null;
            }

            bool isInCheck = IsKingInCheck(otherGroup);

            // return back
            this.pieces[startRow, startCol] = piece;
            this.pieces[endRow, endCol] = null;
            piece.position = new Point(startRow, startCol);

            if (capturedPiece != null)
            {
                otherGroup.pieces[endRow, endCol] = capturedPiece;
            }

            return !isInCheck;
        }

        // -- All the moves Check --
        private bool RookCheck(int startRow, int startCol, int endRow, int endCol, Player OtherGroup)
        {
            if (startRow == endRow)
            {
                int minCol = Math.Min(startCol, endCol);
                int maxCol = Math.Max(startCol, endCol);

                for (int c = minCol + 1; c < maxCol; c++)
                {
                    if (pieces[startRow, c] != null || OtherGroup.pieces[startRow, c] != null) // If there is a piece in the way
                    {
                        return false;
                    }
                }
            }

            else if (startCol == endCol)
            {
                int minRow = Math.Min(startRow, endRow);
                int maxRow = Math.Max(startRow, endRow);

                for (int r = minRow + 1; r < maxRow; r++)
                {
                    if (pieces[r, startCol] != null || OtherGroup.pieces[r, startCol] != null) // If there is a piece in the way
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private bool BishopCheck(int startRow, int startCol, int endRow, int endCol, Player OtherGroup)
        {
            if (Math.Abs(startRow - endRow) == Math.Abs(startCol - endCol))
            {
                int rowStep = (endRow > startRow) ? 1 : -1;
                int colStep = (endCol > startCol) ? 1 : -1;

                int r = startRow + rowStep;
                int c = startCol + colStep;

                while (r != endRow && c != endCol)
                {
                    if (pieces[r, c] != null || OtherGroup.pieces[r, c] != null) // If there is a piece in the way
                    {
                        return false;
                    }

                    r += rowStep;
                    c += colStep;
                }
            }

            return true;
        }

        private bool PawnCheck(int startRow, int startCol, int endRow, int endCol, Player OtherGroup)
        {
            int direction = (this.UserType == UserType.User) ? -1 : 1;

            if ((this.UserType == UserType.User && startRow == 6) || (this.UserType == UserType.Server && startRow == 1))
            {
                if (endRow == startRow + (2 * direction) && endCol == startCol)
                {
                    if (this.pieces[startRow + direction, startCol] == null && OtherGroup.pieces[startRow + direction, startCol] == null) // If there is a piece in the way
                    {
                        return true;
                    }
                    return false;
                }
            }

            return true;
        }

        // -- Check --- Checkmate --- Draw --
        public bool IsKingInCheck(Player opponent)
        {
            // Finding the current player's king
            ChessPiece king = this.pieces.OfType<King>().FirstOrDefault();
            if (king == null) return false;

            // Go over all the opponent's tools
            for (int r = 0; r < GameUserServer.ROWS; r++)
            {
                for (int c = 0; c < GameUserServer.COLUMNS; c++)
                {
                    ChessPiece opponentPiece = opponent.pieces[r, c];
                    if (opponentPiece != null)
                    {
                        if (opponentPiece.Attack(king.position.X, king.position.Y))
                        {
                            if (opponentPiece is Rook && RookCheck(r, c, king.position.X, king.position.Y, opponent))
                            {
                                return true; ;

                            }
                            else if (opponentPiece is Bishop && BishopCheck(r, c, king.position.X, king.position.Y, opponent))
                            {
                                return true;
                            }
                            else if (!(opponentPiece is Rook) && !(opponentPiece is Bishop))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public bool IsCheckmate(Player opponent, Button[,] boardButtons)
        {
            // Check if the king is in check
            if (!opponent.IsKingInCheck(this))
                return false;

            // Check if no valid move can be made
            foreach (var piece in opponent.pieces)
            {
                if (piece == null) continue;

                var possibleMoves = opponent.ThePossibilities(this, piece.position.X, piece.position.Y, piece, boardButtons);
                if (possibleMoves.Any())
                {
                    return false;
                }
            }

            return true; // No legal moves and the king in chess -> mate
        }

        public bool IsDraw(Player opponent, Button[,] boardButtons)
        {
            // Checking for lack of tools for victory
            if (HasInsufficientMaterial(this) && HasInsufficientMaterial(opponent))
            {
                return true;
            }

            // Checking if there are no legal moves
            foreach (var piece in opponent.pieces)
            {
                if (piece == null) continue;

                var possibleMoves = opponent.ThePossibilities(this, piece.position.X, piece.position.Y, piece, boardButtons);
                if (possibleMoves.Any())
                {
                    return false;
                }
            }

            // If there are no legal moves, and the king is not in chess -> draw
            if (!opponent.IsKingInCheck(this))
            {
                return true;
            }


            return false;
        }

        private bool HasInsufficientMaterial(Player player)
        {
            int pieceCount = 0;
            bool hasBishop = false;
            bool hasKnight = false;

            for (int i = 0; i < player.pieces.GetLength(0); i++)
            {
                for (int j = 0; j < player.pieces.GetLength(1); j++)
                {
                    var piece = player.pieces[i, j];
                    if (piece != null)
                    {
                        pieceCount++;

                        if (piece is Bishop) hasBishop = true;
                        if (piece is Knight) hasKnight = true;
                    }
                }
            }

            if (pieceCount == 1) return true;

            // If there are a king and a knight or a king and a knight left
            if (pieceCount == 2 && (hasBishop || hasKnight)) return true;

            return false;
        }

        // -- Special case --
        public ChessPiece Promotion(string selectedPieceName, int row, int col)
        {

            if (selectedPieceName == "Bishop")
            {
                this.pieces[row, col] = new Bishop(this.Color) { position = new Point(row, col) };
            }
            else if (selectedPieceName == "Knight")
            {
                this.pieces[row, col] = new Knight(this.Color) { position = new Point(row, col) };
            }
            else if (selectedPieceName == "Rook")
            {
                this.pieces[row, col] = new Rook(this.Color) { position = new Point(row, col) };
            }

            return this.pieces[row, col];
        }
    }
}
