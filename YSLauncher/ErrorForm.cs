using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace YSLauncher
{
    public class ErrorForm : FlatForm
    {
        private string errorMessage = "";
        private System.Windows.Forms.RichTextBox richTextBox1;
        private FlatLabel flatLabel2;
        private System.Windows.Forms.Button ReportButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button CloseButton;
        private FlatLabel flatLabel1;

        public ErrorForm(string error)
        {
            errorMessage = error;
            DrawOutline = true;
        }

        private void InitializeComponent()
        {
            this.flatLabel1 = new YSLauncher.FlatLabel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.flatLabel2 = new YSLauncher.FlatLabel();
            this.ReportButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.CloseButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flatLabel1
            // 
            this.flatLabel1.AutoSize = true;
            this.flatLabel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.flatLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.flatLabel1.ForeColor = System.Drawing.SystemColors.Control;
            this.flatLabel1.Location = new System.Drawing.Point(12, 42);
            this.flatLabel1.Name = "flatLabel1";
            this.flatLabel1.Size = new System.Drawing.Size(412, 16);
            this.flatLabel1.TabIndex = 0;
            this.flatLabel1.Text = "Oh no! It seems like the launcher has encountered an error!";
            this.flatLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Orchid;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.ForeColor = System.Drawing.Color.White;
            this.richTextBox1.Location = new System.Drawing.Point(12, 61);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(407, 120);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // flatLabel2
            // 
            this.flatLabel2.AutoSize = true;
            this.flatLabel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.flatLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.flatLabel2.ForeColor = System.Drawing.SystemColors.Control;
            this.flatLabel2.Location = new System.Drawing.Point(12, 184);
            this.flatLabel2.Name = "flatLabel2";
            this.flatLabel2.Size = new System.Drawing.Size(449, 16);
            this.flatLabel2.TabIndex = 2;
            this.flatLabel2.Text = "If you want to improve the launcher, you can try reporting the bug";
            this.flatLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ReportButton
            // 
            this.ReportButton.BackColor = System.Drawing.Color.HotPink;
            this.ReportButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReportButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ReportButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ReportButton.ForeColor = System.Drawing.Color.White;
            this.ReportButton.Location = new System.Drawing.Point(125, 3);
            this.ReportButton.Name = "ReportButton";
            this.ReportButton.Size = new System.Drawing.Size(83, 26);
            this.ReportButton.TabIndex = 3;
            this.ReportButton.Text = "Report";
            this.ReportButton.UseVisualStyleBackColor = false;
            this.ReportButton.Click += new System.EventHandler(this.ReportButtonClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.94737F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.05263F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 21.05263F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 28.94737F));
            this.tableLayoutPanel1.Controls.Add(this.CloseButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.ReportButton, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 203);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(424, 32);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // CloseButton
            // 
            this.CloseButton.BackColor = System.Drawing.Color.HotPink;
            this.CloseButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CloseButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CloseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CloseButton.ForeColor = System.Drawing.Color.White;
            this.CloseButton.Location = new System.Drawing.Point(214, 3);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(83, 26);
            this.CloseButton.TabIndex = 4;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = false;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // ErrorForm
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(211)))));
            this.ClientSize = new System.Drawing.Size(431, 247);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.flatLabel2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.flatLabel1);
            this.ForeColor = System.Drawing.Color.HotPink;
            this.Name = "ErrorForm";
            this.Text = "Error Window";
            this.Load += new System.EventHandler(this.ErrorForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public static void HandleException(object sender, ThreadExceptionEventArgs e)
        {
            ErrorForm error = new ErrorForm(e.Exception.Message + e.Exception.StackTrace + Environment.NewLine);
            error.InitializeComponent();
            error.Show();
        }
        public static void HandleException(object sender, FirstChanceExceptionEventArgs e)
        {
            ErrorForm error = new ErrorForm(e.Exception.Message + e.Exception.StackTrace + Environment.NewLine);
            error.InitializeComponent();
            error.Show();
        }

        private void ReportButtonClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://forms.gle/kZBfLLdtC8SrmMer7");
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ErrorForm_Load(object sender, EventArgs e)
        {
            this.richTextBox1.Text = errorMessage;
        }
    }
}
