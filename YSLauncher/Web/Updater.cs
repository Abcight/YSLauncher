using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YSLauncher
{
    public class Updater
    {
        private const string UrlList = "http://yanderesimulator.com/urls.txt";
        private const string DownloadURL = "https://dl.yanderesimulator.com/latest.zip";

        private static string statusFormat;
        private static long fileSize;

        private static string contentZipPath;
        private static string unzippedGamePath;

        public static void DownloadGame()
        {
            contentZipPath = LauncherData.DataDirectoryPath + "/content.zip";
            unzippedGamePath = LauncherData.DataDirectoryPath + "/Game";

            Launcher.PlayButton.Toggle(false);
            Launcher.InstallButton.Toggle(false);
            Launcher.StatusLabel.Show();
            Launcher.StatusLabel.Text = "Fetching file size...";

            #region Get filesize from mega
            WebClient client = new WebClient();
            string urlPage = client.DownloadString(UrlList);
            string megaUrl = null;
            using (StringReader reader = new StringReader(urlPage))
            {
                string line = string.Empty;
                while (line != null)
                {
                    line = reader.ReadLine();
                    if (line != null)
                    {
                        string url = line.Substring(line.IndexOf("https"));
                        if (url.StartsWith("https://mega.nz")) megaUrl = url;
                    }
                }
            }

            fileSize = Util.GetMegaSize(megaUrl);
            #endregion

            if (File.Exists(contentZipPath)) File.Delete(contentZipPath);
            if (Directory.Exists(unzippedGamePath)) Directory.Delete(unzippedGamePath, true);

            statusFormat = "Downloading {0}/" + fileSize.GetFilesize();

            Launcher.ProgressBar.Show();
            using (WebClient wc = new WebClient())
            {
                wc.Proxy = null;
                wc.DownloadProgressChanged += progressChangedHandler;
                wc.DownloadFileCompleted += onDownloadFinish;
                wc.DownloadFileAsync(new Uri(DownloadURL), contentZipPath);
            }
        }
        public static void progressChangedHandler(object sender, DownloadProgressChangedEventArgs e)
        {
            Launcher.StatusLabel.Text = string.Format(statusFormat, e.BytesReceived.GetFilesize());
            Launcher.ProgressBar.Value = (int)(((float)e.BytesReceived/(float)fileSize)*100);
            Launcher.ProgressBar.Update();
        }
        private static void onDownloadFinish(object sender, AsyncCompletedEventArgs e)
        {
            Launcher.StatusLabel.Text = "Unzipping...";
            Task.Run(()=>UnzipGame());
        }
        public static async Task UnzipGame()
        {
            if(Directory.Exists(unzippedGamePath))
            {
                Directory.Delete(unzippedGamePath, true);
            }
            ZipFile.ExtractToDirectory(contentZipPath, unzippedGamePath);
            File.Delete(contentZipPath);

            Launcher.Instance.Invoke(new MethodInvoker(() =>
            {
                Launcher.ProgressBar.Hide();
                Launcher.StatusLabel.Hide();

                LauncherData.Data.CurrentVersion = LauncherData.Data.NewestVersion;
                LauncherData.BuildState = BuildState.UpToDate;
                LauncherData.Flush();

                Launcher.EvaluateLoadedData();
            }));
        }
    }
}
