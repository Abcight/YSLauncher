using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
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
        public static Launcher Instance;
        public static Button PlayButton;
        public static Button InstallButton;
        public static ProgressBar ProgressBar;
        public static Label StatusLabel;

        private List<BlogpostTab> blogTabs = new List<BlogpostTab>();

        public Launcher()
        {
            Fonts.Load();
            InitializeComponent();
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

        private void Form1_Load(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.FirstChanceException += ErrorForm.HandleException;
            this.Enabled = true;
            this.DownloadProgressbar.Hide();
            this.DownloadProgressLabel.Hide();

            #region Assign globals
            ProgressBar = DownloadProgressbar;
            StatusLabel = DownloadProgressLabel;
            PlayButton = playButtonBig;
            InstallButton = installButton;
            #endregion

            #region Assign fonts
            PlayButton.Font = new Font(Fonts.Odin, PlayButton.Font.SizeInPoints, FontStyle.Bold);
            installButton.Font = new Font(Fonts.Odin, installButton.Font.SizeInPoints, FontStyle.Regular);
            #endregion

            ((FlatButton)PlayButton).DrawShadow = true;
            ((FlatButton)InstallButton).DrawShadow = true;

            FormBorderStyle = FormBorderStyle.None;
        }
        private void Form1_Shown(object sender, EventArgs e)
        {
            PlayButton.Text = "Loading...";
            PlayButton.Toggle(false);
            installButton.Toggle(false);
            Task.Run(()=>loadData());
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal && blogTabs.Count!=0)
            {
                int tabSpacing = Settings.BlogTabSpacing;
                int allPostWidth = LauncherData.Posts.Length * (Settings.BlogTabSize.Width + tabSpacing);
                int postOffset = Settings.BlogTabSize.Width / 2 + (Width - allPostWidth) / 2;
                for (int i = 0; i < blogTabs.Count; i++)
                {
                    blogTabs[i].Position = new Point(postOffset + i * (blogTabs[i].Size.Width + tabSpacing), 200);
                    blogTabs[i].Draw();
                }
            }
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
                BlogPost post = LauncherData.Posts[i];
                BlogpostData postData = new BlogpostData();
                postData.Text = post.Content.RemoveHTMLTags();
                postData.Title = post.Title;
                postData.Thumbnail = Util.FitToBox(Util.GetThumbnail(post.Url), Settings.BlogTabSize);
                newsData.Add(postData);
            }

            this.Invoke((MethodInvoker)delegate { instantiateBlogTabs(newsData.ToArray()); });
        }
        async Task instantiateBlogTabs(BlogpostData[] data)
        {
            int tabSpacing = 20;
            int allPostWidth = LauncherData.Posts.Length * (Settings.BlogTabSize.Width + tabSpacing);
            int postOffset = Settings.BlogTabSize.Width/2 + (Width - allPostWidth) / 2;
            for (int i = 0; i < LauncherData.Posts.Length; i++)
            {
                await Task.Delay(100);
                BlogPost post = LauncherData.Posts[i];
                BlogpostTab postTab = new BlogpostTab(Controls, post.Url);
                postTab.Data = data[i];
                postTab.Offset.X = 1000;
                postTab.Size = Settings.BlogTabSize;
                postTab.Position = new Point(postOffset + i * (Settings.BlogTabSize.Width+ tabSpacing), 200);
                postTab.Draw();
                blogTabs.Add(postTab);
                LerpTab(postTab);
            }
            Controls.Owner.Update();
        }
        bool LerpTab(BlogpostTab tab)
        {
            while (tab.Offset.X != 0)
            {
                tab.Offset.X = Extensions.Lerp(tab.Offset.X, 0, 0.5f);
                tab.Draw();
                Controls.Owner.Update();
            }
            Controls.Owner.Update();
            return true;
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

        private void DownloadPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Util.ReleaseCapture();
                Util.SendMessage(Handle, Util.WM_NCLBUTTONDOWN, Util.HTCAPTION, 0);
            }
        }

        private void playButtonBig_Click(object sender, EventArgs e)
        {

        }
    }
}
