using System.Drawing;
using System.Windows.Forms;

namespace Client_Half_Chess
{
    public partial class CustomMessageBox : Form
    {
        private Button okButton;
        private Button yesButton;
        private Button noButton;
        private Label messageLabel;
        private bool userChoice;

        // Constructor with type (1 for OK, 2 for Yes/No)
        public CustomMessageBox(string message, int type)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new Size(400, 200);
            this.BackColor = Color.FromArgb(64, 64, 64);

            messageLabel = new Label();
            messageLabel.Text = message;
            messageLabel.ForeColor = Color.White;
            messageLabel.Location = new Point(20, 30);
            messageLabel.Size = new Size(360, 60);
            messageLabel.TextAlign = ContentAlignment.MiddleCenter;
            messageLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            this.Controls.Add(messageLabel);

            if (type == 1)
            {
                okButton = new Button();
                okButton.Text = "OK";
                okButton.ForeColor = Color.White;
                okButton.BackColor = Color.LimeGreen;
                okButton.Size = new Size(100, 40);
                okButton.Location = new Point(150, 100);
                okButton.Click += (sender, e) => this.Close();
                this.Controls.Add(okButton);
            }
            else if (type == 2)
            {
                yesButton = new Button();
                yesButton.Text = "Yes";
                yesButton.ForeColor = Color.White;
                yesButton.BackColor = Color.IndianRed;
                yesButton.Size = new Size(100, 40);
                yesButton.Location = new Point(70, 100);
                yesButton.Click += (sender, e) => { userChoice = true; this.Close(); };
                this.Controls.Add(yesButton);

                noButton = new Button();
                noButton.Text = "No";
                noButton.ForeColor = Color.White;
                noButton.BackColor = Color.LimeGreen;
                noButton.Size = new Size(100, 40);
                noButton.Location = new Point(230, 100);
                noButton.Click += (sender, e) => { userChoice = false; this.Close(); };
                this.Controls.Add(noButton);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Color borderColor = Color.Gray;
            int borderWidth = 5;

            using (Pen borderPen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawRectangle(borderPen, 0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1);
            }
        }

        public static bool Show(string message, int type)
        {
            CustomMessageBox cmb = new CustomMessageBox(message, type);
            cmb.ShowDialog();
            return cmb.userChoice;
        }
    }
}
