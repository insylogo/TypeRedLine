namespace TypeRedLine
{
    partial class Main
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
            System.Windows.Forms.BindingSource bindingSource1;
            this.statusRace = new System.Windows.Forms.StatusStrip();
            this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.pnlMain = new System.Windows.Forms.SplitContainer();
            this.txtTypingBox = new System.Windows.Forms.TextBox();
            this.rtbRaceText = new System.Windows.Forms.RichTextBox();
            this.lblBestWpm = new System.Windows.Forms.Label();
            this.lblCurrentWpm = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.statusRace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.Panel1.SuspendLayout();
            this.pnlMain.Panel2.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // statusRace
            // 
            this.statusRace.Dock = System.Windows.Forms.DockStyle.Top;
            this.statusRace.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progressBar});
            this.statusRace.Location = new System.Drawing.Point(0, 0);
            this.statusRace.Name = "statusRace";
            this.statusRace.Size = new System.Drawing.Size(479, 22);
            this.statusRace.SizingGrip = false;
            this.statusRace.TabIndex = 0;
            this.statusRace.Text = "statusStrip1";
            // 
            // progressBar
            // 
            this.progressBar.MarqueeAnimationSpeed = 10;
            this.progressBar.Maximum = 256;
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(475, 16);
            this.progressBar.Step = 5;
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.IsSplitterFixed = true;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // pnlMain.Panel1
            // 
            this.pnlMain.Panel1.BackColor = System.Drawing.SystemColors.HighlightText;
            this.pnlMain.Panel1.Controls.Add(this.comboBox1);
            this.pnlMain.Panel1.Controls.Add(this.txtTypingBox);
            this.pnlMain.Panel1.Controls.Add(this.rtbRaceText);
            // 
            // pnlMain.Panel2
            // 
            this.pnlMain.Panel2.Controls.Add(this.lblBestWpm);
            this.pnlMain.Panel2.Controls.Add(this.lblCurrentWpm);
            this.pnlMain.Panel2.Controls.Add(this.btnStart);
            this.pnlMain.Panel2.Controls.Add(this.statusRace);
            this.pnlMain.Size = new System.Drawing.Size(479, 454);
            this.pnlMain.SplitterDistance = 312;
            this.pnlMain.TabIndex = 0;
            // 
            // txtTypingBox
            // 
            this.txtTypingBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTypingBox.Location = new System.Drawing.Point(0, 18);
            this.txtTypingBox.Margin = new System.Windows.Forms.Padding(0);
            this.txtTypingBox.Name = "txtTypingBox";
            this.txtTypingBox.Size = new System.Drawing.Size(479, 26);
            this.txtTypingBox.TabIndex = 1;
            this.txtTypingBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.txtTypingBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTypingBox_KeyDown);
            this.txtTypingBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTypingBox_KeyUp);
            // 
            // rtbRaceText
            // 
            this.rtbRaceText.BackColor = System.Drawing.Color.White;
            this.rtbRaceText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbRaceText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.rtbRaceText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbRaceText.Location = new System.Drawing.Point(0, 44);
            this.rtbRaceText.Name = "rtbRaceText";
            this.rtbRaceText.ReadOnly = true;
            this.rtbRaceText.Size = new System.Drawing.Size(479, 268);
            this.rtbRaceText.TabIndex = 0;
            this.rtbRaceText.TabStop = false;
            this.rtbRaceText.Text = "";
            // 
            // lblBestWpm
            // 
            this.lblBestWpm.AutoSize = true;
            this.lblBestWpm.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBestWpm.Location = new System.Drawing.Point(124, 26);
            this.lblBestWpm.Name = "lblBestWpm";
            this.lblBestWpm.Size = new System.Drawing.Size(97, 18);
            this.lblBestWpm.TabIndex = 2;
            this.lblBestWpm.Text = "Best Score:";
            this.lblBestWpm.Visible = false;
            // 
            // lblCurrentWpm
            // 
            this.lblCurrentWpm.AutoSize = true;
            this.lblCurrentWpm.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentWpm.Location = new System.Drawing.Point(13, 26);
            this.lblCurrentWpm.Name = "lblCurrentWpm";
            this.lblCurrentWpm.Size = new System.Drawing.Size(54, 18);
            this.lblCurrentWpm.TabIndex = 1;
            this.lblCurrentWpm.Text = "WPM:";
            this.lblCurrentWpm.Visible = false;
            // 
            // btnStart
            // 
            this.btnStart.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnStart.Location = new System.Drawing.Point(3, 45);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(473, 90);
            this.btnStart.TabIndex = 0;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(147, 0);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnStart;
            this.ClientSize = new System.Drawing.Size(479, 454);
            this.Controls.Add(this.pnlMain);
            this.Name = "Main";
            this.Text = "Typing RedLine";
            this.statusRace.ResumeLayout(false);
            this.statusRace.PerformLayout();
            this.pnlMain.Panel1.ResumeLayout(false);
            this.pnlMain.Panel1.PerformLayout();
            this.pnlMain.Panel2.ResumeLayout(false);
            this.pnlMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(bindingSource1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusRace;
        private System.Windows.Forms.SplitContainer pnlMain;
        private System.Windows.Forms.TextBox txtTypingBox;
        private System.Windows.Forms.RichTextBox rtbRaceText;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ToolStripProgressBar progressBar;
        private System.Windows.Forms.Label lblCurrentWpm;
        private System.Windows.Forms.Label lblBestWpm;
        private System.Windows.Forms.ComboBox comboBox1;


    }
}

