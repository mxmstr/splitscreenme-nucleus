using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Nucleus.Coop.Forms;
using Nucleus.Gaming;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.ProtoInput;
using Nucleus.Gaming.Generic.Step;
using Nucleus.Gaming.Windows;
using Nucleus.Gaming.Windows.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WindowScrape.Constants;
using System.Media;
using System.Net.NetworkInformation;
using Nucleus.Gaming.Coop.Generic;
using System.Linq;
using Microsoft.Win32;
using System.Drawing.Text;

namespace Nucleus.Coop
{
    /// <summary>
    /// Central UI class to the Nucleus Coop application
    /// </summary>
    public partial class MainForm : BaseForm, IDynamicSized
    {
        private readonly IniFile ini = new IniFile(Path.Combine(Directory.GetCurrentDirectory(), "Settings.ini"));
        private IniFile iconsIni;
        public string version = "v2.1";
        private string faq_link = "https://www.splitscreen.me/docs/faq";
        private string gameDescription;
        protected string api = "https://hub.splitscreen.me/api/v1/";
        private string NucleusEnvironmentRoot = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        private string DocumentsRoot = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string ChoosenTheme;
        public string themePath;
        public string customFont;
        public IniFile theme;

        public Form backgroundForm;
        private Settings settingsForm = null;
        private ContentManager content;
        private IGameHandler handler;
        private GameManager gameManager;
        private Dictionary<UserGameInfo, GameControl> controls;
        private SearchDisksForm form;
        private GameControl currentControl;
        private UserGameInfo currentGameInfo;
        private GenericGameInfo currentGame;
        private GameProfile currentProfile;
        private AssetsScraper getAssets;
        private List<UserInputControl> stepsList;
        private UserInputControl currentStep;
        public PositionsControl positionsControl;
        private PlayerOptionsControl optionsControl;
        private JSUserInputControl jsControl;
        private Handler hubHandler = null;
        private ScriptDownloader getId;
        private DownloadPrompt downloadPrompt;
        private SoundPlayer splayer;
        private int KillProcess_HotkeyID = 1;
        private int TopMost_HotkeyID = 2;
        private int StopSession_HotkeyID = 3;
        private int currentStepIndex;

        private List<string> profilePaths = new List<string>();
        private List<Control> ctrls = new List<Control>();
        private List<Form> backgroundForms = new List<Form>();

        private Thread handlerThread;
        public Action<IntPtr> RawInputAction { get; set; }

        private Bitmap defBackground;
        private Bitmap coverImg;
        private Bitmap screenshotImg;
        public Bitmap AppButtons;
        public bool connected;
        private bool currentlyUpdatingScript = false;
        private bool Splash_On;
        private bool stopSleep = false;
        private bool TopMostToggle = true;
        private bool formClosing;
        private bool noGamesPresent;
        public bool mouseClick;
        private bool roundedcorners;
        public bool useButtonsBorder;
        private bool DisableOfflineIcon;
        private Color ChoosenColor;

        private System.Windows.Forms.Timer DisposeTimer;//dispose splash screen timer
        private System.Windows.Forms.Timer slideshow;
        private System.Windows.Forms.Timer loadTimer;

        public string[] rgb_font;
        public string[] rgb_MouseOverColor;
        public string[] rgb_MenuStripBackColor;
        public string[] rgb_MenuStripFontColor;
        public string[] rgb_TitleBarColor;
        public string[] rgb_HandlerNoteBackColor;
        public string[] rgb_HandlerNoteFontColor;
        public string[] rgb_ButtonsBorderColor;
        public string[] rgb_ThirdPartyToolsLinks;
        public Color TitleBarColor;
        public Color MouseOverBackColor;
        public Color MenuStripBackColor;
        public Color MenuStripFontColor;
        public Color ButtonsBorderColor;
        private Color HandlerNoteBackColor;
        private Color HandlerNoteFontColor;
        public FileInfo fontPath;

        public void CheckNetCon()
        {
            System.Threading.Tasks.Task.Run(() =>// Run this in another thread to not block UI 
             {
                 try
                 {
                     Ping myPing = new Ping();
                     String host = "hub.splitscreen.me";
                     byte[] buffer = new byte[32];
                     int timeout = 1000;//3000
                     PingOptions pingOptions = new PingOptions();
                     PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                     if (reply.Status == IPStatus.Success)
                     {
                         this.connected = true;
                         myPing.Dispose();
                         return;
                     }
                     else
                     {
                         this.connected = false;
                         myPing.Dispose();
                         return;
                     }
                 }
                 catch (Exception)
                 {
                     this.connected = false;
                     return;
                 }
             });
        }


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

        public enum MachineType : ushort
        {
            IMAGE_FILE_MACHINE_UNKNOWN = 0x0,
            IMAGE_FILE_MACHINE_AM33 = 0x1d3,
            IMAGE_FILE_MACHINE_AMD64 = 0x8664,
            IMAGE_FILE_MACHINE_ARM = 0x1c0,
            IMAGE_FILE_MACHINE_EBC = 0xebc,
            IMAGE_FILE_MACHINE_I386 = 0x14c,
            IMAGE_FILE_MACHINE_IA64 = 0x200,
            IMAGE_FILE_MACHINE_M32R = 0x9041,
            IMAGE_FILE_MACHINE_MIPS16 = 0x266,
            IMAGE_FILE_MACHINE_MIPSFPU = 0x366,
            IMAGE_FILE_MACHINE_MIPSFPU16 = 0x466,
            IMAGE_FILE_MACHINE_POWERPC = 0x1f0,
            IMAGE_FILE_MACHINE_POWERPCFP = 0x1f1,
            IMAGE_FILE_MACHINE_R4000 = 0x166,
            IMAGE_FILE_MACHINE_SH3 = 0x1a2,
            IMAGE_FILE_MACHINE_SH3DSP = 0x1a3,
            IMAGE_FILE_MACHINE_SH4 = 0x1a6,
            IMAGE_FILE_MACHINE_SH5 = 0x1a8,
            IMAGE_FILE_MACHINE_THUMB = 0x1c2,
            IMAGE_FILE_MACHINE_WCEMIPSV2 = 0x169,
        }

        public static MachineType GetDllMachineType(string dllPath)
        {
            // See http://www.microsoft.com/whdc/system/platform/firmware/PECOFF.mspx
            // Offset to PE header is always at 0x3C.
            // The PE header starts with "PE\0\0" =  0x50 0x45 0x00 0x00,
            // followed by a 2-byte machine type field (see the document above for the enum).
            //
            FileStream fs = new FileStream(dllPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            fs.Seek(0x3c, SeekOrigin.Begin);
            int peOffset = br.ReadInt32();
            fs.Seek(peOffset, SeekOrigin.Begin);
            uint peHead = br.ReadUInt32();

            if (peHead != 0x00004550) // "PE\0\0", little-endian
            {
                throw new Exception("Can't find PE header");
            }

            MachineType machineType = (MachineType)br.ReadUInt16();
            br.Close();
            fs.Close();
            return machineType;
        }

        // Returns true if the exe is 64-bit, false if 32-bit, and null if unknown
        public static bool? Is64Bit(string exePath)
        {
            switch (GetDllMachineType(exePath))
            {
                case MachineType.IMAGE_FILE_MACHINE_AMD64:
                case MachineType.IMAGE_FILE_MACHINE_IA64:
                    return true;
                case MachineType.IMAGE_FILE_MACHINE_I386:
                    return false;
                default:
                    return null;
            }
        }

        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        //Movable borderless window stuffs
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public void Moveable(params Control[] controls)
        {
            foreach (var ctrl in controls)
            {
                ctrl.MouseDown += (s, e) =>
                {
                    if (e.Button == MouseButtons.Left)
                    {
                        ReleaseCapture();
                        SendMessage(ctrl.FindForm().Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                        // Checks if Y = 0, if so maximize the form
                        if (ctrl.FindForm().Location.Y == 0) { ctrl.FindForm().WindowState = FormWindowState.Maximized; }
                    }
                };
            }
        }

        public void SoundPlayer(string filePath)
        {
            splayer = new SoundPlayer(filePath);
            splayer.Play();
            splayer.Dispose();
        }

        public void button_Click(object sender, EventArgs e)
        {
            if (mouseClick)
            {
                SoundPlayer(themePath + "\\button_click.wav");
            }
        }

        private void minimizeButton(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void maximizeButton(object sender, EventArgs e)
        {
            SuspendLayout();
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;

            if (roundedcorners)
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 0, 0));
                }
                else
                {
                    Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
                }
            }
            ResumeLayout();
        }

        private void closeButton(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("SplitCalculator");

            foreach (Process SplitCalculator in processes)
            {
                SplitCalculator.Kill();
            }

            Application.Exit();
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

      

        public MainForm()
        {          
            try
            {
                //RegistryKey DocKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", true);
                //customDocumentsRoot = DocKey.GetValue("Personal").ToString();
                //Console.WriteLine(customDocumentsRoot);
                ChoosenTheme = ini.IniReadValue("Theme", "Theme");
                theme = new IniFile(Path.Combine(Directory.GetCurrentDirectory() + "\\gui\\theme\\" + ChoosenTheme, "theme.ini"));
                iconsIni = new IniFile(Path.Combine(Directory.GetCurrentDirectory() + "\\gui\\icons\\icons.ini"));
                Splash_On = Convert.ToBoolean(ini.IniReadValue("Dev", "SplashScreen_On"));
                DisableOfflineIcon = Convert.ToBoolean(ini.IniReadValue("Dev", "DisableOfflineIcon"));
                themePath = Path.Combine(Application.StartupPath, @"gui\theme\" + ChoosenTheme);
                mouseClick = Convert.ToBoolean(ini.IniReadValue("Dev", "MouseClick"));
                roundedcorners = Convert.ToBoolean(theme.IniReadValue("Misc", "UseRoundedCorners"));
                useButtonsBorder = Convert.ToBoolean(theme.IniReadValue("Misc", "UseButtonsBorder"));
                customFont = theme.IniReadValue("Font", "FontFamily");
                rgb_font = theme.IniReadValue("Colors", "Font").Split(',');
                rgb_MouseOverColor = theme.IniReadValue("Colors", "MouseOver").Split(',');
                rgb_MenuStripBackColor = theme.IniReadValue("Colors", "MenuStripBack").Split(',');
                rgb_MenuStripFontColor = theme.IniReadValue("Colors", "MenuStripFont").Split(',');
                rgb_TitleBarColor = theme.IniReadValue("Colors", "TitleBar").Split(',');
                rgb_HandlerNoteBackColor = theme.IniReadValue("Colors", "HandlerNoteBack").Split(',');
                rgb_HandlerNoteFontColor = theme.IniReadValue("Colors", "HandlerNoteFont").Split(',');
                rgb_ButtonsBorderColor = theme.IniReadValue("Colors", "ButtonsBorder").Split(',');
                float fontSize = float.Parse(theme.IniReadValue("Font", "MainFontSize")); 
               
                bool coverBorderOff = Convert.ToBoolean(theme.IniReadValue("Misc", "DisableCoverBorder"));
                bool noteBorderOff = Convert.ToBoolean(theme.IniReadValue("Misc", "DisableNoteBorder"));

                TitleBarColor = Color.FromArgb(Convert.ToInt32(rgb_TitleBarColor[0]), Convert.ToInt32(rgb_TitleBarColor[1]), Convert.ToInt32(rgb_TitleBarColor[2]));
                MouseOverBackColor = Color.FromArgb(Convert.ToInt32(rgb_MouseOverColor[0]), Convert.ToInt32(rgb_MouseOverColor[1]), Convert.ToInt32(rgb_MouseOverColor[2]));
                MenuStripBackColor = Color.FromArgb(Convert.ToInt32(rgb_MenuStripBackColor[0]), Convert.ToInt32(rgb_MenuStripBackColor[1]), Convert.ToInt32(rgb_MenuStripBackColor[2]));
                MenuStripFontColor = Color.FromArgb(Convert.ToInt32(rgb_MenuStripFontColor[0]), Convert.ToInt32(rgb_MenuStripFontColor[1]), Convert.ToInt32(rgb_MenuStripFontColor[2]));
                HandlerNoteBackColor = Color.FromArgb(Convert.ToInt32(rgb_HandlerNoteBackColor[0]), Convert.ToInt32(rgb_HandlerNoteBackColor[1]), Convert.ToInt32(rgb_HandlerNoteBackColor[2]));
                HandlerNoteFontColor = Color.FromArgb(Convert.ToInt32(rgb_HandlerNoteFontColor[0]), Convert.ToInt32(rgb_HandlerNoteFontColor[1]), Convert.ToInt32(rgb_HandlerNoteFontColor[2]));
                ButtonsBorderColor = Color.FromArgb(Convert.ToInt32(rgb_ButtonsBorderColor[0]), Convert.ToInt32(rgb_ButtonsBorderColor[1]), Convert.ToInt32(rgb_ButtonsBorderColor[2]));

              
                
                InitializeComponent();
                         
                SuspendLayout();

                if (roundedcorners)
                {
                    Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
                }

                splashTimer();

                BackColor = TitleBarColor;
                linksPanel.BackColor = BackColor;
                Font = new Font(customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                ForeColor = Color.FromArgb(Convert.ToInt32(rgb_font[0]), Convert.ToInt32(rgb_font[1]), Convert.ToInt32(rgb_font[2]));
                scriptAuthorTxt.BackColor = HandlerNoteBackColor;
                scriptAuthorTxt.ForeColor = HandlerNoteFontColor;
                //Controls Pictures

                AppButtons = new Bitmap(themePath + "\\button.png");
                splash.Image = new Bitmap(themePath + "\\splash.gif");
                clientAreaPanel.BackgroundImage = new Bitmap(themePath + "\\background.jpg");
                mainButtonFrame.BackgroundImage = new Bitmap(themePath + "\\main_buttons_frame.png");
                rightFrame.BackgroundImage = new Bitmap(themePath + "\\right_panel.png");
                game_listSizer.BackgroundImage = new Bitmap(themePath + "\\game_list.png");
                btnAutoSearch.BackgroundImage = AppButtons;
                button_UpdateAvailable.BackgroundImage = AppButtons;
                btnSearch.BackgroundImage = AppButtons;
                btn_gameOptions.BackgroundImage = AppButtons;
                btn_Download.BackgroundImage = AppButtons;
                btn_Play.BackgroundImage = AppButtons;
                btn_Extract.BackgroundImage = AppButtons;
                btn_GameDesc.BackgroundImage = AppButtons;
                btn_scriptAuthorTxt.BackgroundImage = AppButtons;
                btnBack.BackgroundImage = new Bitmap(themePath + "\\arrow_left.png");
                btn_Next.BackgroundImage = new Bitmap(themePath + "\\arrow_right.png");
                coverFrame.BackgroundImage = new Bitmap(themePath + "\\cover_layer.png");
                stepPanelPictureBox.Image = new Bitmap(themePath + "\\logo.png");
                logo.BackgroundImage = new Bitmap(themePath + "\\title_logo.png");
                btn_Discord.BackgroundImage = new Bitmap(themePath + "\\discord.png");
                btn_downloadAssets.BackgroundImage = new Bitmap(themePath + "\\title_download_assets.png");
                btn_faq.BackgroundImage = new Bitmap(themePath + "\\faq.png");
                btn_Links.BackgroundImage = new Bitmap(themePath + "\\title_dropdown_closed.png");
                btn_noHub.BackgroundImage = new Bitmap(themePath + "\\title_no_hub.png");
                btn_reddit.BackgroundImage = new Bitmap(themePath + "\\reddit.png");
                btn_SplitCalculator.BackgroundImage = new Bitmap(themePath + "\\splitcalculator.png");
                btn_thirdPartytools.BackgroundImage = new Bitmap(themePath + "\\thirdpartytools.png");
                closeBtn.BackgroundImage = new Bitmap(themePath + "\\title_close.png");
                maximizeBtn.BackgroundImage = new Bitmap(themePath + "\\title_maximize.png");
                minimizeBtn.BackgroundImage = new Bitmap(themePath + "\\title_minimize.png");
                btn_settings.BackgroundImage = new Bitmap(themePath + "\\title_settings.png");
                btn_dlFromHub.BackgroundImage = new Bitmap(themePath + "\\dlhub_btn.png");
                glowingLine0.Image = new Bitmap(themePath + "\\lightbar_top.gif");
                StepPanel.BackgroundImage = new Bitmap(themePath + "\\setup_screen.png");
                //
                btn_Extract.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
                btnAutoSearch.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
                button_UpdateAvailable.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
                btnSearch.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
                btn_gameOptions.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
                btn_Download.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
                btn_Play.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
                btnBack.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
                btn_Next.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
                btn_GameDesc.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
                btn_scriptAuthorTxt.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
                btn_dlFromHub.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
                gameContextMenuStrip.BackColor = MenuStripBackColor;
                gameContextMenuStrip.ForeColor = MenuStripFontColor;

                if (useButtonsBorder)
                {
                    btnAutoSearch.FlatAppearance.BorderSize = 1;
                    btnAutoSearch.FlatAppearance.BorderColor = ButtonsBorderColor;

                    button_UpdateAvailable.FlatAppearance.BorderSize = 1;
                    button_UpdateAvailable.FlatAppearance.BorderColor = ButtonsBorderColor;
                    btnSearch.FlatAppearance.BorderSize = 1;
                    btnSearch.FlatAppearance.BorderColor = ButtonsBorderColor;
                    btn_gameOptions.FlatAppearance.BorderSize = 1;
                    btn_gameOptions.FlatAppearance.BorderColor = ButtonsBorderColor;
                    btn_Download.FlatAppearance.BorderSize = 1;
                    btn_Download.FlatAppearance.BorderColor = ButtonsBorderColor;
                    btn_Play.FlatAppearance.BorderSize = 1;
                    btn_Play.FlatAppearance.BorderColor = ButtonsBorderColor;
                    btn_Extract.FlatAppearance.BorderSize = 1;
                    btn_Extract.FlatAppearance.BorderColor = ButtonsBorderColor;
                    btn_GameDesc.FlatAppearance.BorderSize = 1;
                    btn_GameDesc.FlatAppearance.BorderColor = ButtonsBorderColor;
                    btn_scriptAuthorTxt.FlatAppearance.BorderSize = 1;
                    btn_scriptAuthorTxt.FlatAppearance.BorderColor = ButtonsBorderColor;
                    btnBack.FlatAppearance.BorderSize = 1;
                    btnBack.FlatAppearance.BorderColor = ButtonsBorderColor;
                    btn_Next.FlatAppearance.BorderSize = 1;
                    btn_Next.FlatAppearance.BorderColor = ButtonsBorderColor;
                    btn_dlFromHub.FlatAppearance.BorderSize = 1;
                    btn_dlFromHub.FlatAppearance.BorderColor = ButtonsBorderColor;
                }


                if (coverBorderOff)
                {
                    cover.BorderStyle = BorderStyle.None;
                }

                if (noteBorderOff)
                {
                    scriptAuthorTxtSizer.BorderStyle = BorderStyle.None;
                }

                foreach (Control titleBarButtons in Controls)
                {
                    titleBarButtons.BackColor = BackColor;//avoid "glitchs" while maximizing the window (aesthetic stuff only)            
                }

                controlscollect();

                foreach (Control control in ctrls)
                {
                    if (control.Name != "btn_Links" && control.Name != "btn_thirdPartytools")//Close "third_party_tools_container" control when an other control in the form is clicked.
                    {
                        control.Font = new Font(customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                        control.Click += new EventHandler(this.this_Click);
                    }

                    control.Click += new EventHandler(button_Click);

                    if (mouseClick)
                    {
                        handleClickSound(true);
                    }
                }

                ResumeLayout();
                
                minimizeBtn.Click += new EventHandler(this.minimizeButton);
                maximizeBtn.Click += new EventHandler(this.maximizeButton);
                closeBtn.Click += new EventHandler(this.closeButton);

                defBackground = new Bitmap(clientAreaPanel.BackgroundImage);

                Moveable(this);//Make the main window moveable on screen.

                positionsControl = new PositionsControl();
                
                Settings settingsForm = new Settings(this, positionsControl);
                positionsControl.Paint += PositionsControl_Paint;

                settingsForm.RegHotkeys(this);

                controls = new Dictionary<UserGameInfo, GameControl>();
                gameManager = new GameManager(this);

                optionsControl = new PlayerOptionsControl();
                jsControl = new JSUserInputControl();

                positionsControl.OnCanPlayUpdated += StepCanPlay;
                optionsControl.OnCanPlayUpdated += StepCanPlay;
                jsControl.OnCanPlayUpdated += StepCanPlay;
                positionsControl.Click += new EventHandler(this_Click);

                getId = new ScriptDownloader(this);
                downloadPrompt = new DownloadPrompt(hubHandler, this, null, true);

                // selects the list of games, so the buttons look equal
                list_Games.Select();
                gameManager.ReorderUserProfile();
            }
            catch (Exception ex)
            {
                Log("ERROR - " + ex.Message + "\n\nSTACKTRACE: " + ex.StackTrace);
            }

            if (Splash_On)//Play intro sound
            {
                SoundPlayer(themePath + "\\intro.wav");
            }

            CenterToScreen();

            DPIManager.Register(this);
            DPIManager.AddForm(this);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckNetCon();
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
            float mainButtonFrameFont = mainButtonFrame.Font.Size * 1.0f;

            if (scale > 1.0f)
            {
                foreach (Control button in mainButtonFrame.Controls)
                {
                    button.Font = new Font(customFont, mainButtonFrameFont, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                }
            }

            scriptAuthorTxt.Font = new Font(customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
            txt_GameDesc.Font = new Font(customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);

            ResumeLayout();
        }

    
        private void splashTimer()
        {
            this.Activate();
            DisposeTimer = new System.Windows.Forms.Timer();//dispose splash screen timer

            if (Splash_On)
            {
                DisposeTimer.Interval = (3500); //millisecond                   
            }
            else
            {
                DisposeTimer.Interval = (1); //millisecond
                splash.Dispose();
            }

            DisposeTimer.Tick += new EventHandler(SplashTimerTick);
            DisposeTimer.Start();
        }
     
        public void handleClickSound(bool enable)
        {
            mouseClick = enable;
        }

        private void SplashTimerTick(Object Object, EventArgs EventArgs)
        {
          
            if (connected)
            {
                btn_noHub.Visible = false;
                btn_downloadAssets.Enabled = true;
                btn_Download.Enabled = true;
                DisposeTimer.Dispose();
            }
            else
            {
                if (!DisableOfflineIcon)
                {
                    btn_noHub.Visible = true;
                }
                btn_downloadAssets.Enabled = false;
                btn_Download.Enabled = false;
            }

            if (Splash_On && !stopSleep)
            {
                Thread.Sleep(1200);
                splash.Dispose();
                splayer.Dispose();
                stopSleep = true;
            }
        }

        private void PositionsControl_Paint(object sender, PaintEventArgs e)
        {
            if (positionsControl.isDisconnected)
            {
                DPIManager.ForceUpdate();
                positionsControl.isDisconnected = false;
            }
        }

        protected override Size DefaultSize => new Size(1050, 701);

        protected override void WndProc(ref Message m)
        {
            //TODO: if close message, kill application not just window

            if (m.Msg == 0x00FF)//WM_INPUT
            {
                RawInputAction(m.LParam);
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == KillProcess_HotkeyID)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    User32Util.ShowTaskBar();
                    Close();
                }
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == TopMost_HotkeyID)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    if (TopMostToggle && handler != null)
                    {
                        try
                        {

                            Process[] procs = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(currentGame.ExecutableName.ToLower()));
                            if (procs.Length > 0)
                            {
                                for (int i = 0; i < procs.Length; i++)
                                {
                                    IntPtr hWnd = procs[i].NucleusGetMainWindowHandle();
                                    User32Interop.SetWindowPos(hWnd, new IntPtr(-2), 0, 0, 0, 0,
                                    (uint)(PositioningFlags.SWP_NOSIZE | PositioningFlags.SWP_NOMOVE));
                                    ShowWindow(hWnd, ShowWindowEnum.Minimize);
                                }
                            }
                        }
                        catch
                        {
                        }

                        User32Util.ShowTaskBar();
                        Activate();
                        BringToFront();
                        TopMostToggle = false;
                    }
                    else if (!TopMostToggle && handler != null)
                    {
                        Process[] procs =
                            Process.GetProcessesByName(
                                Path.GetFileNameWithoutExtension(currentGame.ExecutableName.ToLower()));
                        if (procs.Length > 0)
                        {
                            for (int i = 0; i < procs.Length; i++)
                            {
                                IntPtr hWnd = procs[i].NucleusGetMainWindowHandle();
                                ShowWindow(hWnd, ShowWindowEnum.Restore);
                                User32Interop.SetWindowPos(hWnd, new IntPtr(-1), 0, 0, 0, 0,
                                    (uint)(PositioningFlags.SWP_NOSIZE | PositioningFlags.SWP_NOMOVE));
                            }
                        }

                        User32Util.HideTaskbar();
                        TopMostToggle = true;
                    }
                }
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == StopSession_HotkeyID)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    if (btn_Play.Text == "S T O P")
                    {

                        WindowState = FormWindowState.Normal;
                        BringToFront();
                        btn_Play.PerformClick();
                    }
                }
            }

            base.WndProc(ref m);
        }
        public void RefreshGames()
        {
            lock (controls)
            {
                foreach (KeyValuePair<UserGameInfo, GameControl> con in controls)
                {
                    if (con.Value != null)
                    {
                        con.Value.Dispose();
                    }
                    con.Value.Click += new EventHandler(button_Click);
                    con.Value.Font = this.Font;
                }
                list_Games.Controls.Clear();
                controls.Clear();

                List<UserGameInfo> games = gameManager.User.Games;
                for (int i = 0; i < games.Count; i++)
                {
                    UserGameInfo game = games[i];
                    NewUserGame(game);
                }

                if (games.Count == 0)
                {
                    noGamesPresent = true;
                    GameControl con = new GameControl(null, null)
                    {
                        Width = game_listSizer.Width,
                        Text = "No games",
                        Font = this.Font
                    };

                    list_Games.Controls.Add(con);
                }
            }

            GameManager.Instance.SaveUserProfile();
        }

        public void NewUserGame(UserGameInfo game)
        {
            if (game.Game == null || !game.IsGamePresent())
            {
                return;
            }

            if (noGamesPresent)
            {
                noGamesPresent = false;
                RefreshGames();
                return;
            }
            list_Games.SuspendLayout();
            GameControl con = new GameControl(game.Game, game)
            {
                Width = game_listSizer.Width
            };

            controls.Add(game, con);
            list_Games.Controls.Add(con);
            list_Games.ResumeLayout();

            ThreadPool.QueueUserWorkItem(GetIcon, game);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            RefreshGames();
            DPIManager.ForceUpdate();
        }

        private void GetIcon(object state)
        {
            UserGameInfo game = (UserGameInfo)state;
            Bitmap bmp = null;
            string iconPath = iconsIni.IniReadValue("GameIcons", game.Game.GameName);

            if (!string.IsNullOrEmpty(iconPath))
            {
                if (iconPath.EndsWith(".exe"))
                {
                    bmp = new Bitmap(Path.Combine(Directory.GetCurrentDirectory() + "\\gui\\icons\\default.png"));
                    Icon icon = Shell32.GetIcon(iconPath, false);
                    bmp = icon.ToBitmap();
                    icon.Dispose();
                }
                else 
                {
                    if (File.Exists(iconPath))
                    {
                        bmp = new Bitmap(iconPath);
                    }
                    else 
                    {
                        if (File.Exists(Path.Combine(Directory.GetCurrentDirectory() + "\\gui\\icons\\default.png")))
                        {
                            bmp = new Bitmap(Path.Combine(Directory.GetCurrentDirectory() + "\\gui\\icons\\default.png"));
                        }
                    }
                }
            }
            else
            {
                Icon icon = Shell32.GetIcon(game.ExePath, false);
                bmp = icon.ToBitmap();
                icon.Dispose();
            }
            game.Icon = bmp;
           

            lock (controls)
            {
                if (controls.ContainsKey(game))
                {
                    GameControl control = controls[game];
                    control.Invoke((Action)delegate ()
                    {
                        control.Click += new EventHandler(button_Click);
                        control.Image = game.Icon;
                    });
                }
            }
        }

        public delegate void CheckForAssets(Label dllabel, bool visible, string game);

        public void DelegateCheckForAssets(Label dllabel, bool visible, string game)
        {
            dllabel.BackColor = this.BackColor;
            dllabel.ForeColor = this.ForeColor;
            dllabel.AutoSize = true;
            dllabel.Text = game;
            dllabel.Location = new Point(this.Width / 2 - dllabel.Width / 2, 12);
            dllabel.Visible = visible;

            Controls.Add(dllabel);

            if (game == "Download Completed")
            {
                SuspendLayout();
                glowingLine0.Image = new Bitmap(themePath + "\\lightbar_top.gif");
                ResumeLayout();
                mainButtonFrame.Enabled = true;
                btn_downloadAssets.Enabled = true;
                game_listSizer.Enabled = true;
                button_UpdateAvailable.Enabled = true;
                btn_gameOptions.Enabled = true;
                StepPanel.Enabled = true;
                btn_settings.Enabled = true;
                Controls.Remove(dllabel);
            }
        }
        private void btn_downloadAssets_Click(object sender, EventArgs e)
        {
            SuspendLayout();
            glowingLine0.Image = new Bitmap(themePath + "\\download_bar.gif");
            ResumeLayout();
            Control dllabel = new Label();
            mainButtonFrame.Enabled = false;
            StepPanel.Enabled = false;
            button_UpdateAvailable.Enabled = false;
            btn_gameOptions.Enabled = false;
            btn_settings.Enabled = false;
            btn_downloadAssets.Enabled = false;
            game_listSizer.Enabled = false;
            CheckForCovers(dllabel);
        }
        private void CheckForCovers(Control dllabel)
        {
            List<UserGameInfo> games = gameManager.User.Games;
            System.Threading.Tasks.Task.Run(() =>// Run this in another thread to not block UI 
            {
                for (int i = 0; i < games.Count; i++)
                {
                    getAssets = new AssetsScraper();
                    UserGameInfo game = games[i];
                    this.BeginInvoke(new CheckForAssets(DelegateCheckForAssets), dllabel, true, "Checking Cover for : " + game.GameGuid);
                    try
                    {
                        hubHandler = getId.GetHandler(game.Game.GetHubId());
                        string _covers = $@"https://images.igdb.com/igdb/image/upload/t_cover_big/{hubHandler.GameCover}.jpg";

                        if (hubHandler.Id != null)
                        {
                            getAssets.SaveCovers(_covers, game.GameGuid);
                        }
                    }
                    catch (Exception)
                    { }
                }
                CheckForScreenshots(dllabel);
            });

        }
        private void CheckForScreenshots(Control dllabel)
        {
            List<UserGameInfo> games = gameManager.User.Games;
            System.Threading.Tasks.Task.Run(() =>// Run this in another thread to not block UI 
            {
                for (int i = 0; i < games.Count; i++)
                {
                    getAssets = new AssetsScraper();
                    UserGameInfo game = games[i];
                    this.BeginInvoke(new CheckForAssets(DelegateCheckForAssets), dllabel, true, "Checking Screenshots for : " + game.GameGuid);
                    try
                    {
                        hubHandler = getId.GetHandler(game.Game.GetHubId());
                        string _screenshots = game.Game.GetScreenshots();

                        if (hubHandler.Id != null)
                        {
                            getAssets.SaveScreenshots(_screenshots, game.GameGuid);
                        }
                    }
                    catch (Exception)
                    { }
                }
                this.BeginInvoke(new CheckForAssets(DelegateCheckForAssets), dllabel, false, "Download Completed");
            });
        }

        bool stepPanelPictureBoxDispose = false;
        private void list_Games_SelectedChanged(object arg1, Control arg2)
        {
            currentControl = (GameControl)arg1;
            currentGameInfo = currentControl.UserGameInfo;

            if (currentGameInfo == null)
            {
                btn_gameOptions.Visible = false;
                button_UpdateAvailable.Visible = false;
                return;
            }
            else
            {
                currentGame = currentGameInfo.Game;
              

                if (!stepPanelPictureBoxDispose)
                {
                    stepPanelPictureBox.Dispose();
                    rightFrame.Visible = true;
                    btn_gameOptions.Visible = true;
                    btn_scriptAuthorTxt.Visible = true;
                    stepPanelPictureBoxDispose = true;
                    StepPanel.Visible = true;
                }

                button_UpdateAvailable.Visible = false;

                gameDescription = "";

                string name = currentGame.GUID;
                try
                {
                    ///Apply covers
                    if (File.Exists(Path.Combine(Application.StartupPath, @"gui\covers\" + name + ".jpeg")))
                    {
                        coverImg = new Bitmap(Path.Combine(Application.StartupPath, @"gui\covers\" + name + ".jpeg"));
                        clientAreaPanel.SuspendLayout();
                        cover.BackgroundImage = coverImg;
                        clientAreaPanel.ResumeLayout();
                        coverFrame.Visible = true;
                        cover.Visible = true;
                    }
                    else
                    {
                        cover.BackgroundImage = new Bitmap(themePath + "\\no_cover.png");
                        clientAreaPanel.SuspendLayout();
                        clientAreaPanel.BackgroundImage = defBackground;
                        clientAreaPanel.ResumeLayout();
                        cover.Visible = true;
                        coverFrame.Visible = true;
                    }

                    //Apply screenshots randomly

                    if (Directory.Exists(Path.Combine(Application.StartupPath, @"gui\screenshots\" + name)))
                    {
                        string[] imgsPath = Directory.GetFiles((Path.Combine(Application.StartupPath, @"gui\screenshots\" + name)));
                        Random rNum = new Random();
                        int RandomIndex = rNum.Next(0, imgsPath.Count());

                        screenshotImg = new Bitmap(Path.Combine(Application.StartupPath, @"gui\screenshots\" + name + "\\" + RandomIndex + "_" + name + ".jpeg"));   //name(1) => directory name ; name(2) = partial image name 
                        clientAreaPanel.SuspendLayout();
                        clientAreaPanel.BackgroundImage = screenshotImg;
                        clientAreaPanel.ResumeLayout();
                    }
                    else
                    {
                        clientAreaPanel.SuspendLayout();
                        clientAreaPanel.BackgroundImage = defBackground;
                        clientAreaPanel.ResumeLayout();
                    }
                }

                catch (Exception)
                { }
            }

            //if (connected)
            //{ 
            //    try//get game description (online)
            //    {
            //        hubHandler = getId.GetHandler(currentGameInfo.Game.GetHubId());
            //        gameDescription = hubHandler.GameDescription;

            //        if (gameDescription.EndsWith("."))
            //        {
            //            txt_GameDesc.Text = hubHandler.GameDescription;
            //            btn_GameDesc.Enabled = true;
            //            btn_GameDesc.Visible = true;
            //        }
            //        else
            //        {
            //            btn_GameDesc.Enabled = false;
            //            txt_GameDescSizer.Visible = false;
            //            txt_GameDesc.Visible = false;                        
            //        }
            //    }
            //    catch (Exception)
            //    {
            //        btn_GameDesc.Enabled = false;
            //        txt_GameDescSizer.Visible = false;
            //        txt_GameDesc.Visible = false;
            //    }
            // }


            btn_Play.Enabled = false;

            stepsList = new List<UserInputControl>
            {
                positionsControl,
                optionsControl
            };

            for (int i = 0; i < currentGame.CustomSteps.Count; i++)
            {
                stepsList.Add(jsControl);
            }

            currentProfile = new GameProfile();
            currentProfile.InitializeDefault(currentGame);
            gameManager.UpdateCurrentGameProfile(currentProfile);

            btn_gameOptions.Enabled = true;

            button_UpdateAvailable.Visible = currentGameInfo.Game.IsUpdateAvailable(false);
            currentlyUpdatingScript = false;

            if (currentGame.Description?.Length > 0)
            {
                btn_scriptAuthorTxt.Enabled = true;
                SuspendLayout();
                scriptAuthorTxt.Text = currentGame.Description;
                ResumeLayout();
                scriptAuthorTxt.Visible = true;
                scriptAuthorTxtSizer.Visible = true;
            }
            else
            {
                btn_scriptAuthorTxt.Enabled = false;
                scriptAuthorTxtSizer.Visible = false;
                scriptAuthorTxt.Visible = false;
            }

            if (content != null)
            {
                content.Dispose();
            }

            // content manager is shared withing the same game
            content = new ContentManager(currentGame);

            GoToStep(0);
        }

        private void EnablePlay()
        {
            btn_Play.Enabled = true;
        }

        private void StepCanPlay(UserControl obj, bool canProceed, bool autoProceed)
        {
            if (!canProceed)
            {
                btn_Next.Enabled = false;
                return;
            }

            if (currentStepIndex + 1 > stepsList.Count - 1)
            {
                EnablePlay();
                return;
            }

            if (autoProceed)
            {
                GoToStep(currentStepIndex + 1);
            }
            else
            {
                btn_Next.Enabled = true;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GoToStep(currentStepIndex + 1);
        }

        private void KillCurrentStep()
        {

            if (currentGameInfo.Game.Description?.Length > 0)
            {
                btn_scriptAuthorTxt.Visible = true;
            }

            if (gameDescription?.Length > 0 && connected)
            {
                txt_GameDesc.Text = hubHandler.GameDescription;
                btn_GameDesc.Enabled = true;
            }

            foreach (Control c in StepPanel.Controls)
            {
                if (!c.Name.Equals("btn_GameDesc") && !c.Name.Equals("scriptAuthorTxt") && !c.Name.Equals("txt_GameDescSizer"))
                {
                    StepPanel.Controls.Remove(c);
                }
            }

        }

        private void GoToStep(int step)
        {
            btnBack.Enabled = step > 0;
            if (step >= stepsList.Count)
            {
                return;
            }

            if (step >= 2)
            {
                // Custom steps
                List<CustomStep> customSteps = currentGame.CustomSteps;
                int customStepIndex = step - 2;
                CustomStep customStep = customSteps[0];

                if (customStep.UpdateRequired != null)
                {
                    customStep.UpdateRequired();
                }

                if (customStep.Required)
                {
                    jsControl.CustomStep = customStep;
                    jsControl.Content = content;
                }
                else
                {
                    EnablePlay();
                    return;
                }
            }

            KillCurrentStep();

            currentStepIndex = step;
            currentStep = stepsList[step];
            currentStep.Size = StepPanel.Size;
            currentStep.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;

            currentStep.Initialize(currentGameInfo, currentProfile);

            btn_Next.Enabled = currentStep.CanProceed && step != stepsList.Count - 1;

            StepPanel.Controls.Add(currentStep);
            currentStep.Size = StepPanel.Size; // for some reason this line must exist or the PositionsControl get messed up

            label_StepTitle.Text = currentStep.Title;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            formClosing = true;

            if (handler != null)
            {
                Log("OnFormClosed method calling Handler End function");
                try
                {
                    handler.End(false);
                }
                catch { }
            }

            User32Util.ShowTaskBar();
        }

        private void btn_Play_Click(object sender, EventArgs e)
        {

            bool useCustomLayout = Convert.ToBoolean(ini.IniReadValue("CustomLayout", "SplitDiv"));
            string splitDivisionBackColor = ini.IniReadValue("CustomLayout", "SplitDivColor");

            if (btn_Play.Text == "S T O P")
            {
                try
                {
                    if (handler != null)
                    {
                        Log("Stop button clicked, calling Handler End function");
                        handler.End(true);
                    }

                    foreach (Form openForm in Application.OpenForms)
                    {
                        if (openForm.Name == "Hide Desktop")
                        {
                            openForm.Close();
                        }
                    }
                }
                catch { }

                User32Util.ShowTaskBar();
                SetBtnToPlay();
                btn_Play.Enabled = false;
                RefreshGames();
                GoToStep(0);
                return;
            }

            currentStep?.Ended();

            btn_Play.Text = "S T O P";
            btnBack.Enabled = false;

            gameManager.AddScript(Path.GetFileNameWithoutExtension(currentGame.JsFileName));

            currentGame = gameManager.GetGame(currentGameInfo.ExePath);
            currentGameInfo.InitializeDefault(currentGame, currentGameInfo.ExePath);

            handler = gameManager.MakeHandler(currentGame);
            handler.Initialize(currentGameInfo, GameProfile.CleanClone(currentProfile));
            handler.Ended += handler_Ended;

            gameManager.Play(handler);
            if (handler.TimerInterval > 0)
            {
                handlerThread = new Thread(UpdateGameManager);
                handlerThread.Start();
            }

            if (currentGame.HideTaskbar && !useCustomLayout)
            {
                User32Util.HideTaskbar();
            }

            if (currentGame.ProtoInput.AutoHideTaskbar || useCustomLayout)
            {
                if (ProtoInput.protoInput.GetTaskbarAutohide())
                {
                    currentGame.ProtoInput.AutoHideTaskbar = false; // If already hidden don't change it, and dont set it unhidden after.
                }
                else
                {
                    ProtoInput.protoInput.SetTaskbarAutohide(true);
                }
            }

            if (useCustomLayout)
            {
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
                    if (color.Key == splitDivisionBackColor)
                    {
                        ChoosenColor = color.Value;
                    }
                }

                loadTimer = new System.Windows.Forms.Timer
                {
                    Interval = (80000) //millisecond
                };

                loadTimer.Tick += new EventHandler(loadTimerTick);
                loadTimer.Start();

                slideshow = new System.Windows.Forms.Timer
                {
                    Interval = (8000) //millisecond
                };

                slideshow.Tick += new EventHandler(slideshowTick);
                slideshow.Start();

                foreach (Screen screen in Screen.AllScreens)
                {
                    Form backgroundForm = new Form
                    {
                        BackColor = Color.Black,
                        Location = new Point(screen.Bounds.X, screen.Bounds.Y),
                        Width = screen.WorkingArea.Size.Width,
                        Height = screen.WorkingArea.Size.Height + 50,
                        BackgroundImage = clientAreaPanel.BackgroundImage,
                        BackgroundImageLayout = ImageLayout.Stretch,
                        FormBorderStyle = FormBorderStyle.None,
                        StartPosition = FormStartPosition.Manual

                    };

                    backgroundForms.Add(backgroundForm);
                    backgroundForm.Show();
                }
            }

            if (currentGame.HideDesktop && !useCustomLayout)
            {
                foreach (Screen screen in Screen.AllScreens)
                {
                    backgroundForm = new Form
                    {
                        BackColor = Color.Black,
                        Location = new Point(screen.Bounds.X, screen.Bounds.Y),

                    };

                    backgroundForm.Width = screen.WorkingArea.Size.Width;
                    backgroundForm.Height = screen.WorkingArea.Size.Height + 50;
                    backgroundForm.FormBorderStyle = FormBorderStyle.None;
                    backgroundForm.StartPosition = FormStartPosition.Manual;
                    backgroundForm.Show();
                }
            }

            WindowState = FormWindowState.Minimized;
        }

        private void slideshowTick(Object myObject, EventArgs myEventArgs)
        {
            if (Directory.Exists(Path.Combine(Application.StartupPath, @"gui\screenshots\" + currentGameInfo.Game.GameName)))
            {
                string[] imgsPath = Directory.GetFiles((Path.Combine(Application.StartupPath, @"gui\screenshots\" + currentGameInfo.Game.GameName)));
                Random rNum = new Random();
                int RandomIndex = rNum.Next(0, imgsPath.Count());

                screenshotImg = new Bitmap(Path.Combine(Application.StartupPath, @"gui\screenshots\" + currentGameInfo.Game.GameName + "\\" + RandomIndex + "_" + currentGameInfo.Game.GameName + ".jpeg"));
                foreach (Form _backgroundForm in backgroundForms)
                {
                    _backgroundForm.SuspendLayout();
                    _backgroundForm.BackgroundImage = screenshotImg;
                    _backgroundForm.ResumeLayout();
                }
            }
        }

        private void loadTimerTick(Object myObject, EventArgs myEventArgs)
        {
            slideshow.Dispose();
            loadTimer.Dispose();

            foreach (Form _backgroundForm in backgroundForms)
            {
                _backgroundForm.BackgroundImage = null;
                _backgroundForm.BackColor = ChoosenColor;
            }

        }

        private void SetBtnToPlay()
        {
            btn_Play.Text = "P L A Y";
        }

        private void handler_Ended()
        {
            Log("Handler ended method called");
            User32Util.ShowTaskBar();
            handler = null;
            try
            {
                if (handlerThread != null)
                {
                    handlerThread.Abort();
                    handlerThread = null;
                }
            }
            catch { }

            Invoke(new Action(SetBtnToPlay));
        }

        private void UpdateGameManager(object state)
        {
            for (; ; )
            {
                try
                {
                    if (gameManager == null || formClosing || handler == null)
                    {
                        break;
                    }

                    string error = gameManager.Error;
                    if (!string.IsNullOrEmpty(error))
                    {

                        Log("Restoring backed up registry files - method 3");
                        string[] regFiles = Directory.GetFiles(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "utils\\backup"), "*.reg", SearchOption.AllDirectories);
                        foreach (string regFilePath in regFiles)
                        {
                            Process proc = new Process();

                            try
                            {
                                proc.StartInfo.FileName = "reg.exe";
                                proc.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                                proc.StartInfo.CreateNoWindow = true;
                                proc.StartInfo.UseShellExecute = false;

                                string command = "import \"" + regFilePath + "\"";
                                proc.StartInfo.Arguments = command;
                                proc.Start();

                                proc.WaitForExit();
                                Log($"Imported {Path.GetFileName(regFilePath)}");
                            }
                            catch (Exception)
                            {
                                proc.Dispose();
                            }

                            File.Delete(regFilePath);
                        }

                        handler_Ended();
                        return;
                    }

                    handler.Update(handler.TimerInterval);
                    Thread.Sleep(TimeSpan.FromMilliseconds(handler.TimerInterval));
                }
                catch (ThreadAbortException)
                {

                }
            }
        }

        private void arrow_Back_Click(object sender, EventArgs e)
        {
            currentStepIndex--;
            if (currentStepIndex < 0)
            {
                currentStepIndex = 0;
                return;
            }
            GoToStep(currentStepIndex);
        }

        private void arrow_Next_Click(object sender, EventArgs e)
        {
            currentStepIndex = Math.Min(currentStepIndex++, stepsList.Count - 1);
            GoToStep(currentStepIndex);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchGame();
        }

        public void SearchGame(string exeName = null)
        {
            try
            {
                using (System.Windows.Forms.OpenFileDialog open = new System.Windows.Forms.OpenFileDialog())
                {

                    if (string.IsNullOrEmpty(exeName))
                    {
                        open.Title = "Select a game executable to add to Nucleus";
                        open.Filter = "Game Executable Files|*.exe";
                    }
                    else
                    {
                        open.Title = string.Format("Select {0} to add the game to Nucleus", exeName);
                        open.Filter = "Game Exe|" + exeName;
                    }

                    if (open.ShowDialog() == DialogResult.OK)
                    {
                        string path = open.FileName;

                        List<GenericGameInfo> info = gameManager.GetGames(path);

                        if (info.Count > 1)
                        {
                            GameList list = new GameList(info);

                            if (list.ShowDialog() == DialogResult.OK)
                            {
                                UserGameInfo game = GameManager.Instance.TryAddGame(path, list.Selected);

                                if (game != null)
                                {
                                    MessageBox.Show(string.Format("The game {0} has been added!", game.Game.GameName), "Nucleus - Game added");
                                    RefreshGames();
                                }
                            }
                        }
                        else if (info.Count == 1)
                        {
                            UserGameInfo game = GameManager.Instance.TryAddGame(path, info[0]);
                            MessageBox.Show(string.Format("The game {0} has been added!", game.Game.GameName), "Nucleus - Game added");
                            RefreshGames();
                        }
                        else
                        {
                            MessageBox.Show(string.Format("The executable '{0}' was not found in any game handler's Game.ExecutableName field. Game has not been added.", Path.GetFileName(path)), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private bool AutoSearchLoaded = false;
        private void btnAutoSearch_Click(object sender, EventArgs e)
        {
            if (!AutoSearchLoaded)
            {
                form = new SearchDisksForm(this);
                form.FormClosed += Form_FormClosed;
                form.Show();
                AutoSearchLoaded = true;
            }
            else
            {
                form.Location = new Point(Location.X + Width / 2 - form.Width / 2, Location.Y + Height / 2 - form.Height / 2);
                form.Visible = true;
            }
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            User32Util.ShowTaskBar();
        }

        private void btnShowTaskbar_Click(object sender, EventArgs e)
        {
            User32Util.ShowTaskBar();
        }

        private bool SettingsLoaded = false;

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            if (!SettingsLoaded)
            {
                settingsForm = new Settings(this, positionsControl);
                settingsForm.Show();
                SettingsLoaded = true;
            }
            else
            {
                settingsForm.Location = new Point(Location.X + Width / 2 - settingsForm.Width / 2, Location.Y + Height / 2 - settingsForm.Height / 2);
                settingsForm.Visible = true;
            }
        }

        private void DetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetDetails();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteGame();
        }

        private void GetDetails()
        {
            string userProfile = gameManager.GetUserProfilePath();

            if (File.Exists(userProfile))
            {
                string jsonString = File.ReadAllText(userProfile);
                JObject jObject = JsonConvert.DeserializeObject(jsonString) as JObject;

                JArray games = jObject["Games"] as JArray;
                for (int i = 0; i < games.Count; i++)
                {
                    string gameGuid = jObject["Games"][i]["GameGuid"].ToString();
                    string profiles = jObject["Games"][i]["Profiles"].ToString();
                    string exePath = jObject["Games"][i]["ExePath"].ToString();

                    if (gameGuid == currentGameInfo.GameGuid && exePath == currentGameInfo.ExePath)
                    {
                        string arch = "";
                        if (Is64Bit(exePath) == true)
                        {
                            arch = "x64";
                        }
                        else if (Is64Bit(exePath) == false)
                        {
                            arch = "x86";
                        }
                        else
                        {
                            arch = "Unknown";
                        }

                        MessageBox.Show(string.Format("Game Name: {0}\nArchitecture: {1}\nSteam ID: {2}\n\nHandler Filename: {3}\nNucleus Game Content Path: {4}\nOrig Exe Path: {5}\n\nMax Players: {6}\nSupports XInput: {7}\nSupports DInput: {8}\nSupports Keyboard: {9}\nSupports multiple keyboards and mice: {10}", currentGameInfo.Game.GameName, arch, currentGameInfo.Game.SteamID, currentGameInfo.Game.JsFileName, Path.Combine(gameManager.GetAppContentPath(), gameGuid), exePath, currentGameInfo.Game.MaxPlayers, currentGameInfo.Game.Hook.XInputEnabled || currentGameInfo.Game.ProtoInput.XinputHook, currentGameInfo.Game.Hook.DInputEnabled, currentGameInfo.Game.SupportsKeyboard, currentGameInfo.Game.SupportsMultipleKeyboardsAndMice), "Game Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void DeleteGame(bool dontConfirm = false)
        {
            string userProfile = gameManager.GetUserProfilePath();

            if (File.Exists(userProfile))
            {
                string jsonString = File.ReadAllText(userProfile);
                JObject jObject = JsonConvert.DeserializeObject(jsonString) as JObject;

                JArray games = jObject["Games"] as JArray;
                for (int i = 0; i < games.Count; i++)
                {
                    string gameGuid = jObject["Games"][i]["GameGuid"].ToString();
                    string profiles = jObject["Games"][i]["Profiles"].ToString();
                    string exePath = jObject["Games"][i]["ExePath"].ToString();

                    if (gameGuid == currentGameInfo.GameGuid && exePath == currentGameInfo.ExePath)
                    {
                        DialogResult dialogResult = dontConfirm ? DialogResult.Yes :
                            MessageBox.Show("Are you sure you want to delete " + currentGameInfo.Game.GameName + "?", "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (dialogResult == DialogResult.Yes)
                        {
                            gameManager.User.Games.RemoveAt(i);
                            jObject["Games"][i].Remove();
                            string output = JsonConvert.SerializeObject(jObject, Formatting.Indented);
                            File.WriteAllText(userProfile, output);
                            RefreshGames();
                        }
                    }
                }
            }
        }

        private void GameContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Control selectedControl = FindControlAtCursor(this);

            if (selectedControl.GetType() == typeof(Label) || selectedControl.GetType() == typeof(PictureBox))
            {
                selectedControl = selectedControl.Parent;
            }

            foreach (Control c in selectedControl.Controls)
            {
                if (c is Label)
                {
                    if (c.Text == "No games")
                    {
                        gameContextMenuStrip.Items[0].Text = "No game selected...";
                        for (int i = 1; i < gameContextMenuStrip.Items.Count; i++)
                        {
                            gameContextMenuStrip.Items[i].Visible = false;
                        }
                        return;
                    }
                }
            }

            if (selectedControl.GetType() == typeof(GameControl) || selectedControl.GetType() == typeof(Button))
            {
                bool btnClick = false;
                if (selectedControl.GetType() == typeof(GameControl))
                {

                    currentControl = (GameControl)selectedControl;
                    currentGameInfo = currentControl.UserGameInfo;
                    gameContextMenuStrip.Items[0].Visible = true;
                    gameContextMenuStrip.Items[2].Visible = true;
                }
                else
                {
                    btnClick = true;
                    gameContextMenuStrip.Items[0].Visible = false;
                }

                gameContextMenuStrip.Items[1].Visible = false;
                gameContextMenuStrip.Items[7].Visible = false;
                gameContextMenuStrip.Items[8].Visible = false;
                gameContextMenuStrip.Items[9].Visible = false;
                gameContextMenuStrip.Items[10].Visible = false;
                gameContextMenuStrip.Items[11].Visible = false;
                gameContextMenuStrip.Items[12].Visible = false;
                gameContextMenuStrip.Items[13].Visible = false;
                gameContextMenuStrip.Items[14].Visible = false;
                gameContextMenuStrip.Items[15].Visible = false;

                if (string.IsNullOrEmpty(currentGameInfo.GameGuid) || currentGameInfo == null)
                {
                    gameContextMenuStrip.Items[0].Text = "No game selected...";
                    for (int i = 1; i < gameContextMenuStrip.Items.Count; i++)
                    {
                        gameContextMenuStrip.Items[i].Visible = false;
                    }
                }
                else
                {
                    gameContextMenuStrip.Items[0].Text = currentGameInfo.Game.GameName;

                    bool userConfigPathExists = false;
                    bool userSavePathExists = false;
                    bool docConfigPathExists = false;
                    bool docSavePathExists = false;

                    //bool userConfigPathConverted = false;
                    if (currentGameInfo.Game.UserProfileConfigPath?.Length > 0 && currentGameInfo.Game.UserProfileConfigPath.ToLower().StartsWith(@"documents\"))
                    {
                        currentGameInfo.Game.DocumentsConfigPath = currentGameInfo.Game.UserProfileConfigPath.Substring(10);
                        currentGameInfo.Game.UserProfileConfigPath = null;
                        currentGameInfo.Game.DocumentsConfigPathNoCopy = currentGameInfo.Game.UserProfileConfigPathNoCopy;
                        currentGameInfo.Game.ForceDocumentsConfigCopy = currentGameInfo.Game.ForceUserProfileConfigCopy;
                        //userConfigPathConverted = true;
                    }

                    //bool userSavePathConverted = false;
                    if (currentGameInfo.Game.UserProfileSavePath?.Length > 0 && currentGameInfo.Game.UserProfileSavePath.ToLower().StartsWith(@"documents\"))
                    {
                        currentGameInfo.Game.DocumentsSavePath = currentGameInfo.Game.UserProfileSavePath.Substring(10);
                        currentGameInfo.Game.UserProfileSavePath = null;
                        currentGameInfo.Game.DocumentsSavePathNoCopy = currentGameInfo.Game.UserProfileSavePathNoCopy;
                        currentGameInfo.Game.ForceDocumentsSaveCopy = currentGameInfo.Game.ForceUserProfileSaveCopy;
                        //userSavePathConverted = true;
                    }

                    for (int i = 1; i < gameContextMenuStrip.Items.Count; i++)
                    {
                        gameContextMenuStrip.Items[i].Visible = true;

                        if (string.IsNullOrEmpty(currentGameInfo.Game.UserProfileConfigPath) && string.IsNullOrEmpty(currentGameInfo.Game.UserProfileSavePath) && string.IsNullOrEmpty(currentGameInfo.Game.DocumentsConfigPath) && string.IsNullOrEmpty(currentGameInfo.Game.DocumentsSavePath))
                        {
                            if (i == 7)
                            {
                                gameContextMenuStrip.Items[i].Visible = false;
                            }
                        }
                        else if (i == 1)
                        {
                            profilePaths.Clear();
                            profilePaths.Add(Environment.GetEnvironmentVariable("userprofile"));
                            profilePaths.Add(DocumentsRoot);

                            if (currentGameInfo.Game.UseNucleusEnvironment)
                            {
                                string targetDirectory = $@"{NucleusEnvironmentRoot}\NucleusCoop\";

                                if (Directory.Exists(targetDirectory))
                                {
                                    string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory, "*", SearchOption.TopDirectoryOnly);
                                    foreach (string subdirectory in subdirectoryEntries)
                                    {
                                        profilePaths.Add(subdirectory);
                                        if ($@"{Path.GetDirectoryName(DocumentsRoot)}\NucleusCoop\" == targetDirectory)
                                        {
                                            profilePaths.Add(subdirectory + "\\Documents");
                                        }
                                    }
                                }


                                if ($@"{Path.GetDirectoryName(DocumentsRoot)}\NucleusCoop\" != targetDirectory)
                                {
                                    targetDirectory = $@"{Path.GetDirectoryName(DocumentsRoot)}\NucleusCoop\";
                                    if (Directory.Exists(targetDirectory))
                                    {
                                        string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory, "*", SearchOption.TopDirectoryOnly);
                                        foreach (string subdirectory in subdirectoryEntries)
                                        {
                                            profilePaths.Add(subdirectory + "\\Documents");
                                        }
                                    }
                                }
                            }

                        }

                        if (i == 9)
                        {
                            (gameContextMenuStrip.Items[8] as ToolStripMenuItem).DropDownItems.Clear();
                            (gameContextMenuStrip.Items[9] as ToolStripMenuItem).DropDownItems.Clear();
                            if (currentGameInfo.Game.UserProfileConfigPath?.Length > 0)
                            {
                                if (profilePaths.Count > 0)
                                {
                                    try
                                    {

                                        foreach (string profilePath in profilePaths)
                                        {
                                            string currPath = Path.Combine(profilePath, currentGameInfo.Game.UserProfileConfigPath);
                                            if (Directory.Exists(currPath))
                                            {
                                                if (!userConfigPathExists)
                                                {
                                                    userConfigPathExists = true;
                                                }

                                                string nucPrefix = "";
                                                if (Directory.GetParent(profilePath).Name == "NucleusCoop")
                                                {
                                                    nucPrefix = "Nucleus: ";
                                                }

                                                (gameContextMenuStrip.Items[8] as ToolStripMenuItem).DropDownItems.Add(nucPrefix + Path.GetFileName(profilePath.TrimEnd('\\')), null, new EventHandler(UserProfileOpenSubmenuItem_Click));
                                                (gameContextMenuStrip.Items[9] as ToolStripMenuItem).DropDownItems.Add(nucPrefix + Path.GetFileName(profilePath.TrimEnd('\\')), null, new EventHandler(UserProfileDeleteSubmenuItem_Click));
                                            }
                                        }
                                    }
                                    catch(Exception)
                                    {

                                    }
                                }
                            }

                            if (!userConfigPathExists)
                            {
                                gameContextMenuStrip.Items[8].Visible = false;
                                gameContextMenuStrip.Items[9].Visible = false;
                            }
                        }

                        if (i == 11)
                        {
                            (gameContextMenuStrip.Items[10] as ToolStripMenuItem).DropDownItems.Clear();
                            (gameContextMenuStrip.Items[11] as ToolStripMenuItem).DropDownItems.Clear();
                            if (currentGameInfo.Game.UserProfileSavePath?.Length > 0)
                            {

                                if (profilePaths.Count > 0)
                                {
                                    try
                                    {
                                        foreach (string profilePath in profilePaths)
                                        {
                                            string currPath = Path.Combine(profilePath, currentGameInfo.Game.UserProfileSavePath);
                                            if (Directory.Exists(currPath))
                                            {
                                                if (!userSavePathExists)
                                                {
                                                    userSavePathExists = true;
                                                }

                                                string nucPrefix = "";
                                                if (Directory.GetParent(profilePath).Name == "NucleusCoop")
                                                {
                                                    nucPrefix = "Nucleus: ";
                                                }

                                                (gameContextMenuStrip.Items[10] as ToolStripMenuItem).DropDownItems.Add(nucPrefix + Path.GetFileName(profilePath.TrimEnd('\\')), null, new EventHandler(UserProfileOpenSubmenuItem_Click));
                                                (gameContextMenuStrip.Items[11] as ToolStripMenuItem).DropDownItems.Add(nucPrefix + Path.GetFileName(profilePath.TrimEnd('\\')), null, new EventHandler(UserProfileDeleteSubmenuItem_Click));
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }
                                
                            }

                            if (!userSavePathExists)
                            {
                                gameContextMenuStrip.Items[10].Visible = false;
                                gameContextMenuStrip.Items[11].Visible = false;
                            }
                        }

                        if (i == 13)
                        {
                            (gameContextMenuStrip.Items[12] as ToolStripMenuItem).DropDownItems.Clear();
                            (gameContextMenuStrip.Items[13] as ToolStripMenuItem).DropDownItems.Clear();
                            if (currentGameInfo.Game.DocumentsConfigPath?.Length > 0)
                            {

                                if (profilePaths.Count > 0)
                                {
                                    try
                                    {
                                        foreach (string profilePath in profilePaths)
                                        {
                                            string currPath = Path.Combine(profilePath, currentGameInfo.Game.DocumentsConfigPath);
                                            if (Directory.Exists(currPath))
                                            {
                                                if (!docConfigPathExists)
                                                {
                                                    docConfigPathExists = true;
                                                }

                                                string nucPrefix = "";
                                                if (Directory.GetParent(Directory.GetParent(profilePath).ToString()).Name == "NucleusCoop")
                                                {
                                                    nucPrefix = "Nucleus: ";
                                                }

                                                (gameContextMenuStrip.Items[12] as ToolStripMenuItem).DropDownItems.Add(nucPrefix + Directory.GetParent(profilePath).Name, null, new EventHandler(DocOpenSubmenuItem_Click));
                                                (gameContextMenuStrip.Items[13] as ToolStripMenuItem).DropDownItems.Add(nucPrefix + Directory.GetParent(profilePath).Name, null, new EventHandler(DocDeleteSubmenuItem_Click));
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }

                                
                        }

                            if (!docConfigPathExists)
                            {
                                gameContextMenuStrip.Items[12].Visible = false;
                                gameContextMenuStrip.Items[13].Visible = false;
                            }
                        }

                        if (i == 15)
                        {
                            (gameContextMenuStrip.Items[14] as ToolStripMenuItem).DropDownItems.Clear();
                            (gameContextMenuStrip.Items[15] as ToolStripMenuItem).DropDownItems.Clear();
                            if (currentGameInfo.Game.DocumentsSavePath?.Length > 0)
                            {
                                if (profilePaths.Count > 0)
                                {
                                    try
                                    {
                                        foreach (string profilePath in profilePaths)
                                        {
                                            string currPath = Path.Combine(profilePath, currentGameInfo.Game.DocumentsSavePath);
                                            if (Directory.Exists(currPath))
                                            {
                                                if (!docSavePathExists)
                                                {
                                                    docSavePathExists = true;
                                                }

                                                string nucPrefix = "";
                                                if (Directory.GetParent(Directory.GetParent(profilePath).ToString()).Name == "NucleusCoop")
                                                {
                                                    nucPrefix = "Nucleus: ";
                                                }

                                                (gameContextMenuStrip.Items[14] as ToolStripMenuItem).DropDownItems.Add(nucPrefix + Directory.GetParent(profilePath).Name, null, new EventHandler(DocOpenSubmenuItem_Click));
                                                (gameContextMenuStrip.Items[15] as ToolStripMenuItem).DropDownItems.Add(nucPrefix + Directory.GetParent(profilePath).Name, null, new EventHandler(DocDeleteSubmenuItem_Click));
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }

                                }
                            }

                            if (!docSavePathExists)
                            {
                                gameContextMenuStrip.Items[14].Visible = false;
                                gameContextMenuStrip.Items[15].Visible = false;
                            }
                        }

                        if (i == 16 && !userConfigPathExists && !userSavePathExists && !docConfigPathExists && !docSavePathExists)
                        {
                            gameContextMenuStrip.Items[7].Visible = false;
                        }

                        if (i == 1 && currentGameInfo.Game.Description == null)
                        {
                            gameContextMenuStrip.Items[i].Visible = false;
                            if (btnClick)
                            {
                                gameContextMenuStrip.Items[2].Visible = false;
                                i++;
                            }
                        }
                    }
                }
            }
            else
            {
                gameContextMenuStrip.Items[0].Text = "No game selected...";

                for (int i = 1; i < gameContextMenuStrip.Items.Count; i++)
                {
                    gameContextMenuStrip.Items[i].Visible = false;
                }
            }
        }

        private void UserProfileOpenSubmenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ToolStripItem parent = item.OwnerItem;

            string pathSuffix;
            if (parent.Text.Contains("Config"))
            {
                pathSuffix = currentGameInfo.Game.UserProfileConfigPath;
            }
            else
            {
                pathSuffix = currentGameInfo.Game.UserProfileSavePath;
            }

            string path;
            if (item.Text.StartsWith("Nucleus: "))
            {
                path = Path.Combine($@"{NucleusEnvironmentRoot}\NucleusCoop\{item.Text.Substring("Nucleus: ".Length)}\", pathSuffix);
            }
            else
            {
                path = Path.Combine(Environment.GetEnvironmentVariable("userprofile"), pathSuffix);
            }

            if (Directory.Exists(path))
            {
                Process.Start(path);
            }
        }

        private void UserProfileDeleteSubmenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ToolStripItem parent = item.OwnerItem;

            string pathSuffix;
            if (parent.Text.Contains("Config"))
            {
                pathSuffix = currentGameInfo.Game.UserProfileConfigPath;
            }
            else
            {
                pathSuffix = currentGameInfo.Game.UserProfileSavePath;
            }

            string path;
            if (item.Text.StartsWith("Nucleus: "))
            {
                path = Path.Combine($@"{NucleusEnvironmentRoot}\NucleusCoop\{item.Text.Substring("Nucleus: ".Length)}\", pathSuffix);
            }
            else
            {
                path = Path.Combine(Environment.GetEnvironmentVariable("userprofile"), pathSuffix);
            }

            if (Directory.Exists(path))
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete '" + path + "' and all its contents?", "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    Directory.Delete(path, true);
                }
            }
        }

        private void DocOpenSubmenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ToolStripItem parent = item.OwnerItem;

            string pathSuffix;
            if (parent.Text.Contains("Config"))
            {
                pathSuffix = currentGameInfo.Game.DocumentsConfigPath;
            }
            else
            {
                pathSuffix = currentGameInfo.Game.DocumentsSavePath;
            }

            string path;
            if (item.Text.StartsWith("Nucleus: "))
            {
                path = Path.Combine($@"{Path.GetDirectoryName(DocumentsRoot)}\NucleusCoop\{item.Text.Substring("Nucleus: ".Length)}\Documents", pathSuffix);
            }
            else
            {
                path = Path.Combine(DocumentsRoot, pathSuffix);
            }

            if (Directory.Exists(path))
            {
                Process.Start(path);
            }
        }

        private void DocDeleteSubmenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            ToolStripItem parent = item.OwnerItem;

            string pathSuffix;
            if (parent.Text.Contains("Config"))
            {
                pathSuffix = currentGameInfo.Game.DocumentsConfigPath;
            }
            else
            {
                pathSuffix = currentGameInfo.Game.DocumentsSavePath;
            }

            string path;
            if (item.Text.StartsWith("Nucleus: "))
            {
                path = Path.Combine($@"{Path.GetDirectoryName(DocumentsRoot)}\NucleusCoop\{item.Text.Substring("Nucleus: ".Length)}\Documents", pathSuffix);
            }
            else
            {
                path = Path.Combine(DocumentsRoot, pathSuffix);
            }

            if (Directory.Exists(path))
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete '" + path + "' and all its contents?", "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    Directory.Delete(path, true);
                }
            }
        }


        public static Control FindControlAtPoint(Control container, Point pos)
        {
            Control child;
            foreach (Control c in container.Controls)
            {
                if (c.Visible && c.Bounds.Contains(pos))
                {
                    child = FindControlAtPoint(c, new Point(pos.X - c.Left, pos.Y - c.Top));
                    if (child == null)
                    {
                        return c;
                    }
                    else
                    {
                        return child;
                    }
                }
            }
            return null;
        }

        public static Control FindControlAtCursor(MainForm form)
        {
            Point pos = Cursor.Position;
            if (form.Bounds.Contains(pos))
            {
                return FindControlAtPoint(form, form.PointToClient(pos));
            }

            return null;
        }

        private void OpenRawGameScript()
        {
            try
            {
                Process.Start("notepad++.exe", "\"" + Path.Combine(gameManager.GetJsScriptsPath(), currentGameInfo.Game.JsFileName) + "\"");
            }
            catch
            {
                Process.Start("notepad.exe", Path.Combine(gameManager.GetJsScriptsPath(), currentGameInfo.Game.JsFileName));
            }
        }

        private void OpenScriptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenRawGameScript();
        }

        private void OpenDataFolder()
        {
            string path = Path.Combine(gameManager.GetAppContentPath(), currentGameInfo.Game.GUID);
            if (Directory.Exists(path))
            {
                Process.Start(path);
            }
            else
            {
                MessageBox.Show("No data present for this game.", "No data found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenDataFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDataFolder();
        }

        private void ChangeIconToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog())
            {
                dlg.Title = "Open Image";
                dlg.Filter = "All Images Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.tiff;*.tif;*.ico;*.exe)|*.png;*.jpg;*.jpeg;*.gif;*.bmp;*.tiff;*.tif;*.ico;*.exe" +
                            "|PNG Portable Network Graphics (*.png)|*.png" +
                            "|JPEG File Interchange Format (*.jpg *.jpeg *jfif)|*.jpg;*.jpeg;*.jfif" +
                            "|BMP Windows Bitmap (*.bmp)|*.bmp" +
                            "|TIF Tagged Imaged File Format (*.tif *.tiff)|*.tif;*.tiff" +
                            "|Icon (*.ico)|*.ico" +
                            "|Executable (*.exe)|*.exe";
                dlg.InitialDirectory = Path.Combine(Directory.GetCurrentDirectory() + "\\gui\\icons");

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    // Create a new Bitmap object from the picture file on disk,
                    // and assign that to the PictureBox.Image property
                    //PictureBox1.Image = new Bitmap(dlg.FileName);

                    if (dlg.FileName.EndsWith(".exe"))
                    {
                        Icon icon = Shell32.GetIcon(dlg.FileName, false);

                        Bitmap bmp = icon.ToBitmap();
                        icon.Dispose();
                        currentGameInfo.Icon = bmp;
                    }
                    else
                    {
                        currentGameInfo.Icon = new Bitmap(dlg.FileName);
                    }
                    iconsIni.IniWriteValue("GameIcons", currentGameInfo.Game.GameName, dlg.FileName);

                    GetIcon(currentGameInfo);
                    RefreshGames();
                }
            }
        }

        private void Log(string logMessage)
        {
            if (ini.IniReadValue("Misc", "DebugLog") == "True")
            {
                using (StreamWriter writer = new StreamWriter("debug-log.txt", true))
                {
                    writer.WriteLine($"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}]MAIN: {logMessage}");
                    writer.Close();
                }
            }
        }

        private void ScriptNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(currentGameInfo.Game.Description, "Handler Author's Notes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void GameOptions_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            gameContextMenuStrip.Show(ptLowerLeft);
        }

        private void OpenOrigExePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(currentGameInfo.ExePath);
            if (Directory.Exists(path))
            {
                Process.Start(path);
            }
            else
            {
                MessageBox.Show("Unable to open original executable path for this game.", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteContentFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(gameManager.GetAppContentPath(), currentGameInfo.Game.GUID);
            if (Directory.Exists(path))
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete '" + path + "' and all its contents?", "Confirm deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    Directory.Delete(path, true);
                }
            }
            else
            {
                MessageBox.Show("No data in content folder to delete.");
            }
        }

        private void btn_scriptAuthorTxt_Click(object sender, EventArgs e)
        {
            if (scriptAuthorTxtSizer.Visible)
            {
                scriptAuthorTxtSizer.Visible = false;
                scriptAuthorTxt.Visible = false;
                txt_GameDescSizer.Visible = true;
                txt_GameDesc.Visible = true;
            }
        }

        private void btn_GameDesc_Click(object sender, EventArgs e)
        {
            if (txt_GameDescSizer.Visible)
            {
                scriptAuthorTxtSizer.Visible = true;
                scriptAuthorTxt.Visible = true;
                txt_GameDescSizer.Visible = false;
                txt_GameDesc.Visible = false;
            }
        }

        private void btn_Download_Click(object sender, EventArgs e)
        {
            getId.ShowDialog();
        }

        private void button_UpdateAvailable_Click(object sender, EventArgs e)
        {
            hubHandler = getId.GetHandler(currentGameInfo.Game.GetHubId());

            if (hubHandler == null)
            {
                MessageBox.Show("Error fetching update information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("An update to this handler is available, download it?", "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    currentlyUpdatingScript = true;

                    DeleteGame(true);
                    StepPanel.Visible = false;
                    label_StepTitle.Text = "Select a game";
                    btn_Play.Enabled = false;
                    btn_Next.Enabled = false;
                   // btn_gameOptions.Visible = false;
                    button_UpdateAvailable.Visible = false;
                    stepsList.Clear();

                    downloadPrompt = new DownloadPrompt(hubHandler, this, null, true);
                    list_Games.SuspendLayout();
                    downloadPrompt.ShowDialog();
                    list_Games.ResumeLayout();
                    StepPanel.Visible = true;

                }
            }
        }


        private void btn_Extract_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog
            {
                Title = "Select a game handler to extract",
                DefaultExt = "nc",
                InitialDirectory = Gaming.GameManager.Instance.GetJsScriptsPath(),
                Filter = "nc files (*.nc)|*.nc"
            };

            DialogResult result = ofd.ShowDialog();
            if (result == DialogResult.OK)
            {
                DownloadPrompt downloadPrompt = new DownloadPrompt(null, this, ofd.FileName);
                downloadPrompt.ShowDialog();
            }
        }

        private void btn_SplitCalculator_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start((Path.Combine(Application.StartupPath, @"utils\SplitCalculator\SplitCalculator.exe")));
            }
            catch (Exception)
            {
                MessageBox.Show(@"SplitCalculator.exe has not been found in the utils\SplitCalculator folder. Try again with a fresh Nucleus Co-op installation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            CheckNetCon();
        }

       

        private void btn_Links_Click(object sender, EventArgs e)
        {

            if (linksPanel.Visible)
            {
                linksPanel.Visible = false;
                btn_Links.BackgroundImage = new Bitmap(themePath + "\\title_dropdown_closed.png");

                if (third_party_tools_container.Visible)
                {
                    third_party_tools_container.Visible = false;
                }
            }
            else
            {
                linksPanel.Visible = true;
                btn_Links.BackgroundImage = new Bitmap(themePath + "\\title_dropdown_opened.png");
            }
        }

        private void splash_Click(object sender, EventArgs e)
        {
            splayer.Stop();
            splayer.Dispose();
            splash.Dispose();
        }

        private void this_Click(object sender, System.EventArgs e)
        {

            linksPanel.Visible = false;
            btn_Links.BackgroundImage = new Bitmap(themePath + "\\title_dropdown_closed.png");

            if (third_party_tools_container.Visible)
            {
                third_party_tools_container.Visible = false;
            }
        }

        private void MainForm_MouseHover(object sender, EventArgs e)
        {
            if (!currentlyUpdatingScript)
            {
                button_UpdateAvailable.Visible = currentGameInfo?.Game?.IsUpdateAvailable(false) ?? button_UpdateAvailable.Visible;
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Process.Start("https://hub.splitscreen.me/");
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start(faq_link);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Process.Start(faq_link);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.com/invite/QDUt8HpCvr");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.reddit.com/r/nucleuscoop/");
        }
        private void logo_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/SplitScreen-Me/splitscreenme-nucleus/releases");
        }
        private void link_faq_Click(object sender, EventArgs e)
        {
            Process.Start(faq_link);
        }
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/nefarius/ScpToolkit/releases");
            third_party_tools_container.Visible = false;
        }
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/ViGEm/HidHide/releases");
            third_party_tools_container.Visible = false;
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/Ryochan7/DS4Windows/releases");
            third_party_tools_container.Visible = false;
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/csutorasa/XOutput/releases");
            third_party_tools_container.Visible = false;
        }
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://discord.com/invite/QDUt8HpCvr");
        }
        private void btn_thirdPartytools_Click(object sender, EventArgs e)
        {
            if (third_party_tools_container.Visible)
            {
                third_party_tools_container.Visible = false;
            }
            else
            {
                third_party_tools_container.Visible = true;
            }
        }
        private void scriptAuthorTxt_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void closeBtn_MouseEnter(object sender, EventArgs e)
        {
            closeBtn.BackgroundImage = new Bitmap(themePath + "\\title_close_mousehover.png");

        }

        private void closeBtn_MouseLeave(object sender, EventArgs e)
        {
            closeBtn.BackgroundImage = new Bitmap(themePath + "\\title_close.png");
        }

        private void maximizeBtn_MouseEnter(object sender, EventArgs e)
        {
            maximizeBtn.BackgroundImage = new Bitmap(themePath + "\\title_maximize_mousehover.png");
        }

        private void maximizeBtn_MouseLeave(object sender, EventArgs e)
        {
            maximizeBtn.BackgroundImage = new Bitmap(themePath + "\\title_maximize.png");
        }

        private void minimizeBtn_MouseLeave(object sender, EventArgs e)
        {
            minimizeBtn.BackgroundImage = new Bitmap(themePath + "\\title_minimize.png");
        }

        private void minimizeBtn_MouseEnter(object sender, EventArgs e)
        {
            minimizeBtn.BackgroundImage = new Bitmap(themePath + "\\title_minimize_mousehover.png");
        }

        private void btn_settings_MouseEnter(object sender, EventArgs e)
        {
            btn_settings.BackgroundImage = new Bitmap(themePath + "\\title_settings_mousehover.png");
        }

        private void btn_settings_MouseLeave(object sender, EventArgs e)
        {
            btn_settings.BackgroundImage = new Bitmap(themePath + "\\title_settings.png");
        }

        private void btn_downloadAssets_MouseEnter(object sender, EventArgs e)
        {
            btn_downloadAssets.BackgroundImage = new Bitmap(themePath + "\\title_download_assets_mousehover.png");
        }

        private void btn_downloadAssets_MouseLeave(object sender, EventArgs e)
        {
            btn_downloadAssets.BackgroundImage = new Bitmap(themePath + "\\title_download_assets.png");
        }

        private void btn_faq_MouseEnter(object sender, EventArgs e)
        {
            btn_faq.BackgroundImage = new Bitmap(themePath + "\\faq_mousehover.png");
        }

        private void btn_faq_MouseLeave(object sender, EventArgs e)
        {
            btn_faq.BackgroundImage = new Bitmap(themePath + "\\faq.png");
        }

        private void btn_reddit_MouseEnter(object sender, EventArgs e)
        {
            btn_reddit.BackgroundImage = new Bitmap(themePath + "\\reddit_mousehover.png");
        }

        private void btn_reddit_MouseLeave(object sender, EventArgs e)
        {
            btn_reddit.BackgroundImage = new Bitmap(themePath + "\\reddit.png");
        }

        private void btn_Discord_MouseEnter(object sender, EventArgs e)
        {
            btn_Discord.BackgroundImage = new Bitmap(themePath + "\\discord_mousehover.png");
        }

        private void btn_Discord_MouseLeave(object sender, EventArgs e)
        {
            btn_Discord.BackgroundImage = new Bitmap(themePath + "\\discord.png");
        }

        private void btn_SplitCalculator_MouseEnter(object sender, EventArgs e)
        {
            btn_SplitCalculator.BackgroundImage = new Bitmap(themePath + "\\splitcalculator_mousehover.png");

        }

        private void btn_SplitCalculator_MouseLeave(object sender, EventArgs e)
        {
            btn_SplitCalculator.BackgroundImage = new Bitmap(themePath + "\\splitcalculator.png");

        }

        private void btn_thirdPartytools_MouseEnter(object sender, EventArgs e)
        {
            btn_thirdPartytools.BackgroundImage = new Bitmap(themePath + "\\thirdPartytools_mousehover.png");
        }

        private void btn_thirdPartytools_MouseLeave(object sender, EventArgs e)
        {
            btn_thirdPartytools.BackgroundImage = new Bitmap(themePath + "\\thirdPartytools.png");
        }

    }
}
