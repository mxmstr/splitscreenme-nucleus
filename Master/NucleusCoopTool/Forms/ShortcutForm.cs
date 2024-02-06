using Nucleus.Coop.Tools;
using Nucleus.Gaming;
using Nucleus.Gaming.Controls.SetupScreen;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.InputManagement.Gamepads;
using Nucleus.Gaming.Forms.NucleusMessageBox;
using Nucleus.Gaming.Platform.PCSpecs;
using Nucleus.Gaming.Tools.GlobalWindowMethods;
using Nucleus.Gaming.Windows;
using Nucleus.Gaming.Windows.Interop;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Nucleus.Coop.Forms
{
    public partial class ShortcutForm : Form
    {
        private int KillProcess_HotkeyID = 1;
        private int TopMost_HotkeyID = 2;
        private int StopSession_HotkeyID = 3;
        private int SetFocus_HotkeyID = 4;
        private int ResetWindows_HotkeyID = 5;
        private int Cutscenes_HotkeyID = 6;
        private int Switch_HotkeyID = 7;
        private int currentStepIndex;
      
        public string lockKeyIniString;

        private bool hotkeysLocked = false;
        private bool ToggleCutscenes = false;

        public Action<IntPtr> RawInputAction { get; set; }

        private System.Windows.Forms.Timer hotkeysLockedTimer;//Avoid hotkeys spamming
        private static GenericGameInfo genericGameInfo;

        public ShortcutForm(string[] args)
        {
            Globals.MainOSD = new WPF_OSD();

            InitializeComponent();
            lockKeyIniString = Globals.ini.IniReadValue("Hotkeys", "LockKey");
            hotkeysLockedTimer = new System.Windows.Forms.Timer();
            hotkeysLockedTimer.Tick += new EventHandler(HotkeysLockedTimerTick);
            Globals.PlayButton = StopButton;
           
            User32Interop.RegisterHotKey(this.Handle, KillProcess_HotkeyID, GetMod(Globals.ini.IniReadValue("Hotkeys", "Close").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), Globals.ini.IniReadValue("Hotkeys", "Close").Split('+')[1].ToString()));
            User32Interop.RegisterHotKey(this.Handle, TopMost_HotkeyID, GetMod(Globals.ini.IniReadValue("Hotkeys", "TopMost").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), Globals.ini.IniReadValue("Hotkeys", "TopMost").Split('+')[1].ToString()));
            User32Interop.RegisterHotKey(this.Handle, StopSession_HotkeyID, GetMod(Globals.ini.IniReadValue("Hotkeys", "Stop").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), Globals.ini.IniReadValue("Hotkeys", "Stop").Split('+')[1].ToString()));
            User32Interop.RegisterHotKey(this.Handle, SetFocus_HotkeyID, GetMod(Globals.ini.IniReadValue("Hotkeys", "SetFocus").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), Globals.ini.IniReadValue("Hotkeys", "SetFocus").Split('+')[1].ToString()));
            User32Interop.RegisterHotKey(this.Handle, ResetWindows_HotkeyID, GetMod(Globals.ini.IniReadValue("Hotkeys", "ResetWindows").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), Globals.ini.IniReadValue("Hotkeys", "ResetWindows").Split('+')[1].ToString()));
            User32Interop.RegisterHotKey(this.Handle, Cutscenes_HotkeyID, GetMod(Globals.ini.IniReadValue("Hotkeys", "Cutscenes").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), Globals.ini.IniReadValue("Hotkeys", "Cutscenes").Split('+')[1].ToString()));
            User32Interop.RegisterHotKey(this.Handle, Switch_HotkeyID, GetMod(Globals.ini.IniReadValue("Hotkeys", "Switch").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), Globals.ini.IniReadValue("Hotkeys", "Switch").Split('+')[1].ToString()));

            SetupScreenControl setupScreen = new SetupScreenControl();
            GameManager gameManager = new GameManager();
            GameProfile gameProfile = new GameProfile();

            genericGameInfo = gameManager.Games.Where(c => c.Value.GUID == args[0]).FirstOrDefault().Value;
            UserGameInfo currentGameInfo = gameManager.User.Games.Where(c => c.GameGuid == genericGameInfo.GUID).FirstOrDefault();
          
            if (genericGameInfo == null)
            {
                NucleusMessageBox.Show("Game not found :(", $"{args[0]} not found in your game list.", false);
                this.Close();
                return;
            }
          
            GameProfile.GameGUID = genericGameInfo.GUID;

            gameProfile.InitializeDefault(genericGameInfo, setupScreen);

            setupScreen.Initialize(currentGameInfo, gameProfile);

            if(!gameProfile.LoadGameProfile(int.Parse(args[1])))
            {
                Console.WriteLine("error no game found ");
                NucleusMessageBox.Show("Profile not found :(", $"Profile n°{args[1]} for {args[0]} has not been found.", false);
                this.Close();
                return;
            }
                       
            string windowsVersion = MachineSpecs.GetPCspecs(null);

            if (!windowsVersion.Contains("Windows 7") &&
                !windowsVersion.Contains("Windows Vista"))
            {
                GamepadShortcuts.GamepadShortcutsThread = new Thread(GamepadShortcuts.GamepadShortcutsUpdate);
                GamepadShortcuts.GamepadShortcutsThread.Start();
                GamepadShortcuts.UpdateShortcutsValue();

                GamepadNavigation.GamepadNavigationThread = new Thread(GamepadNavigation.GamepadNavigationUpdate);
                GamepadNavigation.GamepadNavigationThread.Start();
                GamepadNavigation.UpdateUINavSettings();
            }
         
            System.Threading.Tasks.Task.Run(() =>
            {
                StartFromShortcut.StartFromShortcutInit(args, this, gameManager, currentGameInfo, genericGameInfo, gameProfile);
            });           
        }

        private int GetMod(string modifier)
        {
            int mod = 0;
            switch (modifier)
            {
                case "Ctrl":
                    mod = 2;
                    break;
                case "Alt":
                    mod = 1;
                    break;
                case "Shift":
                    mod = 4;
                    break;
            }
            return mod;
        }

        private void HotkeysLockedTimerTick(Object Object, EventArgs EventArgs)
        {
            hotkeysLocked = false;
            hotkeysLockedTimer.Stop();
        }

        public void TriggerOSD(int timerMS, string text)
        {
            if (!hotkeysLocked)
            {
                hotkeysLockedTimer.Stop();
                hotkeysLocked = true;
                hotkeysLockedTimer.Interval = (timerMS); //millisecond
                hotkeysLockedTimer.Start();

                Globals.MainOSD.Show(timerMS, text);
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x00FF)//WM_INPUT
            {
                RawInputAction(m.LParam);
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == KillProcess_HotkeyID)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    if (hotkeysLocked)
                    {
                        return;
                    }

                    User32Util.ShowTaskBar();
                    Close();
                }
                else
                {
                    TriggerOSD(1600, $"Unlock Inputs First (Press {lockKeyIniString} key)");
                }
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == TopMost_HotkeyID)
            {
                if (hotkeysLocked || StartFromShortcut.I_GameHandler == null)
                {
                    return;
                }

                GlobalWindowMethods.ShowHideWindows(genericGameInfo);
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == StopSession_HotkeyID)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    if (hotkeysLocked || StartFromShortcut.I_GameHandler == null)
                    {
                        return;
                    }

                    StopButton_Click(null, null);

                    TriggerOSD(2000, "Session Ended");
                }
                else
                {
                    TriggerOSD(1600, $"Unlock Inputs First (Press {lockKeyIniString} key)");
                }
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == SetFocus_HotkeyID)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    if (hotkeysLocked || StartFromShortcut.I_GameHandler == null)
                    {
                        return;
                    }

                    GlobalWindowMethods.ChangeForegroundWindow();
                    TriggerOSD(2000, "Game Windows Unfocused");
                    this.Focus();
                }
                else
                {
                    TriggerOSD(1600, $"Unlock Inputs First (Press {lockKeyIniString} key)");
                }
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == ResetWindows_HotkeyID)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    if (hotkeysLocked || StartFromShortcut.I_GameHandler == null)
                    {
                        return;
                    }

                    StartFromShortcut.I_GameHandler.Update(genericGameInfo.HandlerInterval, true);
                }
                else
                {
                    TriggerOSD(1600, $"Unlock Inputs First (Press {lockKeyIniString} key)");
                }
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == Cutscenes_HotkeyID)
            {
                if (hotkeysLocked || StartFromShortcut.I_GameHandler == null)
                {
                    return;
                }

                if (!ToggleCutscenes)
                {
                    GlobalWindowMethods.ToggleCutScenesMode(true);
                    ToggleCutscenes = true;
                }
                else
                {
                    GlobalWindowMethods.ToggleCutScenesMode(false);
                    ToggleCutscenes = false;
                }
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == Switch_HotkeyID)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    if (hotkeysLocked || StartFromShortcut.I_GameHandler == null)
                    {
                        return;
                    }

                    GlobalWindowMethods.SwitchLayout();
                }
                else
                {
                    TriggerOSD(1600, $"Unlock Inputs First (Press {lockKeyIniString} key)");
                }
            }

            base.WndProc(ref m);
        }

        private void I_GameHandlerEndFunc(string msg, bool stopButton)
        {
           if (StartFromShortcut.I_GameHandler != null)
           {
             StartFromShortcut.I_GameHandler.End(stopButton);
           }
        }

        private void ShortcutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            I_GameHandlerEndFunc("Stop button clicked", true);
        }

        public void Handler_Ended()
        {         
            Close();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            I_GameHandlerEndFunc("Stop button clicked", true);
        }

        private void ShortcutForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Process.GetCurrentProcess().Kill(); 
        }

        public void CloseAbortReason()
        {


        }
    }
}
