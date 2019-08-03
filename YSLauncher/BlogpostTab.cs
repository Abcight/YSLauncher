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
    public class BlogpostTab
    {
        public float Scale = 1;
        public Size Size;
        public Point Position;
        public Point Offset;
        public Image Thumbnail;
        public string Title;
        public string Text;
        private PictureBox thumbnailBox = new PictureBox();
        private Label titleLabel = new Label();
        private Label textPreviewLabel = new Label();
        private Button continueReading = new Button();
        private Panel shadowPanel = new Panel();

        public BlogpostTab(ControlCollection control, string posturl)
        {
            control.Add(thumbnailBox);
            control.Add(titleLabel);
            control.Add(textPreviewLabel);
            control.Add(continueReading);
            control.Add(shadowPanel);

            thumbnailBox.BackColor = Color.Azure;
            titleLabel.Parent = thumbnailBox;
            titleLabel.BackColor = Color.FromArgb(100,0,0,0);
            titleLabel.ForeColor = Color.White;
            textPreviewLabel.BackColor = Color.HotPink;
            continueReading.BackColor = Color.LightPink;
            shadowPanel.BackColor = Color.FromArgb(100,0,0,0);

            continueReading.Click += delegate
            {
                System.Diagnostics.Process.Start(posturl);
            };
            continueReading.FlatStyle = FlatStyle.Flat;
            continueReading.Text = "Continue reading";
            continueReading.ForeColor = Color.White;
            continueReading.Font = new Font(continueReading.Font.FontFamily, 12, FontStyle.Bold);

            textPreviewLabel.Font = new Font(textPreviewLabel.Font.FontFamily, 10, FontStyle.Italic);
            textPreviewLabel.ForeColor = Color.White;

            thumbnailBox.SizeMode = PictureBoxSizeMode.Normal;
        }
        public void Draw()
        {
            Size drawSize = new Size((int)Math.Ceiling(Size.Width*Scale), (int)Math.Ceiling(Size.Height*Scale));
            Point drawPos = new Point(Position.X-drawSize.Width / 2+Offset.X, Position.Y-drawSize.Height / 2+Offset.Y);

            thumbnailBox.Image = Thumbnail;
            titleLabel.Text = Title;
            titleLabel.Font = new Font(titleLabel.Font.FontFamily, (300 / Title.Length).Clamp(10,15), FontStyle.Bold);
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;
            textPreviewLabel.Text = Text;

            int sizeUnit = drawSize.Height / 20;
            shadowPanel.Size = new Size(drawSize.Width, sizeUnit*20);
            continueReading.Size = new Size(drawSize.Width, sizeUnit * 2);
            textPreviewLabel.Size = new Size(drawSize.Width, sizeUnit * 8);
            thumbnailBox.Size = new Size(drawSize.Width, sizeUnit * 10);

            titleLabel.Size = new Size(drawSize.Width, sizeUnit*3);

            shadowPanel.Location = new Point(drawPos.X+5, drawPos.Y+5);

            thumbnailBox.Location = new Point(drawPos.X, drawPos.Y);
            textPreviewLabel.Location = new Point(drawPos.X, thumbnailBox.Location.Y+ thumbnailBox.Size.Height);
            continueReading.Location = new Point(drawPos.X, textPreviewLabel.Location.Y + textPreviewLabel.Size.Height);

            titleLabel.Location = new Point(0, thumbnailBox.Size.Height-titleLabel.Size.Height);
            titleLabel.BringToFront();
            continueReading.BringToFront();
        }
    }
}
