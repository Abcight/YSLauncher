using System;
using System.Diagnostics;

namespace YSLauncher
{
    public static class ButtonEvents
    {
        public static void DownloadEvent(object sender, EventArgs e)
        {
            Updater.DownloadGame();
        }
        public static void PlayEvent(object sender, EventArgs e)
        {
            Process.Start(LauncherData.DataDirectoryPath + "/Game/YandereSimulator.exe");
        }
    }
}
