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
        private static List<string> cachedNicknamesList = new List<string>();

        public static void LoadPlayersIdentityCache()
        {
            LoadNicknamesCache();
            LoadSteamIdsCache();
        }

        //Nicknames
        private static void LoadNicknamesCache()
        {
            if (File.Exists(nickJsonPath))
            {
                string jsonString = File.ReadAllText(nickJsonPath);

                JArray JNicks = (JArray)JsonConvert.DeserializeObject(jsonString);

                foreach (JToken nick in JNicks)
                {
                   AddNicknameToCache(nick.ToString());
                }
            }

            LoadNicknamesFromSettingsIni();
        }

        public static void SaveNicknamesCache()
        {         
            using (FileStream stream = new FileStream(nickJsonPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string json = JsonConvert.SerializeObject(GetCachedNicknamesList, Formatting.Indented);
                    writer.Write(json);
                    stream.Flush();
                }

                stream.Dispose();
            }
        }

        public static List<string> GetCachedNicknamesList
        {
            get => cachedNicknamesList;
        }

        public static void AddNicknameToCache(string nickname)
        {
            if (!cachedNicknamesList.Contains(nickname))
            {
                cachedNicknamesList.Add(nickname);
            }
        }

        public static List<string> SettingsIniNicknamesList = new List<string>();

        private static void LoadNicknamesFromSettingsIni()
        {
            for (int i = 0; i < Globals.NucleusMaxPlayers; i++)
            {
                SettingsIniNicknamesList.Add(Globals.ini.IniReadValue("ControllerMapping", "Player_" + (i + 1)).ToString());
            }
        }

        //Steam ids
        private static readonly string sidJsonPath = $"{Globals.GameProfilesFolder}\\SteamIds.json";
        private static List<string> cachedSteamIdsList = new List<string>();

        //load both json cache and add ini content to cache
        private static void LoadSteamIdsCache()
        {
            if (File.Exists(sidJsonPath))
            {
                string jsonString = File.ReadAllText(sidJsonPath);

                JArray JIds = (JArray)JsonConvert.DeserializeObject(jsonString);

                foreach (JToken id in JIds)
                {
                    AddSteamIdToCache(id.ToString());
                }
            }

            LoadSteamIdsFromSettingsIni();
        }

        public static void SaveSteamidsCache()
        {
            using (FileStream stream = new FileStream(sidJsonPath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string json = JsonConvert.SerializeObject(GetCachedSteamIdsList, Formatting.Indented);
                    writer.Write(json);
                    stream.Flush();
                }

                stream.Dispose();
            }
        }

        //list of SteamIds.json
        public static List<string> GetCachedSteamIdsList
        {
            get => cachedSteamIdsList;
        }

        //add new steamids (will be saved in SteamIds.json)
        public static void AddSteamIdToCache(string steamid)
        {
            if (!cachedSteamIdsList.Contains(steamid))
            {
                cachedSteamIdsList.Add(steamid);
            }
        }

        //list of settings.ini steam ids
        public static List<string> SettingsIniSteamIdsList = new List<string>();

        private static void LoadSteamIdsFromSettingsIni()
        {
            for (int i = 0; i < Globals.NucleusMaxPlayers; i++)
            {
                SettingsIniSteamIdsList.Add(Globals.ini.IniReadValue("SteamIDs", "Player_" + (i + 1)).ToString());
            }
        }
    }
}
