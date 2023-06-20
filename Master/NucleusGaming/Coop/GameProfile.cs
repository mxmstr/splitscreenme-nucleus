using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nucleus.Coop;
using Nucleus.Gaming.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Nucleus.Gaming.Coop
{
    public class GameProfile
    {
        private readonly IniFile ini = Globals.ini;

        private List<UserScreen> screens;
        public List<UserScreen> Screens => screens;
        private List<PlayerInfo> playerData;

        public static List<Rectangle> AllScreens = new List<Rectangle>();

        public static GameProfile currentProfile;
        public static GenericGameInfo Game;
        private SetupScreen setupScreen = null;

        private static int totalPlayers = 0;
        public static int TotalPlayers => totalPlayers;

        private static int profilesCount = 0;//Used to check if we need to create a new profile 
        private static int profileToSave;

        private static string modeText = "New Profile";
        public static string ModeText => modeText;

        public static string GameGUID;

        public static List<string> profilesPathList = new List<string>();

        public Dictionary<string, object> Options => options;
        private Dictionary<string, object> options;

        public static IDictionary<string, string> AudioInstances = new Dictionary<string, string>();

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

        private static string title;
        public static string Title
        {
            get => title;
            set => title = value;
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

        private static int gamepadCount;
        public static int GamepadCount => gamepadCount;

        private static int keyboardCount;
        public static int KeyboardCount => keyboardCount;



        private static bool saved = false;
        public static bool Saved => saved;

        public static bool Ready = false;

        private static bool useXinputIndex;

        /// <summary>
        /// Return a list of all players(connected devices)
        /// </summary>
        public List<PlayerInfo> PlayersList => playerData;

        //private List<PlayerInfo> GetPlayerList()
        //{
        //    lock (playerData)
        //    {
        //        return playerData;
        //    }
        //}

        public static List<ProfilePlayer> ProfilePlayersList = new List<ProfilePlayer>();

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

        public void Reset()
        {
            ProfilePlayersList.Clear();
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
            useXinputIndex = bool.Parse(ini.IniReadValue("Dev", "UseXinputIndex"));

            notes = string.Empty;
            title = string.Empty;

            hWndInterval = 0;
            pauseBetweenInstanceLaunch = 0;

            profileToSave = 0;
            totalPlayers = 0;

            gamepadCount = 0;
            keyboardCount = 0;

            modeText = "New Profile";

            Ready = false;
            saved = false;

            if (playerData != null)//Switching profile
            {
                foreach (PlayerInfo player in playerData)
                {
                    player.EditBounds = player.SourceEditBounds;
                    player.ScreenIndex = -1;
                    player.PlayerID = -1;
                    player.SteamID = -1;
                    player.Nickname = null;
                }
            }

            options = new Dictionary<string, object>();

            foreach (GameOption opt in Game.Options)
            {
                options.Add(opt.Key, opt.Value);
            }

            Label unload = new Label();

            setupScreen.ResetScreensTotalPlayers();
            setupScreen.gameProfilesList.profileBtn_CheckedChanged(unload, null);
            setupScreen.loadedProfilePlayers.Clear();

            setupScreen.profileSettings_Tooltip = CustomToolTips.SetToolTip(setupScreen.profileSettings_btn, $"{GameProfile.Game.GameName} {GameProfile.ModeText.ToLower()} settings.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            ListGameProfiles();
        }

        public void InitializeDefault(GenericGameInfo game, SetupScreen pc)
        {
            currentProfile = this;
            Game = game;
            setupScreen = pc;

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

        public bool LoadGameProfile(int _profileToLoad)
        {
            string path = Path.Combine(Application.StartupPath, $"games profiles\\{GameGUID}\\Profile[{_profileToLoad}].json");

            string jsonString = File.ReadAllText(path);

            JObject Jprofile = (JObject)JsonConvert.DeserializeObject(jsonString);

            Reset();

            profileToSave = _profileToLoad;///to keep after reset()

            if (Jprofile == null)
            {
                Ready = false;
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
            Title = (string)Jprofile["Title"];

            JToken JplayersInfos = Jprofile["Data"] as JToken;

            for (int i = 0; i < JplayersInfos.Count(); i++)
            {
                ProfilePlayer player = new ProfilePlayer();

                player.PlayerID = (int)JplayersInfos[i]["PlayerID"];
                player.Nickname = (string)JplayersInfos[i]["Nickname"];
                player.SteamID = (long)JplayersInfos[i]["SteamID"];
                player.GamepadGuid = (Guid)JplayersInfos[i]["GamepadGuid"];
                player.OwnerType = (int)JplayersInfos[i]["Owner"]["Type"];
                player.DisplayIndex = (int)JplayersInfos[i]["Owner"]["DisplayIndex"];

                player.OwnerDisplay =new Rectangle(
                                               (int)JplayersInfos[i]["Owner"]["Display"]["X"],
                                               (int)JplayersInfos[i]["Owner"]["Display"]["Y"],
                                               (int)JplayersInfos[i]["Owner"]["Display"]["Width"],
                                               (int)JplayersInfos[i]["Owner"]["Display"]["Height"]);

                player.OwnerUIBounds = new Rectangle(
                                               (int)JplayersInfos[i]["Owner"]["UiBounds"]["X"],
                                               (int)JplayersInfos[i]["Owner"]["UiBounds"]["Y"],
                                               (int)JplayersInfos[i]["Owner"]["UiBounds"]["Width"],
                                               (int)JplayersInfos[i]["Owner"]["UiBounds"]["Height"]);


                player.MonitorBounds = new Rectangle(
                                               (int)JplayersInfos[i]["MonitorBounds"]["X"],
                                               (int)JplayersInfos[i]["MonitorBounds"]["Y"],
                                               (int)JplayersInfos[i]["MonitorBounds"]["Width"],
                                               (int)JplayersInfos[i]["MonitorBounds"]["Height"]);

                player.EditBounds = new Rectangle(
                                            (int)JplayersInfos[i]["EditBounds"]["X"],
                                            (int)JplayersInfos[i]["EditBounds"]["Y"],
                                            (int)JplayersInfos[i]["EditBounds"]["Width"],
                                            (int)JplayersInfos[i]["EditBounds"]["Height"]);


                player.IsDInput = (bool)JplayersInfos[i]["IsDInput"];
                player.IsXInput = (bool)JplayersInfos[i]["IsXInput"];

                player.IsKeyboardPlayer = (bool)JplayersInfos[i]["IsKeyboardPlayer"];
                player.IsRawMouse = (bool)JplayersInfos[i]["IsRawMouse"];

                string[] hidIds = new string[] { "", "" };
                for (int h = 0; h < JplayersInfos[i]["HIDDeviceID"].Count(); h++)
                {
                    hidIds.SetValue(JplayersInfos[i]["HIDDeviceID"][h].ToString(), h);
                }

                player.HIDDeviceIDs = hidIds;

                player.ScreenPriority = (int)JplayersInfos[i]["ScreenPriority"];
                player.ScreenIndex = (int)JplayersInfos[i]["ScreenIndex"];

                player.IdealProcessor = (string)JplayersInfos[i]["Processor"]["IdealProcessor"];
                player.Affinity = (string)JplayersInfos[i]["Processor"]["ProcessorAffinity"];
                player.PriorityClass = (string)JplayersInfos[i]["Processor"]["ProcessorPriorityClass"];

                if (player.IsXInput || player.IsDInput)
                {
                    gamepadCount++;
                }
                else
                {
                    keyboardCount++;
                }

                ProfilePlayersList.Add(player);
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

            if(ProfilePlayersList.Any(pl => pl.ScreenIndex > ScreensUtil.AllScreens().Count()-1))//ensure that the missing screens are used by any players before showing message
            {             
                Globals.MainOSD.Show(2000, $"There Is Not Enough Active Screens");
                return false;
            }

            totalPlayers = JplayersInfos.Count();
            modeText = $"Profile n°{profileToSave}";
           
            if(ProfilePlayersList.Any(x => x.IsXInput == true) && !useXinputIndex)
            {
                Globals.MainOSD.Show(2000, $"Press A Button On Each GamePad");
            }
            else 
            {
                Globals.MainOSD.Show(2000, $"Game Profile N°{_profileToLoad} loaded");
            }

            setupScreen.profileSettings_Tooltip.SetToolTip(setupScreen.profileSettings_btn, $"{GameProfile.Game.GameName} {GameProfile.ModeText.ToLower()} settings.");

            Ready = true;
           
            return true;
        }

        public static void UpdateGameProfile(GameProfile profile)
        {
            string path;

            bool profileDisabled = bool.Parse(Globals.ini.IniReadValue("Misc", "DisableGameProfiles"));

            if (profilesCount + 1 >= 21 || profileDisabled)
            {
                if (!profileDisabled)
                {
                    Globals.MainOSD.Show(2000, $"Limit Of 20 Profiles Has Been Reach Already");
                }

                return;
            }

            if (ModeText == "New Profile" || profilesCount == 0)
            {
                profilesCount++;//increase to set new profile name
                path = Path.Combine(Application.StartupPath, $"games profiles\\{GameGUID}\\Profile[{profilesCount}].json");
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
            JObject JCts_Settings = new JObject(new JProperty("Cutscenes_KeepAspectRatio", cts_KeepAspectRatio),
                                                new JProperty("Cutscenes_MuteAudioOnly", cts_MuteAudioOnly),
                                                new JProperty("Cutscenes_Unfocus", cts_Unfocus));

            JObject JAudioInstances = new JObject();

            foreach (KeyValuePair<string, string> JaudioDevice in AudioInstances)
            {
                JAudioInstances.Add(new JProperty(JaudioDevice.Key, JaudioDevice.Value));
            }

            JObject JAudioSettings = new JObject(new JProperty("CustomSettings", audioCustomSettings), new JProperty("DefaultSettings", audioDefaultSettings));

            List<JObject> playersInfos = new List<JObject>();//Players object

            int gamepadCount = 0;
            int keyboardCount = 0;

            for (int i = 0; i < ProfilePlayersList.Count(); i++)//build per players object
            {
                ProfilePlayer player = ProfilePlayersList[i];

                if(player.IsXInput || player.IsDInput)
                {
                    gamepadCount++;
                }
                else
                {
                    keyboardCount++;
                }

                JObject JOwner = new JObject(
                                      new JProperty("Type", player.OwnerType),

                                       new JProperty("UiBounds", new JObject(
                                                               new JProperty("X", player.OwnerUIBounds.X),
                                                               new JProperty("Y", player.OwnerUIBounds.Y),
                                                               new JProperty("Width", player.OwnerUIBounds.Width),
                                                               new JProperty("Height", player.OwnerUIBounds.Height))),

                                      new JProperty("DisplayIndex", player.DisplayIndex),
                                      new JProperty("Display", new JObject(
                                                               new JProperty("X", player.OwnerDisplay.X),
                                                               new JProperty("Y", player.OwnerDisplay.Y),
                                                               new JProperty("Width", player.OwnerDisplay.Width),
                                                               new JProperty("Height", player.OwnerDisplay.Height))));

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

                JObject JProcessor = new JObject
                {
                    new JProperty("IdealProcessor", player.IdealProcessor),
                    new JProperty("ProcessorAffinity", player.Affinity),
                    new JProperty("ProcessorPriorityClass", player.PriorityClass)
                };

                JObject JPData = new JObject(//build all individual player datas object
                                 new JProperty("PlayerID", player.PlayerID),
                                 new JProperty("Nickname", player.Nickname),
                                 new JProperty("SteamID", player.SteamID),
                                 new JProperty("GamepadGuid", player.GamepadGuid),
                                 new JProperty("IsDInput", player.IsDInput),
                                 new JProperty("IsXInput", player.IsXInput),
                                 new JProperty("Processor", JProcessor),
                                 new JProperty("IsKeyboardPlayer", player.IsKeyboardPlayer),
                                 new JProperty("IsRawMouse", player.IsRawMouse),
                                 new JProperty("HIDDeviceID", player.HIDDeviceIDs),
                                 new JProperty("ScreenPriority", player.ScreenPriority),
                                 new JProperty("ScreenIndex", player.ScreenIndex),
                                 new JProperty("EditBounds", JEditBounds),
                                 new JProperty("MonitorBounds", JMonitorBounds),
                                 new JProperty("Owner", JOwner)
                    );

                playersInfos.Add(JPData);
            }

            List<JObject> JScreens = new List<JObject>();

            for (int s = 0; s < AllScreens.Count(); s++)
            {
                JObject JScreen = new JObject(new JProperty("X", AllScreens[s].X),
                                              new JProperty("Y", AllScreens[s].Y),
                                              new JProperty("Width", AllScreens[s].Width),
                                              new JProperty("Height", AllScreens[s].Height)
                                              );

                JScreens.Add(JScreen);
            }

            JObject profileJson = new JObject//shared settings object
            (
               new JProperty("Title", Title),
               new JProperty("Notes", Notes),
               new JProperty("Player(s)", ProfilePlayersList.Count),
               new JProperty("Controller(s)", gamepadCount),
               new JProperty("Use XInput Index", useXinputIndex),
               new JProperty("K&M", keyboardCount),
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

            modeText = $"Profile n°{profileToSave}";

            Globals.MainOSD.Show(1600, $"Game Profile Updated");
        }

        public static void SaveGameProfile(GameProfile profile)
        {
            string path;
            bool profileDisabled = bool.Parse(Globals.ini.IniReadValue("Misc", "DisableGameProfiles"));

            if (profilesCount + 1 >= 21 || profileDisabled)
            {
                if (!profileDisabled)
                {
                    Globals.MainOSD.Show(2000, $"Limit Of 20 Profiles Has Been Reach Already");
                }

                saved = true;
                return;
            }

            if (profile.playerData.Count != totalPlayers || ModeText == "New Profile" || profilesCount == 0)
            {
                profilesCount++;//increase to set new profile name
                path = Path.Combine(Application.StartupPath, $"games profiles\\{GameGUID}\\Profile[{profilesCount}].json");

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

            List<PlayerInfo> players = (List<PlayerInfo>)profile.PlayersList.OrderBy(c => c.PlayerID).ToList();//need to do this because sometimes it's reversed
            List<JObject> playersInfos = new List<JObject>();//Players object

            int gamepadCount = 0;
            int keyboardCount = 0;

            for (int i = 0; i < players.Count(); i++)//build per players object
            {
                if (players[i].IsXInput || players[i].IsDInput)
                {
                    gamepadCount++;
                }
                else
                {
                    keyboardCount++;
                }

                JObject JOwner = new JObject(
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

                JObject JProcessor = new JObject
                {
                    new JProperty("IdealProcessor", players[i].IdealProcessor),
                    new JProperty("ProcessorAffinity", players[i].Affinity),
                    new JProperty("ProcessorPriorityClass", players[i].PriorityClass)
                };
   
                JObject JPData = new JObject(//build all individual player datas object
                                 new JProperty("PlayerID", players[i].PlayerID),
                                 new JProperty("Nickname", players[i].Nickname),
                                 new JProperty("SteamID", players[i].SteamID),
                                 new JProperty("GamepadGuid", players[i].GamepadGuid),
                                 new JProperty("IsDInput", players[i].IsDInput),
                                 new JProperty("IsXInput", players[i].IsXInput),
                                 new JProperty("Processor", JProcessor),
                                 new JProperty("IsKeyboardPlayer", players[i].IsKeyboardPlayer),
                                 new JProperty("IsRawMouse", players[i].IsRawMouse),
                                 new JProperty("HIDDeviceID", players[i].HIDDeviceID),
                                 new JProperty("ScreenPriority", players[i].ScreenPriority),
                                 new JProperty("ScreenIndex", players[i].ScreenIndex),
                                 new JProperty("EditBounds", JEditBounds),
                                 new JProperty("MonitorBounds", JMonitorBounds),
                                 new JProperty("Owner", JOwner)
                                 );

                playersInfos.Add(JPData);
            }

            List<JObject> JScreens = new List<JObject>();

            for (int s = 0; s < SetupScreen.screens.Count(); s++)
            {
                UserScreen screen = SetupScreen.screens[s];

                JObject JScreen = new JObject(new JProperty("X", screen.UIBounds.X),
                                              new JProperty("Y", screen.UIBounds.Y),
                                              new JProperty("Width", screen.UIBounds.Width),
                                              new JProperty("Height", screen.UIBounds.Height)
                                             );
                JScreens.Add(JScreen);
            }

            JObject profileJson = new JObject//shared settings object
            (
               new JProperty("Title", Title),
               new JProperty("Notes", Notes),
               new JProperty("Player(s)", profile.playerData.Count),
               new JProperty("Controller(s)", gamepadCount),
               new JProperty("K&M", keyboardCount),
               new JProperty("Use XInput Index", useXinputIndex),
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
            Globals.MainOSD.Show(1600, $"Game Profile Saved");
        }

        public static GameProfile CleanClone(GameProfile profile)
        {
            GameProfile nprof = new GameProfile
            {
                playerData = new List<PlayerInfo>(),
                screens = profile.screens.ToList(),
            };

            List<PlayerInfo> source = profile.playerData;


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
