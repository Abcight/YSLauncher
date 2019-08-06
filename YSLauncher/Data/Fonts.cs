using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

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
            IntPtr data = Marshal.AllocCoTaskMem(fontLength);
            Marshal.Copy(fontdata, 0, data, fontLength);

            pfc.AddMemoryFont(data, fontLength);
            Odin = pfc.Families[0];
        }
    }
}
