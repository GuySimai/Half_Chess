namespace Client_Half_Chess
{
    partial class Recordings
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Recordings));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStripUp = new System.Windows.Forms.ToolStrip();
            this.Exit = new System.Windows.Forms.ToolStripButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Run_Recording = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.TblDataGridView = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.TblBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.comboBoxOrderBy = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.toolStripUp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TblDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TblBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(497, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(76, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 31;
            this.pictureBox1.TabStop = false;
            // 
            // toolStripUp
            // 
            this.toolStripUp.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripUp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Exit});
            this.toolStripUp.Location = new System.Drawing.Point(0, 0);
            this.toolStripUp.Name = "toolStripUp";
            this.toolStripUp.Size = new System.Drawing.Size(731, 27);
            this.toolStripUp.TabIndex = 30;
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
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(295, 335);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(146, 20);
            this.textBox1.TabIndex = 27;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(292, 317);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 15);
            this.label3.TabIndex = 25;
            this.label3.Text = "Game ID:";
            // 
            // Run_Recording
            // 
            this.Run_Recording.BackColor = System.Drawing.Color.LimeGreen;
            this.Run_Recording.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Run_Recording.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Run_Recording.ForeColor = System.Drawing.Color.White;
            this.Run_Recording.Location = new System.Drawing.Point(290, 371);
            this.Run_Recording.Name = "Run_Recording";
            this.Run_Recording.Size = new System.Drawing.Size(155, 28);
            this.Run_Recording.TabIndex = 24;
            this.Run_Recording.Text = "Run the recording!";
            this.Run_Recording.UseVisualStyleBackColor = false;
            this.Run_Recording.Click += new System.EventHandler(this.Run_Recording_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(235, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(272, 55);
            this.label2.TabIndex = 23;
            this.label2.Text = "Half-Chess";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(132, 157);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(0, 21);
            this.comboBox2.TabIndex = 22;
            // 
            // TblDataGridView
            // 
            this.TblDataGridView.AllowUserToAddRows = false;
            this.TblDataGridView.AllowUserToDeleteRows = false;
            this.TblDataGridView.AllowUserToResizeColumns = false;
            this.TblDataGridView.AllowUserToResizeRows = false;
            this.TblDataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TblDataGridView.BackgroundColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.TblDataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.TblDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.TblDataGridView.DefaultCellStyle = dataGridViewCellStyle2;
            this.TblDataGridView.EnableHeadersVisualStyles = false;
            this.TblDataGridView.GridColor = System.Drawing.Color.White;
            this.TblDataGridView.Location = new System.Drawing.Point(81, 114);
            this.TblDataGridView.MultiSelect = false;
            this.TblDataGridView.Name = "TblDataGridView";
            this.TblDataGridView.ReadOnly = true;
            this.TblDataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.TblDataGridView.RowHeadersVisible = false;
            this.TblDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.TblDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.TblDataGridView.ShowEditingIcon = false;
            this.TblDataGridView.Size = new System.Drawing.Size(565, 190);
            this.TblDataGridView.TabIndex = 21;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(288, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 31);
            this.label1.TabIndex = 32;
            this.label1.Text = "Recordings";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // comboBoxOrderBy
            // 
            this.comboBoxOrderBy.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.comboBoxOrderBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxOrderBy.ForeColor = System.Drawing.Color.White;
            this.comboBoxOrderBy.FormattingEnabled = true;
            this.comboBoxOrderBy.Location = new System.Drawing.Point(84, 321);
            this.comboBoxOrderBy.Name = "comboBoxOrderBy";
            this.comboBoxOrderBy.Size = new System.Drawing.Size(86, 21);
            this.comboBoxOrderBy.TabIndex = 34;
            this.comboBoxOrderBy.SelectedIndexChanged += new System.EventHandler(this.comboBoxOrderBy_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(81, 303);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 15);
            this.label5.TabIndex = 33;
            this.label5.Text = "Order by:";
            // 
            // Recordings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(731, 442);
            this.Controls.Add(this.comboBoxOrderBy);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.toolStripUp);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Run_Recording);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.TblDataGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Recordings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Recordings";
            this.Load += new System.EventHandler(this.Recordings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.toolStripUp.ResumeLayout(false);
            this.toolStripUp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TblDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TblBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStrip toolStripUp;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Run_Recording;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.DataGridView TblDataGridView;
        private System.Windows.Forms.ToolStripButton Exit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.BindingSource TblBindingSource;
        private System.Windows.Forms.ComboBox comboBoxOrderBy;
        private System.Windows.Forms.Label label5;
    }
}