using System.Drawing;
using System.Windows.Forms;

namespace YSLauncher
{
    public class Shadow : PictureBox
    {
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            for(int x = 0; x< 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    pe.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(5,0,0,0)), x, y, Size.Width- 10, Size.Height- 10);
                }
            }
        }
    }
}
