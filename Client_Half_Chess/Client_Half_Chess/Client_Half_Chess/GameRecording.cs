using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using Client_Half_Chess.Models;

namespace Client_Half_Chess
{
    public partial class GameRecording : GameBoard
    {
        private List<ChessMove> moves = new List<ChessMove>();
        public class ChessMove
        {
            public int MoveNumber { get; set; }
            public int FromX { get; set; }
            public int FromY { get; set; }
            public int ToX { get; set; }
            public int ToY { get; set; }
            public string Promotion { get; set; }

        }

        public GameRecording(int gameID, Color playerColor) : base(playerColor)
        {
            InitializeComponent();
            ReadGameMoves(gameID);

            if (moves[0].MoveNumber == 1)
            {
                if (playerColor == Color.White)
                {
                    currentPlayer = UserPlayer;
                }
                else
                {
                    currentPlayer = ServerPlayer;
                }
            }
            MovePiecesAsync();

        }

        // Update moves list
        private void ReadGameMoves(int gameId)
        {
            string queryString = @"
        SELECT Moves.MoveNumber, Moves.FromX, Moves.FromY, Moves.ToX, Moves.ToY, Moves.Promotion
        FROM dbo.TblMoves AS Moves
        WHERE Moves.GameID = @GameID
        ORDER BY Moves.MoveNumber";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@GameID", gameId);

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ChessMove move = new ChessMove
                    {
                        MoveNumber = reader.GetInt32(0),
                        FromX = reader.GetInt32(1),
                        FromY = reader.GetInt32(2),
                        ToX = reader.GetInt32(3),
                        ToY = reader.GetInt32(4),
                        Promotion = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                    };

                    moves.Add(move);
                }

                reader.Close();
            }
        }

        // Move pieces for the record effect
        private async void MovePiecesAsync()
        {
            foreach (var move in moves)
            {
                await WaitForSeconds();

                if (currentPlayer == UserPlayer)
                {
                    await MovePiecesHelperAsync(UserPlayer, ServerPlayer, move);
                }
                else
                {
                    await MovePiecesHelperAsync(ServerPlayer, UserPlayer, move);
                }
            }
        }

        private async Task MovePiecesHelperAsync(Player playerGroup, Player secondGroup, ChessMove move)
        {
            GetPossibleMoves(playerGroup, secondGroup, move.FromX, move.FromY);

            await WaitForSeconds();

            PieceMove(playerGroup, secondGroup, move.ToX, move.ToY, move.Promotion);
        }

        private void PieceMove(Player PiecesGroup, Player OtherGroup, int row, int col, string promotion)
        {
            SelectedPieceMovesHelper(PiecesGroup, OtherGroup, row, col);

            // In case of a pawn promotion
            if (selectedPiece is Pawn)
            {
                Pawn p = (Pawn)selectedPiece;
                if (p.IsPromotion())
                {
                    selectedPiece = PiecesGroup.Promotion(promotion, row, col);
                }
            }

            // Update the board
            DrawPieces(UserPlayer);
            DrawPieces(ServerPlayer);

            // Check if the king is in check
            MarkKingInCheck(OtherGroup);

            // End of turn, switch to the other player's turn
            currentPlayer = (currentPlayer == UserPlayer) ? ServerPlayer : UserPlayer;
            selectedPiece = null;
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
