using System;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace YSLauncher
{
    public static class LauncherData
    {
        public static Data Data;
        public static BuildState BuildState;
        public static string DataDirectoryPath;
        public static string DataFilePath;
        public static BlogPost[] Posts;

        public static void Load()
        {
            string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            DataDirectoryPath = appdataPath + "/YandereSimulator/Launcher";
            DataFilePath = DataDirectoryPath + "/data.json";
            if (!Directory.Exists(DataDirectoryPath))
            {
                Directory.CreateDirectory(DataDirectoryPath);
            }
            if (!File.Exists(DataFilePath))
            {
                Flush();
            }
            Data = JsonConvert.DeserializeObject<Data>(File.ReadAllText(DataFilePath));
            //Get newest version number from the YanSim server
            Data.NewestVersion = int.Parse(new WebClient().DownloadString("http://yanderesimulator.com/version.txt"));
            //Get 3 most recent posts to display in the news feed
            Posts = Util.GetPosts("yanderedev.wordpress.com", 3);

            BuildState = BuildState.UpToDate;
            if (Data.CurrentVersion == 0)
            {
                BuildState = BuildState.NotDownloaded;
            }
            else if(Data.CurrentVersion < Data.NewestVersion)
            {
                BuildState = BuildState.UpdateAvailable;
            }
        }
        public static void Flush()
        {
            string content = JsonConvert.SerializeObject(Data);
            File.WriteAllText(DataFilePath, content);
        }
    }
    public struct Data
    {
        public int CurrentVersion;
        public int NewestVersion;
    }
    public enum BuildState
    {
        NotDownloaded, UpdateAvailable, UpToDate
    }
}
