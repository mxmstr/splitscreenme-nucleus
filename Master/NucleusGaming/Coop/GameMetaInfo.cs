using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Nucleus.Gaming.Coop
{
    public class GameMetaInfo
    {           
        private  readonly string nucleusEnvironment = $@"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\NucleusCoop";
        private  readonly string metaInfoJson = "metaInfo.json";
        private string guid;

        private string lastPlayedAt;
        public string LastPlayedAt => GetLastPlayed();

        private string totalPlayTime;
        public string TotalPlayTime => FormatPlayTime();

        private string iconPath;
        public string IconPath
        {
            get => iconPath;
            set
            {
                iconPath = value;
                SaveGameMetaInfo();
            }
        }

        private bool saveProfile;
        public bool SaveProfile
        {
            get => saveProfile;
            set
            {
                saveProfile = value;
                SaveGameMetaInfo();
            }
        }

        public void LoadGameMetaInfo(string gameGuid)
        {
            try
            {
                guid = gameGuid;

                string path = $"{nucleusEnvironment}\\{metaInfoJson}";

                if (File.Exists(path))
                {
                    string jsonString = File.ReadAllText(path);

                    JObject JMetaInfo = (JObject)JsonConvert.DeserializeObject(jsonString);

                    bool hasMetaInfo = JMetaInfo[gameGuid] != null;

                    if (hasMetaInfo)
                    {
                        totalPlayTime = (string)JMetaInfo[gameGuid]["TotalPlayTime"] != null ? (string)JMetaInfo[gameGuid]["TotalPlayTime"] : null;
                        lastPlayedAt = (string)JMetaInfo[gameGuid]["LastPlayedAt"] != null ? (string)JMetaInfo[gameGuid]["LastPlayedAt"] : null; 
                        iconPath = (string)JMetaInfo[gameGuid]["IconPath"] != null ? (string)JMetaInfo[gameGuid]["IconPath"] : null;
                        saveProfile = JMetaInfo[gameGuid]["SaveProfile"] != null ? (bool)JMetaInfo[gameGuid]["SaveProfile"] : true; 
                        return;
                    }

                    totalPlayTime = null;
                    lastPlayedAt = null;
                    iconPath = null;
                    saveProfile = true;
                }              
            }
            catch (Exception ex)
            {

            }
        }

        public void SaveGameMetaInfo()
        {
            try
            {
                string path = $"{nucleusEnvironment}\\{metaInfoJson}";

                if (!Directory.Exists(nucleusEnvironment))
                {
                    Directory.CreateDirectory(nucleusEnvironment);
                }

                JObject JMetaInfo;

                if (File.Exists(path))
                {
                    string jsonString = File.ReadAllText(path);

                    JMetaInfo = (JObject)JsonConvert.DeserializeObject(jsonString);

                    bool hasMetaInfo = JMetaInfo[guid] != null;

                    if (hasMetaInfo)
                    {
                        JMetaInfo[guid]["TotalPlayTime"] = totalPlayTime;
                        JMetaInfo[guid]["LastPlayedAt"] = lastPlayedAt;
                        JMetaInfo[guid]["IconPath"] = iconPath;
                        JMetaInfo[guid]["SaveProfile"] = saveProfile;
                    }
                    else
                    {
                        //new game metaInfo
                        JProperty gameMeta = 
                            new JProperty(guid, new JObject(new JProperty("TotalPlayTime", totalPlayTime),
                                                            new JProperty("LastPlayedAt", lastPlayedAt),
                                                            new JProperty("IconPath", iconPath),
                                                            new JProperty("SaveProfile", saveProfile)
                                                            ));
                        JMetaInfo.Add(gameMeta);
                    }
                }
                else
                {
                    JObject gameMeta = new JObject(new JProperty("TotalPlayTime", totalPlayTime),
                                                   new JProperty("LastPlayedAt", lastPlayedAt),
                                                   new JProperty("IconPath", iconPath),
                                                   new JProperty("SaveProfile", saveProfile)
                                                   );

                    JMetaInfo = new JObject
                    (
                       new JProperty(guid, gameMeta)
                    );
                }

                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        string json = JsonConvert.SerializeObject(JMetaInfo, Formatting.Indented);
                        writer.Write(json);
                        stream.Flush();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }      
       
        private string FormatPlayTime()
        {
            if (totalPlayTime == null)
            {
                return "...";// 00h:00m:00s";
            }

            ulong totalSeconds = ulong.Parse(totalPlayTime);

            ulong seconds = (totalSeconds % 60);
            ulong minutes = (totalSeconds % 3600) / 60;
            ulong hours = (totalSeconds % 86400) / 3600;

            string formatHours = hours >= 10 ? "" : "0";
            string formatMinutes = minutes >= 10 ? "" : "0";
            string formatSecondes = seconds >= 10 ? "" : "0";

            return $"{formatHours}{hours}h:{formatMinutes}{minutes}m:{formatSecondes}{seconds}s";
        }

        private string GetLastPlayed()
        {
            if (lastPlayedAt == null)
            {
                return "...";
            }

            return lastPlayedAt.Split(' ')[0];//dispaly the date only
        }

        public void SaveGameplayTime(ulong playedTime)
        {
            if (totalPlayTime == null)
            {
                totalPlayTime = playedTime.ToString();
            }
            else
            {
                totalPlayTime = (playedTime + ulong.Parse(totalPlayTime)).ToString();
            }

            lastPlayedAt = DateTime.Now.ToString();
            SaveGameMetaInfo();
        }
    }
}
