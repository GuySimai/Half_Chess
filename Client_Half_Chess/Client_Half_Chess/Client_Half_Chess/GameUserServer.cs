using Client_Half_Chess.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_Half_Chess
{
    public partial class GameUserServer : GameBoard
    {
        // Constants
        public static int ToolStripHeight;

        // For the game control 
        private Timer turnTimer;
        private int turnTimeRemaining;
        private int turnTime;
        private User User;
        private Game game;
        private ServerChoice SC;
        private Pointers pointers;
        private Color color;
        private int gameID;
        private int moveNumber;
        private Boolean endGame;


        // Drewing
        private PictureBox pictureBox;
        private bool isDrawing = false;
        private Point previousPoint = Point.Empty;

        public GameUserServer(Color color, int turnTime, User user) : base(color)
        {
            InitializeComponent();

            User = user;
            ToolStripHeight = toolStripUp.Height;
            SC = new ServerChoice();
            pointers = new Pointers();
            this.color = color;
            endGame = false;

            if (color == Color.White)
            {
                currentPlayer = UserPlayer;
            }
            else
            {
                currentPlayer = ServerPlayer;
                ServerSelectAsync(ServerPlayer, UserPlayer);

            }

            this.turnTime = turnTime;
            turnTimer = new Timer();
            turnTimer.Interval = 1000; // Tick every second
            turnTimer.Tick += TurnTimer_Tick;
            StartTurnTimer();

            // Draw
            this.KeyDown += GameForm_KeyDown;
            this.KeyPreview = true;

            game = new Game();
            game.StartTime = DateTime.Now;
            game.UserID = user.UserID;

            SaveTblRecordings();
        }


        // -- For the board --
        private void GameForm_Load(object sender, EventArgs e)
        {
            FunBoardButtons();
        }

        private void FunBoardButtons()
        {
            for (int row = 0; row < ROWS; row++)
            {
                for (int col = 0; col < COLUMNS; col++)
                {
                    boardButtons[row, col].Click += (sender, e) => BoardButton_Click(sender, e);
                }
            }
        }

        // User play 
        private void BoardButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            int row = (clickedButton.Top - MARGIN) / clickedButton.Height;
            int col = (clickedButton.Left - MARGIN) / clickedButton.Width;

            if (currentPlayer == UserPlayer)
            {
                HandlePlayerMove(UserPlayer, ServerPlayer, row, col);
            }
        }

        // User clicks
        private void HandlePlayerMove(Player PiecesGroup, Player OtherGroup, int row, int col)
        {
            if (selectedPiece == null)
            {
                // First click - select a valid Piece.
                if (PiecesGroup.pieces[row, col] != null)
                {
                    GetPossibleMoves(PiecesGroup, OtherGroup, row, col);
                }
            }  // Piece Moves
            else if (IsMoveLegal(new Point(row, col), possibilities))
            {
                SelectedPieceMovesAsync(PiecesGroup, OtherGroup, row, col);
                if(endGame == false)
                {
                    ServerSelectAsync(ServerPlayer, UserPlayer);

                }
            }
            else if (PiecesGroup.pieces[row, col] != null)
            {
                // Focus On Another Piece
                ResetButtonColors(possibilities);
                GetPossibleMoves(PiecesGroup, OtherGroup, row, col);
            }
        }

        // Check if the new location is among the valid options
        private Boolean IsMoveLegal(Point newPosition, List<Button> possibleMoves)
        {
            foreach (Button button in possibleMoves)
            {
                int row = (button.Top - MARGIN) / button.Height;
                int col = (button.Left - MARGIN) / button.Width;

                if (row == newPosition.X && col == newPosition.Y)
                {
                    return true;
                }
            }
            return false;
        }

        // Server play
        private async void ServerSelectAsync(Player PiecesGroup, Player OtherGroup)
        {
            SC.FillPiecePositions(UserPlayer, ServerPlayer, boardButtons);
            pointers = await SC.SendToServerAndGetPointsAsync();
            GetPossibleMoves(PiecesGroup, OtherGroup, pointers.FromX, pointers.FromY);
            await WaitForSeconds();
            SelectedPieceMovesAsync(ServerPlayer, UserPlayer, pointers.ToX, pointers.ToY);
        }

        // Piece moves
        private async void SelectedPieceMovesAsync(Player PiecesGroup, Player OtherGroup, int row, int col)
        {
            int fromX = selectedPiece.position.X;
            int fromY = selectedPiece.position.Y;
            int toX = row;
            int toY = col;
            string selectedPieceName = String.Empty;

            SelectedPieceMovesHelper(PiecesGroup, OtherGroup, row, col);

            // In case of a pawn promotion
            if (selectedPiece is Pawn)
            {
                Pawn p = (Pawn)selectedPiece;
                if (p.IsPromotion())
                {
                    selectedPieceName = await PawnPromotionAsync();

                    selectedPiece = PiecesGroup.Promotion(selectedPieceName, row, col);
                }
            }

            SaveTblMoves(fromX, fromY, toX, toY, selectedPieceName);

            // Update the board
            DrawPieces(UserPlayer);
            DrawPieces(ServerPlayer);

            // Check if the king is in check
            MarkKingInCheck(OtherGroup);

            if (currentPlayer.IsCheckmate(OtherGroup, boardButtons))
            {
                endGame = true;
                EndGame(true, false);
                return;
            }

            // Check for a draw
            if (currentPlayer.IsDraw(OtherGroup, boardButtons))
            {
                endGame = true;
                EndGame(true, true);
                return;
            }

            // End of turn, switch to the other player's turn
            currentPlayer = (currentPlayer == UserPlayer) ? ServerPlayer : UserPlayer;
            selectedPiece = null;
            StartTurnTimer();
        }

        private async Task<string> PawnPromotionAsync()
        {
            string selectedPieceName;
            if (currentPlayer == UserPlayer)
            {
                Promotion CNCP = new Promotion();
                CNCP.ShowDialog(this);
                selectedPieceName = CNCP.SelectedPiece;

            }
            else
            {
                selectedPieceName = await SC.GetRandomStringAsync();
            }

            return selectedPieceName;
        }

        private async void EndGame(Boolean NotexitOrEndTurn, Boolean IsDraw)
        {
            int result=-1;
            if (IsDraw)
            {
                result = 0;
                game.Winner = "Draw";
            }

            if (NotexitOrEndTurn && IsDraw == false)
            {
                await Task.Delay(1000);

                if (currentPlayer == UserPlayer)
                {
                    result = 1;
                    game.Winner = UserPlayer.UserType.ToString();
                }
                else
                {
                    result = 2;
                    game.Winner = ServerPlayer.UserType.ToString();
                }
            }
            else if(IsDraw == false && !NotexitOrEndTurn)
            {
                if (currentPlayer == UserPlayer)
                {
                    result = 2;
                    game.Winner = ServerPlayer.UserType.ToString();
                }
                else
                {
                    result = 1;
                    game.Winner = UserPlayer.UserType.ToString();
                }
            }

            await EndGameHelperAsync(result);
        }

        private async Task EndGameHelperAsync(int result)
        {
            turnTimer.Stop();
            await WaitForSeconds();
            UpdateMovesCount();
            TimeSpan gameDuration = DateTime.Now - game.StartTime;
            string formattedGameDuration = gameDuration.ToString(@"hh\:mm\:ss");
            game.GameLength = formattedGameDuration;
            EndGame EG = new EndGame(User, game, result);
            this.Hide();
            EG.ShowDialog();
            this.Close();
        }

        // -- ToolStrip -- 
        private void Exit_Click(object sender, EventArgs e)
        {
            bool userWantsToExit = CustomMessageBox.Show("Are you sure you want to exit? You will lose automatically.", 2);
            if (userWantsToExit)
            {
                game.Winner = ServerPlayer.UserType.ToString();
                _ = EndGameHelperAsync(2);
            }
        }

        private void Rules_Click(object sender, EventArgs e)
        {
            Rules rules = new Rules();
            rules.ShowDialog();
        }

        private void TurnTimer_Tick(object sender, EventArgs e)
        {
            if (turnTimeRemaining >= 0)
            {
                TimeLeft.Text = $"{currentPlayer.UserType} Time Left:";
                Clock.Text = $"{TimeSpan.FromSeconds(turnTimeRemaining):mm\\:ss}";
                turnTimeRemaining--;
                if (turnTimeRemaining < 5)
                {
                    Clock.ForeColor = Color.DarkRed;
                }
            }
            else
            {
                EndGame(false, false);
            }
        }

        private void StartTurnTimer()
        {
            Clock.ForeColor = Color.White;
            turnTimeRemaining = turnTime;
            turnTimer.Start();
            TimeLeft.Text = $"{currentPlayer.UserType} Time Left:";
            Clock.Text = $"{TimeSpan.FromSeconds(turnTimeRemaining):mm\\:ss}";
        }

        private void Draw_Click(object sender, EventArgs e)
        {
            if (pictureBox == null)
            {
                CaptureScreen();
            }
            else
            {
                RemoveScreenshot();
            }
        }

        // D key
        private void GameForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                Draw_Click(sender, e);
            }
        }

        private void CaptureScreen()
        {

            Bitmap screenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(0, ToolStripHeight, 0, ToolStripHeight, new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height - ToolStripHeight));
            }

            // Create PictureBox
            pictureBox = new PictureBox
            {
                Image = screenshot,
                Size = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height),
                Location = new Point(0, 0),
                SizeMode = PictureBoxSizeMode.StretchImage  // Stretch
            };

            // Adding PictureBox
            this.Controls.Add(pictureBox);

            pictureBox.BringToFront();
            toolStripUp.BringToFront();

            // Draw
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseUp += PictureBox_MouseUp;
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                previousPoint = e.Location;
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                using (Graphics g = Graphics.FromImage(pictureBox.Image))
                {
                    Pen pen = new Pen(Color.Red, 5);

                    g.DrawLine(pen, previousPoint, e.Location);
                }

                previousPoint = e.Location;
                pictureBox.Invalidate();
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
        }


        private void RemoveScreenshot()
        {
            if (pictureBox != null)
            {
                this.Controls.Remove(pictureBox);
                pictureBox.Dispose();
                pictureBox = null;
            }
        }

        // Save to the DB
        private void SaveTblRecordings()
        {
            string queryString = "INSERT INTO dbo.TblRecordings (UserID, UserName, UserColor, Date, MovesNumber) " +
                                 "VALUES (@UserID, @UserName, @UserColor, @Date, @MovesNumber); " +
                                 "SELECT SCOPE_IDENTITY();";

            using (SqlConnection connection = new SqlConnection(GameUserServer.connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@UserID", User.UserID);
                command.Parameters.AddWithValue("@UserName", User.Name);
                command.Parameters.AddWithValue("@UserColor", color.Name);
                command.Parameters.AddWithValue("@Date", DateTime.Now.Date);
                command.Parameters.AddWithValue("@MovesNumber", 0);

                connection.Open();

                var result = command.ExecuteScalar();

                if (result != DBNull.Value)
                {
                    gameID = Convert.ToInt32(result);
                }
            }
        }

        private void SaveTblMoves(int fromX, int fromY, int toX, int toY, string promotion)
        {
            string queryString = "INSERT INTO dbo.TblMoves (GameID, MoveNumber, FromX, FromY, ToX, ToY, Promotion) " +
                                 "VALUES (@GameID, @MoveNumber, @FromX, @FromY, @ToX, @ToY, @Promotion)";

            using (SqlConnection connection = new SqlConnection(GameUserServer.connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@GameID", gameID);
                moveNumber++;
                command.Parameters.AddWithValue("@MoveNumber", moveNumber);
                command.Parameters.AddWithValue("@FromX", fromX);
                command.Parameters.AddWithValue("@FromY", fromY);
                command.Parameters.AddWithValue("@ToX", toX);
                command.Parameters.AddWithValue("@ToY", toY);

                if (string.IsNullOrEmpty(promotion))
                {
                    command.Parameters.AddWithValue("@Promotion", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Promotion", promotion);
                }

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        private void UpdateMovesCount()
        {
            string queryString = "UPDATE dbo.TblRecordings SET MovesNumber = @MovesNumber WHERE GameID = @GameID";

            using (SqlConnection connection = new SqlConnection(GameUserServer.connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@MovesNumber", moveNumber);
                command.Parameters.AddWithValue("@GameID", gameID);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }



    }
}
