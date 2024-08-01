using NAudio.CoreAudioApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nucleus.Gaming;
using Nucleus.Gaming.App.Settings;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Controls;
using Nucleus.Gaming.Controls.SetupScreen;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.InputManagement;
using Nucleus.Gaming.Tools.MonitorsDpiScaling;
using Nucleus.Gaming.Tools.Steam;
using Nucleus.Gaming.UI;
using Nucleus.Gaming.Windows.Interop;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;


namespace Nucleus.Coop
{

    public partial class Settings : Form, IDynamicSized
    {
        private MainForm mainForm = null;
        private SetupScreenControl setupScreen;

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

        public Settings(MainForm mf, SetupScreenControl pc)
        {
            fontSize = float.Parse(Globals.ThemeConfigFile.IniReadValue("Font", "SettingsFontSize"));
            mainForm = mf;
            setupScreen = pc;

            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;

            default_Cursor = Theme_Settings.Default_Cursor;
            hand_Cursor = Theme_Settings.Hand_Cursor;
            Cursor = default_Cursor;

            var borderscolor = Globals.ThemeConfigFile.IniReadValue("Colors", "ProfileSettingsBorder").Split(',');
            selectionColor = Theme_Settings.SelectedBackColor;
            bordersPen = new Pen(Color.FromArgb(int.Parse(borderscolor[0]), int.Parse(borderscolor[1]), int.Parse(borderscolor[2])));
            BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "other_backgrounds.jpg");

            _ctrlr_shorcuts = ctrlr_shorcutsBtn;

            Controlscollect();

            foreach (Control c in ctrls)
            {
                if ((string)c.Tag == "HotKeyTextBox")
                {
                    c.KeyPress += HKTxt_KeyPress;
                    c.TextChanged += HKTxt_TextChanged;
                }

                if (c is CheckBox || c is Label || c is RadioButton)
                {
                    if (c.Name != "audioWarningLabel" && c.Name != "warningLabel")
                    {
                        c.Font = new Font(mf.customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }
                }

                if (c is ComboBox || c is TextBox || c is GroupBox)
                {
                    c.Font = new Font(mf.customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                }

                if (c is CustomNumericUpDown)
                {
                    c.Font = new Font(mf.customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                }

                if (c.Name != "settingsTab" && c.Name != "playersTab" && c.Name != "audioTab" &&
                    c.Name != "layoutTab" && c.Name != "layoutSizer"
                    && !(c is Label) && !(c is TextBox))
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
                    c.Click += new EventHandler(TabsButtons_highlight);
                    tabsButtons.Add(c);
                }

                if (c.Name.Contains("steamid") && c is ComboBox)
                {
                    c.KeyPress += new KeyPressEventHandler(this.Num_KeyPress);
                    c.Click += new System.EventHandler(Steamid_Click);
                }

                if (c is Button)
                {
                    Button isButton = c as Button;
                    isButton.FlatAppearance.BorderSize = 0;
                    isButton.FlatAppearance.MouseOverBackColor = selectionColor;
                }

                if (c.Parent.Name == "hotkeyBox")
                {
                    c.Font = new Font(mf.customFont, fontSize, FontStyle.Bold, GraphicsUnit.Pixel, 0);
                }
            }

            ForeColor = Color.FromArgb(int.Parse(mf.rgb_font[0]), int.Parse(mf.rgb_font[1]), int.Parse(mf.rgb_font[2]));

            audioBtnPicture.BackgroundImage = ImageCache.GetImage(mf.theme + "audio.png");
            playersBtnPicture.BackgroundImage = ImageCache.GetImage(mf.theme + "players.png");
            settingsBtnPicture.BackgroundImage = ImageCache.GetImage(mf.theme + "shared.png");
            layoutBtnPicture.BackgroundImage = ImageCache.GetImage(mf.theme + "layout.png");
            closeBtnPicture.BackgroundImage = ImageCache.GetImage(mf.theme + "title_close.png");
            audioRefresh.BackgroundImage = ImageCache.GetImage(mf.theme + "refresh.png");
            btnNext.BackgroundImage = ImageCache.GetImage(mf.theme + "page1.png");
            refreshScreenDatasButton.BackgroundImage = ImageCache.GetImage(mf.theme + "refresh.png");

            plus1.ForeColor = ForeColor;
            plus2.ForeColor = ForeColor;
            plus3.ForeColor = ForeColor;
            plus4.ForeColor = ForeColor;
            plus5.ForeColor = ForeColor;
            plus6.ForeColor = ForeColor;
            plus7.ForeColor = ForeColor;
            plus8.ForeColor = ForeColor;
            plus9.ForeColor = ForeColor;

            audioBtnPicture.Click += new EventHandler(AudioBtnPicture_Click);

            audioRefresh.BackColor = Color.Transparent;

            def_sid_comboBox.KeyPress += new KeyPressEventHandler(ReadOnly_KeyPress);
            ctrlr_shorcutsBtn.FlatAppearance.BorderSize = 1;
            btn_Gb_Update.FlatAppearance.BorderSize = 1;

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

            for (int i = 0; i < Globals.NucleusMaxPlayers; i++)
            {
                var nickField = controllerNicks[i];
                nickField.TextChanged += new EventHandler(SwapNickname);
                nickField.KeyPress += new KeyPressEventHandler(CheckTypingNick);
                nickField.MouseHover += new EventHandler(CacheNickname);
                nickField.LostFocus += new EventHandler(UpdateControllerNickItems);

                var sidField = steamIds[i];
                sidField.TextChanged += new EventHandler(SwapSteamId);
                sidField.MouseHover += new EventHandler(CacheSteamId);
                sidField.LostFocus += new EventHandler(UpdateSteamIdsItems);
            }

            splitDiv.Checked = App_Layouts.SplitDiv;
            hideDesktop.Checked = App_Layouts.HideOnly;
            cts_Mute.Checked = App_Layouts.Cts_MuteAudioOnly;
            cts_kar.Checked = App_Layouts.Cts_KeepAspectRatio;
            cts_unfocus.Checked = App_Layouts.Cts_Unfocus;

            enable_WMerger.Checked = App_Layouts.WindowsMerger;
            losslessHook.Checked = App_Layouts.LosslessHook;

             numUpDownHor.Value = App_Layouts.HorizontalLines;           
             numUpDownVer.Value = App_Layouts.VerticalLines;
             numMaxPlyrs.Value =    App_Layouts.MaxPlayers;
            

            disableGameProfiles.Checked = App_Misc.DisableGameProfiles;
            gamepadsAssignMethods.Checked = App_Misc.UseXinputIndex;
            gamepadsAssignMethods.Visible = !disableGameProfiles.Checked;

            DevicesFunctions.UseGamepadApiIndex = gamepadsAssignMethods.Checked;

            ///network setting
            RefreshCmbNetwork();

            if (App_Misc.Network != "")
            {
                cmb_Network.Text = App_Misc.Network;
                cmb_Network.SelectedIndex = cmb_Network.Items.IndexOf(App_Misc.Network);
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

            SplitColors.SelectedItem = App_Layouts.SplitDivColor;

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
            cmb_EpicLang.SelectedItem = App_Misc.EpicLang;

            useNicksCheck.Checked = App_Misc.UseNicksInGame;

            keepAccountsCheck.Checked = App_Misc.KeepAccounts;

            ///auto scale setting
            scaleOptionCbx.Checked = App_Misc.AutoDesktopScaling;

            ///Custom HotKey setting
            lockKey_Cmb.Text = App_Hotkeys.LockInputs;

            if (App_Misc.SteamLang != "")
            {
                cmb_Lang.Text = App_Misc.SteamLang;
            }
            else
            {
                cmb_Lang.SelectedIndex = 0;
            }

            if ((App_Hotkeys.CloseApp.Item1 == "Ctrl" || App_Hotkeys.CloseApp.Item1 == "Alt" || App_Hotkeys.CloseApp.Item1 == "Shift") && App_Hotkeys.CloseApp.Item2.Length == 1 && Regex.IsMatch(App_Hotkeys.CloseApp.Item2, @"^[a-zA-Z0-9]+$"))
            {
                cn_Cmb.SelectedItem = App_Hotkeys.CloseApp.Item1;
                cn_HKTxt.Text = App_Hotkeys.CloseApp.Item2;
            }

            if ((App_Hotkeys.StopSession.Item1 == "Ctrl" || App_Hotkeys.StopSession.Item1 == "Alt" || App_Hotkeys.StopSession.Item1 == "Shift") && App_Hotkeys.StopSession.Item2.Length == 1 && Regex.IsMatch(App_Hotkeys.StopSession.Item2, @"^[a-zA-Z0-9]+$"))
            {
                ss_Cmb.SelectedItem = App_Hotkeys.StopSession.Item1;
                ss_HKTxt.Text = App_Hotkeys.StopSession.Item2;
            }

            if ((App_Hotkeys.TopMost.Item1 == "Ctrl" || App_Hotkeys.TopMost.Item1 == "Alt" || App_Hotkeys.TopMost.Item1 == "Shift") && App_Hotkeys.TopMost.Item2.Length == 1 && Regex.IsMatch(App_Hotkeys.TopMost.Item2, @"^[a-zA-Z0-9]+$"))
            {
                ttm_Cmb.SelectedItem = App_Hotkeys.TopMost.Item1;
                ttm_HKTxt.Text = App_Hotkeys.TopMost.Item2;
            }

            if ((App_Hotkeys.SetFocus.Item1 == "Ctrl" || App_Hotkeys.SetFocus.Item1 == "Alt" || App_Hotkeys.SetFocus.Item1 == "Shift") && App_Hotkeys.SetFocus.Item2.Length == 1 && Regex.IsMatch(App_Hotkeys.SetFocus.Item2, @"^[a-zA-Z0-9]+$"))
            {
                tu_Cmb.SelectedItem = App_Hotkeys.SetFocus.Item1;
                tu_HKTxt.Text = App_Hotkeys.SetFocus.Item2;
            }

            if ((App_Hotkeys.ResetWindows.Item1 == "Ctrl" || App_Hotkeys.ResetWindows.Item1 == "Alt" || App_Hotkeys.ResetWindows.Item1 == "Shift") && App_Hotkeys.ResetWindows.Item2.Length == 1 && Regex.IsMatch(App_Hotkeys.ResetWindows.Item2, @"^[a-zA-Z0-9]+$"))
            {
                rw_Cmb.SelectedItem = App_Hotkeys.ResetWindows.Item1;
                rw_HKTxt.Text = App_Hotkeys.ResetWindows.Item2;
            }

            if ((App_Hotkeys.CutscenesMode.Item1 == "Ctrl" || App_Hotkeys.CutscenesMode.Item1 == "Alt" || App_Hotkeys.CutscenesMode.Item1 == "Shift") && App_Hotkeys.CutscenesMode.Item2.Length == 1 && Regex.IsMatch(App_Hotkeys.CutscenesMode.Item2, @"^[a-zA-Z0-9]+$"))
            {
                csm_Cmb.SelectedItem = App_Hotkeys.CutscenesMode.Item1;
                csm_HKTxt.Text = App_Hotkeys.CutscenesMode.Item2;
            }

            if ((App_Hotkeys.SwitchLayout.Item1 == "Ctrl" || App_Hotkeys.SwitchLayout.Item1 == "Alt" || App_Hotkeys.SwitchLayout.Item1 == "Shift") && App_Hotkeys.SwitchLayout.Item2.Length == 1 && Regex.IsMatch(App_Hotkeys.SwitchLayout.Item2, @"^[a-zA-Z0-9]+$"))
            {
                swl_Cmb.SelectedItem = App_Hotkeys.SwitchLayout.Item1;
                swl_HKTxt.Text = App_Hotkeys.SwitchLayout.Item2;
            }

            if ((App_Hotkeys.ShortcutsReminder.Item1 == "Ctrl" || App_Hotkeys.ShortcutsReminder.Item1 == "Alt" || App_Hotkeys.ShortcutsReminder.Item1 == "Shift") && App_Hotkeys.ShortcutsReminder.Item2.Length == 1 && Regex.IsMatch(App_Hotkeys.ShortcutsReminder.Item2, @"^[a-zA-Z0-9]+$"))
            {
                rm_Cmb.SelectedItem = App_Hotkeys.ShortcutsReminder.Item1;
                rm_HKTxt.Text = App_Hotkeys.ShortcutsReminder.Item2;
            }



            if ((App_Hotkeys.SwitchMergerForeGroundChild.Item1 == "Ctrl" || App_Hotkeys.SwitchMergerForeGroundChild.Item1 == "Alt" || App_Hotkeys.SwitchMergerForeGroundChild.Item1 == "Shift") && App_Hotkeys.SwitchMergerForeGroundChild.Item2.Length == 1 && Regex.IsMatch(App_Hotkeys.SwitchMergerForeGroundChild.Item2, @"^[a-zA-Z0-9]+$"))
            {
                smfw_Cmb.SelectedItem = App_Hotkeys.SwitchMergerForeGroundChild.Item1;
                smfw_HKTxt.Text = App_Hotkeys.SwitchMergerForeGroundChild.Item2;
            }

            ignoreInputLockReminderCheckbox.Checked = App_Misc.IgnoreInputLockReminder;
            

            debugLogCheck.Checked = App_Misc.DebugLog;

            if (App_Misc.NucleusAccountPassword != "")
            {
                nucUserPassTxt.Text = App_Misc.NucleusAccountPassword;
            }

            if (App_Audio.Custom == "0")
            {
                audioDefaultSettingsRadio.Checked = true;
                audioCustomSettingsBox.Enabled = false;
            }
            else
            {
                audioCustomSettingsRadio.Checked = true;
            }

            GetAllScreensResolutions();

            RefreshAudioList();

            GetPlayersNickNameAndSteamIds();

            Rectangle area = Screen.PrimaryScreen.Bounds;
            if (App_Misc.SettingsLocation != "")
            {
                string[] windowLocation = App_Misc.SettingsLocation.Split('X');

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

            float newFontSize = Font.Size * scale;

            foreach (Control c in ctrls)
            {
                if (scale > 1.0F)
                {
                    if (c is ComboBox || c is TextBox || c is GroupBox)
                    {
                        if (c.Parent.Name != "hotkeyBox")
                        {
                            c.Font = new Font(c.Font.FontFamily, c is TextBox ? newFontSize + 3 : newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                        }
                    }
                }

                if (c is Label && !(c is CustomNumericUpDown))
                {
                    if (c.Name.Contains("hkLabel"))
                    {
                        c.Location = new Point(tu_Cmb.Left - c.Width, c.Location.Y);
                    }
                }
            }

            def_sid_comboBox.Font = new Font(def_sid_comboBox.Font.FontFamily, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);

            audioRefresh.Location = new Point((audioTab.Width / 2) - (audioRefresh.Width / 2), audioRefresh.Location.Y);
            default_sid_list_label.Location = new Point(def_sid_comboBox.Left - default_sid_list_label.Width, ((def_sid_comboBox.Location.Y + def_sid_comboBox.Height / 2) - default_sid_list_label.Height / 2) /*- 4*/);
            audioWarningLabel.Location = new Point(audioTab.Width / 2 - audioWarningLabel.Width / 2, audioWarningLabel.Location.Y);
            gamepadsAssignMethods.Location = new Point((page1.Location.X + label7.Location.X) + 2, (page1.Top - 5) - gamepadsAssignMethods.Height);
            refreshScreenDatasButton.Location = new Point(mergerResSelectorLabel.Right, refreshScreenDatasButton.Location.Y);

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
        }

        private void SetToolTips()
        {
            CustomToolTips.SetToolTip(splitDiv, "May not work for all games", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(hideDesktop, "Will only show the splitscreen division window without adjusting the game windows size and offset.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(disableGameProfiles, "Disables profiles, Nucleus will use the global settings instead.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(gamepadsAssignMethods, "Can break controller support in some handlers. If enabled profiles\n" +
                                                             "will not save per player gamepad but use XInput indexes instead \n" +
                                                             "(switching modes could prevent some profiles to load properly).\n" +
                                                             "Note: Nucleus will return to the main screen.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(enable_WMerger, "Game windows will be merged to a single window\n" +
                                                       "so Lossless Scaling can be used with Nucleus.\n " +
                                                       "Note that there's no mutiple monitor support yet.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });

            CustomToolTips.SetToolTip(losslessHook, "Lossless will not stop upscaling if an other window get the focus, useful\n" +
                                                    "if game windows requires real focus to receive inputs.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(refreshScreenDatasButton, "Refresh screens info.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(mergerShortcutLabel, "Each press will set an other child window as foreground window(similar to Alt+Tab).", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
        }

        private void GetPlayersNickNameAndSteamIds()
        {
            for (int i = 0; i < Globals.NucleusMaxPlayers; i++)
            {
                var sidField = steamIds[i];
                sidField.Items.AddRange(PlayersIdentityCache.GetCachedSteamIdsList.ToArray());
                sidField.Text = PlayersIdentityCache.SettingsIniSteamIdsList[i];
                sidField.SelectedItem = sidField.Text;

                var nickField = controllerNicks[i];
                nickField.Items.AddRange(PlayersIdentityCache.GetCachedNicknamesList.ToArray());
                nickField.Items.AddRange(PlayersIdentityCache.SettingsIniNicknamesList.Where(nic => !nickField.Items.Contains(nic)).ToArray());
                nickField.Text = PlayersIdentityCache.SettingsIniNicknamesList[i];
                nickField.SelectedItem = nickField.Text;
            }
        }

        private void CloseBtnPicture_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Globals.GameProfilesFolder))
            {
                Directory.CreateDirectory(Globals.GameProfilesFolder);
            }

            bool sidWrongValue = false;
            bool hasEmptyNickname = false;

            for (int i = 0; i < Globals.NucleusMaxPlayers; i++)
            {
                var nickField = controllerNicks[i];

                if (nickField.Text != "")
                {
                    Globals.ini.IniWriteValue("ControllerMapping", "Player_" + (i + 1), nickField.Text);

                    PlayersIdentityCache.SettingsIniNicknamesList[i] = nickField.Text;

                    if (PlayersIdentityCache.GetCachedNicknamesList.All(n => n != nickField.Text))
                    {
                        PlayersIdentityCache.AddNicknameToCache(nickField.Text);
                    }

                    for (int n = 0; n < Globals.NucleusMaxPlayers; n++)
                    {
                        if (controllerNicks[n].Items.Contains(nickField.Text))
                        {
                            continue;
                        }

                        controllerNicks[n].Items.Add(nickField.Text);
                    }
                }
                else
                {
                    hasEmptyNickname = true;
                }

                var sidField = steamIds[i];

                if (Regex.IsMatch(sidField.Text, "^[0-9]+$") && sidField.Text.Length == 17 || sidField.Text.Length == 0)
                {
                    Globals.ini.IniWriteValue("SteamIDs", "Player_" + (i + 1), sidField.Text);

                    PlayersIdentityCache.SettingsIniSteamIdsList[i] = sidField.Text;

                    if (sidField.Text != "")
                    {
                        if (PlayersIdentityCache.GetCachedSteamIdsList.All(n => n != sidField.Text.ToString()))
                        {
                            PlayersIdentityCache.AddSteamIdToCache(sidField.Text.ToString());
                        }
                    }
                }
                else
                {
                    sidField.BackColor = Color.Red;
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

            PlayersIdentityCache.SaveNicknamesCache();
            PlayersIdentityCache.SaveSteamIdsCache();

            if (audioDefaultSettingsRadio.Checked)
            {
                App_Audio.Custom = 0.ToString();
            }
            else
            {
                App_Audio.Custom = 1.ToString();
            }

            foreach (Control ctrl in audioCustomSettingsBox.Controls)
            {
                if (ctrl is ComboBox cmb)
                {
                    if (audioDevices?.Count > 0 && audioDevices.Keys.Contains(cmb.Text))
                    {
                        App_Audio.SaveAudioOutput(cmb.Name, audioDevices[cmb.Text]);
                    }
                }
            }

            foreach (Control ht in hotkeyBox.Controls)
            {
                if (ht is TextBox)
                {
                    if (ht.Text == "")
                    {
                        MessageBox.Show("Hotkeys values can't be empty", "Invalid hotkeys values!");
                        return;
                    }
                }
            }


            if (smfw_HKTxt.Text == "")
            {
                MessageBox.Show("Merger hotkey value can't be empty", "Invalid hotkey value!");
                return;
            }

            App_Hotkeys.CloseApp = Tuple.Create(cn_Cmb.SelectedItem.ToString(), cn_HKTxt.Text);
            App_Hotkeys.StopSession = Tuple.Create(ss_Cmb.SelectedItem.ToString(), ss_HKTxt.Text);
            App_Hotkeys.TopMost = Tuple.Create(ttm_Cmb.SelectedItem.ToString(), ttm_HKTxt.Text);
            App_Hotkeys.SetFocus = Tuple.Create(tu_Cmb.SelectedItem.ToString(), tu_HKTxt.Text);
            App_Hotkeys.ResetWindows = Tuple.Create(rw_Cmb.SelectedItem.ToString(), rw_HKTxt.Text);
            App_Hotkeys.LockInputs = lockKey_Cmb.SelectedItem.ToString();
            App_Hotkeys.CutscenesMode = Tuple.Create(csm_Cmb.SelectedItem.ToString(), csm_HKTxt.Text);
            App_Hotkeys.SwitchLayout = Tuple.Create(swl_Cmb.SelectedItem.ToString(), swl_HKTxt.Text);
            App_Hotkeys.ShortcutsReminder = Tuple.Create(rm_Cmb.SelectedItem.ToString(), rm_HKTxt.Text);
            App_Hotkeys.SwitchMergerForeGroundChild = Tuple.Create(smfw_Cmb.SelectedItem.ToString(), smfw_HKTxt.Text);

            App_Misc.UseNicksInGame = useNicksCheck.Checked;
            App_Misc.KeepAccounts = keepAccountsCheck.Checked;
            App_Misc.Network = cmb_Network.Text.ToString();
            App_Misc.IgnoreInputLockReminder = ignoreInputLockReminderCheckbox.Checked;
            App_Misc.DebugLog = debugLogCheck.Checked;
            App_Misc.SteamLang = cmb_Lang.SelectedItem.ToString();
            App_Misc.EpicLang = cmb_EpicLang.SelectedItem.ToString();

            App_Misc.NucleusAccountPassword = nucUserPassTxt.Text;
            App_Misc.AutoDesktopScaling = scaleOptionCbx.Checked;

            App_Misc.UseXinputIndex = gamepadsAssignMethods.Checked;

            App_Layouts.SplitDiv = splitDiv.Checked;
            App_Layouts.HideOnly = hideDesktop.Checked;
            App_Layouts.SplitDivColor = SplitColors.Text.ToString();
            App_Layouts.HorizontalLines = numUpDownHor.Value;
            App_Layouts.VerticalLines = numUpDownVer.Value;
            App_Layouts.MaxPlayers = numMaxPlyrs.Value;

            App_Layouts.Cts_MuteAudioOnly = cts_Mute.Checked;
            App_Layouts.Cts_KeepAspectRatio = cts_kar.Checked;
            App_Layouts.Cts_Unfocus = cts_unfocus.Checked;
            App_Layouts.WindowsMerger = enable_WMerger.Checked;

            if (setupScreen != null)
            {
                if (DevicesFunctions.UseGamepadApiIndex != gamepadsAssignMethods.Checked)
                {
                    DevicesFunctions.UseGamepadApiIndex = gamepadsAssignMethods.Checked;
                    mainForm.RefreshUI(true);
                }
            }

            bool disableGameProfileschanged = disableGameProfiles.Checked != App_Misc.DisableGameProfiles;

            if (disableGameProfileschanged)
            {
                App_Misc.DisableGameProfiles = disableGameProfiles.Checked;
            }

            bool needToRestart = false;

            if (themeCbx.SelectedItem.ToString() != prevTheme)
            {
                App_Misc.Theme = themeCbx.SelectedItem.ToString();
                mainForm.restartRequired = true;
                needToRestart = true;
            }

            if (GameProfile.IsNewProfile || disableGameProfileschanged)
            {
                if (GameProfile.Instance != null)
                {
                    GameProfile.Instance.Reset();
                    ProfileSettings.ProfileRefreshAudioList();
                }
            }

            if (mainForm.Xinput_S_Setup.Visible)
            {
                mainForm.Xinput_S_Setup.Visible = false;
            }

            #region take a picture of the hotkeys

            Color defColor = hotkeyBox.BackColor;

            try
            {
                hotkeyBox.BackColor = Color.Black;

                Graphics g = hotkeyBox.CreateGraphics();
                Bitmap bmp = new Bitmap(hotkeyBox.Width, hotkeyBox.Height);
                hotkeyBox.DrawToBitmap(bmp, new Rectangle(0, 0, hotkeyBox.Width, hotkeyBox.Height));

                string savePath = Path.Combine(Application.StartupPath, $@"gui\shortcuts");

                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                bmp.Save(Path.Combine(savePath, "KbShortcutsReminder.jpeg"), ImageFormat.Jpeg);
                bmp.Dispose();
                g.Dispose();

                hotkeyBox.BackColor = defColor;
            }
            catch
            {
                hotkeyBox.BackColor = defColor;
            }

            #endregion

            if (needToRestart)
            {
                Thread.Sleep(300);
                Application.Restart();
                Process.GetCurrentProcess().Kill();
                return;
            }

            Globals.MainOSD.Show(500, "Settings Saved");

            if (Location.X == -32000 || Width == 0)
            {
                Visible = false;
                return;
            }

            App_Misc.SettingsLocation = Location.X + "X" + Location.Y;
            mainForm.BringToFront();
            Visible = false;
        }

        private void Steamid_Click(object sender, EventArgs e)
        {
            ComboBox id = (ComboBox)sender;
            id.BackColor = Color.White;
        }

        private void Cmb_Network_DropDown(object sender, EventArgs e)
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

        private void Cmb_Network_DropDownClosed(object sender, EventArgs e)
        {
            if (cmb_Network.SelectedItem == null)
            {
                cmb_Network.SelectedIndex = 0;
            }
        }

        private void AudioCustomSettingsRadio_CheckedChanged(object sender, EventArgs e)
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
                if (ctrl is ComboBox cmb)
                {
                    string lastItem = cmb.Text;
                    cmb.Items.Clear();
                    cmb.Items.AddRange(audioDevices.Keys.ToArray());

                    if (cmb.Items.Contains(lastItem))
                    {
                        cmb.SelectedItem = lastItem;
                    }
                    else if (audioDevices.Values.Contains(App_Audio.Instances_AudioOutput[cmb.Name]))
                    {
                        cmb.SelectedItem = audioDevices.FirstOrDefault(x => x.Value == App_Audio.Instances_AudioOutput[cmb.Name]).Key;
                    }
                    else
                    {
                        cmb.SelectedItem = "Default";
                    }
                }
            }
        }

        private void AudioRefresh_Click(object sender, EventArgs e)
        {
            RefreshAudioList();
        }

        private void TabsButtons_highlight(object sender, EventArgs e)
        {
            Control c = sender as Control;

            for (int i = 0; i < tabsButtons.Count; i++)
            {
                var button = tabsButtons[i];

                if (i < tabs.Count)
                {
                    var tab = tabs[i];

                    if (tab.Name != (string)c.Tag)
                    {
                        tab.Visible = false;
                    }
                    else
                    {
                        tab.Visible = true;
                        tab.BringToFront();
                    }
                }

                if (!(button is Button))
                {
                    continue;
                }

                if (button.Tag != c.Tag)
                {
                    button.BackColor = Color.Transparent;
                }
                else
                {
                    button.BackColor = selectionColor;
                }
            }
        }

        private void AudioBtnPicture_Click(object sender, EventArgs e)
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDevice audioDefault = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            audioDefaultDevice.Text = "Default: " + audioDefault.FriendlyName;
        }

        private void CloseBtnPicture_MouseEnter(object sender, EventArgs e)
        {
            closeBtnPicture.BackgroundImage = ImageCache.GetImage(mainForm.theme + "title_close_mousehover.png");
        }

        private void CloseBtnPicture_MouseLeave(object sender, EventArgs e)
        {
            closeBtnPicture.BackgroundImage = ImageCache.GetImage(mainForm.theme + "title_close.png");
        }

        private void Ctrlr_shorcuts_Click(object sender, EventArgs e)
        {
            if (mainForm.Xinput_S_Setup.Visible)
            {
                mainForm.Xinput_S_Setup.BringToFront();
                return;
            }

            mainForm.Xinput_S_Setup.Show();
        }

        private void KeepAccountsCheck_Click(object sender, EventArgs e)
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

        private void Num_KeyPress(object sender, KeyPressEventArgs e)
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

        private void HKTxt_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = string.Concat(textBox.Text.Where(char.IsLetterOrDigit));
        }

        private void HKTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = "";
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Cts_Mute_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox mute = (CheckBox)sender;

            if (mute.Checked)
            {
                cts_kar.Checked = false;
                cts_kar.Enabled = false;
                cts_unfocus.Checked = false;
                cts_unfocus.Enabled = false;
            }
            else
            {
                cts_kar.Enabled = true;
                cts_unfocus.Enabled = true;
            }
        }

        private void BtnNext_Click(object sender, EventArgs e)
        {
            if (page1.Visible)
            {
                btnNext.BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "page2.png");
                page1.Visible = false;
                page2.BringToFront();
                page2.Visible = true;
            }
            else
            {
                btnNext.BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "page1.png");
                page2.Visible = false;
                page1.BringToFront();
                page1.Visible = true;
            }
        }

        private void Controlscollect()
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

        private void LayoutSizer_Paint(object sender, PaintEventArgs e)
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

        private void Btn_Gb_Update_Click(object sender, EventArgs e)
        {
            GoldbergUpdaterForm gbUpdater = new GoldbergUpdaterForm();
            gbUpdater.ShowDialog();
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

            if (cb.Text == "")
            {
                return;
            }

            if (!PlayersIdentityCache.GetCachedNicknamesList.Any(n => n == cb.Text))
            {
                PlayersIdentityCache.AddNicknameToCache(cb.Text);
            }

            for (int i = 0; i < Globals.NucleusMaxPlayers; i++)
            {
                if (!controllerNicks[i].Items.Contains(cb.Text))
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

            if (!PlayersIdentityCache.GetCachedSteamIdsList.Any(n => n == cb.Text))
            {
                PlayersIdentityCache.AddSteamIdToCache(cb.Text);
            }

            for (int i = 0; i < Globals.NucleusMaxPlayers; i++)
            {
                if (!steamIds[i].Items.Contains(cb.Text))
                {
                    steamIds[i].Items.Add(cb.Text);
                }
            }
        }

        private Dictionary<string, List<string>> AllScreensRes;
        private List<Label> screensLabels;
        private List<Rectangle> screensResControlsRow;

        private void GetAllScreensResolutions()
        {
            screen_panel.Visible = false;
            screensResControlsRow = new List<Rectangle>();
            screensLabels = new List<Label>();
            AllScreensRes = new Dictionary<string, List<string>>();
            screen_panel.Controls.Clear();

            MonitorsDpiScaling.DEVMODE vDevMode = new MonitorsDpiScaling.DEVMODE();

            int i = 0;

            foreach (Screen screen in Screen.AllScreens)
            {
                string mergerRes = App_Layouts.WindowsMergerRes;

                if (mergerRes == "")
                {
                    if (screen.Primary)
                    {
                        App_Layouts.WindowsMergerRes = $"{screen.Bounds.Width}X{screen.Bounds.Height}";
                        selectedRes.Text = App_Layouts.WindowsMergerRes;
                    }
                }
                else
                {
                    selectedRes.Text = App_Layouts.WindowsMergerRes;
                }

                ComboBox resCmb = new ComboBox();
                resCmb.BackColor = Color.Black;
                resCmb.ForeColor = Color.White;
                resCmb.FlatStyle = FlatStyle.Flat;
                resCmb.TextChanged += SaveSelectedRes;
                resCmb.DropDownStyle = ComboBoxStyle.DropDownList;

                Label resLabel = new Label();
                resLabel.AutoSize = true;
                string cleanName = screen.DeviceName.Substring(screen.DeviceName.LastIndexOf('\\') + 1);
                resLabel.Text = $"⛶ {cleanName}";

                CustomToolTips.SetToolTip(resLabel, "Click here to identify the screen", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });

                if (screensLabels.Count == 0)
                {
                    screen_panel.Controls.Add(resLabel);
                    resLabel.Location = new Point(0, 5);
                    screensLabels.Add(resLabel);
                    screen_panel.Controls.Add(resCmb);
                }
                else
                {
                    screen_panel.Controls.Add(resLabel);
                    resLabel.Location = new Point(screensLabels[screensLabels.Count - 1].Location.X, screensLabels[screensLabels.Count - 1].Bottom + 4);
                    screensLabels.Add(resLabel);
                    screen_panel.Controls.Add(resCmb);
                }

                List<string> resolutions = new List<string>();

                while (MonitorsDpiScaling.EnumDisplaySettings(screen.DeviceName, i, ref vDevMode))
                {
                    string resString = $"{vDevMode.dmPelsWidth}X{vDevMode.dmPelsHeight}";

                    if (resolutions.Contains(resString) || vDevMode.dmPelsWidth > screen.Bounds.Width || vDevMode.dmPelsHeight > screen.Bounds.Height)
                    {
                        i++;
                        continue;
                    }

                    resolutions.Add(resString);
                    i++;
                }

                string currentScreenRes = $"{screen.Bounds.Width}X{screen.Bounds.Height}";

                resCmb.Items.AddRange(resolutions.ToArray());
                resCmb.SelectedItem = mergerRes == "" ? currentScreenRes : mergerRes;
                resLabel.Text += $" ({currentScreenRes})";

                if (screensLabels.Count == 0)
                {
                    resCmb.Location = new Point(resLabel.Right + 4, resLabel.Top);
                }
                else
                {
                    resCmb.Location = new Point(resLabel.Right + 4, resLabel.Top);
                }

                resLabel.Location = new Point(resLabel.Left, resCmb.Location.Y + (resCmb.Height / 2 - resLabel.Height / 2));
                resLabel.Tag = screen.Bounds.Location;
                resLabel.Click += IdentifyScreen;

                Rectangle border = new Rectangle(resLabel.Location.X, resCmb.Location.Y, resCmb.Right, resCmb.Height - 1);
                screensResControlsRow.Add(border);

                AllScreensRes.Add(screen.DeviceName, resolutions);

                i = 0;
            }

            refreshScreensData = false;
            screen_panel.Visible = true;
        }

        private void IdentifyScreen(object sender, EventArgs e)
        {
            Label lb = sender as Label;
            ScreenId identifierWindow = new ScreenId((Point)lb.Tag);
            identifierWindow.Show();
        }

        private void SaveSelectedRes(object sender, EventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            selectedRes.Text = cmb.Text;
            App_Layouts.WindowsMergerRes = cmb.Text;
        }

        private void Settings_VisibleChanged(object sender, EventArgs e)
        {
            if (Visible)
            {
                //Reload do not cache so we always have fresh screen datas.
                GetAllScreensResolutions();
                //Update in case the logmanager prompt updated the value
                debugLogCheck.Checked = App_Misc.DebugLog;
            }

            mainForm.DisableGameProfiles = disableGameProfiles.Checked;
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

        private void LosslessHook_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            App_Layouts.LosslessHook = checkBox.Checked;
        }

        private void Screen_panel_Paint(object sender, PaintEventArgs e)
        {
            if (!refreshScreensData)
                e.Graphics.DrawRectangles(bordersPen, screensResControlsRow.ToArray());
        }

        private bool refreshScreensData;

        private void RefreshScreenDatasButton_Click(object sender, EventArgs e)
        {
            refreshScreensData = true;
            GetAllScreensResolutions();
        }
    }
}