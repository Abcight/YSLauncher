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
            base.OnPaint(e);
            //TODO: ADD SHADOWS
        }
    }
}
