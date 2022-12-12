using NAudio.CoreAudioApi;
using Nucleus.Gaming;
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
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Nucleus.Coop
{

    public partial class Settings : UserControl, IDynamicSized
    {
        private MainForm mainForm = null;
        private IniFile ini = Globals.ini;
        private PositionsControl positionsControl;
        private ComboBox[] controllerNicks;
        private List<string> nicksList = new List<string>();
        private List<string> steamIdsList = new List<string>();
        private ComboBox[] steamIds;
        public int KillProcess_HotkeyID = 1;
        public int TopMost_HotkeyID = 2;
        public int StopSession_HotkeyID = 3;
        public int SetFocus_HotkeyID = 4;
        public int ResetWindows_HotkeyID = 5;
        private float fontSize;
        private List<Control> ctrls = new List<Control>();
        private DirectInput dinput;
        private IDictionary<string, string> audioDevices;
        private string epicLang;
        private string epicLangText;
        private string prevTheme;
        private Cursor hand_Cursor;
        private Cursor default_Cursor;
        private static Settings settings;

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

        public Settings(MainForm mf)
        {
            fontSize = float.Parse(mf.themeIni.IniReadValue("Font", "SettingsFontSize"));
            settings = this;
            InitializeComponent();

            SuspendLayout();

            default_Cursor = mf.default_Cursor;
            Cursor = default_Cursor;
            hand_Cursor = mf.hand_Cursor;

            if (mf.roundedcorners)
            {
                Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            }

            Location = new Point(mf.Location.X + mf.Width / 2 - Width / 2, mf.Location.Y + mf.Height / 2 - Height / 2);
            Visible = false;

            controlscollect();

            foreach (Control control in ctrls)
            {
                control.Font = new Font(mf.customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                control.Cursor = hand_Cursor;
            }

            ForeColor = Color.FromArgb(int.Parse(mf.rgb_font[0]), int.Parse(mf.rgb_font[1]), int.Parse(mf.rgb_font[2]));
            plus1.ForeColor = ForeColor;
            plus2.ForeColor = ForeColor;
            plus3.ForeColor = ForeColor;
            plus4.ForeColor = ForeColor;
            plus5.ForeColor = ForeColor;

            //BackgroundImage = new Bitmap(mf.theme + "other_backgrounds.jpg");
            BackColor = Color.FromArgb(190,0,0,0);
            btn_credits.BackgroundImage = mf.AppButtons;
            settingsCloseBtn.BackgroundImage = mf.AppButtons;
            settingsSaveBtn.BackgroundImage = mf.AppButtons;
            //
            //MouseOverColor
            //
            btn_credits.FlatAppearance.MouseOverBackColor = mf.MouseOverBackColor;
            settingsCloseBtn.FlatAppearance.MouseOverBackColor = mf.MouseOverBackColor;
            settingsSaveBtn.FlatAppearance.MouseOverBackColor = mf.MouseOverBackColor;


            if (mf.useButtonsBorder)
            {
                btn_credits.FlatAppearance.BorderSize = 1;
                btn_credits.FlatAppearance.BorderColor = mf.ButtonsBorderColor;
                settingsSaveBtn.FlatAppearance.BorderSize = 1;
                settingsSaveBtn.FlatAppearance.BorderColor = mf.ButtonsBorderColor;
                settingsCloseBtn.FlatAppearance.BorderSize = 1;
                settingsCloseBtn.FlatAppearance.BorderColor = mf.ButtonsBorderColor;
            }


            ResumeLayout();
        

            if (mf.mouseClick)
            {
                foreach (Control button in this.Controls) { if (button is Button) { button.Click += new System.EventHandler(this.button_Click); } }
            }

            mainForm = mf;

            string[] themeList = Directory.GetDirectories(Path.Combine(Application.StartupPath, @"gui\theme\"));

            foreach (string path in themeList)
            {
                string[] _path = path.Split('\\');
                int last = _path.Length-1;

                themeCbx.Items.AddRange(new object[] {
                    _path[last]
                });

                string[] themeName = mainForm.theme.Split('\\');
                if (_path[last] == themeName[themeName.Length-2])
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
                string[] foreground = ini.IniReadValue("Hotkeys", "ResetWindows").Split('+');
                if ((foreground[0] == "Ctrl" || foreground[0] == "Alt" || foreground[0] == "Shift") && foreground[1].Length == 1 && Regex.IsMatch(foreground[1], @"^[a-zA-Z0-9]+$"))
                {
                    r1.SelectedItem = foreground[0];
                    r2.Text = foreground[1];
                }
            }
            else
            {
                ini.IniWriteValue("Hotkeys", "ResetWindows", "");
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

            if (scale > 1.0F)
            {
                float newTabsSize = (Font.Size - 2.0f) * scale;
                float newFontSize = Font.Size * scale;
                foreach (Control c in Controls)
                {
                    if (c.GetType() == typeof(ComboBox) || c.GetType() == typeof(TextBox) || c.GetType() == typeof(GroupBox) && (c.Name != "def_sid_textBox" || c.Name != "def_sid_textBox_container"))
                    {
                        c.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);

                    }
                    else if (c.GetType() == typeof(Button))
                    {
                        c.Font = new Font(mainForm.customFont, Font.Size, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }
                }



                foreach (Control c in hotkeyBox.Controls)
                {
                    if (c.GetType() == typeof(Label))
                    {
                        c.Font = new Font(mainForm.customFont, Font.Size, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }

                    if (c.GetType() == typeof(ComboBox) || c.GetType() == typeof(TextBox))
                    {
                        c.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                        c.Size = new Size(c.Width, (c.Height + 25) * (int)scale);
                    }
                }

                nucUserPassTxt.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);

            }

            settingLabel_Container.Location = new Point((Width / 2) - (settingLabel_Container.Width / 2), settingLabel_Container.Location.Y);         
           // password_Label.Location = new Point((passwordPanel.Width / 2) - (password_Label.Width / 2), password_Label.Location.Y);
            label38.Location = new Point((hotkeyBox.Width / 2) - (label38.Width / 2), label38.Location.Y);

            ResumeLayout();
        }

        private void num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void SettingsSaveBtn_Click(object sender, EventArgs e)
        {
            ini.IniWriteValue("Hotkeys", "Close", settingsCloseCmb.SelectedItem.ToString() + "+" + settingsCloseHKTxt.Text);
            ini.IniWriteValue("Hotkeys", "Stop", settingsStopCmb.SelectedItem.ToString() + "+" + settingsStopTxt.Text);
            ini.IniWriteValue("Hotkeys", "TopMost", settingsTopCmb.SelectedItem.ToString() + "+" + settingsTopTxt.Text);
            ini.IniWriteValue("Hotkeys", "SetFocus", settingsFocusCmb.SelectedItem.ToString() + "+" + settingsFocusHKTxt.Text);
            ini.IniWriteValue("Hotkeys", "ResetWindows", r1.SelectedItem.ToString() + "+" + r2.Text);///
            ini.IniWriteValue("Hotkeys", "LockKey", comboBox_lockKey.SelectedItem.ToString());

            User32Interop.UnregisterHotKey(mainForm.Handle, KillProcess_HotkeyID);
            User32Interop.UnregisterHotKey(mainForm.Handle, TopMost_HotkeyID);
            User32Interop.UnregisterHotKey(mainForm.Handle, StopSession_HotkeyID);
            User32Interop.UnregisterHotKey(mainForm.Handle, SetFocus_HotkeyID);
            User32Interop.UnregisterHotKey(mainForm.Handle, ResetWindows_HotkeyID);

            User32Interop.RegisterHotKey(mainForm.Handle, KillProcess_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "Close").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "Close").Split('+')[1].ToString()));
            User32Interop.RegisterHotKey(mainForm.Handle, TopMost_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "TopMost").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "TopMost").Split('+')[1].ToString()));
            User32Interop.RegisterHotKey(mainForm.Handle, StopSession_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "Stop").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "Stop").Split('+')[1].ToString()));
            User32Interop.RegisterHotKey(mainForm.Handle, SetFocus_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "SetFocus").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "SetFocus").Split('+')[1].ToString()));
            User32Interop.RegisterHotKey(mainForm.Handle, ResetWindows_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "ResetWindows").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "ResetWindows").Split('+')[1].ToString()));

        
            ini.IniWriteValue("Misc", "IgnoreInputLockReminder", ignoreInputLockReminderCheckbox.Checked.ToString());
            ini.IniWriteValue("Misc", "DebugLog", debugLogCheck.Checked.ToString());
            ini.IniWriteValue("Misc", "SteamLang", cmb_Lang.SelectedItem.ToString());
            ini.IniWriteValue("Misc", "EpicLang", cmb_EpicLang.SelectedItem.ToString());
            ini.IniWriteValue("Misc", "ShowStatus", statusCheck.Checked.ToString());
            ini.IniWriteValue("Misc", "NucleusAccountPassword", nucUserPassTxt.Text);
            ini.IniWriteValue("Dev", "MouseClick", clickSoundChkB.Checked.ToString());
            ini.IniWriteValue("Dev", "SplashScreen_On", splashScreenChkB.Checked.ToString());

            mainForm.handleClickSound(clickSoundChkB.Checked);

            Globals.MainOSD.Settings(500, Color.LimeGreen, "Settings saved");
            if (themeCbx.SelectedItem.ToString() != prevTheme)
            {
                ini.IniWriteValue("Theme", "Theme", themeCbx.SelectedItem.ToString());
                mainForm.themeSwitch = true;
                Thread.Sleep(200);
                Application.Restart();
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
            mainForm.btn_settings.PerformClick();
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
                User32Interop.RegisterHotKey(form.Handle, ResetWindows_HotkeyID, GetMod(ini.IniReadValue("Hotkeys", "ResetWindows").Split('+')[0].ToString()), (int)Enum.Parse(typeof(Keys), ini.IniReadValue("Hotkeys", "ResetWindows").Split('+')[1].ToString()));
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
           "\n- Support for per-game profiles."+
           "\n- Many quality of life improvements & bug fixes." +
           "\nAnd so much more!" +
           "\nSpecial thanks to: Talos91, dr.oldboi, PoundlandBacon, Pizzo and the rest of the Splitscreen Dreams discord community.", "Credits", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }     

        private void label32_Click(object sender, EventArgs e)
        {

        }
    }
}