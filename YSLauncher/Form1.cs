using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YSLauncher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }
        
        private void downloadButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine(Util.GetMegaSize("https://mega.nz/#!1sJxXAbZ!C5244bPDvpAkaEzpI0NEsEARGngAV9YzLewFPsrywzE").GetFilesize());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Enabled = false;
            LauncherData.Load();
            this.Enabled = true;
            InstantiateBlogTabs();
        }
        async Task InstantiateBlogTabs()
        {
            int tabSpacing = 20;
            int allPostWidth = LauncherData.Posts.Length * (250 + tabSpacing);
            int postOffset = 125+(Width - allPostWidth) / 2;
            for (int i = 0; i < LauncherData.Posts.Length; i++)
            {
                await Task.Delay(100);
                Post post = LauncherData.Posts[i];
                BlogpostTab postTab = new BlogpostTab(Controls, post.URL);
                postTab.Thumbnail = Util.FitToBox(Util.GetThumbnail(post.URL),new Size(250,300));
                postTab.Offset.X = 1000;
                postTab.Title = post.title;
                postTab.Text = post.content.RemoveHTMLTags();
                postTab.Size = new Size(250, 300);
                postTab.Position = new Point(postOffset + i * (postTab.Size.Width + tabSpacing), 180);
                postTab.Draw();
                LerpTab(postTab);
            }
        }
        bool LerpTab(BlogpostTab tab)
        {
            while (tab.Offset.X != 0)
            {
                tab.Offset.X = (int)Util.Lerp(tab.Offset.X, 0, 0.5f);
                tab.Draw();
                Controls.Owner.Update();
            }
            return true;
        }
    }
}
