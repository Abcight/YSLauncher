using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpYaml.Serialization;

namespace YSLauncher
{
    public static class LauncherData
    {
        public static Data Data;
        public static string DataFilePath;
        public static Post[] Posts;
        public static void Load()
        {
            Serializer yaml = new Serializer();
            string appdataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string dataDirectoryPath = appdataPath + "/YandereSimulator/Launcher";
            DataFilePath = dataDirectoryPath + "/data.yml";
            if (!Directory.Exists(dataDirectoryPath))
            {
                Directory.CreateDirectory(dataDirectoryPath);
            }
            if (!File.Exists(DataFilePath))
            {
                string content = yaml.Serialize(Data);
                File.WriteAllText(DataFilePath, content);
            }
            yaml.Deserialize(File.ReadAllText(DataFilePath), Data);

            if (!File.Exists(dataDirectoryPath + "/content.zip"))
            {
                Data.CurrentVersion = 0;
            }

            Posts = Util.GetPosts("yanderedev.wordpress.com", 3);
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
        public string NewestVersion;
        public int NewestFileSize;
    }
}
