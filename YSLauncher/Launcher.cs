using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YSLauncher
{
    public partial class Launcher : FlatForm
    {
        public static Button PlayButton;
        public static Button InstallButton;
        public static ProgressBar ProgressBar;
        public static Label StatusLabel;

        private Size blogtabSize = new Size(250, 300);

        public Launcher()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        #region Render to backbuffer and swap buffers when ready
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
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

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Enabled = true;
            this.DownloadProgressbar.Hide();
            this.DownloadProgressLabel.Hide();

            ProgressBar = DownloadProgressbar;
            StatusLabel = DownloadProgressLabel;
            PlayButton = playButtonBig;
            InstallButton = installButton;

            FormBorderStyle = FormBorderStyle.None;
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            PlayButton.Toggle(false);
            installButton.Toggle(false);
            Task.Run(()=>loadData());
        }
        public static void EvaluateLoadedData()
        {
            if (LauncherData.BuildState == BuildState.NotDownloaded)
            {
                PlayButton.Toggle(true);
                PlayButton.Text = "Download";
                InstallButton.Toggle(false);
            }
            PlayButton.Click -= ButtonEvents.DownloadEvent;
            PlayButton.Click -= ButtonEvents.PlayEvent;
            PlayButton.Toggle(true);
            InstallButton.Toggle(true);
            PlayButton.Text = "Play";
            switch (LauncherData.BuildState)
            {
                case BuildState.NotDownloaded:
                    PlayButton.Text = "Download";
                    PlayButton.Click += ButtonEvents.DownloadEvent;
                    InstallButton.Toggle(false);
                    break;
                case BuildState.UpdateAvailable:
                    PlayButton.Click += ButtonEvents.PlayEvent;
                    InstallButton.Text = "Update";
                    break;
                case BuildState.UpToDate:
                    PlayButton.Click += ButtonEvents.PlayEvent;
                    InstallButton.Text = "Force reinstall";
                    break;
            }
        }
        async Task loadData()
        {
            LauncherData.Load();

            this.Invoke((MethodInvoker)delegate { EvaluateLoadedData(); });
            List<BlogpostData> newsData = new List<BlogpostData>();
            for (int i = 0; i < LauncherData.Posts.Length; i++)
            {
                Post post = LauncherData.Posts[i];
                BlogpostData postData = new BlogpostData();
                postData.Text = post.content.RemoveHTMLTags();
                postData.Title = post.title;
                postData.Thumbnail = Util.FitToBox(Util.GetThumbnail(post.URL), blogtabSize);
                newsData.Add(postData);
            }

            this.Invoke((MethodInvoker)delegate { instantiateBlogTabs(newsData.ToArray()); });
        }
        async Task instantiateBlogTabs(BlogpostData[] data)
        {
            int tabSpacing = 20;
            int allPostWidth = LauncherData.Posts.Length * (blogtabSize.Width + tabSpacing);
            int postOffset = 125 + (Width - allPostWidth) / 2;
            for (int i = 0; i < LauncherData.Posts.Length; i++)
            {
                await Task.Delay(100);
                Post post = LauncherData.Posts[i];
                BlogpostTab postTab = new BlogpostTab(Controls, post.URL);
                postTab.Data = data[i];
                postTab.Offset.X = 1000;
                postTab.Size = blogtabSize;
                postTab.Position = new Point(postOffset + i * (postTab.Size.Width + tabSpacing), 200);
                postTab.Draw();
                LerpTab(postTab);
            }
        }
        bool LerpTab(BlogpostTab tab)
        {
            while (tab.Offset.X != 0)
            {
                tab.Offset.X = Extensions.Lerp(tab.Offset.X, 0, 0.5f);
                tab.Draw();
                Controls.Owner.Update();
            }
            return true;
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void installButton_Click(object sender, EventArgs e)
        {
            Updater.DownloadGame();
        }
    }
}
