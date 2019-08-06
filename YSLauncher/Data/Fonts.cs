using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace YSLauncher
{
    public static class Fonts
    {
        public static FontFamily Odin;
        public static void Load()
        {
            PrivateFontCollection pfc = new PrivateFontCollection();

            int fontLength = Properties.Resources.OSP_DIN.Length;
            byte[] fontdata = Properties.Resources.OSP_DIN;
            System.IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);

            pfc.AddMemoryFont(data, fontLength);
            Odin = pfc.Families[0];
        }
    }
}
