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
    public class FlatLabel : Label
    {
        public float GradientAngle;
        public bool DrawShadow = true;
        protected override void OnPaint(PaintEventArgs e)
        {
            Font drawFont = new Font(Fonts.Odin, Font.SizeInPoints - 1, Font.Style);
            int shadowOffset = ((int)Math.Ceiling((drawFont.SizeInPoints / 8))).Clamp(1, 8);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawString(Text, drawFont, new SolidBrush(Color.FromArgb(100, 0, 0, 0)), new Rectangle(shadowOffset, shadowOffset, Width,Height));
            e.Graphics.DrawString(Text, drawFont, new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height));
        }
    }
}
