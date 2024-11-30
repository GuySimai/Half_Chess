using Client_Half_Chess.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_Half_Chess
{

    public partial class GameBoard : Form
    {
        // Constants
        public static int ROWS = 8;
        public static int COLUMNS = 4;
        public static int MARGIN = 40;
        protected static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\Database.mdf';Integrated Security=True";
        protected readonly Color LIGHT_COLOR = Color.FromArgb(204, 153, 102);
        protected readonly Color DARK_COLOR = Color.FromArgb(139, 69, 19);

        // Variables
        protected Player UserPlayer;
        protected Player ServerPlayer;
        protected Button[,] boardButtons;
        protected Label[][] Labels;
        protected ChessPiece selectedPiece;
        protected List<Button> possibilities;
        protected Player currentPlayer;

        // Default
        public GameBoard()
        {
        }

        public GameBoard(Color color)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;

            boardButtons = new Button[ROWS, COLUMNS];
            Labels = new Label[4][];
            selectedPiece = null;

            UserPlayer = new Player(UserType.User, color, Color.Green, Color.DarkGreen);
            ServerPlayer = new Player(UserType.Server, color == Color.Black ? Color.White : Color.Black, Color.Red, Color.DarkRed);

            int cellWidth = (ClientSize.Width - 2 * MARGIN) / COLUMNS;
            int cellHeight = (ClientSize.Height + GameUserServer.ToolStripHeight / 2 - 2 * MARGIN) / ROWS;

            MakeboardButtons(cellWidth, cellHeight);

            DrawPieces(UserPlayer);
            DrawPieces(ServerPlayer);
        }


        // -- For the board --
        private void GameBoard_Load(object sender, EventArgs e)
        {
            int cellWidth = (ClientSize.Width - 2 * MARGIN) / COLUMNS;
            int cellHeight = (ClientSize.Height + GameUserServer.ToolStripHeight / 2 - 2 * MARGIN) / ROWS;//

            MakeboardButtons(cellWidth, cellHeight);
            MakeLabelsRightLeft(cellHeight);
            MakeLabelsUpBottom(cellWidth);
        }

        private void MakeboardButtons(int cellWidth, int cellHeight)
        {
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLUMNS; col++)
                {
                    if (boardButtons[row, col] == null)
                    {
                        boardButtons[row, col] = new Button
                        {
                            BackColor = (row + col) % 2 == 0 ? LIGHT_COLOR : DARK_COLOR,
                            FlatStyle = FlatStyle.Flat
                        };
                        this.Controls.Add(boardButtons[row, col]);
                    }
                    boardButtons[row, col].Size = new Size(cellWidth, cellHeight);
                    boardButtons[row, col].Location = new Point(col * cellWidth + MARGIN, row * cellHeight + MARGIN);
                }
            }
        }

        private void MakeLabelsRightLeft(int cellHeight)
        {
            Label[] LeftLabels = new Label[ROWS];
            Label[] rightLabels = new Label[ROWS];

            for (int row = 0; row < ROWS; row++)
            {
                if (LeftLabels[row] == null)
                {
                    LeftLabels[row] = MakeLabelRightLeft(row, cellHeight);
                    this.Controls.Add(LeftLabels[row]);
                }
                LeftLabels[row].Location = new Point(MARGIN - LeftLabels[row].Width, row * cellHeight + MARGIN);



                if (rightLabels[row] == null)
                {
                    rightLabels[row] = MakeLabelRightLeft(row, cellHeight);
                    this.Controls.Add(rightLabels[row]);
                }
                rightLabels[row].Location = new Point(ClientSize.Width - MARGIN, row * cellHeight + MARGIN);
            }

            Labels[0] = LeftLabels;
            Labels[1] = rightLabels;
        }


        private void MakeLabelsUpBottom(int cellWidth)
        {
            Label[] UpLabels = new Label[COLUMNS];
            Label[] bottomLabels = new Label[COLUMNS];

            for (int col = 0; col < COLUMNS; col++)
            {
                if (UpLabels[col] == null)
                {
                    UpLabels[col] = MakeLabelUpBottom(col, cellWidth);
                    this.Controls.Add(UpLabels[col]);
                }

                UpLabels[col].Location = new Point(col * cellWidth + MARGIN, MARGIN / 3);

                if (bottomLabels[col] == null)
                {
                    bottomLabels[col] = MakeLabelUpBottom(col, cellWidth);
                    this.Controls.Add(bottomLabels[col]);
                }
                bottomLabels[col].Location = new Point(col * cellWidth + MARGIN, ClientSize.Height - bottomLabels[col].Height);
            }
            Labels[2] = UpLabels;
            Labels[3] = bottomLabels;
        }
        private Label MakeLabelRightLeft(int row, int cellHeight)
        {
            Label label = new Label
            {
                Text = (ROWS - row).ToString(),
                Size = new Size(MARGIN, cellHeight),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            return label;
        }

        private Label MakeLabelUpBottom(int col, int cellWidth)
        {
            Label label = new Label
            {
                Text = ((char)('A' + col)).ToString(),
                Size = new Size(cellWidth, MARGIN),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            return label;
        }



        protected void DrawPieces(Player PiecesGroup)
        {
            foreach (var piece in PiecesGroup.pieces)
            {
                if (piece != null)
                {
                    int row = piece.position.X;
                    int col = piece.position.Y;

                    if (boardButtons[row, col] != null)
                    {
                        boardButtons[row, col].BackgroundImage = piece.pieceBitmap;
                        boardButtons[row, col].BackgroundImageLayout = ImageLayout.Zoom;
                    }

                }
            }
        }

        // -- For the game and the recordings
        protected void GetPossibleMoves(Player PiecesGroup, Player OtherGroup, int row, int col)
        {
            if (PiecesGroup.pieces[row, col] != null)
            {
                selectedPiece = PiecesGroup.pieces[row, col];

                // Update the piece button color
                boardButtons[selectedPiece.position.X, selectedPiece.position.Y].BackColor = PiecesGroup.SelectedPieceColor;

                // Show the available options.
                possibilities = PiecesGroup.ThePossibilities(OtherGroup, row, col, selectedPiece, boardButtons);
                MarkPossibleMoves(possibilities, PiecesGroup.MoveColor);
            }
        }

        private void MarkPossibleMoves(List<Button> possibleMoves, Color moveColor)
        {
            foreach (Button button in possibleMoves)
            {
                button.BackColor = moveColor;
            }
        }

        protected void SelectedPieceMovesHelper(Player PiecesGroup, Player OtherGroup, int row, int col)
        {
            // If there is an opponent's piece in the same position
            if (selectedPiece.Attack(row, col))
            {
                OtherGroup.pieces[row, col] = null;
                boardButtons[row, col].BackgroundImage = null;
            }

            // Move the piece to the new position
            PiecesGroup.pieces[selectedPiece.position.X, selectedPiece.position.Y] = null;
            boardButtons[selectedPiece.position.X, selectedPiece.position.Y].BackgroundImage = null;

            // Update button color and remove possible moves colors
            ResetButtonColors(possibilities);

            // Update the piece's position
            PiecesGroup.pieces[row, col] = selectedPiece;
            selectedPiece.position = new Point(row, col);
        }

        protected void ResetButtonColors(List<Button> possibleMoves)
        {
            boardButtons[selectedPiece.position.X, selectedPiece.position.Y].BackColor =
                        (selectedPiece.position.X + selectedPiece.position.Y) % 2 == 0 ? LIGHT_COLOR : DARK_COLOR;

            foreach (Button button in possibleMoves)
            {
                int row = (button.Top - MARGIN) / button.Height;
                int col = (button.Left - MARGIN) / button.Width;

                button.BackColor = (row + col) % 2 == 0 ? LIGHT_COLOR : DARK_COLOR;
            }
        }


        // Mark king
        protected async void MarkKingInCheck(Player opponent)
        {
            if (opponent.IsKingInCheck(currentPlayer))
            {
                ChessPiece king = opponent.pieces.OfType<King>().FirstOrDefault();
                if (king != null)
                {
                    Button kingButton = boardButtons[king.position.X, king.position.Y];
                    await FlashButton(kingButton, Color.Red, kingButton.BackColor);
                }
            }
        }

        private async Task FlashButton(Button button, Color color, Color originalColor)
        {
            for (int i = 0; i < 3; i++)
            {
                button.BackColor = color;
                await Task.Delay(250);
                button.BackColor = originalColor;
                await Task.Delay(250);
            }
        }

        protected async Task WaitForSeconds()
        {
            await Task.Delay(1500);
        }



    }
}
