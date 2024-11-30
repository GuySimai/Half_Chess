using System;
using System.Windows.Forms;

namespace Client_Half_Chess
{
    public partial class Rules : Form
    {
        public Rules()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.KeyDown += Rules_KeyDown;
            this.MaximizeBox = false;
        }

        // ENTER
        private void Rules_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OK_Click(sender, e);
            }
        }

        private void OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
