using System;
using System.Collections.Generic;
using System.Drawing;
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
            Rectangle rec = e.ClipRectangle;

            rec.Width = (int)(rec.Width * ((double)Value / Maximum)) - 4;
            if (ProgressBarRenderer.IsSupported)
                ProgressBarRenderer.DrawHorizontalBar(e.Graphics, e.ClipRectangle);
            rec.Height = rec.Height - 4;
            e.Graphics.FillRectangle(new SolidBrush(ForeColor), 2, 2, rec.Width, rec.Height);
            e.Graphics.DrawRectangle(new Pen(BackColor, 2), 0, 0, Width, Height);
        }
    }
}
