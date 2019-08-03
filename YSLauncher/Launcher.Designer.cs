using System.Windows.Forms;

namespace YSLauncher
{
    partial class Launcher
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.installButton = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.DownloadProgressLabel = new YSLauncher.FlatLabel();
            this.DownloadProgressbar = new YSLauncher.ColoredProgressBar();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.closeButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.minimizeButton = new System.Windows.Forms.Button();
            this.titleLabel = new System.Windows.Forms.Label();
            this.playButtonBig = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 439);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(860, 110);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.playButtonBig);
            this.panel2.Controls.Add(this.installButton);
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(252, 104);
            this.panel2.TabIndex = 2;
            // 
            // installButton
            // 
            this.installButton.AccessibleName = "downloadButton";
            this.installButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(99)))), ((int)(((byte)(187)))));
            this.installButton.CausesValidation = false;
            this.installButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.installButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.installButton.ForeColor = System.Drawing.SystemColors.Control;
            this.installButton.Location = new System.Drawing.Point(3, 69);
            this.installButton.Name = "installButton";
            this.installButton.Size = new System.Drawing.Size(246, 33);
            this.installButton.TabIndex = 4;
            this.installButton.Text = "Force reinstall";
            this.installButton.UseVisualStyleBackColor = false;
            this.installButton.Click += new System.EventHandler(this.installButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.DownloadProgressLabel);
            this.panel1.Controls.Add(this.DownloadProgressbar);
            this.panel1.Location = new System.Drawing.Point(261, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(596, 104);
            this.panel1.TabIndex = 3;
            // 
            // DownloadProgressLabel
            // 
            this.DownloadProgressLabel.AutoSize = true;
            this.DownloadProgressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.DownloadProgressLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.DownloadProgressLabel.Location = new System.Drawing.Point(0, 39);
            this.DownloadProgressLabel.Name = "DownloadProgressLabel";
            this.DownloadProgressLabel.Size = new System.Drawing.Size(121, 24);
            this.DownloadProgressLabel.TabIndex = 5;
            this.DownloadProgressLabel.Text = "Downloading";
            // 
            // DownloadProgressbar
            // 
            this.DownloadProgressbar.ForeColor = System.Drawing.Color.LightPink;
            this.DownloadProgressbar.Location = new System.Drawing.Point(4, 69);
            this.DownloadProgressbar.Name = "DownloadProgressbar";
            this.DownloadProgressbar.Size = new System.Drawing.Size(589, 32);
            this.DownloadProgressbar.TabIndex = 4;
            // 
            // closeButton
            // 
            this.closeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.closeButton.FlatAppearance.BorderSize = 0;
            this.closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DeepPink;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.closeButton.ForeColor = System.Drawing.SystemColors.Control;
            this.closeButton.Location = new System.Drawing.Point(47, 3);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(38, 29);
            this.closeButton.TabIndex = 2;
            this.closeButton.Text = "X";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.minimizeButton, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.closeButton, 1, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(798, -4);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(88, 35);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // minimizeButton
            // 
            this.minimizeButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.minimizeButton.FlatAppearance.BorderSize = 0;
            this.minimizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.minimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.minimizeButton.ForeColor = System.Drawing.SystemColors.Control;
            this.minimizeButton.Location = new System.Drawing.Point(3, 3);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(38, 29);
            this.minimizeButton.TabIndex = 3;
            this.minimizeButton.Text = "_";
            this.minimizeButton.UseVisualStyleBackColor = true;
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.titleLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.titleLabel.Location = new System.Drawing.Point(9, 9);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(231, 13);
            this.titleLabel.TabIndex = 4;
            this.titleLabel.Text = "Yandere Simulator Launcher by Abcight";
            // 
            // playButtonBig
            // 
            this.playButtonBig.FlatStyle = FlatStyle.Popup;
            this.playButtonBig.BackColor = System.Drawing.Color.HotPink;
            this.playButtonBig.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.playButtonBig.ForeColor = System.Drawing.SystemColors.Control;
            this.playButtonBig.Location = new System.Drawing.Point(3, 3);
            this.playButtonBig.Name = "playButtonBig";
            this.playButtonBig.Size = new System.Drawing.Size(246, 62);
            this.playButtonBig.TabIndex = 5;
            this.playButtonBig.Text = "Play";
            this.playButtonBig.UseVisualStyleBackColor = false;
            // 
            // Launcher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(211)))));
            this.ClientSize = new System.Drawing.Size(884, 561);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Launcher";
            this.Text = "Yandere Simulator Launcher";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        public Button installButton;
        private Panel panel2;
        private Button closeButton;
        private TableLayoutPanel tableLayoutPanel2;
        private ColoredProgressBar DownloadProgressbar;
        private Label titleLabel;
        private Button minimizeButton;
        private FlatLabel DownloadProgressLabel;
        private Button playButtonBig;
    }
}

