namespace Client_Half_Chess
{
    partial class GameUserServer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameUserServer));
            this.toolStripUp = new System.Windows.Forms.ToolStrip();
            this.Exit = new System.Windows.Forms.ToolStripButton();
            this.Rules = new System.Windows.Forms.ToolStripButton();
            this.Clock = new System.Windows.Forms.ToolStripLabel();
            this.TimeLeft = new System.Windows.Forms.ToolStripLabel();
            this.Draw = new System.Windows.Forms.ToolStripButton();
            this.toolStripUp.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripUp
            // 
            this.toolStripUp.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripUp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Exit,
            this.Rules,
            this.Clock,
            this.TimeLeft,
            this.Draw});
            this.toolStripUp.Location = new System.Drawing.Point(0, 0);
            this.toolStripUp.Name = "toolStripUp";
            this.toolStripUp.Size = new System.Drawing.Size(800, 27);
            this.toolStripUp.TabIndex = 0;
            this.toolStripUp.Text = "toolStripUp";
            // 
            // Exit
            // 
            this.Exit.BackColor = System.Drawing.Color.Red;
            this.Exit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Exit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Exit.Image = ((System.Drawing.Image)(resources.GetObject("Exit.Image")));
            this.Exit.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Exit.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.Exit.Name = "Exit";
            this.Exit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Exit.Size = new System.Drawing.Size(23, 21);
            this.Exit.Text = "X";
            this.Exit.ToolTipText = "Exit";
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // Rules
            // 
            this.Rules.BackColor = System.Drawing.Color.LimeGreen;
            this.Rules.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Rules.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Rules.Image = ((System.Drawing.Image)(resources.GetObject("Rules.Image")));
            this.Rules.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Rules.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.Rules.Name = "Rules";
            this.Rules.Size = new System.Drawing.Size(41, 21);
            this.Rules.Text = "Rules";
            this.Rules.Click += new System.EventHandler(this.Rules_Click);
            // 
            // Clock
            // 
            this.Clock.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.Clock.Font = new System.Drawing.Font("Adobe Fan Heiti Std B", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clock.ForeColor = System.Drawing.Color.White;
            this.Clock.Name = "Clock";
            this.Clock.Size = new System.Drawing.Size(49, 24);
            this.Clock.Text = "00:00";
            // 
            // TimeLeft
            // 
            this.TimeLeft.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TimeLeft.Font = new System.Drawing.Font("David", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLeft.ForeColor = System.Drawing.Color.White;
            this.TimeLeft.Name = "TimeLeft";
            this.TimeLeft.Size = new System.Drawing.Size(116, 24);
            this.TimeLeft.Text = "User Time Left:";
            // 
            // Draw
            // 
            this.Draw.BackColor = System.Drawing.Color.LimeGreen;
            this.Draw.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.Draw.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Draw.Image = ((System.Drawing.Image)(resources.GetObject("Draw.Image")));
            this.Draw.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Draw.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
            this.Draw.Name = "Draw";
            this.Draw.Size = new System.Drawing.Size(41, 21);
            this.Draw.Text = "Draw";
            this.Draw.Click += new System.EventHandler(this.Draw_Click);
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStripUp);
            this.Name = "GameForm";
            this.Text = "Form";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.toolStripUp.ResumeLayout(false);
            this.toolStripUp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripUp;
        private System.Windows.Forms.ToolStripButton Exit;
        private System.Windows.Forms.ToolStripButton Rules;
        private System.Windows.Forms.ToolStripLabel TimeLeft;
        private System.Windows.Forms.ToolStripLabel Clock;
        private System.Windows.Forms.ToolStripButton Draw;
    }
}