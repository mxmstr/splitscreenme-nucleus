using NAudio.CoreAudioApi;
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
        private IniFile ini = Globals.ini;
        private PositionsControl positionsControl;
      
        private List<string> nicksList = new List<string>();
        private List<string> steamIdsList = new List<string>();
        
        private ComboBox[] controllerNicks;
        private ComboBox[] steamIds;
        private ComboBox[] IdealProcessors;
        private ComboBox[] Affinitys;
        private ComboBox[] PriorityClasses;

        private Button highlighted;

        private float fontSize;
        private List<Control> ctrls = new List<Control>();
        private IDictionary<string, string> audioDevices;
        private Cursor hand_Cursor;
        private Cursor default_Cursor;
        private static ProfileSettings settings;
        private System.Windows.Forms.Timer rainbowTimer;
        private  Color selectionColor;
        private int r = 0;
        private int g = 0;
        private int b = 0;
        private bool loop = false;
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

        public ProfileSettings(MainForm mf, PositionsControl pc)
        {
            fontSize = float.Parse(mf.themeIni.IniReadValue("Font", "SettingsFontSize"));
            settings = this;

            InitializeComponent();

            SuspendLayout();

            default_Cursor = mf.default_Cursor;
            Cursor = default_Cursor;
            hand_Cursor = mf.hand_Cursor;
            var rgb_selectionColor = mf.themeIni.IniReadValue("Colors", "Selection").Split(',');
            var borderscolor = mf.themeIni.IniReadValue("Colors", "profileSettingsBorder").Split(',');
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

            //BackgroundImage = new Bitmap(mf.theme + "other_backgrounds.jpg");
            BackColor = Color.FromArgb(190, 0, 0, 0);
            saveBtnPicture.BackgroundImage = new Bitmap(mf.theme + "save.png");
            audioBtnPicture.BackgroundImage = new Bitmap(mf.theme + "audio.png");
            playersBtnPicture.BackgroundImage = new Bitmap(mf.theme + "players.png");
            sharedBtnPicture.BackgroundImage = new Bitmap(mf.theme + "shared.png");
            prioritiesBtnPicture.BackgroundImage = new Bitmap(mf.theme + "priorities.png");
            closeBtnPicture.BackgroundImage = new Bitmap(mf.theme + "title_close.png");
            audioRefresh.BackgroundImage = new Bitmap(mf.theme + "refresh.png");

            Panel1.BackColor = Color.Transparent;
            Panel2.BackColor = Color.Transparent;
            Panel3.BackColor = Color.Transparent;
            audioRefresh.BackColor = Color.Transparent;
            priorities.BackColor = Color.Transparent;
            //
            //MouseOverColor
            //
            sharedBtn.Click += new EventHandler(tabsButtons_highlight);
            playersBtn.Click += new EventHandler(tabsButtons_highlight);
            audioBtn.Click += new EventHandler(tabsButtons_highlight);
            prioritiesBtn.Click += new EventHandler(tabsButtons_highlight);


            sharedBtnPicture.Click += new EventHandler(button1_Click);
            playersBtnPicture.Click += new EventHandler(button2_Click);
            audioBtnPicture.Click += new EventHandler(button3_Click);
            prioritiesBtnPicture.Click += new EventHandler(button4_Click);
            saveBtnPicture.Click += new EventHandler(SettingsSaveBtn_Click);
            closeBtnPicture.Click += new EventHandler(SettingsCloseBtn_Click);

            sharedBtnPicture.Click += new EventHandler(tabsButtons_highlight);
            playersBtnPicture.Click += new EventHandler(tabsButtons_highlight);
            audioBtnPicture.Click += new EventHandler(tabsButtons_highlight);
            prioritiesBtnPicture.Click += new EventHandler(tabsButtons_highlight);
            saveBtnPicture.Click += new EventHandler(tabsButtons_highlight);

            sharedBtn.FlatAppearance.MouseOverBackColor = selectionColor;
            playersBtn.FlatAppearance.MouseOverBackColor = selectionColor;
            audioBtn.FlatAppearance.MouseOverBackColor = selectionColor;
            prioritiesBtn.FlatAppearance.MouseOverBackColor = selectionColor;      
            closeBtn.FlatAppearance.MouseOverBackColor = selectionColor; 
            saveBtn.FlatAppearance.MouseOverBackColor = selectionColor;
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

            IdealProcessors = new ComboBox[] {
                IdealProcessor1, IdealProcessor2, IdealProcessor3, IdealProcessor4, IdealProcessor5, IdealProcessor6, IdealProcessor7, IdealProcessor8,
                IdealProcessor9, IdealProcessor10, IdealProcessor11, IdealProcessor12, IdealProcessor13, IdealProcessor14, IdealProcessor15,IdealProcessor16,
                IdealProcessor17, IdealProcessor18, IdealProcessor19, IdealProcessor20, IdealProcessor21, IdealProcessor22, IdealProcessor23, IdealProcessor24,
                IdealProcessor25, IdealProcessor26, IdealProcessor27, IdealProcessor28, IdealProcessor29, IdealProcessor30, IdealProcessor31, IdealProcessor32};

            Affinitys = new ComboBox[] {
                Affinity1, Affinity2, Affinity3, Affinity4, Affinity5, Affinity6, Affinity7, Affinity8,
                Affinity9, Affinity10, Affinity11, Affinity12, Affinity13, Affinity14, Affinity15,Affinity16,
                Affinity17, Affinity18, Affinity19, Affinity20, Affinity21, Affinity22, Affinity23, Affinity24,
                Affinity25, Affinity26, Affinity27, Affinity28, Affinity29, Affinity30, Affinity31, Affinity32};

            PriorityClasses = new ComboBox[] {
                PriorityClass1, PriorityClass2, PriorityClass3, PriorityClass4, PriorityClass5, PriorityClass6, PriorityClass7, PriorityClass8,
                PriorityClass9, PriorityClass10, PriorityClass11,PriorityClass12, PriorityClass13, PriorityClass14, PriorityClass15,PriorityClass16,
                Affinity17, Affinity18, Affinity19, Affinity20, Affinity21, Affinity22, Affinity23, Affinity24,
                PriorityClass25, PriorityClass26, PriorityClass27, PriorityClass28, PriorityClass29, PriorityClass30, PriorityClass31, PriorityClass32};

            

            //for(int p = 0; p < Environment.ProcessorCount;p++)
            //{
            //    for (int all = 0; all < IdealProcessors.Length; all++)
            //    {
            //        if(p == 0)
            //        {
            //            PriorityClasses[all].SelectedIndex = 0;
            //            IdealProcessors[all].Items.Add("*");
            //            IdealProcessors[all].SelectedIndex = 0;
            //            Affinitys[all].Items.Add("*");
            //            Affinitys[all].SelectedIndex = 0;
            //        }

            //        IdealProcessors[all].Items.Add((IdealProcessors[all].Items.Count).ToString());
            //        Affinitys[all].Items.Add((Affinitys[all].Items.Count).ToString());                 
            //    }
            //}

            foreach (Control cAddEvent in Panel1.Controls)
            {
                if (cAddEvent.Name.Contains("pauseBetweenInstanceLaunch_TxtBox") || cAddEvent.Name.Contains("WindowsSetupTiming_TextBox")) 
                {
                    cAddEvent.KeyPress += new KeyPressEventHandler(this.num_KeyPress);
                }         
            }

            foreach (Control cAddEvent in Panel2.Controls)
            {
                if (cAddEvent.Name.Contains("steamid") && cAddEvent.GetType() == typeof(ComboBox))
                {
                    cAddEvent.KeyPress += new KeyPressEventHandler(this.num_KeyPress);
                }            
            }

            if (mf.mouseClick)
            {
                foreach (Control button in this.Controls) 
                {
                    if (button is Button)
                    {
                        Button isButton = button as Button;
                        isButton.Click += new System.EventHandler(this.button_Click);
                        isButton.FlatAppearance.BorderSize = 0;//provisoir
                    }
                }
            }

            mainForm = mf;
            positionsControl = pc;
            //cmb_Network.MainItem.ReadOnly = true;
            //network setting
            RefreshCmbNetwork();

            rainbowTimer = new System.Windows.Forms.Timer();
            rainbowTimer.Interval = (25); //millisecond                   
            rainbowTimer.Tick += new EventHandler(rainbowTimerTick);
            rainbowTimer.Start();

            Panel1.Parent = this;
            Panel1.Location = new Point(sharedBtn.Location.X-1, sharedBtn.Bottom);
            Panel2.Parent = this;
            Panel2.Location = new Point(sharedBtn.Location.X-1, sharedBtn.Bottom);
            Panel3.Parent = this;
            Panel3.Location = new Point(sharedBtn.Location.X-1, sharedBtn.Bottom);
            priorities.Location = new Point(sharedBtn.Location.X - 1, sharedBtn.Bottom);
            Panel1.BringToFront();  

            //default steam id list
            def_sid_comboBox.SelectedIndex = 0;

            numUpDownVer.MaxValue = 5;
            numUpDownVer.InvalidParent = true;
  
            numUpDownHor.MaxValue = 5;
            numUpDownHor.InvalidParent = true;

            RefreshAudioList();
            UpdateProfileSettingsValues();

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
                float newTabsSize = (Font.Size - 2.0f) * scale;
                float newFontSize = Font.Size * scale;

                foreach (Control c in Panel1.Controls)
                {
                    if ( c.GetType() == typeof(ComboBox) || c.GetType() == typeof(TextBox) || c.GetType() == typeof(GroupBox))
                    {
                        c.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }
                }

                foreach (Control c in Panel2.Controls)
                {
                    if ( c.GetType() == typeof(ComboBox) || c.GetType() == typeof(TextBox) || c.GetType() == typeof(GroupBox) && (c.Name != "def_sid_textBox" || c.Name != "def_sid_textBox_container"))
                    {
                        c.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);

                    }
                    else if (c.GetType() == typeof(Button))
                    {
                        c.Font = new Font(mainForm.customFont, Font.Size, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }
                }

               
                foreach (Control c in Panel3.Controls)
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
 
                def_sid_comboBox.Font = new Font(mainForm.customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                default_sid_list_label.Location = new Point(def_sid_comboBox.Left - default_sid_list_label.Width, ((def_sid_comboBox.Location.Y + def_sid_comboBox.Height / 2) - default_sid_list_label.Height / 2) - 4);
            }

            def_sid_textBox_container.Location = new Point((Panel2.Width / 2) - (def_sid_textBox_container.Width / 2), def_sid_textBox_container.Location.Y);
            slitWarning_Label.Location = new Point((Panel1.Width / 2) - (slitWarning_Label.Width / 2), slitWarning_Label.Location.Y);
            audioRefresh.Location = new Point((Panel3.Width / 2) - (audioRefresh.Width / 2), audioRefresh.Location.Y);
            ResumeLayout();
        }

        private void num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        public static void _UpdateProfileSettingsValues()
        {
          settings.UpdateProfileSettingsValues();
        }

        private void UpdateProfileSettingsValues()
        {        
            List<long> SteamIDs = GameProfile.SteamIDs;
            List<string> Nicknames = GameProfile.Nicknames;

            if (SteamIDs != null)
            {            
                steamIdsList.Clear();
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
                        steamIds[i].SelectedItem ="";
                        steamIds[i].Text = "";
                    }                   
                }
            }
            else
            {
                steamIdsList.Clear();
            }

            if (Nicknames != null)
            {
                nicksList.Clear();
                for (int i = 0; i < 32; i++)
                {

                    if (i <= Nicknames.Count - 1)
                    {
                        nicksList.Add(Nicknames[i]);
                    }
                    else
                    {
                        nicksList.Add("Player" +(i + 1).ToString());
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

            SplitColors.Items.Add("Black");
            SplitColors.Items.Add("Gray");
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
            SplitColors.SelectedItem = GameProfile.SplitDivColor;
            scaleOptionCbx.Checked = GameProfile.AutoDesktopScaling;
            useNicksCheck.Checked = GameProfile.UseNicknames;          
            cmb_Network.SelectedItem = GameProfile.Network;
            numUpDownVer.Value = GameProfile.CustomLayout_Ver;
            numUpDownHor.Value = GameProfile.CustomLayout_Hor;
            numMaxPlyrs.Value = GameProfile.CustomLayout_Max;
            if(GameProfile.currentProfile != null)
            modeLabel.Text = GameProfile.ModeText;
        }

        private void SettingsSaveBtn_Click(object sender, EventArgs e)
        {
            GameProfile.Nicknames.Clear();
            for (int i = 0; i < 32; i++)
            {             
                GameProfile.Nicknames.Add(controllerNicks[i].Text);              
            }

            GameProfile.SteamIDs.Clear();
            for (int i = 0; i < 32; i++)
            {
                if (Regex.IsMatch(steamIds[i].Text, "^[0-9]+$") && steamIds[i].Text.Length == 17 || steamIds[i].Text.Length == 0 || steamIds[i].Text == "0")
                {
                    if (steamIds[i].Text != "")
                        GameProfile.SteamIDs.Add(long.Parse(steamIds[i].Text.ToString()));
                }
                else
                {
                    MessageBox.Show("Must be 17 numbers e.g. 76561199075562883 ", "Incorrect steam id format!");
                    return;
                }
            }

            //if (positionsControl != null && !GameProfile.IsNew)
            //{
            //    GameProfile.currentProfile.PlayerData.Clear();
            //    positionsControl.loadedProfilePlayers.Clear();
            //    positionsControl.UpdatePlayers();   
            //}

            GameProfile.AutoPlay = autoPlay.Checked;
            GameProfile.AudioDefaultSettings = audioDefaultSettingsRadio.Checked;
            GameProfile.AudioCustomSettings = audioCustomSettingsRadio.Checked;
            GameProfile.Network = cmb_Network.SelectedItem;
            GameProfile.CustomLayout_Ver = (int)numUpDownVer.Value;
            GameProfile.CustomLayout_Hor = (int)numUpDownHor.Value;
            GameProfile.CustomLayout_Max = (int)numMaxPlyrs.Value;
            GameProfile.UseNicknames = useNicksCheck.Checked;
            GameProfile.KeepAccounts = keepAccountsCheck.Checked;
            GameProfile.UseSplitDiv = SplitDiv.Checked;
            GameProfile.SplitDivColor = SplitColors.SelectedItem;
            GameProfile.AutoDesktopScaling = scaleOptionCbx.Checked;
            GameProfile.PauseBetweenInstanceLaunch = int.Parse(pauseBetweenInstanceLaunch_TxtBox.Text);
            GameProfile.HWndInterval = int.Parse(WindowsSetupTiming_TextBox.Text.ToString());
            GameProfile.KeepSymLink = KeepSymLinkCheckBox.Checked;
            GameProfile.AudioInstances.Clear();

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

            Globals.MainOSD.Settings(500,Color.LimeGreen, "Profile Settings Saved");

        }   

        private void SettingsCloseBtn_Click(object sender, EventArgs e)
        {
            mainForm.positionsControlPlayerSetup_Click(null,null);
        }
           
        private void Btn_Refresh_Click(object sender, EventArgs e)
        {
            UpdateProfileSettingsValues();
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
            //if (cmb_Network.SelectedItem == null)
            //{
            //    cmb_Network.SelectedIndex = 0;
            //}
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
                    else if(GameProfile.AudioInstances.Count > 0)                  
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
            if(sender.GetType() == typeof(PictureBox))
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
            Panel1.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;  
            Panel2.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;        
            RefreshAudioList();
            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            MMDevice audioDefault = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            audioDefaultDevice.Text = "Default: " + audioDefault.FriendlyName;      
            Panel3.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            priorities.BringToFront();
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

            saveBtn.BackColor = Color.FromArgb(b,0,r, 0);          
        }

        private void ProfileSettings_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Rectangle[] tabBorders = new Rectangle[]
            {
               new Rectangle(sharedBtn.Location.X-1,sharedBtn.Location.Y-1,sharedBtn.Width+sharedBtnPicture.Width+2,sharedBtn.Height+1),
               new Rectangle(playersBtn.Location.X-1,playersBtn.Location.Y-1,playersBtn.Width+playersBtnPicture.Width+2,playersBtn.Height+1),
               new Rectangle(audioBtn.Location.X-1,audioBtn.Location.Y-1,audioBtn.Width+audioBtnPicture.Width+2,audioBtn.Height+1),
               new Rectangle(prioritiesBtn.Location.X-1,prioritiesBtn.Location.Y-1,prioritiesBtn.Width+prioritiesBtnPicture.Width+2,prioritiesBtn.Height+1),
               new Rectangle(saveBtn.Location.X-1,saveBtn.Location.Y-1,saveBtn.Width+saveBtnPicture.Width+2,saveBtn.Height+1),
               new Rectangle(closeBtn.Location.X-1,closeBtn.Location.Y-1,closeBtn.Width+closeBtnPicture.Width+2,closeBtn.Height+1),
               new Rectangle(Panel1.Location.X,Panel1.Location.Y,Panel1.Width,Panel1.Height),
               new Rectangle(Panel2.Location.X,Panel2.Location.Y,Panel2.Width,Panel2.Height),
               new Rectangle(Panel3.Location.X,Panel3.Location.Y,Panel3.Width,Panel3.Height),

            };

            g.DrawRectangles(bordersPen, tabBorders);

            numMaxPlyrs.Value = (numUpDownHor.Value+1) * (numUpDownVer.Value+1);
        }

    }
}