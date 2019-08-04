using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace YSLauncher
{
    public static class Extensions
    {
        #region String extensions
        public static string RemoveHTMLTags(this string text)
        {
            return Regex.Replace(text, "<.*?>", "");
        }
        public static string GetFilesize(this long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        #endregion

        #region Mathematical extensions
        public static float Clamp(this float value, float min, float max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        public static int Clamp(this int value, int min, int max)
        {
            if (value < min) value = min;
            if (value > max) value = max;
            return value;
        }
        public static float Lerp(this float from, float to, float factor)
        {
            return from * (1 - factor) + to * factor;
        }
        public static int Lerp(this int from, float to, float factor)
        {
            return (int)Math.Round(from * (1 - factor) + to * factor);
        }
        #endregion

        #region Form extensions
        public static void Toggle(this Button button, bool on)
        {
            button.Enabled = on;
            button.ForeColor = on ? Color.White : Color.FromArgb(255, 64, 64, 64);
            button.BackColor = on ? Color.HotPink : Color.DarkGray;
        }
        #endregion

        #region Graphics extensions
        public static void DrawRoundedRectangle(this Graphics graphics, Pen pen, Rectangle bounds, int cornerRadius)
        {
            using (GraphicsPath path = Util.RoundedRect(bounds, cornerRadius))
            {
                graphics.DrawPath(pen, path);
            }
        }
        #endregion
    }
}
