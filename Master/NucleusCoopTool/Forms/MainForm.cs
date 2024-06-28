using Nucleus.Coop.Controls;
using Nucleus.Coop.Forms;
using Nucleus.Coop.Tools;
using Nucleus.Gaming;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Controls;
using Nucleus.Gaming.Controls.SetupScreen;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.Generic;
using Nucleus.Gaming.Coop.InputManagement;
using Nucleus.Gaming.Coop.InputManagement.Gamepads;
using Nucleus.Gaming.Forms.NucleusMessageBox;
using Nucleus.Gaming.Generic.Step;
using Nucleus.Gaming.Platform.PCSpecs;
using Nucleus.Gaming.Tools.GlobalWindowMethods;
using Nucleus.Gaming.UI;
using Nucleus.Gaming.Windows;
using Nucleus.Gaming.Windows.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WindowScrape.Static;

namespace Nucleus.Coop
{
    /// <summary>
    /// Central UI class to the Nucleus Coop application
    /// </summary>
    public partial class MainForm : BaseForm, IDynamicSized
    {
        public readonly string version = "v" + Globals.Version;
        public readonly IniFile themeIni = Globals.ThemeConfigFile;
        public readonly string theme = Globals.ThemeFolder;

        protected string faq_link = "https://www.splitscreen.me/docs/faq";
        protected string api = "https://hub.splitscreen.me/api/v1/";
        private string NucleusEnvironmentRoot => Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        private string DocumentsRoot => Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public string customFont;
        public string lockKeyIniString;
        public string[] rgb_font;
        public string[] rgb_MouseOverColor;
        public string[] rgb_MenuStripBackColor;
        public string[] rgb_MenuStripFontColor;
        public string[] rgb_BorderGradient;
        public string[] rgb_BorderColor;
        public string[] rgb_HandlerNoteTitleFont;
        public string[] rgb_HandlerNoteFontColor;
        public string[] rgb_HandlerNoteTitleFontColor;
        public string[] rgb_ButtonsBorderColor;
        public string[] rgb_ThirdPartyToolsLinks;

        public XInputShortcutsSetup Xinput_S_Setup;
        private Settings settings = null;
        private ProfileSettings profileSettings = null;
        private DonationPanel donationPanel = null;

        private ContentManager content;
        private IGameHandler I_GameHandler;
        public GameManager gameManager;
        public Dictionary<UserGameInfo, GameControl> controls;

        private GameControl currentControl;
        private GameControl menuCurrentControl;
        private AddGameButton btn_AddGame;
        private UserGameInfo currentGameInfo;
        private UserGameInfo menuCurrentGameInfo;
        private GenericGameInfo currentGame;
        private GameProfile currentProfile;

        private UserInputControl currentStep;
        public SetupScreenControl setupScreen;
        private PlayerOptionsControl optionsControl;
        private JSUserInputControl jsControl;
        private Handler handler = null;
        private DownloadPrompt downloadPrompt;
        public HubWebView webView;
        public HandlerNotesZoom handlerNotesZoom;
        private Button nearestVisible;

        public int currentStepIndex { get; private set; }

        private List<string> profilePaths = new List<string>();
        private List<Control> allFormControls = new List<Control>();
        private List<UserInputControl> stepsList;

        public Action<IntPtr> RawInputAction { get; set; }

        public Bitmap defBackground;

        public bool restartRequired = false;

        private bool formClosing;
        private bool noGamesPresent;
        public bool roundedCorners;
        private bool canResize = false;
        private bool hotkeysCooldown = false;
        private bool rainbowTimerRunning = false;
        private bool disableForcedNote;
        public bool ShowFavoriteOnly { get; set; }
        private bool enableParticles;
        
        private bool disableGameProfiles;
        public bool DisableGameProfiles
        {
            get => disableGameProfiles;
            set
            {
                if (disableGameProfiles != value)
                {
                    disableGameProfiles = value;
                    RefreshUI(true);
                }
            }
        }

        private static bool connected;
        public bool Connected
        {
            get => connected;
            set
            {
                connected = value;
                Hub.Connected = value;

                if (value == true)
                {
                    RefreshGames();
                    btn_downloadAssets.Enabled = true;

                    System.Threading.Tasks.Task.Run(() =>
                    {
                        foreach (KeyValuePair<string, GenericGameInfo> game in gameManager.Games)
                        {
                            game.Value.UpdateAvailable = game.Value.Hub.IsUpdateAvailable(true);
                        }
                    });

                    if (currentControl != null)
                    {
                        button_UpdateAvailable.Visible = currentControl.GameInfo.UpdateAvailable;
                    }

                    if (btn_AddGame != null)
                    {
                        btn_AddGame.Update(value);
                    }
                }
            }
        }

        private System.Windows.Forms.Timer WebStatusTimer;
        private System.Windows.Forms.Timer rainbowTimer;
        private System.Windows.Forms.Timer hotkeysCooldownTimer;//Avoid hotkeys spamming

        public Color buttonsBackColor;
        public Color BorderGradient;
        public Color GameBorderGradientTop;
        public Color GameBorderGradientBottom;
        public Color MouseOverBackColor;
        public Color MenuStripBackColor;
        public Color MenuStripFontColor;
        public Color ButtonsBorderColor;
        private Color HandlerNoteFontColor;
        private Color HandlerNoteTitleFont;
        private Color SelectionBackColor;
        private int[] backGradient;

        public SolidBrush borderBrush;
        private Rectangle osdBounds;
        public FileInfo fontPath;

        public Cursor hand_Cursor;
        public Cursor default_Cursor;

        private void Controlscollect()
        {
            foreach (Control control in Controls)
            {
                allFormControls.Add(control);

                foreach (Control container1 in control.Controls)
                {
                    allFormControls.Add(container1);
                    foreach (Control container2 in container1.Controls)
                    {
                        allFormControls.Add(container2);
                        foreach (Control container3 in container2.Controls)
                        {
                            allFormControls.Add(container3);
                        }
                    }
                }
            }
        }

        private System.Windows.Forms.Timer FadeInTimer;

        private void FadeIn()
        {
            FadeInTimer = new System.Windows.Forms.Timer();
            FadeInTimer.Interval = (50); //millisecond
            FadeInTimer.Tick += new EventHandler(FadeInTick);
            FadeInTimer.Start();
        }

        private void FadeInTick(object Object, EventArgs EventArgs)
        {
            if (Opacity < 1.0F)
            {
                Opacity += .1;
            }
            else
            {
                Globals.MainOSD = new WPF_OSD(osdBounds);
                FadeInTimer.Dispose();
            }
        }

        private System.Windows.Forms.Timer FadeOutTimer;

        private void FadeOut()
        {
            if (webView != null)
            {
               if(webView.Downloading)
                return;
            }

            formClosing = true;
            I_GameHandlerEndFunc("Close button clicked", true);

            FadeOutTimer = new System.Windows.Forms.Timer();
            FadeOutTimer.Interval = (50); //millisecond
            FadeOutTimer.Tick += new EventHandler(FadeOutTick);
            FadeOutTimer.Start();
        }

        private void FadeOutTick(object Object, EventArgs EventArgs)
        {
            if (Opacity > 0.0F)
            {
                Opacity -= .1;
            }
            else
            {
                SaveNucleusWindowPosAndLoc();

                Process[] processes = Process.GetProcessesByName("SplitCalculator");
                foreach (Process SplitCalculator in processes)
                {
                    SplitCalculator.Kill();
                }

                Process.GetCurrentProcess().Kill();
            }
        }

        public MainForm()
        {
            FadeIn();

            connected = Program.Connected;
            Hub.Connected = connected;

            ShowFavoriteOnly = bool.Parse(Globals.ini.IniReadValue("Dev", "ShowFavoriteOnly"));
            roundedCorners = bool.Parse(themeIni.IniReadValue("Misc", "UseroundedCorners"));
            disableGameProfiles = bool.Parse(ini.IniReadValue("Misc", "DisableGameProfiles"));
            disableForcedNote = bool.Parse(ini.IniReadValue("Misc", "DisableForcedNote"));
         
            customFont = themeIni.IniReadValue("Font", "FontFamily");
            rgb_font = themeIni.IniReadValue("Colors", "Font").Split(',');
            enableParticles = bool.Parse(themeIni.IniReadValue("Misc", "EnableParticles"));

            rgb_MouseOverColor = themeIni.IniReadValue("Colors", "MouseOver").Split(',');
            rgb_MenuStripBackColor = themeIni.IniReadValue("Colors", "MenuStripBack").Split(',');
            rgb_MenuStripFontColor = themeIni.IniReadValue("Colors", "MenuStripFont").Split(',');
            rgb_BorderColor = themeIni.IniReadValue("Colors", "WindowBorder").Split(',');
            rgb_BorderGradient = themeIni.IniReadValue("Colors", "WindowBorderGradient").Split(',');
            rgb_HandlerNoteFontColor = themeIni.IniReadValue("Colors", "HandlerNoteFont").Split(',');
            rgb_HandlerNoteTitleFontColor = themeIni.IniReadValue("Colors", "HandlerNoteTitleFont").Split(',');
            rgb_ButtonsBorderColor = themeIni.IniReadValue("Colors", "ButtonsBorder").Split(',');

            float fontSize = float.Parse(themeIni.IniReadValue("Font", "MainFontSize"));
            bool coverBorderOff = bool.Parse(themeIni.IniReadValue("Misc", "DisableCoverBorder"));
            bool noteBorderOff = bool.Parse(themeIni.IniReadValue("Misc", "DisableNoteBorder"));

            borderBrush = new SolidBrush(Color.FromArgb(int.Parse(rgb_BorderColor[0]), int.Parse(rgb_BorderColor[1]), int.Parse(rgb_BorderColor[2])));
            BorderGradient = Color.FromArgb(int.Parse(rgb_BorderGradient[0]), int.Parse(rgb_BorderGradient[1]), int.Parse(rgb_BorderGradient[2]));
            MouseOverBackColor = Color.FromArgb(int.Parse(rgb_MouseOverColor[0]), int.Parse(rgb_MouseOverColor[1]), int.Parse(rgb_MouseOverColor[2]), int.Parse(rgb_MouseOverColor[3]));
            MenuStripBackColor = Color.FromArgb(int.Parse(rgb_MenuStripBackColor[0]), int.Parse(rgb_MenuStripBackColor[1]), int.Parse(rgb_MenuStripBackColor[2]));
            MenuStripFontColor = Color.FromArgb(int.Parse(rgb_MenuStripFontColor[0]), int.Parse(rgb_MenuStripFontColor[1]), int.Parse(rgb_MenuStripFontColor[2]));
            HandlerNoteFontColor = Color.FromArgb(int.Parse(rgb_HandlerNoteFontColor[0]), int.Parse(rgb_HandlerNoteFontColor[1]), int.Parse(rgb_HandlerNoteFontColor[2]));

            HandlerNoteTitleFont = Color.FromArgb(int.Parse(rgb_HandlerNoteTitleFontColor[0]), int.Parse(rgb_HandlerNoteTitleFontColor[1]), int.Parse(rgb_HandlerNoteTitleFontColor[2]));
            ButtonsBorderColor = Color.FromArgb(int.Parse(rgb_ButtonsBorderColor[0]), int.Parse(rgb_ButtonsBorderColor[1]), int.Parse(rgb_ButtonsBorderColor[2]));
            SelectionBackColor = Theme_Settings.SelectedBackColor;

            lockKeyIniString = ini.IniReadValue("Hotkeys", "LockKey");

            InitializeComponent();

            if (ini.IniReadValue("Misc", "WindowSize") != "")
            {
                string[] windowSize = ini.IniReadValue("Misc", "WindowSize").Split('X');
                Size = new Size(int.Parse(windowSize[0]), int.Parse(windowSize[1]));
            }

            default_Cursor = Theme_Settings.Default_Cursor;
            hand_Cursor = Theme_Settings.Hand_Cursor;

            Cursor = default_Cursor;

            if (roundedCorners)
            {
                Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
                clientAreaPanel.Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, clientAreaPanel.Width, clientAreaPanel.Height, 20, 20));
            }

            Font = new Font(customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
            ForeColor = Color.FromArgb(int.Parse(rgb_font[0]), int.Parse(rgb_font[1]), int.Parse(rgb_font[2]));

            scriptAuthorTxt.ForeColor = HandlerNoteFontColor;

            lastPlayedAt.ForeColor = ForeColor;
            playTime.ForeColor = ForeColor;

            scriptAuthorTxtSizer.BackColor = Color.FromArgb(int.Parse(themeIni.IniReadValue("Colors", "HandlerNoteBackground").Split(',')[0]),
                                               int.Parse(themeIni.IniReadValue("Colors", "HandlerNoteBackground").Split(',')[1]),
                                               int.Parse(themeIni.IniReadValue("Colors", "HandlerNoteBackground").Split(',')[2]),
                                               int.Parse(themeIni.IniReadValue("Colors", "HandlerNoteBackground").Split(',')[3]));

            infoPanel.BackColor = Color.Transparent;

            buttonsBackColor = Color.FromArgb(int.Parse(themeIni.IniReadValue("Colors", "ButtonsBackground").Split(',')[0]),
                                               int.Parse(themeIni.IniReadValue("Colors", "ButtonsBackground").Split(',')[1]),
                                               int.Parse(themeIni.IniReadValue("Colors", "ButtonsBackground").Split(',')[2]),
                                               int.Parse(themeIni.IniReadValue("Colors", "ButtonsBackground").Split(',')[3]));

            btn_Play.ForeColor = Color.FromArgb(int.Parse(themeIni.IniReadValue("Colors", "PlayButtonFont").Split(',')[0]),
                                               int.Parse(themeIni.IniReadValue("Colors", "PlayButtonFont").Split(',')[1]),
                                               int.Parse(themeIni.IniReadValue("Colors", "PlayButtonFont").Split(',')[2]),
                                               int.Parse(themeIni.IniReadValue("Colors", "PlayButtonFont").Split(',')[3]));

            mainButtonFrame.BackColor = Color.FromArgb(int.Parse(themeIni.IniReadValue("Colors", "MainButtonFrameBackground").Split(',')[0]),
                                               int.Parse(themeIni.IniReadValue("Colors", "MainButtonFrameBackground").Split(',')[1]),
                                               int.Parse(themeIni.IniReadValue("Colors", "MainButtonFrameBackground").Split(',')[2]),
                                               int.Parse(themeIni.IniReadValue("Colors", "MainButtonFrameBackground").Split(',')[3]));

            rightFrame.BackColor = Color.FromArgb(int.Parse(themeIni.IniReadValue("Colors", "RightFrameBackground").Split(',')[0]),
                                           int.Parse(themeIni.IniReadValue("Colors", "RightFrameBackground").Split(',')[1]),
                                           int.Parse(themeIni.IniReadValue("Colors", "RightFrameBackground").Split(',')[2]),
                                           int.Parse(themeIni.IniReadValue("Colors", "RightFrameBackground").Split(',')[3]));

            icons_Container.BackColor = icons_Container.BackColor = Color.FromArgb(rightFrame.BackColor.A - rightFrame.BackColor.A, rightFrame.BackColor.R, rightFrame.BackColor.G, rightFrame.BackColor.B);

            game_listSizer.BackColor = Color.FromArgb(int.Parse(themeIni.IniReadValue("Colors", "GameListBackground").Split(',')[0]),
                                               int.Parse(themeIni.IniReadValue("Colors", "GameListBackground").Split(',')[1]),
                                               int.Parse(themeIni.IniReadValue("Colors", "GameListBackground").Split(',')[2]),
                                               int.Parse(themeIni.IniReadValue("Colors", "GameListBackground").Split(',')[3]));

            StepPanel.BackColor = Color.FromArgb(int.Parse(themeIni.IniReadValue("Colors", "SetupScreenBackground").Split(',')[0]),
                                                int.Parse(themeIni.IniReadValue("Colors", "SetupScreenBackground").Split(',')[1]),
                                                int.Parse(themeIni.IniReadValue("Colors", "SetupScreenBackground").Split(',')[2]),
                                                int.Parse(themeIni.IniReadValue("Colors", "SetupScreenBackground").Split(',')[3]));

            backGradient = new int[] {int.Parse(themeIni.IniReadValue("Colors", "BackgroundGradient").Split(',')[0]),
                                       int.Parse(themeIni.IniReadValue("Colors", "BackgroundGradient").Split(',')[1]),
                                       int.Parse(themeIni.IniReadValue("Colors", "BackgroundGradient").Split(',')[2]),
                                       int.Parse(themeIni.IniReadValue("Colors", "BackgroundGradient").Split(',')[2])};

            clientAreaPanel.BackgroundImage = ImageCache.GetImage(theme + "background.jpg");

            button_UpdateAvailable.BackgroundImage = ImageCache.GetImage(theme + "update.png");

            btn_gameOptions.BackColor = buttonsBackColor;
            btn_Play.BackColor = buttonsBackColor;

            btn_gameOptions.BackgroundImage = ImageCache.GetImage(theme + "game_options.png");
            btn_Prev.BackgroundImage = ImageCache.GetImage(theme + "arrow_left.png");
            btn_Next.BackgroundImage = ImageCache.GetImage(theme + "arrow_right.png");
            coverFrame.BackgroundImage = ImageCache.GetImage(theme + "cover_layer.png");
            stepPanelPictureBox.Image = ImageCache.GetImage(theme + "logo.png");
            logo.BackgroundImage = ImageCache.GetImage(theme + "title_logo.png");
            btn_downloadAssets.BackgroundImage = ImageCache.GetImage(theme + "title_download_assets.png");

            btn_Links.BackgroundImage = ImageCache.GetImage(theme + "title_dropdown_closed.png");
            closeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_close.png");
            maximizeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_maximize.png");
            minimizeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_minimize.png");
            btn_settings.BackgroundImage = ImageCache.GetImage(theme + "title_settings.png");
            btn_expandNotes.Image = ImageCache.GetImage(theme + "expand_Notes.png");

            btn_Extract.BackgroundImage = ImageCache.GetImage(theme + "extract_nc.png");
            btnSearch.BackgroundImage = ImageCache.GetImage(theme + "search_game.png");
            btn_debuglog.BackgroundImage = ImageCache.GetImage(theme + "log.png");
            donationBtn.BackgroundImage = ImageCache.GetImage(theme + "donation.png");

            instruction_btn.BackgroundImage = ImageCache.GetImage(theme + "instruction_closed.png");
            profilesList_btn.BackgroundImage = ImageCache.GetImage(theme + "profiles_list.png");
            profileSettings_btn.BackgroundImage = ImageCache.GetImage(theme + "profile_settings.png");

            if (ini.IniReadValue("Misc", "DebugLog") == "True")
            {
                btn_debuglog.Visible = true;
            }

            CustomToolTips.SetToolTip(btn_Extract, "Extract a handler from a \".nc\" archive.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(btnSearch, "Search and add a game to the game list (its handler must be installed).", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(btn_downloadAssets, "Download or update games covers and screenshots.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(btn_settings, "Global Nucleus Co-op settings.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(btn_debuglog, "Open Nucleus debug-log.txt file if available, debug log can be disabled in Nucleus settings in the \"Settings\" tab.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(donationBtn, "Nucleus Co-op developers donations links.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(instruction_btn, "How to setup players.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(btn_gameOptions, "Game options menu.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(button_UpdateAvailable, "Update game handler.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(btn_expandNotes, "Expand handler notes.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });

            btn_Extract.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button_UpdateAvailable.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnSearch.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn_gameOptions.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn_Play.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn_Prev.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn_Next.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn_debuglog.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btn_debuglog.BackColor = Color.Transparent;
            donationBtn.FlatAppearance.MouseOverBackColor = Color.Transparent;

            instruction_btn.BackgroundImage = ImageCache.GetImage(theme + "instruction_closed.png");
            profilesList_btn.BackgroundImage = ImageCache.GetImage(theme + "profiles_list.png");
            profileSettings_btn.BackgroundImage = ImageCache.GetImage(theme + "profile_settings.png");

            gameContextMenuStrip.BackColor = MenuStripBackColor;
            gameContextMenuStrip.ForeColor = MenuStripFontColor;

            btn_expandNotes.Cursor = hand_Cursor;

            logo.Cursor = hand_Cursor;
            gameContextMenuStrip.Cursor = hand_Cursor;
            socialLinksMenu.Cursor = hand_Cursor;
            button_UpdateAvailable.Cursor = hand_Cursor;
            saveProfileRadioBtn.TickCursor = hand_Cursor;

            Globals.PlayButton = btn_Play;
            Globals.Btn_debuglog = btn_debuglog;
            Globals.ProfilesList_btn = profilesList_btn;

            if (coverBorderOff)
            {
                cover.BorderStyle = BorderStyle.None;
            }

            if (noteBorderOff)
            {
                scriptAuthorTxtSizer.BorderStyle = BorderStyle.None;
            }

            Controlscollect();

            foreach (Control control in allFormControls)
            {
                if (control.Name != "HandlerNoteTitle" && control.Name != "scriptAuthorTxt")
                {
                    if (control == btn_Play)
                    {
                        control.Font = new Font(customFont, fontSize, FontStyle.Bold, GraphicsUnit.Pixel, 0);
                    }

                    if (!(control is TransparentRichTextBox) && control != InputsTextLabel)
                    {
                        control.Font = new Font(customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }
                }

                if (control is Button)
                {
                    control.Cursor = hand_Cursor;
                }

                control.Click += new EventHandler(ClickAnyControl);
            }
#if DEBUG
            txt_version.ForeColor = Color.LightSteelBlue;
            txt_version.Text = "DEBUG " + version;
#else
            if (bool.Parse(themeIni.IniReadValue("Misc", "HideVersion")) == false)
            {
                txt_version.Text = version;
                txt_version.ForeColor = ForeColor;
            }
            else
            {
                txt_version.Text = "";
            }
#endif

            foreach (Control b in setupButtonsPanel.Controls)
            {
                if (b is CustomRadioButton) { continue; }
                b.MouseEnter += Btn_ZoomIn;
                b.MouseLeave += Btn_ZoomOut;
            }

            PlayersIdentityCache.LoadPlayersIdentityCache();

            minimizeBtn.Click += new EventHandler(MinimizeButtonClick);
            maximizeBtn.Click += new EventHandler(MaximizeButtonClick);
            closeBtn.Click += new EventHandler(CloseButtonClick);

            defBackground = clientAreaPanel.BackgroundImage as Bitmap;

            setupScreen = new SetupScreenControl();

            profileSettings_btn.Click += new EventHandler(ProfileSettings_btn_Click);
            profilesList_btn.Click += new EventHandler(ProfilesList_btn_Click);

            setupScreen.OnCanPlayUpdated += StepCanPlay;
            setupScreen.Click += new EventHandler(ClickAnyControl);

            settings = new Settings(this, setupScreen);
            profileSettings = new ProfileSettings(this, setupScreen);

            setupScreen.Paint += SetupScreen_Paint;

            controls = new Dictionary<UserGameInfo, GameControl>();
            gameManager = new GameManager();
            optionsControl = new PlayerOptionsControl();
            jsControl = new JSUserInputControl();

            optionsControl.OnCanPlayUpdated += StepCanPlay;
            jsControl.OnCanPlayUpdated += StepCanPlay;

            downloadPrompt = new DownloadPrompt(handler, this, null, true);
            Xinput_S_Setup = new XInputShortcutsSetup();

            handlerNotesZoom = new HandlerNotesZoom
            {
                Visible = false,
                Size = clientAreaPanel.Size,
                Location = new Point(0, 0),
                Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom,
            };

            clientAreaPanel.Controls.Add(handlerNotesZoom);
            Globals.HandlerNotesZoom = handlerNotesZoom;

            hotkeysCooldownTimer = new System.Windows.Forms.Timer();
            hotkeysCooldownTimer.Tick += new EventHandler(HotkeysCooldownTimerTick);

            gameContextMenuStrip.Renderer = new CustomToolStripRenderer.MyRenderer();
            gameContextMenuStrip.BackgroundImage = ImageCache.GetImage(theme + "other_backgrounds.jpg");

            socialLinksMenu.Renderer = new CustomToolStripRenderer.MyRenderer();
            socialLinksMenu.BackgroundImage = ImageCache.GetImage(theme + "other_backgrounds.jpg");

            GetGameIcon.MainForm = this;

            RefreshGames();

            StartPosition = FormStartPosition.Manual;

            Rectangle area = Screen.PrimaryScreen.Bounds;
            osdBounds = area;

            if (ini.IniReadValue("Misc", "WindowLocation") != "")
            {
                string[] windowLocation = ini.IniReadValue("Misc", "WindowLocation").Split('X');

                if (ScreensUtil.AllScreens().All(s => !s.MonitorBounds.Contains(int.Parse(windowLocation[0]), int.Parse(windowLocation[1]))))
                {
                    CenterToScreen();
                }
                else
                {
                    var destBoundsRect = ScreensUtil.AllScreens().Where(s => s.MonitorBounds.Contains(int.Parse(windowLocation[0]), int.Parse(windowLocation[1]))).FirstOrDefault().MonitorBounds;
                    osdBounds = destBoundsRect;
                    Location = new Point(area.X + int.Parse(windowLocation[0]), area.Y + int.Parse(windowLocation[1]));
                }

                if (Size == area.Size)
                {
                    WindowState = FormWindowState.Maximized;
                }
            }
            else
            {
                CenterToScreen();
            }

            //Enable only for Windows versions with default support for xinput1.4.dll ,
            //might be fixable by placing the dll at the root of our exe but not for now.
            string windowsVersion = MachineSpecs.GetPCspecs();
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
            else
            {
                Settings._ctrlr_shorcuts.Text = "Windows 8™ and up only";
                Settings._ctrlr_shorcuts.Enabled = false;
            }

            DPIManager.Register(this);
            DPIManager.AddForm(this);
        }

        public new void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }

            float mainButtonFrameFont = mainButtonFrame.Font.Size * 1.0f;

            if (scale > 1.0f)
            {
                foreach (Control button in mainButtonFrame.Controls)
                {
                    if (button != InputsTextLabel)
                    {
                        button.Font = new Font(customFont, mainButtonFrameFont, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }
                }
            }

            gameContextMenuStrip.Font = new Font(gameContextMenuStrip.Font.FontFamily, 10.25f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
            socialLinksMenu.Font = new Font(gameContextMenuStrip.Font.FontFamily, 10.25f, FontStyle.Regular, GraphicsUnit.Pixel, 0);

            btn_Play.Font = new Font(customFont, mainButtonFrameFont, FontStyle.Bold, GraphicsUnit.Pixel, 0);

            scriptAuthorTxt.Font = new Font(customFont, 12 * scale, FontStyle.Regular, GraphicsUnit.Pixel, 0);
            //scriptAuthorTxt.Size = new Size((int)(189 * scale), (int)(191 * scale));

            lastPlayedAt.Font = new Font(customFont, 10, FontStyle.Bold, GraphicsUnit.Pixel, 0);
            lastPlayedAtValue.Font = new Font(customFont, 10, FontStyle.Bold, GraphicsUnit.Pixel, 0);

            playTime.Font = new Font(customFont, 10, FontStyle.Bold, GraphicsUnit.Pixel, 0);
            playTimeValue.Font = new Font(customFont, 10, FontStyle.Bold, GraphicsUnit.Pixel, 0);

            logo.Location = new Point(logo.Location.X, mainButtonFrame.Height / 2 - logo.Height / 2);
            txt_version.Location = new Point(logo.Right, logo.Location.Y + logo.Height / 2 - txt_version.Height / 2);

            saveProfileRadioBtn.Location = new Point((profilesList_btn.Left - saveProfileRadioBtn.Width) - 5, saveProfileRadioBtn.Location.Y);
        }

        public void InsertWebview(object sender, EventArgs e)
        {
            if (e is MouseEventArgs click)
            {
                if (click.Button == MouseButtons.Right)
                {
                    return;
                }
            }

            if (GenericGameHandler.Instance != null)
            {
                if (!GenericGameHandler.Instance.HasEnded)
                {
                    return;
                }
            }

            if (webView != null)
            {
                return;
            }

            webView = new HubWebView(this);
            webView.Disposed += WebviewDisposed;
            clientAreaPanel.Controls.Add(webView);

            RefreshUI(true);

            webView.Size = clientAreaPanel.Size;
            webView.Location = game_listSizer.Location;
            btn_AddGame.Selected = true;

            Invalidate(false);
        }

        public void WebviewDisposed(object sender, EventArgs e)
        {
            if (webView == null)
            {
                return;
            }

            if (webView.Downloading)
            { 
                return;
            }

            if (e is MouseEventArgs click)
            {
                if (click.Button == MouseButtons.Right)
                {
                    return;
                }
            }        

            clientAreaPanel.Controls.Remove(webView);
            webView.Dispose();
            webView = null;

            btn_AddGame.Selected = false;

            if (sender is HubWebView)
            {
                stepPanelPictureBox.Visible = true;    
            }

           Invalidate(false);          
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            WebStatusTimer = new System.Windows.Forms.Timer();
            WebStatusTimer.Interval = (2000);
            WebStatusTimer.Tick += new EventHandler(WebStatusTimerTick);
            WebStatusTimer.Start();
            DPIManager.ForceUpdate();        
        }

        private void WebStatusTimerTick(object Object, EventArgs EventArgs)
        {
            if (connected)
            {
                btn_downloadAssets.Enabled = true;
                WebStatusTimer.Dispose();
            }
            else
            {
                btn_downloadAssets.Enabled = false;
            }
        }

        private void SetupScreen_Paint(object sender, PaintEventArgs e)
        {
            if (DevicesFunctions.isDisconnected)
            {
                DPIManager.ForceUpdate();
                DevicesFunctions.isDisconnected = false;
            }
        }

        protected override Size DefaultSize => new Size(1050, 701);

        protected override void WndProc(ref Message m)
        {
            const int RESIZE_HANDLE_SIZE = 10;

            if (this.WindowState == FormWindowState.Normal)
            {
                switch (m.Msg)//resizing messages handling
                {
                    case 0x0084/*NCHITTEST*/ :
                        base.WndProc(ref m);

                        if ((int)m.Result == 0x01/*HTCLIENT*/)
                        {
                            Point screenPoint = new Point(m.LParam.ToInt32());
                            Point clientPoint = this.PointToClient(screenPoint);

                            if (clientPoint.Y <= RESIZE_HANDLE_SIZE)
                            {
                                if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                    m.Result = (IntPtr)13/*HTTOPLEFT*/ ;
                                else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                    m.Result = (IntPtr)12/*HTTOP*/ ;
                                else
                                    m.Result = (IntPtr)14/*HTTOPRIGHT*/ ;
                            }
                            else if (clientPoint.Y <= (Size.Height - RESIZE_HANDLE_SIZE))
                            {
                                if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                    m.Result = (IntPtr)10/*HTLEFT*/ ;
                                else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                    m.Result = (IntPtr)2/*HTCAPTION*/ ;
                                else
                                    m.Result = (IntPtr)11/*HTRIGHT*/ ;
                            }
                            else
                            {
                                if (clientPoint.X <= RESIZE_HANDLE_SIZE)
                                    m.Result = (IntPtr)16/*HTBOTTOMLEFT*/ ;
                                else if (clientPoint.X < (Size.Width - RESIZE_HANDLE_SIZE))
                                    m.Result = (IntPtr)15/*HTBOTTOM*/ ;
                                else
                                    m.Result = (IntPtr)17/*HTBOTTOMRIGHT*/ ;
                            }
                        }

                        return;
                }
            }

            Point cursorPos = PointToClient(Cursor.Position);
            Rectangle outRect = new Rectangle(RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, Width - RESIZE_HANDLE_SIZE * 2, Height - RESIZE_HANDLE_SIZE * 2);

            if (!outRect.Contains(cursorPos))
            {
                canResize = true;
            }
            else
            {
                if (Cursor.Current != default_Cursor)
                {
                    Cursor.Current = default_Cursor;
                }

                canResize = false;
            }

            if (!canResize)
            {
                if (m.Msg == 0x020)//Do not reset custom cursor when the mouse hover over the Form background(needed because of the custom resizing/moving messages handling) 
                {
                    m.Result = IntPtr.Zero;
                    return;
                }
            }

            if (m.Msg == 0x00FF)//WM_INPUT
            {
                RawInputAction(m.LParam);
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == (int)Hotkeys.TopMost_HotkeyID)
            {
                if (hotkeysCooldown || I_GameHandler == null)
                {
                    return;
                }

                GlobalWindowMethods.ShowHideWindows();
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == (int)Hotkeys.StopSession_HotkeyID)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    if (hotkeysCooldown || I_GameHandler == null)
                    {
                        return;
                    }

                    if (btn_Play.Text == "S T O P")
                    {
                        Btn_Play_Click(this, null);
                        TriggerOSD(2000, "Session Ended");
                    }
                }
                else
                {
                    TriggerOSD(1600, $"Unlock Inputs First (Press {lockKeyIniString} Key)");
                }
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == (int)Hotkeys.SetFocus_HotkeyID)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    if (hotkeysCooldown || I_GameHandler == null)
                    {
                        return;
                    }

                    GlobalWindowMethods.ChangeForegroundWindow();
                    TriggerOSD(2000, "Game Windows Unfocused");                  
                }
                else
                {
                    TriggerOSD(1600, $"Unlock Inputs First (Press {lockKeyIniString} Key)");
                }
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == (int)Hotkeys.ResetWindows_HotkeyID)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    if (hotkeysCooldown || I_GameHandler == null)
                    {
                        return;
                    }

                    GlobalWindowMethods.ResetingWindows = true;
                }
                else
                {
                    TriggerOSD(1600, $"Unlock Inputs First (Press {lockKeyIniString} Key)");
                }
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == (int)Hotkeys.Cutscenes_HotkeyID)
            {
                if (hotkeysCooldown || I_GameHandler == null)
                {
                    return;
                }

                GlobalWindowMethods.ToggleCutScenesMode();
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == (int)Hotkeys.Switch_HotkeyID)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    if (hotkeysCooldown || I_GameHandler == null)
                    {
                        return;
                    }

                    GlobalWindowMethods.SwitchLayout();
                }
                else
                {
                    TriggerOSD(1600, $"Unlock Inputs First (Press {lockKeyIniString} Key)");
                }
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == (int)Hotkeys.KillProcess_HotkeyID)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    if (hotkeysCooldown)
                    {
                        return;
                    }

                    User32Util.ShowTaskBar();
                    Close();
                }
                else
                {
                    TriggerOSD(1600, $"Unlock Inputs First (Press {lockKeyIniString} Key)");
                }
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == (int)Hotkeys.Reminder_HotkeyID)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    if (hotkeysCooldown)
                    {
                        return;
                    }

                    foreach (ShortcutsReminder reminder in GenericGameHandler.Instance.shortcutsReminders)
                    {
                        reminder.Toggle(7);
                    }
                }
                else
                {
                    TriggerOSD(1600, $"Unlock Inputs First (Press {lockKeyIniString} Key)");
                }
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == (int)Hotkeys.Merger_WindowSwitch)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    if (hotkeysCooldown)
                    {
                        return;
                    }

                    WindowsMerger.Instance?.SwitchChildFocus();
                }
                else
                {
                    TriggerOSD(1600, $"Unlock Inputs First (Press {lockKeyIniString} Key)");
                }
            }

            base.WndProc(ref m);
        }

        public void TriggerOSD(int timerMS, string text)
        {
            if (!hotkeysCooldown)
            {
                hotkeysCooldownTimer.Stop();
                hotkeysCooldown = true;
                hotkeysCooldownTimer.Interval = (timerMS); //millisecond
                hotkeysCooldownTimer.Start();

                Globals.MainOSD.Show(timerMS, text);
            }
        }

        private void HotkeysCooldownTimerTick(object Object, EventArgs EventArgs)
        {
            hotkeysCooldown = false;
            hotkeysCooldownTimer.Stop();
        }

        public void RefreshGames()
        {
            list_Games.Visible = false;///smoother transition

            lock (controls)
            {
                foreach (KeyValuePair<UserGameInfo, GameControl> con in controls)
                {
                    con.Value?.Dispose();
                }

                list_Games.Controls.Clear();
                controls.Clear();

                List<UserGameInfo> games = gameManager.User.Games;

                if (games.Count == 0)
                {
                    noGamesPresent = true;
                    GameControl con = new GameControl(null, null, false)
                    {
                        Width = game_listSizer.Width,
                        Text = "No games",
                        Font = this.Font,
                    };

                    list_Games.Controls.Add(con);
                }
                else
                {
                    for (int i = 0; i < games.Count; i++)
                    {
                        UserGameInfo game = games[i];

                        if (game.Game == null && games.Count == 1)
                        {
                            noGamesPresent = true;
                            GameControl con = new GameControl(null, null, false)
                            {
                                Width = game_listSizer.Width,
                                Text = "No games",
                                Font = this.Font,
                            };

                            list_Games.Controls.Add(con);

                            break;
                        }

                        NewUserGame(game);
                    }
                }

                if (btn_AddGame == null)
                {
                    btn_AddGame = new AddGameButton(this, game_listSizer.Width, list_Games.Controls[0].Height);
                    game_listSizer.Controls.Add(btn_AddGame);
                    list_Games.Height -= btn_AddGame.Height;
                    btn_AddGame.Anchor = AnchorStyles.Top | AnchorStyles.Left;
                    btn_AddGame.Location = new Point(0, 0);
                    list_Games.Top = btn_AddGame.Bottom;
                }

                list_Games.Visible = true;

                if (currentGame != null)
                {
                    list_Games.ScrollControlIntoView(currentControl);
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

            bool favorite = game.Game.MetaInfo.Favorite;

            GameControl con = new GameControl(game.Game, game, favorite)
            {
                Width = game_listSizer.Width,
            };

            con.Click += new EventHandler(WebviewDisposed);

            if (ShowFavoriteOnly)
            {
                if (favorite || con.GameInfo == currentGame)
                {
                    if (con.GameInfo == currentGame)
                    {
                        con.RadioSelected();
                        currentControl = con;
                    }

                    controls.Add(game, con);
                    list_Games.Controls.Add(con);
                    ThreadPool.QueueUserWorkItem(GetGameIcon.GetIcon, game);
                }
            }
            else
            {
                if (con.GameInfo == currentGame)
                {
                    con.RadioSelected();
                    currentControl = con;
                }

                controls.Add(game, con);
                list_Games.Controls.Add(con);
                ThreadPool.QueueUserWorkItem(GetGameIcon.GetIcon, game);
            }
        }

        public void RefreshUI(bool refreshAll)
        {
            if (refreshAll)
            {
                InputsTextLabel.Text = "";
                currentGame = null;
                setupButtonsPanel.Visible = false;
                rightFrame.Visible = false;
                StepPanel.Visible = false;
                stepPanelPictureBox.Visible = webView == null;
                rainbowTimer?.Dispose();
                rainbowTimerRunning = false;
                btn_Prev.BackgroundImage = ImageCache.GetImage(theme + "arrow_left.png");
                clientAreaPanel.BackgroundImage = defBackground;
            }

            DevicesFunctions.gamepadTimer?.Dispose();
            RefreshGames();

            mainButtonFrame.Focus();
        }

        private void Btn_downloadAssets_Click(object sender, EventArgs e)
        {
            if (gameManager.User.Games.Count == 0)
            {
                TriggerOSD(1600, $"Add Game(s) In Your List");
                return;
            }

            AssetsDownloader.DownloadAllGamesAssets(this, gameManager, currentControl);
        }

        private int r = 0;
        private int g = 0;
        private int b = 0;
        private bool loop = false;

        private void RainbowTimerTick(object Object, EventArgs eventArgs)
        {
            string text = HandlerNoteTitle.Text;

            if (text == "Handler Notes" || text == "Read First")
            {
                if (!loop)
                {
                    HandlerNoteTitle.Text = "Handler Notes";
                    if (r < 200 && b < 200) { r += 3; b += 3; };
                    if (b >= 200 && r >= 200)
                        loop = true;
                    HandlerNoteTitle.Font = new Font(HandlerNoteTitle.Font.FontFamily, HandlerNoteTitle.Font.Size, FontStyle.Bold);
                }
                else
                {
                    HandlerNoteTitle.Text = "Read First";
                    if (r > 0 && b > 0) { r -= 3; b -= 3; }
                    if (b <= 0 && r <= 0)
                        loop = false;
                }

                HandlerNoteTitle.ForeColor = Color.FromArgb(r, r, 255, b);
            }
            else if (text.Contains("Description"))
            {
                HandlerNoteTitle.ForeColor = Color.Gold;
                HandlerNoteTitle.Font = new Font(HandlerNoteTitle.Font.FontFamily, (float)HandlerNoteTitle.Font.Size, FontStyle.Regular);
            }
        }

        private void List_Games_SelectedChanged(object arg1, Control arg2)
        {
            if (GenericGameHandler.Instance != null)
            {
                if (!GenericGameHandler.Instance.HasEnded)
                {
                    return;
                }
            }

            currentControl = (GameControl)arg1;
            currentGameInfo = currentControl.UserGameInfo;

            if (currentGameInfo == null)
            {
                return;
            }

            if (!CheckGameRequirements.MatchRequirements(currentGameInfo))
            {
                RefreshUI(true);
                Invalidate(false);
                return;
            }

            setupButtonsPanel.Visible = false;
            stepPanelPictureBox.Visible = false;
            profileSettings.Visible = false;
            handlerNotesZoom.Visible = false;

            currentGame = currentGameInfo.Game;
            saveProfileRadioBtn.RadioChecked = currentGame.MetaInfo.SaveProfile;

            SetBackroundAndCover.ApplyBackgroundAndCover(this, currentGame.GUID);

            lastPlayedAtValue.Text = currentGame.MetaInfo.LastPlayedAt;
            lastPlayedAtValue.Location = new Point(lastPlayedAt.Right, lastPlayedAt.Location.Y);

            playTimeValue.Text = currentGame.MetaInfo.TotalPlayTime;
            playTimeValue.Location = new Point(playTime.Right, playTime.Location.Y);

            if (!currentGameInfo.Game.KeepSymLinkOnExit)
            {
                if (!currentGameInfo.Game.MetaInfo.KeepSymLink)
                {
                    CleanGameContent.CleanContentFolder(currentGame);
                }
            }

            HandlerNoteTitle.Text = "Handler Notes";

            if (!rainbowTimerRunning)
            {
                rainbowTimer = new System.Windows.Forms.Timer();
                rainbowTimer.Interval = (25); //millisecond                   
                rainbowTimer.Tick += new EventHandler(RainbowTimerTick);
                rainbowTimer.Start();
                rainbowTimerRunning = true;
            }

            icons_Container.Controls.Clear();
            icons_Container.Controls.AddRange(InputIcons.SetInputsIcons(this, currentGame));

            btn_Play.Enabled = false;
            rightFrame.Visible = true;

            StepPanel.Visible = true;

            currentProfile = new GameProfile();

            GameProfile.GameInfo = currentGameInfo;

            stepsList = new List<UserInputControl> { setupScreen, optionsControl };

            for (int i = 0; i < currentGame.CustomSteps.Count; i++)
            {
                stepsList.Add(jsControl);
            }

            currentProfile.InitializeDefault(currentGame, setupScreen);
            gameManager.UpdateCurrentGameProfile(currentProfile);

            if (!disableGameProfiles && !currentGameInfo.Game.MetaInfo.DisableProfiles)
            {
                profileSettings_btn.Visible = true;

                ProfilesList.Instance.Update_ProfilesList();

                bool showList = GameProfile.profilesPathList.Count > 0;

                if (!currentGameInfo.Game.MetaInfo.FirstLaunch)
                {
                    setupScreen.ProfilesList.Visible = showList;
                }

                nearestVisible = showList ? profilesList_btn : profileSettings_btn;
                button_UpdateAvailable.Location = showList ?
                    new Point((profilesList_btn.Left - button_UpdateAvailable.Width) - (button_UpdateAvailable.Margin.Right + nearestVisible.Margin.Left), button_UpdateAvailable.Location.Y) : 
                    new Point((profileSettings_btn.Left - button_UpdateAvailable.Width) - (button_UpdateAvailable.Margin.Right + nearestVisible.Margin.Left), button_UpdateAvailable.Location.Y);

                CustomToolTips.SetToolTip(profilesList_btn, $"{currentGameInfo.Game.GameName} profiles list.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
                CustomToolTips.SetToolTip(profileSettings_btn, $"Profile settings.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });

                profilesList_btn.Visible = showList;

                ProfilesList.Instance.Locked = false;
            }
            else
            {
                profileSettings_btn.Visible = false;
                setupScreen.ProfilesList.Visible = false;
                profilesList_btn.Visible = false;
                nearestVisible = btn_gameOptions;
                button_UpdateAvailable.Location = new Point((btn_gameOptions.Left - button_UpdateAvailable.Width) - (button_UpdateAvailable.Margin.Right + nearestVisible.Margin.Left), button_UpdateAvailable.Location.Y);
            }

            button_UpdateAvailable.Visible = currentGameInfo.Game.UpdateAvailable && currentGame.MetaInfo.CheckUpdate;

            saveProfileRadioBtn.Location = button_UpdateAvailable.Visible ? new Point((button_UpdateAvailable.Left - saveProfileRadioBtn.Width) - 5, saveProfileRadioBtn.Location.Y) : new Point((nearestVisible.Left - saveProfileRadioBtn.Width) - 5, saveProfileRadioBtn.Location.Y);

            if (currentGame.Description?.Length > 0)
            {
                scriptAuthorTxt.Text = currentGame.Description;
                scriptAuthorTxtSizer.Visible = true;
            }
            else
            {
                scriptAuthorTxtSizer.Visible = false;
                scriptAuthorTxt.Text = "";
            }

            if (currentGameInfo.Game.MetaInfo.FirstLaunch && currentGame.Description != null && !disableForcedNote)
            {
                Btn_magnifier_Click(null, null);
            }
            else
            {
                handlerNotesZoom.Visible = false;
            }

            content?.Dispose();

            // content manager is shared within the same game
            content = new ContentManager(currentGame);
            setupButtonsPanel.Visible = true;

            GoToStep(0);
        }

        private void EnablePlay()
        => btn_Play.Enabled = true;

        private void StepCanPlay(UserControl obj, bool canProceed, bool autoProceed)
        {          
            if (canProceed || autoProceed)
            {
                saveProfileRadioBtn.Visible = !GameProfile.Loaded && !currentGameInfo.Game.MetaInfo.DisableProfiles;
            }
            else
            {
                saveProfileRadioBtn.Visible = false;
            }

            if (btn_Prev.Enabled)
            {
                btn_Prev.BackgroundImage = ImageCache.GetImage(theme + "arrow_left_mousehover.png");
            }
            else
            {
                btn_Prev.BackgroundImage = ImageCache.GetImage(theme + "arrow_left.png");
            }

            if (Opacity == 1.0)//If resizing the main window skip that
            {
                setupButtonsPanel.Visible = StepPanel.Visible && currentStepIndex == 0;
            }

            if (!canProceed)
            {
                btn_Prev.Enabled = false;

                if (btn_Play.Text == "START" || btn_Next.Enabled)
                {
                    btn_Play.Enabled = false;
                }

                btn_Next.Enabled = false;
                btn_Next.BackgroundImage = ImageCache.GetImage(theme + "arrow_right.png");
                btn_Prev.BackgroundImage = ImageCache.GetImage(theme + "arrow_left.png");
                return;
            }
            else
            {
                btn_Next.BackgroundImage = ImageCache.GetImage(theme + "arrow_right.png");
            }

            if (currentGame.Options.Count == 0)
            {
                EnablePlay();
                return;
            }

            if (currentStepIndex + 1 > stepsList.Count - 1)
            {
                EnablePlay();
                return;
            }
            else
            {
                if (btn_Play.Text == "START")
                {
                    btn_Play.Enabled = false;
                }
                else
                {
                    btn_Play.Enabled = true;
                }
            }

            if (autoProceed)
            {
                GoToStep(currentStepIndex + 1);
            }
            else
            {
                btn_Next.Enabled = true;
                btn_Next.BackgroundImage = ImageCache.GetImage(theme + "arrow_right_mousehover.png");
            }
        }

        private void BtnNext_Click(object sender, EventArgs e) => GoToStep(currentStepIndex + 1);

        private void KillCurrentStep()
        {
            foreach (Control c in StepPanel.Controls)
            {
                StepPanel.Controls.Remove(c);
            }
        }

        private void GoToStep(int step)
        {
            btn_Prev.Enabled = step > 0;

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

            if (GameProfile.Ready)
            {
                if (currentGame.CustomSteps.Count > 0)
                {
                    jsControl.CustomStep = currentGame.CustomSteps[0];
                    jsControl.Content = content;

                    currentStepIndex = stepsList.Count - 1;
                    currentStep = stepsList[stepsList.Count - 1];
                    currentStep.Size = StepPanel.Size;
                    currentStep.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                    currentStep = stepsList[stepsList.Count - 1];
                    currentStep.Initialize(currentGameInfo, currentProfile);

                    StepPanel.Controls.Add(currentStep);

                    btn_Next.Enabled = currentStep.CanProceed && step != stepsList.Count - 1;

                    if (GameProfile.AutoPlay)
                    {
                        EnablePlay();
                    }

                    return;
                }
            }

            currentStepIndex = step;
            currentStep = stepsList[step];
            currentStep.Size = StepPanel.Size;
            currentStep.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            currentStep.Initialize(currentGameInfo, currentProfile);

            StepPanel.Controls.Add(currentStep);

            btn_Next.Enabled = currentStep.CanProceed && step != stepsList.Count - 1;

            game_listSizer.Refresh();
            StepPanel.Refresh();
            mainButtonFrame.Refresh();
            rightFrame.Refresh();

            Invalidate(false);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            formClosing = true;

            I_GameHandlerEndFunc("OnFormClosed", false);

            User32Util.ShowTaskBar();

            webView?.Dispose();

            if (!restartRequired)
            {
                Process.GetCurrentProcess().Kill();
            }
        }

        private void I_GameHandlerEndFunc(string msg, bool stopButton)
        {
            try
            {
                if (I_GameHandler != null)
                {
                    Log($"{msg}, calling Handler End function");
                    I_GameHandler.End(stopButton);
                }
            }
            catch { }
        }

        private void Btn_Play_Click(object sender, EventArgs e)
        {
            if (btn_Play.Text == "S T O P")
            {
                I_GameHandlerEndFunc("Stop button clicked", true);
                GameProfile.Instance.Reset();
                DevicesFunctions.gamepadTimer = new System.Threading.Timer(DevicesFunctions.GamepadTimer_Tick, null, 0, 500);
                return;
            }
          
            DevicesFunctions.gamepadTimer.Dispose();

            currentStep?.Ended();

            btn_Play.Text = "S T O P";

            btn_Prev.Enabled = false;

            //reload the handler here so it can be edited/updated until play button get clicked
            gameManager.AddScript(Path.GetFileNameWithoutExtension(currentGame.JsFileName), new bool[] { false, currentGame.UpdateAvailable });

            currentGame = gameManager.GetGame(currentGameInfo.ExePath);
            currentGameInfo.InitializeDefault(currentGame, currentGameInfo.ExePath);

            I_GameHandler = gameManager.MakeHandler(currentGame);
            I_GameHandler.Initialize(currentGameInfo, GameProfile.CleanClone(currentProfile), I_GameHandler);
            I_GameHandler.Ended += Handler_Ended;

            if(bool.Parse(ini.IniReadValue("CustomLayout", "WindowsMerger")) && !GameProfile.Ready)
            {
                string[] mergerRes = ini.IniReadValue("CustomLayout", "WindowsMergerRes").Split('X');
                WindowsMergerThread.StartWindowsMerger(new System.Windows.Size(int.Parse(mergerRes[0]), int.Parse(mergerRes[1])));
            }
            else if (GameProfile.EnableWindowsMerger)
            {
                string[] mergerRes = GameProfile.MergerResolution.Split('X');//GameProfile string "MergerRes" here
                WindowsMergerThread.StartWindowsMerger(new System.Windows.Size(int.Parse(mergerRes[0]), int.Parse(mergerRes[1])));
            }
                
            GameProfile.Game = currentGame;

            if (profileSettings.Visible)
            {
                profileSettings.Visible = false;
            }

            settings.RegHotkeys(this);

            WindowState = FormWindowState.Minimized;

            RefreshUI(true);
        }

        private void Handler_Ended()
        {
            Log("Handler ended method called");

            User32Util.ShowTaskBar();

            if (!formClosing)
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    I_GameHandler = null;
                    currentControl = null;

                    WindowState = FormWindowState.Normal;

                    mainButtonFrame.Focus();
                    btn_Play.Text = "START";
                    btn_Play.Enabled = false;
                    settings.UnRegHotkeys(this);

                    //WindowsMerger.Instance?.Dispose();

                    BringToFront();
                });
            }
        }

        private void Btn_Prev_Click(object sender, EventArgs e)
        {
            currentStepIndex--;

            if (currentStepIndex < 0)
            {
                currentStepIndex = 0;
                return;
            }

            GameProfile.Ready = false;
            GoToStep(currentStepIndex);
        }

        private void BtnSearch_Click(object sender, EventArgs e) => SearchGame.Search(this, null, null);

        private void DetailsToolStripMenuItem_Click(object sender, EventArgs e) => GetGameDetails.GetDetails(menuCurrentGameInfo);

        private void RemoveGameMenuItem_Click(object sender, EventArgs e) => RemoveGame.Remove(this, menuCurrentGameInfo, false);

        private void GameContextMenuStrip_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Control selectedControl = FindControlAtCursor(this);

            if (selectedControl == null)
            {
                return;
            }

            if (selectedControl is Label || selectedControl is PictureBox)
            {
                selectedControl = selectedControl.Parent;
            }

            foreach (Control c in selectedControl?.Controls)
            {
                if (c is Label)
                {
                    if (c.Text == "No games")
                    {
                        gameContextMenuStrip.Items["gameNameMenuItem"].Text = "No game selected...";
                        for (int i = 1; i < gameContextMenuStrip.Items.Count; i++)
                        {
                            gameContextMenuStrip.Items[i].Visible = false;
                        }
                        return;
                    }
                }
            }

            if (selectedControl is GameControl || selectedControl is Button)
            {
                bool isButton = !(selectedControl is GameControl);

                menuCurrentControl = isButton ? currentControl : (GameControl)selectedControl;
                menuCurrentGameInfo = menuCurrentControl.UserGameInfo;
                gameContextMenuStrip.Items["gameNameMenuItem"].Visible = !isButton;
                gameContextMenuStrip.Items["detailsMenuItem"].Visible = !isButton;
                gameContextMenuStrip.Items["notesMenuItem"].Visible = !isButton;
                gameContextMenuStrip.Items["menuSeparator2"].Visible = false;

                gameContextMenuStrip.Items["openUserProfConfigMenuItem"].Visible = false;
                gameContextMenuStrip.Items["deleteUserProfConfigMenuItem"].Visible = false;
                gameContextMenuStrip.Items["openUserProfSaveMenuItem"].Visible = false;
                gameContextMenuStrip.Items["deleteUserProfSaveMenuItem"].Visible = false;
                gameContextMenuStrip.Items["openDocumentConfMenuItem"].Visible = false;
                gameContextMenuStrip.Items["deleteDocumentConfMenuItem"].Visible = false;
                gameContextMenuStrip.Items["openDocumentSaveMenuItem"].Visible = false;
                gameContextMenuStrip.Items["deleteDocumentSaveMenuItem"].Visible = false;
                gameContextMenuStrip.Items["deleteContentFolderMenuItem"].Visible = false;
                gameContextMenuStrip.Items["openBackupFolderMenuItem"].Visible = false;
                gameContextMenuStrip.Items["deleteBackupFolderMenuItem"].Visible = false;

                gameContextMenuStrip.Items["gameNameMenuItem"].ForeColor = Color.DodgerBlue;
                gameContextMenuStrip.Items["gameNameMenuItem"].ImageAlign = ContentAlignment.MiddleCenter;
                gameContextMenuStrip.Items["gameNameMenuItem"].ImageScaling = ToolStripItemImageScaling.SizeToFit;
                gameContextMenuStrip.Items["gameNameMenuItem"].Image = menuCurrentGameInfo.Icon;

                if (string.IsNullOrEmpty(menuCurrentGameInfo?.GameGuid) || menuCurrentGameInfo == null)
                {
                    gameContextMenuStrip.Items["gameNameMenuItem"].Text = "No game selected...";
                    for (int i = 1; i < gameContextMenuStrip.Items.Count; i++)
                    {
                        gameContextMenuStrip.Items[i].Visible = false;
                    }
                }
                else
                {
                    gameContextMenuStrip.Items["gameNameMenuItem"].Text = menuCurrentGameInfo.Game.GameName;

                    bool userConfigPathExists = false;
                    bool userSavePathExists = false;
                    bool docConfigPathExists = false;
                    bool docSavePathExists = false;
                    bool backupFolderExist = false;
                    //bool userConfigPathConverted = false;
                    if (menuCurrentGameInfo.Game.UserProfileConfigPath?.Length > 0 && menuCurrentGameInfo.Game.UserProfileConfigPath.ToLower().StartsWith(@"documents\"))
                    {
                        menuCurrentGameInfo.Game.DocumentsConfigPath = menuCurrentGameInfo.Game.UserProfileConfigPath.Substring(10);
                        menuCurrentGameInfo.Game.UserProfileConfigPath = null;
                        menuCurrentGameInfo.Game.DocumentsConfigPathNoCopy = menuCurrentGameInfo.Game.UserProfileConfigPathNoCopy;
                        menuCurrentGameInfo.Game.ForceDocumentsConfigCopy = menuCurrentGameInfo.Game.ForceUserProfileConfigCopy;
                        //userConfigPathConverted = true;
                    }

                    //bool userSavePathConverted = false;
                    if (menuCurrentGameInfo.Game.UserProfileSavePath?.Length > 0 && menuCurrentGameInfo.Game.UserProfileSavePath.ToLower().StartsWith(@"documents\"))
                    {
                        menuCurrentGameInfo.Game.DocumentsSavePath = menuCurrentGameInfo.Game.UserProfileSavePath.Substring(10);
                        menuCurrentGameInfo.Game.UserProfileSavePath = null;
                        menuCurrentGameInfo.Game.DocumentsSavePathNoCopy = menuCurrentGameInfo.Game.UserProfileSavePathNoCopy;
                        menuCurrentGameInfo.Game.ForceDocumentsSaveCopy = menuCurrentGameInfo.Game.ForceUserProfileSaveCopy;
                        //userSavePathConverted = true;
                    }

                    for (int i = 1; i < gameContextMenuStrip.Items.Count; i++)
                    {
                        gameContextMenuStrip.Items[i].Visible = true;

                        if (string.IsNullOrEmpty(menuCurrentGameInfo.Game.UserProfileConfigPath) && string.IsNullOrEmpty(menuCurrentGameInfo.Game.UserProfileSavePath) && string.IsNullOrEmpty(menuCurrentGameInfo.Game.DocumentsConfigPath) && string.IsNullOrEmpty(menuCurrentGameInfo.Game.DocumentsSavePath))
                        {
                            if (i == gameContextMenuStrip.Items.IndexOf(menuSeparator2))
                            {
                                gameContextMenuStrip.Items["menuSeparator2"].Visible = false;
                            }
                        }
                        else if (i == gameContextMenuStrip.Items.IndexOf(notesMenuItem))
                        {
                            profilePaths.Clear();
                            profilePaths.Add(Environment.GetEnvironmentVariable("userprofile"));
                            profilePaths.Add(DocumentsRoot);

                            if (menuCurrentGameInfo.Game.UseNucleusEnvironment)
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

                        if (i == gameContextMenuStrip.Items.IndexOf(openUserProfConfigMenuItem))
                        {
                            (gameContextMenuStrip.Items["openUserProfConfigMenuItem"] as ToolStripMenuItem).DropDownItems.Clear();
                            (gameContextMenuStrip.Items["deleteUserProfConfigMenuItem"] as ToolStripMenuItem).DropDownItems.Clear();

                            if (menuCurrentGameInfo.Game.UserProfileConfigPath?.Length > 0)
                            {
                                if (profilePaths.Count > 0)
                                {
                                    try
                                    {
                                        foreach (string profilePath in profilePaths)
                                        {
                                            string currPath = Path.Combine(profilePath, menuCurrentGameInfo.Game.UserProfileConfigPath);
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

                                                (gameContextMenuStrip.Items["openUserProfConfigMenuItem"] as ToolStripMenuItem).DropDownItems.Add(nucPrefix + Path.GetFileName(profilePath.TrimEnd('\\')), null, new EventHandler(UserProfileOpenSubmenuItem_Click));
                                                (gameContextMenuStrip.Items["deleteUserProfConfigMenuItem"] as ToolStripMenuItem).DropDownItems.Add(nucPrefix + Path.GetFileName(profilePath.TrimEnd('\\')), null, new EventHandler(UserProfileDeleteSubmenuItem_Click));
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }
                            }
                        }

                        if (!userConfigPathExists)
                        {
                            gameContextMenuStrip.Items["openUserProfConfigMenuItem"].Visible = false;
                            gameContextMenuStrip.Items["deleteUserProfConfigMenuItem"].Visible = false;
                        }

                        if (i == gameContextMenuStrip.Items.IndexOf(openUserProfSaveMenuItem))
                        {
                            (gameContextMenuStrip.Items["openUserProfSaveMenuItem"] as ToolStripMenuItem).DropDownItems.Clear();
                            (gameContextMenuStrip.Items["deleteUserProfSaveMenuItem"] as ToolStripMenuItem).DropDownItems.Clear();

                            if (menuCurrentGameInfo.Game.UserProfileSavePath?.Length > 0)
                            {
                                if (profilePaths.Count > 0)
                                {
                                    try
                                    {
                                        foreach (string profilePath in profilePaths)
                                        {
                                            string currPath = Path.Combine(profilePath, menuCurrentGameInfo.Game.UserProfileSavePath);
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

                                                (gameContextMenuStrip.Items["openUserProfSaveMenuItem"] as ToolStripMenuItem).DropDownItems.Add(nucPrefix + Path.GetFileName(profilePath.TrimEnd('\\')), null, new EventHandler(UserProfileOpenSubmenuItem_Click));
                                                (gameContextMenuStrip.Items["deleteUserProfSaveMenuItem"] as ToolStripMenuItem).DropDownItems.Add(nucPrefix + Path.GetFileName(profilePath.TrimEnd('\\')), null, new EventHandler(UserProfileDeleteSubmenuItem_Click));
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }
                            }
                        }

                        if (!userSavePathExists)
                        {
                            gameContextMenuStrip.Items["openUserProfSaveMenuItem"].Visible = false;
                            gameContextMenuStrip.Items["deleteUserProfSaveMenuItem"].Visible = false;
                        }

                        if (i == gameContextMenuStrip.Items.IndexOf(openDocumentConfMenuItem))
                        {
                            (gameContextMenuStrip.Items["openDocumentConfMenuItem"] as ToolStripMenuItem).DropDownItems.Clear();
                            (gameContextMenuStrip.Items["deleteDocumentConfMenuItem"] as ToolStripMenuItem).DropDownItems.Clear();

                            if (menuCurrentGameInfo.Game.DocumentsConfigPath?.Length > 0)
                            {
                                if (profilePaths.Count > 0)
                                {
                                    try
                                    {
                                        foreach (string profilePath in profilePaths)
                                        {
                                            string currPath = Path.Combine(profilePath, menuCurrentGameInfo.Game.DocumentsConfigPath);
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

                                                (gameContextMenuStrip.Items["openDocumentConfMenuItem"] as ToolStripMenuItem).DropDownItems.Add(nucPrefix + Directory.GetParent(profilePath).Name, null, new EventHandler(DocOpenSubmenuItem_Click));
                                                (gameContextMenuStrip.Items["deleteDocumentConfMenuItem"] as ToolStripMenuItem).DropDownItems.Add(nucPrefix + Directory.GetParent(profilePath).Name, null, new EventHandler(DocDeleteSubmenuItem_Click));
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }
                            }
                        }

                        if (!docConfigPathExists)
                        {
                            gameContextMenuStrip.Items["openDocumentConfMenuItem"].Visible = false;
                            gameContextMenuStrip.Items["deleteDocumentConfMenuItem"].Visible = false;
                        }

                        if (i == gameContextMenuStrip.Items.IndexOf(openDocumentSaveMenuItem))
                        {
                            (gameContextMenuStrip.Items["openDocumentSaveMenuItem"] as ToolStripMenuItem).DropDownItems.Clear();
                            (gameContextMenuStrip.Items["deleteDocumentSaveMenuItem"] as ToolStripMenuItem).DropDownItems.Clear();

                            if (menuCurrentGameInfo.Game.DocumentsSavePath?.Length > 0)
                            {
                                if (profilePaths.Count > 0)
                                {
                                    try
                                    {
                                        foreach (string profilePath in profilePaths)
                                        {
                                            string currPath = Path.Combine(profilePath, menuCurrentGameInfo.Game.DocumentsSavePath);
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

                                                (gameContextMenuStrip.Items["openDocumentSaveMenuItem"] as ToolStripMenuItem).DropDownItems.Add(nucPrefix + Directory.GetParent(profilePath).Name, null, new EventHandler(DocOpenSubmenuItem_Click));
                                                (gameContextMenuStrip.Items["deleteDocumentSaveMenuItem"] as ToolStripMenuItem).DropDownItems.Add(nucPrefix + Directory.GetParent(profilePath).Name, null, new EventHandler(DocDeleteSubmenuItem_Click));
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }
                            }

                            if (!userConfigPathExists && !userSavePathExists && !docConfigPathExists && !docSavePathExists)
                            {
                                gameContextMenuStrip.Items["menuSeparator2"].Visible = false;
                            }
                        }

                        if (!docSavePathExists)
                        {
                            gameContextMenuStrip.Items["openDocumentSaveMenuItem"].Visible = false;
                            gameContextMenuStrip.Items["deleteDocumentSaveMenuItem"].Visible = false;
                        }

                        if (i == gameContextMenuStrip.Items.IndexOf(openBackupFolderMenuItem))
                        {
                            (gameContextMenuStrip.Items["openBackupFolderMenuItem"] as ToolStripMenuItem).DropDownItems.Clear();
                            (gameContextMenuStrip.Items["deleteBackupFolderMenuItem"] as ToolStripMenuItem).DropDownItems.Clear();

                            string backupsPath = $"{NucleusEnvironmentRoot}\\_Game Files Backup_\\NucleusCoop\\{menuCurrentGameInfo.Game.GUID}";

                            if (Directory.Exists(backupsPath))
                            {
                                string[] playersBackup = Directory.GetDirectories(backupsPath, "*", SearchOption.TopDirectoryOnly);

                                foreach (string playerBackup in playersBackup)
                                {
                                    string path = playerBackup;
                                    string playerName = playerBackup.Split('\\').Last();
                                    (gameContextMenuStrip.Items["openBackupFolderMenuItem"] as ToolStripMenuItem).DropDownItems.Add(playerName, null, new EventHandler(OpenBackupFolderSubmenuItem_Click));
                                    (gameContextMenuStrip.Items["deleteBackupFolderMenuItem"] as ToolStripMenuItem).DropDownItems.Add(playerName, null, new EventHandler(DeleteBackupFolderSubmenuItem_Click));
                                }

                                backupFolderExist = true;
                            }
                        }

                        if (!backupFolderExist)
                        {
                            gameContextMenuStrip.Items["openBackupFolderMenuItem"].Visible = false;
                            gameContextMenuStrip.Items["deleteBackupFolderMenuItem"].Visible = false;
                        }

                        if (i == gameContextMenuStrip.Items.IndexOf(notesMenuItem) && menuCurrentGameInfo.Game.Description == null)
                        {
                            gameContextMenuStrip.Items["notesMenuItem"].Visible = false;
                            if (isButton)
                            {
                                gameContextMenuStrip.Items["detailsMenuItem"].Visible = false;
                                i++;
                            }
                        }

                        if (i == gameContextMenuStrip.Items.IndexOf(keepInstancesFolderMenuItem))
                        {
                            if (!menuCurrentGameInfo.Game.KeepSymLinkOnExit)
                            {
                                if (menuCurrentGameInfo.Game.MetaInfo.KeepSymLink)
                                {
                                    gameContextMenuStrip.Items["keepInstancesFolderMenuItem"].Image = ImageCache.GetImage(theme + "locked.png");
                                }
                                else
                                {
                                    gameContextMenuStrip.Items["keepInstancesFolderMenuItem"].Image = ImageCache.GetImage(theme + "unlocked.png");
                                }
                            }
                            else
                            {
                                gameContextMenuStrip.Items["keepInstancesFolderMenuItem"].Visible = false;
                            }
                        }

                        if (i == gameContextMenuStrip.Items.IndexOf(disableProfilesMenuItem))
                        {
                            if (!DisableGameProfiles)
                            {
                                if (menuCurrentGameInfo.Game.MetaInfo.DisableProfiles)
                                {
                                    gameContextMenuStrip.Items["disableProfilesMenuItem"].Image = ImageCache.GetImage(theme + "locked.png");
                                }
                                else
                                {
                                    gameContextMenuStrip.Items["disableProfilesMenuItem"].Image = ImageCache.GetImage(theme + "unlocked.png");
                                }
                            }
                            else
                            {
                                gameContextMenuStrip.Items["disableProfilesMenuItem"].Visible = false;
                            }
                        }
                        if (i == gameContextMenuStrip.Items.IndexOf(gameAssetsMenuItem))
                        {
                            if (gameAssetsMenuItem.DropDownItems.Count > 0)
                            {
                                int visibleCount = 0;

                                for (int d = 0; d < gameAssetsMenuItem.DropDownItems.Count; d++)
                                {
                                    ToolStripItem subItem = gameAssetsMenuItem.DropDownItems[d];

                                    if (subItem == screenshotsMenuItem)
                                    {
                                        bool showItem = Directory.Exists(Path.Combine(Application.StartupPath, $"gui\\screenshots\\{menuCurrentControl.UserGameInfo.GameGuid}"));
                                        subItem.Visible = showItem;
                                        if (showItem)
                                            visibleCount++;
                                    }

                                    if (subItem == coverMenuItem)
                                    {
                                        bool showItem = File.Exists(Path.Combine(Application.StartupPath, $"gui\\covers\\{menuCurrentControl.UserGameInfo.GameGuid}.jpeg"));
                                        subItem.Visible = showItem;
                                        if (showItem)
                                            visibleCount++;
                                    }
                                }

                                gameContextMenuStrip.Items[i].Visible = visibleCount > 0;
                            }
                        }

                        if (i == gameContextMenuStrip.Items.IndexOf(disableHandlerUpdateMenuItem))
                        {
                            gameContextMenuStrip.Items["disableHandlerUpdateMenuItem"].ImageAlign = ContentAlignment.MiddleCenter;
                            gameContextMenuStrip.Items["disableHandlerUpdateMenuItem"].ImageScaling = ToolStripItemImageScaling.SizeToFit;

                            if (menuCurrentGameInfo.Game.MetaInfo.CheckUpdate)
                            {
                                gameContextMenuStrip.Items["disableHandlerUpdateMenuItem"].Image = ImageCache.GetImage(theme + "unlocked.png");
                            }
                            else
                            {
                                gameContextMenuStrip.Items["disableHandlerUpdateMenuItem"].Image = ImageCache.GetImage(theme + "locked.png");                              
                            }
                        }

                        gameContextMenuStrip.Items["gameNameMenuItem"].Visible = (!isButton) || !StepPanel.Visible;
                        gameContextMenuStrip.Items["detailsMenuItem"].Visible = (!isButton && currentGameInfo != menuCurrentGameInfo) || !StepPanel.Visible;
                        gameContextMenuStrip.Items["notesMenuItem"].Visible = (!isButton && currentGameInfo != menuCurrentGameInfo) || !StepPanel.Visible;
                        gameContextMenuStrip.Items["menuSeparator1"].Visible = (!isButton && currentGameInfo != menuCurrentGameInfo) || !StepPanel.Visible;
                    }

                    foreach (ToolStripMenuItem menuItem in gameContextMenuStrip.Items.OfType<ToolStripMenuItem>())
                    {
                        if (menuItem != disableProfilesMenuItem && menuItem != keepInstancesFolderMenuItem && menuItem != gameNameMenuItem && menuItem != disableHandlerUpdateMenuItem)
                        {
                            menuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
                        }

                        if (menuItem.DropDownItems.Count > 0)
                        {
                            menuItem.DropDown.BackgroundImage = gameContextMenuStrip.BackgroundImage;

                            for (int d = 0; d < menuItem.DropDownItems.Count; d++)
                            {
                                menuItem.DropDownItems[d].BackColor = Color.Transparent;
                                menuItem.DropDownItems[d].ForeColor = gameContextMenuStrip.ForeColor;

                                ((ToolStripDropDownMenu)menuItem.DropDown).ShowImageMargin = false;
                            }
                        }
                    }
                }
            }
            else
            {
                gameContextMenuStrip.Items["gameNameMenuItem"].Text = "No game selected...";

                for (int i = 1; i < gameContextMenuStrip.Items.Count; i++)
                {
                    gameContextMenuStrip.Items[i].Visible = false;
                }
            }
        }

        private void GameContextMenuStrip_Opened(object sender, EventArgs e)
        => gameContextMenuStrip.Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(2, 2, gameContextMenuStrip.Width - 1, gameContextMenuStrip.Height, 20, 20));

        private void GameContextMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if (currentControl != null)
                menuCurrentControl = currentControl;
        }

        private void OpenBackupFolderSubmenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            string backupsPath = $"{NucleusEnvironmentRoot}\\NucleusCoop\\_Game Files Backup_\\{menuCurrentGameInfo.Game.GUID}";

            string path = $"{backupsPath}\\{item.Text}";

            if (Directory.Exists(path))
            {
                Process.Start(path);
            }
        }

        private void DeleteBackupFolderSubmenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            string backupsPath = $"{NucleusEnvironmentRoot}\\NucleusCoop\\_Game Files Backup_\\{menuCurrentGameInfo.Game.GUID}";

            string path = $"{backupsPath}\\{item.Text}";

            DialogResult dialogResult = MessageBox.Show($"Do you really want to delete \"{menuCurrentControl.GameInfo.GUID}\" {item.Text}'s backup folder?", $"Delete {item.Text}'s backup folder.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
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
                pathSuffix = menuCurrentGameInfo.Game.UserProfileConfigPath;
            }
            else
            {
                pathSuffix = menuCurrentGameInfo.Game.UserProfileSavePath;
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
                pathSuffix = menuCurrentGameInfo.Game.UserProfileConfigPath;
            }
            else
            {
                pathSuffix = menuCurrentGameInfo.Game.UserProfileSavePath;
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
                pathSuffix = menuCurrentGameInfo.Game.DocumentsConfigPath;
            }
            else
            {
                pathSuffix = menuCurrentGameInfo.Game.DocumentsSavePath;
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
                pathSuffix = menuCurrentGameInfo.Game.DocumentsConfigPath;
            }
            else
            {
                pathSuffix = menuCurrentGameInfo.Game.DocumentsSavePath;
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

        private void OpenHandlerMenuItem_Click(object sender, EventArgs e)
        => OpenHandler.OpenRawHandler(menuCurrentGameInfo);

        private void OpenDataFolderMenuItem_Click(object sender, EventArgs e)
        => OpenGameContentFolder.OpenDataFolder(menuCurrentGameInfo);

        private void ChangeIconMenuItem_Click(object sender, EventArgs e)
        => ChangeGameIcon.ChangeIcon(this, menuCurrentGameInfo);

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

        private void NotesMenuItem_Click(object sender, EventArgs e) => NucleusMessageBox.Show("Handler Author's Notes", menuCurrentGameInfo.Game.Description, true);

        private void GameOptions_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            gameContextMenuStrip.Show(ptLowerLeft);
        }

        private void OpenOrigExePathMenuItem_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(menuCurrentGameInfo.ExePath);
            if (Directory.Exists(path))
            {
                Process.Start(path);
            }
            else
            {
                MessageBox.Show("Unable to open original executable path for this game.", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DeleteContentFolderMenuItem_Click(object sender, EventArgs e)
        {
            string path = Path.Combine(gameManager.GetAppContentPath(), menuCurrentGameInfo.Game.GUID);
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

        private void Button_UpdateAvailable_Click(object sender, EventArgs e)
        {
            handler = HubCache.SearchById(currentGameInfo.Game.HandlerId);

            if (handler == null)
            {
                button_UpdateAvailable.Visible = false;
                MessageBox.Show("Error fetching update information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are sure you want to update this handler?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    btn_Play.Enabled = false;
                    btn_Next.Enabled = false;

                    downloadPrompt = new DownloadPrompt(handler, this, null, true);
                    downloadPrompt.ShowDialog();

                    RefreshUI(false);

                    //Re-select the current game, so we refresh the handler info after updating
                    GameControl con = controls.Where(c => c.Key.GameGuid == currentControl.GameInfo.GUID).FirstOrDefault().Value;
                    List_Games_SelectedChanged(con, null);
                }
            }
        }

        private void Btn_Extract_Click(object sender, EventArgs e)
        => ExtractHandler.Extract(this);

        private void Btn_SplitCalculator_Click(object sender, EventArgs e)
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

        private void Btn_noHub_Click(object sender, EventArgs e)
        => Connected = StartChecks.CheckHubResponse();

        private void Btn_Links_Click(object sender, EventArgs e)
        {
            Button btnSender = (Button)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            socialLinksMenu.Show(ptLowerLeft);
        }

        public void ClickAnyControl(object sender, EventArgs e)
        {
            if (settings.Visible)
            {
                settings.BringToFront();
            }

            if (profileSettings.Visible)
            {
                profileSettings.BringToFront();
            }
           
            if (sender != donationBtn)
            {
                if (donationPanel != null)
                {
                    clientAreaPanel.Controls.Remove(donationPanel);
                    donationPanel.Dispose();
                    donationPanel = null;
                }
            }
        }

        private void Logo_Click(object sender, EventArgs e) => Process.Start("https://github.com/SplitScreen-Me/splitscreenme-nucleus/releases");

        private void Link_faq_Click(object sender, EventArgs e) => Process.Start(faq_link);

        private void ScriptAuthorTxt_LinkClicked(object sender, LinkClickedEventArgs e) => Process.Start(e.LinkText);

        private void CloseBtn_MouseEnter(object sender, EventArgs e) => closeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_close_mousehover.png");

        private void CloseBtn_MouseLeave(object sender, EventArgs e) => closeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_close.png");

        private void MaximizeBtn_MouseEnter(object sender, EventArgs e) => maximizeBtn.BackgroundImage = maximizeBtn.BackgroundImage = WindowState == FormWindowState.Maximized ? ImageCache.GetImage(theme + "title_windowed_mousehover.png") : ImageCache.GetImage(theme + "title_maximize_mousehover.png");

        private void MaximizeBtn_MouseLeave(object sender, EventArgs e) => maximizeBtn.BackgroundImage = WindowState == FormWindowState.Maximized ? ImageCache.GetImage(theme + "title_windowed.png") : ImageCache.GetImage(theme + "title_maximize.png");

        private void MinimizeBtn_MouseLeave(object sender, EventArgs e) => minimizeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_minimize.png");

        private void MinimizeBtn_MouseEnter(object sender, EventArgs e) => minimizeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_minimize_mousehover.png");

        private void Btn_settings_MouseEnter(object sender, EventArgs e) { if (profileSettings.Visible) { return; } btn_settings.BackgroundImage = ImageCache.GetImage(theme + "title_settings_mousehover.png"); }

        private void Btn_settings_MouseLeave(object sender, EventArgs e) => btn_settings.BackgroundImage = ImageCache.GetImage(theme + "title_settings.png");

        private void Btn_downloadAssets_MouseEnter(object sender, EventArgs e) => btn_downloadAssets.BackgroundImage = ImageCache.GetImage(theme + "title_download_assets_mousehover.png");

        private void Btn_downloadAssets_MouseLeave(object sender, EventArgs e) => btn_downloadAssets.BackgroundImage = ImageCache.GetImage(theme + "title_download_assets.png");

        private void Btn_magnifier_Click(object sender, EventArgs e)
        {
            if (!handlerNotesZoom.Visible)
            {
                if (currentGameInfo.Game.MetaInfo.FirstLaunch)
                {
                    handlerNotesZoom.warning.Visible = true;
                    handlerNotesZoom.warning.Text = "⚠ Important! Launch the game out of Nucleus before launching the handler for the first time.  ⚠";
                    handlerNotesZoom.Notes.Text = scriptAuthorTxt.Text;
                }
                else
                {
                    handlerNotesZoom.Notes.Text = scriptAuthorTxt.Text;
                    handlerNotesZoom.warning.Visible = false;
                }

                handlerNotesZoom.Visible = true;
                handlerNotesZoom.BringToFront();
            }
            else
            {
                handlerNotesZoom.Visible = false;
            }
        }

        private void SettingsBtn_Click(object sender, EventArgs e)
        {
            if (GenericGameHandler.Instance != null)
            {
                if (!GenericGameHandler.Instance.HasEnded)
                {
                    return;
                }
            }

            if (profileSettings.Visible || settings == null)
            {
                return;
            }

            if (!settings.Visible)
            {
                settings.Visible = true;
                settings.BringToFront();
            }
            else
            {
                settings.BringToFront();
            }
        }

        private void ProfilesList_btn_Click(object sender, EventArgs e)
        {
            if (settings.Visible)
            {
                return;
            }

            if (GameProfile.profilesPathList.Count == 0)
            {
                setupScreen.ProfilesList.Visible = false;
                return;
            }

            setupScreen.ProfilesList.Visible = !setupScreen.ProfilesList.Visible;
        }

        public void ProfileSettings_btn_Click(object sender, EventArgs e)
        {
            if (settings.Visible)
            {
                return;
            }

            if (!profileSettings.Visible)
            {
                ProfilesList.Instance.Locked = true;
                ProfileSettings.UpdateProfileSettingsUiValues();
                profileSettings.Visible = true;
                profileSettings.BringToFront();
            }
            else
            {
                profileSettings.BringToFront();
            }
        }

        private void MinimizeButtonClick(object sender, EventArgs e)
        => WindowState = FormWindowState.Minimized;

        private void MaximizeButtonClick(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
            maximizeBtn.BackgroundImage = WindowState == FormWindowState.Maximized ? ImageCache.GetImage(theme + "title_windowed.png") : ImageCache.GetImage(theme + "title_maximize.png");
        }

        private void MainForm_ClientSizeChanged(object sender, EventArgs e)
        {
            Invalidate(false);

            if (roundedCorners)
            {
                if (WindowState == FormWindowState.Maximized)
                {
                    Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, Width, Height, 0, 0));
                    clientAreaPanel.Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, clientAreaPanel.Width, clientAreaPanel.Height, 0, 0));
                }
                else
                {
                    Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
                    clientAreaPanel.Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, clientAreaPanel.Width, clientAreaPanel.Height, 20, 20));
                }
            }

            maximizeBtn.BackgroundImage = maximizeBtn.BackgroundImage = WindowState == FormWindowState.Maximized ? ImageCache.GetImage(theme + "title_windowed.png") : ImageCache.GetImage(theme + "title_maximize.png");

            if (setupScreen != null && I_GameHandler == null)
            {
                GameProfile.Instance?.Reset();

                if (stepsList != null)
                {
                    GoToStep(0);
                }

                if (ProfilesList.Instance != null)
                {
                    ProfilesList.Instance.Locked = false;
                }
            }
        }

        private void MainForm_ResizeBegin(object sender, EventArgs e)
        {
            clientAreaPanel.Visible = false;
            Opacity = 0.6D;
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            clientAreaPanel.Visible = true;

            game_listSizer.Refresh();
            StepPanel.Refresh();
            mainButtonFrame.Refresh();
            rightFrame.Refresh();
            stepPanelPictureBox.Refresh();
            Opacity = 1.0D;
            Refresh();
        }

        private void CloseButtonClick(object sender, EventArgs e)
        => FadeOut();

        private void KeepInstancesFolderMenuItem_Click(object sender, EventArgs e)
        {
            if (menuCurrentGameInfo.Game.MetaInfo.KeepSymLink)
            {
                menuCurrentGameInfo.Game.MetaInfo.KeepSymLink = false;
                gameContextMenuStrip.Items["deleteContentFolderMenuItem"].Image = ImageCache.GetImage(theme + "unlocked.png");
            }
            else
            {
                menuCurrentGameInfo.Game.MetaInfo.KeepSymLink = true;
                gameContextMenuStrip.Items["deleteContentFolderMenuItem"].Image = ImageCache.GetImage(theme + "locked.png");
               
            }

            GameManager.Instance.SaveUserProfile();
        }

        private void SaveNucleusWindowPosAndLoc()
        {
            if (Location.X == -32000 || Width == 0)
            {
                return;
            }

            ini.IniWriteValue("Misc", "WindowSize", Width + "X" + Height);
            ini.IniWriteValue("Misc", "WindowLocation", Location.X + "X" + Location.Y);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) => SaveNucleusWindowPosAndLoc();

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        private void MainButtonFrame_MouseDown(object sender, MouseEventArgs e)
        {
            ClickAnyControl(mainButtonFrame, null);

            if (e.Button == MouseButtons.Left)
            {
                User32Interop.ReleaseCapture();
                IntPtr nucHwnd = User32Interop.FindWindow(null, Text);
                User32Interop.SendMessage(nucHwnd, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, (IntPtr)0);
            }

            if (settings.Visible)
            {
                settings.BringToFront();
            }

            if (profileSettings.Visible)
            {
                profileSettings.BringToFront();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            int edgingHeight = clientAreaPanel.Location.Y;
            Rectangle topGradient = new Rectangle(0, 0, Width, edgingHeight);
            Rectangle bottomGradient = new Rectangle(0, Height - edgingHeight, Width, edgingHeight);

            Color edgingColorTop;
            Color edgingColorBottom;

            if (!mainButtonFrame.Enabled)
            {
                edgingColorTop = Color.Red;
                edgingColorBottom = Color.Red;
            }
            else if (webView != null)
            {
                edgingColorTop = Color.FromArgb(255, 0, 98, 190);
                edgingColorBottom = Color.FromArgb(255, 0, 98, 190);
            }
            else if (StepPanel.Visible)
            {
                edgingColorTop = GameBorderGradientTop;
                edgingColorBottom = GameBorderGradientBottom;
            }
            else
            {
                edgingColorTop = BorderGradient;
                edgingColorBottom = BorderGradient;
            }

            LinearGradientBrush topLinearGradientBrush = new LinearGradientBrush(topGradient, Color.Transparent, edgingColorTop, 0F);
            LinearGradientBrush bottomLinearGradientBrush = new LinearGradientBrush(bottomGradient, Color.Transparent, edgingColorBottom, 0F);

            ColorBlend topcblend = new ColorBlend(3);
            topcblend.Colors = new Color[3] { Color.Transparent, edgingColorTop, Color.Transparent };
            topcblend.Positions = new float[3] { 0f, 0.5f, 1f };

            topLinearGradientBrush.InterpolationColors = topcblend;

            ColorBlend bottomcblend = new ColorBlend(3);
            bottomcblend.Colors = new Color[3] { Color.Transparent, edgingColorBottom, Color.Transparent };
            bottomcblend.Positions = new float[3] { 0f, 0.5f, 1f };

            bottomLinearGradientBrush.InterpolationColors = bottomcblend;

            Rectangle fill = new Rectangle(0, 0, Width, Height);

            e.Graphics.FillRectangle(borderBrush, fill);
            e.Graphics.FillRectangle(topLinearGradientBrush, topGradient);
            e.Graphics.FillRectangle(bottomLinearGradientBrush, bottomGradient);

            topLinearGradientBrush.Dispose();
            bottomLinearGradientBrush.Dispose();
        }

        private void Btn_debuglog_Click(object sender, EventArgs e)
        {
            if (File.Exists(Path.Combine(Application.StartupPath, "debug-log.txt")))
            {
                OpenDebugLog.OpenDebugLogFile();
                return;
            }

            TriggerOSD(2000, "No Available Log");
        }

        private void Btn_Extract_MouseEnter(object sender, EventArgs e) => btn_Extract.BackgroundImage = ImageCache.GetImage(theme + "extract_nc_mousehover.png");

        private void Btn_Extract_MouseLeave(object sender, EventArgs e) => btn_Extract.BackgroundImage = ImageCache.GetImage(theme + "extract_nc.png");

        private void BtnSearch_MouseEnter(object sender, EventArgs e) => btnSearch.BackgroundImage = ImageCache.GetImage(theme + "search_game_mousehover.png");

        private void BtnSearch_MouseLeave(object sender, EventArgs e) => btnSearch.BackgroundImage = ImageCache.GetImage(theme + "search_game.png");

        private void Btn_debuglog_MouseEnter(object sender, EventArgs e) => btn_debuglog.BackgroundImage = ImageCache.GetImage(theme + "log_mousehover.png");

        private void Btn_debuglog_MouseLeave(object sender, EventArgs e) => btn_debuglog.BackgroundImage = ImageCache.GetImage(theme + "log.png");

        private void DonationBtn_Click(object sender, EventArgs e)
        {
            if (donationPanel == null)
            {
                donationPanel = new DonationPanel();
                donationPanel.Visible = false;
                clientAreaPanel.Controls.Add(donationPanel);
                donationPanel.Location = new Point(donationBtn.Left - donationPanel.Width, donationBtn.Bottom + 5);
                donationPanel.BringToFront();
                donationPanel.Visible = true;
            }
            else
            {
                clientAreaPanel.Controls.Remove(donationPanel);
                donationPanel.Dispose();
                donationPanel = null;
            }
        }

        private void MainForm_Deactivate(object sender, EventArgs e)
        {
            txt_version.Focus();
        }

        private void DisableProfilesMenuItem_Click(object sender, EventArgs e)
        {
            setupButtonsPanel.Visible = false;
            
            if (menuCurrentGameInfo.Game.MetaInfo.DisableProfiles)
            {
                menuCurrentGameInfo.Game.MetaInfo.DisableProfiles = false;
                gameContextMenuStrip.Items["disableProfilesMenuItem"].Image = ImageCache.GetImage(theme + "locked.png");

                if (menuCurrentGameInfo == currentGameInfo && stepButtonsPanel.Visible)
                {
                    GameProfile.Instance.InitializeDefault(currentControl.GameInfo, setupScreen);
                    ProfilesList.Instance.Update_ProfilesList();
                    bool showList = GameProfile.profilesPathList.Count > 0;

                    if (!menuCurrentGameInfo.Game.MetaInfo.FirstLaunch)
                    {
                        setupScreen.ProfilesList.Visible = showList;
                    }

                    profilesList_btn.Visible = showList;
                    profileSettings_btn.Visible = true;
                    nearestVisible = showList ? profilesList_btn : profileSettings_btn;
                    button_UpdateAvailable.Location = showList ? new Point((profilesList_btn.Left - button_UpdateAvailable.Width) - (button_UpdateAvailable.Margin.Right + nearestVisible.Margin.Left), button_UpdateAvailable.Location.Y) : new Point((profileSettings_btn.Left - button_UpdateAvailable.Width) - (button_UpdateAvailable.Margin.Right + nearestVisible.Margin.Left), button_UpdateAvailable.Location.Y);
              
                    CustomToolTips.SetToolTip(profilesList_btn, $"{currentGameInfo.Game.GameName} profiles list.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });

                    if (stepsList != null)
                    {
                        GoToStep(0);
                    }
                }
            }
            else
            {
                menuCurrentGameInfo.Game.MetaInfo.DisableProfiles = true;
                gameContextMenuStrip.Items["disableProfilesMenuItem"].Image = ImageCache.GetImage(theme + "unlocked.png");
                               
                if (menuCurrentGameInfo == currentGameInfo && stepButtonsPanel.Visible)
                {
                    GameProfile.Instance.InitializeDefault(currentControl.GameInfo, setupScreen);
                    setupScreen.ProfilesList.Visible = false;
                    profilesList_btn.Visible = false;
                    profileSettings_btn.Visible = false;
                    button_UpdateAvailable.Location = new Point((btn_gameOptions.Left - button_UpdateAvailable.Width) - (button_UpdateAvailable.Margin.Right + nearestVisible.Margin.Left), button_UpdateAvailable.Location.Y);
                }

                if (stepsList != null)
                {
                    GoToStep(0);
                }
            }

            if(stepButtonsPanel.Visible)
            {
                saveProfileRadioBtn.Location = button_UpdateAvailable.Visible ? new Point((button_UpdateAvailable.Left - saveProfileRadioBtn.Width) - 5, saveProfileRadioBtn.Location.Y) : new Point((nearestVisible.Left - saveProfileRadioBtn.Width) - 5, saveProfileRadioBtn.Location.Y);
                setupButtonsPanel.Visible = true;
            }
                            
            GameManager.Instance.SaveUserProfile();
        }

        private void FAQToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start(faq_link);

        private void RedditToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start("https://www.reddit.com/r/nucleuscoop/");

        private void DiscordToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start("https://discord.com/invite/QDUt8HpCvr");

        private void SocialLinksMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            socialLinksMenu.BackColor = MenuStripBackColor;
            socialLinksMenu.ForeColor = MenuStripFontColor;

            btn_Links.BackgroundImage = ImageCache.GetImage(theme + "title_dropdown_opened.png");

            for (int i = 0; i < socialLinksMenu.Items.Count; i++)
            {
                if (socialLinksMenu.Items[i].GetType() == typeof(ToolStripSeparator))
                {
                    continue;
                }

                ToolStripMenuItem item = socialLinksMenu.Items[i] as ToolStripMenuItem;

                item.ImageScaling = ToolStripItemImageScaling.None;
                item.ImageAlign = ContentAlignment.MiddleLeft;

                if (item == fAQMenuItem)
                {
                    item.ForeColor = Color.Aqua;
                    item.ToolTipText = "Nucleus Co-op FAQ.";
                }

                if (item == redditMenuItem)
                {
                    item.ForeColor = Color.Aqua;
                    item.ToolTipText = "Official Nucleus Co-op Subreddit.";
                }

                if (item == discordMenuItem)
                {
                    item.ForeColor = Color.Aqua;
                    item.ToolTipText = "Join the official Nucleus Co-op discord server.";
                }

                if (item == splitCalculatorMenuItem)
                {
                    item.ToolTipText = "This program can estimate the system requirements needed to run a game in split-screen.";
                }

                if (item == thirdPartyToolsToolStripMenuItem)
                {
                    item.DropDown.BackgroundImage = socialLinksMenu.BackgroundImage;

                    if (item.DropDownItems.Count > 0)
                    {
                        for (int d = 0; d < item.DropDownItems.Count; d++)
                        {
                            ToolStripItem subItem = item.DropDownItems[d];

                            subItem.BackColor = Color.Transparent;
                            subItem.ForeColor = Color.Aqua;

                            ((ToolStripDropDownMenu)item.DropDown).ShowImageMargin = false;

                            if (subItem == xOutputToolStripMenuItem)
                            {
                                subItem.ToolTipText = "XOutput is a software that can convert DirectInput into XInput.";
                            }

                            if (subItem == dS4WindowsToolStripMenuItem)
                            {
                                subItem.ToolTipText = "Xinput emulator for Ps4 controllers.";
                            }

                            if (subItem == hidHideToolStripMenuItem)
                            {
                                subItem.ToolTipText = "With HidHide it is possible to deny a specific application access to one or more human interface devices.";
                            }

                            if (subItem == scpToolkitToolStripMenuItem)
                            {
                                subItem.ToolTipText = "Xinput emulator for Ps3 controllers.";
                            }
                        }
                    }
                }
            }
        }

        private void SocialLinksMenu_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            btn_Links.BackgroundImage = ImageCache.GetImage(theme + "title_dropdown_closed.png");
        }

        private void SplitCalculatorToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void XOutputToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start("https://github.com/csutorasa/XOutput/releases");

        private void DS4WindowsToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start("https://github.com/Ryochan7/DS4Windows/releases");

        private void HidHideToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start("https://github.com/ViGEm/HidHide/releases");

        private void ScpToolkitToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start("https://github.com/nefarius/ScpToolkit/releases");

        private void SocialLinksMenu_Opened(object sender, EventArgs e)
        {
            socialLinksMenu.Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(2, 2, socialLinksMenu.Width - 1, socialLinksMenu.Height, 20, 20));
        }


        private void ClientAreaPanel_Paint(object sender, PaintEventArgs e)
        {
            if (backGradient[0] == 0)
            {
                return;
            }

            Rectangle gradientBrushbounds = new Rectangle(0, 0, mainButtonFrame.Width, Height);

            Color color1 = Color.FromArgb(230, backGradient[1], backGradient[2], backGradient[3]);
            Color color2 = Color.FromArgb(195, backGradient[1], backGradient[2], backGradient[3]);
            Color color3 = Color.FromArgb(155, backGradient[1], backGradient[2], backGradient[3]);
            Color color4 = Color.FromArgb(130, backGradient[1], backGradient[2], backGradient[3]);
            Color color5 = Color.FromArgb(115, backGradient[1], backGradient[2], backGradient[3]);
            Color color6 = Color.FromArgb(130, backGradient[1], backGradient[2], backGradient[3]);
            Color color7 = Color.FromArgb(155, backGradient[1], backGradient[2], backGradient[3]);
            Color color8 = Color.FromArgb(195, backGradient[1], backGradient[2], backGradient[3]);
            Color color9 = Color.FromArgb(230, backGradient[1], backGradient[2], backGradient[3]);

            LinearGradientBrush ClientAreaPanel_LinearGradientBrush =
                new LinearGradientBrush(gradientBrushbounds, color1, color5, 90f);

            ColorBlend topcblend = new ColorBlend(9);
            topcblend.Colors = new Color[9] { color1, color2, color3, color4, color5, color6, color7, color8, color9 };
            topcblend.Positions = new float[9] { 0f, 0.125f, 0.250f, 0.375f, 0.500f, 0.625f, 0.750f, 0.875f, 1.0f };

            ClientAreaPanel_LinearGradientBrush.InterpolationColors = topcblend;

            e.Graphics.FillRectangle(ClientAreaPanel_LinearGradientBrush, clientAreaPanel.ClientRectangle);
            ClientAreaPanel_LinearGradientBrush.Dispose();
        }

        private void SocialLinksMenu_Opened_1(object sender, EventArgs e) => socialLinksMenu.Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(2, 2, socialLinksMenu.Width - 1, socialLinksMenu.Height, 20, 20));

        private void Game_listSizer_Paint(object sender, PaintEventArgs e)
        {
            if (backGradient[0] == 0)
            {
                return;
            }

            Rectangle gradientBrushbounds = new Rectangle(0, 0, game_listSizer.Width / 2, game_listSizer.Height);

            Color color1 = Color.FromArgb(20, backGradient[1], backGradient[2], backGradient[3]);
            Color color2 = Color.FromArgb(100, backGradient[1], backGradient[2], backGradient[3]);
            Color color3 = Color.FromArgb(100, backGradient[1], backGradient[2], backGradient[3]);
            Color color4 = Color.FromArgb(20, backGradient[1], backGradient[2], backGradient[3]);

            LinearGradientBrush game_listSizer_LinearGradientBrush =
            new LinearGradientBrush(gradientBrushbounds, Color.Transparent, color1, 90f);

            ColorBlend topcblend = new ColorBlend(6);
            topcblend.Colors = new Color[6] { Color.Transparent, color1, color2, color3, color4, Color.Transparent };
            topcblend.Positions = new float[6] { 0f, 0.2f, 0.4f, 0.6f, 0.8f, 1.0f };
            game_listSizer_LinearGradientBrush.InterpolationColors = topcblend;

            e.Graphics.FillRectangle(game_listSizer_LinearGradientBrush, game_listSizer.ClientRectangle);
            game_listSizer_LinearGradientBrush.Dispose();
        }

        private void RightFrame_Paint(object sender, PaintEventArgs e)
        {
            if (backGradient[0] == 0)
            {
                return;
            }

            Rectangle gradientBrushbounds = new Rectangle(0, 0, rightFrame.Width / 2, rightFrame.Height);

            Color color1 = Color.FromArgb(20, backGradient[1], backGradient[2], backGradient[3]);
            Color color2 = Color.FromArgb(100, backGradient[1], backGradient[2], backGradient[3]);
            Color color3 = Color.FromArgb(100, backGradient[1], backGradient[2], backGradient[3]);
            Color color4 = Color.FromArgb(20, backGradient[1], backGradient[2], backGradient[3]);

            LinearGradientBrush rightFrame_LinearGradientBrush =
                new LinearGradientBrush(gradientBrushbounds, Color.Transparent, color1, 90f);

            ColorBlend topcblend = new ColorBlend(6);
            topcblend.Colors = new Color[6] { Color.Transparent, color1, color2, color3, color4, Color.Transparent };
            topcblend.Positions = new float[6] { 0f, 0.2f, 0.4f, 0.6f, 0.8f, 1.0f };

            rightFrame_LinearGradientBrush.InterpolationColors = topcblend;

            e.Graphics.FillRectangle(rightFrame_LinearGradientBrush, rightFrame.ClientRectangle);
            rightFrame_LinearGradientBrush.Dispose();
        }    

        private void StepPanelPictureBox_Paint(object sender, PaintEventArgs e)
        {
            if(enableParticles)
            DrawParticles.Draw(sender, e);
        }

        private void SaveProfileRadioBtn_Click(object sender, EventArgs e)
        {
            CustomRadioButton radio = (CustomRadioButton)sender;
            currentGame.MetaInfo.SaveProfile = radio.RadioChecked;
        }

        private void Instruction_btn_Click(object sender, EventArgs e)
        {
            setupScreen.instructionImg.Visible = !setupScreen.instructionImg.Visible;
            instruction_btn.BackgroundImage = setupScreen.instructionImg.Visible ? 
            instruction_btn.BackgroundImage = ImageCache.GetImage(theme + "instruction_opened.png") :
            instruction_btn.BackgroundImage = ImageCache.GetImage(theme + "instruction_closed.png");
            setupScreen.instructionImg.BringToFront();  
        }

        private void Btn_ZoomIn(object sender, EventArgs e)
        {
            Control con = sender as Control;
            con.Size = new Size(con.Width += 3, con.Height += 3);
            con.Location = new Point(con.Location.X - 1, con.Location.Y - 1);
        }

        private void Btn_ZoomOut(object sender, EventArgs e)
        {
            Control con = sender as Control;
            con.Size = new Size(con.Width -= 3, con.Height -= 3);
            con.Location = new Point(con.Location.X + 1, con.Location.Y + 1);
        }

        private void StepPanel_Paint(object sender, PaintEventArgs e)
        {
            if (GameProfile.Instance == null)
            {
                InputsTextLabel.Text = "";
                return;
            }

            var inputText = InputsText.GetInputText(this);

            InputsTextLabel.Text = inputText.Item1;
            InputsTextLabel.ForeColor = inputText.Item2;
            InputsTextLabel.Location = new Point(InputsTextLabel.Location.X, setupButtonsPanel.Location.Y + ( setupButtonsPanel.Height/2 - InputsTextLabel.Height/2));
        }

        private void CoverMenuItem_Click(object sender, EventArgs e)
        {
            string coverPath = Path.Combine(Application.StartupPath, $"gui\\covers");

            if (File.Exists($"{coverPath}\\{menuCurrentGameInfo.GameGuid}.jpeg"))
            {
                Process.Start($"{coverPath}\\{menuCurrentGameInfo.GameGuid}.jpeg");
                Process.Start(coverPath);         
            }
        }

        private void ScreenshotsMenuItem_Click(object sender, EventArgs e)
        {
            string screenshotsPath = Path.Combine(Application.StartupPath, $"gui\\screenshots\\{menuCurrentGameInfo.GameGuid}");
            if (Directory.Exists(screenshotsPath))
            {
                Process.Start(screenshotsPath);
            }
        }

        private void DisableHandlerUpdateMenuItem_Click(object sender, EventArgs e)
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                this.Invoke((MethodInvoker)delegate ()
                {
                    bool update = false;

                    bool showList = GameProfile.profilesPathList.Count > 0;
                    nearestVisible = showList ? profilesList_btn : profileSettings_btn;

                    if (menuCurrentGameInfo.Game.MetaInfo.CheckUpdate)
                    {
                        menuCurrentGameInfo.Game.MetaInfo.CheckUpdate = false;
                        menuCurrentGameInfo.Game.UpdateAvailable = false;
                        gameContextMenuStrip.Items["disableHandlerUpdateMenuItem"].Image = ImageCache.GetImage(theme + "locked.png");
                    }
                    else
                    {
                        menuCurrentGameInfo.Game.MetaInfo.CheckUpdate = true;
                        update = menuCurrentGameInfo.Game.Hub.CheckUpdateAvailable();
                        menuCurrentGameInfo.Game.UpdateAvailable = update;
                        gameContextMenuStrip.Items["disableHandlerUpdateMenuItem"].Image = ImageCache.GetImage(theme + "unlocked.png");
                    }

                    if (menuCurrentGameInfo == currentGameInfo)
                    {
                        button_UpdateAvailable.Visible = update && menuCurrentGameInfo.Game.MetaInfo.CheckUpdate;
                        button_UpdateAvailable.Location = menuCurrentGameInfo.Game.MetaInfo.DisableProfiles ?
                        new Point((btn_gameOptions.Left - button_UpdateAvailable.Width) - (button_UpdateAvailable.Margin.Right + btn_gameOptions.Margin.Left), button_UpdateAvailable.Location.Y) :
                        new Point((nearestVisible.Left - button_UpdateAvailable.Width) - (button_UpdateAvailable.Margin.Right + nearestVisible.Margin.Left), button_UpdateAvailable.Location.Y);

                        saveProfileRadioBtn.Location = button_UpdateAvailable.Visible ? 
                        new Point((button_UpdateAvailable.Left - saveProfileRadioBtn.Width) - 5, saveProfileRadioBtn.Location.Y) :                      
                        new Point((nearestVisible.Left - saveProfileRadioBtn.Width) - 5, saveProfileRadioBtn.Location.Y);
                    }
                });
            });
        }

    }
}
