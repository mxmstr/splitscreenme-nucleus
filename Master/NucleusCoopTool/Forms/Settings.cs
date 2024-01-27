using NAudio.CoreAudioApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nucleus.Coop.Forms;
using Nucleus.Coop.Tools;
using Nucleus.Gaming;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Controls;
using Nucleus.Gaming.Controls.SetupScreen;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Forms;
using Nucleus.Gaming.Forms.NucleusMessageBox;
using Nucleus.Gaming.Tools.Steam;
using Nucleus.Gaming.Windows.Interop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Nucleus.Coop
{

    public partial class Settings : BaseForm, IDynamicSized
    {
        private MainForm mainForm = null;
        private SetupScreenControl setupScreen;
        public int KillProcess_HotkeyID = 1;
        public int TopMost_HotkeyID = 2;
        public int StopSession_HotkeyID = 3;
        public int SetFocus_HotkeyID = 4;
        public int ResetWindows_HotkeyID = 5;
        public int Cutscenes_HotkeyID = 6;
        public int Switch_HotkeyID = 7;

        private List<string> nicksList = new List<string>();
        private List<string> steamIdsList = new List<string>();

        private string prevTheme;
        private string currentNickname;
        private string currentSteamId;

        private List<Panel> tabs = new List<Panel>();
        private List<Control> tabsButtons = new List<Control>();

        private ComboBox[] controllerNicks;
        private ComboBox[] steamIds;

        public static Button _ctrlr_shorcuts;
        private float fontSize;
        private List<Control> ctrls = new List<Control>();
        private IDictionary<string, string> audioDevices;
        private Cursor hand_Cursor;
        private Cursor default_Cursor;
        private Color selectionColor;

        private Rectangle[] tabBorders;
        private Pen bordersPen;
        private bool shouldSwapNick = true;


        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleparams = base.CreateParams;
                handleparams.ExStyle = 0x02000000;
                return handleparams;
            }
        }

        public void button_Click(object sender, EventArgs e)
        {
            if (mainForm.mouseClick)
                mainForm.SoundPlayer(mainForm.theme + "button_click.wav");
        }

        public Settings(MainForm mf, SetupScreenControl pc)
        {
            fontSize = float.Parse(mf.themeIni.IniReadValue("Font", "SettingsFontSize"));
            mainForm = mf;
            setupScreen = pc;
            InitializeComponent();
         
            FormBorderStyle = FormBorderStyle.None;
            default_Cursor = mf.default_Cursor;
            Cursor = default_Cursor;
            hand_Cursor = mf.hand_Cursor;
            var rgb_selectionColor = mf.themeIni.IniReadValue("Colors", "Selection").Split(',');
            var borderscolor = mf.themeIni.IniReadValue("Colors", "ProfileSettingsBorder").Split(',');
            selectionColor = Color.FromArgb(int.Parse(rgb_selectionColor[0]), int.Parse(rgb_selectionColor[1]), int.Parse(rgb_selectionColor[2]), int.Parse(rgb_selectionColor[3]));
            bordersPen = new Pen(Color.FromArgb(int.Parse(borderscolor[0]), int.Parse(borderscolor[1]), int.Parse(borderscolor[2])));
            BackgroundImage = ImageCache.GetImage(Globals.Theme + "other_backgrounds.jpg");

            _ctrlr_shorcuts = ctrlr_shorcutsBtn;

            controlscollect();

            foreach (Control c in ctrls)
            {
                if (c.GetType() == typeof(CheckBox) || c.GetType() == typeof(Label) || c.GetType() == typeof(RadioButton))
                {
                    if (c.Name != "audioWarningLabel" && c.Name != "warningLabel")
                    {
                        c.Font = new Font(mf.customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }
                }

                if (c.GetType() == typeof(ComboBox) || c.GetType() == typeof(TextBox) || c.GetType() == typeof(GroupBox))
                {
                    c.Font = new Font(mf.customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                }

                if (c.GetType() == typeof(CustomNumericUpDown))
                {
                    c.Font = new Font(mf.customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                }

                if (c.Name != "settingsTab" && c.Name != "playersTab" && c.Name != "audioTab" &&
                    c.Name != "layoutTab" && c.Name != "layoutSizer"
                    && c.GetType() != typeof(Label) && c.GetType() != typeof(TextBox))
                {
                    c.Cursor = hand_Cursor;
                }

                if (c.Name == "settingsTab" || c.Name == "playersTab" || c.Name == "audioTab" || c.Name == "layoutTab")
                {
                    c.BackColor = Color.Transparent;
                    tabs.Add(c as Panel);
                }

                if ((string)c.Tag == "settingsTab" || (string)c.Tag == "playersTab" || (string)c.Tag == "audioTab" || (string)c.Tag == "layoutTab")
                {
                    c.Click += new EventHandler(tabsButtons_highlight);
                    tabsButtons.Add(c);
                }

                if (c.Name.Contains("steamid") && c.GetType() == typeof(ComboBox))
                {
                    c.KeyPress += new KeyPressEventHandler(this.num_KeyPress);
                    c.Click += new System.EventHandler(Steamid_Click);
                }

                if (c is Button)
                {
                    Button isButton = c as Button;

                    if (mf.mouseClick)
                    {
                        isButton.Click += new System.EventHandler(this.button_Click);
                    }

                    isButton.FlatAppearance.BorderSize = 0;
                    isButton.FlatAppearance.MouseOverBackColor = selectionColor;
                }
            }

            ForeColor = Color.FromArgb(int.Parse(mf.rgb_font[0]), int.Parse(mf.rgb_font[1]), int.Parse(mf.rgb_font[2]));

            audioBtnPicture.BackgroundImage = ImageCache.GetImage(mf.theme + "audio.png");
            playersBtnPicture.BackgroundImage = ImageCache.GetImage(mf.theme + "players.png");
            settingsBtnPicture.BackgroundImage = ImageCache.GetImage(mf.theme + "shared.png");
            layoutBtnPicture.BackgroundImage = ImageCache.GetImage(mf.theme + "layout.png");
            closeBtnPicture.BackgroundImage = ImageCache.GetImage(mf.theme + "title_close.png");
            btn_credits.BackgroundImage = ImageCache.GetImage(mf.theme + "credits.png");
            audioRefresh.BackgroundImage = ImageCache.GetImage(mf.theme + "refresh.png");
            btnNext.BackgroundImage = ImageCache.GetImage(mf.theme + "page1.png");

            btn_credits.FlatAppearance.MouseOverBackColor = Color.Transparent;

            plus1.ForeColor = ForeColor;
            plus2.ForeColor = ForeColor;
            plus3.ForeColor = ForeColor;
            plus4.ForeColor = ForeColor;
            plus5.ForeColor = ForeColor;
            plus6.ForeColor = ForeColor;
            plus7.ForeColor = ForeColor;

            audioBtnPicture.Click += new EventHandler(audioBtnPicture_Click);

            audioRefresh.BackColor = Color.Transparent;

            def_sid_comboBox.KeyPress += new KeyPressEventHandler(ReadOnly_KeyPress);
            ctrlr_shorcutsBtn.FlatAppearance.BorderSize = 1;
            btn_Gb_Update.FlatAppearance.BorderSize = 1;

            btn_SteamExePath.ForeColor = ForeColor;
            btn_SteamExePath.Cursor = hand_Cursor;
            btn_SteamExePath.FlatAppearance.BorderSize = 1;

            settingsTab.Parent = this;
            settingsTab.Location = new Point(0, settingsTabBtn.Bottom);
            settingsTab.BringToFront();

            playersTab.Parent = this;
            playersTab.Location = settingsTab.Location;

            audioTab.Parent = this;
            audioTab.Location = settingsTab.Location;

            layoutTab.Parent = this;
            layoutTab.Location = settingsTab.Location;

            page1.Location = new Point(playersTab.Width / 2 - page1.Width / 2, playersTab.Height / 2 - page1.Height / 2);
            page2.Location = page1.Location;
            page1.BringToFront();

            btnNext.Parent = playersTab;
            btnNext.BackColor = mf.buttonsBackColor;
            btnNext.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnNext.Location = new Point(page1.Right - btnNext.Width, (page1.Top - btnNext.Height) - 5);

            default_sid_list_label.Location = new Point(def_sid_comboBox.Left - default_sid_list_label.Width, ((def_sid_comboBox.Location.Y + def_sid_comboBox.Height / 2) - default_sid_list_label.Height / 2) - 4);

            audioRefresh.Location = new Point((audioTab.Width / 2) - (audioRefresh.Width / 2), audioRefresh.Location.Y);

            def_sid_comboBox.SelectedIndex = 0;

            numUpDownVer.MaxValue = 5;
            numUpDownVer.InvalidParent = true;

            numUpDownHor.MaxValue = 5;
            numUpDownHor.InvalidParent = true;

            controllerNicks = new ComboBox[] {
                player1N, player2N, player3N, player4N, player5N, player6N, player7N, player8N,
                player9N, player10N, player11N, player12N, player13N, player14N, player15N, player16N,
                player17N, player18N, player19N, player20N, player21N, player22N, player23N, player24N,
                player25N, player26N, player27N, player28N, player29N, player30N, player31N, player32N};         

            steamIds = new ComboBox[] {
                steamid1, steamid2, steamid3, steamid4, steamid5, steamid6, steamid7, steamid8,
                steamid9, steamid10, steamid11, steamid12, steamid13, steamid14, steamid15, steamid16,
                steamid17, steamid18, steamid19, steamid20, steamid21, steamid22, steamid23, steamid24,
                steamid25, steamid26, steamid27, steamid28, steamid29, steamid30, steamid31, steamid32};

            for (int i = 0; i < 32; i++)
            {
                controllerNicks[i].TextChanged += new EventHandler(SwapNickname);
                controllerNicks[i].KeyPress += new KeyPressEventHandler(CheckTypingNick);

                controllerNicks[i].MouseHover += new EventHandler(CacheNickname);
                controllerNicks[i].LostFocus += new EventHandler(UpdateControllerNickItems);

                steamIds[i].TextChanged += new EventHandler(SwapSteamId);
                steamIds[i].MouseHover += new EventHandler(CacheSteamId);
                steamIds[i].LostFocus += new EventHandler(UpdateSteamIdsItems); 
            }

            splitDiv.Checked = bool.Parse(ini.IniReadValue("CustomLayout", "SplitDiv"));
            hideDesktop.Checked = bool.Parse(ini.IniReadValue("CustomLayout", "HideOnly"));
            cts_Mute.Checked = bool.Parse(ini.IniReadValue("CustomLayout", "Cts_MuteAudioOnly"));
            cts_kar.Checked = bool.Parse(ini.IniReadValue("CustomLayout", "Cts_KeepAspectRatio"));
            cts_unfocus.Checked = bool.Parse(ini.IniReadValue("CustomLayout", "Cts_Unfocus"));

            if(ini.IniReadValue("CustomLayout", "Cts_BringToFront") != "")//such checks are there so testers/users can still use there existing profiles
                                                                       //after new options implementation. Any new option must have that null check.
            {
                cts_bringToFront.Checked = bool.Parse(ini.IniReadValue("CustomLayout", "Cts_BringToFront"));
            }
        
            disableGameProfiles.Checked = bool.Parse(ini.IniReadValue("Misc", "DisableGameProfiles"));
            mf.DisableGameProfiles = disableGameProfiles.Checked;

            if (ini.IniReadValue("CustomLayout", "HorizontalLines") != "")
            {
                numUpDownHor.Value = int.Parse(ini.IniReadValue("CustomLayout", "HorizontalLines"));
            }

            if (ini.IniReadValue("CustomLayout", "VerticalLines") != "")
            {
                numUpDownVer.Value = int.Parse(ini.IniReadValue("CustomLayout", "VerticalLines"));
            }

            if (ini.IniReadValue("CustomLayout", "MaxPlayers") != "")
            {
                numMaxPlyrs.Value = int.Parse(ini.IniReadValue("CustomLayout", "MaxPlayers"));
            }

            gamepadsAssignMethods.Checked = bool.Parse(ini.IniReadValue("Dev", "UseXinputIndex"));
            gamepadsAssignMethods.Visible = !disableGameProfiles.Checked;

            DevicesFunctions.UseGamepadApiIndex = gamepadsAssignMethods.Checked;

            ///network setting
            RefreshCmbNetwork();

            if (ini.IniReadValue("Misc", "Network") != "")
            {
                cmb_Network.Text = ini.IniReadValue("Misc", "Network");
            }
            else
            {
                cmb_Network.SelectedIndex = 0;
            }

            SplitColors.Items.Add("Black");
            SplitColors.Items.Add("Gray");
            SplitColors.Items.Add("White");
            SplitColors.Items.Add("Dark Blue");
            SplitColors.Items.Add("Blue");
            SplitColors.Items.Add("Purple");
            SplitColors.Items.Add("Pink");
            SplitColors.Items.Add("Red");
            SplitColors.Items.Add("Orange");
            SplitColors.Items.Add("Yellow");
            SplitColors.Items.Add("Green");

            SplitColors.SelectedItem = ini.IniReadValue("CustomLayout", "SplitDivColor");

            string[] themeList = Directory.GetDirectories(Path.Combine(Application.StartupPath, @"gui\theme\"));

            foreach (string themePath in themeList)
            {
                string[] _path = themePath.Split('\\');
                int last = _path.Length - 1;

                themeCbx.Items.AddRange(new object[] {
                    _path[last]
                });

                string[] themeName = mf.theme.Split('\\');
                if (_path[last] != themeName[themeName.Length - 2])
                {
                    continue;
                }

                themeCbx.Text = _path[last];
                prevTheme = _path[last];

            }

            ///epiclangs setting
            cmb_EpicLang.SelectedItem = ini.IniReadValue("Misc", "EpicLang");

            ///mouse click sound setting          
            clickSoundChkB.Checked = bool.Parse(ini.IniReadValue("Dev", "MouseClick"));

            useNicksCheck.Checked = bool.Parse(ini.IniReadValue("Misc", "UseNicksInGame"));

            keepAccountsCheck.Checked = bool.Parse(ini.IniReadValue("Misc", "KeepAccounts"));

            ///auto scale setting
            scaleOptionCbx.Checked = bool.Parse(ini.IniReadValue("Misc", "AutoDesktopScaling"));

            ///Custom HotKey setting
            comboBox_lockKey.Text = ini.IniReadValue("Hotkeys", "LockKey");

            if (ini.IniReadValue("Misc", "SteamLang") != "")
            {
                cmb_Lang.Text = ini.IniReadValue("Misc", "SteamLang");
            }
            else
            {
                cmb_Lang.SelectedIndex = 0;
            }

            if (ini.IniReadValue("Hotkeys", "Close").Contains('+'))
            {
                string[] closeHk = ini.IniReadValue("Hotkeys", "Close").Split('+');
                if ((closeHk[0] == "Ctrl" || closeHk[0] == "Alt" || closeHk[0] == "Shift") && closeHk[1].Length == 1 && Regex.IsMatch(closeHk[1], @"^[a-zA-Z0-9]+$"))
                {
                    settingsCloseCmb.SelectedItem = closeHk[0];
                    settingsCloseHKTxt.Text = closeHk[1];
                }
            }
            else
            {
                ini.IniWriteValue("Hotkeys", "Close", "");
            }

            if (ini.IniReadValue("Hotkeys", "Stop").Contains('+'))
            {
                string[] stopHk = ini.IniReadValue("Hotkeys", "Stop").Split('+');
                if ((stopHk[0] == "Ctrl" || stopHk[0] == "Alt" || stopHk[0] == "Shift") && stopHk[1].Length == 1 && Regex.IsMatch(stopHk[1], @"^[a-zA-Z0-9]+$"))
                {
                    settingsStopCmb.SelectedItem = stopHk[0];
                    settingsStopTxt.Text = stopHk[1];
                }
            }
            else
            {
                ini.IniWriteValue("Hotkeys", "Stop", "");
            }

            if (ini.IniReadValue("Hotkeys", "TopMost").Contains('+'))
            {
                string[] topHk = ini.IniReadValue("Hotkeys", "TopMost").Split('+');
                if ((topHk[0] == "Ctrl" || topHk[0] == "Alt" || topHk[0] == "Shift") && topHk[1].Length == 1 && Regex.IsMatch(topHk[1], @"^[a-zA-Z0-9]+$"))
                {
                    settingsTopCmb.SelectedItem = topHk[0];
                    settingsTopTxt.Text = topHk[1];
                }
            }

            if (ini.IniReadValue("Hotkeys", "Stop").Contains('+'))
            {
                string[] stopHk = ini.IniReadValue("Hotkeys", "Stop").Split('+');
                if ((stopHk[0] == "Ctrl" || stopHk[0] == "Alt" || stopHk[0] == "Shift") && stopHk[1].Length == 1 && Regex.IsMatch(stopHk[1], @"^[a-zA-Z0-9]+$"))
                {
                    settingsStopCmb.SelectedItem = stopHk[0];
                    settingsStopTxt.Text = stopHk[1];
                }
            }
            else
            {
                ini.IniWriteValue("Hotkeys", "Stop", "");
            }

            if (ini.IniReadValue("Hotkeys", "SetFocus").Contains('+'))
            {
                string[] foreground = ini.IniReadValue("Hotkeys", "SetFocus").Split('+');
                if ((foreground[0] == "Ctrl" || foreground[0] == "Alt" || foreground[0] == "Shift") && foreground[1].Length == 1 && Regex.IsMatch(foreground[1], @"^[a-zA-Z0-9]+$"))
                {
                    settingsFocusCmb.SelectedItem = foreground[0];
                    settingsFocusHKTxt.Text = foreground[1];
                }
            }
            else
            {
                ini.IniWriteValue("Hotkeys", "SetFocus", "");
            }

            if (ini.IniReadValue("Hotkeys", "ResetWindows").Contains('+'))
            {
                string[] resetWindows = ini.IniReadValue("Hotkeys", "ResetWindows").Split('+');
                if ((resetWindows[0] == "Ctrl" || resetWindows[0] == "Alt" || resetWindows[0] == "Shift") && resetWindows[1].Length == 1 && Regex.IsMatch(resetWindows[1], @"^[a-zA-Z0-9]+$"))
                {
                    r1.SelectedItem = resetWindows[0];
                    r2.Text = resetWindows[1];
                }
            }
            else
            {
                ini.IniWriteValue("Hotkeys", "ResetWindows", "");
            }

            if (ini.IniReadValue("Hotkeys", "Cutscenes").Contains('+'))
            {
                string[] cutscenes = ini.IniReadValue("Hotkeys", "Cutscenes").Split('+');
                if ((cutscenes[0] == "Ctrl" || cutscenes[0] == "Alt" || cutscenes[0] == "Shift") && cutscenes[1].Length == 1 && Regex.IsMatch(cutscenes[1], @"^[a-zA-Z0-9]+$"))
                {
                    csm_comboBox.SelectedItem = cutscenes[0];
                    csm_textBox.Text = cutscenes[1];
                }
            }
            else
            {
                ini.IniWriteValue("Hotkeys", "Cutscenes", "");
            }

            if (ini.IniReadValue("Hotkeys", "Switch").Contains('+'))
            {
                string[] swl = ini.IniReadValue("Hotkeys", "Switch").Split('+');
                if ((swl[0] == "Ctrl" || swl[0] == "Alt" || swl[0] == "Shift") && swl[1].Length == 1 && Regex.IsMatch(swl[1], @"^[a-zA-Z0-9]+$"))
                {
                    swl_comboBox.SelectedItem = swl[0];
                    swl_textBox.Text = swl[1];
                }
            }
            else
            {
                ini.IniWriteValue("Hotkeys", "Switch", "");
            }

            if (ini.IniReadValue("Misc", "IgnoreInputLockReminder") != "")
            {
                ignoreInputLockReminderCheckbox.Checked = bool.Parse(ini.IniReadValue("Misc", "IgnoreInputLockReminder"));
            }

            if (ini.IniReadValue("Misc", "DebugLog") != "")
            {
                debugLogCheck.Checked = bool.Parse(ini.IniReadValue("Misc", "DebugLog"));
            }

            if (ini.IniReadValue("Misc", "ShowStatus") != "")
            {
                statusCheck.Checked = bool.Parse(ini.IniReadValue("Misc", "ShowStatus"));
            }

            if (ini.IniReadValue("Misc", "NucleusAccountPassword") != "")
            {
                nucUserPassTxt.Text = ini.IniReadValue("Misc", "NucleusAccountPassword");
            }

            if (ini.IniReadValue("Audio", "Custom") == "0")
            {
                audioDefaultSettingsRadio.Checked = true;
                audioCustomSettingsBox.Enabled = false;
            }
            else
            {
                audioCustomSettingsRadio.Checked = true;
            }

            showUIInfoMsg.Checked = bool.Parse(ini.IniReadValue("Dev", "ShowToolTips"));

            RefreshAudioList();

            ///network setting
            RefreshCmbNetwork();

            string path = $"{Globals.GameProfilesFolder}\\Nicknames.json";

            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);

                JArray JNicks = (JArray)JsonConvert.DeserializeObject(jsonString);

                foreach (JToken nick in JNicks)
                {
                    NicknamesCache.Add(nick.ToString());
                }
            }

            string idspath = $"{Globals.GameProfilesFolder}\\SteamIds.json";

            if (File.Exists(idspath))
            {
                string jsonString = File.ReadAllText(idspath);

                JArray JIds = (JArray)JsonConvert.DeserializeObject(jsonString);

                foreach (JToken id in JIds)
                {
                    SteamIdsCache.Add(id.ToString());
                }
            }

            GetPlayersNickNameAndSteamIds();

            Rectangle area = Screen.PrimaryScreen.Bounds;
            if (ini.IniReadValue("Misc", "SettingsLocation") != "")
            {
                string[] windowLocation = ini.IniReadValue("Misc", "SettingsLocation").Split('X');

                if (ScreensUtil.AllScreens().All(s => !s.MonitorBounds.Contains(int.Parse(windowLocation[0]), int.Parse(windowLocation[1]))))
                {
                    CenterToScreen();
                }
                else
                {
                    Location = new Point(area.X + int.Parse(windowLocation[0]), area.Y + int.Parse(windowLocation[1]));
                }
            }
            else
            {
                CenterToScreen();
            }

            SetToolTips();

            DPIManager.Register(this);
            DPIManager.Update(this);
        }

        public Settings()
        {
            DPIManager.Unregister(this);
        }

        public void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }

            SuspendLayout();

            float newFontSize = Font.Size * scale;

            foreach (Control c in ctrls)
            {
                if (scale > 1.0F)
                {
                    if (c.GetType() == typeof(ComboBox) || c.GetType() == typeof(TextBox) || c.GetType() == typeof(GroupBox))
                    {
                        c.Font = new Font(c.Font.FontFamily, c.GetType() == typeof(TextBox) ? newFontSize + 3 : newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }
                }

                if (c.GetType() == typeof(Label) && c.GetType() != typeof(CustomNumericUpDown))
                {
                    if (c.Name.Contains("hkLabel"))
                    {
                        c.Location = new Point(settingsFocusCmb.Left - c.Width, c.Location.Y);
                    }
                }
            }

            def_sid_comboBox.Font = new Font(def_sid_comboBox.Font.FontFamily, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);

            audioRefresh.Location = new Point((audioTab.Width / 2) - (audioRefresh.Width / 2), audioRefresh.Location.Y);
            default_sid_list_label.Location = new Point(def_sid_comboBox.Left - default_sid_list_label.Width, ((def_sid_comboBox.Location.Y + def_sid_comboBox.Height / 2) - default_sid_list_label.Height / 2) /*- 4*/);
            audioWarningLabel.Location = new Point(audioTab.Width / 2 - audioWarningLabel.Width / 2, audioWarningLabel.Location.Y);
            gamepadsAssignMethods.Location = new Point((page1.Location.X + label7.Location.X) + 2, (page1.Top - 5) - gamepadsAssignMethods.Height);

            int tabButtonsY = settingsTabBtn.Location.Y - 1;
            int tabButtonsHeight = settingsTabBtn.Height + 1;

            tabBorders = new Rectangle[]
            {
               new Rectangle(0, tabButtonsY,settingsTabBtn.Width + settingsBtnPicture.Width+1, tabButtonsHeight),
               new Rectangle(settingsBtnPicture.Right, tabButtonsY, playersTabBtn.Width + playersBtnPicture.Width + 2, tabButtonsHeight),
               new Rectangle(playersBtnPicture.Right, tabButtonsY,audioTabBtn.Width + audioBtnPicture.Width + 2 , tabButtonsHeight),
               new Rectangle(audioBtnPicture.Right, tabButtonsY ,layoutTabBtn.Width + layoutBtnPicture.Width + 2, tabButtonsHeight),

               new Rectangle(settingsTab.Location.X, settingsTab.Location.Y, settingsTab.Width - 1, settingsTab.Height),
            };

            ResumeLayout();
        }

        private void SetToolTips()
        {
            CustomToolTips.SetToolTip(btn_credits, "Credits", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(splitDiv, "May not work for all games", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(hideDesktop, "Will only show the splitscreen division window without adjusting the game windows size and offset.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(disableGameProfiles, "Disables profiles, Nucleus will use the global settings instead.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(gamepadsAssignMethods, "Can break controller support in some handlers. If enabled profiles\n" +
                                                             "will not save per player gamepad but use XInput indexes instead \n" +
                                                             "(switching modes could prevent some profiles to load properly).\n" +
                                                             "Note: Nucleus will return to the main screen.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
        }

        private void GetPlayersNickNameAndSteamIds()
        {
            for (int i = 0; i < 32; i++)
            {
                nicksList.Add(ini.IniReadValue("ControllerMapping", "Player_" + (i + 1)).ToString());
                steamIdsList.Add(ini.IniReadValue("SteamIDs", "Player_" + (i + 1)).ToString());
            }

            for (int i = 0; i < 32; i++)
            {
                steamIds[i].Items.AddRange(SteamIdsCache.Get.ToArray());
                steamIds[i].SelectedItem = ini.IniReadValue("SteamIDs", "Player_" + (i + 1));
                steamIds[i].Text = ini.IniReadValue("SteamIDs", "Player_" + (i + 1));

                controllerNicks[i].Items.AddRange(NicknamesCache.Get.ToArray());

                foreach(string nick in nicksList)
                {
                    if(!controllerNicks[i].Items.Contains(nick))
                    {
                        controllerNicks[i].Items.Add(nick);
                    }
                }

                controllerNicks[i].SelectedItem = ini.IniReadValue("ControllerMapping", "Player_" + (i + 1));
                controllerNicks[i].Text = ini.IniReadValue("ControllerMapping", "Player_" + (i + 1));
            }
        }

        private void closeBtnPicture_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Globals.GameProfilesFolder))
            {
                Directory.CreateDirectory(Globals.GameProfilesFolder);
            }

            bool sidWrongValue = false;
            bool hasEmptyNickname = false;

            for (int i = 0; i < 32; i++)
            {
                if (controllerNicks[i].Text != "")
                {
                    ini.IniWriteValue("ControllerMapping", "Player_" + (i + 1), controllerNicks[i].Text);

                    if (NicknamesCache.Get.All(n => n != controllerNicks[i].Text))
                    {
                        NicknamesCache.Add(controllerNicks[i].Text);
                    }

                    for (int n = 0; n < 32; n++)
                    {
                        if (controllerNicks[n].Items.Contains(controllerNicks[i].Text))
                        {
                            continue;
                        }

                        controllerNicks[n].Items.Add(controllerNicks[i].Text);
                    }
                }
                else 
                {
                    hasEmptyNickname = true;
                }

                if (Regex.IsMatch(steamIds[i].Text, "^[0-9]+$") && steamIds[i].Text.Length == 17 || steamIds[i].Text.Length == 0)
                {
                    ini.IniWriteValue("SteamIDs", "Player_" + (i + 1), steamIds[i].Text);

                    if (steamIds[i].Text != "")
                    {
                        if (SteamIdsCache.Get.All(n => n != steamIds[i].Text.ToString()))
                        {
                            SteamIdsCache.Add(steamIds[i].Text.ToString());
                        }
                    }
                }
                else
                {
                    steamIds[i].BackColor = Color.Red;
                    sidWrongValue = true;
                }
            }

            if (hasEmptyNickname)
            {
                playersTab.BringToFront();
                MessageBox.Show("Nickname fields can't be empty!");
                return;
            }

            if (sidWrongValue)
            {
                playersTab.BringToFront();
                MessageBox.Show("Must be 17 numbers e.g. 76561199075562883 ", "Incorrect steam id format!");
                return;
            }

            string path = $"{Globals.GameProfilesFolder}\\Nicknames.json";
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string json = JsonConvert.SerializeObject(NicknamesCache.Get, Formatting.Indented);
                    writer.Write(json);
                    stream.Flush();
                }

                stream.Dispose();
            }

            string idspath = $"{Globals.GameProfilesFolder}\\SteamIds.json";
            using (FileStream stream = new FileStream(idspath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string json = JsonConvert.SerializeObject(SteamIdsCache.Get, Formatting.Indented);
                    writer.Write(json);
                    stream.Flush();
                }

                stream.Dispose();
            }

            if (audioDefaultSettingsRadio.Checked)
            {
                ini.IniWriteValue("Audio", "Custom", 0.ToString());
            }
            else
            {
                ini.IniWriteValue("Audio", "Custom", 1.ToString());
            }

            foreach (Control ctrl in audioCustomSettingsBox.Controls)
            {
                if (ctrl is ComboBox)
                {
                    ComboBox cmb = (ComboBox)ctrl;
                    if (audioDevices?.Count > 0 && audioDevices.Keys.Contains(cmb.Text))
                    {
                        ini.IniWriteValue("Audio", cmb.Name, audioDevices[cmb.Text]);
                    }
                }
            }

            foreach (Control ht in hotkeyBox.Controls)
            {
                if (ht.GetType() == typeof(TextBox))
                {
                    if (ht.Text == "")
                    {
                        MessageBox.Show("Hotkeys values can't be empty", "Invalid hotkeys values!");
                        return;
                    }
                }
            }

            ini.IniWriteValue("Hotkeys", "Close", settingsCloseCmb.SelectedItem.ToString() + "+" + settingsCloseHKTxt.Text);
            ini.IniWriteValue("Hotkeys", "Stop", settingsStopCmb.SelectedItem.ToString() + "+" + settingsStopTxt.Text);
            ini.IniWriteValue("Hotkeys", "TopMost", settingsTopCmb.SelectedItem.ToString() + "+" + settingsTopTxt.Text);
            ini.IniWriteValue("Hotkeys", "SetFocus", settingsFocusCmb.SelectedItem.ToString() + "+" + settingsFocusHKTxt.Text);
            ini.IniWriteValue("Hotkeys", "ResetWindows", r1.SelectedItem.ToString() + "+" + r2.Text);
            ini.IniWriteValue("Hotkeys", "LockKey", comboBox_lockKey.SelectedItem.ToString());
            ini.IniWriteValue("Hotkeys", "Cutscenes", csm_comboBox.SelectedItem.ToString() + "+" + csm_textBox.Text);
            ini.IniWriteValue("Hotkeys", "Switch", swl_comboBox.SelectedItem.ToString() + "+" + swl_textBox.Text);

            User32Interop.UnregisterHotKey(mainForm.Handle, KillProcess_HotkeyID);
            User32Interop.UnregisterHotKey(mainForm.Handle, TopMost_HotkeyID);
            User32Interop.UnregisterHotKey(mainForm.Handle, StopSession_HotkeyID);
            User32Interop.UnregisterHotKey(mainForm.Handle, SetFocus_HotkeyID);
            User32Interop.UnregisterHotKey(mainForm.Handle, ResetWindows_HotkeyID);
            User32Interop.UnregisterHotKey(mainForm.Handle, Cutscenes_HotkeyID);
            User32Interop.UnregisterHotKey(mainForm.Handle, Switch_HotkeyID);

            User32Interop.RegisterHotKey(mainForm.Handle, KillProcess_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "Close").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "Close").Split('+')[1].ToString()));
            User32Interop.RegisterHotKey(mainForm.Handle, TopMost_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "TopMost").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "TopMost").Split('+')[1].ToString()));
            User32Interop.RegisterHotKey(mainForm.Handle, StopSession_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "Stop").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "Stop").Split('+')[1].ToString()));
            User32Interop.RegisterHotKey(mainForm.Handle, SetFocus_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "SetFocus").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "SetFocus").Split('+')[1].ToString()));
            User32Interop.RegisterHotKey(mainForm.Handle, ResetWindows_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "ResetWindows").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "ResetWindows").Split('+')[1].ToString()));
            User32Interop.RegisterHotKey(mainForm.Handle, Cutscenes_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "Cutscenes").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "Cutscenes").Split('+')[1].ToString()));
            User32Interop.RegisterHotKey(mainForm.Handle, Switch_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "Switch").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "Switch").Split('+')[1].ToString()));

            ini.IniWriteValue("Misc", "UseNicksInGame", useNicksCheck.Checked.ToString());
            ini.IniWriteValue("Misc", "KeepAccounts", keepAccountsCheck.Checked.ToString());
            ini.IniWriteValue("Misc", "Network", cmb_Network.Text.ToString());
            ini.IniWriteValue("Misc", "IgnoreInputLockReminder", ignoreInputLockReminderCheckbox.Checked.ToString());
            ini.IniWriteValue("Misc", "DebugLog", debugLogCheck.Checked.ToString());
            ini.IniWriteValue("Misc", "SteamLang", cmb_Lang.SelectedItem.ToString());
            ini.IniWriteValue("Misc", "EpicLang", cmb_EpicLang.SelectedItem.ToString());
            ini.IniWriteValue("Misc", "ShowStatus", statusCheck.Checked.ToString());
            ini.IniWriteValue("Misc", "NucleusAccountPassword", nucUserPassTxt.Text);
            ini.IniWriteValue("Misc", "AutoDesktopScaling", scaleOptionCbx.Checked.ToString());

            ini.IniWriteValue("Dev", "MouseClick", clickSoundChkB.Checked.ToString());
            ini.IniWriteValue("Dev", "MouseClick", clickSoundChkB.Checked.ToString());
            ini.IniWriteValue("Dev", "UseXinputIndex", gamepadsAssignMethods.Checked.ToString());

            ini.IniWriteValue("CustomLayout", "SplitDiv", splitDiv.Checked.ToString());
            ini.IniWriteValue("CustomLayout", "HideOnly", hideDesktop.Checked.ToString());
            ini.IniWriteValue("CustomLayout", "SplitDivColor", SplitColors.Text.ToString());
            ini.IniWriteValue("CustomLayout", "HorizontalLines", numUpDownHor.Value.ToString());
            ini.IniWriteValue("CustomLayout", "VerticalLines", numUpDownVer.Value.ToString());
            ini.IniWriteValue("CustomLayout", "MaxPlayers", numMaxPlyrs.Value.ToString());

            ini.IniWriteValue("CustomLayout", "Cts_MuteAudioOnly", cts_Mute.Checked.ToString());
            ini.IniWriteValue("CustomLayout", "Cts_KeepAspectRatio", cts_kar.Checked.ToString());
            ini.IniWriteValue("CustomLayout", "Cts_Unfocus", cts_unfocus.Checked.ToString());
            ini.IniWriteValue("CustomLayout", "Cts_BringToFront", cts_bringToFront.Checked.ToString());

            mainForm.HandleClickSound(clickSoundChkB.Checked);

            mainForm.lockKeyIniString = comboBox_lockKey.SelectedItem.ToString();
            mainForm.DebugButtonState(debugLogCheck.Checked);

            ini.IniWriteValue("Dev", "ShowToolTips", showUIInfoMsg.Checked.ToString());

            if (setupScreen != null)
            {
                if (DevicesFunctions.UseGamepadApiIndex != gamepadsAssignMethods.Checked)
                {
                    DevicesFunctions.UseGamepadApiIndex = gamepadsAssignMethods.Checked;
                    mainForm.RefreshUI(true);
                }
            }

            bool disableGameProfileschanged = disableGameProfiles.Checked != bool.Parse(ini.IniReadValue("Misc", "DisableGameProfiles"));

            if (disableGameProfileschanged)
            {
                ini.IniWriteValue("Misc", "DisableGameProfiles", disableGameProfiles.Checked.ToString());
                mainForm.DisableGameProfiles = disableGameProfiles.Checked;
            }

            bool needToRestart = false;

            if (themeCbx.SelectedItem.ToString() != prevTheme)
            {
                ini.IniWriteValue("Theme", "Theme", themeCbx.SelectedItem.ToString());
                mainForm.restartRequired = true;
                needToRestart = true;
            }

            if (GameProfile.ModeText == "New Profile" || disableGameProfileschanged)
            {
                if (GameProfile._GameProfile != null)
                {
                    GameProfile._GameProfile.Reset();
                    ProfileSettings.ProfileRefreshAudioList();
                }
            }

            if (mainForm.Xinput_S_Setup.Visible)
            {
                mainForm.Xinput_S_Setup.Visible = false;
            }

            Visible = false;

            if (Location.X == -32000 || Width == 0)
            {
                return;
            }       

            Globals.MainOSD.Show(500, "Settings saved");

            if (needToRestart)
            {
                ini.IniWriteValue("Misc", "SettingsLocation", Location.X + "X" + Location.Y);
                Thread.Sleep(300);
                Application.Restart();
                Process.GetCurrentProcess().Kill();
            }

            ini.IniWriteValue("Misc", "SettingsLocation", Location.X + "X" + Location.Y);
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

        public void RegHotkeys(MainForm form)
        {
            mainForm = form;

            try
            {
                User32Interop.RegisterHotKey(form.Handle, KillProcess_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "Close").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "Close").Split('+')[1].ToString()));
                User32Interop.RegisterHotKey(form.Handle, TopMost_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "TopMost").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "TopMost").Split('+')[1].ToString()));
                User32Interop.RegisterHotKey(form.Handle, StopSession_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "Stop").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "Stop").Split('+')[1].ToString()));
                User32Interop.RegisterHotKey(form.Handle, SetFocus_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "SetFocus").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "SetFocus").Split('+')[1].ToString()));
                User32Interop.RegisterHotKey(form.Handle, ResetWindows_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "ResetWindows").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "ResetWindows").Split('+')[1].ToString()));
                User32Interop.RegisterHotKey(form.Handle, Cutscenes_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "Cutscenes").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "Cutscenes").Split('+')[1].ToString()));
                User32Interop.RegisterHotKey(form.Handle, Switch_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "Switch").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "Switch").Split('+')[1].ToString()));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error registering hotkeys " + ex.Message, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RegHotkeys(int id, int mod, int key)
        {
            User32Interop.RegisterHotKey(mainForm.Handle, id, mod, key);
        }

        private void Steamid_Click(object sender, EventArgs e)
        {
            ComboBox id = (ComboBox)sender;
            id.BackColor = Color.White;
        }

        private void cmb_Network_DropDown(object sender, EventArgs e)
        {
            RefreshCmbNetwork();
        }

        private void RefreshCmbNetwork()
        {
            cmb_Network.Items.Clear();

            cmb_Network.Items.Add("Automatic");

            NetworkInterface[] ni = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface item in ni)
            {
                if (item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            cmb_Network.Items.Add(item.Name);
                        }
                    }
                }
            }
        }

        private void cmb_Network_DropDownClosed(object sender, EventArgs e)
        {
            if (cmb_Network.SelectedItem == null)
            {
                cmb_Network.SelectedIndex = 0;
            }
        }

        private void audioCustomSettingsRadio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            audioCustomSettingsBox.Enabled = radio.Checked;
        }

        private void RefreshAudioList()
        {
            audioDevices = new Dictionary<string, string>();
            audioDevices.Clear();

            if (!audioDevices.ContainsKey("Default"))
            {
                audioDevices.Add("Default", "Default");
            }

            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();

            foreach (MMDevice endpoint in enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active))
            {
                if (!audioDevices.ContainsKey(endpoint.FriendlyName))
                {
                    audioDevices.Add(endpoint.FriendlyName, endpoint.ID);
                }
            }

            foreach (Control ctrl in audioCustomSettingsBox.Controls)
            {
                if (ctrl is ComboBox)
                {
                    ComboBox cmb = (ComboBox)ctrl;
                    string lastItem = cmb.Text;
                    cmb.Items.Clear();
                    cmb.Items.AddRange(audioDevices.Keys.ToArray());

                    if (cmb.Items.Contains(lastItem))
                    {
                        cmb.SelectedItem = lastItem;
                    }
                    else if (audioDevices.Values.Contains(ini.IniReadValue("Audio", cmb.Name)))
                    {
                        cmb.SelectedItem = audioDevices.FirstOrDefault(x => x.Value == ini.IniReadValue("Audio", cmb.Name)).Key;
                    }
                    else
                    {
                        cmb.SelectedItem = "Default";
                    }
                }
            }
        }

        private void audioRefresh_Click(object sender, EventArgs e)
        {
            RefreshAudioList();
        }

        private void tabsButtons_highlight(object sender, EventArgs e)
        {
            Control c = sender as Control;

            for (int i = 0; i < tabsButtons.Count; i++)
            {
                if (i < tabs.Count)
                {
                    if (tabs[i].Name != (string)c.Tag)
                    {
                        tabs[i].Visible = false;
                    }
                    else
                    {
                        tabs[i].Visible = true;
                        tabs[i].BringToFront();
                    }
                }

                if (tabsButtons[i].GetType() != typeof(Button))
                {
                    continue;
                }

                if (tabsButtons[i].Tag != c.Tag)
                {
                    tabsButtons[i].BackColor = Color.Transparent;
                }
                else
                {
                    tabsButtons[i].BackColor = selectionColor;
                }
            }
        }

        private void audioBtnPicture_Click(object sender, EventArgs e)
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDevice audioDefault = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            audioDefaultDevice.Text = "Default: " + audioDefault.FriendlyName;
        }

        private void closeBtnPicture_MouseEnter(object sender, EventArgs e)
        {
            closeBtnPicture.BackgroundImage = ImageCache.GetImage(mainForm.theme + "title_close_mousehover.png");
        }

        private void closeBtnPicture_MouseLeave(object sender, EventArgs e)
        {
            closeBtnPicture.BackgroundImage = ImageCache.GetImage(mainForm.theme + "title_close.png");
        }

        private void btn_credits_MouseEnter(object sender, EventArgs e)
        {
            btn_credits.BackgroundImage = ImageCache.GetImage(mainForm.theme + "credits_mousehover.png");
        }

        private void btn_credits_MouseLeave(object sender, EventArgs e)
        {
            btn_credits.BackgroundImage = ImageCache.GetImage(mainForm.theme + "credits.png");
        }

        private void ctrlr_shorcuts_Click(object sender, EventArgs e)
        {
            if (!mainForm.Xinput_S_Setup.Visible)
            {
                mainForm.Xinput_S_Setup?.Show();
            }
            else
            {
                mainForm.Xinput_S_Setup?.BringToFront();
            }
        }

        private void keepAccountsCheck_Click(object sender, EventArgs e)
        {
            if (!keepAccountsCheck.Checked)
            {
                DialogResult res = MessageBox.Show("Warning: by disabling this feature, the next time you run a game that uses different user accounts, all Nucleus-made user accounts (and their files, saves) will be deleted. Do you wish to proceed?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (res != DialogResult.OK)
                {
                    keepAccountsCheck.Checked = true;
                }
            }
        }

        private void num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void ReadOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void settingsCloseHKTxt_TextChanged(object sender, EventArgs e)
        {
            settingsCloseHKTxt.Text = string.Concat(settingsCloseHKTxt.Text.Where(char.IsLetterOrDigit));
        }

        private void settingsCloseHKTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            settingsCloseHKTxt.Text = "";
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void settingsStopTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            settingsStopTxt.Text = "";
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void settingsTopTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            settingsTopTxt.Text = "";
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void settingsFocusHKTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            settingsTopTxt.Text = "";
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cts_Mute_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox mute = (CheckBox)sender;

            if (mute.Checked)
            {
                cts_kar.Checked = false;
                cts_kar.Enabled = false;
                cts_unfocus.Checked = false;
                cts_unfocus.Enabled = false;
                cts_bringToFront.Checked = false;
                cts_bringToFront.Enabled = false;
            }
            else
            {
                cts_kar.Enabled = true;
                cts_unfocus.Enabled = true;
                cts_bringToFront.Enabled = true;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (page1.Visible)
            {
                SuspendLayout();
                btnNext.BackgroundImage = ImageCache.GetImage(Globals.Theme + "page2.png");
                page1.Visible = false;
                page2.BringToFront();
                page2.Visible = true;
                ResumeLayout();
            }
            else
            {
                SuspendLayout();
                btnNext.BackgroundImage = ImageCache.GetImage(Globals.Theme + "page1.png");
                page2.Visible = false;
                page1.BringToFront();
                page1.Visible = true;
                ResumeLayout();
            }
        }

        private void controlscollect()
        {
            foreach (Control control in Controls)
            {
                ctrls.Add(control);
                foreach (Control container1 in control.Controls)
                {
                    ctrls.Add(container1);
                    foreach (Control container2 in container1.Controls)
                    {
                        ctrls.Add(container2);
                        foreach (Control container3 in container2.Controls)
                        {
                            ctrls.Add(container3);
                            foreach (Control container4 in container3.Controls)
                            {
                                ctrls.Add(container4);
                            }
                        }
                    }
                }
            }
        }

        private void btn_credits_Click(object sender, EventArgs e)
        {
           string text = 
           "Nucleus Co-op - " + mainForm.version +
           "\n " +
           "\nOriginal Nucleus Co-op Project: Lucas Assis(lucasassislar)" +
           "\nNew Nucleus Co-op fork: ZeroFox" +
           "\nMultiple keyboards / mice & hooks: Ilyaki" +
           "\nWebsite & handler API: r - mach" +
           "\nNew UI design, bug fixes, per game profiles and gamepad UI control/shortcuts support : Mikou27(nene27)" +
           "\nHandlers development & testing: Talos91, PoundlandBacon, Pizzo, dr.oldboi and many more." +
           "\nThis new & improved Nucleus Co-op brings a ton of enhancements, such as:" +
           "\n-Massive increase to the amount of compatible games, 400 + as of now." +
           "\n-Beautiful new overhauled user interface with support for themes, game covers & screenshots." +
           "\n-Support for per-game profiles." +
           "\n-Many quality of life improvements & bug fixes." +
           "\n-And so much more!\n" +
           "\nSpecial thanks to: Talos91, dr.oldboi, PoundlandBacon, Pizzo and the rest of the Splitscreen Dreams discord community.";
            NucleusMessageBox.Show("Credits", text,false);
        }
      
        private void layoutSizer_Paint(object sender, PaintEventArgs e)
        {
            Graphics gs = e.Graphics;

            int LayoutHeight = layoutSizer.Size.Height - 20;
            int LayoutWidth = layoutSizer.Size.Width - 20;

            Rectangle outline = new Rectangle(10, 10, LayoutWidth, LayoutHeight);

            gs.DrawRectangle(bordersPen, outline);

            int[] hlines = new int[(int)numUpDownHor.Value];
            int[] vlines = new int[(int)numUpDownVer.Value];

            for (int i = 0; i < (int)numUpDownHor.Value; i++)
            {
                int divisions = (int)numUpDownHor.Value + 1;

                int y = (LayoutHeight / divisions);
                if (i == 0)
                {
                    hlines[i] = y + 10;
                }
                else
                {
                    hlines[i] = y + hlines[i - 1];
                }
                gs.DrawLine(bordersPen, 10, hlines[i], 10 + LayoutWidth, hlines[i]);
            }

            for (int i = 0; i < (int)numUpDownVer.Value; i++)
            {

                int divisions = (int)numUpDownVer.Value + 1;

                int x = (LayoutWidth / divisions);
                if (i == 0)
                {
                    vlines[i] = x + 10;
                }
                else
                {
                    vlines[i] = x + vlines[i - 1];
                }
                gs.DrawLine(bordersPen, vlines[i], 10, vlines[i], 10 + LayoutHeight);
            }

            numMaxPlyrs.Value = (numUpDownHor.Value + 1) * (numUpDownVer.Value + 1);
            gs.Dispose();
        }

        private void Settings_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawRectangles(bordersPen, tabBorders);
        }

        private void btn_Gb_Update_Click(object sender, EventArgs e)
        {
            GoldbergUpdaterForm gbUpdater = new GoldbergUpdaterForm();
            gbUpdater.ShowDialog();
        }

        private void disableGameProfiles_CheckedChanged(object sender, EventArgs e)
        {
            gamepadsAssignMethods.Visible = !disableGameProfiles.Checked;
        }

        private void CacheNickname(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            currentNickname = cb.Text;
            shouldSwapNick = true;
        }

        private void SwapNickname(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if (cb.Text == "" || !shouldSwapNick)
            {
                return;
            }

            ComboBox ch = controllerNicks.Where(tb => tb.Text == cb.Text && tb != cb).FirstOrDefault();

            if (ch != null)
            {
                ch.Text = currentNickname;
                currentNickname = cb.Text;
            }
        }

        private void CheckTypingNick(object sender, KeyPressEventArgs e)
        {
            shouldSwapNick = false;
        }

        private void CacheSteamId(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            currentSteamId = cb.Text;
        }

        private void SwapSteamId(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if (cb.Text == "")
            {
                return;
            }

            ComboBox ch = steamIds.Where(tb => tb.Text == cb.Text && tb != cb).FirstOrDefault();

            if (ch != null)
            {
                ch.Text = currentSteamId;
                currentSteamId = cb.Text;
            }
        }

        private void UpdateControllerNickItems(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            
            if(cb.Text == "")
            {
                return;
            }

            if(!NicknamesCache.Get.Any(n => n == cb.Text))
            {
                NicknamesCache.Add(cb.Text);
            }

            for (int i = 0; i < 32; i++)
            {
                if(!controllerNicks[i].Items.Contains(cb.Text))
                {
                    controllerNicks[i].Items.Add(cb.Text);
                }
            }
        }

        private void UpdateSteamIdsItems(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;

            if (cb.Text == "")
            {
                return;
            }

            if (!SteamIdsCache.Get.Any(n => n == cb.Text))
            {
                SteamIdsCache.Add(cb.Text);
            }

            for (int i = 0; i < 32; i++)
            {
                if (!steamIds[i].Items.Contains(cb.Text))
                {
                    steamIds[i].Items.Add(cb.Text);
                }
            }
        }

        private void Settings_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                for (int i = 0; i < 32; i++)
                {
                    foreach (string nick in NicknamesCache.Get)
                    {
                        if (!controllerNicks[i].Items.Contains(nick))
                        {
                            controllerNicks[i].Items.Add(nick);
                        }
                    }

                    foreach (string sid in SteamIdsCache.Get)
                    {
                        if (!steamIds[i].Items.Contains(sid))
                        {
                            steamIds[i].Items.Add(sid);
                        }
                    }
                }

                //Update in case the logmanager prompt updated the value
                debugLogCheck.Checked = bool.Parse(ini.IniReadValue("Misc", "DebugLog"));            
            }
        }


        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        private void Settings_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                User32Interop.ReleaseCapture();
                IntPtr nucHwnd = User32Interop.FindWindow(null, Text);
                User32Interop.SendMessage(nucHwnd, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, (IntPtr)0);
            }
        }

        private void btn_SteamExePath_Click(object sender, EventArgs e)
        {
            using (System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog())
            {

                open.Title = "Select the steam client executable(steam.exe)";
                open.Filter = "Steam Client Exe|*steam.exe";

                if (open.ShowDialog() == DialogResult.OK)
                {
                    string path = open.FileName;

                    Globals.ini.IniWriteValue("SearchPaths", "SteamClientExePath", open.FileName);
                }
            }
        }
    }
}