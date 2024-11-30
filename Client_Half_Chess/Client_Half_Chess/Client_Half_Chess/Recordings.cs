using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Client_Half_Chess
{
    public partial class Recordings : Form
    {
        private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='|DataDirectory|\Database.mdf';Integrated Security=True";
        public Recordings()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedDialog; // It is not possible to change the size of the form.
            this.MaximizeBox = false;

            comboBoxOrderBy.Items.Add("GameID");
            comboBoxOrderBy.Items.Add("UserID");
            comboBoxOrderBy.SelectedIndex = 0;

        }
        private void Recordings_Load(object sender, EventArgs e)
        {
            ReadTblRecordingsData(comboBoxOrderBy.Text);
            TblDataGridView.DataSource = TblBindingSource;
        }

        private void comboBoxOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            string orderBy = comboBoxOrderBy.SelectedItem.ToString();
            ReadTblRecordingsData(orderBy);
        }

        // -- Read Data --
        private void ReadTblRecordingsData(string orderBy)
        {
            string queryString = $"SELECT * FROM dbo.TblRecordings ORDER BY {orderBy}";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                reader.Close();

                TblBindingSource.DataSource = dataTable;
            }
        }


        private void Run_Recording_Click(object sender, EventArgs e)
        {
            CheckGameId();
        }

        private void CheckGameId()
        {
            string gameIdText = textBox1.Text;

            // Make sure the user did not leave the box empty
            if (string.IsNullOrEmpty(gameIdText))
            {
                CustomMessageBox.Show("Please enter a user ID.", 1);
                return;
            }

            // Check if the GAMEID is a number
            int gameId;
            if (!int.TryParse(gameIdText, out gameId))
            {
                CustomMessageBox.Show("Game not found. Please check the ID.", 1);
                return;
            }

            CheckIfGameIdExists(gameId);
        }

        private void CheckIfGameIdExists(int gameId)
        {
            string queryString = "SELECT COUNT(*) FROM dbo.TblRecordings WHERE GameID = @GameID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@GameID", gameId);

                connection.Open();

                int count = (int)command.ExecuteScalar();

                if (count > 0)
                {
                    CheckIfGameHasMoves(gameId, count, connection);

                }
                else
                {
                    CustomMessageBox.Show("Please enter a valid ID.", 1);
                }
            }
        }

        private void CheckIfGameHasMoves(int gameId, int count, SqlConnection connection)
        {
            string queryString = "SELECT COUNT(*) FROM dbo.TblMoves WHERE GameID = @GameID";
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@GameID", gameId);

            int moveCount = (int)command.ExecuteScalar();

            if (moveCount > 0)
            {
                OpenGameRecording(gameId, connection);
            }
            else
            {
                CustomMessageBox.Show("There are no moves in the game.", 1);
            }
        }

        // Select Color
        private void OpenGameRecording(int gameId, SqlConnection connection)
        {
            string queryString = "SELECT UserColor FROM dbo.TblRecordings WHERE GameID = @GameID";
            SqlCommand command = new SqlCommand(queryString, connection);
            command.Parameters.AddWithValue("@GameID", gameId);

            string colorString = (string)command.ExecuteScalar();

            Color userColor;
            switch (colorString.ToLower())
            {
                case "white":
                    userColor = Color.White;
                    break;
                case "black":
                    userColor = Color.Black;
                    break;
                default:
                    userColor = Color.White; // Default
                    break;
            }

            GameRecording game = new GameRecording(gameId, userColor);
            this.Hide();
            game.ShowDialog();
            this.Show();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
