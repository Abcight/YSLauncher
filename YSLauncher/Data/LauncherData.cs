using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpYaml.Serialization;
using System.Net;

namespace YSLauncher
{
    public static class LauncherData
    {
        public static Data Data;
        public static BuildState BuildState;
        public static string DataDirectoryPath;
        public static string DataFilePath;
        public static Post[] Posts;
        public static void Load()
        {
            Serializer yaml = new Serializer();
            string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            DataDirectoryPath = appdataPath + "/YandereSimulator/Launcher";
            DataFilePath = DataDirectoryPath + "/data.yml";
            if (!Directory.Exists(DataDirectoryPath))
            {
                Directory.CreateDirectory(DataDirectoryPath);
            }
            if (!File.Exists(DataFilePath))
            {
                string content = yaml.Serialize(Data);
                File.WriteAllText(DataFilePath, content);
            }

            yaml.Deserialize(File.ReadAllText(DataFilePath), Data);
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
            //Flush();
        }
        public static void Flush()
        {
            Serializer yaml = new Serializer();
            string content = yaml.Serialize(Data);
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
