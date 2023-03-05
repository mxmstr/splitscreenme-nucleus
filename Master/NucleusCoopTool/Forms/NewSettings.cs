using NAudio.CoreAudioApi;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nucleus.Gaming;
using Nucleus.Gaming.Controls;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.Generic;
using Nucleus.Gaming.Windows.Interop;
using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Nucleus.Coop
{

    public partial class NewSettings : UserControl, IDynamicSized
    {
        private IniFile ini = Globals.ini;
        private MainForm mainForm = null;
        private PositionsControl positionsControl;

        public int KillProcess_HotkeyID = 1;
        public int TopMost_HotkeyID = 2;
        public int StopSession_HotkeyID = 3;
        public int SetFocus_HotkeyID = 4;
        public int ResetWindows_HotkeyID = 5;
        public int Cutscenes_HotkeyID = 6;
        public int Switch_HotkeyID = 7;

        private List<string> nicksList = new List<string>();
        private List<string> steamIdsList = new List<string>();
        private List<string> jsonNicksList = new List<string>();
        private List<string> jsonsteamIdsList = new List<string>();
        private string prevTheme;
        private string epicLang;
        private string epicLangText;

        private List<Panel> tabs = new List<Panel>();

        private ComboBox[] controllerNicks;
        private ComboBox[] steamIds;

        private Button highlighted;

        private float fontSize;
        private List<Control> ctrls = new List<Control>();
        private IDictionary<string, string> audioDevices;
        private Cursor hand_Cursor;
        private Cursor default_Cursor;
        private static NewSettings newSettings;
        private GameProfile profile;
        private Color selectionColor;

        private Pen bordersPen;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
       (
          int nLeftRect,     // x-coordinate of upper-left corner
          int nTopRect,      // y-coordinate of upper-left corner
          int nRightRect,    // x-coordinate of lower-right corner
          int nBottomRect,   // y-coordinate of lower-right corner
          int nWidthEllipse, // width of ellipse
          int nHeightEllipse // height of ellipse
       );

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams handleparams = base.CreateParams;
                handleparams.ExStyle = 0x02000000;
                return handleparams;
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
                        }
                    }
                }
            }
        }

        public void button_Click(object sender, EventArgs e)
        {
            if (mainForm.mouseClick)
                mainForm.SoundPlayer(mainForm.theme + "button_click.wav");
        }

        public NewSettings(MainForm mf, PositionsControl pc)
        {
            fontSize = float.Parse(mf.themeIni.IniReadValue("Font", "SettingsFontSize"));
            newSettings = this;

            InitializeComponent();

            SuspendLayout();

            default_Cursor = mf.default_Cursor;
            Cursor = default_Cursor;
            hand_Cursor = mf.hand_Cursor;
            var rgb_selectionColor = mf.themeIni.IniReadValue("Colors", "Selection").Split(',');
            var borderscolor = mf.themeIni.IniReadValue("Colors", "ProfileSettingsBorder").Split(',');
            selectionColor = Color.FromArgb(int.Parse(rgb_selectionColor[0]), int.Parse(rgb_selectionColor[1]), int.Parse(rgb_selectionColor[2]), int.Parse(rgb_selectionColor[3]));
            bordersPen = new Pen(Color.FromArgb(int.Parse(borderscolor[0]), int.Parse(borderscolor[1]), int.Parse(borderscolor[2])));
            Location = new Point(mf.Location.X + mf.Width / 2 - Width / 2, mf.Location.Y + mf.Height / 2 - Height / 2);
            Visible = false;

            controlscollect();

            foreach (Control control in ctrls)
            {
                foreach (Control child in control.Controls)
                {
                    if (child.GetType() == typeof(CheckBox) || child.GetType() == typeof(Label) || child.GetType() == typeof(RadioButton))
                    {
                        child.Font = new Font(mf.customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }
                }

                if (control.GetType() == typeof(CustomNumericUpDown))
                {
                    control.Font = new Font(mf.customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                }

                if (control.Name != "settingsTab" && control.Name != "playersTab" && control.Name != "audioTab" &&
                    control.Name != "layoutTab" && control.Name != "layoutSizer"  
                    && control.GetType() != typeof(Label) && control.GetType() != typeof(TextBox))
                {
                    control.Cursor = hand_Cursor;
                }

                if (control.Name == "settingsTab" || control.Name == "playersTab" || control.Name == "audioTab" || control.Name == "layoutTab")
                {
                    tabs.Add(control as Panel);
                }

            }

            ForeColor = Color.FromArgb(int.Parse(mf.rgb_font[0]), int.Parse(mf.rgb_font[1]), int.Parse(mf.rgb_font[2]));

            BackColor = Color.FromArgb(int.Parse(mf.themeIni.IniReadValue("Colors", "ProfileSettingsBackground").Split(',')[0]),
                                                   int.Parse(mf.themeIni.IniReadValue("Colors", "ProfileSettingsBackground").Split(',')[1]),
                                                   int.Parse(mf.themeIni.IniReadValue("Colors", "ProfileSettingsBackground").Split(',')[2]),
                                                   int.Parse(mf.themeIni.IniReadValue("Colors", "ProfileSettingsBackground").Split(',')[3]));

            audioBtnPicture.BackgroundImage = new Bitmap(mf.theme + "audio.png");
            playersBtnPicture.BackgroundImage = new Bitmap(mf.theme + "players.png");
            settingsBtnPicture.BackgroundImage = new Bitmap(mf.theme + "shared.png");
            layoutBtnPicture.BackgroundImage = new Bitmap(mf.theme + "layout.png");
            closeBtnPicture.BackgroundImage = new Bitmap(mf.theme + "title_close.png");
            btn_credits.BackgroundImage = new Bitmap(mf.theme + "credits.png");
            audioRefresh.BackgroundImage = new Bitmap(mf.theme + "refresh.png");

            SettingsTab.BackColor = Color.Transparent;
            playersTab.BackColor = Color.Transparent;
            audioTab.BackColor = Color.Transparent;
            audioRefresh.BackColor = Color.Transparent;
            layoutTab.BackColor = Color.Transparent;
            //
            //MouseOverColor
            //
            settingsTabBtn.Click += new EventHandler(tabsButtons_highlight);
            playersTabBtn.Click += new EventHandler(tabsButtons_highlight);
            audioTabBtn.Click += new EventHandler(tabsButtons_highlight);
            layoutTabBtn.Click += new EventHandler(tabsButtons_highlight);


            settingsBtnPicture.Click += new EventHandler(button1_Click);
            playersBtnPicture.Click += new EventHandler(button2_Click);
            audioBtnPicture.Click += new EventHandler(button3_Click);
            layoutBtnPicture.Click += new EventHandler(button4_Click);


            settingsBtnPicture.Click += new EventHandler(tabsButtons_highlight);
            playersBtnPicture.Click += new EventHandler(tabsButtons_highlight);
            audioBtnPicture.Click += new EventHandler(tabsButtons_highlight);
            layoutBtnPicture.Click += new EventHandler(tabsButtons_highlight);


            settingsTabBtn.FlatAppearance.MouseOverBackColor = selectionColor;
            playersTabBtn.FlatAppearance.MouseOverBackColor = selectionColor;
            audioTabBtn.FlatAppearance.MouseOverBackColor = selectionColor;
            layoutTabBtn.FlatAppearance.MouseOverBackColor = selectionColor;

            audioRefresh.BackColor = Color.Transparent;


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



            SplitDiv.Checked = bool.Parse(ini.IniReadValue("CustomLayout", "SplitDiv"));
            cts_Mute.Checked = bool.Parse(ini.IniReadValue("CustomLayout", "Cts_MuteAudioOnly"));
            cts_kar.Checked = bool.Parse(ini.IniReadValue("CustomLayout", "Cts_KeepAspectRatio"));
            cts_unfocus.Checked = bool.Parse(ini.IniReadValue("CustomLayout", "Cts_Unfocus"));

            //network setting
            RefreshCmbNetwork();

            if (ini.IniReadValue("Misc", "Network") != "")
            {
                cmb_Network.Text = ini.IniReadValue("Misc", "Network");
            }
            else
            {
                cmb_Network.SelectedIndex = 0;
            }

            IDictionary<string, Color> splitColors = new Dictionary<string, Color>();

            splitColors.Add("Black", Color.Black);
            splitColors.Add("Gray", Color.DimGray);
            splitColors.Add("White", Color.White);
            splitColors.Add("Dark Blue", Color.DarkBlue);
            splitColors.Add("Blue", Color.Blue);
            splitColors.Add("Purple", Color.Purple);
            splitColors.Add("Pink", Color.Pink);
            splitColors.Add("Red", Color.Red);
            splitColors.Add("Orange", Color.Orange);
            splitColors.Add("Yellow", Color.Yellow);
            splitColors.Add("Green", Color.Green);

            foreach (KeyValuePair<string, Color> color in splitColors)
            {
                SplitColors.Items.Add(color.Key);

                if (color.Key == ini.IniReadValue("CustomLayout", "SplitDivColor"))
                {
                    SplitColors.Text = color.Key;
                }
            }

            string[] themeList = Directory.GetDirectories(Path.Combine(Application.StartupPath, @"gui\theme\"));

            foreach (string themePath in themeList)
            {
                string[] _path = themePath.Split('\\');
                int last = _path.Length - 1;

                themeCbx.Items.AddRange(new object[] {
                    _path[last]
                });

                string[] themeName = mf.theme.Split('\\');
                if (_path[last] == themeName[themeName.Length - 2])
                {
                    themeCbx.Text = _path[last];
                    prevTheme = _path[last];
                }
            }
         
            //epiclangs setting
            IDictionary<string, string> epiclangs = new Dictionary<string, string>();

            epiclangs.Add("Arabic", "ar");
            epiclangs.Add("Brazilian", "pt-BR");
            epiclangs.Add("Bulgarian", "bg");
            epiclangs.Add("Chinese", "zh");
            epiclangs.Add("Czech", "cs");
            epiclangs.Add("Danish", "da");
            epiclangs.Add("Dutch", "nl");
            epiclangs.Add("English", "en");
            epiclangs.Add("Finnish", "fi");
            epiclangs.Add("French", "fr");
            epiclangs.Add("German", "de");
            epiclangs.Add("Greek", "el");
            epiclangs.Add("Hungarian", "hu");
            epiclangs.Add("Italian", "it");
            epiclangs.Add("Japanese", "ja");
            epiclangs.Add("Koreana", "ko");
            epiclangs.Add("Norwegian", "no");
            epiclangs.Add("Polish", "pl");
            epiclangs.Add("Portuguese", "pt");
            epiclangs.Add("Romanian", "ro");
            epiclangs.Add("Russian", "ru");
            epiclangs.Add("Spanish", "es");
            epiclangs.Add("Swedish", "sv");
            epiclangs.Add("Thai", "th");
            epiclangs.Add("Turkish", "tr");
            epiclangs.Add("Ukrainian", "uk");

            foreach (KeyValuePair<string, string> lang in epiclangs)
            {
                if (lang.Key == ini.IniReadValue("Misc", "EpicLang"))
                {
                    epicLangText = lang.Key;
                    epicLang = lang.Value;
                }
            }
           
            //splash screen setting                   
            splashScreenChkB.Checked = bool.Parse(ini.IniReadValue("Dev", "SplashScreen_On"));

            //mouse click sound setting          
            clickSoundChkB.Checked = bool.Parse(ini.IniReadValue("Dev", "MouseClick"));

            useNicksCheck.Checked = bool.Parse(ini.IniReadValue("Misc", "UseNicksInGame"));

            keepAccountsCheck.Checked = bool.Parse(ini.IniReadValue("Misc", "KeepAccounts"));
           
            //auto scale setting
            scaleOptionCbx.Checked = bool.Parse(ini.IniReadValue("Misc", "AutoDesktopScaling"));
          

            //Custom HotKey setting
            comboBox_lockKey.Text = ini.IniReadValue("Hotkeys", "LockKey");

            if (ini.IniReadValue("Misc", "SteamLang") != "")
            {
                cmb_Lang.Text = ini.IniReadValue("Misc", "SteamLang");
            }
            else
            {
                cmb_Lang.SelectedIndex = 0;
            }

            if (ini.IniReadValue("Misc", "EpicLang") != "")
            {
                cmb_EpicLang.Text = epicLangText;
            }
            else
            {
                cmb_EpicLang.SelectedIndex = 0;
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

            foreach (Control cAddEvent in playersTab.Controls)
            {
                if (cAddEvent.Name.Contains("steamid") && cAddEvent.GetType() == typeof(ComboBox))
                {
                    cAddEvent.KeyPress += new KeyPressEventHandler(this.num_KeyPress);
                    cAddEvent.Click += new System.EventHandler(Steamid_Click);
                }
            }

            foreach (Control button in this.Controls)
            {
                if (button is Button)
                {
                    Button isButton = button as Button;
                    if (mf.mouseClick)
                    {
                        isButton.Click += new System.EventHandler(this.button_Click);
                    }
                    isButton.FlatAppearance.BorderSize = 0;
                }
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

            RefreshAudioList();

            mainForm = mf;
            positionsControl = pc;

            //network setting
            RefreshCmbNetwork();

            SettingsTab.Parent = this;
            SettingsTab.Location = new Point(settingsTabBtn.Location.X - 1, settingsTabBtn.Bottom);
            playersTab.Parent = this;
            playersTab.Location = new Point(settingsTabBtn.Location.X - 1, settingsTabBtn.Bottom);
            audioTab.Parent = this;
            audioTab.Location = new Point(settingsTabBtn.Location.X - 1, settingsTabBtn.Bottom);
            layoutTab.Location = new Point(settingsTabBtn.Location.X - 1, settingsTabBtn.Bottom);

            SettingsTab.BringToFront();

            default_sid_list_label.Location = new Point(def_sid_comboBox.Left - default_sid_list_label.Width, ((def_sid_comboBox.Location.Y + def_sid_comboBox.Height / 2) - default_sid_list_label.Height / 2) - 4);
        
            audioRefresh.Location = new Point((audioTab.Width / 2) - (audioRefresh.Width / 2), audioRefresh.Location.Y);

            def_sid_comboBox.SelectedIndex = 0;

            numUpDownVer.MaxValue = 5;
            numUpDownVer.InvalidParent = true;

            numUpDownHor.MaxValue = 5;
            numUpDownHor.InvalidParent = true;
          
            string path = Path.Combine(Application.StartupPath, $"games profiles\\Nicknames.json");
            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);

                JArray JNicks = (JArray)JsonConvert.DeserializeObject(jsonString);

                foreach (JToken nick in JNicks)
                {
                    jsonNicksList.Add(nick.ToString());

                }
            }

            string idspath = Path.Combine(Application.StartupPath, $"games profiles\\SteamIds.json");
            if (File.Exists(idspath))
            {
                string jsonString = File.ReadAllText(idspath);

                JArray JIds = (JArray)JsonConvert.DeserializeObject(jsonString);

                foreach (JToken id in JIds)
                {
                    jsonsteamIdsList.Add(id.ToString());
                }
            }

            GetPlayersNickNameAndSteamIds();

            SetToolTips();

            ResumeLayout();

            DPIManager.Register(this);
            DPIManager.Update(this);
        }

        public NewSettings()
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

            if (scale > 1.0F)
            {
                float newFontSize = Font.Size * scale;

                foreach (Control tab in /*playersTab.*/Controls)
                {
                    foreach (Control child in /*playersTab.*/tab.Controls)
                    {
                        if (child.GetType() == typeof(ComboBox)/* || child.GetType() == typeof(TextBox) || child.GetType() == typeof(GroupBox) && (child.Name != "def_sid_textBox")*/)
                        {
                            child.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);

                        }

                        //else if (child.GetType() == typeof(Button))
                        //{
                        //    child.Font = new Font(mainForm.customFont, Font.Size, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                        //}

                        //Console.WriteLine(child.Name);
                    }
                }

                def_sid_comboBox.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);

            }

            audioRefresh.Location = new Point((audioTab.Width / 2) - (audioRefresh.Width / 2), audioRefresh.Location.Y);

            default_sid_list_label.Location = new Point(def_sid_comboBox.Left - default_sid_list_label.Width, ((def_sid_comboBox.Location.Y + def_sid_comboBox.Height / 2) - default_sid_list_label.Height / 2) /*- 4*/);
            ResumeLayout();
        }

        private ToolTip SplitDiv_Tooltip;
        private ToolTip btn_credits_Tooltip;

        private void SetToolTips()
        {
            btn_credits_Tooltip = new ToolTip();
            btn_credits_Tooltip.InitialDelay = 100;
            btn_credits_Tooltip.ReshowDelay = 100;
            btn_credits_Tooltip.AutoPopDelay = 5000;
            btn_credits_Tooltip.SetToolTip(btn_credits, "Credits");

            SplitDiv_Tooltip = new ToolTip();
            SplitDiv_Tooltip.InitialDelay = 100;
            SplitDiv_Tooltip.ReshowDelay = 100;
            SplitDiv_Tooltip.AutoPopDelay = 5000;
            SplitDiv_Tooltip.SetToolTip(SplitDiv, "May not work for all games");
        }

        private void GetPlayersNickNameAndSteamIds()
        {         
            for (int i = 0; i < 32; i++)
            {
                nicksList.Add(ini.IniReadValue("ControllerMapping", "Player_" + (i + 1)).ToString());
                steamIdsList.Add(ini.IniReadValue("SteamIDs", "Player_" + (i + 1)).ToString());
            }

            //for (int i = 0; i < 32; i++)
            //{
            //    controllerNicks[i].Items.AddRange(jsonNicksList.ToArray());
            //    controllerNicks[i].Items.AddRange(nicksList.ToArray());
            //    controllerNicks[i].SelectedItem = ini.IniReadValue("ControllerMapping", "Player_" + (i + 1));
            //    controllerNicks[i].Text = ini.IniReadValue("ControllerMapping", "Player_" + (i + 1));
            //}

            for (int i = 0; i < 32; i++)
            {
                steamIds[i].Items.AddRange(jsonsteamIdsList.ToArray());
                steamIds[i].Items.AddRange(steamIdsList.ToArray());
                steamIds[i].SelectedItem = ini.IniReadValue("SteamIDs", "Player_" + (i + 1));
                steamIds[i].Text = ini.IniReadValue("SteamIDs", "Player_" + (i + 1));

                controllerNicks[i].Items.AddRange(jsonNicksList.ToArray());
                controllerNicks[i].Items.AddRange(nicksList.ToArray());
                controllerNicks[i].SelectedItem = ini.IniReadValue("ControllerMapping", "Player_" + (i + 1));
                controllerNicks[i].Text = ini.IniReadValue("ControllerMapping", "Player_" + (i + 1));
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
    
        private void closeBtnPicture_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Path.Combine(Application.StartupPath, $"games profiles")))
            {
                Directory.CreateDirectory(Path.Combine(Application.StartupPath, $"games profiles"));
            }

            for (int i = 0; i < 32; i++)
            {
                ini.IniWriteValue("ControllerMapping", "Player_" + (i + 1), controllerNicks[i].Text);

                if (!jsonNicksList.Any(n => n == controllerNicks[i].Text) && controllerNicks[i].Text.ToString() != $"Player{i + 1}")
                {                  
                    jsonNicksList.Add(controllerNicks[i].Text);
                }
            }

            string path = Path.Combine(Application.StartupPath, $"games profiles\\Nicknames.json");
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string json = JsonConvert.SerializeObject(jsonNicksList, Formatting.Indented);
                    writer.Write(json);
                    stream.Flush();
                }

                stream.Dispose();
            }

            bool sidWrongValue = false;

            for (int i = 0; i < 32; i++)
            {
                if (Regex.IsMatch(steamIds[i].Text, "^[0-9]+$") && steamIds[i].Text.Length == 17 || steamIds[i].Text.Length == 0 || steamIds[i].Text != "0")
                {
                    if (steamIds[i].Text != "")
                    {
                        ini.IniWriteValue("SteamIDs", "Player_" + (i + 1), steamIds[i].Text);

                        if (!jsonsteamIdsList.Any(n => n == steamIds[i].Text.ToString()))
                        {
                                                      
                            jsonsteamIdsList.Add(steamIds[i].Text.ToString());
                        }
                    }
                }
                else
                {
                    steamIds[i].BackColor = Color.Red;
                    sidWrongValue = true;
                }
            }

            string idspath = Path.Combine(Application.StartupPath, $"games profiles\\SteamIds.json");
            using (FileStream stream = new FileStream(idspath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string json = JsonConvert.SerializeObject(jsonsteamIdsList, Formatting.Indented);
                    writer.Write(json);
                    stream.Flush();
                }

                stream.Dispose();
            }

            if (sidWrongValue)
            {
                playersTab.BringToFront();
                MessageBox.Show("Must be 17 numbers e.g. 76561199075562883 ", "Incorrect steam id format!");
                return;
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

            ini.IniWriteValue("Hotkeys", "Close", settingsCloseCmb.SelectedItem.ToString() + "+" + settingsCloseHKTxt.Text);
            ini.IniWriteValue("Hotkeys", "Stop", settingsStopCmb.SelectedItem.ToString() + "+" + settingsStopTxt.Text);
            ini.IniWriteValue("Hotkeys", "TopMost", settingsTopCmb.SelectedItem.ToString() + "+" + settingsTopTxt.Text);
            ini.IniWriteValue("Hotkeys", "SetFocus", settingsFocusCmb.SelectedItem.ToString() + "+" + settingsFocusHKTxt.Text);
            ini.IniWriteValue("Hotkeys", "ResetWindows", r1.SelectedItem.ToString() + "+" + r2.Text);///
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
            ini.IniWriteValue("Dev", "SplashScreen_On", splashScreenChkB.Checked.ToString());
            ini.IniWriteValue("Dev", "MouseClick", clickSoundChkB.Checked.ToString());

            ini.IniWriteValue("CustomLayout", "SplitDiv", SplitDiv.Checked.ToString());
            ini.IniWriteValue("CustomLayout", "SplitDivColor", SplitColors.Text.ToString());
            ini.IniWriteValue("CustomLayout", "HorizontalLines", numUpDownHor.Value.ToString());
            ini.IniWriteValue("CustomLayout", "VerticalLines", numUpDownVer.Value.ToString());
            ini.IniWriteValue("CustomLayout", "MaxPlayers", numMaxPlyrs.Value.ToString());

            ini.IniWriteValue("CustomLayout", "Cts_MuteAudioOnly", cts_Mute.Checked.ToString());
            ini.IniWriteValue("CustomLayout", "Cts_KeepAspectRatio", cts_kar.Checked.ToString());
            ini.IniWriteValue("CustomLayout", "Cts_Unfocus", cts_unfocus.Checked.ToString());

            if (GameProfile.ModeText == "New Profile")
            {
                if (GameProfile.currentProfile != null)
                {
                    GameProfile.currentProfile.Reset();
                    ProfileSettings.UpdateProfileSettingsUiValues(true);
                }
            }

            if (themeCbx.SelectedItem.ToString() != prevTheme)
            {
                ini.IniWriteValue("Theme", "Theme", themeCbx.SelectedItem.ToString());
                Application.Restart();
            }

            mainForm.handleClickSound(clickSoundChkB.Checked);

            Globals.MainOSD.Settings(500, Color.LimeGreen, "Settings saved");

            if (themeCbx.SelectedItem.ToString() != prevTheme)
            {
                ini.IniWriteValue("Theme", "Theme", themeCbx.SelectedItem.ToString());
                mainForm.themeSwitch = true;
                Thread.Sleep(200);
                Application.Restart();
            }

            if (mainForm.Xinput_S_Setup.Visible)
            {
                mainForm.Xinput_S_Setup.Visible = false;
            }

            Visible = false;
        }

        private int GetMod(string modifier)
        {
            int mod = 0;
            switch (modifier)
            {
                case "Ctrl": // Ctrl
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

            //p.Dispose();
            gs.Dispose();
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
            if (sender.GetType() == typeof(PictureBox))
            {
                PictureBox pict = sender as PictureBox;
                foreach (Control b in Controls)
                {
                    if (b.GetType() == typeof(Button))
                    {
                        Button _button = (Button)b;

                        if (highlighted != null)
                        {
                            if (highlighted != _button)
                            {
                                highlighted.BackColor = Color.Transparent;
                            }
                        }

                        if (pict.Name.Contains(_button.Name))
                        {
                            highlighted = _button;
                            _button.BackColor = selectionColor;
                            break;
                        }
                    }
                }

                return;
            }

            Button button = sender as Button;

            if (highlighted != null)
            {
                if (highlighted != button)
                {
                    highlighted.BackColor = Color.Transparent;
                }
            }

            highlighted = button;
            button.BackColor = selectionColor;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            foreach (Panel p in tabs)
            {
                if (p.Name != "settingsTab")
                {
                    p.Visible = false;
                }
                else
                {
                    p.Visible = true;
                    p.BringToFront();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (Panel p in tabs)
            {
                if (p.Name != "playersTab")
                {
                    p.Visible = false;
                }
                else
                {
                    p.Visible = true;
                    p.BringToFront();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (Panel p in tabs)
            {
                if (p.Name != "layoutTab")
                {
                    p.Visible = false;
                }
                else
                {
                    p.Visible = true;
                    p.BringToFront();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshAudioList();
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDevice audioDefault = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            audioDefaultDevice.Text = "Default: " + audioDefault.FriendlyName;
            foreach (Panel p in tabs)
            {
                if (p.Name != "audioTab")
                {
                    p.Visible = false;
                }
                else
                {
                    p.Visible = true;
                    p.BringToFront();
                }
            }
        }

        private void ProfileSettings_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle[] tabBorders = new Rectangle[]
            {
               new Rectangle(settingsTabBtn.Location.X-1,settingsTabBtn.Location.Y-1,settingsTabBtn.Width+settingsBtnPicture.Width+2,settingsTabBtn.Height+1),
               new Rectangle(playersTabBtn.Location.X-1,playersTabBtn.Location.Y-1,playersTabBtn.Width+playersBtnPicture.Width+2,playersTabBtn.Height+1),
               new Rectangle(audioTabBtn.Location.X-1,audioTabBtn.Location.Y-1,audioTabBtn.Width+audioBtnPicture.Width+2,audioTabBtn.Height+1),
               new Rectangle(layoutTabBtn.Location.X-1,layoutTabBtn.Location.Y-1,layoutTabBtn.Width+layoutBtnPicture.Width+2,layoutTabBtn.Height+1),
               //new Rectangle(saveBtn.Location.X-1,saveBtn.Location.Y-1,saveBtn.Width+saveBtnPicture.Width+2,saveBtn.Height+1),
               //new Rectangle(closeBtn.Location.X-1,closeBtn.Location.Y-1,closeBtn.Width+closeBtnPicture.Width+2,closeBtn.Height+1),
               new Rectangle(SettingsTab.Location.X,SettingsTab.Location.Y,SettingsTab.Width,SettingsTab.Height),
               new Rectangle(playersTab.Location.X,playersTab.Location.Y,playersTab.Width,playersTab.Height),
               new Rectangle(audioTab.Location.X,audioTab.Location.Y,audioTab.Width,audioTab.Height),

            };

            g.DrawRectangles(bordersPen, tabBorders);

            numMaxPlyrs.Value = (numUpDownHor.Value + 1) * (numUpDownVer.Value + 1);
        }

        private void cts_settings1_CheckedChanged(object sender, EventArgs e)
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
        private void closeBtnPicture_MouseEnter(object sender, EventArgs e)
        {
            closeBtnPicture.BackgroundImage = new Bitmap(mainForm.theme + "title_close_mousehover.png");
        }

        private void closeBtnPicture_MouseLeave(object sender, EventArgs e)
        {
            closeBtnPicture.BackgroundImage = new Bitmap(mainForm.theme + "title_close.png");
        }

        
        private void btn_credits_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Nucleus Co-op - " + mainForm.version +
           "\n " +
           "\nOriginal Nucleus Co-op Project: Lucas Assis(lucasassislar)" +
           "\nNew Nucleus Co-op fork: ZeroFox" +
           "\nMultiple keyboards / mice & hooks: Ilyaki" +
           "\nWebsite & handler API: r - mach" +
           "\nNew UI design & bug fixes: Mikou27(nene27)" +
           "\nHandlers development & testing: Talos91, PoundlandBacon, Pizzo, dr.oldboi and many more." +
           "\nThis new & improved Nucleus Co-op brings a ton of enhancements, such as:" +
           "\n- Massive increase to the amount of compatible games, 400 + as of now." +
           "\n- Beautiful new overhauled user interface with support for themes, game covers & screenshots." +
           "\n- Support for per-game profiles." +
           "\n- Many quality of life improvements & bug fixes." +
           "\nAnd so much more!" +
           "\nSpecial thanks to: Talos91, dr.oldboi, PoundlandBacon, Pizzo and the rest of the Splitscreen Dreams discord community.", "Credits", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btn_credits_MouseEnter(object sender, EventArgs e)
        {
            btn_credits.BackgroundImage = new Bitmap(mainForm.theme + "credits_mousehover.png");
        }

        private void btn_credits_MouseLeave(object sender, EventArgs e)
        {
            btn_credits.BackgroundImage = new Bitmap(mainForm.theme + "credits.png");
        }

        private void ctrlr_shorcuts_Click(object sender, EventArgs e)
        {
            if (!mainForm.Xinput_S_Setup.Visible)
            {
                if (mainForm.Xinput_S_Setup != null)
                    mainForm.Xinput_S_Setup.Show();
            }
            else
            {
                mainForm.Xinput_S_Setup.Visible = false;
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

        private void settingsCloseHKTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //settingsCloseHKTxt.Text = "";
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void settingsStopTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //settingsStopTxt.Text = "";
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
            && !char.IsSeparator(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void settingsTopTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //settingsTopTxt.Text = "";
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void settingsFocusHKTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            //settingsTopTxt.Text = "";
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
            && !char.IsSeparator(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void settingsCloseHKTxt_TextChanged(object sender, EventArgs e)
        {
            settingsCloseHKTxt.Text = string.Concat(settingsCloseHKTxt.Text.Where(char.IsLetterOrDigit));
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
            }
            else
            {
                cts_kar.Enabled = true;
                cts_unfocus.Enabled = true;
            }
        }
    }
}