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
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Nucleus.Coop
{

    public partial class ProfileSettings : UserControl, IDynamicSized
    {
        private MainForm mainForm = null;
        private PositionsControl positionsControl;

        private List<string> nicksList = new List<string>();
        private List<string> steamIdsList = new List<string>();
        private List<string> jsonNicksList = new List<string>();
        private List<string> jsonsteamIdsList = new List<string>();

        private ComboBox[] controllerNicks;
        private ComboBox[] steamIds;
        private ComboBox[] IdealProcessors;
        private TextBox[] Affinitys;
        private ComboBox[] PriorityClasses;

        private Button highlighted;

        private float fontSize;
        private List<Control> ctrls = new List<Control>();
        private IDictionary<string, string> audioDevices;
        private Cursor hand_Cursor;
        private Cursor default_Cursor;
        private static ProfileSettings profileSettings;
        private System.Windows.Forms.Timer rainbowTimer;
        private Color selectionColor;
        private int r = 0;
        private int g = 0;
        private int b = 0;
        private bool loop = false;
        private Pen bordersPen;
        private ToolTip autoPlayTooltip;
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

        public ProfileSettings(MainForm mf, PositionsControl pc)
        {
            fontSize = float.Parse(mf.themeIni.IniReadValue("Font", "SettingsFontSize"));
            profileSettings = this;

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
                control.Font = new Font(mf.customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                control.Cursor = hand_Cursor;
            }

            ForeColor = Color.FromArgb(int.Parse(mf.rgb_font[0]), int.Parse(mf.rgb_font[1]), int.Parse(mf.rgb_font[2]));

            BackColor = Color.FromArgb(int.Parse(mf.themeIni.IniReadValue("Colors", "ProfileSettingsBackground").Split(',')[0]),
                                                   int.Parse(mf.themeIni.IniReadValue("Colors", "ProfileSettingsBackground").Split(',')[1]),
                                                   int.Parse(mf.themeIni.IniReadValue("Colors", "ProfileSettingsBackground").Split(',')[2]),
                                                   int.Parse(mf.themeIni.IniReadValue("Colors", "ProfileSettingsBackground").Split(',')[3]));

            saveBtnPicture.BackgroundImage = new Bitmap(mf.theme + "save.png");
            audioBtnPicture.BackgroundImage = new Bitmap(mf.theme + "audio.png");
            playersBtnPicture.BackgroundImage = new Bitmap(mf.theme + "players.png");
            sharedBtnPicture.BackgroundImage = new Bitmap(mf.theme + "shared.png");
            processorBtnPicture.BackgroundImage = new Bitmap(mf.theme + "processor.png");
            closeBtnPicture.BackgroundImage = new Bitmap(mf.theme + "title_close.png");
            audioRefresh.BackgroundImage = new Bitmap(mf.theme + "refresh.png");

            sharedTab.BackColor = Color.Transparent;
            playersTab.BackColor = Color.Transparent;
            audioTab.BackColor = Color.Transparent;
            audioRefresh.BackColor = Color.Transparent;
            processorTab.BackColor = Color.Transparent;
            //
            //MouseOverColor
            //
            sharedTabBtn.Click += new EventHandler(tabsButtons_highlight);
            playersTabBtn.Click += new EventHandler(tabsButtons_highlight);
            audioTabBtn.Click += new EventHandler(tabsButtons_highlight);
            processorTabBtn.Click += new EventHandler(tabsButtons_highlight);


            sharedBtnPicture.Click += new EventHandler(button1_Click);
            playersBtnPicture.Click += new EventHandler(button2_Click);
            audioBtnPicture.Click += new EventHandler(button3_Click);
            processorBtnPicture.Click += new EventHandler(button4_Click);
            saveBtnPicture.Click += new EventHandler(SettingsSaveBtn_Click);
            closeBtnPicture.Click += new EventHandler(SettingsCloseBtn_Click);

            sharedBtnPicture.Click += new EventHandler(tabsButtons_highlight);
            playersBtnPicture.Click += new EventHandler(tabsButtons_highlight);
            audioBtnPicture.Click += new EventHandler(tabsButtons_highlight);
            processorBtnPicture.Click += new EventHandler(tabsButtons_highlight);
            saveBtnPicture.Click += new EventHandler(tabsButtons_highlight);

            sharedTabBtn.FlatAppearance.MouseOverBackColor = selectionColor;
            playersTabBtn.FlatAppearance.MouseOverBackColor = selectionColor;
            audioTabBtn.FlatAppearance.MouseOverBackColor = selectionColor;
            processorTabBtn.FlatAppearance.MouseOverBackColor = selectionColor;
            closeBtn.FlatAppearance.MouseOverBackColor = selectionColor;
            saveBtn.FlatAppearance.MouseOverBackColor = selectionColor;
            audioRefresh.BackColor = Color.Transparent;

            autoPlayTooltip = new ToolTip();
            autoPlayTooltip.SetToolTip(autoPlay, "Compatible with controllers only, you must always use the same controllers for it to auto start.");

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

            foreach (Control cAddEvent in sharedTab.Controls)
            {
                if (cAddEvent.Name.Contains("pauseBetweenInstanceLaunch_TxtBox") || cAddEvent.Name.Contains("WindowsSetupTiming_TextBox"))
                {
                    cAddEvent.KeyPress += new KeyPressEventHandler(this.num_KeyPress);
                }
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

            mainForm = mf;
            positionsControl = pc;

            //network setting
            RefreshCmbNetwork();

            rainbowTimer = new System.Windows.Forms.Timer();
            rainbowTimer.Interval = (25); //millisecond                   
            rainbowTimer.Tick += new EventHandler(rainbowTimerTick);
            rainbowTimer.Start();

            sharedTab.Parent = this;
            sharedTab.Location = new Point(sharedTabBtn.Location.X - 1, sharedTabBtn.Bottom);
            playersTab.Parent = this;
            playersTab.Location = new Point(sharedTabBtn.Location.X - 1, sharedTabBtn.Bottom);
            audioTab.Parent = this;
            audioTab.Location = new Point(sharedTabBtn.Location.X - 1, sharedTabBtn.Bottom);
            processorTab.Location = new Point(sharedTabBtn.Location.X - 1, sharedTabBtn.Bottom);
            sharedTab.BringToFront();

            //default steam id list
            def_sid_comboBox.SelectedIndex = 0;

            numUpDownVer.MaxValue = 5;
            numUpDownVer.InvalidParent = true;

            numUpDownHor.MaxValue = 5;
            numUpDownHor.InvalidParent = true;

            RefreshAudioList();
            //UpdateProfileSettingsValues(false);

            string path = Path.Combine(Application.StartupPath, $"Games Profiles\\Nicknames.json");
            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);

                JArray JNicks = (JArray)JsonConvert.DeserializeObject(jsonString);

                foreach (JToken nick in JNicks)
                {
                    jsonNicksList.Add(nick.ToString());
                }
            }

            string idspath = Path.Combine(Application.StartupPath, $"Games Profiles\\SteamIds.json");
            if (File.Exists(idspath))
            {
                string jsonString = File.ReadAllText(idspath);

                JArray JIds = (JArray)JsonConvert.DeserializeObject(jsonString);

                foreach (JToken id in JIds)
                {
                    jsonsteamIdsList.Add(id.ToString());
                }
            }

            ResumeLayout();

            DPIManager.Register(this);
            DPIManager.Update(this);
        }

        public ProfileSettings()
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

                foreach (Control c in sharedTab.Controls)
                {
                    if (c.GetType() == typeof(ComboBox) || c.GetType() == typeof(TextBox) || c.GetType() == typeof(GroupBox))
                    {
                        c.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }
                }

                foreach (Control c in playersTab.Controls)
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

                foreach (Control c in processorTab.Controls)
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

                foreach (Control c in audioTab.Controls)
                {
                    if (c.GetType() == typeof(NumericUpDown) || c.GetType() == typeof(ComboBox) || c.GetType() == typeof(TextBox) || c.GetType() == typeof(GroupBox))
                    {
                        c.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);

                    }
                    else if (c.GetType() == typeof(Panel))
                    {
                        c.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }

                    else if (c.GetType() == typeof(Label) || c.GetType() == typeof(RadioButton) || c.GetType() == typeof(Button))
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

                    if (c.GetType() == typeof(ComboBox))
                    {
                        c.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }
                }

                notes_text.Size = new Size((int)(260* scale), (int)(81 * scale));
                def_sid_comboBox.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                default_sid_list_label.Location = new Point(def_sid_comboBox.Left - default_sid_list_label.Width, ((def_sid_comboBox.Location.Y + def_sid_comboBox.Height / 2) - default_sid_list_label.Height / 2) - 4);
            }

            modeLabel.Size = new Size(Width - closeBtnPicture.Right, modeLabel.Height);
            modeLabel.Location = new Point(closeBtnPicture.Right, modeLabel.Location.Y);
            audioRefresh.Location = new Point((audioTab.Width / 2) - (audioRefresh.Width / 2), audioRefresh.Location.Y);
            ResumeLayout();
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

        public static void _UpdateProfileSettingsValues(bool save)
        {
            profileSettings.UpdateProfileSettingsValues(save);
        }

        private void UpdateProfileSettingsValues(bool save)
        {
            List<long> SteamIDs = GameProfile.SteamIDs;
            List<string> Nicknames = GameProfile.Nicknames;         

            if (SteamIDs != null)
            {
                steamIdsList.Clear();
                steamIdsList.AddRange(jsonsteamIdsList); 
                for (int i = 0; i < 32; i++)
                {
                    if (i <= SteamIDs.Count - 1)
                    {
                        steamIdsList.Add(SteamIDs[i].ToString());
                    }
                }

                for (int i = 0; i < 32; i++)
                {
                    steamIds[i].Items.Clear();
                    steamIds[i].Items.AddRange(steamIdsList.ToArray());

                    if (i < SteamIDs.Count)
                    {
                        steamIds[i].SelectedItem = SteamIDs[i];
                        steamIds[i].Text = SteamIDs[i].ToString();
                    }
                    else
                    {
                        steamIds[i].SelectedItem = "";
                        steamIds[i].Text = "";
                    }
                }
            }
            else
            {
                steamIdsList.Clear();
                steamIdsList.AddRange(jsonsteamIdsList);
            }
            
            if (Nicknames != null)
            {
                nicksList.Clear();
                nicksList.AddRange(jsonNicksList);
                for (int i = 0; i < 32; i++)
                {
                    if (i <= Nicknames.Count - 1)
                    {
                        nicksList.Add(Nicknames[i]);
                    }
                    else
                    {
                        nicksList.Add("Player" + (i + 1).ToString());
                    }
                }

                for (int i = 0; i < 32; i++)
                {
                    controllerNicks[i].Items.Clear();
                    controllerNicks[i].Items.AddRange(nicksList.ToArray());

                    if (i < Nicknames.Count)
                    {
                        controllerNicks[i].SelectedItem = Nicknames[i];
                        controllerNicks[i].Text = Nicknames[i].ToString();
                    }
                    else
                    {
                        controllerNicks[i].SelectedItem = "Player" + (i + 1).ToString();
                        controllerNicks[i].Text = "Player" + (i + 1).ToString();                    
                    }
                }
            }
            else
            {
                nicksList.Clear();
                nicksList.AddRange(jsonNicksList);

                for (int i = 0; i < 32; i++)
                {
                    nicksList.Add("Player" + (i + 1).ToString());          
                }

                for (int i = 0; i < 32; i++)
                {
                    controllerNicks[i].Items.Clear();
                    controllerNicks[i].Items.AddRange(nicksList.ToArray());
                    controllerNicks[i].SelectedItem = "Player" + (i + 1).ToString();
                    controllerNicks[i].Text = "Player" + (i + 1).ToString();
                }
            }

            List<string> _IdealProcessors = GameProfile.IdealProcessors;

            if (_IdealProcessors != null)
            {
                for (int i = 0; i < 32; i++)
                {
                    if (i < _IdealProcessors.Count)
                    {
                        IdealProcessors[i].SelectedIndex = IdealProcessors[i].Items.IndexOf(GameProfile.IdealProcessors[i]);
                    }
                    else
                    {
                        IdealProcessors[i].SelectedIndex = 0;
                    }
                }
            }

            List<string> _Affinitys = GameProfile.Affinitys;

            if (_Affinitys != null)
            {
                for (int i = 0; i < 32; i++)
                {
                    if (i < _Affinitys.Count)
                    {
                        Affinitys[i].Text = GameProfile.Affinitys[i];
                    }
                    else
                    {
                        Affinitys[i].Text = "";
                    }
                }
            }

            List<string> _PriorityClasses = GameProfile.PriorityClasses;

            if (_PriorityClasses != null)
            {
                for (int i = 0; i < 32; i++)
                {
                    if (i < _PriorityClasses.Count)
                    {
                        PriorityClasses[i].SelectedIndex = PriorityClasses[i].Items.IndexOf(GameProfile.PriorityClasses[i]);
                    }
                    else
                    {
                        PriorityClasses[i].SelectedIndex = 0;
                    }
                }
            }

            foreach (Control ctrl in audioCustomSettingsBox.Controls)
            {
                if (ctrl is ComboBox)
                {
                    ComboBox cmb = (ComboBox)ctrl;

                    if (GameProfile.AudioInstances.Count > 0)
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
            }

            audioDefaultSettingsRadio.Checked = GameProfile.AudioDefaultSettings;
            audioCustomSettingsBox.Enabled = false;

            if (audioDefaultSettingsRadio.Checked == false)
            {
                audioCustomSettingsRadio.Checked = true;
            }

            autoPlay.Checked = GameProfile.AutoPlay;
            pauseBetweenInstanceLaunch_TxtBox.Text = GameProfile.PauseBetweenInstanceLaunch.ToString();
            WindowsSetupTiming_TextBox.Text = GameProfile.HWndInterval.ToString();
            keepAccountsCheck.Checked = GameProfile.KeepAccounts;
            SplitDiv.Checked = GameProfile.UseSplitDiv;
            KeepSymLinkCheckBox.Checked = GameProfile.KeepSymLink;
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

            if (GameProfile.currentProfile != null)
                modeLabel.Text = GameProfile.ModeText;

            if (save)
            {
                SettingsSaveBtn_Click(null, null);
            }
        }

        public void SettingsSaveBtn_Click(object sender, EventArgs e)
        {
            GameProfile.Nicknames.Clear();
            for (int i = 0; i < 32; i++)
            {
                GameProfile.Nicknames.Add(controllerNicks[i].Text);
                
                if (!jsonNicksList.Any(n => n == controllerNicks[i].Text) && controllerNicks[i].Text.ToString() != $"Player{i+1}")
                {
                    jsonNicksList.Add(controllerNicks[i].Text);
                }
            }

            string path = Path.Combine(Application.StartupPath, $"Games Profiles\\Nicknames.json");
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string json = JsonConvert.SerializeObject(jsonNicksList, Formatting.Indented);
                    writer.Write(json);
                    stream.Flush();
                }
            }

            bool sidWrongValue = false;
            GameProfile.SteamIDs.Clear();
            for (int i = 0; i < 32; i++)
            {
                if (Regex.IsMatch(steamIds[i].Text, "^[0-9]+$") && steamIds[i].Text.Length == 17 || steamIds[i].Text.Length == 0 || steamIds[i].Text == "0")
                {
                    if (steamIds[i].Text != "")
                    {
                        GameProfile.SteamIDs.Add(long.Parse(steamIds[i].Text.ToString()));
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

            string idspath = Path.Combine(Application.StartupPath, $"Games Profiles\\SteamIds.json");
            using (FileStream stream = new FileStream(idspath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    string json = JsonConvert.SerializeObject(jsonsteamIdsList, Formatting.Indented);
                    writer.Write(json);
                    stream.Flush();
                }
            }

            if (sidWrongValue)
            {
                GameProfile.SteamIDs.Clear();
                playersTab.BringToFront();
                MessageBox.Show("Must be 17 numbers e.g. 76561199075562883 ", "Incorrect steam id format!");
                return;
            }

            bool ipWrongValue = false;
            GameProfile.IdealProcessors.Clear();
            for (int i = 0; i < 32; i++)
            {
                if (IdealProcessors[i].Text == "")
                {
                    IdealProcessors[i].Text = "*";
                }

                int wrongValue = Environment.ProcessorCount;
                if (IdealProcessors[i].Text != "*")
                {
                    if (int.Parse(IdealProcessors[i].Text) > wrongValue)
                    {
                        IdealProcessors[i].BackColor = Color.Red;
                        ipWrongValue = true;
                    }
                }

                GameProfile.IdealProcessors.Add(IdealProcessors[i].Text);
            }

            if (ipWrongValue)
            {
                GameProfile.IdealProcessors.Clear();
                processorTab.BringToFront();
                MessageBox.Show("This PC do not have so much cores ;)", "Invalid CPU core value!");
                return;
            }

            bool afWrongValue = false;
            GameProfile.Affinitys.Clear();
            for (int i = 0; i < 32; i++)
            {
                int maxValue = Environment.ProcessorCount;
                string[] values = Affinitys[i].Text.Split(',');
                foreach (string val in values)
                {
                    if (val != "")
                    {
                        if (int.Parse(val) > maxValue)
                        {
                            Affinitys[i].BackColor = Color.Red;

                            afWrongValue = true;
                        }
                    }
                }

                GameProfile.Affinitys.Add(Affinitys[i].Text);
            }

            if (afWrongValue)
            {
                processorTab.BringToFront();
                GameProfile.Affinitys.Clear();
                MessageBox.Show("This PC do not have so much cores ;)", "Invalid CPU core value!");
                return;
            }

            GameProfile.PriorityClasses.Clear();
            for (int i = 0; i < 32; i++)
            {
                GameProfile.PriorityClasses.Add(PriorityClasses[i].Text);
            }

            GameProfile.Notes = notes_text.Text;
            GameProfile.AutoPlay = autoPlay.Checked;
            GameProfile.AudioDefaultSettings = audioDefaultSettingsRadio.Checked;
            GameProfile.AudioCustomSettings = audioCustomSettingsRadio.Checked;
            GameProfile.Network = cmb_Network.Text;
            GameProfile.CustomLayout_Ver = (int)numUpDownVer.Value;
            GameProfile.CustomLayout_Hor = (int)numUpDownHor.Value;
            GameProfile.CustomLayout_Max = (int)numMaxPlyrs.Value;
            GameProfile.UseNicknames = useNicksCheck.Checked;
            GameProfile.KeepAccounts = keepAccountsCheck.Checked;
            GameProfile.UseSplitDiv = SplitDiv.Checked;
            GameProfile.SplitDivColor = SplitColors.Text;
            GameProfile.AutoDesktopScaling = scaleOptionCbx.Checked;
            GameProfile.PauseBetweenInstanceLaunch = int.Parse(pauseBetweenInstanceLaunch_TxtBox.Text);
            GameProfile.HWndInterval = int.Parse(WindowsSetupTiming_TextBox.Text.ToString());
            GameProfile.KeepSymLink = KeepSymLinkCheckBox.Checked;
            GameProfile.AudioInstances.Clear();
            GameProfile.Cts_MuteAudioOnly = cts_Mute.Checked;
            GameProfile.Cts_KeepAspectRatio = cts_kar.Checked;
            GameProfile.Cts_Unfocus = cts_unfocus.Checked;

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

            if (sender != null)
                Globals.MainOSD.Settings(500, Color.LimeGreen, "Profile Settings Saved");
        }

        private void Steamid_Click(object sender, EventArgs e)
        {
            ComboBox id = (ComboBox)sender;
            id.BackColor = Color.White;
        }

        private void SettingsCloseBtn_Click(object sender, EventArgs e)
        {
            ProfilesList.profilesList.Locked = false;
            Visible = false;
        }

        private void Btn_Refresh_Click(object sender, EventArgs e)
        {
            UpdateProfileSettingsValues(false);
        }

        private void layoutSizer_Paint(object sender, PaintEventArgs e)
        {
            Graphics gs = e.Graphics;

            Pen p = new Pen(new SolidBrush(Color.White));
            int LayoutHeight = layoutSizer.Size.Height - 20;
            int LayoutWidth = layoutSizer.Size.Width - 20;

            Rectangle outline = new Rectangle(10, 10, LayoutWidth, LayoutHeight);

            gs.DrawRectangle(p, outline);

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
                gs.DrawLine(p, 10, hlines[i], 10 + LayoutWidth, hlines[i]);
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
                    else if (GameProfile.AudioInstances.Count > 0)
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
            sharedTab.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            playersTab.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RefreshAudioList();
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDevice audioDefault = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            audioDefaultDevice.Text = "Default: " + audioDefault.FriendlyName;
            audioTab.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            processorTab.BringToFront();
        }

        private void IdealProcessor_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 32; i++)
            {
                IdealProcessors[i].BackColor = Color.White;
                Affinitys[i].BackColor = Color.Gray;
                Affinitys[i].Text = "";

                if (i < GameProfile.Affinitys.Count)
                {
                    GameProfile.Affinitys[i] = "";
                }
            }
        }

        private void Affinity_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 32; i++)
            {
                Affinitys[i].BackColor = Color.White;
                IdealProcessors[i].BackColor = Color.Gray;
                IdealProcessors[i].SelectedIndex = 0;

                if (i < GameProfile.IdealProcessors.Count)
                {
                    GameProfile.IdealProcessors[i] = "*";
                }
            }
        }

        private void rainbowTimerTick(Object Object, EventArgs EventArgs)
        {
            if (!loop)
            {
                if (r < 90 && b < 90) { r += 3; b += 3; };
                if (b >= 90 && r >= 90)
                    loop = true;
            }
            else
            {
                if (r > 0 && b > 0) { r -= 3; b -= 3; }
                if (b <= 0 && r <= 0)
                    loop = false;
            }

            saveBtn.BackColor = Color.FromArgb(b, 0, r, 0);
        }

        private void ProfileSettings_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle[] tabBorders = new Rectangle[]
            {
               new Rectangle(sharedTabBtn.Location.X-1,sharedTabBtn.Location.Y-1,sharedTabBtn.Width+sharedBtnPicture.Width+2,sharedTabBtn.Height+1),
               new Rectangle(playersTabBtn.Location.X-1,playersTabBtn.Location.Y-1,playersTabBtn.Width+playersBtnPicture.Width+2,playersTabBtn.Height+1),
               new Rectangle(audioTabBtn.Location.X-1,audioTabBtn.Location.Y-1,audioTabBtn.Width+audioBtnPicture.Width+2,audioTabBtn.Height+1),
               new Rectangle(processorTabBtn.Location.X-1,processorTabBtn.Location.Y-1,processorTabBtn.Width+processorBtnPicture.Width+2,processorTabBtn.Height+1),
               new Rectangle(saveBtn.Location.X-1,saveBtn.Location.Y-1,saveBtn.Width+saveBtnPicture.Width+2,saveBtn.Height+1),
               new Rectangle(closeBtn.Location.X-1,closeBtn.Location.Y-1,closeBtn.Width+closeBtnPicture.Width+2,closeBtn.Height+1),
               new Rectangle(sharedTab.Location.X,sharedTab.Location.Y,sharedTab.Width,sharedTab.Height),
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
                GameProfile.Cts_KeepAspectRatio = false;
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