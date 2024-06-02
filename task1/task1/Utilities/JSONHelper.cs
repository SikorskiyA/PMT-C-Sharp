using System.Text.Json;

namespace task1.Utilities
{
    [Serializable]
    public class JSONHelper
    {
        public string RandomApi { get; set; }
        public JSONHelperSettings Settings { get; set; }

        public JSONHelper(string randomApi, JSONHelperSettings settings)
        {
            RandomApi = randomApi;
            Settings = settings;
        }
        public JSONHelper()
        {
        }
        public void Deserialize()
        {
            string fileName = "Configuration/appsettings.json";
            string json = File.ReadAllText(fileName);
            JSONHelper jh = JsonSerializer.Deserialize<JSONHelper>(json);
            RandomApi = jh.RandomApi;
            Settings = jh.Settings;
        }
        public void Update()
        {
            string fileName = "Configuration/appsettings.json";
            string json = JsonSerializer.Serialize(this);
            File.WriteAllText(fileName, json);
        }

        public static bool IsBlackListed(string str)
        {
            JSONHelper jh = new JSONHelper();
            jh.Deserialize();
            return jh.Settings.BlackList.Contains(str);
        }
    }
    [Serializable]
    public class JSONHelperSettings
    {
        public List<string> BlackList { get; set; }

        public JSONHelperSettings(List<string> blackList)
        {
            BlackList = blackList;
        }
    }
}
