using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace YSLauncher
{
    public class FlatForm : Form
    {

        public bool DrawOutline;

        #region Make borders round
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse, 
            int nHeightEllipse 
        );
        #endregion

        #region Enable form dragging
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;
        #endregion

        public FlatForm()
        {
            this.SetStyle(
    ControlStyles.AllPaintingInWmPaint | 
    ControlStyles.UserPaint | 
    ControlStyles.DoubleBuffer, 
    true);
            FormBorderStyle = FormBorderStyle.None;
            MaximizeBox = false;
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            if(DrawOutline) e.Graphics.DrawRectangle(new Pen(ForeColor, 10), 0, 0, Width, Height);
            e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(100,0,0,0)), 5, 0, Width, 35);
            e.Graphics.FillRectangle(new SolidBrush(ForeColor), 0, 0, Width, 30);
            e.Graphics.DrawString(Text, new Font(DefaultFont.FontFamily, 10, FontStyle.Bold), Brushes.White, new Point(10, 8));
        }
    }
}
