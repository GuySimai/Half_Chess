using Client_Half_Chess.Models;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client_Half_Chess
{
    public partial class WelcomeForm : Form
    {
        // Variables and constants
        private static HttpClient client = new HttpClient();
        private const string PATH = "https://localhost:7054/";
        private const string TO_ALL_USERS = "api/TblUsers/";

        public WelcomeForm()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog; // It is not possible to change the size of the form.
            this.KeyDown += WelcomeForm_KeyDown;
            this.MaximizeBox = false;

            comboBoxColor.Items.AddRange(new string[] { "White", "Black" });
            comboBoxColor.SelectedIndex = 0;
            comboBoxTime.Items.AddRange(new string[] { "10", "15", "20", "30", "40", "60", "80", "100" });
            comboBoxTime.SelectedIndex = 2;

            // To ignore SSL certificate.
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
            (sender, certificate, chain, sslPolicyErrors) => true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            client.BaseAddress = new Uri(PATH);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            TblDataGridView.DataSource = TblBindingSource;

        }

        // Update TblBindingSource
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                UpdateUserFromTblBindingSource(sender, e);
            }
            else
            {
                TblBindingSource.DataSource = null;
            }

        }

        private async void UpdateUserFromTblBindingSource(object sender, EventArgs e)
        {
            string TO_ONE_USER = TO_ALL_USERS + textBox1.Text;
            User user = await GetUserByIdAsync(PATH + TO_ONE_USER);
            TblBindingSource.DataSource = user;
        }

        private async Task<User> GetUserByIdAsync(string path)
        {
            User user = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();
            }
            return user;
        }

        // Lets play
        private async void Lets_play_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                string TO_ONE_USER = TO_ALL_USERS + textBox1.Text;
                User user = await GetUserByIdAsync(PATH + TO_ONE_USER);

                if (user != null)
                {
                    string selectedColor = comboBoxColor.Text;
                    Color color = Color.FromName(selectedColor);
                    int selectedTurnTime;
                    int.TryParse(comboBoxTime.Text, out selectedTurnTime);

                    GameUserServer gameForm = new GameUserServer(color, selectedTurnTime, user);
                    this.Hide();
                    gameForm.ShowDialog();
                    this.Show();
                }
                else
                {
                    CustomMessageBox.Show("User not found. Please check the ID.", 1);
                }
            }
            else
            {

                CustomMessageBox.Show("Please enter a user ID.", 1);
            }
        }

        // ENTER
        private void WelcomeForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Lets_play_Click(sender, e);
            }
        }

        private void Rules_Click(object sender, EventArgs e)
        {
            Rules rules = new Rules();
            rules.ShowDialog();
        }

        private void CreateANewUser_Click(object sender, EventArgs e)
        {
            string url = "https://localhost:7054/Users/Create";
            Process.Start(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            });
        }

        private void Recordings_Click(object sender, EventArgs e)
        {
            Recordings recordings = new Recordings();
            this.Hide();
            recordings.ShowDialog();
            this.Show();
        }
    }
}
