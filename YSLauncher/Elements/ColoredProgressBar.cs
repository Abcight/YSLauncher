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
    public class ColoredProgressBar : ProgressBar
    {
        public ColoredProgressBar()
        {
            SetStyle(ControlStyles.UserPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Brush fillBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, Height), 
                                                      Color.FromArgb(ForeColor.R-50,ForeColor.G-50,ForeColor.B-50), ForeColor);

            Rectangle rec = e.ClipRectangle;

            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            rec.Height = rec.Height - 4;
            e.Graphics.FillRectangle(fillBrush, 2, 2, rec.Width, rec.Height);
            //e.Graphics.DrawRectangle(new Pen(BackColor, 2), 0, 0, Width, Height);
            e.Graphics.DrawRectangle(new Pen(BackColor,2), 0, 0, Width, Height);
        }
    }
}
