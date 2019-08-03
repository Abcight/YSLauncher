using CG.Web.MegaApiClient;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

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
        public static Post[] GetPosts(string url, int count)
        {
            using (System.Net.WebClient webClient = new System.Net.WebClient())
            {
                string blogUrl = string.Format("https://public-api.wordpress.com/rest/v1.1/sites/{0}/posts/", url);
                string response = webClient.DownloadString(blogUrl);
                Rootobject blogPosts = JsonConvert.DeserializeObject<Rootobject>(response);

                List<Post> recentPosts = blogPosts.posts.OrderBy(o => o.date).ToList();
                recentPosts.Reverse();
                List<Post> loadedPosts = new List<Post>();
                for (int i = 0; i < count; i++)
                {
                    if (recentPosts.Count >= i)
                    {
                        loadedPosts.Add(recentPosts[i]);
                    }
                }
                return loadedPosts.ToArray();
            }
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
    }
}
