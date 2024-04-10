using NAudio.CoreAudioApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nucleus.Gaming;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Controls;
using Nucleus.Gaming.Controls.SetupScreen;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Forms.NucleusMessageBox;
using Nucleus.Gaming.UI;
using Nucleus.Gaming.Windows.Interop;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Nucleus.Coop
{

    public partial class ProfileSettings : BaseForm, IDynamicSized
    {

        private MainForm mainForm = null;
        private SetupScreenControl setupScreen = null;
        private static ProfileSettings profileSettings;

        private List<string> nicksList = new List<string>();
        private List<string> steamIdsList = new List<string>();

        private string currentNickname;
        private string currentSteamId;

        private List<Panel> tabs = new List<Panel>();
        private List<Control> tabsButtons = new List<Control>();

        private ComboBox[] controllerNicks;
        private ComboBox[] steamIds;
        private ComboBox[] IdealProcessors;
        private TextBox[] Affinitys;
        private ComboBox[] PriorityClasses;
        private List<Control> ctrls = new List<Control>();

        private float fontSize;

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

        public ProfileSettings(MainForm mf, SetupScreenControl pc)
        {
            fontSize = float.Parse(mf.themeIni.IniReadValue("Font", "SettingsFontSize"));
            profileSettings = this;
            mainForm = mf;
            setupScreen = pc;

            InitializeComponent();

            default_Cursor = Theme_Settings.Default_Cursor;
            hand_Cursor = Theme_Settings.Hand_Cursor;
            Cursor = default_Cursor;

            
            var borderscolor = mf.themeIni.IniReadValue("Colors", "ProfileSettingsBorder").Split(',');
            selectionColor = Theme_Settings.SelectedBackColor;
            bordersPen = new Pen(Color.FromArgb(int.Parse(borderscolor[0]), int.Parse(borderscolor[1]), int.Parse(borderscolor[2])));

            BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "other_backgrounds.jpg");

            controlscollect();

            foreach (Control c in ctrls)
            {
                if (c is CheckBox || c is Label || c is RadioButton)
                {
                    if (c.Name != "audioWarningLabel" && c.Name != "warningLabel" && c.Name != "modeLabel")
                        c.Font = new Font(mf.customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                }

                if (c is ComboBox || c is TextBox || c is GroupBox)
                {
                    if (c.Name != "notes_text" && c.Name != "profileTitle")
                        c.Font = new Font(mf.customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                }

                if (c is CustomNumericUpDown)
                {
                    c.Font = new Font(mf.customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                }

                if (c.Name != "sharedTab" && c.Name != "playersTab" && c.Name != "audioTab" &&
                    c.Name != "processorTab" && c.Name != "layoutTab" && c.Name != "layoutSizer" && c.Name != "notes_text"
                    && !(c is Label) && !(c is TextBox))
                {
                    c.Cursor = hand_Cursor;
                }

                if (c.Name == "sharedTab" || c.Name == "playersTab" || c.Name == "audioTab" || c.Name == "processorTab" || c.Name == "layoutTab")
                {
                    c.BackColor = Color.Transparent;
                    tabs.Add(c as Panel);
                }

                if ((string)c.Tag == "sharedTab" || (string)c.Tag == "playersTab" || (string)c.Tag == "audioTab" || (string)c.Tag == "processorTab" || (string)c.Tag == "layoutTab")
                {
                    c.Click += new EventHandler(tabsButtons_highlight);
                    tabsButtons.Add(c);
                }

                if (c is Button)
                {
                    Button isButton = c as Button;
                    isButton.FlatAppearance.BorderSize = 0;
                    isButton.FlatAppearance.MouseOverBackColor = selectionColor;
                }

                if (c.Name.Contains("pauseBetweenInstanceLaunch_TxtBox") || c.Name.Contains("WindowsSetupTiming_TextBox"))
                {
                    c.KeyPress += new KeyPressEventHandler(this.num_KeyPress);
                }

                if (c.Name.Contains("steamid") && c is ComboBox)
                {
                    c.KeyPress += new KeyPressEventHandler(this.num_KeyPress);
                    c.Click += new System.EventHandler(Steamid_Click);
                }
            }

            ForeColor = Color.FromArgb(int.Parse(mf.rgb_font[0]), int.Parse(mf.rgb_font[1]), int.Parse(mf.rgb_font[2]));

            audioBtnPicture.BackgroundImage = ImageCache.GetImage(mf.theme + "audio.png");
            playersBtnPicture.BackgroundImage = ImageCache.GetImage(mf.theme + "players.png");
            sharedBtnPicture.BackgroundImage = ImageCache.GetImage(mf.theme + "shared.png");
            processorBtnPicture.BackgroundImage = ImageCache.GetImage(mf.theme + "processor.png");
            layoutBtnPicture.BackgroundImage = ImageCache.GetImage(mf.theme + "layout.png");
            closeBtnPicture.BackgroundImage = ImageCache.GetImage(mf.theme + "title_close.png");
            audioRefresh.BackgroundImage = ImageCache.GetImage(mf.theme + "refresh.png");
            profile_info_btn.BackgroundImage = ImageCache.GetImage(mf.theme + "profile_info.png");
            btnNext.BackgroundImage = ImageCache.GetImage(mf.theme + "page1.png");
            btnProcessorNext.BackgroundImage = ImageCache.GetImage(mf.theme + "page1.png");

            audioBtnPicture.Click += new EventHandler(audioTabBtn_Click);

            btnNext.BackColor = mf.buttonsBackColor;
            btnProcessorNext.BackColor = mf.buttonsBackColor;

            audioRefresh.BackColor = Color.Transparent;

            modeLabel.Cursor = default_Cursor;

            def_sid_comboBox.KeyPress += new KeyPressEventHandler(ReadOnly_KeyPress);

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

            IdealProcessors = new ComboBox[] {
                IdealProcessor1, IdealProcessor2, IdealProcessor3, IdealProcessor4, IdealProcessor5, IdealProcessor6, IdealProcessor7, IdealProcessor8,
                IdealProcessor9, IdealProcessor10, IdealProcessor11, IdealProcessor12, IdealProcessor13, IdealProcessor14, IdealProcessor15,IdealProcessor16,
                IdealProcessor17, IdealProcessor18, IdealProcessor19, IdealProcessor20, IdealProcessor21, IdealProcessor22, IdealProcessor23, IdealProcessor24,
                IdealProcessor25, IdealProcessor26, IdealProcessor27, IdealProcessor28, IdealProcessor29, IdealProcessor30, IdealProcessor31, IdealProcessor32};

            Affinitys = new TextBox[] {
                Affinity1, Affinity2, Affinity3, Affinity4, Affinity5, Affinity6, Affinity7, Affinity8,
                Affinity9, Affinity10, Affinity11, Affinity12, Affinity13, Affinity14, Affinity15,Affinity16,
                Affinity17, Affinity18, Affinity19, Affinity20, Affinity21, Affinity22, Affinity23, Affinity24,
                Affinity25, Affinity26, Affinity27, Affinity28, Affinity29, Affinity30, Affinity31, Affinity32};

            PriorityClasses = new ComboBox[] {
                PriorityClass1, PriorityClass2, PriorityClass3, PriorityClass4, PriorityClass5, PriorityClass6, PriorityClass7, PriorityClass8,
                PriorityClass9, PriorityClass10, PriorityClass11,PriorityClass12, PriorityClass13, PriorityClass14, PriorityClass15,PriorityClass16,
                PriorityClass17, PriorityClass18, PriorityClass19, PriorityClass20, PriorityClass21, PriorityClass22, PriorityClass23, PriorityClass24,
                PriorityClass25, PriorityClass26, PriorityClass27, PriorityClass28, PriorityClass29, PriorityClass30, PriorityClass31, PriorityClass32};


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

            for (int p = 0; p < Environment.ProcessorCount; p++)
            {
                for (int all = 0; all < IdealProcessors.Length; all++)
                {
                    if (p == 0)
                    {
                        IdealProcessors[all].Items.Add("*");
                        IdealProcessors[all].SelectedIndex = 0;
                    }

                    IdealProcessors[all].Items.Add((IdealProcessors[all].Items.Count).ToString());
                    IdealProcessors[all].Click += new EventHandler(IdealProcessor_Click);
                    IdealProcessors[all].SelectedIndexChanged += new EventHandler(IdealProcessor_Click);
                    IdealProcessors[all].KeyPress += new KeyPressEventHandler(Affinity_KeyPress);

                    Affinitys[all].Text = "";
                    Affinitys[all].Click += new EventHandler(Affinity_Click);
                    Affinitys[all].KeyPress += new KeyPressEventHandler(Affinity_KeyPress);
                }
            }

            coreCountLabel.Text = $"Cores/Threads {Environment.ProcessorCount} (Max Value)";
            coreCountLabel.ForeColor = Color.Yellow;

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

            foreach (ComboBox pClass in PriorityClasses)
            {
                pClass.SelectedIndex = 0;
                pClass.KeyPress += new KeyPressEventHandler(ReadOnly_KeyPress);
            }

            ///network setting
            RefreshCmbNetwork();

            sharedTab.Parent = this;
            sharedTab.Location = new Point(0, sharedTabBtn.Bottom);

            playersTab.Parent = this;
            playersTab.Location = sharedTab.Location;

            audioTab.Parent = this;
            audioTab.Location = sharedTab.Location;

            processorTab.Parent = this;
            processorTab.Location = sharedTab.Location;

            layoutTab.Parent = this;
            layoutTab.Location = sharedTab.Location;

            page1.Parent = playersTab;
            page1.Location = new Point(playersTab.Width / 2 - page1.Width / 2, playersTab.Height / 2 - page1.Height / 2);
            page1.BringToFront();

            page2.Parent = playersTab;
            page2.Location = page1.Location;

            btnNext.Parent = playersTab;
            btnNext.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnNext.Location = new Point(page1.Right - btnNext.Width, (page1.Top - btnNext.Height) - 5);

            processorPage1.Parent = processorTab;
            processorPage1.Location = new Point(processorTab.Width / 2 - processorPage1.Width / 2, processorTab.Height / 2 - processorPage1.Height / 2);
            processorPage1.BringToFront();

            processorPage2.Parent = processorTab;
            processorPage2.Location = processorPage1.Location;

            btnProcessorNext.Parent = processorTab;
            btnProcessorNext.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnProcessorNext.Location = new Point(processorPage1.Right - btnProcessorNext.Width, (processorPage1.Top - btnProcessorNext.Height) - 5);

            sharedTab.BringToFront();
            default_sid_list_label.Location = new Point(def_sid_comboBox.Left - default_sid_list_label.Width, ((def_sid_comboBox.Location.Y + def_sid_comboBox.Height / 2) - default_sid_list_label.Height / 2) - 4);

            Vector2 dist1 = Vector2.Subtract(new Vector2(profile_info_btn.Location.X, profile_info_btn.Location.Y), new Vector2(processorBtnPicture.Location.X, processorBtnPicture.Location.Y));
            float modeLabelX = (processorBtnPicture.Right + (dist1.X / 2)) - modeLabel.Width / 2;

            modeLabel.Location = new Point((int)modeLabelX, sharedTabBtn.Location.Y + sharedTabBtn.Height / 2 - modeLabel.Height / 2);
            audioRefresh.Location = new Point((audioTab.Width / 2) - (audioRefresh.Width / 2), audioRefresh.Location.Y);
            warningLabel.ForeColor = Color.Yellow;

            def_sid_comboBox.SelectedIndex = 0;

            numUpDownVer.MaxValue = 5;
            numUpDownVer.InvalidParent = true;

            numUpDownHor.MaxValue = 5;
            numUpDownHor.InvalidParent = true;

            RefreshAudioList();

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

            GetPlayersNickNameAndIds();

            Rectangle area = Screen.PrimaryScreen.Bounds;
            if (ini.IniReadValue("Misc", "ProfileSettingsLocation") != "")
            {
                string[] windowLocation = ini.IniReadValue("Misc", "ProfileSettingsLocation").Split('X');

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

        public ProfileSettings()
        {
            DPIManager.Unregister(this);
        }

        public new void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }

            SuspendLayout();

            if (scale > 1.0F)
            {
                float newFontSize = Font.Size * scale;

                foreach (Control c in ctrls)
                {
                    if (c is ComboBox || c is TextBox || c is GroupBox)
                    {
                        c.Font = new Font(c.Font.FontFamily, c is TextBox ? newFontSize + 3 : newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }
                }

                notes_text.Size = new Size((int)(260 * scale), (int)(81 * scale));
                def_sid_comboBox.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
            }

            Vector2 dist1 = Vector2.Subtract(new Vector2(profile_info_btn.Location.X, profile_info_btn.Location.Y), new Vector2(layoutBtnPicture.Location.X, layoutBtnPicture.Location.Y));
            float modeLabelX = (layoutBtnPicture.Right + (dist1.X / 2)) - modeLabel.Width / 2;

            modeLabel.Location = new Point((int)modeLabelX, sharedTabBtn.Location.Y + sharedTabBtn.Height / 2 - modeLabel.Height / 2);
            audioRefresh.Location = new Point((audioTab.Width / 2) - (audioRefresh.Width / 2), audioRefresh.Location.Y);
            default_sid_list_label.Location = new Point(def_sid_comboBox.Left - default_sid_list_label.Width, ((def_sid_comboBox.Location.Y + def_sid_comboBox.Height / 2) - default_sid_list_label.Height / 2));
            warningLabel.Location = new Point(sharedTab.Width / 2 - warningLabel.Width / 2, warningLabel.Location.Y);
            audioWarningLabel.Location = new Point(audioTab.Width / 2 - audioWarningLabel.Width / 2, audioWarningLabel.Location.Y);

            int tabButtonsY = sharedTabBtn.Location.Y - 1;
            int tabButtonsHeight = sharedTabBtn.Height + 1;

            tabBorders = new Rectangle[]
            {
               new Rectangle(0, tabButtonsY, sharedTabBtn.Width + sharedBtnPicture.Width+1, tabButtonsHeight),
               new Rectangle(sharedBtnPicture.Right, tabButtonsY, playersTabBtn.Width + playersBtnPicture.Width + 2, tabButtonsHeight),
               new Rectangle(playersBtnPicture.Right, tabButtonsY, audioTabBtn.Width + audioBtnPicture.Width + 2, tabButtonsHeight),
               new Rectangle(audioBtnPicture.Right, tabButtonsY, processorTabBtn.Width + processorBtnPicture.Width + 2, tabButtonsHeight),
               new Rectangle(processorBtnPicture.Right, tabButtonsY ,layoutTabBtn.Width + layoutBtnPicture.Width + 2, tabButtonsHeight),

               new Rectangle(sharedTab.Location.X, sharedTab.Location.Y, sharedTab.Width - 1, sharedTab.Height),
            };

            ResumeLayout();
        }

        private void SetToolTips()
        {
            CustomToolTips.SetToolTip(autoPlay, "Automatically launch game instances on profile selection.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(WindowsSetupTiming_TextBox, "Speedup windows resizing and positioning (1000ms is fine in most cases).\n" +
                                                                  "Could break hooks or xinputplus for some games.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(pauseBetweenInstanceLaunch_TxtBox, "How much time to wait before starting the next game instance.\n" +
                                                                         "Could break positioning/resizing for some games.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(splitDiv, "May not work for all games.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(hideDesktop, "Will only show the splitscreen division window without adjusting the game windows size and offset.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
        }

        private void GetPlayersNickNameAndIds()
        {
            List<ProfilePlayer> playersList = GameProfile.ProfilePlayersList;

            steamIdsList.Clear();

            for (int nic = 0; nic < 32; nic++)
            {
                if (!NicknamesCache.Get.Any(n => n == "Player" + (nic + 1)))
                {
                    NicknamesCache.Add(ini.IniReadValue("ControllerMapping", "Player_" + (nic + 1)));
                }
            }

            steamIdsList.AddRange(SteamIdsCache.Get.ToArray());

            nicksList.Clear();
            nicksList.AddRange(NicknamesCache.Get);

            for (int i = 0; i < 32; i++)
            {
                ProfilePlayer player = null;

                if (i < playersList.Count)
                {
                    player = playersList[i];

                    steamIds[i].Items.Clear();
                    steamIds[i].Items.AddRange(steamIdsList.ToArray());

                    steamIds[i].SelectedItem = player.SteamID.ToString();
                    steamIds[i].Text = player.SteamID.ToString();
                    steamIds[i].Enabled = true;

                    controllerNicks[i].Items.Clear();
                    controllerNicks[i].Items.AddRange(nicksList.ToArray());

                    controllerNicks[i].SelectedItem = player.Nickname;
                    controllerNicks[i].Text = player.Nickname.ToString();
                    controllerNicks[i].Enabled = true;
                }
                else
                {
                    steamIds[i].Items.Clear();
                    steamIds[i].Items.AddRange(SteamIdsCache.Get.ToArray());

                    steamIds[i].Text = ini.IniReadValue("SteamIDs", "Player_" + (i + 1));
                    steamIds[i].SelectedItem = steamIds[i].Text;
                    steamIds[i].Enabled = false;

                    controllerNicks[i].Items.Clear();
                    controllerNicks[i].Items.AddRange(nicksList.ToArray());

                    controllerNicks[i].SelectedItem = ini.IniReadValue("ControllerMapping", "Player_" + (i + 1));
                    controllerNicks[i].Text = ini.IniReadValue("ControllerMapping", "Player_" + (i + 1));
                    controllerNicks[i].Enabled = false;

                    if (GameProfile.ModeText == "New Profile")
                    {
                        if (player != null)
                        {
                            player.Nickname = controllerNicks[i].Text;
                            player.SteamID = long.Parse(steamIds[i].Text);
                        }

                        steamIds[i].Enabled = true;
                        controllerNicks[i].Enabled = true;
                    }
                }
            }
        }

        public static void UpdateProfileSettingsUiValues()
        {
            profileSettings?.UpdateUiValues();
        }

        private void UpdateUiValues()
        {
            GetPlayersNickNameAndIds();

            for (int i = 0; i < 32; i++)
            {
                ProfilePlayer player = null;

                if (i < GameProfile.ProfilePlayersList.Count)
                {
                    player = GameProfile.ProfilePlayersList[i];
                }

                if (player != null)
                {
                    if (player.IdealProcessor != null)
                    {
                        IdealProcessors[i].SelectedIndex = IdealProcessors[i].Items.IndexOf(player.IdealProcessor);
                    }
                    else
                    {
                        IdealProcessors[i].SelectedIndex = 0;
                    }

                    if (player.Affinity != null)
                    {
                        Affinitys[i].Text = player.Affinity;
                    }
                    else
                    {
                        Affinitys[i].Text = "";
                    }

                    if (player.PriorityClass != null)
                    {
                        PriorityClasses[i].SelectedIndex = PriorityClasses[i].Items.IndexOf(player.PriorityClass);
                    }
                    else
                    {
                        player.PriorityClass = "Normal";
                        PriorityClasses[i].SelectedIndex = 0;
                    }

                    IdealProcessors[i].Enabled = true;
                    Affinitys[i].Enabled = true;
                    PriorityClasses[i].Enabled = true;
                }
                else
                {
                    IdealProcessors[i].Text = "*";
                    IdealProcessors[i].Enabled = false;

                    Affinitys[i].Text = "";
                    Affinitys[i].Enabled = false;

                    PriorityClasses[i].Text = "Normal";
                    PriorityClasses[i].Enabled = false;

                    if (GameProfile.ModeText == "New Profile")
                    {
                        IdealProcessors[i].Enabled = true;
                        Affinitys[i].Enabled = true;
                        PriorityClasses[i].Enabled = true;
                    }
                }
            }

            if (GameProfile.ModeText == "New Profile")
            {
                GameProfile.AudioInstances.Clear();
            }

            RefreshAudioList();

            audioDefaultSettingsRadio.Checked = GameProfile.AudioDefaultSettings;
            audioCustomSettingsRadio.Checked = GameProfile.AudioCustomSettings;
            audioCustomSettingsBox.Enabled = audioCustomSettingsRadio.Checked;

            autoPlay.Checked = GameProfile.AutoPlay;
            pauseBetweenInstanceLaunch_TxtBox.Text = GameProfile.PauseBetweenInstanceLaunch.ToString();
            WindowsSetupTiming_TextBox.Text = GameProfile.HWndInterval.ToString();
            splitDiv.Checked = GameProfile.UseSplitDiv;
            hideDesktop.Checked = GameProfile.HideDesktopOnly;
            SplitColors.Text = GameProfile.SplitDivColor;
            scaleOptionCbx.Checked = GameProfile.AutoDesktopScaling;
            useNicksCheck.Checked = GameProfile.UseNicknames;
            cmb_Network.Text = GameProfile.Network;
            numUpDownVer.Value = GameProfile.CustomLayout_Ver;
            numUpDownHor.Value = GameProfile.CustomLayout_Hor;
            numMaxPlyrs.Value = GameProfile.CustomLayout_Max;
            cts_Mute.Checked = GameProfile.Cts_MuteAudioOnly;
            cts_kar.Checked = GameProfile.Cts_KeepAspectRatio;
            cts_unfocus.Checked = GameProfile.Cts_Unfocus;
            notes_text.Text = GameProfile.Notes;
            profileTitle.Text = GameProfile.Title;

            if (GameProfile.Instance != null)
            {
                modeLabel.Text = GameProfile.ModeText;
            }
        }

        private void closeBtnPicture_Click(object sender, EventArgs e)///Set GameProfile values
        {
            if (!Directory.Exists(Globals.GameProfilesFolder))
            {
                Directory.CreateDirectory(Globals.GameProfilesFolder);
            }

            bool sidWrongValue = false;
            bool IdealProcessorsWrongValue = false;
            bool affinitysWrongValue = false;
            bool hasEmptyNickname = false;

            if (GameProfile.ProfilePlayersList.Count == 0)
            {
                ///Create profile players for new game profile
                for (int i = 0; i < 32; i++)
                {
                    ProfilePlayer player = new ProfilePlayer();
                    GameProfile.ProfilePlayersList.Add(player);
                }
            }

            for (int i = 0; i < GameProfile.ProfilePlayersList.Count; i++)
            {
                ProfilePlayer player = GameProfile.ProfilePlayersList[i];

                if (controllerNicks[i].Text != "")
                {
                    ///Set GameProfile Nicknames
                    player.Nickname = controllerNicks[i].Text;

                    if (NicknamesCache.Get.All(n => n != controllerNicks[i].Text))
                    {
                        NicknamesCache.Add(controllerNicks[i].Text);
                    }
                }
                else
                {
                    hasEmptyNickname = true;
                }

                ///Set GameProfile Steam ids
                if ((Regex.IsMatch(steamIds[i].Text, "^[0-9]+$") && steamIds[i].Text.Length == 17 || steamIds[i].Text.Length == 0) || steamIds[i].Text == "0" || steamIds[i].Text == "-1")
                {
                    if (steamIds[i].Text != "")
                    {
                        player.SteamID = long.Parse(steamIds[i].Text.ToString());

                        if (SteamIdsCache.Get.All(n => n == steamIds[i].Text.ToString()) && steamIds[i].Text != "0" && steamIds[i].Text != "-1")
                        {
                            SteamIdsCache.Add(steamIds[i].Text.ToString());
                        }
                    }
                    else
                    {
                        player.SteamID = -1;
                    }
                }
                else
                {
                    ///Invalid Steam id detected
                    steamIds[i].BackColor = Color.Red;
                    sidWrongValue = true;
                    break;
                }

                ///Set GameProfile Ideal Processors
                if (IdealProcessors[i].Text == "" || IdealProcessors[i].Text == null)
                {
                    IdealProcessors[i].Text = "*";
                }

                int wrongValue = Environment.ProcessorCount;
                if (IdealProcessors[i].Text != "*")
                {
                    if (int.Parse(IdealProcessors[i].Text) > wrongValue)
                    {
                        ///Invalid Ideal Processor detected
                        IdealProcessors[i].BackColor = Color.Red;
                        IdealProcessorsWrongValue = true;
                        break;
                    }
                }

                player.IdealProcessor = IdealProcessors[i].Text;

                ///Set GameProfile Processors Affinity
                if (Affinitys[i].Text == null)
                {
                    Affinitys[i].Text = "";
                }

                int maxValue = Environment.ProcessorCount;

                string[] values = Affinitys[i].Text.Split(',');
                foreach (string val in values)
                {
                    if (val != "")
                    {
                        if (int.Parse(val) > maxValue)
                        {
                            ///Invalid affinity detected
                            Affinitys[i].BackColor = Color.Red;
                            affinitysWrongValue = true;
                            break;
                        }
                    }
                }

                player.Affinity = Affinitys[i].Text;

                ///Set GameProfile Priority Classes
                if (PriorityClasses[i].Text == "" || PriorityClasses[i].Text == null)
                {
                    PriorityClasses[i].Text = "Normal";
                }

                player.PriorityClass = PriorityClasses[i].Text;
            }

            ///Warn user for empty nickame fields
            if (hasEmptyNickname)
            {
                playersTab.BringToFront();
                MessageBox.Show("Nickname fields can't be empty!");
                return;
            }

            ///Warn user for invalid Steam ids
            if (sidWrongValue)
            {
                playersTab.BringToFront();
                MessageBox.Show("Must be 17 numbers e.g. 76561199075562883 ", "Incorrect steam id format!");
                return;
            }

            ///Warn user for invalid ideal processors 
            if (IdealProcessorsWrongValue)
            {
                processorTab.BringToFront();
                MessageBox.Show("This PC do not have so much cores ;)", "Invalid CPU core value!");
                return;
            }

            ///Warn user for invalid processors affinity
            if (affinitysWrongValue)
            {
                processorTab.BringToFront();
                MessageBox.Show("This PC do not have so much cores ;)", "Invalid CPU core value!");
                return;
            }

            ///Save new added nicknames in Nicknames.json
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

            ///Save new added Steam ids in SteamIds.json
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

            ///Set shared GameProfile options
            GameProfile.Title = profileTitle.Text;
            GameProfile.Notes = notes_text.Text;
            GameProfile.AutoPlay = autoPlay.Checked;
            GameProfile.AudioDefaultSettings = audioDefaultSettingsRadio.Checked;
            GameProfile.AudioCustomSettings = audioCustomSettingsRadio.Checked;
            GameProfile.Network = cmb_Network.Text;
            GameProfile.CustomLayout_Ver = (int)numUpDownVer.Value;
            GameProfile.CustomLayout_Hor = (int)numUpDownHor.Value;
            GameProfile.CustomLayout_Max = (int)numMaxPlyrs.Value;
            GameProfile.UseNicknames = useNicksCheck.Checked;
            GameProfile.UseSplitDiv = splitDiv.Checked;
            GameProfile.HideDesktopOnly = hideDesktop.Checked;
            GameProfile.SplitDivColor = SplitColors.Text;
            GameProfile.AutoDesktopScaling = scaleOptionCbx.Checked;
            GameProfile.PauseBetweenInstanceLaunch = int.Parse(pauseBetweenInstanceLaunch_TxtBox.Text);
            GameProfile.HWndInterval = int.Parse(WindowsSetupTiming_TextBox.Text.ToString());
            GameProfile.AudioInstances.Clear();
            GameProfile.Cts_MuteAudioOnly = cts_Mute.Checked;
            GameProfile.Cts_KeepAspectRatio = cts_kar.Checked;
            GameProfile.Cts_Unfocus = cts_unfocus.Checked;

            ///Set GameProfile AudioInstances (part of shared GameProfile options)
            foreach (Control ctrl in audioCustomSettingsBox.Controls)
            {
                if (ctrl is ComboBox)
                {
                    ComboBox cmb = (ComboBox)ctrl;
                    if (audioDevices?.Count > 0 && audioDevices.Keys.Contains(cmb.Text))
                    {
                        GameProfile.AudioInstances.Add(cmb.Name, audioDevices[cmb.Text]);
                    }
                }
            }

            ///Unlock profiles list on close          
            ProfilesList.Instance.Locked = false;

            ///Update profile[#].json file
            if (GameProfile.ModeText != "New Profile")
            {
                GameProfile.UpdateGameProfile(GameProfile.Instance);
                setupScreen.gameProfilesList.Update_ProfilesList();

                ///Send control selection event to the Profiles List (update the list and reload the profile)
                MouseEventArgs eventArgs = new MouseEventArgs(MouseButtons.Left, 1, 0, 0, 0);

                Label selected = new Label();
                selected.Name = int.Parse(Regex.Match(GameProfile.ModeText, @"\d+").Value).ToString();
                selected.Text = $"{ProfilesList.PartialTitle} {selected.Name}";

                setupScreen.gameProfilesList.ProfileBtn_CheckedChanged(selected, eventArgs);
            }

            ini.IniWriteValue("Misc", "ProfileSettingsLocation", Location.X + "X" + Location.Y);

            Visible = false;
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

        public static void ProfileRefreshAudioList()
        {
            profileSettings?.RefreshAudioList();
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

            if (GameProfile.ModeText == "New Profile")
            {
                GameProfile.AudioInstances.Clear();
            }

            foreach (Control ctrl in audioCustomSettingsBox.Controls)
            {
                if (ctrl is ComboBox)
                {
                    ComboBox cmb = (ComboBox)ctrl;
                    cmb.Items.Clear();
                    cmb.Items.AddRange(audioDevices.Keys.ToArray());

                    if (GameProfile.ModeText == "New Profile")
                    {
                        if (audioDevices.Values.Contains(ini.IniReadValue("Audio", cmb.Name)))
                        {
                            cmb.SelectedItem = audioDevices.FirstOrDefault(x => x.Value == ini.IniReadValue("Audio", cmb.Name)).Key;
                            GameProfile.AudioInstances.Add(cmb.Name, ini.IniReadValue("Audio", cmb.Name));
                        }
                        else
                        {
                            cmb.SelectedItem = "Default";
                        }
                    }
                    else
                    {
                        if (GameProfile.AudioInstances.Count > 0)
                        {
                            if (GameProfile.AudioInstances.Any(device => device.Key == cmb.Name))
                            {
                                if (audioDevices.Values.Contains(GameProfile.AudioInstances[cmb.Name]))
                                {
                                    cmb.SelectedItem = audioDevices.FirstOrDefault(x => x.Value == GameProfile.AudioInstances[cmb.Name]).Key;
                                }
                            }
                            else
                            {
                                cmb.SelectedItem = "Default";
                            }
                        }
                        else
                        {
                            cmb.SelectedItem = "Default";
                        }
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

                if (!(tabsButtons[i] is Button))
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

        private void audioTabBtn_Click(object sender, EventArgs e)
        {
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDevice audioDefault = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            audioDefaultDevice.Text = "Default: " + audioDefault.FriendlyName;
        }

        private void IdealProcessor_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 32; i++)
            {
                IdealProcessors[i].BackColor = Color.White;
                Affinitys[i].BackColor = Color.Gray;
                Affinitys[i].Text = "";
            }
        }

        private void Affinity_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 32; i++)
            {
                Affinitys[i].BackColor = Color.White;
                IdealProcessors[i].BackColor = Color.Gray;
                IdealProcessors[i].SelectedIndex = 0;
            }
        }

        private void num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void Affinity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }

        private void ReadOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void closeBtnPicture_MouseEnter(object sender, EventArgs e)
        {
            closeBtnPicture.BackgroundImage = ImageCache.GetImage(mainForm.theme + "title_close_mousehover.png");
        }

        private void closeBtnPicture_MouseLeave(object sender, EventArgs e)
        {
            closeBtnPicture.BackgroundImage = ImageCache.GetImage(mainForm.theme + "title_close.png");
        }

        private void profile_info_btn_MouseEnter(object sender, EventArgs e)
        {
            profile_info_btn.BackgroundImage = ImageCache.GetImage(mainForm.theme + "profile_info_mousehover.png");
        }

        private void profile_info_btn_MouseLeave(object sender, EventArgs e)
        {
            profile_info_btn.BackgroundImage = ImageCache.GetImage(mainForm.theme + "profile_info.png");
        }

        private void profile_info_btn_Click(object sender, EventArgs e)
        {
            NucleusMessageBox.Show(
                "Info", "Profiles will only save after all instances finish setting up correctly.\n" +
                "To load a profile click the profile list icon in the setup screen and choose the profile to load by clicking on it in the list.\n" +
                "Profile settings can be modified before pressing the play button.\n" +
                "Some of the profile settings could break hooks, resizing or positioning functions so it's better to launch the game with the default profile settings once.", false);
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
                btnNext.BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "page2.png");
                page1.Visible = false;
                page2.BringToFront();
                page2.Visible = true;
                ResumeLayout();
            }
            else
            {
                SuspendLayout();
                btnNext.BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "page1.png");
                page2.Visible = false;
                page1.BringToFront();
                page1.Visible = true;
                ResumeLayout();
            }
        }

        private void btnProcessorNext_Click_1(object sender, EventArgs e)
        {
            if (processorPage1.Visible)
            {
                SuspendLayout();
                btnProcessorNext.BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "page2.png");
                processorPage1.Visible = false;
                processorPage2.BringToFront();
                processorPage2.Visible = true;
                ResumeLayout();
            }
            else
            {
                SuspendLayout();
                btnProcessorNext.BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "page1.png");
                processorPage2.Visible = false;
                processorPage1.BringToFront();
                processorPage1.Visible = true;
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

        private void ProfileSettings_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawRectangles(bordersPen, tabBorders);
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

            if (ch?.Text == "" || ch?.Text == null)
            {
                return;
            }

            if (ch != null)
            {
                if (currentSteamId != null)
                {
                    ch.Text = currentSteamId;
                }

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

            if (!NicknamesCache.Get.Any(n => n == cb.Text))
            {
                NicknamesCache.Add(cb.Text);
            }

            for (int i = 0; i < 32; i++)
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

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        private void ProfileSettings_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                User32Interop.ReleaseCapture();
                IntPtr nucHwnd = User32Interop.FindWindow(null, Text);
                User32Interop.SendMessage(nucHwnd, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, (IntPtr)0);
            }
        }

        private void modeLabel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                User32Interop.ReleaseCapture();
                IntPtr nucHwnd = User32Interop.FindWindow(null, Text);
                User32Interop.SendMessage(nucHwnd, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, (IntPtr)0);
            }
        }
    }
}