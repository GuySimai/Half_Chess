namespace Client_Half_Chess
{
    partial class GameRecording
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GameRecording));
            this.toolStripUp = new System.Windows.Forms.ToolStrip();
            this.Exit = new System.Windows.Forms.ToolStripButton();
            this.toolStripUp.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripUp
            // 
            this.toolStripUp.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripUp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Exit});
            this.toolStripUp.Location = new System.Drawing.Point(0, 0);
            this.toolStripUp.Name = "toolStripUp";
            this.toolStripUp.Size = new System.Drawing.Size(800, 27);
            this.toolStripUp.TabIndex = 1;
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
            // GameRecording
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.toolStripUp);
            this.Name = "GameRecording";
            this.Text = "GameRecording";
            this.toolStripUp.ResumeLayout(false);
            this.toolStripUp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStripUp;
        private System.Windows.Forms.ToolStripButton Exit;
    }
}