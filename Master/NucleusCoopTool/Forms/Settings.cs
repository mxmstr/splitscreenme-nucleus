using NAudio.CoreAudioApi;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop.InputManagement;
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
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Nucleus.Coop
{
   
    public partial class Settings : UserControl, IDynamicSized
    {

        private MainForm mainForm = null;
        private IniFile ini;
        private PositionsControl positionsControl;
        private ComboBox[] controllerNicks;
        private List<string> nicksList = new List<string>();
        private List<string> steamIdsList = new List<string>();
        private ComboBox[] steamIds;
        public int KillProcess_HotkeyID = 1;
        public int TopMost_HotkeyID = 2;
        public int StopSession_HotkeyID = 3;
        public int SetFocus_HotkeyID = 4;
        private float fontSize;
        private List<Control> ctrls = new List<Control>();
        private DirectInput dinput;
        private IDictionary<string, string> audioDevices;
        private string epicLang;
        private string epicLangText;
        private string prevTheme;

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
            mainForm.SoundPlayer(mainForm.themePath + "\\button_click.wav");
        }

        public Settings(MainForm mf, PositionsControl pc)
        {
            ini = mf.ini;
            fontSize = float.Parse(mf.theme.IniReadValue("Font", "SettingsFontSize"));
            
            InitializeComponent();
            
            SuspendLayout();

            if (mf.roundedcorners)
            {
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            }

            tabControl2.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, tabControl2.Width, tabControl2.Height, 5, 5));
            Location = new Point(mf.Location.X + mf.Width / 2 - Width / 2, mf.Location.Y + mf.Height / 2 - Height / 2);
            Visible = false;
            //form Fore Color
            controlscollect();

            foreach (Control control in ctrls)
            {
                control.Font = new Font(mf.customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
            }
           
            ForeColor = Color.FromArgb(Convert.ToInt32(mf.rgb_font[0]), Convert.ToInt32(mf.rgb_font[1]), Convert.ToInt32(mf.rgb_font[2]));
            //
            BackgroundImage = new Bitmap(mf.themePath + "\\other_backgrounds.jpg");
          
            tabPage1.BackgroundImage = new Bitmap(mf.themePath + "\\other_backgrounds.jpg");
            tabPage3.BackgroundImage = new Bitmap(mf.themePath + "\\other_backgrounds.jpg");
            tabPage4.BackgroundImage = new Bitmap(mf.themePath + "\\other_backgrounds.jpg");
            tabPage5.BackgroundImage = new Bitmap(mf.themePath + "\\other_backgrounds.jpg");
            //setting_Label.BackgroundImage = mf.AppButtons;
            btn_credits.BackgroundImage = mf.AppButtons;
            settingsCloseBtn.BackgroundImage = mf.AppButtons;
            settingsSaveBtn.BackgroundImage = mf.AppButtons;
            //
            //MouseOverColor
            //
            btn_credits.FlatAppearance.MouseOverBackColor = mf.MouseOverBackColor;
            settingsCloseBtn.FlatAppearance.MouseOverBackColor = mf.MouseOverBackColor;
            settingsSaveBtn.FlatAppearance.MouseOverBackColor = mf.MouseOverBackColor;
            //btn_Refresh.BackColor = mf.MouseOverBackColor;
            audioRefresh.BackColor = mf.MouseOverBackColor;

            if (mf.useButtonsBorder)
            {
                btn_credits.FlatAppearance.BorderSize = 1;
                btn_credits.FlatAppearance.BorderColor = mf.ButtonsBorderColor;
                settingsSaveBtn.FlatAppearance.BorderSize = 1;
                settingsSaveBtn.FlatAppearance.BorderColor = mf.ButtonsBorderColor;
                settingsCloseBtn.FlatAppearance.BorderSize = 1;
                settingsCloseBtn.FlatAppearance.BorderColor = mf.ButtonsBorderColor;
            }

            controllerNicks = new ComboBox[] { controllerOneNick, controllerTwoNick, controllerThreeNick, controllerFourNick, controllerFiveNick, controllerSixNick, controllerSevenNick, controllerEightNick, controllerNineNick, controllerTenNick, controllerElevenNick, controllerTwelveNick, controllerThirteenNick, controllerFourteenNick, controllerFifteenNick, controllerSixteenNick };
            steamIds = new ComboBox[] { steamid1, steamid2, steamid3, steamid4, steamid5, steamid6, steamid7, steamid8, steamid9, steamid10, steamid11, steamid12, steamid13, steamid14, steamid15, steamid16};

           
            ResumeLayout();

            foreach (Control stmId in tabPage5.Controls)
            {
                if (stmId.Name.Contains("steamid") && stmId.GetType() == typeof(ComboBox))
                {
                    stmId.KeyPress += new KeyPressEventHandler(this.steamid_KeyPress);
                }
            }

            prevTheme = mf.ChoosenTheme;

            if (mf.mouseClick)
            {
                foreach (Control button in this.Controls) { if (button is Button) { button.Click += new System.EventHandler(this.button_Click); } }
            }
           
            mainForm = mf;
            positionsControl = pc;

            string[] themeList = Directory.GetDirectories(Path.Combine(Application.StartupPath, @"gui\theme\"));

            foreach (string path in themeList)
            {
                string[] _path = path.Split('\\');
                int last = _path.Length - 1;

                themeCbx.Items.AddRange(new object[] {
                    _path[last]
                });

                if (_path[last] == mf.ChoosenTheme)
                {
                    themeCbx.Text = _path[last]; 
                }
            }

            if (ini.IniReadValue("CustomLayout", "SplitDiv") == "True")
            {
                SplitDiv.Checked = true;
            }
            else
            {
                SplitDiv.Checked = false;
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
                if (color.Key == ini.IniReadValue("CustomLayout", "SplitDivColor"))
                {
                    SplitColors.Text = color.Key;
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

            foreach(KeyValuePair<string, string> lang in epiclangs)
            {
                if (lang.Key == ini.IniReadValue("Misc", "EpicLang"))
                {
                    epicLangText = lang.Key;
                    epicLang = lang.Value;
                }             
            }
            //splash screen setting
            if (ini.IniReadValue("Dev", "SplashScreen_On") == "True")
            {
                splashScreenChkB.Checked = true;             
            }
            else
            {
                splashScreenChkB.Checked = false;
            }
            //mouse click sound setting
            if (ini.IniReadValue("Dev", "MouseClick") == "True")
            {
                clickSoundChkB.Checked = true;
            }
            else
            {
                clickSoundChkB.Checked = false;
            }
            //auto scale setting
            if (ini.IniReadValue("Misc", "AutoDesktopScaling") == "True")
            {
                scaleOptionCbx.Checked = true;
            }
            else
            {
                scaleOptionCbx.Checked = false;
            }
            //Custom HotKey setting
            comboBox_lockKey.Text = ini.IniReadValue("Hotkeys", "LockKey");
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

            //Controll
            GetPlayersNickName();

            if (ini.IniReadValue("CustomLayout", "HorizontalLines") != "")
            {
                numHorDiv.Value = int.Parse(ini.IniReadValue("CustomLayout", "HorizontalLines"));
            }
            if (ini.IniReadValue("CustomLayout", "VerticalLines") != "")
            {
                numVerDiv.Value = int.Parse(ini.IniReadValue("CustomLayout", "VerticalLines"));
            }
            if (ini.IniReadValue("CustomLayout", "MaxPlayers") != "")
            {
                numMaxPlyrs.Value = int.Parse(ini.IniReadValue("CustomLayout", "MaxPlayers"));
            }

            //Misc
            if (ini.IniReadValue("Misc", "UseNicksInGame") != "")
            {
                useNicksCheck.Checked = bool.Parse(ini.IniReadValue("Misc", "UseNicksInGame"));
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

            if (ini.IniReadValue("Misc", "KeepAccounts") != "")
            {
                keepAccountsCheck.Checked = bool.Parse(ini.IniReadValue("Misc", "KeepAccounts"));
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
           
            RefreshAudioList();
            
            DPIManager.Register(this);
           // DPIManager.AddForm(this);
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

            if (scale > 1.0F)
            {

                float newTabsSize = (Font.Size - 2.0f) * scale;
                float newFontSize = Font.Size * scale;

                tabControl2.Font = new Font(mainForm.customFont, newTabsSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);


                foreach (Control c in tabPage3.Controls)
                {
                    if (c.GetType() == typeof(NumericUpDown) ^ c.GetType() == typeof(ComboBox) ^ c.GetType() == typeof(TextBox) ^ c.GetType() == typeof(GroupBox))
                    {
                        c.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);

                    }
                }

                foreach (Control c in tabPage1.Controls)
                {
                    if (c.GetType() == typeof(NumericUpDown) ^ c.GetType() == typeof(ComboBox) ^ c.GetType() == typeof(TextBox) ^ c.GetType() == typeof(GroupBox))
                    {
                        c.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);

                    }
                    else if (c.GetType() == typeof(Panel))
                    {
                        c.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }

                    else if (c.GetType() == typeof(Label) ^ c.GetType() == typeof(RadioButton) ^ c.GetType() == typeof(Button))
                    {
                        c.Font = new Font(mainForm.customFont, Font.Size, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }
                }
                foreach (Control c in audioCustomSettingsBox.Controls)
                {
                    if (c.GetType() == typeof(Label))
                    {
                        c.Font = new Font(mainForm.customFont, Font.Size, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }
                }

                foreach (Control c in groupBox1.Controls)
                {
                    if (c.GetType() == typeof(Label))
                    {
                        c.Font = new Font(mainForm.customFont, Font.Size, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }
                }


                foreach (Control c in tabPage4.Controls)
                {
                    if (c.GetType() == typeof(NumericUpDown) ^ c.GetType() == typeof(ComboBox) ^ c.GetType() == typeof(TextBox) ^ c.GetType() == typeof(GroupBox) ^ c.GetType() == typeof(Panel))
                    {
                        c.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);

                    }

                }
                foreach (Control c in tabPage5.Controls)
                {
                    if (c.GetType() == typeof(NumericUpDown) ^ c.GetType() == typeof(ComboBox) ^ c.GetType() == typeof(TextBox) ^ c.GetType() == typeof(GroupBox))
                    {
                        c.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);

                    }

                    else if (c.GetType() == typeof(Button))
                    {
                        c.Font = new Font(mainForm.customFont, Font.Size, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }

                }
            }

            ResumeLayout();
        }

        //protected override void OnShown(EventArgs e)
        //{
        //    base.OnShown(e);
        //    DPIManager.Register(this);
        //}

        private void steamid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void GetPlayersNickName()
        {

            for (int i = 0; i < 16; i++)
            {
                nicksList.Add(ini.IniReadValue("ControllerMapping", "Player_" + (i + 1)).ToString());
                steamIdsList.Add(ini.IniReadValue("SteamIDs", "Player_" + (i + 1)).ToString());
            }

            for (int i = 0; i < 16; i++)
            {
                controllerNicks[i].Items.AddRange(nicksList.ToArray());
                controllerNicks[i].SelectedItem = ini.IniReadValue("ControllerMapping", "Player_" + (i + 1));
                controllerNicks[i].Text = ini.IniReadValue("ControllerMapping", "Player_" + (i + 1));
            }

            for (int i = 0; i < 16; i++)
            {
                steamIds[i].Items.AddRange(steamIdsList.ToArray());
                steamIds[i].SelectedItem = ini.IniReadValue("SteamIDs", "Player_" + (i + 1));
                steamIds[i].Text = ini.IniReadValue("SteamIDs", "Player_" + (i + 1));
            }
        }

        private void SettingsSaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                ini.IniWriteValue("Hotkeys", "Close", settingsCloseCmb.SelectedItem.ToString() + "+" + settingsCloseHKTxt.Text);
                ini.IniWriteValue("Hotkeys", "Stop", settingsStopCmb.SelectedItem.ToString() + "+" + settingsStopTxt.Text);
                ini.IniWriteValue("Hotkeys", "TopMost", settingsTopCmb.SelectedItem.ToString() + "+" + settingsTopTxt.Text);
                ini.IniWriteValue("Hotkeys", "SetFocus", settingsFocusCmb.SelectedItem.ToString() + "+" + settingsFocusHKTxt.Text);///

                User32Interop.UnregisterHotKey(mainForm.Handle, KillProcess_HotkeyID);
                User32Interop.UnregisterHotKey(mainForm.Handle, TopMost_HotkeyID);
                User32Interop.UnregisterHotKey(mainForm.Handle, StopSession_HotkeyID);
                User32Interop.UnregisterHotKey(mainForm.Handle, SetFocus_HotkeyID);

                User32Interop.RegisterHotKey(mainForm.Handle, KillProcess_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "Close").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "Close").Split('+')[1].ToString()));
                User32Interop.RegisterHotKey(mainForm.Handle, TopMost_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "TopMost").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "TopMost").Split('+')[1].ToString()));
                User32Interop.RegisterHotKey(mainForm.Handle, StopSession_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "Stop").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "Stop").Split('+')[1].ToString()));
                User32Interop.RegisterHotKey(mainForm.Handle, SetFocus_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "SetFocus").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "SetFocus").Split('+')[1].ToString()));
               
                for (int i = 0; i < controllerNicks.Length; i++)
                {
                    ini.IniWriteValue("ControllerMapping", "Player_" + (i + 1), controllerNicks[i].Text);
                }

                for (int i = 0; i < steamIds.Length; i++)
                {
                    if (Regex.IsMatch(steamIds[i].Text, "^[0-9]+$") && steamIds[i].Text.Length == 17 || steamIds[i].Text.Length == 0)
                    {
                        ini.IniWriteValue("SteamIDs", "Player_" + (i + 1), steamIds[i].Text);
                    }
                    else 
                    {
                        MessageBox.Show("Must be 17 numbers e.g. 76561199075562883 ", "Incorrect steam id format!");
                        return;
                    }
                }

                nicksList.Clear();
                steamIdsList.Clear();

                for (int i = 0; i < 16; i++)
                {
                    nicksList.Add(ini.IniReadValue("ControllerMapping", "Player_" + (i + 1)).ToString());
                    steamIdsList.Add(ini.IniReadValue("SteamIDs", "Player_" + (i + 1)).ToString());

                    controllerNicks[i].Items.Clear();
                    steamIds[i].Items.Clear();
                }

                for (int i = 0; i < 16; i++)
                {
                    controllerNicks[i].Items.AddRange(nicksList.ToArray());
                    steamIds[i].Items.AddRange(steamIdsList.ToArray());
                }

                if (positionsControl != null)
                {
                    positionsControl.Refresh();
                }

                ini.IniWriteValue("Misc", "UseNicksInGame", useNicksCheck.Checked.ToString());
                ini.IniWriteValue("Misc", "IgnoreInputLockReminder", ignoreInputLockReminderCheckbox.Checked.ToString());
                ini.IniWriteValue("Misc", "DebugLog", debugLogCheck.Checked.ToString());
                ini.IniWriteValue("Misc", "Network", cmb_Network.SelectedItem.ToString());
                ini.IniWriteValue("Misc", "SteamLang", cmb_Lang.SelectedItem.ToString());
                ini.IniWriteValue("Misc", "EpicLang", cmb_EpicLang.SelectedItem.ToString());
                ini.IniWriteValue("Misc", "ShowStatus", statusCheck.Checked.ToString());
                ini.IniWriteValue("Misc", "KeepAccounts", keepAccountsCheck.Checked.ToString());
                ini.IniWriteValue("Misc", "NucleusAccountPassword", nucUserPassTxt.Text);
                ini.IniWriteValue("CustomLayout", "HorizontalLines", numHorDiv.Value.ToString());
                ini.IniWriteValue("CustomLayout", "VerticalLines", numVerDiv.Value.ToString());
                ini.IniWriteValue("CustomLayout", "MaxPlayers", numMaxPlyrs.Value.ToString());

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

                if (clickSoundChkB.Checked)
                {
                    ini.IniWriteValue("Dev", "MouseClick", "True");
                    mainForm.handleClickSound(true);
                }
                else
                {
                    ini.IniWriteValue("Dev", "MouseClick", "False");
                    mainForm.handleClickSound(false);
                }

                if (splashScreenChkB.Checked)
                {
                    ini.IniWriteValue("Dev", "SplashScreen_On", "True");
                }
                else
                {
                    ini.IniWriteValue("Dev", "SplashScreen_On", "False");
                }

                MessageBox.Show("Settings saved succesfully!", "Saved", MessageBoxButtons.OK, MessageBoxIcon.None);

                if (themeCbx.SelectedItem.ToString() != prevTheme)
                {
                    ini.IniWriteValue("Theme", "Theme", themeCbx.SelectedItem.ToString());
                    
                    Application.Restart();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void SettingsCloseBtn_Click(object sender, EventArgs e)
        {
            Hide();               
        }

        private void SettingsCloseHKTxt_TextChanged(object sender, EventArgs e)
        {
            settingsCloseHKTxt.Text = string.Concat(settingsCloseHKTxt.Text.Where(char.IsLetterOrDigit));
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

        private void SettingsCloseHKTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            settingsCloseHKTxt.Text = "";
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void SettingsStopTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            settingsStopTxt.Text = "";
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void SettingsTopTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            settingsTopTxt.Text = "";
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void SettingsFocusTxt_KeyPress(object sender, KeyPressEventArgs e)
        {
            settingsTopTxt.Text = "";
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
             && !char.IsSeparator(e.KeyChar) && !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void Btn_Refresh_Click(object sender, EventArgs e)
        {
            GetPlayersNickName();
        }

        private void Btn_credits_Click(object sender, EventArgs e)
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
               "\n- Many quality of life improvements & bug fixes." +
               "\nAnd so much more!" +
               "\nSpecial thanks to: Talos91, dr.oldboi, PoundlandBacon, Pizzo and the rest of the Splitscreen Dreams discord community.", "Credits", MessageBoxButtons.OK, MessageBoxIcon.Information);           
        }

        private void NumHorDiv_ValueChanged(object sender, EventArgs e)
        {    
            layoutSizer.Invalidate();
            tabControl2.TabPages[1].Invalidate();
            numMaxPlyrs.Value = (numHorDiv.Value + 1) * (numVerDiv.Value + 1);
        }

        private void NumVerDiv_ValueChanged(object sender, EventArgs e)
        {         
            layoutSizer.Invalidate();
            tabControl2.TabPages[1].Invalidate();
            numMaxPlyrs.Value = (numHorDiv.Value + 1) * (numVerDiv.Value + 1);
        }

        private void layoutSizer_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics gs = e.Graphics;

            Pen p = new Pen(new SolidBrush(Color.White));
            int LayoutHeight = layoutSizer.Size.Height - 20;
            int LayoutWidth = layoutSizer.Size.Width - 20;
            int LayoutPosX = layoutSizer.Location.X + 10;
            int LayoutPosY = layoutSizer.Location.Y + 10;


            Rectangle outline = new Rectangle(10, 10, LayoutWidth, LayoutHeight);
            // Rectangle outline = new Rectangle(370, 45, 360, 240);
            gs.DrawRectangle(p, outline);

            int[] hlines = new int[(int)numHorDiv.Value];
            int[] vlines = new int[(int)numVerDiv.Value];

            for (int i = 0; i < (int)numHorDiv.Value; i++)
            {
                int divisions = (int)numHorDiv.Value + 1;

                //370-380
                int y = (LayoutHeight / divisions);
                if (i == 0)
                {
                    hlines[i] = y + 10;
                }
                else
                {
                    hlines[i] = y + hlines[i - 1];
                }
                gs.DrawLine(p, 10, hlines[i], 10 + LayoutWidth, hlines[i]);
            }

            for (int i = 0; i < (int)numVerDiv.Value; i++)
            {

                int divisions = (int)numVerDiv.Value + 1;

                //45-460
                int x = (LayoutWidth / divisions);
                if (i == 0)
                {
                    vlines[i] = x + 10;
                }
                else
                {
                    vlines[i] = x + vlines[i - 1];
                }
                gs.DrawLine(p, vlines[i], 10, vlines[i], 10 + LayoutHeight);
            }

            p.Dispose();
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

        private void audioCustomSettingsRadio_CheckedChanged(object sender, EventArgs e)
        {
            if (audioCustomSettingsRadio.Checked)
            {
                audioCustomSettingsBox.Enabled = true;
            }
            else
            {
                audioCustomSettingsBox.Enabled = false;
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedTab.Text == "Audio")
            {
                RefreshAudioList();
                MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
                MMDevice audioDefault = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
                audioDefaultDevice.Text = "Default: " + audioDefault.FriendlyName;
            }
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (SplitDiv.Checked)
            {               
                ini.IniWriteValue("CustomLayout", "SplitDiv", "True");
            }
            else 
            {
                ini.IniWriteValue("CustomLayout", "SplitDiv", "False");
            }
        }

        private void SplitColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            ini.IniWriteValue("CustomLayout", "SplitDivColor", SplitColors.SelectedItem.ToString());
        }

        private void comboBox_lockKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            ini.IniWriteValue("Hotkeys", "LockKey", comboBox_lockKey.SelectedItem.ToString());
        }

        private void scaleOptionCbx_CheckedChanged(object sender, EventArgs e)
        {
            if (scaleOptionCbx.Checked)
            {
                ini.IniWriteValue("Misc", "AutoDesktopScaling", "True");
            }
            else
            {
                ini.IniWriteValue("Misc", "AutoDesktopScaling", "False");
            }

        }
    }
}
