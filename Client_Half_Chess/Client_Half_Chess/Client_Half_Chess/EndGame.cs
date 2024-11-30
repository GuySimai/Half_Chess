using Client_Half_Chess.Models;
using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_Half_Chess
{
    public partial class EndGame : Form
    {
        // Constants
        private static HttpClient client;
        private const string PATH = "https://localhost:7054/";
        private const string TO_ALL_USERS = "api/TblUsers/";
        private const string TO_ALL_GAMES = "api/TblChessGames/";

        // Variables
        int gameResult; // 0 - Draw, 1 - Win, 2 - Lose
        User User;
        Game Game;
        Timer closeTimer; // Timer to close the form after 5 seconds

        public EndGame(User user, Game game, int result)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None;

            User = user;
            Game = game;
            gameResult = result;
            User.NumberOfGames += 1;

            if (client == null)
            {
                client = new HttpClient
                {
                    BaseAddress = new Uri(PATH)
                };
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            }
        }

        private void EndGame_Load(object sender, EventArgs e)
        {
            UpdateUserInTblBindingSource(sender, e);
            CreateGameInTblBindingSource(sender, e);

            DisplayGameResult();

            closeTimer = new Timer();
            closeTimer.Interval = 5000; // 5 seconds
            closeTimer.Tick += CloseTimer_Tick;
            closeTimer.Start();
        }

        // -- Update server DB -- 
        private async void UpdateUserInTblBindingSource(object sender, EventArgs e)
        {
            string TO_ONE_USER = TO_ALL_USERS + User.UserID;
            await PutUserByIdAsync(PATH + TO_ONE_USER, User);
        }

        private async Task<User> PutUserByIdAsync(string path, User user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(path, content);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<User>();
            }
            return null;
        }

        private async void CreateGameInTblBindingSource(object sender, EventArgs e)
        {
            var content = new StringContent(JsonConvert.SerializeObject(Game), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(TO_ALL_GAMES, content);

            if (response.IsSuccessStatusCode)
            {
                await response.Content.ReadAsAsync<Game>();
            }
        }

        // Create Panel and Label of the result
        private async void DisplayGameResult()
        {
            var (resultMessage, resultImagePath) = GetResultDetails();

            Panel resultPanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackgroundImageLayout = ImageLayout.Stretch
            };

            Label resultLabel = new Label
            {
                Text = resultMessage,
                ForeColor = Color.White,
                Font = new Font("Arial", 50, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(400, 100),
                BackColor = Color.Transparent
            };

            resultLabel.Location = new Point(
                (this.ClientSize.Width - resultLabel.Width) / 2, 20);

            resultPanel.Controls.Add(resultLabel);
            this.Controls.Add(resultPanel);

            Image resultImage = await LoadImageAsync(resultImagePath);
            resultPanel.BackgroundImage = resultImage;
        }

        private async Task<Image> LoadImageAsync(string imagePath)
        {
            return await Task.Run(() => Image.FromFile(imagePath));
        }

        // Get result Message and ImagePath
        private (string, string) GetResultDetails()
        {
            string resultMessage = "";
            string resultImagePath = "";

            string projectDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..")); // (bin\Debug)
            string imagesDirectory = Path.Combine(projectDirectory, "Images");

            switch (gameResult)
            {
                case 0:
                    resultMessage = "It's a Draw!";
                    resultImagePath = Path.Combine(imagesDirectory, "draw.jpg");
                    break;
                case 1:
                    resultMessage = "You Win!";
                    resultImagePath = Path.Combine(imagesDirectory, "victory.jpg");
                    break;
                case 2:
                    resultMessage = "You Lose!";
                    resultImagePath = Path.Combine(imagesDirectory, "loss.jpg");
                    break;
            }

            return (resultMessage, resultImagePath);
        }

        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            closeTimer.Stop();
            this.Close();
        }
    }

}
