using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace YSLauncher
{
    public struct BlogpostData
    {
        public string Title;
        public string Text;
        public Image Thumbnail;
    }
    public class BlogpostTab
    {
        public Size Size;
        public Point Position;
        public Point Offset;
        public float Scale = 1f;
        public BlogpostData Data;

        private PictureBox thumbnailBox = new PictureBox();
        private Label titleLabel = new Label();
        private Label contentLabel = new Label();
        private Button continueReading = new Button();
        private Panel shadowPanel = new Panel();

        public BlogpostTab(ControlCollection control, string posturl)
        {
            #region Register elements
            control.Add(thumbnailBox);
            control.Add(titleLabel);
            control.Add(contentLabel);
            control.Add(continueReading);
            control.Add(shadowPanel);
            #endregion

            #region Set elements color
            thumbnailBox.BackColor = Color.Azure;
            titleLabel.Parent = thumbnailBox;
            titleLabel.BackColor = Color.FromArgb(100,0,0,0);
            titleLabel.ForeColor = Color.White;
            contentLabel.BackColor = Color.HotPink;
            continueReading.BackColor = Color.LightPink;
            shadowPanel.BackColor = Color.FromArgb(100,0,0,0);
            #endregion

            #region Set up the continue reading button
            continueReading.Click += delegate
            {
                System.Diagnostics.Process.Start(posturl);
            };
            continueReading.FlatStyle = FlatStyle.Flat;
            continueReading.Text = "Continue reading";
            continueReading.ForeColor = Color.White;
            continueReading.Font = new Font(continueReading.Font.FontFamily, 12, FontStyle.Bold);
            #endregion

            #region Set up the content label
            contentLabel.Font = new Font(contentLabel.Font.FontFamily, 10, FontStyle.Italic);
            contentLabel.ForeColor = Color.White;
            #endregion

            thumbnailBox.SizeMode = PictureBoxSizeMode.Normal;
        }

        public void Draw()
        {
            Size drawSize = new Size((int)Math.Ceiling(Size.Width*Scale), (int)Math.Ceiling(Size.Height*Scale));
            Point drawPos = new Point(Position.X-drawSize.Width / 2+Offset.X, Position.Y-drawSize.Height / 2+Offset.Y);

            thumbnailBox.Image = Data.Thumbnail;
            titleLabel.Text = Data.Title;
            titleLabel.Font = new Font(titleLabel.Font.FontFamily, (300 / Data.Title.Length).Clamp(10,15), FontStyle.Bold);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            contentLabel.Text = Data.Text;

            int sizeUnit = Size.Height / 20;
            shadowPanel.Size = new Size(Size.Width, sizeUnit*20);
            continueReading.Size = new Size(Size.Width, sizeUnit * 2);
            contentLabel.Size = new Size(Size.Width, sizeUnit * 8);
            thumbnailBox.Size = new Size(Size.Width, sizeUnit * 10);

            titleLabel.Size = new Size(drawSize.Width, sizeUnit*3);

            shadowPanel.Location = new Point(drawPos.X+5, drawPos.Y+5);

            thumbnailBox.Location = new Point(drawPos.X, drawPos.Y);
            contentLabel.Location = new Point(drawPos.X, thumbnailBox.Location.Y+ thumbnailBox.Size.Height);
            continueReading.Location = new Point(drawPos.X, contentLabel.Location.Y + contentLabel.Size.Height);

            titleLabel.Location = new Point(0, thumbnailBox.Size.Height-titleLabel.Size.Height);
            titleLabel.BringToFront();
            continueReading.BringToFront();
        }
    }
}
