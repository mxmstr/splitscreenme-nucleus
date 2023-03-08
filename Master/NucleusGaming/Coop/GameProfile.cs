using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nucleus.Coop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Nucleus.Gaming.Coop
{
    public class GameProfile
    {
        private readonly IniFile ini = Globals.ini;

        private List<UserScreen> screens;
        public List<UserScreen> Screens => screens;
        private List<PlayerInfo> playerData;     
        public List<PlayerInfo> PlayerData => playerData;

        public static List<Rectangle> MonitorBounds = new List<Rectangle>();
        public static List<Rectangle> OwnerDisplays = new List<Rectangle>();
        public static List<Rectangle> OwnerUIBounds = new List<Rectangle>();
        public static List<Rectangle> EditBounds = new List<Rectangle>();
        public static List<Rectangle> AllScreens = new List<Rectangle>();

        public static List<Guid> GamepadsGuid = new List<Guid>();

        public static List<int> ScreenPrioritys = new List<int>();
        public static List<int> ScreenIndexes = new List<int>();
        public static List<int> PlayerIDs = new List<int>();
        public static List<int> OwnerType = new List<int>();
        public static List<int> DisplaysIndexes = new List<int>();

        public static GameProfile currentProfile;
        public static GenericGameInfo Game;
        private PositionsControl positionsControl = null;

        private static int totalPlayers = 0;
        public static int TotalPlayers => totalPlayers;


        private static int profilesCount = 0;//Used to check if we need to create a new profile 
        private static int profileToSave;

        private static string modeText = "New Profile";
        public static string ModeText => modeText;
   
        public static string GameGUID;

        public static List<string> profilesPathList = new List<string>();
        public static List<string> Nicknames = new List<string>();

        public static List<string> IdealProcessors = new List<string>();
        public static List<string> Affinitys = new List<string>();
        public static List<string> PriorityClasses = new List<string>();

        public static List<long> SteamIDs = new List<long>();
      
        public static List<bool> IsDInputs = new List<bool>();
        public static List<bool> IsXInputs = new List<bool>();
        public static List<bool> IsKeyboardPlayer = new List<bool>();
        public static List<bool> IsRawMouses = new List<bool>();
        public static List<string> IsExpanded = new List<string>();

        public Dictionary<string, object> Options => options;
        private Dictionary<string, object> options;
        public static IDictionary<string, string> AudioInstances = new Dictionary<string, string>();

        public static List<string> RawKeyboardDeviceHandles = new List<string>();
        public static List<string> RawMouseDeviceHandles = new List<string>();

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

        private static bool autoPlay = false;
        public static bool AutoPlay
        {
            get => autoPlay;
            set => autoPlay = value;
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

        private static string notes;
        public static string Notes
        {
            get => notes;
            set => notes = value;
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

        private static bool cts_MuteAudioOnly = false;
        public static bool Cts_MuteAudioOnly
        {
            get => cts_MuteAudioOnly;
            set => cts_MuteAudioOnly = value;
        }

        private static bool cts_KeepAspectRatio = false;
        public static bool Cts_KeepAspectRatio
        {
            get => cts_KeepAspectRatio;
            set => cts_KeepAspectRatio = value;
        }

        private static bool cts_Unfocus = false;
        public static bool Cts_Unfocus
        {
            get => cts_Unfocus;
            set => cts_Unfocus = value;
        }

        private static bool saved = false;
        public static bool Saved => saved;

        public static bool Ready = false;

        private void ListGameProfiles()
        {
            string path = GetProfilesPath();
            profilesPathList.Clear();
            profilesCount = 0;

            if (path != null)
            {
                profilesPathList = Directory.EnumerateFiles(path).OrderBy(s => int.Parse(Regex.Match(s, @"\d+").Value)).ToList();
                profilesCount = profilesPathList.Count();
            }
        }

        private static string GetProfilesPath()
        {
            string path = Path.Combine(Application.StartupPath, $"games profiles\\{GameGUID}");
            if (!Directory.Exists(path))
            {
                return null;
            }

            return path;
        }

        private void GetPlayersNickNameAndSteamIds()
        {
            SteamIDs.Clear();
            Nicknames.Clear();

            for (int i = 0; i < 32; i++)
            {
                Nicknames.Add(ini.IniReadValue("ControllerMapping", "Player_" + (i + 1)));

                string sid = ini.IniReadValue("SteamIDs", "Player_" + (i + 1));
                if (sid == "")
                {
                    continue;
                }

                SteamIDs.Add((long)long.Parse(ini.IniReadValue("SteamIDs", "Player_" + (i + 1))));              
            }
        }

        public void Reset()
        {
            GetPlayersNickNameAndSteamIds();

            PlayerIDs.Clear();
            GamepadsGuid.Clear();
            OwnerType.Clear();
            IsDInputs.Clear();
            IsXInputs.Clear();
            IsKeyboardPlayer.Clear();
            IsRawMouses.Clear();

            ScreenIndexes.Clear();
            EditBounds.Clear();
            OwnerDisplays.Clear();
            DisplaysIndexes.Clear();
            MonitorBounds.Clear();
            IsExpanded.Clear();
            IdealProcessors.Clear();
            Affinitys.Clear();
            PriorityClasses.Clear();
            AllScreens.Clear();

            autoPlay = false;
            autoDesktopScaling = bool.Parse(ini.IniReadValue("Misc", "AutoDesktopScaling"));
            useNicknames = bool.Parse(ini.IniReadValue("Misc", "UseNicksInGame"));
            useSplitDiv = bool.Parse(ini.IniReadValue("CustomLayout", "SplitDiv"));
            customLayout_Ver = int.Parse(ini.IniReadValue("CustomLayout", "VerticalLines"));
            customLayout_Hor = int.Parse(ini.IniReadValue("CustomLayout", "HorizontalLines"));
            customLayout_Max = int.Parse(ini.IniReadValue("CustomLayout", "MaxPlayers"));
            splitDivColor = ini.IniReadValue("CustomLayout", "SplitDivColor");
            network = ini.IniReadValue("Misc", "Network");

            audioCustomSettings = int.Parse(ini.IniReadValue("Audio", "Custom")) == 1;
            audioDefaultSettings = audioCustomSettings == false;

            cts_MuteAudioOnly = bool.Parse(ini.IniReadValue("CustomLayout", "Cts_MuteAudioOnly"));
            cts_KeepAspectRatio = bool.Parse(ini.IniReadValue("CustomLayout", "Cts_KeepAspectRatio"));
            cts_Unfocus = bool.Parse(ini.IniReadValue("CustomLayout", "Cts_Unfocus"));
            notes = string.Empty;

            hWndInterval = 0;
            pauseBetweenInstanceLaunch = 0;

            profileToSave = 0;
            totalPlayers = 0;

            modeText = "New Profile";

            Ready = false;
            saved = false;

            PlayerData?.Clear();

            options = new Dictionary<string, object>();

            foreach (GameOption opt in Game.Options)
            {
                options.Add(opt.Key, opt.Value);
            }

            Label unload = new Label();

            positionsControl.ResetScreensTotalPlayers();
            positionsControl.gameProfilesList.profileBtn_CheckedChanged(unload, null);
            positionsControl.loadedProfilePlayers.Clear();

            positionsControl.UpdatePlayers();
            positionsControl.profileSettings_Tooltip.SetToolTip(positionsControl.profileSettings_btn, $"{GameProfile.Game.GameName} {GameProfile.ModeText.ToLower()} settings.");
            ListGameProfiles();

            //Console.WriteLine("Game Profile Unloaded");
        }

        public void InitializeDefault(GenericGameInfo game, PositionsControl pc)
        {
            currentProfile = this;
            Game = game;
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
            string path = Path.Combine(Application.StartupPath, $"games profiles\\{GameGUID}\\Profile[{_profileToLoad}].json");

            string jsonString = File.ReadAllText(path);

            JObject Jprofile = (JObject)JsonConvert.DeserializeObject(jsonString);

            Reset();

            profileToSave = _profileToLoad;//to keep after reset()

            if (Jprofile == null)
            {
                Ready = false;
                return false;
            }

            Nicknames.Clear();
            SteamIDs.Clear();

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
            UseNicknames = (bool)Jprofile["UseNicknames"]["Use"];
            Cts_KeepAspectRatio = (bool)Jprofile["CutscenesModeSettings"]["Cutscenes_KeepAspectRatio"];
            Cts_MuteAudioOnly = (bool)Jprofile["CutscenesModeSettings"]["Cutscenes_MuteAudioOnly"];
            Cts_Unfocus = (bool)Jprofile["CutscenesModeSettings"]["Cutscenes_Unfocus"];
            Network = (string)Jprofile["Network"]["Type"];
            CustomLayout_Ver = (int)Jprofile["CustomLayout"]["Ver"];
            CustomLayout_Hor = (int)Jprofile["CustomLayout"]["Hor"];
            CustomLayout_Max = (int)Jprofile["CustomLayout"]["Max"];
            AutoPlay = (bool)Jprofile["AutoPlay"]["Enabled"];
            Notes = (string)Jprofile["Notes"];

            JToken JplayersInfos = Jprofile["Data"] as JToken;

            for (int i = 0; i < JplayersInfos.Count(); i++)
            {
                PlayerIDs.Add((int)JplayersInfos[i]["PlayerID"]);
                Nicknames.Add((string)JplayersInfos[i]["Nickname"]);
                SteamIDs.Add((long)JplayersInfos[i]["SteamID"]);
                GamepadsGuid.Add((Guid)JplayersInfos[i]["GamepadGuid"]);
                OwnerType.Add((int)JplayersInfos[i]["Owner"]["Type"]);
                DisplaysIndexes.Add((int)JplayersInfos[i]["Owner"]["DisplayIndex"]);

                OwnerDisplays.Add(new Rectangle(
                                               (int)JplayersInfos[i]["Owner"]["Display"]["X"],
                                               (int)JplayersInfos[i]["Owner"]["Display"]["Y"],
                                               (int)JplayersInfos[i]["Owner"]["Display"]["Width"],
                                               (int)JplayersInfos[i]["Owner"]["Display"]["Height"]));
                OwnerUIBounds.Add(new Rectangle(
                                              (int)JplayersInfos[i]["Owner"]["UiBounds"]["X"],
                                              (int)JplayersInfos[i]["Owner"]["UiBounds"]["Y"],
                                              (int)JplayersInfos[i]["Owner"]["UiBounds"]["Width"],
                                              (int)JplayersInfos[i]["Owner"]["UiBounds"]["Height"]));


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
                RawKeyboardDeviceHandles.Add((string)JplayersInfos[i]["RawKeyboardDeviceHandle"]);
                IsRawMouses.Add((bool)JplayersInfos[i]["IsRawMouse"]);
                RawMouseDeviceHandles.Add((string)JplayersInfos[i]["RawMouseDeviceHandle"]);

                IsExpanded.Add((string)JplayersInfos[i]["IsExpanded"]);
                ScreenPrioritys.Add((int)JplayersInfos[i]["ScreenPriority"]);
                ScreenIndexes.Add((int)JplayersInfos[i]["ScreenIndex"]);

                IdealProcessors.Add((string)JplayersInfos[i]["Processor"]["IdealProcessor"]);
                Affinitys.Add((string)JplayersInfos[i]["Processor"]["ProcessorAffinity"]);
                PriorityClasses.Add((string)JplayersInfos[i]["Processor"]["ProcessorPriorityClass"]);
            }

            JToken JaudioSettings = Jprofile["AudioSettings"] as JToken;

            foreach (JProperty JaudioSetting in JaudioSettings)
            {
                if (JaudioSetting.Name.Contains("Custom"))
                {
                    AudioCustomSettings = (bool)JaudioSetting.Value;
                }
                else
                {
                    AudioDefaultSettings = (bool)JaudioSetting.Value;
                }
            }

            AudioInstances.Clear();

            JToken JAudioInstances = Jprofile["AudioInstances"] as JToken;
            foreach (JProperty JaudioDevice in JAudioInstances)
            {
                AudioInstances.Add((string)JaudioDevice.Name, (string)JaudioDevice.Value);
            }

            JToken JAllscreens = Jprofile["AllScreens"] as JToken;
            for (int s = 0; s < JAllscreens.Count(); s++)
            {
                AllScreens.Add(new Rectangle((int)JAllscreens[s]["X"], (int)JAllscreens[s]["Y"], (int)JAllscreens[s]["Width"], (int)JAllscreens[s]["Height"]));
            }

            bool hasKeyboard = false;

            if (AutoPlay)
            {
                positionsControl.GamepadTimer_Tick(null);

                foreach (int index in ScreenIndexes)//Check if there is enough screens
                {
                    if (index > ScreensUtil.AllScreens().Length)
                    {
                        Globals.MainOSD.Settings(2000, Color.Orange, $"Requires {index + 1} Screens");
                        Reset();
                        return false;
                    }
                }

                int allPlayers = 0;

                foreach (PlayerInfo player in PlayerData)
                {
                    if (IsKeyboardPlayer.Contains(true))
                    {
                        hasKeyboard = true;
                        break;
                    }

                    if (GamepadsGuid.Contains(player.GamepadGuid))
                    {
                        allPlayers++;
                    }
                }

                if (hasKeyboard)
                {
                    Globals.MainOSD.Settings(2000, Color.Orange, $"Game Profile N°{_profileToLoad} Loaded\nThe Auto Play Option Is Not Compatible With Keyboards");
                    AutoPlay = false;
                }

                if (allPlayers < PlayerIDs.Count && !hasKeyboard)
                {
                    AutoPlay = false;
                    positionsControl.UpdatePlayers();
                }
            }

            foreach (int index in ScreenIndexes)//Check if there is enough screens
            {
                if (index > ScreensUtil.AllScreens().Length)
                {
                    Globals.MainOSD.Settings(2000, Color.Orange, $"Can't Find The Required Screen(s)");
                    Reset();
                    return false;
                }
            }

            totalPlayers = JplayersInfos.Count();
            modeText = $"Profile n°{profileToSave}";

            string mod1 = string.Empty;
            string mod2 = " Loaded";

            if (AutoPlay)
            {
                mod1 = "Starting ";
                mod2 = string.Empty;
            }

            Globals.MainOSD.Settings(2000, Color.YellowGreen, $"{mod1}Game Profile N°{_profileToLoad}{mod2}");

            LogManager.Log($"Game profile n°{_profileToLoad} Loaded");
            positionsControl.profileSettings_Tooltip.SetToolTip(positionsControl.profileSettings_btn, $"{GameProfile.Game.GameName} {GameProfile.ModeText.ToLower()} settings.");

            Ready = true;
            return true;
        }

        public static void saveUserProfile(GameProfile profile)
        {
            string path;
            bool profileDisabled = bool.Parse(Globals.ini.IniReadValue("Misc", "DisableGameProfiles"));

            if (profilesCount + 1 >= 21 || profileDisabled)
            {
                if (!profileDisabled)
                {
                    Globals.MainOSD.Settings(2000, Color.Orange, $"Limit Of 20 Profiles Has Been Reach Already");
                }

                saved = true;
                return;
            }

            if (profile.PlayerData.Count != totalPlayers || ModeText == "New Profile" || profilesCount == 0)
            {
                profilesCount++;//increase to set new profile name
                path = Path.Combine(Application.StartupPath, $"games profiles\\{GameGUID}\\Profile[{profilesCount}].json");
                //Console.WriteLine("Creating new a game profile...");
            }
            else
            {
                path = Path.Combine(Application.StartupPath, $"games profiles\\{GameGUID}\\Profile[{profileToSave}].json");
            }

            if (!Directory.Exists(Path.Combine(Application.StartupPath, $"games profiles\\{GameGUID}")))
            {
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, $"games profiles\\{GameGUID}"));
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

            JObject JCustomLayout = new JObject(new JProperty("Ver", customLayout_Ver),
                                                new JProperty("Hor", customLayout_Hor),
                                                new JProperty("Max", CustomLayout_Max));
            JObject JUseSplitDiv = new JObject(new JProperty("Enabled", useSplitDiv),
                                               new JProperty("Color", splitDivColor));

            JObject JAutoDesktopScaling = new JObject(new JProperty("Enabled", autoDesktopScaling));
            JObject JUseNicknames = new JObject(new JProperty("Use", useNicknames));
            JObject JNetwork = new JObject(new JProperty("Type", network));
            JObject JAutoPlay = new JObject(new JProperty("Enabled", autoPlay));
            JObject JAudioInstances = new JObject();
            JObject JCts_Settings = new JObject(new JProperty("Cutscenes_KeepAspectRatio", cts_KeepAspectRatio),
                                                new JProperty("Cutscenes_MuteAudioOnly", cts_MuteAudioOnly),
                                                new JProperty("Cutscenes_Unfocus", cts_Unfocus));

            foreach (KeyValuePair<string, string> JaudioDevice in AudioInstances)
            {
                JAudioInstances.Add(new JProperty(JaudioDevice.Key, JaudioDevice.Value));
            }

            JObject JAudioSettings = new JObject(new JProperty("CustomSettings", audioCustomSettings), new JProperty("DefaultSettings", audioDefaultSettings));

            List<PlayerInfo> players = (List<PlayerInfo>)profile.PlayerData.OrderBy(c => c.PlayerID).ToList();//need to do this because sometimes it's reversed
            List<JObject> playersInfos = new List<JObject>();//Players object

            for (int i = 0; i < players.Count(); i++)//build per players object
            {
                JObject JOwner = new JObject();

                JOwner = new JObject(
                                  new JProperty("Type", players[i].Owner.Type),

                                   new JProperty("UiBounds", new JObject(
                                                           new JProperty("X", players[i].Owner.UIBounds.X),
                                                           new JProperty("Y", players[i].Owner.UIBounds.Y),
                                                           new JProperty("Width", players[i].Owner.UIBounds.Width),
                                                           new JProperty("Height", players[i].Owner.UIBounds.Height))),
                                  new JProperty("DisplayIndex", players[i].Owner.DisplayIndex),
                                  new JProperty("Display", new JObject(
                                                           new JProperty("X", players[i].Owner.display.X),
                                                           new JProperty("Y", players[i].Owner.display.Y),
                                                           new JProperty("Width", players[i].Owner.display.Width),
                                                           new JProperty("Height", players[i].Owner.display.Height))));


                JObject JMonitorBounds = new JObject(
                                         new JProperty("X", useSplitDiv ? players[i].MonitorBounds.Location.X - 1 : players[i].MonitorBounds.Location.X),
                                         new JProperty("Y", useSplitDiv ? players[i].MonitorBounds.Location.Y - 1 : players[i].MonitorBounds.Location.Y),
                                         new JProperty("Width", useSplitDiv ? players[i].MonitorBounds.Width + 2 : players[i].MonitorBounds.Width),
                                         new JProperty("Height", useSplitDiv ? players[i].MonitorBounds.Height + 2 : players[i].MonitorBounds.Height));

                JObject JEditBounds = new JObject(
                                      new JProperty("X", players[i].EditBounds.X),
                                      new JProperty("Y", players[i].EditBounds.Y),
                                      new JProperty("Width", players[i].EditBounds.Width),
                                      new JProperty("Height", players[i].EditBounds.Height));

                JObject JProcessor = new JObject();

                if (i < IdealProcessors.Count)
                {
                    JProcessor.Add(new JProperty("IdealProcessor", IdealProcessors[i]));
                }

                if (i < Affinitys.Count)
                {
                    JProcessor.Add(new JProperty("ProcessorAffinity", Affinitys[i]));
                }

                if (i < PriorityClasses.Count)
                {
                    JProcessor.Add(new JProperty("ProcessorPriorityClass", PriorityClasses[i]));
                }

                JObject JPData = new JObject(//build all individual player datas object
                                 new JProperty("PlayerID", players[i].PlayerID),
                                 new JProperty("Nickname", players[i].Nickname),
                                 new JProperty("SteamID", players[i].SteamID),
                                 new JProperty("GamepadGuid", players[i].GamepadGuid),
                                 new JProperty("IsDInput", players[i].IsDInput),
                                 new JProperty("IsXInput", players[i].IsXInput),
                                 new JProperty("Processor", JProcessor),
                                 new JProperty("IsKeyboardPlayer", players[i].IsKeyboardPlayer),
                                 new JProperty("RawKeyboardDeviceHandle", (string)players[i].HIDDeviceID),
                                 new JProperty("IsRawMouse", players[i].IsRawMouse),
                                 new JProperty("RawMouseDeviceHandle", (string)players[i].HIDDeviceID),
                                 new JProperty("IsExpanded", players[i].IsExpanded),
                                 new JProperty("ScreenPriority", players[i].ScreenPriority),
                                 new JProperty("ScreenIndex", players[i].ScreenIndex),
                                 new JProperty("EditBounds", JEditBounds),
                                 new JProperty("MonitorBounds", JMonitorBounds),
                                 new JProperty("Owner", JOwner)
                    );

                playersInfos.Add(JPData);
            }

            List<JObject> JScreens = new List<JObject>();

            for (int s = 0; s < PositionsControl.screens.Count(); s++)
            {
                UserScreen screen = PositionsControl.screens[s];

                JObject JScreen = new JObject(new JProperty("X", screen.UIBounds.X),
                                              new JProperty("Y", screen.UIBounds.Y),
                                              new JProperty("Width", screen.UIBounds.Width),
                                              new JProperty("Height", screen.UIBounds.Height),
                                              new JProperty("Index", s));

                JScreens.Add(JScreen);
            }

            JObject profileJson = new JObject//shared settings object
            (
               new JProperty("Notes", Notes),
               new JProperty("Player(s)", profile.PlayerData.Count),
               new JProperty("AutoPlay", JAutoPlay),
               new JProperty("Data", playersInfos),
               new JProperty("Options", options),
               new JProperty("UseNicknames", JUseNicknames),
               new JProperty("AutoDesktopScaling", JAutoDesktopScaling),
               new JProperty("UseSplitDiv", JUseSplitDiv),
               new JProperty("CustomLayout", JCustomLayout),
               new JProperty("WindowsSetupTiming", JHWndInterval),
               new JProperty("PauseBetweenInstanceLaunch", JPauseBetweenInstanceLaunch),
               new JProperty("Network", JNetwork),
               new JProperty("AudioSettings", JAudioSettings),
               new JProperty("AudioInstances", JAudioInstances),
               new JProperty("CutscenesModeSettings", JCts_Settings),
               new JProperty("AllScreens", JScreens)

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

            saved = true;

            LogManager.Log("Game Profile Saved");
            Globals.MainOSD.Settings(1600, Color.GreenYellow, $"Game Profile Saved");
        }

        public static GameProfile CleanClone(GameProfile profile)
        {
            GameProfile nprof = new GameProfile
            {
                playerData = new List<PlayerInfo>(),
                screens = profile.screens.ToList(),
            };

            List<PlayerInfo> source;

            source = profile.playerData;

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
