using System;
using System.Drawing;
using System.Windows.Forms;

namespace Client_Half_Chess
{
    public partial class Promotion : Form
    {
        public string SelectedPiece { get; private set; }

        public Promotion()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new string[] { "Bishop", "Knight", "Rook" });
            comboBox1.SelectedIndex = 0;
        }

        // Paint border form
        protected override void OnPaint(PaintEventArgs e)
        {
            Color borderColor = Color.Gray;
            int borderWidth = 5;
            using (Pen borderPen = new Pen(borderColor, borderWidth))
            {
                e.Graphics.DrawRectangle(borderPen, 0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1);
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            string selectedValue = comboBox1.SelectedItem?.ToString();
            if (!string.IsNullOrEmpty(selectedValue))
            {
                SelectedPiece = selectedValue;
            }
            this.Close();
        }
    }
}
