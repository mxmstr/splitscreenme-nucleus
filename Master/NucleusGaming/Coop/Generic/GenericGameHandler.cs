﻿using Microsoft.Win32;
using Nucleus.Coop;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.Generic;
using Nucleus.Gaming.Coop.Generic.Cursor;
using Nucleus.Gaming.Coop.InputManagement;
using Nucleus.Gaming.Coop.InputManagement.Gamepads;
using Nucleus.Gaming.Coop.ProtoInput;
using Nucleus.Gaming.Forms;
using Nucleus.Gaming.Platform.PCSpecs;
using Nucleus.Gaming.Tools.AudioReroute;
using Nucleus.Gaming.Tools.BackupFiles;
using Nucleus.Gaming.Tools.DevReorder;
using Nucleus.Gaming.Tools.DInputBlocker;
using Nucleus.Gaming.Tools.DirectX9Wrapper;
using Nucleus.Gaming.Tools.DllsInjector;
using Nucleus.Gaming.Tools.EACBypass;
using Nucleus.Gaming.Tools.FlawlessWidescreen;
using Nucleus.Gaming.Tools.GameStarter;
using Nucleus.Gaming.Tools.GlobalWindowMethods;
using Nucleus.Gaming.Tools.HexEdit;
using Nucleus.Gaming.Tools.MonitorsDpiScaling;
using Nucleus.Gaming.Tools.NemirtingasEpicEmu;
using Nucleus.Gaming.Tools.NemirtingasGalaxyEmu;
using Nucleus.Gaming.Tools.Network;
using Nucleus.Gaming.Tools.NucleusUsers;
using Nucleus.Gaming.Tools.Steam;
using Nucleus.Gaming.Tools.WindowFakeFocus;
using Nucleus.Gaming.Tools.X360ce;
using Nucleus.Gaming.Tools.XInputPlusDll;
using Nucleus.Gaming.Util;
using Nucleus.Gaming.Windows;
using Nucleus.Gaming.Windows.Interop;
using Nucleus.Interop.User32;
using SharpDX.DirectWrite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Media;
using WindowScrape.Constants;


namespace Nucleus.Gaming
{
    public class GenericGameHandler : IGameHandler, ILogNode
    {
        public void TriggerOSD(int timerMS, string text)
        {
            Globals.MainOSD.Show(timerMS, text);
        }

        public List<WPFDiv> splitForms = new List<WPFDiv>();
        public string keyboardInstance;
        public string logMsg;
        public string origExePath;
        public string NucleusEnvironmentRoot => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        public string DocumentsRoot => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string nucleusFolderPath;
        public string exePath;
        public string instanceExeFolder;
        public string garch;
        public string nucleusUserAccountsPassword = "12345";
        public string JsFilename;
        public string HandlerGUID;
        public static string ofdPath;
        public string ssePath = string.Empty;
        public string[] folderUsers;

        public string nucleusRootFolder;
        public string tempDir;
        public string exeFolder;
        public string rootFolder;
        public string workingFolder;
        public string linkFolder;
        public string linkBinFolder;
        public string origRootFolder;
        public Window nextWindowToInject = null;
        public List<PlayerInfo> players;
        public PlayerInfo player;
        public Process proc = null;
        public string startArgs;
        public Rectangle playerBounds;
        public ProcessData data;
        public Process cmd;
        public string[] cmdOps;

        public int prevProcId = 0;
        public int plyrIndex = 0;
        public int kbi = 1;
        public int origWidth = 0;
        public int origHeight = 0;
        public int playerBoundsWidth = 0;
        public int playerBoundsHeight = 0;
        public int prevWindowWidth = 0;
        public int prevWindowHeight = 0;
        public int prevWindowX = 0;
        public int prevWindowY = 0;
        public int exited;
        public int[] procOrder;
        public int keyboardProcId;
        public int numPlayers = 0;
        public string mutexNames = "";
        public string workingDir = null;

        protected int totalPlayers;
        public int TotalPlayers => totalPlayers;

        public double origRatio = 1;
        public double timer { get; set; }
        protected double timerInterval = 1000;
        public double TimerInterval => timerInterval;

        public int HWndInterval = 10000;

        internal CursorModule _cursorModule { get; set; }
        public GameProfile profile;
        public GenericGameInfo gen;
        public GenericGameInfo currentGameInfo => gen;
        public GenericContext context;
        public static GenericGameHandler instance;
        public Thread statusWinThread;
        public UserGameInfo userGame;
        public GameManager gameManager;
        public ProcessData prevProcessData;
        public Process launchProc;
        public UserScreen owner;
        public readonly IniFile ini = Globals.ini;
        public Thread FakeFocus => WindowFakeFocus.fakeFocus;
        //public Thread _ControllersShortcuts = ControllersShortcuts.ctrlsShortcuts;

        public event Action Ended;
        public static GenericGameHandler Instance => instance;

        public Dictionary<string, string> jsData;

        public List<Process> attachedLaunchers = new List<Process>();
        public List<int> attachedIdsLaunchers = new List<int>();
        public List<int> mutexProcs = new List<int>();
        public List<string> userBackedFiles = new List<string>();
        public List<Display> screensInUse = new List<Display>();
        public List<string> nucUsers = new List<string>();
        public List<string> nucSIDs = new List<string>();
        public List<string> addedFiles = new List<string>();
        public List<string> backupFiles = new List<string>();
        public List<Process> attached = new List<Process>();
        public List<int> attachedIds = new List<int>();

        public bool earlyExit;
        public bool processingExit = false;
        public bool useDocs;
        public bool symlinkNeeded;
        public bool isPrevent;
        public bool hasKeyboardPlayer = false;
        public bool hasEnded;
        public virtual bool HasEnded => hasEnded;
        public bool gameIs64 { get; set; }
        public bool UsingNucleusAccounts;
        public bool isDebug;
        public bool dllResize = false;
        public bool dllRepos = false;
        public string error;

        public bool Initialize(UserGameInfo game, GameProfile profile, IGameHandler handler)
        {
            instance = this;
            userGame = game;
            this.profile = profile;

            GlobalWindowMethods.profile = profile;

            if (ini.IniReadValue("Misc", "DebugLog") == "True")
            {
                isDebug = true;
            }

            Network.iniNetworkInterface = GameProfile.Network;

            List<PlayerInfo> players = profile.DevicesList;

            gen = game.Game as GenericGameInfo;
            // see if we have any save game to backup
            if (gen == null)
            {
                // you fucked up
                return false;
            }

            RawInputProcessor.CurrentGameInfo = gen;

            try
            {
                for (int pl = 0; pl < players.Count; pl++)
                {
                    if (players[pl].IsKeyboardPlayer)
                    {
                        hasKeyboardPlayer = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " " + ex.StackTrace);
            }

            if (gen.LockMouse)
            {
                _cursorModule = new CursorModule();
            }

            jsData = new Dictionary<string, string>
            {
                { Folder.Documents.ToString(), Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) },
                { Folder.MainGameFolder.ToString(), Path.GetDirectoryName(game.ExePath) },
                { Folder.InstancedGameFolder.ToString(), Path.GetDirectoryName(game.ExePath) }
            };

            timerInterval = gen.HandlerInterval;

            LogManager.RegisterForLogCallback(this);

            JsFilename = gen.JsFileName;
            HandlerGUID = gen.GUID;

            if (gen.LaunchAsDifferentUsers || gen.LaunchAsDifferentUsersAlt)
            {
                UsingNucleusAccounts = true;
            }

            if (GameProfile.HWndInterval > 0)
            {
                HWndInterval = GameProfile.HWndInterval;
                Log($"Set Windows Setup Timing to {HWndInterval} ms");
            }

            totalPlayers = profile.DevicesList.Count;

            error = null;

            // ThreadPool.QueueUserWorkItem(StartPlay, handler);

            Thread PlayThread = new Thread(delegate ()
            {
                StartPlay(handler);
                System.Windows.Threading.Dispatcher.Run();
            });

            PlayThread.SetApartmentState(ApartmentState.STA); // needs to be STA or throws exception
            PlayThread.Start();

            if (TimerInterval > 0)
            {
                StartUpdateTick(gen.HandlerInterval);
            }

            return true;
        }

        private void StartPlay(object handler)
        {
            try
            {
                error = ((IGameHandler)handler).Play();
            }
            catch (Exception ex)
            {
                error = ex.Message;

                try
                {
                    RegistryUtil.RestoreRegistry("Error Restore from GenericGameHandler");
                    // try to save the exception
                    LogManager.Instance.LogExceptionFile(ex);
                }
                catch
                {
                    LogManager.Instance.LogExceptionFile(ex);
                    error = "We failed so hard we failed while trying to record the reason we failed initially. Sorry.";
                    return;
                }
            }
        }

        public void ShowStatus()
        {
            try
            {
                string lblTxt = "Engine starting up";
                if (processingExit)
                {
                    lblTxt = "Starting shut down procedures";
                }

                Label statusLbl = new Label
                {
                    Text = lblTxt,
                    Width = 560,
                    Height = 93,
                    AutoSize = false,
                    Location = new Point(12, 9),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                //private Form statusForm;
                Form statusForm = new Form()
                {
                    Text = "Nucleus Coop - Status",
                    Width = 600,
                    Height = 150,

                    ControlBox = true,
                    StartPosition = FormStartPosition.Manual,
                    Location = new Point(0, 0),
                    MaximizeBox = false,
                    MinimizeBox = false,
                    FormBorderStyle = FormBorderStyle.FixedSingle,
                    TopMost = true,
                    AutoScaleMode = AutoScaleMode.Font,
                    AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F)
                };

                statusForm.FormClosing += new FormClosingEventHandler(StatusForm_Closing);
                statusForm.Controls.Add(statusLbl);
                statusForm.Show();

                while (statusWinThread != null && statusWinThread.IsAlive)
                {
                    try
                    {
                        if (statusWinThread != null && statusWinThread.IsAlive)
                        {
                            Thread.Sleep(100);

                            statusForm.TopMost = true;
                            statusForm.TopMost = false;
                            statusForm.TopMost = true;
                            statusLbl.Text = logMsg;
                            Application.DoEvents();

                            WindowScrape.Static.HwndInterface.MakeTopMost(statusForm.Handle);

                            try
                            {
                                statusForm.Refresh();
                            }
                            catch { }
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        public string Play()
        {

            if (ini.IniReadValue("Misc", "IgnoreInputLockReminder") != "True")
            {
                MessageBox.Show("Some handlers will require you to press the End key to lock input. Remember to unlock input by pressing End again when you finish playing. You can disable this message in the Settings. ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //if (gen.NeedSteamClient)
            //{
            //    SteamFunctions.StartSteamClient();            
            //}

            if (gen.HideTaskbar && !GameProfile.UseSplitDiv)
            {
                User32Util.HideTaskbar();
            }

            if (gen.ProtoInput.AutoHideTaskbar || GameProfile.UseSplitDiv)
            {
                if (ProtoInput.protoInput.GetTaskbarAutohide())
                {
                    gen.ProtoInput.AutoHideTaskbar = false; // If already hidden don't change it, and dont set it unhidden after.
                }
                else
                {
                    ProtoInput.protoInput.SetTaskbarAutohide(true);
                }
            }

            garch = "x86";
            if (MachineSpecs.GetMachineArch(userGame.ExePath) == true)
            {
                gameIs64 = true;
                garch = "x64";
            }

            if (gen.ForceGameArch?.Length > 0)
            {
                if (gen.ForceGameArch == "x86")
                {
                    gameIs64 = false;
                    garch = "x86";
                }
                else if (gen.ForceGameArch == "x64")
                {
                    gameIs64 = true;
                    garch = "x64";
                }
            }

            if (isDebug)
            {
                Log("--------------------- START ---------------------");
                Log(string.Format("Game: {0}, Arch: {1}, Executable: {2}, Launcher: {3}, SteamID: {4}, Handler: {5}, Content Folder: {6}", gen.GameName, garch, gen.ExecutableName, gen.LauncherExe, gen.SteamID, gen.JsFileName, gen.GUID));
                MachineSpecs.GetPCspecs(this);
            }

            ProcessUtil.KillRemainingProcess(this, gen);

            //Merge raw keyboard/mouse players into one 
            var groupWindows = profile.DevicesList.Where(x => x.IsRawKeyboard || x.IsRawMouse).GroupBy(x => x.MonitorBounds).ToList();

            foreach (var group in groupWindows)
            {
                if (group.Count() == 1)
                {
                    continue;//skip already merged k&m devices on profile load 
                }

                var firstInGroup = group.First();
                var secondInGroup = group.Last();

                firstInGroup.IsRawKeyboard = group.Count(x => x.IsRawKeyboard) > 0;
                firstInGroup.IsRawMouse = group.Count(x => x.IsRawMouse) > 0;

                if (firstInGroup.IsRawKeyboard) firstInGroup.RawKeyboardDeviceHandle = group.First(x => x.RawKeyboardDeviceHandle != (IntPtr)(-1)).RawKeyboardDeviceHandle;
                if (firstInGroup.IsRawMouse) firstInGroup.RawMouseDeviceHandle = group.First(x => x.RawMouseDeviceHandle != (IntPtr)(-1)).RawMouseDeviceHandle;


                firstInGroup.HIDDeviceID = new string[2] { firstInGroup.HIDDeviceID[0], secondInGroup.HIDDeviceID[0] };

                int insertAt = profile.DevicesList.FindIndex(toInsert => toInsert == firstInGroup);//Get index of the player so it can be re-inserted where it was.

                foreach (var x in group)
                {
                    profile.DevicesList.Remove(x);
                }

                profile.DevicesList.Insert(insertAt, firstInGroup);//Re-insert the player where it was before its deletion  
            }

            players = profile.DevicesList;

            Log("Determining which monitors will be used by Nucleus");

            foreach (Display dp in ScreensUtil.AllScreensParams())
            {
                if (players.Any(p => p.Owner.DisplayIndex == dp.DisplayIndex))
                {
                    screensInUse.Add(dp);
                }

                if (screensInUse.Contains(dp))
                {
                    if ((GameProfile.UseSplitDiv == true && gen.SplitDivCompatibility == true) || gen.HideDesktop)
                    {
                        WPFDivFormThread.StartBackgroundForm(gen, dp);
                    }
                }
            }

            gen.SetPlayerList(players);

            gen.SetProtoInputValues();

            if (GameProfile.AutoDesktopScaling == true)
            {
                MonitorsDpiScaling.SetupMonitors(this);
            }
            else
            {
                Log("The Windows deskop scale will not be set to 100% because this option has been disabled in settings or game profile");
            }

            UserScreen[] all = profile.Screens.ToArray();

            Log(string.Format("Display - DPIHandling: {0}, DPI Scale: {1}", gen.DPIHandling, DPIManager.Scale));
            for (int x = 0; x < all.Length; x++)
            {
                Log(string.Format("Monitor {0} - Resolution: {1}", x, all[x].MonitorBounds.Width + "x" + all[x].MonitorBounds.Height));
            }

            string nucleusRootFolder = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            nucleusFolderPath = nucleusRootFolder;

            tempDir = GameManager.Instance.GempTempFolder(gen);
            exeFolder = Path.GetDirectoryName(userGame.ExePath).ToLower();
            rootFolder = exeFolder;
            workingFolder = exeFolder;

            if (!string.IsNullOrEmpty(gen.BinariesFolder))
            {
                rootFolder = StringUtil.ReplaceCaseInsensitive(exeFolder, gen.BinariesFolder.ToLower(), "");
            }

            if (!string.IsNullOrEmpty(gen.WorkingFolder))
            {
                workingFolder = Path.Combine(exeFolder, gen.WorkingFolder.ToLower());
            }

            gen.LockInputToggleKey = RawInputProcessor.ToggleLockInputKey;

            RawInputManager.windows.Clear();

            numPlayers = players.Count;

            Log(string.Format("Number of players: {0}", numPlayers));

            if (ini.IniReadValue("Misc", "ShowStatus") == "True")
            {
                try
                {
                    if (statusWinThread != null && statusWinThread.IsAlive)
                    {
                        statusWinThread.Abort();
                        Thread.Sleep(50);
                    }

                    statusWinThread = new Thread(ShowStatus);
                    statusWinThread.Start();
                }
                catch { }
            }

            if (isDebug)
            {
                Log("Nucleus Co-op version: " + Globals.Version);

                Log("########## START OF HANDLER ##########");
                string line;

                StreamReader file = new StreamReader(Path.Combine(GameManager.Instance.GetJsScriptsPath(), gen.JsFileName));
                while ((line = file.ReadLine()) != null)
                {
                    Log(line);
                }

                file.Close();

                Log("########## END OF HANDLER ##########");
            }

            if (ini.IniReadValue("Misc", "NucleusAccountPassword") != "12345" && ini.IniReadValue("Misc", "NucleusAccountPassword") != "")
            {
                nucleusUserAccountsPassword = ini.IniReadValue("Misc", "NucleusAccountPassword");
            }

            for (int i = 0; i < players.Count; i++)
            {
                if (processingExit)
                {
                    return string.Empty;
                }

                PlayerInfo player = players[i];
                player.PlayerID = i;
                plyrIndex = i;

                if (SetupPlayerInstance() != 0) return string.Empty;
            }

            if (gen.LockInputAtStart)
            {
                Thread.Sleep(5000);

                if (gen.ToggleUnfocusOnInputsLock)
                {
                    GlobalWindowMethods.ChangeForegroundWindow();
                }

                LockInput.Lock(gen.LockInputSuspendsExplorer, gen.ProtoInput.FreezeExternalInputWhenInputNotLocked, gen?.ProtoInput);

                TriggerOSD(1600, "Inputs Locked");
            }

            // Call the input lock/unlock callbacks, just in case they haven't been called with the players fully setup
            if (LockInput.IsLocked)
            {
                gen.ProtoInput.OnInputLocked?.Invoke();
            }
            else
            {
                gen.ProtoInput.OnInputUnlocked?.Invoke();
            }

            if (gen.SetTopMostAtEnd)
            {
                if (!gen.PromptBetweenInstances)
                {
                    if (!gen.NotTopMost)
                    {
                        for (int i = 0; i < players.Count; i++)
                        {
                            PlayerInfo p = players[i];
                            ProcessData data = p.ProcessData;

                            Log("Set game window to top most");
                            data.HWnd.TopMost = true;
                        }
                    }
                }
            }

            if (gen.UseNucleusEnvironment)
            {
                RegistryUtil.RestoreUserEnvironmentRegistryPath();
            }

            try
            {
                if (statusWinThread != null && statusWinThread.IsAlive)
                {
                    Thread.Sleep(5000);
                    if (statusWinThread != null && statusWinThread.IsAlive)
                    {
                        statusWinThread.Abort();
                    }
                }
            }
            catch { }

            if (!processingExit)
            {
                GamepadNavigation.EnabledRuntime = false;

                GameProfile.SaveGameProfile(profile);
            }

            gen.OnFinishedSetup?.Invoke();

            Log("All done!");

            return string.Empty;
        }

        private int KillMutexHelper(string[] killMutex, string killMutexType, bool partialMutexSearch, int killMutexDelay, string processExe, List<int> processIds, List<Process> processes = null, ProcessData pdata = null)
        {
            for (; ; )
            {
                if (exited > 0)
                {
                    return 1;
                }

                Thread.Sleep(1000);

                if (killMutex != null)
                {
                    if (killMutex.Length > 0)
                    {
                        if (killMutexDelay > 0)
                        {
                            Thread.Sleep((killMutexDelay * 1000));
                        }

                        Process[] currentProcs = Process.GetProcesses();
                        Process proc = null;
                        foreach (Process currentProc in currentProcs)
                        {
                            if (currentProc.ProcessName.ToLower() == Path.GetFileNameWithoutExtension(processExe).ToLower())
                            {
                                if (!processIds.Contains(currentProc.Id))
                                {
                                    proc = currentProc;
                                    processIds.Add(currentProc.Id);
                                    processes?.Add(currentProc);
                                }
                            }
                        }

                        if (proc == null)
                        {
                            Log("Could not find process to kill mutexes");
                            break;
                        }

                        if (StartGameUtil.MutexExists(proc, killMutexType, partialMutexSearch, killMutex))
                        {
                            // mutexes still exist, must kill
                            StartGameUtil.KillMutex(proc, killMutexType, partialMutexSearch, killMutex);
                            if (pdata != null)
                            {
                                pdata.KilledMutexes = true;
                            }
                            break;
                        }
                        else
                        {
                            // mutexes dont exist anymore
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }

                if (processingExit)
                {
                    return 1;
                }
            }

            return 0;
        }

        private void DoPostHook()
        {
            if (gen.PreventWindowDeactivation && !isPrevent)
            {
                Log("PreventWindowDeactivation detected, setting flag");
                isPrevent = true;
            }

            if (isPrevent)
            {
                if (players[plyrIndex - 1].IsKeyboardPlayer && gen.KeyboardPlayerSkipPreventWindowDeactivate)
                {
                    Log("Ignoring PreventWindowDeactivation for keyboard player");
                    gen.PreventWindowDeactivation = false;
                }
                else
                {
                    Log("Keeping PreventWindowDeactivation on");
                    gen.PreventWindowDeactivation = true;
                }
            }

            if (gen.PostHookInstances?.Length > 0)
            {
                string[] instancesToHook = gen.PostHookInstances.Split(',');
                foreach (string instanceToHook in instancesToHook)
                {
                    if (int.Parse(instanceToHook) == plyrIndex)
                    {
                        Log("Injecting hook DLL for previous instance");
                        PlayerInfo before = players[plyrIndex - 1];
                        Thread.Sleep(1000);
                        ProcessData pdata = before.ProcessData;
                        User32Interop.SetForegroundWindow(pdata.Process.NucleusGetMainWindowHandle());
                        DllsInjector.InjectDLLs(this, gen, pdata.Process, nextWindowToInject, before);
                    }
                }
            }
            else
            {
                Log("Injecting hook DLL for previous instance");
                PlayerInfo before = players[plyrIndex - 1];
                Thread.Sleep(1000);
                ProcessData pdata = before.ProcessData;
                User32Interop.SetForegroundWindow(pdata.Process.NucleusGetMainWindowHandle());
                DllsInjector.InjectDLLs(this, gen, pdata.Process, nextWindowToInject, before);
            }
        }

        private int DoSymlink()
        {
            List<string> dirExclusions = new List<string>();
            List<string> fileExclusions = new List<string>();
            List<string> fileCopies = new List<string>();

            // symlink the game folder (and not the bin folder, if we have one)
            linkFolder = Path.Combine(tempDir, $"Instance{plyrIndex}");

            Log("Commencing file operations");

            if ((plyrIndex == 0 && (gen.SymlinkGame || gen.HardlinkGame)) || gen.HardcopyGame)
            {
                for (int f = 0; f < players.Count; f++)
                {
                    string insFolder = Path.Combine(tempDir, "Instance" + f);
                    Log(string.Format("Creating instance folder {0}", insFolder.Substring(insFolder.IndexOf("content\\"))));
                    Directory.CreateDirectory(insFolder);
                }
            }

            linkBinFolder = linkFolder;

            if (!string.IsNullOrEmpty(gen.BinariesFolder))
            {
                linkBinFolder = Path.Combine(linkFolder, gen.BinariesFolder);
            }

            exePath = Path.Combine(linkBinFolder, userGame.Game.ExecutableName);
            origExePath = Path.Combine(linkBinFolder, userGame.Game.ExecutableName);

            if ((plyrIndex == 0 && (gen.SymlinkGame || gen.HardlinkGame)) || gen.HardcopyGame)
            {
                if (gen.CopyFiles != null)
                {
                    Log($"Copying {gen.CopyFiles.Length} files in Game.CopyFiles");
                    string[] filesToCopy = gen.CopyFiles;
                    for (int c = 0; c < filesToCopy.Length; c++)
                    {
                        string s = filesToCopy[c].ToLower();
                        File.Copy(Path.Combine(rootFolder, s), Path.Combine(linkFolder, s), true);
                    }
                }
            }

            origRootFolder = rootFolder;
            instanceExeFolder = linkBinFolder;

            Log("Trying to unlock all game files.");
            StartGameUtil.UnlockGameFiles(rootFolder);

            if (!string.IsNullOrEmpty(gen.WorkingFolder))
            {
                linkBinFolder = Path.Combine(linkFolder, gen.WorkingFolder);
                dirExclusions.Add(gen.WorkingFolder);
            }

            // some games have save files inside their game folder, so we need to access them inside the loop
            jsData[Folder.InstancedGameFolder.ToString()] = linkFolder;

            Thread.Sleep(1000);
            if (plyrIndex == 0 && (gen.LauncherExe?.Length > 0 && gen.LauncherExe.EndsWith("NucleusDefined")))
            {
                string exeName = null;
                if (gen.LauncherExe.Contains('|'))
                {
                    exeName = gen.LauncherExe.Split('|')[0];
                    Log($"User needs to select launcher exe ({exeName})");
                    Forms.Prompt prompt = new Forms.Prompt($"Press OK after selecting the game launcher executable ({exeName}), and you're ready to continue.", true, exeName);
                    prompt.ShowDialog();
                }
                else
                {
                    Log($"User needs to select launcher exe");
                    Forms.Prompt prompt = new Forms.Prompt("Press OK after selecting the game launcher executable, and you're ready to continue.", true, exeName);
                    prompt.ShowDialog();
                }

                Thread.Sleep(1000);
                gen.LauncherExe = ofdPath;

                if (ofdPath != null && !string.IsNullOrEmpty(ofdPath))
                {
                    Log("User selected " + ofdPath + " as the launcher exe, updating handler file");
                    string text = File.ReadAllText(Path.Combine(GameManager.Instance.GetJsScriptsPath(), gen.JsFileName));
                    //text = text.Replace("Game.LauncherExe = \"NucleusDefined\"", "Game.LauncherExe = \"" + ofdPath + "\"");
                    text = Regex.Replace(text, @"Game.LauncherExe = (.*)", $"Game.LauncherExe = \"{ofdPath}\";");
                    File.WriteAllText(Path.Combine(GameManager.Instance.GetJsScriptsPath(), gen.JsFileName), text);

                    Thread.Sleep(3000);
                }
                else
                {
                    gen.LauncherExe = null;
                    Log("User did not select a file to be the launcher");
                }
            }

            if ((plyrIndex == 0 && (gen.SymlinkGame || gen.HardlinkGame)) || gen.HardcopyGame)
            {
                if (gen.Hook.CustomDllEnabled)
                {
                    fileExclusions.Add("xinput1_3.dll");
                    fileExclusions.Add("ncoop.ini");
                }

                if (!gen.SymlinkExe)
                {
                    Log($"Game executable ({gen.ExecutableName}) will be copied and not symlinked");
                    fileCopies.Add(gen.ExecutableName.ToLower());
                    if (gen.LauncherExe?.Length > 0 && !gen.LauncherExe.Contains(':'))
                    {
                        Log($"Launcher executable ({gen.LauncherExe}) will be copied and not symlinked");
                        fileCopies.Add(gen.LauncherExe.ToLower());
                    }
                }

                // additional ignored files by the generic info
                if (gen.FileSymlinkExclusions != null)
                {
                    Log($"{gen.FileSymlinkExclusions.Length} Files in Game.FileSymlinkExclusions will not be symlinked");
                    string[] symlinkExclusions = gen.FileSymlinkExclusions;
                    for (int k = 0; k < symlinkExclusions.Length; k++)
                    {
                        string s = symlinkExclusions[k];
                        // make sure it's lower case
                        fileExclusions.Add(s.ToLower());
                    }
                }

                if (gen.FileSymlinkCopyInstead != null)
                {
                    Log($"{gen.FileSymlinkCopyInstead.Length} Files in Game.FileSymlinkCopyInstead will be copied instead of symlinked");
                    string[] fileSymlinkCopyInstead = gen.FileSymlinkCopyInstead;
                    for (int k = 0; k < fileSymlinkCopyInstead.Length; k++)
                    {
                        string s = fileSymlinkCopyInstead[k];
                        // make sure it's lower case
                        fileCopies.Add(s.ToLower());
                    }
                }

                if (gen.DirSymlinkExclusions != null)
                {
                    Log($"{gen.DirSymlinkExclusions.Length} Directories in Game.DirSymlinkExclusions will be ignored");
                    string[] symlinkExclusions = gen.DirSymlinkExclusions;
                    for (int k = 0; k < symlinkExclusions.Length; k++)
                    {
                        string s = symlinkExclusions[k];
                        // make sure it's lower case
                        dirExclusions.Add(s.ToLower());
                    }
                }

                if (gen.DirExclusions != null)
                {
                    Log($"{gen.DirExclusions.Length} Directories (and their contents) in Game.DirExclusions will be skipped");
                    string[] skipExclusions = gen.DirExclusions;
                    for (int k = 0; k < skipExclusions.Length; k++)
                    {
                        string s = skipExclusions[k];
                        // make sure it's lower case
                        dirExclusions.Add($"direxskip{s.ToLower()}");
                    }
                }

                if (gen.DirSymlinkCopyInstead != null)
                {
                    Log($"{gen.DirSymlinkCopyInstead.Length} Directories and all its contents in Game.DirSymlinkCopyInstead will be copied instead of symlinked");
                    string[] dirSymlinkCopyInstead = gen.DirSymlinkCopyInstead;

                    SearchOption toSearch = SearchOption.TopDirectoryOnly;
                    if (gen.DirSymlinkCopyInsteadIncludeSubFolders)
                    {
                        toSearch = SearchOption.AllDirectories;
                    }

                    for (int k = 0; k < dirSymlinkCopyInstead.Length; k++)
                    {
                        if (Directory.Exists(Path.Combine(origRootFolder, dirSymlinkCopyInstead[k])))
                        {
                            dirExclusions.Add(dirSymlinkCopyInstead[k].ToLower());

                            if (gen.DirSymlinkCopyInsteadIncludeSubFolders)
                            {
                                foreach (string dir in Directory.GetDirectories(Path.Combine(rootFolder, dirSymlinkCopyInstead[k]), "*", SearchOption.AllDirectories))
                                {
                                    int extraChar = 1;
                                    if (rootFolder.EndsWith("\\"))
                                    {
                                        extraChar = 0;
                                    }

                                    dirExclusions.Add(dir.Substring(rootFolder.Length + extraChar).ToLower());
                                }
                            }

                            string[] files = Directory.GetFiles(Path.Combine(rootFolder, dirSymlinkCopyInstead[k]), "*", toSearch);

                            foreach (string s in files)
                            {
                                fileCopies.Add(Path.GetFileName(s).ToLower());
                            }
                        }
                        else
                        {
                            Log($"The subfolder {dirSymlinkCopyInstead[k]} does not exist in the original game folder. Skipping.");
                        }
                    }
                }

                for (int p = 0; p < players.Count; p++)
                {
                    string path = Path.Combine(tempDir, "Instance" + p);
                    if (!Directory.Exists(path) || Directory.Exists(path) && !Directory.EnumerateFileSystemEntries(path).Any<string>())
                    {
                        symlinkNeeded = true;
                    }
                }
            }

            string[] fileExclusionsArr = fileExclusions.ToArray();
            string[] fileCopiesArr = fileCopies.ToArray();

            bool skipped = false;

            if (gen.ForceSymlink || !gen.KeepSymLinkOnExit || symlinkNeeded)
            {
                if (gen.ForceSymlink)
                {
                    Log("Forcing symlink/copy");
                }

                if (gen.HardcopyGame)
                {
                    Log(string.Format("Copying game folder {0} to {1} ", rootFolder, linkFolder));
                    // copy the directory
                    FileUtil.CopyDirectory(rootFolder, new DirectoryInfo(rootFolder), linkFolder, out int exitCode, dirExclusions.ToArray(), fileExclusionsArr, true);

                    while (exitCode != 1)
                    {
                        if (processingExit)
                        {
                            return 1;
                        }

                        Thread.Sleep(25);
                    }
                }
                else if (gen.HardlinkGame)
                {
                    if (plyrIndex == 0)
                    {
                        Log(string.Format("Hardlinking game folder and files at {0} to {1}, for each instance", rootFolder, tempDir));
                        int exitCode;
                        while (!StartGameUtil.SymlinkGame(rootFolder, linkFolder, out exitCode, dirExclusions.ToArray(), fileExclusionsArr, fileCopiesArr, true, gen.SymlinkFolders, players.Count))
                        {
                            if (processingExit)
                            {
                                return 1;
                            }

                            Thread.Sleep(25);
                        }
                    }
                }
                else
                {
                    if (plyrIndex == 0)
                    {
                        Log(string.Format("Symlinking game folder and files at {0} to {1}, for each instance", rootFolder, tempDir));
                        int exitCode;
                        while (!StartGameUtil.SymlinkGame(rootFolder, linkFolder, out exitCode, dirExclusions.ToArray(), fileExclusionsArr, fileCopiesArr, false, gen.SymlinkFolders, players.Count))
                        {
                            if (processingExit)
                            {
                                return 1;
                            }

                            Thread.Sleep(25);
                        }
                    }
                }
            }
            else
            {
                skipped = true;
                Log("Skipping linking or copying files as it is not needed");
            }

            if (gen.LauncherExe?.Length > 0)
            {
                if (gen.LauncherExe.Contains(':') && gen.LauncherExe.Contains('\\'))
                {
                    exePath = gen.LauncherExe;
                }
                else
                {
                    if (!gen.LauncherExeIgnoreFileCheck)
                    {
                        string[] launcherFiles = Directory.GetFiles(linkFolder, gen.LauncherExe, SearchOption.AllDirectories);

                        if (launcherFiles.Length < 1)
                        {
                            Log($"ERROR - Could not find {gen.LauncherExe} in instance folder, Game executable will be used instead; {exePath}");
                        }
                        else if (launcherFiles.Length == 1)
                        {
                            exePath = launcherFiles[0];
                            Log($"Found launcher exe at {exePath}. This will be used to launch the game");
                        }
                        else
                        {
                            exePath = launcherFiles[0];
                            Log($"Multiple {gen.LauncherExe}'s found in instance folder. Using {exePath} to launch the game");
                        }
                    }
                    else
                    {
                        Log($"Ignoring validation check of launcher exe. Will use filepath: {Path.Combine(linkFolder, gen.LauncherExe)}");
                        exePath = Path.Combine(linkFolder, gen.LauncherExe);
                    }
                }
            }

            if (gen.CopyFoldersTo?.Length > 0)
            {
                foreach (string sourceFolder in gen.CopyFoldersTo)
                {
                    string[] splitStr = sourceFolder.Split('|');
                    string sourcePath = Path.Combine(rootFolder, splitStr[0]);
                    string destinationPath = Path.Combine(linkFolder, splitStr[1]);
                    Log($"Copying folder {sourcePath} and all its contents to {destinationPath}");

                    Directory.CreateDirectory(destinationPath);

                    foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                    {
                        Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));
                    }

                    foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                    {
                        File.Copy(newPath, newPath.Replace(sourcePath, destinationPath), true);
                    }
                }
            }

            if (gen.SymlinkFoldersTo?.Length > 0)
            {
                foreach (string sourceFolder in gen.SymlinkFoldersTo)
                {
                    string[] splitStr = sourceFolder.Split('|');

                    string sourcePath = Path.Combine(rootFolder, splitStr[0]);
                    string destinationPath = Path.Combine(linkFolder, splitStr[1]);
                    Log($"Symlinking folder {sourcePath} to {destinationPath}");

                    if (gen.SymlinkFolders)
                    {
                        Platform.Windows.Interop.Kernel32Interop.CreateSymbolicLink(destinationPath, sourcePath, Platform.Windows.Interop.SymbolicLink.Directory);
                    }
                    else
                    {
                        Directory.CreateDirectory(destinationPath);
                    }

                    foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                    {
                        if (gen.SymlinkFolders)
                        {
                            Platform.Windows.Interop.Kernel32Interop.CreateSymbolicLink(dirPath.Replace(sourcePath, destinationPath), dirPath, Platform.Windows.Interop.SymbolicLink.Directory);
                        }
                        else
                        {
                            Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));
                        }
                    }

                    foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                        Platform.Windows.Interop.Kernel32Interop.CreateSymbolicLink(newPath.Replace(sourcePath, destinationPath), newPath, Platform.Windows.Interop.SymbolicLink.File);
                }
            }

            if (gen.HardlinkFoldersTo?.Length > 0)
            {
                foreach (string sourceFolder in gen.HardlinkFoldersTo)
                {
                    string[] splitStr = sourceFolder.Split('|');

                    string sourcePath = Path.Combine(rootFolder, splitStr[0]);
                    string destinationPath = Path.Combine(linkFolder, splitStr[1]);
                    Log($"Creating folders in {sourcePath} and hardlinking their contents to {destinationPath}");

                    Directory.CreateDirectory(destinationPath);

                    foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                    {
                        Directory.CreateDirectory(dirPath.Replace(sourcePath, destinationPath));
                    }

                    foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
                    {
                        Platform.Windows.Interop.Kernel32Interop.CreateHardLink(newPath.Replace(sourcePath, destinationPath), newPath, IntPtr.Zero);
                    }
                }
            }

            if (!skipped)
            {
                Log("File operations complete");
            }

            return 0;
        }

        private void CheckLauncher()
        {
            exePath = userGame.ExePath;
            origExePath = userGame.ExePath;

            linkBinFolder = workingFolder;
            linkFolder = rootFolder;

            instanceExeFolder = linkBinFolder;
            origRootFolder = rootFolder;

            if (!gen.ForceLauncherExeIgnoreFileCheck)
            {
                if (gen.LauncherExe?.Length > 0)
                {
                    if (gen.LauncherExe.Contains(':') && gen.LauncherExe.Contains('\\'))
                    {
                        exePath = gen.LauncherExe;
                        origExePath = gen.LauncherExe;
                    }
                    else
                    {
                        string[] launcherFiles = Directory.GetFiles(linkFolder, gen.LauncherExe, SearchOption.AllDirectories);
                        if (launcherFiles.Length < 1)
                        {
                            Log($"ERROR - Could not find {gen.LauncherExe} in instance folder, Game executable will be used instead; {exePath}");
                        }
                        else if (launcherFiles.Length == 1)
                        {
                            exePath = launcherFiles[0];
                            origExePath = launcherFiles[0];
                            Log($"Found launcher exe at {exePath}. This will be used to launch the game");
                        }
                        else
                        {
                            exePath = launcherFiles[0];
                            origExePath = launcherFiles[0];
                            Log($"Multiple {gen.LauncherExe}'s found in instance folder. Using {exePath} to launch the game");
                        }
                    }
                }
            }
        }

        public void SetLauncherExe()
        {
            Log($"Force ignoring validation check of launcher exe. Will use filepath: {Path.GetDirectoryName(userGame.ExePath).ToLower()}");
            linkFolder = Path.GetDirectoryName(userGame.ExePath).ToLower();
            exePath = Path.Combine(Path.GetDirectoryName(userGame.ExePath).ToLower(), gen.LauncherExe);

            if (gen.LauncherExe?.Length > 0)
            {
                if (gen.LauncherExe.Contains(':') && gen.LauncherExe.Contains('\\'))
                {
                    exePath = gen.LauncherExe;
                    origExePath = gen.LauncherExe;
                }
                else
                {
                    string[] launcherFiles = Directory.GetFiles(linkFolder, gen.LauncherExe, SearchOption.AllDirectories);
                    if (launcherFiles.Length < 1)
                    {
                        Log($"ERROR - Could not find {gen.LauncherExe} in instance folder, Game executable will be used instead; {exePath}");
                    }
                    else if (launcherFiles.Length == 1)
                    {
                        exePath = launcherFiles[0];
                        origExePath = launcherFiles[0];
                        Log($"Found launcher exe at {exePath}. This will be used to launch the game");
                    }
                    else
                    {
                        exePath = launcherFiles[0];
                        origExePath = launcherFiles[0];
                        Log($"Multiple {gen.LauncherExe}'s found in instance folder. Using {exePath} to launch the game");
                    }
                }
            }

            gen.LauncherExe = exePath;
        }

        private void SetupContext()
        {
            bool userConfigPathConverted = false;
            if (gen.UserProfileConfigPath?.Length > 0 && gen.UserProfileConfigPath.ToLower().StartsWith(@"documents\"))
            {
                gen.DocumentsConfigPath = gen.UserProfileConfigPath.Substring(10);
                gen.DocumentsConfigPathNoCopy = gen.UserProfileConfigPathNoCopy;
                gen.ForceDocumentsConfigCopy = gen.ForceUserProfileConfigCopy;
                userConfigPathConverted = true;
            }

            bool userSavePathConverted = false;
            if (gen.UserProfileSavePath?.Length > 0 && gen.UserProfileSavePath.ToLower().StartsWith(@"documents\"))
            {
                gen.DocumentsSavePath = gen.UserProfileSavePath.Substring(10);
                gen.DocumentsSavePathNoCopy = gen.UserProfileSavePathNoCopy;
                gen.ForceDocumentsSaveCopy = gen.ForceUserProfileSaveCopy;
                userSavePathConverted = true;
            }

            if (gen.DocumentsConfigPath?.Length > 0 || gen.DocumentsSavePath?.Length > 0)
            {
                useDocs = true;
            }

            if (!userConfigPathConverted && !gen.UserProfileConfigPathNoCopy && (gen.UserProfileConfigPath?.Length > 0 || gen.ForceUserProfileConfigCopy) && gen.UseNucleusEnvironment)
            {
                NucleusUsers.UserProfileConfigCopy(this, gen, player);
            }

            if (!userSavePathConverted && !gen.UserProfileSavePathNoCopy && (gen.UserProfileSavePath?.Length > 0 || gen.ForceUserProfileSaveCopy) && gen.UseNucleusEnvironment)
            {
                NucleusUsers.UserProfileSaveCopy(this, gen, player);
            }

            if (!gen.DocumentsConfigPathNoCopy && (gen.DocumentsConfigPath?.Length > 0 || gen.ForceDocumentsConfigCopy) && gen.UseNucleusEnvironment)
            {
                NucleusUsers.DocumentsConfigCopy(this, gen, player);
            }

            if (!gen.DocumentsSavePathNoCopy && (gen.DocumentsSavePath?.Length > 0 || gen.ForceDocumentsSaveCopy) && gen.UseNucleusEnvironment)
            {
                NucleusUsers.DocumentsSaveCopy(this, gen, player);
            }

            if (gen.DeleteFilesInConfigPath?.Length > 0)
            {
                NucleusUsers.DeleteFilesInConfigPath(this, gen, player);
            }

            if (gen.DeleteFilesInSavePath?.Length > 0)
            {
                NucleusUsers.DeleteFilesInSavePath(this, gen, player);
            }

            if (gen.ChangeExe)
            {
                ExecutableUtil.ChangeExeName(this, gen, userGame, instanceExeFolder, plyrIndex);
            }

            if (gen.RenameAndOrMoveFiles?.Length > 0)
            {
                FileUtil.RenameOrMoveFiles(this, gen, linkFolder, plyrIndex);
            }

            if (gen.DeleteFiles?.Length > 0)
            {
                FileUtil.DeleteFiles(this, gen, linkFolder, plyrIndex);
            }

            owner = player.Owner;
            bool isFullscreen = owner.Type == UserScreenType.FullScreen;

            context = gen.CreateContext(profile, player, this, hasKeyboardPlayer);
            context.PlayerID = player.PlayerID;
            context.IsFullscreen = isFullscreen;
            context.ExePath = exePath;
            context.RootInstallFolder = exeFolder;
            context.RootFolder = linkFolder;
            context.OrigRootFolder = rootFolder;
            context.UserProfileConfigPath = gen.UserProfileConfigPath;
            context.UserProfileSavePath = gen.UserProfileSavePath;

            if (gen.SymlinkFiles != null)
            {
                Log($"Symlinking {gen.SymlinkFiles.Length} files in Game.SymlinkFiles");
                context.SymlinkFiles = gen.SymlinkFiles;
            }

            if (gen.ForceLauncherExeIgnoreFileCheck)
            {
                SetLauncherExe();
            }

            if (gen.LauncherExe?.Length > 0)
            {
                context.LauncherFolder = Path.GetDirectoryName(gen.LauncherExe);
            }

            if (userConfigPathConverted || userSavePathConverted)
            {
                context.UserProfileConvertedToDocuments = true;
                if (userConfigPathConverted)
                {
                    context.UserProfileConfigPath = gen.DocumentsConfigPath;
                }

                if (userSavePathConverted)
                {
                    context.UserProfileSavePath = gen.DocumentsSavePath;
                }
            }
            else
            {
                context.UserProfileConvertedToDocuments = false;
            }

            context.DocumentsConfigPath = gen.DocumentsConfigPath;
            context.DocumentsSavePath = gen.DocumentsSavePath;

            if (plyrIndex == 0)
            {
                for (int plyri = 0; plyri < players.Count; plyri++)
                {
                    if (players[plyri].IsKeyboardPlayer)
                    {
                        context.NumKeyboards++;
                    }
                    else
                    {
                        context.NumControllers++;
                    }
                }
            }
        }

        private void DoCmdBatchLaunch()
        {
            string forceBindexe = string.Empty;

            cmdOps = gen.CMDOptions;
            cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.UseShellExecute = false;

            if (gen.UseForceBindIP)
            {
                cmd.StartInfo.WorkingDirectory = Path.GetDirectoryName(exePath);
            }

            cmd.Start();

            if (gen.UseForceBindIP)
            {
                if (gameIs64)
                {
                    forceBindexe = "ForceBindIP64.exe";
                }
                else //if (Is64Bit(exePath) == false)
                {
                    forceBindexe = "ForceBindIP.exe";
                }
            }

            if (gen.UseNucleusEnvironment)
            {
                Log("Setting up Nucleus environment");
                NucleusUsers.CreateUserEnvironment(
                    null, cmd, false,
                    (gen.DocumentsConfigPath?.Length > 0 || gen.DocumentsSavePath?.Length > 0),
                    player.Nickname, NucleusEnvironmentRoot, DocumentsRoot
                );
            }


            if (gen.CMDBatchBefore?.Length > 0 || gen.CMDBatchAfter?.Length > 0)
            {
                cmd.StandardInput.WriteLine($@"set NUCLEUS_EXE=" + Path.GetFileName(exePath));
                cmd.StandardInput.WriteLine($@"set NUCLEUS_INST_EXE_FOLDER=" + Path.GetDirectoryName(exePath));
                cmd.StandardInput.WriteLine($@"set NUCLEUS_INST_FOLDER=" + linkFolder);
                cmd.StandardInput.WriteLine($@"set NUCLEUS_FOLDER=" + nucleusFolderPath);
                cmd.StandardInput.WriteLine($@"set NUCLEUS_ORIG_EXE_FOLDER=" + exeFolder);
                cmd.StandardInput.WriteLine($@"set NUCLEUS_ORIG_FOLDER=" + exeFolder.Substring(0, (exeFolder.Length - gen.BinariesFolder.Length)));
            }

            if (gen.CMDBatchBefore?.Length > 0)
            {
                for (int x = 0; x < gen.CMDBatchBefore.Length; x++)
                {
                    if (!gen.CMDBatchBefore[x].Contains("|"))
                    {
                        Log("Running command line: " + gen.CMDBatchBefore[x]);
                        cmd.StandardInput.WriteLine(gen.CMDBatchBefore[x]);
                    }
                    else
                    {
                        string[] clineSplit = gen.CMDBatchBefore[x].Split('|');
                        if (clineSplit[0] == plyrIndex.ToString())
                        {
                            Log("Running command line: " + clineSplit[1]);
                            cmd.StandardInput.WriteLine(clineSplit[1]);
                        }
                    }
                }
            }

            if (gen.PauseCMDBatchBefore > 0)
            {
                Log(string.Format("Pausing for {0} seconds", gen.PauseCMDBatchBefore));
                Thread.Sleep(TimeSpan.FromSeconds(gen.PauseCMDBatchBefore));
            }

            if (gen.UseForceBindIP)
            {
                string iParam = string.Empty;

                if (gen.ForceBindIPDelay)
                {
                    iParam = "-i ";
                }

                string dummy = " dummy";

                if (gen.ForceBindIPNoDummy)
                {
                    dummy = string.Empty;
                }

                string cmdLine = "\"" + Path.Combine(GameManager.Instance.GetUtilsPath(), "ForceBindIP\\" + forceBindexe) + 
                    "\" " + iParam + "127.0.0." + (plyrIndex + 2) + " \"" + exePath + "\"" + dummy + startArgs;

                Log(string.Format("Launching game using ForceBindIP command line argument: {0}", cmdLine));
                cmd.StandardInput.WriteLine(cmdLine);
            }
            else
            {
                string cmdLine = "\"" + exePath + "\" " + startArgs;

                if (!gen.CMDStartArgsInside)
                {
                    cmdLine = "\"" + exePath + " " + startArgs + "\"";
                }

                if (cmdOps?.Length > 0 && plyrIndex < cmdOps.Length)
                {
                    cmdLine = cmdOps[plyrIndex] + " \"" + exePath + "\" " + startArgs;
                    if (!gen.CMDStartArgsInside)
                    {
                        cmdLine = cmdOps[plyrIndex] + " \"" + exePath + " " + startArgs + "\"";
                    }
                }

                Log(string.Format("Launching game via command prompt with the following line: {0}", cmdLine));
                cmd.StandardInput.WriteLine(cmdLine);
            }


            if (gen.CMDBatchAfter?.Length > 0)
            {
                for (int x = 0; x < gen.CMDBatchAfter.Length; x++)
                {
                    if (!gen.CMDBatchAfter[x].Contains("|"))
                    {
                        Log("Running command line: " + gen.CMDBatchAfter[x]);
                        cmd.StandardInput.WriteLine(gen.CMDBatchAfter[x]);
                    }
                    else
                    {
                        string[] clineSplit = gen.CMDBatchAfter[x].Split('|');
                        if (clineSplit[0] == plyrIndex.ToString())
                        {
                            Log("Running command line: " + clineSplit[1]);
                            cmd.StandardInput.WriteLine(clineSplit[1]);
                        }
                    }
                }
            }

            if (gen.PauseCMDBatchAfter > 0)
            {
                Log(string.Format("Pausing for {0} seconds", gen.PauseCMDBatchAfter));
                Thread.Sleep(TimeSpan.FromSeconds(gen.PauseCMDBatchAfter));
            }
        }

        private void DoDefaultDirectLaunch()
        {
            IntPtr envPtr = IntPtr.Zero;

            if (gen.UseNucleusEnvironment)
            {
                Log("Setting up Nucleus environment");
                NucleusUsers.CreateUserEnvironment(
                    Environment.GetEnvironmentVariables(), null, false,
                    (gen.DocumentsConfigPath?.Length > 0 || gen.DocumentsSavePath?.Length > 0),
                    player.Nickname, NucleusEnvironmentRoot, DocumentsRoot
                );
            }

            ProcessUtil.STARTUPINFO startup = new ProcessUtil.STARTUPINFO();
            startup.cb = Marshal.SizeOf(startup);

            bool success = ProcessUtil.CreateProcess(null, exePath + " " + startArgs, IntPtr.Zero, IntPtr.Zero, false, (uint)ProcessUtil.ProcessCreationFlags.CREATE_UNICODE_ENVIRONMENT, envPtr, Path.GetDirectoryName(exePath), ref startup, out ProcessUtil.PROCESS_INFORMATION processInformation);
            Log(string.Format("Launching game directly at {0} with args {1}", exePath, startArgs));

            if (!success)
            {
                int error = Marshal.GetLastWin32Error();
                Log(string.Format("ERROR {0} - CreateProcess failed - startGamePath: {1}, startArgs: {2}, dirpath: {3}", error, exePath, startArgs, Path.GetDirectoryName(exePath)));
            }

            try
            {
                proc = Process.GetProcessById(processInformation.dwProcessId);
            }
            catch
            {
                proc = null;
            }
        }

        private void DoStartupInjectLaunch()
        {
            if (gen.RenameNotKillMutex && context.KillMutex?.Length > 0)
            {
                for (int m = 0; m < gen.KillMutex.Length; m++)
                {
                    mutexNames += gen.KillMutex[m];

                    if (m != gen.KillMutex.Length - 1)
                    {
                        mutexNames += "|==|";
                    }
                }
            }

            Log(string.Format("Launching game located at {0} through StartGameUtil", exePath));

            uint sguOutPID = StartGameUtil.StartGame(this);

            try
            {
                proc = Process.GetProcessById((int)sguOutPID);
            }
            catch (Exception)
            {
                proc = null;
                Log("Process By ID failed, setting process to null and continuing, will try and catch it later");
            }
        }

        private void DoLogonLaunch()
        {
            IntPtr envPtr = IntPtr.Zero;
            var sb = new StringBuilder();
            IDictionary envVars = Environment.GetEnvironmentVariables();

            if (gen.UseNucleusEnvironment)
            {
                Log("Setting up Nucleus environment");
                NucleusUsers.CreateUserEnvironment(
                    envVars, cmd, false,
                    (gen.DocumentsConfigPath?.Length > 0 || gen.DocumentsSavePath?.Length > 0),
                    player.Nickname, NucleusEnvironmentRoot, DocumentsRoot
                );

            }
            else if (gen.UseCurrentUserEnvironment)
            {
                Log("Setting up Nucleus environment");
                NucleusUsers.CreateUserEnvironment(
                    envVars, cmd, true,
                    (gen.DocumentsConfigPath?.Length > 0 || gen.DocumentsSavePath?.Length > 0),
                    player.Nickname, NucleusEnvironmentRoot, DocumentsRoot
                );
            }

            ProcessUtil.STARTUPINFO startup = new ProcessUtil.STARTUPINFO();
            startup.cb = Marshal.SizeOf(startup);

            bool success = ProcessUtil.CreateProcessWithLogonW(
                $"nucleusplayer{plyrIndex + 1}", Environment.UserDomainName, nucleusUserAccountsPassword,
                ProcessUtil.LogonFlags.LOGON_WITH_PROFILE, null, exePath + " " + startArgs,
                ProcessUtil.ProcessCreationFlags.CREATE_UNICODE_ENVIRONMENT, (uint)envPtr,
                Path.GetDirectoryName(exePath), ref startup, out ProcessUtil.PROCESS_INFORMATION processInformation
            );
            Log(string.Format("Launching game directly at {0} with args {1} as user: nucleusplayer{2}", exePath, startArgs, (plyrIndex + 1)));

            if (!success)
            {
                int error = Marshal.GetLastWin32Error();
                Log(string.Format("ERROR {0} - CreateProcessWithLogonW failed", error));
            }

            try
            {
                proc = Process.GetProcessById(processInformation.dwProcessId);
            }
            catch
            {
                proc = null;
            }
        }

        private void DoLogonLaunchAlt()
        {
            //create users OR reset their password if they exists.
            Thread.Sleep(1000);

            cmd = new Process();
            cmd.StartInfo.UseShellExecute = false;
            cmd.StartInfo.Verb = "runas";
            string cmdLine;
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.WorkingDirectory = Path.GetDirectoryName(exePath);
            cmdLine = $"elevate /C runas /savecred /env /user:nucleusplayer{plyrIndex + 1}" + " \"" + exePath + " " + startArgs + "\"";
            cmd.StartInfo.Arguments = cmdLine;

            if (gen.UseNucleusEnvironment)
            {
                Log("Setting up Nucleus environment");
                NucleusUsers.CreateUserEnvironment(
                    (IDictionary)cmd.StartInfo.EnvironmentVariables, cmd, false,
                    (gen.DocumentsConfigPath?.Length > 0 || gen.DocumentsSavePath?.Length > 0), 
                    player.Nickname, NucleusEnvironmentRoot, DocumentsRoot
                );

            }
            else if (gen.UseCurrentUserEnvironment)
            {
                Log("Setting up Nucleus environment");
                NucleusUsers.CreateUserEnvironment(
                    (IDictionary)cmd.StartInfo.EnvironmentVariables, cmd, true,
                    (gen.DocumentsConfigPath?.Length > 0 || gen.DocumentsSavePath?.Length > 0),
                    player.Nickname, NucleusEnvironmentRoot, DocumentsRoot
                );
            }

            Log(string.Format("Launching game as user: nucleusplayer{0}, using command: {1}", (plyrIndex + 1), cmdLine));

            cmd.Start();
            cmd.WaitForExit();

            proc = null;
        }

        private void DoDirectLaunch()
        {
            if (gen.ExecutableToLaunch?.Length > 0)
            {
                Log("Different executable provided to launch");
                exePath = Path.Combine(linkFolder, gen.ExecutableToLaunch);
            }
                        
            if (gen.ProtoInput.InjectStartup || 
                ((gen.HookInit || (gen.RenameNotKillMutex && context.KillMutex?.Length > 0) ||
                gen.SetWindowHookStart || gen.BlockRawInput || gen.CreateSingleDeviceFile) &&
                !gen.CMDLaunch && !gen.UseForceBindIP && !gen.LaunchAsDifferentUsers &&
                !gen.LaunchAsDifferentUsersAlt)) /*|| (gen.CMDLaunch && i==0))*/
            {
                DoStartupInjectLaunch();
            }
            else
            {
                if (gen.LaunchAsDifferentUsersAlt)
                {
                    DoLogonLaunchAlt();
                }
                else if (gen.LaunchAsDifferentUsers)
                {
                    DoLogonLaunch();
                }
                else if (gen.CMDLaunch /*&& i >= 1*/ || (gen.UseForceBindIP && plyrIndex > 0))
                {
                    DoCmdBatchLaunch();
                }
                else
                {
                    DoDefaultDirectLaunch();
                }
            }
        }

        private int DoLauncherLaunch()
        {
            Log("Launching exe " + origExePath);
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = origExePath;
            proc = Process.Start(startInfo);

            int counter = 0;
            bool found = false;
            if (gen.GameName == "Ghost Recon Wildlands" && plyrIndex > 0)
            {
                Log("Launching exe again " + origExePath);
                startInfo = new ProcessStartInfo();
                startInfo.FileName = origExePath;
                proc = Process.Start(startInfo);

                Log("Waiting to find process by window title");

                while (!found)
                {
                    Process[] processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(gen.ExecutableName));
                    foreach (var process in processes)
                    {
                        if ((int)process.NucleusGetMainWindowHandle() > 0 && process.MainWindowTitle == gen.Hook.ForceFocusWindowName && (attachedIds.Count == 0 || (attachedIds.Count > 0 && !attachedIds.Contains(process.Id))))
                        {
                            Log("Process found, " + process.ProcessName + " pid (" + process.Id + ") after " + counter + " seconds");
                            proc = process;
                            attached.Add(process);
                            attachedIds.Add(process.Id);
                            player.ProcessID = process.Id;
                            found = true;
                            Log(string.Format("Process details; Name: {0}, ID: {1}, MainWindowtitle: {2}, NucleusGetMainWindowHandle(): {3}", process.ProcessName, process.Id, process.MainWindowTitle, process.NucleusGetMainWindowHandle()));
                            break;
                        }
                    }
                    counter++;
                    Thread.Sleep(1000);

                    if (processingExit)
                    {
                        return 1;
                    }
                }

                Thread.Sleep(1000);
            }
            else
            {
                Log("Waiting to find process by window title");

                while (!found)
                {
                    Process[] processes = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(gen.ExecutableName));
                    foreach (var process in processes)
                    {
                        if ((int)process.NucleusGetMainWindowHandle() > 0 && process.MainWindowTitle == gen.Hook.ForceFocusWindowName && (attachedIds.Count == 0 || (attachedIds.Count > 0 && !attachedIds.Contains(process.Id))))
                        {
                            Log("Process found, " + process.ProcessName + " pid (" + process.Id + ") after " + counter + " seconds");
                            proc = process;
                            attached.Add(process);
                            attachedIds.Add(process.Id);
                            player.ProcessID = process.Id;
                            found = true;
                            Log(string.Format("Process details; Name: {0}, ID: {1}, MainWindowtitle: {2}, NucleusGetMainWindowHandle(): {3}", process.ProcessName, process.Id, process.MainWindowTitle, process.NucleusGetMainWindowHandle()));
                            break;
                        }
                    }

                    counter++;
                    Thread.Sleep(1000);

                    if (processingExit)
                    {
                        return 1;
                    }
                }
            }

            Thread.Sleep(10000);

            return 0;
        }

        private void SearchForGameProcessByName()
        {
            Log("Searching for game process");

            //bool foundUnique = false;
            for (int times = 0; times < 200; times++)
            {
                Thread.Sleep(50);

                string proceName = Path.GetFileNameWithoutExtension(gen.ExecutableName).ToLower();
                if (gen.ChangeExe)
                {
                    proceName = Path.GetFileNameWithoutExtension(userGame.Game.ExecutableName) + " - Player " + (plyrIndex + 1) + ".exe";
                }

                Process[] procs = Process.GetProcesses();
                for (int j = 0; j < procs.Length; j++)
                {
                    Process p = procs[j];

                    string lowerP = p.ProcessName.ToLower();

                    if (lowerP == proceName)
                    {
                        if (!attachedIds.Contains(p.Id))
                        {
                            if (p.ProcessName == "javaw" || p.ProcessName == "GRW" || p.ProcessName == "steamclient_loader")
                            {
                                if ((int)p.NucleusGetMainWindowHandle() == 0)
                                {
                                    continue;
                                }
                            }

                            Log(string.Format("Found process {0} (pid {1})", p.ProcessName, p.Id));

                            attached.Add(p);
                            attachedIds.Add(p.Id);
                            player.ProcessID = p.Id;
                            if (player.IsKeyboardPlayer && !player.IsRawKeyboard)
                            {
                                keyboardProcId = p.Id;
                            }

                            proc = p;
                            prevProcId = p.Id;

                            break;
                        }
                    }
                }

                if (proc != null)
                {
                    break;
                }
            }
        }

        private void SearchForGameProcessByWindowTitle()
        {
            Log("Process is no longer running. Attempting to find process by window title");
            Process[] processes = Process.GetProcesses();
            foreach (var process in processes)
            {
                if (process.MainWindowTitle == gen.Hook.ForceFocusWindowName && !attachedIds.Contains(process.Id))
                {
                    Log("Process found, " + process.ProcessName + " pid (" + process.Id + ")");
                    proc = process;
                    attached.Add(proc);
                    attachedIds.Add(proc.Id);
                    player.ProcessID = proc.Id;
                    if (player.IsKeyboardPlayer && !player.IsRawKeyboard)
                    {
                        keyboardProcId = proc.Id;
                    }
                    break;
                }
            }
        }

        private void SetupProcessData()
        {
            Log("Setting process data to process " + proc.ProcessName + " (pid " + proc.Id + ")");
            data = new ProcessData(proc);
            prevProcessData = data;

            playerBoundsWidth = playerBounds.Width;
            playerBoundsHeight = playerBounds.Height;

            if (context.Hook.WindowX > 0 && context.Hook.WindowY > 0)
            {
                data.Position = new Point(context.Hook.WindowX, context.Hook.WindowY);
                prevWindowX = context.Hook.WindowX;
                prevWindowY = context.Hook.WindowY;
            }
            else
            {
                data.Position = new Point(playerBounds.X, playerBounds.Y);
                prevWindowX = playerBounds.X;
                prevWindowY = playerBounds.Y;
            }

            if (context.Hook.ResWidth > 0 && context.Hook.ResHeight > 0)
            {
                data.Size = new Size(context.Hook.ResWidth, context.Hook.ResHeight);
                prevWindowWidth = context.Hook.ResWidth;
                prevWindowHeight = context.Hook.ResHeight;
            }
            else
            {
                data.Size = new Size(playerBounds.Width, playerBounds.Height);
                prevWindowWidth = playerBounds.Width;
                prevWindowHeight = playerBounds.Height;
            }

            data.KilledMutexes = context.KillMutex?.Length == 0;
            player.ProcessData = data;
        }

        private void SetupProcessorPriority()
        {
            ProfilePlayer profilePlayer = null;

            if (GameProfile.ProfilePlayersList.Count > 0)
            {
                profilePlayer = GameProfile.ProfilePlayersList[plyrIndex];
            }

            if (profilePlayer?.PriorityClass != "Normal" && profilePlayer?.PriorityClass != null)
            {
                player.PriorityClass = profilePlayer.PriorityClass;
                gen.ProcessorPriorityClass = profilePlayer.PriorityClass;
                ProcessUtil.SetProcessorPriorityClass(gen, proc);
            }
            else if (gen.ProcessorPriorityClass?.Length > 0)
            {
                ProcessUtil.SetProcessorPriorityClass(gen, proc);
            }

            if (profilePlayer?.IdealProcessor != "*" && profilePlayer?.IdealProcessor != null)
            {
                gen.IdealProcessor = int.Parse(profilePlayer.IdealProcessor) - 1;
                ProcessUtil.SetIdealProcessor(gen, proc);
            }
            else if (gen.IdealProcessor > 0)
            {
                ProcessUtil.SetIdealProcessor(gen, proc);
            }

            if (profilePlayer?.Affinity != "" && profilePlayer?.Affinity != null)
            {
                player.Affinity = profilePlayer.Affinity;
                gen.UseProcessor = profilePlayer.Affinity;
                ProcessUtil.SetProcessorProcessorAffinity(gen, proc);
            }
            else if ((gen.UseProcessor != null ? (gen.UseProcessor.Length > 0 ? 1 : 0) : 0) != 0)
            {
                ProcessUtil.SetProcessorProcessorAffinity(gen, proc);
            }
            else
            {
                ProcessUtil.SetProcessorAffinityPerInstance(gen, proc, plyrIndex);
            }
        }

        private void AddIDToWindowTitle()
        {
            if ((int)proc.NucleusGetMainWindowHandle() == 0)
            {
                for (int times = 0; times < 200; times++)
                {
                    Thread.Sleep(50);
                    if ((int)proc.NucleusGetMainWindowHandle() > 0)
                    {
                        break;
                    }
                }
            }
            if ((int)proc.NucleusGetMainWindowHandle() > 0)
            {
                string windowTitle = proc.MainWindowTitle + "(" + plyrIndex + ")";
                if (!string.IsNullOrEmpty(gen.FlawlessWidescreen))
                {
                    windowTitle = "Nucleus Instance " + (plyrIndex + 1) + "(" + gen.Hook.ForceFocusWindowName + ")";
                }
                Log(string.Format("Setting window text to {0}", windowTitle));
                GlobalWindowMethods.SetWindowText(proc, windowTitle);
            }
            else
            {
                Log(string.Format("ERROR - IdInWindowTitle could not find main window handle for {0} (pid {1})", proc.ProcessName, proc.Id));
                MessageBox.Show(string.Format("IdInWindowTitle: Could not find main window handle for {0} (pid:{1})", proc.ProcessName, proc.Id), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DoProtoInputRuntimeInjection()
        {
            Log("Injecting ProtoInput at runtime into pid " + (uint)proc.Id);

            ProtoInputLauncher.InjectRuntime(
                gen.ProtoInput.InjectRuntime_EasyHookMethod,
                gen.ProtoInput.InjectRuntime_EasyHookStealthMethod,
                gen.ProtoInput.InjectRuntime_RemoteLoadMethod,
                (uint)proc.Id,
                nucleusFolderPath,
                plyrIndex + 1,
                gen,
                player,
                (player.IsRawMouse ? (int)player.RawMouseDeviceHandle : -1),
                (player.IsRawKeyboard ? (int)player.RawKeyboardDeviceHandle : -1),
                (gen.ProtoInput.MultipleProtoControllers ? (player.ProtoController1) : ((player.IsRawMouse || player.IsRawKeyboard) ? 0 : player.GamepadId + 1)),
                (gen.ProtoInput.MultipleProtoControllers ? player.ProtoController2 : 0),
                (gen.ProtoInput.MultipleProtoControllers ? player.ProtoController3 : 0),
                (gen.ProtoInput.MultipleProtoControllers ? player.ProtoController4 : 0)
            );
        }

        private void PromptLaunchNextInstance()
        {
            if (gen.PauseBetweenStarts > 0)
            {
                Log(string.Format("Pausing for {0} seconds", gen.PauseBetweenStarts));
                Thread.Sleep(TimeSpan.FromSeconds(gen.PauseBetweenStarts));
            }

            Log(string.Format("Prompted user for Instance {0}", (plyrIndex + 2)));

            Prompt prompt = new Prompt("Press OK when ready to launch instance " + (plyrIndex + 2) + ".");
            prompt.ShowDialog();
        }

        private void PromptInstallPostHooks()
        {
            if (gen.PauseBetweenStarts > 0)
            {
                Log(string.Format("Pausing for {0} seconds", gen.PauseBetweenStarts));
                Thread.Sleep(TimeSpan.FromSeconds(gen.PauseBetweenStarts));
            }

            Log("Prompted user to install post hooks");

            Prompt prompt = new Prompt("Press OK when ready to install hooks and/or start sending fake messages.");
            prompt.ShowDialog();

            foreach (Process aproc in attached)
            {
                IntPtr topMostFlag = new IntPtr(-1);

                if (gen.NotTopMost)
                {
                    topMostFlag = new IntPtr(-2);
                }

                User32Interop.SetWindowPos(aproc.NucleusGetMainWindowHandle(), topMostFlag, 0, 0, 0, 0, (uint)(PositioningFlags.SWP_NOMOVE | PositioningFlags.SWP_NOSIZE | PositioningFlags.SWP_SHOWWINDOW));
            }
        }

        private void FindProcessByWindowTitle()
        {
            Log("Process is no longer running. Attempting to find process by window title");

            Process[] processes = Process.GetProcesses();

            foreach (var process in processes)
            {
                if (process.MainWindowTitle == gen.Hook.ForceFocusWindowName && !attachedIds.Contains(process.Id))
                {
                    Log("Process found, " + process.ProcessName + " pid (" + process.Id + ")");
                    proc = process;
                    attached.Add(proc);
                    attachedIds.Add(proc.Id);
                    player.ProcessID = proc.Id;
                    if (player.IsKeyboardPlayer && !player.IsRawKeyboard)
                    {
                        keyboardProcId = proc.Id;
                    }

                    Log("Recreating player process data");
                    data = new ProcessData(proc);
                    prevProcessData = data;

                    playerBoundsWidth = playerBounds.Width;
                    playerBoundsHeight = playerBounds.Height;

                    if (context.Hook.WindowX > 0 && context.Hook.WindowY > 0)
                    {
                        data.Position = new Point(context.Hook.WindowX, context.Hook.WindowY);
                        prevWindowX = context.Hook.WindowX;
                        prevWindowY = context.Hook.WindowY;
                    }
                    else
                    {
                        data.Position = new Point(playerBounds.X, playerBounds.Y);
                        prevWindowX = playerBounds.X;
                        prevWindowY = playerBounds.Y;
                    }

                    if (context.Hook.ResWidth > 0 && context.Hook.ResHeight > 0)
                    {
                        data.Size = new Size(context.Hook.ResWidth, context.Hook.ResHeight);
                        prevWindowWidth = context.Hook.ResWidth;
                        prevWindowHeight = context.Hook.ResHeight;
                    }
                    else
                    {
                        data.Size = new Size(playerBounds.Width, playerBounds.Height);
                        prevWindowWidth = playerBounds.Width;
                        prevWindowHeight = playerBounds.Height;
                    }

                    data.KilledMutexes = context.KillMutex?.Length == 0;
                    player.ProcessData = data;

                    Log(string.Format("Process details; Name: {0}, ID: {1}, MainWindowtitle: {2}, NucleusGetMainWindowHandle(): {3}", proc.ProcessName, proc.Id, proc.MainWindowTitle, proc.NucleusGetMainWindowHandle()));
                }
            }
        }

        private int KillLastMutex()
        {
            for (; ; )
            {
                if (gen.KillMutex != null)
                {
                    if (gen.KillMutex.Length > 0 && !players[plyrIndex].ProcessData.KilledMutexes)
                    {
                        if (gen.KillMutexDelay > 0)
                        {
                            Thread.Sleep((gen.KillMutexDelay * 1000));
                        }

                        if (StartGameUtil.MutexExists(players[plyrIndex].ProcessData.Process, gen.KillMutexType, gen.PartialMutexSearch, gen.KillMutex))
                        {
                            // mutexes still exist, must kill
                            StartGameUtil.KillMutex(players[plyrIndex].ProcessData.Process, gen.KillMutexType, gen.PartialMutexSearch, gen.KillMutex);
                            players[plyrIndex].ProcessData.KilledMutexes = true;
                            break;
                        }
                        else
                        {
                            // mutexes dont exist anymore
                            break;
                        }
                    }
                }
                else
                {
                    break;
                }

                if (processingExit)
                {
                    return 1;
                }
            }

            return 0;
        }

        private void RenameWindowTitle()
        {
            foreach (Process aproc in attached)
            {
                if ((int)aproc.NucleusGetMainWindowHandle() == 0)
                {
                    for (int times = 0; times < 200; times++)
                    {
                        Thread.Sleep(50);
                        if ((int)aproc.NucleusGetMainWindowHandle() > 0)
                        {
                            break;
                        }
                    }
                }
                if ((int)aproc.NucleusGetMainWindowHandle() > 0)
                {
                    Log(string.Format("Renaming window title {0} to {1} for pid {2}", aproc.NucleusGetMainWindowHandle(), gen.Hook.ForceFocusWindowName, aproc.Id));
                    GlobalWindowMethods.SetWindowText(aproc, gen.Hook.ForceFocusWindowName);
                }
                else
                {
                    Log(string.Format("ERROR - ChangeWindowTitle could not find main window handle for {0} (pid:{1})", aproc.ProcessName, aproc.Id));
                    MessageBox.Show(string.Format("ChangeWindowTitle: Could not find main window handle for {0} (pid:{1})", aproc.ProcessName, aproc.Id), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ResetWindowTitle()
        {
            for (int fw = 0; fw < players.Count; fw++)
            {
                Process fwProc = Process.GetProcessById(players[fw].ProcessData.Process.Id);
                string windowTitle = "Nucleus Instance " + (fw + 1) + "(" + gen.Hook.ForceFocusWindowName + ")";

                if (fwProc.MainWindowTitle != windowTitle)
                {
                    Log(string.Format("Resetting window text for pid {0} to {0}", fwProc.Id, windowTitle));
                    GlobalWindowMethods.SetWindowText(fwProc, windowTitle);
                }
            }
        }

        private void InjectLastInstance()
        {
            if (gen.PreventWindowDeactivation && !isPrevent)
            {
                Log("PreventWindowDeactivation detected, setting flag");
                isPrevent = true;
            }

            if (isPrevent)
            {
                if (players[plyrIndex].IsKeyboardPlayer && gen.KeyboardPlayerSkipPreventWindowDeactivate)
                {
                    Log("Ignoring PreventWindowDeactivation for keyboard player");
                    gen.PreventWindowDeactivation = false;
                }
                else
                {
                    Log("Keeping PreventWindowDeactivation on");
                    gen.PreventWindowDeactivation = true;
                }
            }

            if (gen.PostHookInstances?.Length > 0)
            {
                string[] instancesToHook = gen.PostHookInstances.Split(',');
                foreach (string instanceToHook in instancesToHook)
                {
                    if (int.Parse(instanceToHook) == (plyrIndex + 1))
                    {
                        Log("Injecting hook DLL for last instance");
                        User32Interop.SetForegroundWindow(data.Process.NucleusGetMainWindowHandle());
                        DllsInjector.InjectDLLs(this, gen, data.Process, nextWindowToInject, players[plyrIndex]);
                    }
                }
            }
            else
            {
                Log("Injecting hook DLL for last instance");
                User32Interop.SetForegroundWindow(data.Process.NucleusGetMainWindowHandle());
                DllsInjector.InjectDLLs(this, gen, data.Process, nextWindowToInject, players[plyrIndex]);
            }
        }

        private void CheckWindowBorder()
        {
            foreach (PlayerInfo plyr in players)
            {
                Thread.Sleep(1000);

                Process plyrProc = plyr.ProcessData.Process;

                if (!gen.DontRemoveBorders)
                {
                    const int flip = 0x00C00000 | 0x00080000 | 0x00040000; //WS_BORDER | WS_SYSMENU

                    var x = (int)User32Interop.GetWindowLong(plyrProc.NucleusGetMainWindowHandle(), User32_WS.GWL_STYLE);
                    if ((x & flip) > 0)//has a border
                    {
                        Log("Process id " + plyrProc.Id + ", still has or regained a border, trying to remove it");
                        x &= (~flip);
                        GlobalWindowMethods.ResetWindows(this, gen, plyr.ProcessData, plyr.ProcessData.Position.X, plyr.ProcessData.Position.Y, plyr.ProcessData.Size.Width, plyr.ProcessData.Size.Height, plyr.PlayerID + 1);
                    }
                }

                if (gen.WindowStyleEndChanges?.Length > 0 || gen.ExtWindowStyleEndChanges?.Length > 0)
                {
                    Thread.Sleep(1000);
                    GlobalWindowMethods.WindowStyleChanges(this, gen, plyr.ProcessData, plyrIndex);
                }

                if (gen.EnableWindows)
                {
                    GlobalWindowMethods.EnableWindow(plyr.ProcessData.HWnd.NativePtr, true);
                }
            }
        }

        private int FindDIEmWndWindow()
        {
            //Window setup
            foreach (var window in RawInputManager.windows)
            {
                var hWnd = window.hWnd;

                //Borderlands 2 (and some other games) requires WM_INPUT to be sent to a window named DIEmWin, not the main hWnd.
                foreach (ProcessThread thread in Process.GetProcessById(window.pid).Threads)
                {

                    int WindowEnum(IntPtr _hWnd, int lParam)
                    {
                        var threadId = WinApi.GetWindowThreadProcessId(_hWnd, out int pid);
                        if (threadId == lParam)
                        {
                            string windowText = WinApi.GetWindowText(_hWnd);

                            if (windowText != null && windowText.ToLower().Contains("DIEmWin".ToLower()))
                            {
                                window.DIEmWin_hWnd = _hWnd;
                            }
                        }

                        return 1;
                    }

                    WinApi.EnumWindows(WindowEnum, thread.Id);
                }
            }

            return 0;
        }

        private int SetupPlayerInstance()
        {
            Log($"********** Setting up player {plyrIndex + 1} **********");
                       

            if (!GameProfile.UseNicknames)
            {
                player.Nickname = $"Player{plyrIndex + 1}";
            }

            ProcessData procData = player.ProcessData;
            bool hasSetted = procData != null && procData.Setted;

            if (gen.PauseBeforeMutexKilling > 0)
            {
                Log(string.Format("Pausing for {0} seconds", gen.PauseBeforeMutexKilling));
                Thread.Sleep(TimeSpan.FromSeconds(gen.PauseBeforeMutexKilling));
            }

            if (plyrIndex > 0 && (gen.KillMutexLauncher?.Length > 0))
            {
                if (KillMutexHelper(gen.KillMutexLauncher, gen.KillMutexTypeLauncher, gen.PartialMutexSearchLauncher, gen.KillMutexDelayLauncher, gen.LauncherExe, attachedIdsLaunchers, attachedLaunchers) != 0)
                {
                    return 1;
                }
            }

            if (!gen.KillMutexAtEnd)
            {
                if (!gen.RenameNotKillMutex && plyrIndex > 0 && (gen.KillMutex?.Length > 0 || !hasSetted))
                {
                    PlayerInfo before = players[plyrIndex - 1];
                    ProcessData pdata = before.ProcessData;

                    if (KillMutexHelper(gen.KillMutex, gen.KillMutexType, gen.PartialMutexSearch, gen.KillMutexDelay, userGame.ExePath, null, null, pdata) != 0)
                    {
                        return 1;
                    }
                }
            }

            if (plyrIndex > 0 && (gen.KillMutexProcess?.Length > 0))
            {
                if (KillMutexHelper(gen.KillMutexProcess, gen.KillMutexTypeProcess, gen.PartialMutexSearchProcess, gen.KillMutexDelayProcess, gen.MutexProcessExe, mutexProcs) != 0)
                {
                    return 1;
                }
            }

            if (plyrIndex > 0 && !gen.ProcessChangesAtEnd && (gen.HookFocus || gen.SetWindowHook || gen.HideCursor || gen.PreventWindowDeactivation || gen.SupportsMultipleKeyboardsAndMice))
            {
                DoPostHook();
            }

            playerBounds = player.MonitorBounds;
            owner = player.Owner;

            int width = playerBounds.Width;
            int height = playerBounds.Height;
            Log($"Player monitor's resolution: {owner.display.Width} x {owner.display.Height}");

            origRootFolder = "";

            if (gen.SymlinkGame || gen.HardcopyGame || gen.HardlinkGame)
            {
                if (DoSymlink() != 0)
                {
                    return 1;
                }
            }
            else
            {
                CheckLauncher();
            }

            if (plyrIndex == 0)
            {
                BackupFiles.StartFilesRestoration(gen);

                Thread.Sleep(200);

                BackupFiles.StartFoldersRestoration(gen);
            }

            if (processingExit)
            {
                return 1;
            }

            if (gen.ChangeIPPerInstanceAlt)
            {
                Network.ChangeIPPerInstanceAltCreateAdapter(this, player);
            }

            if (gen.LaunchAsDifferentUsers || gen.LaunchAsDifferentUsersAlt)
            {
                WindowsUsersUtil.CreateWindowsUser(this, gen, players, player, plyrIndex);
            }

            SetupContext();            
                        
            if (gen.CustomUserGeneralPrompts?.Length > 0)
            {
                CustomPromptRuntime.CustomUserGeneralPrompts(this, gen, context, player);
            }

            if (gen.CustomUserPlayerPrompts?.Length > 0)
            {
                CustomPromptRuntime.CustomUserPlayerPrompts(this, gen, context, player);
            }

            if (gen.CustomUserInstancePrompts?.Length > 0)
            {
                CustomPromptRuntime.CustomUserInstancePrompts(this, gen, context, player);
            }

            bool setupDll = true;

            if (!gen.SymlinkGame && !gen.HardlinkGame && !gen.HardcopyGame && plyrIndex > 0)
            {
                setupDll = false;
            }

            if (gen.UseSteamless)
            {
                Log($"Apply Steamless patch for {gen.ExecutableName} Timing: {gen.SteamlessTiming}ms");

                SteamFunctions.SteamlessProc(linkBinFolder, gen.ExecutableName, gen.SteamlessArgs, gen.SteamlessTiming);
                Thread.Sleep(gen.SteamlessTiming + 2000);
            }

            if (processingExit)
            {
                return 1;
            }

            if (gen.GamePlayBeforeGameSetup && !gen.GamePlayAfterLaunch)
            {
                gen.PrePlay(context, this, player);
            }

            if (gen.HexEditExeAddress?.Length > 0)
            {
                HexEdit.HexEditExeAddress(this, gen, context, exePath, plyrIndex);
            }

            if (gen.HexEditFileAddress?.Length > 0)
            {
                HexEdit.HexEditFileAddress(this, gen, plyrIndex, linkFolder);
            }

            if (gen.HexEditAllExes?.Length > 0)
            {
                HexEdit.HexEditAllExes(gen, context, exePath, plyrIndex);
            }

            if (gen.HexEditExe?.Length > 0)
            {
                HexEdit.HexEditExe(gen, context, exePath, plyrIndex);
            }

            if (gen.HexEditAllFiles?.Length > 0)
            {
                HexEdit.HexEditAllFiles(gen, context, plyrIndex, linkFolder);
            }

            if (gen.HexEditFile?.Length > 0)
            {
                HexEdit.HexEditFile(gen, context, plyrIndex, linkFolder);
            }

            if (gen.UseSteamStubDRMPatcher)
            {
                SteamFunctions.UseSteamStubDRMPatcher(this, gen, garch, setupDll);
            }

            if (gen.UseEACBypass)
            {
                EACBypass.UseEACBypass(this, gen, linkFolder, setupDll);
            }

            if (gen.UseGoldberg)
            {
                SteamFunctions.UseGoldberg(this, gen, context, rootFolder, nucleusRootFolder, linkFolder, plyrIndex, player, players, setupDll, exePath);
            }

            if (gen.UseNemirtingasEpicEmu)
            {
                context.StartArguments += "";
                NemirtingasEpicEmu.UseNemirtingasEpicEmu(this, gen, rootFolder, linkFolder, plyrIndex, player, setupDll);
            }

            if (gen.UseNemirtingasGalaxyEmu)
            {
                NemirtingasGalaxyEmu.UseNemirtingasGalaxyEmu(this, gen, rootFolder, linkFolder, plyrIndex, player, setupDll);
            }

            if (gen.CreateSteamAppIdByExe)
            {
                SteamFunctions.CreateSteamAppIdByExe(this, gen, setupDll);
            }

            if (gen.XInputPlusDll?.Length > 0 && !gen.ProcessChangesAtEnd)
            {
                XInputPlusDll.SetupXInputPlusDll(this, gen, garch, player, context, plyrIndex, setupDll);
            }

            if (gen.UseDevReorder && !gen.ProcessChangesAtEnd)
            {
                DevReorder.UseDevReorder(this, gen, garch, player, players, plyrIndex, setupDll);
            }

            if (gen.UseDInputBlocker)
            {
                DInputBlocker.UseDInputBlocker(this, gen, garch, setupDll);
            }

            if (gen.UseX360ce && !gen.ProcessChangesAtEnd)
            {
                X360ce.UseX360ce(this, gen, plyrIndex, players, player, context, setupDll);
            }

            if (gen.UseDirectX9Wrapper)
            {
                DirectX9Wrapper.UseDirectX9Wrapper(this, gen, setupDll);
            }

            if (gen.CopyCustomUtils?.Length > 0)
            {
                FileUtil.CopyCustomUtils(this, gen, plyrIndex, linkFolder, setupDll);
            }

            if (!string.IsNullOrEmpty(gen.FlawlessWidescreen))
            {
                FlawlessWidescreen.UseFlawlessWidescreen(this, gen, context, profile, plyrIndex);
            }

            if (!gen.GamePlayBeforeGameSetup && !gen.GamePlayAfterLaunch)
            {
                gen.PrePlay(context, this, player);
            }

            if (gen.AltEpicEmuArgs)
            {
                Log("Using pre-defined epic emu params");
                context.StartArguments += " ";
                context.StartArguments += $" -AUTH_LOGIN=unused -AUTH_PASSWORD=bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb -AUTH_TYPE=exchangecode -epicapp=Nucleus -epicenv=Prod -EpicPortal -epiclocale={gen.EpicLang} -epicusername={player.Nickname} -epicuserid={player.Nickname} ";
            }

            if (gen.EpicEmuArgs)
            {
                context.StartArguments += " ";
                Log("Using alternative pre-defined epic emu params");

                if (!context.StartArguments.Contains("-AUTH_LOGIN"))
                {
                    Log("Epic Emu parameters not found in arguments. Adding the necessary parameters to existing starting arguments");
                    //AUTH_LOGIN = unused - AUTH_PASSWORD = cdcdcdcdcdcdcdcdcdcdcdcdcdcdcdcd - AUTH_TYPE = exchangecode - epicapp = CrabTest - epicenv = Prod - EpicPortal - epiclocale = en - epicusername <= same username than in the.json > -epicuserid <= same epicid than in the.json                     
                    context.StartArguments += $" -AUTH_LOGIN=unused -AUTH_PASSWORD=bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb -AUTH_TYPE=exchangecode -epicapp=Nucleus -epicenv=Prod -EpicPortal -epiclocale={gen.EpicLang} -epicusername={player.Nickname} -epicuserid=0000000000000000000000000player{plyrIndex + 1} ";
                }
            }

            if (gen.PauseBetweenContextAndLaunch > 0)
            {
                Log(string.Format("Pausing for {0} seconds", gen.PauseBetweenContextAndLaunch));
                Thread.Sleep(TimeSpan.FromSeconds(gen.PauseBetweenContextAndLaunch));
            }

            startArgs = context.StartArguments;

            if (!string.IsNullOrEmpty(SteamFunctions.lobbyConnectArg) && plyrIndex > 0)
            {
                startArgs = SteamFunctions.lobbyConnectArg + " " + startArgs;
                Log("Goldberg Lobby Connect: Will join lobby ID " + SteamFunctions.lobbyConnectArg.Substring(15));
            }

            if (context.Hook.CustomDllEnabled && !gen.ProcessChangesAtEnd)
            {
                XInputPlusDll.CustomDllEnabled(this, gen, context, player, playerBounds, plyrIndex, setupDll);
            }

            if (gen.GoldbergWriteSteamIDAndAccount)
            {
                SteamFunctions.GoldbergWriteSteamIDAndAccount(this, gen, linkFolder, plyrIndex, player);
            }

            if (gen.ChangeIPPerInstance && !gen.ProcessChangesAtEnd)
            {
                Network.ChangeIPPerInstance(this, plyrIndex);
            }

            if (gen.LauncherExe?.Length > 0)
            {
                //Force no starting arguments as a launcher is being used
                if (gen.HookInit || gen.RenameNotKillMutex || gen.SetWindowHookStart || gen.BlockRawInput || gen.CreateSingleDeviceFile)
                {
                    Log("Disabling start up hooks as a launcher is being used");
                    gen.HookInit = false;
                    gen.RenameNotKillMutex = false;
                    gen.SetWindowHookStart = false;
                    gen.BlockRawInput = false;
                    gen.CreateSingleDeviceFile = false;
                }
            }

            if (processingExit)
            {
                return 1;
            }
            else
            {
                TriggerOSD(1200, $"Starting {gen.GameName} instance for {player.Nickname} as Player #{player.PlayerID + 1}");
            }

            if (context.NeedsSteamEmulation)
            {
                SteamFunctions.SmartSteamEmu(this, gen, context, player, plyrIndex, linkFolder, startArgs, exePath, setupDll);
                proc = null;//leave this here just in case for now
                Thread.Sleep(5000);
            }
            else
            {
                if (gen.ForceEnvironmentUse && gen.ThirdPartyLaunch)
                {
                    Log("Force Nucleus environment use");
                    NucleusUsers.CreateUserEnvironment(
                        Environment.GetEnvironmentVariables(), cmd, false, 
                        (gen.DocumentsConfigPath?.Length > 0 || gen.DocumentsSavePath?.Length > 0),
                        player.Nickname, NucleusEnvironmentRoot, DocumentsRoot
                    );
                }

                if (!gen.ThirdPartyLaunch)
                {
                    DoDirectLaunch();
                }
                else
                {
                    Log("Skipping launching of game via Nucleus for third party launch");

                    if (!gen.IgnoreThirdPartyPrompt)
                    {
                        Forms.Prompt prompt = new Forms.Prompt("Press OK when game has launched.");
                        prompt.ShowDialog();
                    }
                }

                if (gen.LauncherExe?.Length > 0)
                {
                    //Force process to be null as we don't want launcher process
                    Log("Dropping process as it is the launcher");
                    proc = null;
                }

                if (proc != null && proc.ProcessName.ToLower() == "steamclient_loader")
                {
                    Log("Dropping process as it is the Goldberg Steamclient Loader");
                    proc = null;
                }

                if (proc != null && !Process.GetProcesses().Any(x => x.Id == proc.Id))
                {
                    Log("Process " + proc.Id + " is no longer running. Will search for process");
                    proc = null;
                }
            }

            if (gen.GamePlayAfterLaunch && !gen.GamePlayBeforeGameSetup)
            {
                gen.PrePlay(context, this, player);
            }

            if (gen.LaunchAsDifferentUsers || gen.LaunchAsDifferentUsersAlt)
            {
                if (launchProc != null)
                {
                    launchProc.Kill();
                    launchProc = null;
                }
                else
                {
                    Log("Unable to find intermediary proc to kill");
                }
            }

            if (GameProfile.PauseBetweenInstanceLaunch > 0)
            {
                gen.PauseBetweenStarts = GameProfile.PauseBetweenInstanceLaunch;
                Log("Set Pause Between Instances Startup to " + gen.PauseBetweenStarts + " s");
            }

            if (gen.ProcessChangesAtEnd)
            {
                if (plyrIndex == (players.Count - 1))
                {
                    if (gen.PauseBetweenStarts > 0)
                    {
                        Log(string.Format("Pausing for {0} seconds", gen.PauseBetweenStarts));
                        Thread.Sleep(TimeSpan.FromSeconds(gen.PauseBetweenStarts));
                    }

                    if (gen.PromptProcessChangesAtEnd)
                    {
                        Log("Prompted user before processing end changes");
                        Forms.Prompt prompt = new Forms.Prompt("Press OK when ready to make changes to game processes.");
                        prompt.ShowDialog();
                    }

                    ProcessChangeAtEnd();

                    return 1;
                }
                else
                {
                    if (gen.PromptAfterFirstInstance && plyrIndex == 0)
                    {
                        Log("Prompted user after first instance");
                        Forms.Prompt prompt = new Forms.Prompt("Press OK when ready to launch the rest of the instances.");
                        prompt.ShowDialog();
                    }

                    if (gen.PromptBetweenInstances)
                    {
                        if (gen.PauseBetweenStarts > 0)
                        {
                            Log(string.Format("Pausing for {0} seconds", gen.PauseBetweenStarts));
                            Thread.Sleep(TimeSpan.FromSeconds(gen.PauseBetweenStarts));
                        }
                        Log(string.Format("Prompted user for Instance {0}", (plyrIndex + 2)));
                        Forms.Prompt prompt = new Forms.Prompt("Press OK when ready to launch instance " + (plyrIndex + 2) + ".");
                        prompt.ShowDialog();
                    }
                    else
                    {
                        if (gen.PauseBetweenStarts > 0)
                        {
                            if (!gen.PromptAfterFirstInstance || (gen.PromptAfterFirstInstance && plyrIndex > 0))
                            {
                                Log(string.Format("Pausing for {0} seconds", gen.PauseBetweenStarts));
                                Thread.Sleep(TimeSpan.FromSeconds(gen.PauseBetweenStarts));
                            }
                        }
                    }

                    return 0;
                }
            }

            if (gen.PromptBeforeProcessGrab)
            {
                Log("Prompted user before searching for game process");

                Forms.Prompt prompt = new Forms.Prompt("Press OK when ready for Nucleus to search for game process.");
                prompt.ShowDialog();
            }

            if (gen.PauseBetweenProcessGrab > 0)
            {
                Log(string.Format("Pausing for {0} seconds", gen.PauseBetweenProcessGrab));
                Thread.Sleep(TimeSpan.FromSeconds(gen.PauseBetweenProcessGrab));
            }

            if (processingExit)
            {
                return 1;
            }

            if (gen.LauncherExe?.Length > 0 && gen.RunLauncherAndExe)
            {
                if (DoLauncherLaunch() != 0) return 1;
            }

            bool processNotRunning = proc != null && !Process.GetProcesses().Any(x => x.Id == proc.Id);

            if (processNotRunning || 
                    gen.ForceProcessSearch || gen.NeedsSteamEmulation || gen.ForceProcessPick || proc == null || gen.CMDLaunch || 
                    gen.UseForceBindIP || gen.GameName == "Halo Custom Edition" || (proc != null && !ProcessUtil.IsRunning(proc)) /*|| gen.LauncherExe?.Length > 0*/)
            {
                if (processNotRunning)
                {
                    Log("Process " + proc.Id + " is no longer running. Will search for process");
                }

                if (gen.GameName == "Halo Custom Edition" || gen.GameName == "Ghost Recon Wildlands" /*|| gen.LauncherExe?.Length > 0*/)
                {
                    //Halo CE and GRW seem to need to wait X additional seconds otherwise crashes...
                    Thread.Sleep(10000);
                }

                string ids = "";

                foreach (int id in attachedIds)
                {
                    ids += id + " ";
                }

                Log("PIDs stored " + ids);

                if (!gen.ForceProcessPick)
                {
                    SearchForGameProcessByName();
                }

                if (proc == null || gen.ForceProcessPick)
                {
                    proc = LaunchProcessPick(player);
                }
            }
            else
            {
                Log(string.Format("Obtained process {0} (pid {1})", proc.ProcessName, proc.Id));
                attached.Add(proc);
                attachedIds.Add(proc.Id);
                player.ProcessID = proc.Id;

                if (player.IsKeyboardPlayer && !player.IsRawKeyboard)
                {
                    keyboardProcId = proc.Id;
                }
            }

            if (processingExit)
            {
                return 1;
            }

            if (!ProcessUtil.IsRunning(proc))
            {
                SearchForGameProcessByWindowTitle();
            }

            Log(string.Format("Process details; Name: {0}, ID: {1}, MainWindowtitle: {2}, NucleusGetMainWindowHandle(): {3}", proc.ProcessName, proc.Id, proc.MainWindowTitle, proc.NucleusGetMainWindowHandle()));

            if (gen.WriteToProcessMemory?.Length > 0)
            {
                if (gen.WriteToProcessMemory.Contains('|'))
                {
                    ProcessUtil.WriteToProcessMemory(gen, proc);
                }
            }

            if (gen.GoldbergLobbyConnect && plyrIndex == 0)
            {
                SteamFunctions.GoldbergLobbyConnect(this);
            }

            if (plyrIndex > 0 && gen.ResetWindows && prevProcessData != null)
            {
                GlobalWindowMethods.ResetWindows(this, gen, prevProcessData, prevWindowX, prevWindowY, prevWindowWidth, prevWindowHeight, plyrIndex);
            }

            SetupProcessData();
            SetupProcessorPriority();

            if (gen.IdInWindowTitle || !string.IsNullOrEmpty(gen.FlawlessWidescreen))
            {
                AddIDToWindowTitle();
            }

            if (!gen.ProtoInput.InjectStartup &&
                (gen.ProtoInput.InjectRuntime_EasyHookMethod ||
                    gen.ProtoInput.InjectRuntime_EasyHookStealthMethod ||
                    gen.ProtoInput.InjectRuntime_RemoteLoadMethod))
            {
                DoProtoInputRuntimeInjection();
            }

            if (gen.PromptAfterFirstInstance)
            {
                if (plyrIndex == 0)
                {
                    Log(string.Format("Prompted user after first instance", (plyrIndex + 2)));
                    Prompt prompt = new Prompt("Press OK when ready to launch the rest of the instances.");
                    prompt.ShowDialog();
                }
            }

            if (gen.PromptBetweenInstances && plyrIndex < (players.Count - 1))
            {
                PromptLaunchNextInstance();
            }
            else if (gen.PromptBetweenInstances && plyrIndex == players.Count - 1 && 
                        (gen.HookFocus || gen.FakeFocus || gen.SetWindowHook || gen.HideCursor || 
                        gen.PreventWindowDeactivation || gen.SetTopMostAtEnd))
            {
                PromptInstallPostHooks();
            }
            else if (!gen.PromptAfterFirstInstance || (gen.PromptAfterFirstInstance && plyrIndex > 0))
            {
                Log(string.Format("Pausing for {0} seconds", gen.PauseBetweenStarts));
                Thread.Sleep(TimeSpan.FromSeconds(gen.PauseBetweenStarts));
            }

            if (!ProcessUtil.IsRunning(proc))
            {
                FindProcessByWindowTitle();
            }

            //Set up raw input window
            //if (player.IsRawKeyboard || player.IsRawMouse)
            /*{
                var window = GlobalWindowMethods.CreateRawInputWindow(this, gen, proc, player);
                nextWindowToInject = window;
            }*/

            if (plyrIndex == (players.Count - 1))
            {
                if (processingExit)
                {
                    return 1;
                }

                Log("All instances accounted for, performing final preperations");

                AudioReroute.DoAudioRerouteForPlayers(players);

                if (gen.KillLastInstanceMutex && !gen.RenameNotKillMutex)
                {
                    if (KillLastMutex() != 0) return 1;
                }

                Thread.Sleep(1000);

                if (gen.ResetWindows)
                {
                    GlobalWindowMethods.ResetWindows(this, gen, data, prevWindowX, prevWindowY, prevWindowWidth, prevWindowHeight, plyrIndex + 1);
                }

                if (gen.FakeFocus)
                {
                    Log($"Start sending fake focus messages every {gen.FakeFocusInterval} ms");
                    WindowFakeFocus.Initialize(this, gen, profile);
                    WindowFakeFocus.fakeFocus = new Thread(WindowFakeFocus.SendFocusMsgs);
                    WindowFakeFocus.fakeFocus.Start();
                }

                if (gen.ForceWindowTitle)
                {
                    RenameWindowTitle();
                }

                if (!string.IsNullOrEmpty(gen.FlawlessWidescreen))
                {
                    ResetWindowTitle();
                }

                if ((plyrIndex > 0 || players.Count == 1) && 
                    (gen.HookFocus || gen.SetWindowHook || gen.HideCursor || 
                    gen.PreventWindowDeactivation || gen.SupportsMultipleKeyboardsAndMice))
                {
                    InjectLastInstance();
                }

                if (!gen.IgnoreWindowBorderCheck)
                {
                    CheckWindowBorder();
                }

                // Fake mouse cursors
                if (gen.DrawFakeMouseCursor)
                {
                    RawInputManager.CreateCursorsOnWindowThread(gen.UpdateFakeMouseWithInternalInput, gen.DrawFakeMouseCursorForControllers);
                }

                if (FindDIEmWndWindow() != 0) return 1;

                RawInputProcessor.Start();

                if (gen.SetForegroundWindowElsewhere)
                {
                    GlobalWindowMethods.ChangeForegroundWindow();
                }

                if (gen.SendFakeFocusMsg)
                {
                    WindowFakeFocus.Initialize(this, gen, profile);
                    WindowFakeFocus.SendFakeFocusMsg();
                }
            }
            Console.WriteLine(player.SteamID);

            return 1;
        }

        #region Here we backup files/folders added if any specified in the game handler
        private void ProceedBackup()
        {
            if (gen.BackupFiles != null)
            {
                //Game.FilesToBackup
                if (gen.BackupFiles.Length > 0)
                {
                    BackupFiles.StartFilesBackup(gen, gen.BackupFiles);
                }
            }

            if (context != null)
            {
                if (context.BackupFiles != null)
                {
                    //Context.FilesToBackup
                    if (context.BackupFiles.Length > 0)
                    {
                        BackupFiles.StartFilesBackup(gen, context.BackupFiles);
                    }
                }
            }

            if (gen.BackupFolders != null)
            {
                //Game.BackupFolders
                if (gen.BackupFolders.Length > 0)
                {
                    BackupFiles.StartFoldersBackup(gen, gen.BackupFolders);
                }
            }

            if (context != null)
            {
                if (context.BackupFolders != null)
                {
                    //Context.BackupFolders
                    if (context.BackupFolders.Length > 0)
                    {
                        BackupFiles.StartFoldersBackup(gen, context.BackupFolders);
                    }
                }
            }
        }

        #endregion

        private Process LaunchProcessPick(PlayerInfo player)
        {
            ProcessPicker ppform = new ProcessPicker(); //using ProcessPicker.cs 
            Log("Launching process picker");

            ppform.pplistBox.DoubleClick += new EventHandler(SelBtn_Click);

            foreach (Control c in ppform.Controls)
            {
                if (c.Name == "refrshBtn")
                {
                    c.Click += new EventHandler(RefrshBtn_Click);
                }
            }

            foreach (Control c in ppform.Controls)
            {
                if (c.Name == "selBtn")
                {
                    c.Click += new EventHandler(SelBtn_Click);
                }
            }

            Process[] allProc = Process.GetProcesses();

            foreach (Process p in allProc)
            {
                if (p.Id == 0 || string.IsNullOrEmpty(p.MainWindowTitle))
                {
                    continue;
                }
                if (attachedIds.Contains(p.Id))
                {
                    if (p.ProcessName.ToLower().Contains(Path.GetFileNameWithoutExtension(gen.ExecutableName).ToLower()) || (gen.LauncherExe?.Length > 0 && p.ProcessName.ToLower().Contains(Path.GetFileNameWithoutExtension(gen.LauncherExe).ToLower())))
                    {
                        ppform.pplistBox.Items.Insert(0, p.Id + " - (DO NOT USE - Already assigned in Nucleus) " + p.ProcessName);
                    }
                    else
                    {
                        ppform.pplistBox.Items.Add(p.Id + " - (DO NOT USE - Already assigned in Nucleus) " + p.ProcessName);
                    }
                }
                else
                {
                    if (p.ProcessName.ToLower().Contains(Path.GetFileNameWithoutExtension(gen.ExecutableName).ToLower()) || (gen.LauncherExe?.Length > 0 && p.ProcessName.ToLower().Contains(Path.GetFileNameWithoutExtension(gen.LauncherExe).ToLower())))
                    {
                        ppform.pplistBox.Items.Insert(0, p.Id + " - " + p.ProcessName);
                    }
                    else
                    {
                        ppform.pplistBox.Items.Add(p.Id + " - " + p.ProcessName);
                    }
                }
            }

            ppform.ShowDialog();
            WindowScrape.Static.HwndInterface.MakeTopMost(ppform.Handle);

            if (ppform.pplistBox.SelectedItem != null)
            {
                Process proc = Process.GetProcessById(int.Parse(ppform.pplistBox.SelectedItem.ToString().Split(' ')[0]));
                Log(string.Format("Obtained process {0} (pid {1}) via process picker", proc.ProcessName, proc.Id));
                attached.Add(proc);
                attachedIds.Add(proc.Id);
                player.ProcessID = proc.Id;
                if (player.IsKeyboardPlayer && !player.IsRawKeyboard)
                {
                    keyboardProcId = proc.Id;
                }

                return proc;
            }

            return null;
        }

        public void SelBtn_Click(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            Form ppform = control.FindForm();

            if (control.GetType() == typeof(ListBox))
            {
                ListBox listBox = control as ListBox;
                if (listBox.SelectedItem == null)
                {
                    MessageBox.Show("No process has been selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else if (control.GetType() == typeof(Button))
            {
                foreach (Control c in ppform.Controls)
                {
                    if (c.GetType() == typeof(ListBox))
                    {
                        ListBox listBox = c as ListBox;
                        if (listBox.SelectedItem == null)
                        {
                            MessageBox.Show("No process has been selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                }
            }

            ppform.Close();
        }

        public void RefrshBtn_Click(object sender, EventArgs e)
        {
            Control control = (Button)sender;

            Form ppform = control.FindForm();
            foreach (Control l in ppform.Controls)
            {
                if (l.GetType() == typeof(ListBox))
                {
                    ListBox listBox = l as ListBox;
                    listBox.Items.Clear();

                    Process[] allProc = Process.GetProcesses();
                    foreach (Process p in allProc)
                    {
                        if (p.Id == 0 || string.IsNullOrEmpty(p.MainWindowTitle))
                        {
                            continue;
                        }
                        if (attachedIds.Contains(p.Id))
                        {
                            if (p.ProcessName.ToLower().Contains(Path.GetFileNameWithoutExtension(gen.ExecutableName).ToLower()) || (gen.LauncherExe?.Length > 0 && p.ProcessName.ToLower().Contains(Path.GetFileNameWithoutExtension(gen.LauncherExe).ToLower())))
                            {
                                listBox.Items.Insert(0, p.Id + " - (DO NOT USE - Already assigned in Nucleus)" + p.ProcessName);
                            }
                            else
                            {
                                listBox.Items.Add(p.Id + " - (DO NOT USE - Already assigned in Nucleus)" + p.ProcessName);
                            }
                        }
                        else
                        {
                            if (p.ProcessName.ToLower().Contains(Path.GetFileNameWithoutExtension(gen.ExecutableName).ToLower()) || (gen.LauncherExe?.Length > 0 && p.ProcessName.ToLower().Contains(Path.GetFileNameWithoutExtension(gen.LauncherExe).ToLower())))
                            {
                                listBox.Items.Insert(0, p.Id + " - " + p.ProcessName);
                            }
                            else
                            {
                                listBox.Items.Add(p.Id + " - " + p.ProcessName);
                            }
                        }
                    }
                }
            }
        }

        private void ProcessChangeAtEnd()
        {
            List<PlayerInfo> players = profile.DevicesList;

            Log("Starting process end changes");

            for (int i = 0; i < players.Count; i++)
            {
                Log("Setting up player " + (i + 1));

                PlayerInfo player = players[i];
                Rectangle playerBounds = player.MonitorBounds;
                owner = player.Owner;
                playerBoundsWidth = playerBounds.Width;
                playerBoundsHeight = playerBounds.Height;

                if (gen.ChangeIPPerInstance)
                {
                    Network.ChangeIPPerInstance(this, i);
                }

                bool setupDll = true;
                if (!gen.SymlinkGame && !gen.HardlinkGame && !gen.HardcopyGame && i > 0)
                {
                    setupDll = false;
                }

                if (gen.XInputPlusDll?.Length > 0)
                {
                    XInputPlusDll.SetupXInputPlusDll(this, gen, garch, player, context, i, setupDll);
                }

                if (gen.UseDevReorder)
                {
                    DevReorder.UseDevReorder(this, gen, garch, player, players, i, setupDll);
                }

                if (gen.UseX360ce)
                {
                    X360ce.UseX360ce(this, gen, i, players, player, context, setupDll);
                }

                if (gen.PromptBetweenInstancesEnd)
                {
                    Log(string.Format("Prompted user for Instance {0}", (i + 1)));

                    Prompt prompt = new Prompt("Press OK when game instance " + (i + 1) + " has been launched and/or you wish to continue.");
                    prompt.ShowDialog();
                }
                else
                {
                    Thread.Sleep(1000);
                }

                ProcessData pdata = null;
                if (player.ProcessData != null)
                {
                    Log(string.Format("Using player process data. Process: {0} (pid {1})", player.ProcessData.Process.ProcessName, player.ProcessData.Process.Id));
                    pdata = player.ProcessData;
                }
                else
                {
                    Process[] cProcs;
                    Log("Player process data was null. Attempting to find process by executable name and start time");
                    try
                    {
                        cProcs = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(gen.ExecutableName)).OrderBy(p => p.StartTime).ToArray();
                    }
                    catch
                    {
                        Log("Unable to get processes by executable name ordered by their start time, using just by exe name alone");
                        cProcs = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(gen.ExecutableName));
                    }

                    foreach (Process p in cProcs)
                    {
                        if (p.ProcessName.ToLower() == Path.GetFileNameWithoutExtension(gen.ExecutableName.ToLower()) && !attachedIds.Contains(p.Id))
                        {
                            Log(string.Format("Found process {0} (pid {1})", p.ProcessName, p.Id));
                            pdata = new ProcessData(p);
                            player.ProcessData = pdata;
                            attachedIds.Add(p.Id);
                            attached.Add(p);
                            player.ProcessID = p.Id;
                            break;
                        }
                    }
                }

                Thread.Sleep(1000);

                Process proc = player.ProcessData.Process;

                var window = GlobalWindowMethods.CreateRawInputWindow(this, gen, proc, players[i]);

                Thread.Sleep(1000);

                if (gen.HookFocus || gen.SetWindowHook || gen.HideCursor || gen.PreventWindowDeactivation || gen.SupportsMultipleKeyboardsAndMice)
                {
                    Log("Injecting post-launch hooks for process " + proc.ProcessName + " (pid " + proc.Id + ")");

                    if (gen.PreventWindowDeactivation && !isPrevent)
                    {
                        Log("PreventWindowDeactivation detected, setting flag");
                        isPrevent = true;
                    }

                    if (isPrevent)
                    {
                        if (players[i].IsKeyboardPlayer && gen.KeyboardPlayerSkipPreventWindowDeactivate)
                        {
                            Log("Ignoring PreventWindowDeactivation for keyboard player");
                            gen.PreventWindowDeactivation = false;
                        }
                        else
                        {
                            Log("Keeping PreventWindowDeactivation on");
                            gen.PreventWindowDeactivation = true;
                        }
                    }

                    if (gen.PostHookInstances?.Length > 0)
                    {
                        string[] instancesToHook = gen.PostHookInstances.Split(',');
                        foreach (string instanceToHook in instancesToHook)
                        {
                            if (int.Parse(instanceToHook) == (i + 1))
                            {
                                User32Interop.SetForegroundWindow(proc.NucleusGetMainWindowHandle());
                                DllsInjector.InjectDLLs(this, gen, proc, window, players[i]);
                            }
                        }
                    }
                    else
                    {
                        User32Interop.SetForegroundWindow(proc.NucleusGetMainWindowHandle());
                        DllsInjector.InjectDLLs(this, gen, proc, window, players[i]);
                    }
                }

                Thread.Sleep(1000);

                GlobalWindowMethods.ChangeGameWindow(this, gen, proc, players, i);

                Thread.Sleep(1000);

                if (gen.KillMutexAtEnd)
                {
                    for (; ; )
                    {
                        Thread.Sleep(1000);

                        if (gen.KillMutex != null)
                        {
                            Log("Beginning mutex killing");
                            if (gen.KillMutex.Length > 0)
                            {
                                if (gen.KillMutexDelay > 0)
                                {
                                    Thread.Sleep((gen.KillMutexDelay * 1000));
                                }

                                if (StartGameUtil.MutexExists(pdata.Process, gen.KillMutexType, gen.PartialMutexSearch, gen.KillMutex))
                                {
                                    // mutexes still exist, must kill
                                    StartGameUtil.KillMutex(pdata.Process, gen.KillMutexType, gen.PartialMutexSearch, gen.KillMutex);
                                    pdata.KilledMutexes = true;
                                    break;
                                }
                                else
                                {
                                    // mutexes dont exist anymore
                                    break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }

                        if (processingExit)
                        {
                            break;
                        }
                    }
                }

                if (gen.ResetWindows)
                {
                    Thread.Sleep(1000);
                    GlobalWindowMethods.ResetWindows(this, gen, players[i].ProcessData, players[i].MonitorBounds.X, players[i].MonitorBounds.Y, players[i].MonitorBounds.Width, players[i].MonitorBounds.Height, i + 1);
                }

                Thread.Sleep(3000);

                if (i == (players.Count - 1))
                {
                    Log("End process changes - All done!");
                    try
                    {
                        if (statusWinThread != null && statusWinThread.IsAlive)
                        {
                            Thread.Sleep(5000);
                            if (statusWinThread != null && statusWinThread.IsAlive)
                            {
                                statusWinThread.Abort();
                            }
                        }
                    }
                    catch { }
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }

            if (gen.FakeFocus)
            {
                Log("Start sending fake focus messages every 1000 ms");
                WindowFakeFocus.Initialize(this, gen, profile);
                WindowFakeFocus.fakeFocus = new Thread(WindowFakeFocus.SendFocusMsgs);
                WindowFakeFocus.fakeFocus.Start();
            }

            if (gen.SetForegroundWindowElsewhere)
            {
                GlobalWindowMethods.ChangeForegroundWindow();
            }

            if (gen.UseNucleusEnvironment)
            {
                RegistryUtil.RestoreUserEnvironmentRegistryPath();
            }

            if (!processingExit)
            {
                GamepadNavigation.EnabledRuntime = false;

                GameProfile.SaveGameProfile(profile);
            }

            gen.OnFinishedSetup?.Invoke();
        }

        struct TickThread
        {
            public double Interval;
            public Action Function;
        }

        struct UpdateTickThread
        {
            public double Interval;
        }

        public void StartUpdateTick(double interval)
        {
            Thread updateTickThread = new Thread(StartUpdateTickThread);

            UpdateTickThread tick = new UpdateTickThread
            {
                Interval = interval
            };

            updateTickThread.Start(tick);
        }

        private void StartUpdateTickThread(object state)
        {
            UpdateTickThread updateTickThread = (UpdateTickThread)state;

            for (; ; )
            {
                if (!string.IsNullOrEmpty(error))
                {
                    End(false);
                    break;
                }

                Thread.Sleep(TimeSpan.FromMilliseconds(updateTickThread.Interval));
                Update(updateTickThread.Interval, false);

                if (hasEnded)
                {                   
                    break;
                }
            }
        }

        public void Update(double delayMS, bool refresh)
        {
            GlobalWindowMethods.UpdateAndRefreshGameWindows(this, gen, profile, delayMS, refresh);
        }

        public void CenterCursor()
        {
            List<PlayerInfo> players = profile.DevicesList;
            if (players == null)
            {
                return;
            }

            for (int i = 0; i < players.Count; i++)
            {
                PlayerInfo p = players[i];

                if (p.IsKeyboardPlayer && !p.IsRawKeyboard)
                {
                    ProcessData data = p.ProcessData;
                    if (data == null)
                    {
                        continue;
                    }

                    if (data.HWnd != null)
                    {
                        User32Interop.SetForegroundWindow(data.HWnd.NativePtr);
                    }
                }
            }
        }

        public void End(bool fromStopButton)
        {
            if (fromStopButton && LockInput.IsLocked)
            {
                //TODO: For some reason the Stop button is clicked during split screen. Temporary fix is to not end if input is locked.//Should be fixed now by unfocusing the stop button.
                Log("IGNORING SHUTDOWN BECAUSE INPUT LOCKED");
                return;
            }

            if (!processingExit)
            {
                processingExit = true;
            }
            else
            {
                Log("Already processing exit");
                return;
            }
        
            if (GamepadNavigation.Enabled)
            {
                GamepadNavigation.EnabledRuntime = true;
            }

            if (ini.IniReadValue("Misc", "ShowStatus") == "True")
            {
                try
                {
                    if (statusWinThread != null && statusWinThread.IsAlive)
                    {
                        statusWinThread.Abort();
                        Thread.Sleep(50);
                    }

                    statusWinThread = new Thread(ShowStatus);
                    statusWinThread.Start();
                }
                catch { }
            }

            Log("----------------- SHUTTING DOWN -----------------");

            GlobalWindowMethods.finish = false;

            if (splitForms.Count > 0)
            {
                foreach (WPFDiv backgroundForm in splitForms)
                {
                    backgroundForm.Dispatcher.Invoke(new Action(() =>
                    {
                        backgroundForm.Close();
                    }));
                }

                splitForms.Clear();
            }

            LockInput.Unlock(false, gen?.ProtoInput);

            gen.OnStop?.Invoke();

            ProcessUtil.KillRemainingProcess(this, gen);

            Thread.Sleep(1000);

            if (gen.CMDBatchClose?.Length > 0)
            {
                cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.UseShellExecute = false;

                cmd.Start();

                for (int x = 0; x < gen.CMDBatchClose.Length; x++)
                {
                    Log("Running command line: " + gen.CMDBatchClose[x]);
                    cmd.StandardInput.WriteLine(gen.CMDBatchClose[x]);
                }

                cmd.StandardInput.Flush();
                cmd.StandardInput.Close();
            }

            Thread.Sleep(1000);

            if (gen.NeedsSteamEmulation && !gen.SymlinkGame && !gen.HardlinkGame && !gen.HardcopyGame && !string.IsNullOrEmpty(ssePath))
            {
                Log("Deleting SmartSteamEmu folder and files");
                Directory.Delete(ssePath, true);
                Thread.Sleep(1000);
            }

            if (gen.DeleteOnClose?.Length > 0)
            {
                string exeFolder = Path.GetDirectoryName(userGame.ExePath).ToLower();
                string rootFolder = exeFolder;
                if (!string.IsNullOrEmpty(gen.BinariesFolder))
                {
                    rootFolder = StringUtil.ReplaceCaseInsensitive(exeFolder, gen.BinariesFolder.ToLower(), "");
                }

                foreach (string file in gen.DeleteOnClose)
                {
                    string fullFilePath = Path.Combine(rootFolder, file);
                    if (File.Exists(fullFilePath))
                    {
                        Log("DeleteOnClose: Deleting " + fullFilePath);
                        File.Delete(fullFilePath);
                    }
                }
            }

            if (userBackedFiles?.Count > 0)
            {
                foreach (string filePath in userBackedFiles)
                {
                    string origFileName = filePath.Replace("_NUCLEUS_BACKUP" + Path.GetExtension(filePath), Path.GetExtension(filePath));
                    Log($"Restoring {origFileName}");
                    if (File.Exists(origFileName) && File.Exists(filePath))
                    {
                        File.Delete(origFileName);
                    }

                    File.Move(filePath, origFileName);
                }
            }
            else
            {
                Log("No Nucleus backed up files found");
            }

            Thread.Sleep(1000);

            RegistryUtil.RestoreRegistry("Restore from GenericGameHandler");

            Thread.Sleep(1000);

            MonitorsDpiScaling.ResetMonitorsSettings(this);

            Thread.Sleep(1000);

            List<PlayerInfo> data = profile.DevicesList;

            foreach (PlayerInfo player in data)
            {
                if (player.DInputJoystick != null)
                {
                    player.DInputJoystick.Dispose();
                }
            }

            //if (WindowFakeFocus.fakeFocus != null && WindowFakeFocus.fakeFocus.IsAlive)
            //{
            //    Log("Aborting thread to send fake focus messages");
            //   // WindowFakeFocus.fakeFocus.Abort();
            //}

            if (!earlyExit)
            {
                folderUsers = Directory.GetDirectories(Path.GetDirectoryName(NucleusEnvironmentRoot));

                if (gen.TransferNucleusUserAccountProfiles)
                {
                    NucleusUsers.TransferNucleusUserAccountProfiles(this, data);
                }

                if (!bool.Parse(ini.IniReadValue("Misc", "KeepAccounts")))
                {
                    if (gen.LaunchAsDifferentUsers || gen.LaunchAsDifferentUsersAlt)
                    {
                        WindowsUsersUtil.DeleteCreatedWindowsUser(this);
                        Thread.Sleep(1000);
                    }
                }

                if (gen.ChangeIPPerInstanceAlt)
                {
                    Network.ChangeIPPerInstanceAltDeleteAdapter(this);
                    Thread.Sleep(1000);
                }

                if (gen.ChangeIPPerInstance)
                {
                    Network.ChangeIPPerInstanceRestoreIP(this);
                }

                if (gen.FlawlessWidescreen?.Length > 0)
                {
                    FlawlessWidescreen.KillFlawlessWidescreen(this, gen);
                }
            }

            if (gen.ProtoInput.AutoHideTaskbar || GameProfile.UseSplitDiv)
            {
                ProtoInput.protoInput.SetTaskbarAutohide(false);
            }

            User32Util.ShowTaskBar();
            
            hasEnded = true;

            GameManager.Instance.ExecuteBackup(userGame.Game);

            LogManager.UnregisterForLogCallback(this);

            foreach (var window in RawInputManager.windows)
            {
                window.HookPipe?.Close();
            }

            Cursor.Clip = Rectangle.Empty; // guarantee were not clipping anymore

            if (_cursorModule != null)
            {
                _cursorModule.Stop();
            }

            Thread.Sleep(1000);
           
            RawInputManager.EndSplitScreen();

            ProceedBackup();

            // delete symlink folder and users accounts 
#if RELEASE
            if (!gen.SymlinkGame && !gen.HardlinkGame && !gen.HardcopyGame)
            {
                FileUtil.CleanOriginalgGameFolder(this);
            }

            if (!gen.KeepSymLinkOnExit && !userGame.KeepSymLink)
            {
                CleanGameContent.CleanContentFolder(gen);
            }

            if  (!bool.Parse(ini.IniReadValue("Misc", "KeepAccounts")))
            {
                WindowsUsersUtil.DeleteCreatedWindowsUserFolder(this);
            }
#endif
            Log("All done closing operations.");

            try
            {
                if (statusWinThread != null && statusWinThread.IsAlive)
                {
                    statusWinThread.Abort();
                }
            }
            catch { }

            Ended?.Invoke();          
        }

        public void Log(StreamWriter writer)
        {
        }

        public void Log(string logMessage)
        {
            try
            {
                logMsg = logMessage;

                if (ini.IniReadValue("Misc", "DebugLog") == "True")
                {
                    using (StreamWriter writer = new StreamWriter("debug-log.txt", true))
                    {
                        writer.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}]HANDLER: {logMessage}");
                        writer.Close();
                    }
                }
            }
            catch { }
        }

        private void StatusForm_Closing(object sender, FormClosingEventArgs e)
        {
            if (!processingExit)
            {
                Thread.Sleep(5000);
            }
        }
    }
}