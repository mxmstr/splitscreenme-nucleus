using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nucleus.Coop;
using Nucleus.Gaming.Coop.Generic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Nucleus.Gaming.Coop
{
    public class GameProfile
    {
        /// <summary>
        /// A reference to the screens as they were
        /// when the user made the profile
        /// (so we can compare if a screen
        /// is missing or added)
        /// </summary>
        public List<UserScreen> Screens => screens;
        private List<PlayerInfo> playerData;
        private List<UserScreen> screens;
        public List<PlayerInfo> PlayerData => playerData;
        public static GameProfile currentProfile;
        private GenericGameInfo _game;
        private PositionsControl positionsControl = null;
        public static List<Rectangle> MonitorBounds = new List<Rectangle>();
        public static List<Rectangle> OwnerDisplays = new List<Rectangle>();
        public static List<Rectangle> EditBounds = new List<Rectangle>();

        public static List<Guid> GamepadsGuid = new List<Guid>();

        public static List<int> ScreenPrioritys = new List<int>();
        public static List<int> ScreenIndexes = new List<int>();
        public static List<int> PlayerIDs = new List<int>();
        public static List<int> OwnerType = new List<int>();
        public static List<int> CustomLayout = new List<int>(); private static int playersCount = 0;
       
        private static int profilesCount = 0;//Used to check if we need to create a new profile with more players
        private static int profileToSave;
        public static string ModeText = "Creation Mode";
        public static string GameGUID;

        public static List<string> profilesPathList = new List<string>();
        public static List<string> Nicknames = new List<string>();

        public static List<long> SteamIDs = new List<long>();

        public static bool createNewProfile = true;

        public static List<bool> IsDInputs = new List<bool>();
        public static List<bool> IsXInputs = new List<bool>();
        public static List<bool> IsKeyboardPlayer = new List<bool>();

        /// <summary>
        /// Options set by the user
        /// </summary>
        public Dictionary<string, object> Options => options;
        private Dictionary<string, object> options;
        public static IDictionary<string,string> AudioInstances = new Dictionary<string, string>();
        // public static List<string> RawKeyboardDeviceHandles = new List<string>();
        // public static List<bool> IsRawMouses = new List<bool>();//mice
        // public static List<string> RawMouseDeviceHandles = new List<string>();

        private static bool isNew = true;
        public static bool IsNew
        {
            get => isNew;
            set => isNew = value;
        }

        private static int hWndInterval;
        public static int HWndInterval
        {
            get => hWndInterval;
            set => hWndInterval = value;
        }

        private static bool useSplitDiv = false;
        public static bool UseSplitDiv
        {
            get => useSplitDiv;
            set => useSplitDiv = value;
        }

        private static int customLayout_Ver = 0;
        public static int CustomLayout_Ver
        {
            get => customLayout_Ver;
            set => customLayout_Ver = value;
        }

        private static int customLayout_Hor = 0;
        public static int CustomLayout_Hor
        {
            get => customLayout_Hor;
            set => customLayout_Hor = value;
        }

        private static int customLayout_Max = 1;
        public static int CustomLayout_Max
        {
            get => customLayout_Max;
            set => customLayout_Max = value;
        }

        private static bool autoDesktopScaling = true;
        public static bool AutoDesktopScaling
        {
            get => autoDesktopScaling;
            set => autoDesktopScaling = value;
        }

        private static bool keepAccounts = false;
        public static bool KeepAccounts
        {
            get => keepAccounts;
            set => keepAccounts = value;
        }

        private static bool autoPlay = false;
        public static bool AutoPlay
        {
            get => autoPlay;
            set => autoPlay = value;
        }

        private static bool keepSymLink = false;
        public static bool KeepSymLink
        {
            get => keepSymLink;
            set => keepSymLink = value;
        }
        
        private static string splitDivColor = "Black";
        public static string SplitDivColor
        {
            get => splitDivColor;
            set => splitDivColor = value;
        }

        private static string network = "Automatic";
        public static string Network
        {
            get => network;
            set => network = value;
        }

        private static int pauseBetweenInstanceLaunch;
        public static int PauseBetweenInstanceLaunch
        {
            get => pauseBetweenInstanceLaunch;
            set => pauseBetweenInstanceLaunch = value;
        }

        private static bool useNicknames = true;
        public static bool UseNicknames
        {
            get => useNicknames;
            set => useNicknames = value;
        }

        private static bool audioDefaultSettings = true;
        public static bool AudioDefaultSettings
        {
            get => audioDefaultSettings;
            set => audioDefaultSettings = value;
        }

        private static bool audioCustomSettings = false;
        public static bool AudioCustomSettings
        {
            get => audioCustomSettings;
            set => audioCustomSettings = value;
        }

        public GameProfile()
        {

        }

        private void ListGameProfiles()
        {
            string path = GetProfilesPath();
            profilesPathList.Clear();

            if (path != null)
            {
                profilesPathList = Directory.EnumerateFiles(path).ToList();
                profilesCount = profilesPathList.Count();
                Console.WriteLine($"Found {profilesCount} Game profile(s)");
            }
        }

        private static string GetProfilesPath()
        {
            string path = Path.Combine(Application.StartupPath, $"Content\\_Games Profiles_\\{GameGUID}");
            if (!Directory.Exists(path))
            {
                Console.WriteLine("No game profile found...");//Log.
                return null;
            }
            return path;
        }

        public void Reset()
        {           
            PlayerIDs.Clear();
            Nicknames.Clear();
            SteamIDs.Clear();
            GamepadsGuid.Clear();
            OwnerType.Clear();
            IsDInputs.Clear();
            IsXInputs.Clear();
            IsKeyboardPlayer.Clear();
            CustomLayout.Clear();
            AudioInstances.Clear();
            ScreenIndexes.Clear();
            EditBounds.Clear();
            OwnerDisplays.Clear();
            MonitorBounds.Clear();
            if (PlayerData != null)
            {
                PlayerData.Clear();
            }
            autoPlay = false;
            keepSymLink = false;
            keepAccounts = false;
            autoDesktopScaling = true;
            useNicknames = true;
            useSplitDiv = false;
            audioDefaultSettings = true;
            audioCustomSettings = false;
            splitDivColor = "Black";
            network = "Automatic";
            ModeText = "Creation Mode";
            isNew = true;
            hWndInterval = 0;
            profileToSave = 0;
            playersCount = 0;
            pauseBetweenInstanceLaunch = 0;
            customLayout_Ver = 0;
            customLayout_Hor = 0;
            customLayout_Max = 1;
            ListGameProfiles();

            options = new Dictionary<string, object>();

            foreach (GameOption opt in _game.Options)
            {
                options.Add(opt.Key, opt.Value);
            }

            createNewProfile = true;

            Label unload = new Label();
            //unload.Text = "UnloadPC";
            positionsControl.gameProfilesPanel.profileBtn_CheckedChanged(unload, null);
            positionsControl.loadedProfilePlayers.Clear();
            positionsControl.GamepadTimer_Tick(null);
            positionsControl.UpdatePlayers();
           
           
            positionsControl.CanUpdate = true;
            Console.WriteLine("Game Profile Unloaded");
        }

        public void InitializeDefault(GenericGameInfo game,PositionsControl pc)
        {
            currentProfile = this;
            _game = game;
            positionsControl = pc;

            Reset();

            if (playerData == null)
            {
                playerData = new List<PlayerInfo>();
            }

            if (screens == null)
            {
                screens = new List<UserScreen>();
            }

            if (options == null)
            {
                options = new Dictionary<string, object>();

                foreach (GameOption opt in game.Options)
                {
                    options.Add(opt.Key, opt.Value);
                }
            }
        }

        public bool LoadUserProfile(int _profileToLoad)
        {            
            string path = Path.Combine(Application.StartupPath, $"Content\\_Games Profiles_\\{GameGUID}\\Profile[{_profileToLoad}].json");

            string jsonString = File.ReadAllText(path);
            Console.WriteLine(jsonString);
   
            JObject Jprofile = (JObject)JsonConvert.DeserializeObject(jsonString);

            Reset();

            profileToSave = _profileToLoad;//to keep after reset()

            if (Jprofile == null)
            {
                return false;
            }

            JToken Joptions = Jprofile["Options"] as JToken;

            options = new Dictionary<string, object>();

            foreach (JProperty Jopt in Joptions)
            {
                options.Add((string)Jopt.Name, Jopt.Value.ToString());
            }

            if (hWndInterval == 0)
            {
                HWndInterval = (int)Jprofile["WindowsSetupTiming"]["Time"];
            }

            if (pauseBetweenInstanceLaunch == 0)
            {
                PauseBetweenInstanceLaunch = (int)Jprofile["PauseBetweenInstanceLaunch"]["Time"];
            }

            UseSplitDiv = (bool)Jprofile["UseSplitDiv"]["Enabled"];
            SplitDivColor = (string)Jprofile["UseSplitDiv"]["Color"];
            AutoDesktopScaling = (bool)Jprofile["AutoDesktopScaling"]["Enabled"];
            KeepAccounts = (bool)Jprofile["KeepAccounts"]["Keep"];
            UseNicknames = (bool)Jprofile["UseNicknames"]["Use"];
            KeepSymLink = (bool)Jprofile["KeepSymLink"]["Keep"];
            Network = (string)Jprofile["Network"]["Type"];
            CustomLayout_Ver = (int)Jprofile["CustomLayout"]["Ver"];
            CustomLayout_Hor = (int)Jprofile["CustomLayout"]["Hor"];
            CustomLayout_Max = (int)Jprofile["CustomLayout"]["Max"];
            AutoPlay = (bool)Jprofile["AutoPlay"]["Enabled"];

            JToken JplayersInfos = Jprofile["Data"] as JToken;

            for (int i = 0; i < JplayersInfos.Count(); i++)
            {
                PlayerIDs.Add((int)JplayersInfos[i]["PlayerID"]);
                Nicknames.Add((string)JplayersInfos[i]["Nickname"]);
                SteamIDs.Add((long)JplayersInfos[i]["SteamID"]);
                GamepadsGuid.Add((Guid)JplayersInfos[i]["GamepadGuid"]);
                OwnerType.Add((int)JplayersInfos[i]["Owner"]["Type"]);

                OwnerDisplays.Add(new Rectangle(
                                               (int)JplayersInfos[i]["Owner"]["Display"]["X"],
                                               (int)JplayersInfos[i]["Owner"]["Display"]["Y"],
                                               (int)JplayersInfos[i]["Owner"]["Display"]["Width"],
                                               (int)JplayersInfos[i]["Owner"]["Display"]["Height"]));

                MonitorBounds.Add(new Rectangle(
                                               (int)JplayersInfos[i]["MonitorBounds"]["X"],
                                               (int)JplayersInfos[i]["MonitorBounds"]["Y"],
                                               (int)JplayersInfos[i]["MonitorBounds"]["Width"],
                                               (int)JplayersInfos[i]["MonitorBounds"]["Height"]));

                EditBounds.Add(new Rectangle(
                                               (int)JplayersInfos[i]["EditBounds"]["X"],
                                               (int)JplayersInfos[i]["EditBounds"]["Y"],
                                               (int)JplayersInfos[i]["EditBounds"]["Width"],
                                               (int)JplayersInfos[i]["EditBounds"]["Height"]));


                IsDInputs.Add((bool)JplayersInfos[i]["IsDInput"]);
                IsXInputs.Add((bool)JplayersInfos[i]["IsXInput"]);
                IsKeyboardPlayer.Add((bool)JplayersInfos[i]["IsKeyboardPlayer"]);
                //RawKeyboardDeviceHandles.Add((string)playersInfos[i]["RawKeyboardDeviceHandle"]);
                //IsRawMouses.Add((bool)playersInfos[i]["IsRawMouse"]);
                // RawMouseDeviceHandles.Add((string)playersInfos[i]["RawMouseDeviceHandle"]);
                ScreenPrioritys.Add((int)JplayersInfos[i]["ScreenPriority"]);
                ScreenIndexes.Add((int)JplayersInfos[i]["ScreenIndex"]);
            }

            JToken JaudioSettings = Jprofile["AudioSettings"] as JToken;

            foreach (JProperty JaudioSetting in JaudioSettings)
            {
                if(JaudioSetting.Name.Contains("Custom"))
                {
                    AudioCustomSettings = (bool)JaudioSetting.Value;
                }
                else
                {
                   AudioDefaultSettings = (bool)JaudioSetting.Value;
                }
            }

            JToken JAudioInstances = Jprofile["AudioInstances"] as JToken;
            foreach (JProperty JaudioDevice in JAudioInstances)
            {
                AudioInstances.Add((string)JaudioDevice.Name, (string)JaudioDevice.Value);
            }
          
            foreach(int index in ScreenIndexes)//Check if there is enough screens
            {
                if (index > ScreensUtil.AllScreens().Length - 1)
                {
                    Globals.MainOSD.Settings(2000, Color.Orange, $"Requires {index + 1} Screens");
                    Reset();
                    return false;
                }
            }

            int allPlayers = 0; 
            foreach(PlayerInfo player in PlayerData)
            {
                if(GamepadsGuid.Contains(player.GamepadGuid))
                {
                    allPlayers++;
                }
            }

            if(allPlayers < PlayerIDs.Count)
            {
                Globals.MainOSD.Settings(2000, Color.Orange, $"Requires {PlayerIDs.Count} Controller(s).\nMaybe All The Controllers Used To Create This Profile Are Not Plugged?");
                Reset();
                return false;
            }

            playersCount = JplayersInfos.Count();
            PlayerData.Count();
            createNewProfile = false;
            ModeText = "Edition Mode";
            isNew = false;
           
            string mod1 = string.Empty;
            string mod2 = " Loaded";

            if (AutoPlay)
            {
                mod1 = "Starting ";
                mod2 = string.Empty;
            }

            Globals.MainOSD.Settings(2000, Color.YellowGreen, $"{mod1}Game Profile N°{_profileToLoad}{mod2}");

            LogManager.Log($"Game profile n°{_profileToLoad} Loaded");
            return true;
        }

        public static void saveUserProfile(GameProfile profile)//A voir pour utiliser default nucleus profiles 
        {
            string path;

            if (profile.PlayerData.Count != playersCount || createNewProfile || profilesCount == 0)
            {
                profilesCount++;//increase to set new profile name
                path = Path.Combine(Application.StartupPath, $"Content\\_Games Profiles_\\{GameGUID}\\Profile[{profilesCount}].json");
                Console.WriteLine("Creating new a game profile...");
            }
            else
            {
                path = Path.Combine(Application.StartupPath, $"Content\\_Games Profiles_\\{GameGUID}\\Profile[{profileToSave}].json");
            }

            if (!Directory.Exists(Path.Combine(Application.StartupPath, $"Content\\_Games Profiles_\\{GameGUID}")))
            {
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, $"Content\\_Games Profiles_\\{GameGUID}"));
            }

            JObject options = new JObject();
            foreach (KeyValuePair<string, object> opt in profile.Options)
            {
                if (opt.Value.GetType() == typeof(System.Dynamic.ExpandoObject))//Only used for options with pictures so far
                {
                    JObject values = new JObject();
                    System.Dynamic.ExpandoObject _vals = (System.Dynamic.ExpandoObject)opt.Value;

                    foreach (var t in _vals)
                    {
                        values.Add(new JProperty(t.Key, t.Value));
                    }

                    options.Add(new JProperty(opt.Key.ToString(), values));
                }
                else
                {
                    options.Add(new JProperty(opt.Key.ToString(), opt.Value.ToString()));
                }
            }

            JObject JHWndInterval = new JObject();

            if (hWndInterval > 0)
            {
                JHWndInterval.Add(new JProperty("Time", hWndInterval.ToString()));
            }
            else
            {
                JHWndInterval.Add(new JProperty("Time", "0"));
            }

            JObject JPauseBetweenInstanceLaunch = new JObject();

            if (pauseBetweenInstanceLaunch > 0)
            {
                JPauseBetweenInstanceLaunch.Add(new JProperty("Time", pauseBetweenInstanceLaunch.ToString()));
            }
            else
            {
                JPauseBetweenInstanceLaunch.Add(new JProperty("Time", "0"));
            }

            JObject JCustomLayout = new JObject(new JProperty("Ver", customLayout_Ver), new JProperty("Hor", customLayout_Hor), new JProperty("Max", CustomLayout_Max));
            JObject JUseSplitDiv = new JObject(new JProperty("Enabled", useSplitDiv), new JProperty("Color", splitDivColor));
            JObject JAutoDesktopScaling = new JObject(new JProperty("Enabled", autoDesktopScaling));
            JObject JkeepAccounts = new JObject(new JProperty("Keep", keepAccounts));
            JObject JUseNicknames = new JObject(new JProperty("Use", useNicknames));
            JObject JKeepSymLink = new JObject(new JProperty("Keep", keepSymLink));
            JObject JNetwork = new JObject(new JProperty("Type", network));
            JObject JAutoPlay = new JObject(new JProperty("Enabled", autoPlay));
            JObject JAudioInstances = new JObject();    
            
            foreach (KeyValuePair<string, string> JaudioDevice in AudioInstances)
            {
                JAudioInstances.Add(new JProperty(JaudioDevice.Key,JaudioDevice.Value));
            }

            JObject JAudioSettings = new JObject(new JProperty("CustomSettings", audioCustomSettings), new JProperty("DefaultSettings", audioDefaultSettings));
            
            IOrderedEnumerable<PlayerInfo> players = profile.PlayerData.OrderBy(c => c.PlayerID);//need to do this cause sometimes it's reversed
            List<JObject> playersInfos = new List<JObject>();
            
            for (int i = 0; i < profile.playerData.Count; i++)
            {
                profile.playerData[i].ScreenIndex = ScreenIndexes[i];
                profile.playerData[i].EditBounds = EditBounds[i];
            }

            if (players != null)
            {
                foreach (PlayerInfo player in players)
                {

                    JObject JOwner = new JObject(
                                     new JProperty("Type", player.Owner.Type),
                                     new JProperty("Display", new JObject(
                                                              new JProperty("X", player.Owner.display.X),
                                                              new JProperty("Y", player.Owner.display.Y),
                                                              new JProperty("Width", player.Owner.display.Width),
                                                              new JProperty("Height", player.Owner.display.Height))));

                    JObject JMonitorBounds = new JObject(
                                             new JProperty("X", player.MonitorBounds.X),
                                             new JProperty("Y", player.MonitorBounds.Y),
                                             new JProperty("Width", player.MonitorBounds.Width),
                                             new JProperty("Height", player.MonitorBounds.Height));


                    JObject JEditBounds = new JObject(
                                          new JProperty("X", player.EditBounds.X),
                                          new JProperty("Y", player.EditBounds.Y),
                                          new JProperty("Width", player.EditBounds.Width),
                                          new JProperty("Height", player.EditBounds.Height));

                    JObject JPData = new JObject(
                                     new JProperty("PlayerID", player.PlayerID),
                                     new JProperty("Nickname", player.Nickname),
                                     new JProperty("SteamID", player.SteamID),
                                     new JProperty("GamepadGuid", player.GamepadGuid),
                                     new JProperty("IsDInput", player.IsDInput),
                                     new JProperty("IsXInput", player.IsXInput),
                                     new JProperty("IsKeyboardPlayer", player.IsKeyboardPlayer),
                                     //new JProperty("RawKeyboardDeviceHandle", (string)player.HIDDeviceID),
                                     //new JProperty("IsRawMouse", player.IsRawMouse),
                                     //new JProperty("RawMouseDeviceHandle", (string)player.HIDDeviceID),
                                     new JProperty("ScreenPriority", player.screenPriority),
                                     new JProperty("ScreenIndex", player.ScreenIndex),
                                     new JProperty("EditBounds", JEditBounds),
                                     new JProperty("MonitorBounds", JMonitorBounds),
                                     new JProperty("Owner", JOwner)

                        );

                    playersInfos.Add(JPData);
                }
            }

            JObject profileJson = new JObject
            (
               new JProperty("Player(s)", profile.PlayerData.Count),
               new JProperty("AutoPlay", JAutoPlay),
               new JProperty("Data", playersInfos),
               new JProperty("Options", options),
               new JProperty("UseNicknames", JUseNicknames),
               new JProperty("KeepAccounts", JkeepAccounts),
               new JProperty("AutoDesktopScaling", JAutoDesktopScaling),
               new JProperty("UseSplitDiv", JUseSplitDiv),
               new JProperty("CustomLayout", JCustomLayout),
               new JProperty("WindowsSetupTiming", JHWndInterval),
               new JProperty("PauseBetweenInstanceLaunch", JPauseBetweenInstanceLaunch),          
               new JProperty("KeepSymLink", JKeepSymLink),
               new JProperty("Network", JNetwork),      
               new JProperty("AudioSettings", JAudioSettings),
               new JProperty("AudioInstances", JAudioInstances)
            );

            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string json = JsonConvert.SerializeObject(profileJson, Formatting.Indented);
                    writer.Write(json);
                    stream.Flush();
                }
            }

            LogManager.Log("Game profile Saved");
        }

        public static GameProfile CleanClone(GameProfile profile)
        {
            GameProfile nprof = new GameProfile
            {
                playerData = new List<PlayerInfo>(),
                screens = profile.screens.ToList(),
            };

            List<PlayerInfo> source;

            if (!isNew)
            {
               source = profile.PlayerData.OrderBy(c => c.PlayerID).ToList();//reoder so it follow the current loaded game profile player order.
            }
            else
            {
               source = profile.playerData;
            }

            for (int i = 0; i < source.Count; i++)
            {
                PlayerInfo player = source[i];
                
                if (player.ScreenIndex != -1)
                {
                    // only add valid players to the clean version
                    nprof.playerData.Add(player);
                }
            }

            Dictionary<string, object> noptions = new Dictionary<string, object>();
            foreach (KeyValuePair<string, object> opt in profile.Options)
            {
                noptions.Add(opt.Key, opt.Value);
            }

            nprof.options = noptions;

            return nprof;
        }
    }
}
