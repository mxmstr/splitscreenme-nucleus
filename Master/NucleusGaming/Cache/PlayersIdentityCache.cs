using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Nucleus.Gaming.Cache
{    /// <summary>
     /// All nicknames and steam ids are cached here so we can easily
     /// share/update the nicknames/steam ids list between profile 
     /// settings and globals settings
     /// </summary>
    public static class PlayersIdentityCache
    {
        private static readonly string nickJsonPath = $"{Globals.GameProfilesFolder}\\Nicknames.json";
        private static readonly string sidJsonPath = $"{Globals.GameProfilesFolder}\\SteamIds.json";

        public static void LoadPlayersIdentityCache()
        {
            LoadNicknamesFromJson();
            LoadNicknamesFromSettingsIni();

            LoadSteamIdsFromSettingsIni();
            LoadSteamIdsFromJson();
        }

        #region Nicknames

        private static List<string> jsonNicknamesList = new List<string>();

        public static List<string> NicknamesBackup
        {
            get => jsonNicknamesList;
        }

        private static List<string> iniNicknameList = new List<string>();

        public static List<string> PlayersNickname
        {
            get => iniNicknameList;
        }

        private static void LoadNicknamesFromSettingsIni()
        {
            for (int i = 0; i < Globals.NucleusMaxPlayers; i++)
            {
                iniNicknameList.Add(Globals.ini.IniReadValue("ControllerMapping", "Player_" + (i + 1)).ToString());
            }
        }

        private static void LoadNicknamesFromJson()
        {
            if (File.Exists(nickJsonPath))
            {
                string jsonString = File.ReadAllText(nickJsonPath);

                JArray JNicks = (JArray)JsonConvert.DeserializeObject(jsonString);

                foreach (JToken nick in JNicks)
                {
                    if (!jsonNicknamesList.Contains(nick.ToString()))
                    {
                        jsonNicknamesList.Add(nick.ToString());
                    }
                }
            }    
        }

        public static string GetNicknameAt(int index)
        {
            if (index > iniNicknameList.Count - 1)
            {
                return null;
            }

            return iniNicknameList[index];
        }

        public static void SetNicknameAt(int index, string nickname)
        {
            if (index > iniNicknameList.Count - 1 || nickname == "" || nickname == null)
            {
                return;
            }

            iniNicknameList[index] = nickname;
            Globals.ini.IniWriteValue("ControllerMapping", "Player_" + (index + 1), nickname);

            if (!jsonNicknamesList.Contains(nickname))
            {
                jsonNicknamesList.Add(nickname);
            }
        }

        public static void BackupNicknames()
        {
            using (FileStream stream = new FileStream(nickJsonPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string json = JsonConvert.SerializeObject(jsonNicknamesList, Formatting.Indented);
                    writer.Write(json);
                    stream.Flush();
                }

                stream.Dispose();
            }
        }

        #endregion

        #region Steam Ids

        //Steam ids
        
        private static List<string> jsonSteamIdList = new List<string>();

        //load both json cache and add ini content to cache
        private static void LoadSteamIdsFromJson()
        {
            if (File.Exists(sidJsonPath))
            {
                string jsonString = File.ReadAllText(sidJsonPath);

                JArray JIds = (JArray)JsonConvert.DeserializeObject(jsonString);

                foreach (JToken id in JIds)
                {
                    if (!jsonSteamIdList.Contains(id.ToString()))
                    {
                        jsonSteamIdList.Add(id.ToString());
                    }
                }
            }       
        }

        public static void BackupSteamIds()
        {
            using (FileStream stream = new FileStream(sidJsonPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string json = JsonConvert.SerializeObject(jsonSteamIdList, Formatting.Indented);
                    writer.Write(json);
                    stream.Flush();
                }

                stream.Dispose();
            }
        }

        //list of SteamIds.json
        public static List<string> SteamIdsBackup
        {
            get => jsonSteamIdList;
        }

        //list of settings.ini steam ids
        private static List<string> settingsIniSteamIdsList = new List<string>();

        private static void LoadSteamIdsFromSettingsIni()
        {
            for (int i = 0; i < Globals.NucleusMaxPlayers; i++)
            {
                settingsIniSteamIdsList.Add(Globals.ini.IniReadValue("SteamIDs", "Player_" + (i + 1)).ToString());
            }
        }

        public static string GetSteamIdAt(int index)
        {
            if (index > settingsIniSteamIdsList.Count - 1)
            {
                return "";
            }

            return settingsIniSteamIdsList[index];
        }

        public static void SetSteamIdAt(int index, string steamid)
        {
            if (index > settingsIniSteamIdsList.Count - 1)
            {
                return;
            }

            if (steamid == null)
            {
                steamid = "";
            }

            settingsIniSteamIdsList[index] = steamid;
            Globals.ini.IniWriteValue("SteamIDs", "Player_" + (index + 1), steamid);

            if (steamid != "" && steamid != "-1" && !jsonSteamIdList.Contains(steamid))
            {
                jsonSteamIdList.Add(steamid);
            }
        } 
        #endregion

    }
}
