using CG.Web.MegaApiClient;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace YSLauncher
{
    public static class Util
    {
        #region Web utilities
        public static long GetMegaSize(string url)
        {
            MegaApiClient client = new MegaApiClient();

            client.LoginAnonymous();
            Uri fileLink = new Uri(url);
            long size = client.GetNodeFromLink(fileLink).Size;
            client.Logout();

            return size;
        }
        public static BlogPost[] GetPosts(string url, int count)
        {
            string baseurl = string.Format("http://public-api.wordpress.com/rest/v1/sites/{0}/posts",url);

            HttpClient client = new HttpClient();
            string jsonData = WebUtility.HtmlDecode(client.GetStringAsync(baseurl).Result);

            JToken token = JObject.Parse(jsonData);

            var postCount = (int)token.SelectToken("found");
            var postArray = token.SelectToken("posts");

            var sr = new JavaScriptSerializer();
            var posts = sr.Deserialize<List<BlogPost>>(postArray.ToString()).Take(3);

            return posts.ToArray();
        }
        #endregion

        #region Image utilities
        public static Image FitToBox(Image source, Size box)
        {
            float sizeRatio = (float)box.Width / source.Width;

            float width = ((float)source.Width) * sizeRatio;
            float height = ((float)source.Height) * sizeRatio;
            int roundWidth = (int)Math.Ceiling(width);
            int roundHeight = (int)Math.Ceiling(height);
            Bitmap resized = ResizeImage(source, new Size(roundWidth, roundHeight));

            Bitmap newImage = new Bitmap(box.Width, box.Height);
            Graphics canvas = Graphics.FromImage(newImage);
            canvas.DrawImage(resized, new Point(0, 0));
            canvas.Flush();
            return newImage;
        }
        public static Bitmap ResizeImage(Image imgToResize, Size size)
        {
            return new Bitmap(imgToResize, size);
        }
        public static Image GetThumbnail(string url)
        {
            try
            {
                System.Net.WebClient client = new System.Net.WebClient();
                string html = client.DownloadString(url);

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(html);

                #region Image thumbnail
                HtmlNode main = doc.DocumentNode.SelectSingleNode("//*[@class=\"entry-content\"]");
                HtmlNode imageNode = null;
                foreach (HtmlNode node in main.Descendants())
                {
                    if (node.Name == "img")
                    {
                        imageNode = node;
                        break;
                    }
                }
                if (imageNode != null)
                {
                    string imageUrl = imageNode.Attributes["src"]?.Value;
                    if (imageUrl != null)
                    {
                        using (System.Net.WebClient webClient = new System.Net.WebClient())
                        {
                            using (Stream stream = webClient.OpenRead(imageUrl))
                            {
                                return Image.FromStream(stream);
                            }
                        }
                    }
                }
                #endregion

                #region Video thumbnail
                HtmlNode videoNode = null;
                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//*[@class=\"youtube-player\"]"))
                {
                    videoNode = node;
                }
                if (videoNode != null)
                {
                    string videoURL = videoNode.Attributes["src"]?.Value;
                    if (videoURL != null)
                    {
                        using (System.Net.WebClient webClient = new System.Net.WebClient())
                        {
                            string thumbUrl = string.Format("http://img.youtube.com/vi/{0}/0.jpg", GetYouTubeId(videoURL));
                            using (Stream stream = webClient.OpenRead(thumbUrl))
                            {
                                return Image.FromStream(stream);
                            }
                        }
                    }
                }
                #endregion
            }
            catch (Exception)
            {
                return null;
            }
            return null;
        }
        public static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            Size size = new Size(diameter, diameter);
            Rectangle arc = new Rectangle(bounds.Location, size);
            GraphicsPath path = new GraphicsPath();
            path.AddArc(arc, 180, 90);
            arc.X = bounds.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = bounds.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = bounds.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }
        public static Bitmap Blurred(this Bitmap image, Int32 blurSize)
        {
            return Blur(image, new Rectangle(0, 0, image.Width, image.Height), blurSize);
        }
        private unsafe static Bitmap Blur(Bitmap image, Rectangle rectangle, Int32 blurSize)
        {
            Bitmap blurred = new Bitmap(image.Width, image.Height);

            // make an exact copy of the bitmap provided
            using (Graphics graphics = Graphics.FromImage(blurred))
                graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);

            // Lock the bitmap's bits
            BitmapData blurredData = blurred.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, blurred.PixelFormat);

            // Get bits per pixel for current PixelFormat
            int bitsPerPixel = Image.GetPixelFormatSize(blurred.PixelFormat);

            // Get pointer to first line
            byte* scan0 = (byte*)blurredData.Scan0.ToPointer();

            // look at every pixel in the blur rectangle
            for (int xx = rectangle.X; xx < rectangle.X + rectangle.Width; xx++)
            {
                for (int yy = rectangle.Y; yy < rectangle.Y + rectangle.Height; yy++)
                {
                    int avgR = 0, avgG = 0, avgB = 0;
                    int blurPixelCount = 0;

                    // average the color of the red, green and blue for each pixel in the
                    // blur size while making sure you don't go outside the image bounds
                    for (int x = xx; (x < xx + blurSize && x < image.Width); x++)
                    {
                        for (int y = yy; (y < yy + blurSize && y < image.Height); y++)
                        {
                            // Get pointer to RGB
                            byte* data = scan0 + x * blurredData.Stride + y * bitsPerPixel / 8;

                            avgB += data[0]; // Blue
                            avgG += data[1]; // Green
                            avgR += data[2]; // Red

                            blurPixelCount++;
                        }
                    }

                    avgR = avgR / blurPixelCount;
                    avgG = avgG / blurPixelCount;
                    avgB = avgB / blurPixelCount;

                    // now that we know the average for the blur size, set each pixel to that color
                    for (int x = xx; x < xx + blurSize && x < image.Width && x < rectangle.Width; x++)
                    {
                        for (int y = yy; y < yy + blurSize && y < image.Height && y < rectangle.Height; y++)
                        {
                            // Get pointer to RGB
                            byte* data = scan0 + x * blurredData.Stride + y * bitsPerPixel / 8;

                            // Change values
                            data[0] = (byte)avgB;
                            data[1] = (byte)avgG;
                            data[2] = (byte)avgR;
                        }
                    }
                }
            }

            // Unlock the bits
            blurred.UnlockBits(blurredData);

            return blurred;
        }
        #endregion

        #region String utilities
        public static string GetMegaLink(string url)
        {

            return null;
        }
        public static string GetYouTubeId(string url)
        {
            var regex = @"(?:youtube\.com\/(?:[^\/]+\/.+\/|(?:v|e(?:mbed)?|watch)\/|.*[?&amp;]v=)|youtu\.be\/)([^""&amp;?\/ ]{11})";

            var match = Regex.Match(url, regex);
             
            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return url;
        }
        #endregion

        #region Form utilities
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;

        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );
        #endregion
    }
}
