using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace YSLauncher
{
    public class FlatLabel : Label
    {
        public bool Centered;
        public bool DrawShadow = true;
        protected override void OnPaint(PaintEventArgs e)
        {
            Font drawFont = new Font(Fonts.Odin, Font.SizeInPoints - 1, Font.Style);
            int shadowOffset = ((int)Math.Ceiling((drawFont.SizeInPoints / 8))).Clamp(1, 8);

            StringFormat format = Centered ? new StringFormat() : null;
            if (Centered)
            {
                format.LineAlignment = StringAlignment.Center;
                format.Alignment = StringAlignment.Center;
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.DrawString(Text, drawFont, new SolidBrush(Util.Transparent()), new Rectangle(shadowOffset, shadowOffset, Width,Height), format);
            e.Graphics.DrawString(Text, drawFont, new SolidBrush(ForeColor), new Rectangle(0, 0, Width, Height), format);
        }
    }
}
