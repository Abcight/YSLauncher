using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace YSLauncher
{
    public class FlatForm : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // height of ellipse
            int nHeightEllipse // width of ellipse
        );

        protected override CreateParams CreateParams
        {
            get
            {
                const int CS_DROPSHADOW = 0x20000;
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100,0,0,0)), 5, 0, Width, 35);
            e.Graphics.FillRectangle(Brushes.HotPink, 0, 0, Width, 30);
        }
    }
}
