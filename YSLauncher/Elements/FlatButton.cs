using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YSLauncher
{
    public class FlatButton : Button
    {
        public bool DrawShadow;
        private bool hover;
        public FlatButton()
        {
            FlatAppearance.BorderSize = 0;
            FlatStyle = FlatStyle.Flat;
            this.MouseEnter += mouseEnter;
            this.MouseLeave += mouseLeave;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Size drawSize = new Size(Width - 5, Height - 5);

            Color rectangleColor = hover ? Color.FromArgb(BackColor.R - 20, BackColor.G - 20, BackColor.B - 20) : BackColor;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (DrawShadow)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100, 0, 0, 0)), 0, 0, Width, Height);
            }
            e.Graphics.FillRectangle(new SolidBrush(rectangleColor), 0, 0, drawSize.Width, drawSize.Height);

            Font drawFont = new Font(Fonts.Odin, Font.Size, Font.Style);
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Center;
            int shadowOffset = ((int)Math.Ceiling((drawFont.SizeInPoints / 8))).Clamp(1, 8);
            e.Graphics.DrawString(Text, drawFont, new SolidBrush(Color.FromArgb(100,0,0,0)), shadowOffset+Width / 2, shadowOffset+Height / 2, format);
            e.Graphics.DrawString(Text, drawFont, new SolidBrush(ForeColor), Width/2, Height/2, format);
        }
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            int factor = Size.Width * Size.Height / 500;
            Region = Region.FromHrgn(Util.CreateRoundRectRgn(0, 0, Size.Width, Size.Height, factor, factor));
        }
        private void mouseEnter(object sender, System.EventArgs e)
        {
            hover = true;
        }

        private void mouseLeave(object sender, System.EventArgs e)
        {
            hover = false;
        }
    }
}
