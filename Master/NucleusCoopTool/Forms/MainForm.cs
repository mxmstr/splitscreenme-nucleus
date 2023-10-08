using NAudio.Gui;
using Nucleus.Coop.Forms;
using Nucleus.Coop.Tools;
using Nucleus.Gaming;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Controls;
using Nucleus.Gaming.Controls.SetupScreen;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.Generic;
using Nucleus.Gaming.Coop.InputManagement.Gamepads;
using Nucleus.Gaming.Forms.NucleusMessageBox;
using Nucleus.Gaming.Generic.Step;
using Nucleus.Gaming.Platform.PCSpecs;
using Nucleus.Gaming.Tools.GlobalWindowMethods;
using Nucleus.Gaming.Windows;
using Nucleus.Gaming.Windows.Interop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows.Forms;


namespace Nucleus.Coop
{
    /// <summary>
    /// Central UI class to the Nucleus Coop application
    /// </summary>
    public partial class MainForm : BaseForm, IDynamicSized
    {
        public readonly string version = "v" + Globals.Version;
        public readonly IniFile iconsIni;
        public readonly IniFile themeIni = Globals.ThemeIni;
        public readonly string theme = Globals.Theme;

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
        public string[] rgb_TitleBarColor;
        public string[] rgb_HandlerNoteTitleFont;
        public string[] rgb_HandlerNoteFontColor;
        public string[] rgb_HandlerNoteTitleFontColor;
        public string[] rgb_ButtonsBorderColor;
        public string[] rgb_ThirdPartyToolsLinks;
        public string[] rgb_HandlerNoteBackColor;
        public string[] rgb_HandlerNoteMagnifierTitleBackColor;

        public XInputShortcutsSetup Xinput_S_Setup;
        private Settings settings = null;
        private ProfileSettings profileSettings = null;
        private SearchDisksForm searchDisksForm = null;

        private ContentManager content;
        private IGameHandler I_GameHandler;
        public GameManager gameManager;
        private Dictionary<UserGameInfo, GameControl> controls;
       
        private GameControl currentControl;
        private UserGameInfo currentGameInfo;
        private GenericGameInfo currentGame;
        public HubShowcase hubShowcase;
        private GameProfile currentProfile;
        
        private UserInputControl currentStep;
        public SetupScreenControl setupScreen;
        private PlayerOptionsControl optionsControl;
        private JSUserInputControl jsControl;
        private Handler handler = null;
        private ScriptDownloader scriptDownloader;
        private DownloadPrompt downloadPrompt;
        private SoundPlayer splayer;
        private AssetsDownloader assetsDownloader;

        private int KillProcess_HotkeyID = 1;
        private int TopMost_HotkeyID = 2;
        private int StopSession_HotkeyID = 3;
        private int SetFocus_HotkeyID = 4;
        private int ResetWindows_HotkeyID = 5;
        private int Cutscenes_HotkeyID = 6;
        private int Switch_HotkeyID = 7;
        private int currentStepIndex;

        private List<string> profilePaths = new List<string>();
        private List<Control> ctrls = new List<Control>();
        private List<UserInputControl> stepsList;

        public Action<IntPtr> RawInputAction { get; set; }

        public Bitmap defBackground;
        public Bitmap coverImg;
        public Bitmap screenshotImg;
        public Color buttonsBackColor;
        private Bitmap favorite_Unselected;
        private Bitmap favorite_Selected;

        public bool restartRequired = false;

        //private bool Splash_On;
        private bool ToggleCutscenes = false;
        private bool formClosing;
        private bool noGamesPresent;
        public bool mouseClick;
        public bool roundedcorners;
        public bool useButtonsBorder;
        private bool DisableOfflineIcon;
        private bool showFavoriteOnly;
        private bool canResize = false;
        public bool disableFastHandlerUpdate = false;
        private bool hotkeysLocked = false;

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
                    btn_noHub.Visible = false;
                    btn_downloadAssets.Enabled = true;
                    btn_Download.Enabled = true;

                    System.Threading.Tasks.Task.Run(() =>
                    {  
                        foreach(KeyValuePair<string, GenericGameInfo> game in gameManager.Games)
                        {
                            game.Value.UpdateAvailable = game.Value.Hub.IsUpdateAvailable(true);
                        }
                        
                    });

                    if (currentControl != null)
                    {
                        button_UpdateAvailable.Visible = currentControl.GameInfo.UpdateAvailable;
                    }                   
                }
            }
        }

        private System.Windows.Forms.Timer DisposeTimer;//dispose splash screen timer
        private System.Windows.Forms.Timer rainbowTimer;
        private System.Windows.Forms.Timer hotkeysLockedTimer;//Avoid hotkeys spamming

        public Color TitleBarColor;
        public Color MouseOverBackColor;
        public Color MenuStripBackColor;
        public Color MenuStripFontColor;
        public Color ButtonsBorderColor;
        private Color HandlerNoteBackColor;
        private Color HandlerNoteFontColor;
        private Color HandlerNoteMagnifierTitleBackColor;
        private Color HandlerNoteTitleFont;
        private Color StripMenuUpdateItemBack;
        private Color StripMenuUpdateItemFont;
        private Color SelectionBackColor;
        public FileInfo fontPath;

        private Label handlerUpdateLabel;
        private Label favoriteOnlyLabel;

        private PictureBox favoriteOnly;

        public Cursor hand_Cursor;
        public Cursor default_Cursor;
    
        public void SoundPlayer(string filePath)
        {
            splayer = new SoundPlayer(filePath);
            splayer.Play();
            splayer.Dispose();
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

        private System.Windows.Forms.Timer FadeInTimer;

        private void FadeIn()
        {
            FadeInTimer = new System.Windows.Forms.Timer();
            FadeInTimer.Interval = (50); //millisecond
            FadeInTimer.Tick += new EventHandler(FadeInTick);
            FadeInTimer.Start();
        }

        private void FadeInTick(Object Object, EventArgs EventArgs)
        {
            if (Opacity < 1.0F)
            {
                Opacity += .1;
            }
            else
            {
                FadeInTimer.Dispose();
            }
        }

        private System.Windows.Forms.Timer FadeOutTimer;

        private void FadeOut()
        {
            FadeOutTimer = new System.Windows.Forms.Timer();
            FadeOutTimer.Interval = (50); //millisecond
            FadeOutTimer.Tick += new EventHandler(FadeOutTick);
            FadeOutTimer.Start();
        }

        private void FadeOutTick(Object Object, EventArgs EventArgs)
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

            connected = Program.connected;
            Hub.Connected = connected;
            iconsIni = new IniFile(Path.Combine(Directory.GetCurrentDirectory() + "\\gui\\icons\\icons.ini"));
            //Splash_On = bool.Parse(ini.IniReadValue("Dev", "SplashScreen_On"));
            DisableOfflineIcon = bool.Parse(ini.IniReadValue("Dev", "DisableOfflineIcon"));
            showFavoriteOnly = bool.Parse(ini.IniReadValue("Dev", "ShowFavoriteOnly"));
            mouseClick = bool.Parse(ini.IniReadValue("Dev", "MouseClick"));
            roundedcorners = bool.Parse(themeIni.IniReadValue("Misc", "UseRoundedCorners"));
            useButtonsBorder = bool.Parse(themeIni.IniReadValue("Misc", "UseButtonsBorder"));
            customFont = themeIni.IniReadValue("Font", "FontFamily");
            rgb_font = themeIni.IniReadValue("Colors", "Font").Split(',');
            rgb_MouseOverColor = themeIni.IniReadValue("Colors", "MouseOver").Split(',');
            rgb_MenuStripBackColor = themeIni.IniReadValue("Colors", "MenuStripBack").Split(',');
            rgb_MenuStripFontColor = themeIni.IniReadValue("Colors", "MenuStripFont").Split(',');
            rgb_TitleBarColor = themeIni.IniReadValue("Colors", "TitleBar").Split(',');
            rgb_HandlerNoteBackColor = themeIni.IniReadValue("Colors", "HandlerNoteBack").Split(',');
            rgb_HandlerNoteFontColor = themeIni.IniReadValue("Colors", "HandlerNoteFont").Split(',');
            rgb_HandlerNoteTitleFontColor = themeIni.IniReadValue("Colors", "HandlerNoteTitleFont").Split(',');
            rgb_ButtonsBorderColor = themeIni.IniReadValue("Colors", "ButtonsBorder").Split(',');
            rgb_HandlerNoteMagnifierTitleBackColor = themeIni.IniReadValue("Colors", "HandlerNoteMagnifierTitleBackColor ").Split(',');
                   
            float fontSize = float.Parse(themeIni.IniReadValue("Font", "MainFontSize"));
            bool coverBorderOff = bool.Parse(themeIni.IniReadValue("Misc", "DisableCoverBorder"));
            bool noteBorderOff = bool.Parse(themeIni.IniReadValue("Misc", "DisableNoteBorder"));

            TitleBarColor = Color.FromArgb(int.Parse(rgb_TitleBarColor[0]), int.Parse(rgb_TitleBarColor[1]), int.Parse(rgb_TitleBarColor[2]));
            MouseOverBackColor = Color.FromArgb(int.Parse(rgb_MouseOverColor[0]), int.Parse(rgb_MouseOverColor[1]), int.Parse(rgb_MouseOverColor[2]), int.Parse(rgb_MouseOverColor[3]));
            MenuStripBackColor = Color.FromArgb(int.Parse(rgb_MenuStripBackColor[0]), int.Parse(rgb_MenuStripBackColor[1]), int.Parse(rgb_MenuStripBackColor[2]));
            MenuStripFontColor = Color.FromArgb(int.Parse(rgb_MenuStripFontColor[0]), int.Parse(rgb_MenuStripFontColor[1]), int.Parse(rgb_MenuStripFontColor[2]));
            HandlerNoteBackColor = Color.FromArgb(int.Parse(rgb_HandlerNoteBackColor[0]), int.Parse(rgb_HandlerNoteBackColor[1]), int.Parse(rgb_HandlerNoteBackColor[2]));
            HandlerNoteFontColor = Color.FromArgb(int.Parse(rgb_HandlerNoteFontColor[0]), int.Parse(rgb_HandlerNoteFontColor[1]), int.Parse(rgb_HandlerNoteFontColor[2]));
            HandlerNoteMagnifierTitleBackColor = Color.FromArgb(int.Parse(rgb_HandlerNoteMagnifierTitleBackColor[0]), int.Parse(rgb_HandlerNoteMagnifierTitleBackColor[1]), int.Parse(rgb_HandlerNoteMagnifierTitleBackColor[2]));
            HandlerNoteTitleFont = Color.FromArgb(int.Parse(rgb_HandlerNoteTitleFontColor[0]), int.Parse(rgb_HandlerNoteTitleFontColor[1]), int.Parse(rgb_HandlerNoteTitleFontColor[2]));
            ButtonsBorderColor = Color.FromArgb(int.Parse(rgb_ButtonsBorderColor[0]), int.Parse(rgb_ButtonsBorderColor[1]), int.Parse(rgb_ButtonsBorderColor[2]));
            SelectionBackColor = Color.FromArgb(int.Parse(themeIni.IniReadValue("Colors", "Selection").Split(',')[0]), int.Parse(themeIni.IniReadValue("Colors", "Selection").Split(',')[1]), int.Parse(themeIni.IniReadValue("Colors", "Selection").Split(',')[2]), int.Parse(themeIni.IniReadValue("Colors", "Selection").Split(',')[3]));
            lockKeyIniString = ini.IniReadValue("Hotkeys", "LockKey");

            InitializeComponent();

            if (ini.IniReadValue("Misc", "WindowSize") != "")
            {
                string[] windowSize = ini.IniReadValue("Misc", "WindowSize").Split('X');
                Size = new Size(int.Parse(windowSize[0]), int.Parse(windowSize[1]));
            }

            SuspendLayout();

            default_Cursor = new Cursor(theme + "cursor.ico");
            hand_Cursor = new Cursor(theme + "cursor_hand.ico");
           
            Cursor = default_Cursor;

            if (roundedcorners)
            {
                Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
                clientAreaPanel.Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, clientAreaPanel.Width, clientAreaPanel.Height, 20, 20));
            }
       
            BackColor = TitleBarColor;          

            Font = new Font(customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
            ForeColor = Color.FromArgb(int.Parse(rgb_font[0]), int.Parse(rgb_font[1]), int.Parse(rgb_font[2]));
            scriptAuthorTxt.BackColor = HandlerNoteBackColor;
            scriptAuthorTxt.ForeColor = HandlerNoteFontColor;
          
            HandlerNoteTitle.ForeColor = HandlerNoteTitleFont;

            scriptAuthorTxtSizer.BackColor = Color.FromArgb(int.Parse(themeIni.IniReadValue("Colors", "HandlerNoteContainerBackground").Split(',')[0]),
                                               int.Parse(themeIni.IniReadValue("Colors", "HandlerNoteContainerBackground").Split(',')[1]),
                                               int.Parse(themeIni.IniReadValue("Colors", "HandlerNoteContainerBackground").Split(',')[2]),
                                               int.Parse(themeIni.IniReadValue("Colors", "HandlerNoteContainerBackground").Split(',')[3]));

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

            linksPanel.BackColor = Color.Transparent;

            third_party_tools_container.BackColor = Color.Transparent;

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

            StripMenuUpdateItemBack = Color.FromArgb(int.Parse(themeIni.IniReadValue("Colors", "StripMenuUpdateItemBack").Split(',')[0]),
                                              int.Parse(themeIni.IniReadValue("Colors", "StripMenuUpdateItemBack").Split(',')[1]),
                                              int.Parse(themeIni.IniReadValue("Colors", "StripMenuUpdateItemBack").Split(',')[2]),
                                              int.Parse(themeIni.IniReadValue("Colors", "StripMenuUpdateItemBack").Split(',')[3]));

            StripMenuUpdateItemFont = Color.FromArgb(int.Parse(themeIni.IniReadValue("Colors", "StripMenuUpdateItemFont").Split(',')[0]),
                                               int.Parse(themeIni.IniReadValue("Colors", "StripMenuUpdateItemFont").Split(',')[1]),
                                               int.Parse(themeIni.IniReadValue("Colors", "StripMenuUpdateItemFont").Split(',')[2]),
                                               int.Parse(themeIni.IniReadValue("Colors", "StripMenuUpdateItemFont").Split(',')[3]));

            clientAreaPanel.BackgroundImage = ImageCache.GetImage(theme + "background.jpg");
            btn_textSwitcher.Image = ImageCache.GetImage(theme + "text_switcher.png");
            btn_AutoSearch.BackColor = buttonsBackColor;
            button_UpdateAvailable.BackColor = Color.FromArgb(150,0,0,0);
            btnSearch.BackColor = buttonsBackColor;
            btn_gameOptions.BackColor = buttonsBackColor;
            btn_Download.BackColor = buttonsBackColor;
            btn_Play.BackColor = buttonsBackColor;
            btn_Extract.BackColor = buttonsBackColor;
            btn_gameOptions.BackgroundImage = ImageCache.GetImage(theme + "game_options.png");
            btn_Prev.BackgroundImage = ImageCache.GetImage(theme + "arrow_left.png");
            btn_Next.BackgroundImage = ImageCache.GetImage(theme + "arrow_right.png");
            coverFrame.BackgroundImage = ImageCache.GetImage(theme + "cover_layer.png");
            stepPanelPictureBox.Image = ImageCache.GetImage(theme + "logo.png");
            logo.BackgroundImage = ImageCache.GetImage(theme + "title_logo.png");
            btn_Discord.BackgroundImage = ImageCache.GetImage(theme + "discord.png");
            btn_downloadAssets.BackgroundImage = ImageCache.GetImage(theme + "title_download_assets.png");
            btn_faq.BackgroundImage = ImageCache.GetImage(theme + "faq.png");
            btn_Links.BackgroundImage = ImageCache.GetImage(theme + "title_dropdown_closed.png");
            btn_noHub.BackgroundImage = ImageCache.GetImage(theme + "title_no_hub.png");
            btn_reddit.BackgroundImage = ImageCache.GetImage(theme + "reddit.png");
            btn_SplitCalculator.BackgroundImage = ImageCache.GetImage(theme + "splitcalculator.png");
            btn_thirdPartytools.BackgroundImage = ImageCache.GetImage(theme + "thirdpartytools.png");
            closeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_close.png");
            maximizeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_maximize.png");
            minimizeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_minimize.png");
            btn_settings.BackgroundImage = ImageCache.GetImage(theme + "title_settings.png");
            btn_dlFromHub.BackColor = buttonsBackColor;
            btn_magnifier.Image = ImageCache.GetImage(theme + "magnifier.png");

            favorite_Unselected = ImageCache.GetImage(theme + "favorite_unselected.png");
            favorite_Selected = ImageCache.GetImage(theme + "favorite_selected.png");

            CustomToolTips.SetToolTip(logo, "Nucleus Co-op Github release page.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(btn_Discord, "Join the official Nucleus Co-op discord server.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(btn_downloadAssets, "Download or update games covers and screenshots.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(btn_faq, "Nucleus Co-op FAQ.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(btn_noHub, "Connection with the game handlers hub couldn't be established from the app,handlers, covers and screenshots downloader will be unavailable.\nClick this button to refresh, if the problem persist, click the FAQ button or ask for help on the official Nucleus Co-op Discord/SubReddit.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(btn_reddit, "Official Nucleus Co-op Subreddit.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(btn_SplitCalculator, "This program can estimate the system requirements needed to run a game in split-screen.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(btn_settings, "Globals Nucleus Co-op settings.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(btn_dlFromHub, "Download handlers (.nc) directly from the handlers hub, use the extract handler option to install them.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(btn_thirdPartytools, "Third party tools useful to make non xinput controllers work with Nucleus Co-op.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(linkLabel1, "XOutput is a software that can convert DirectInput into XInput.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(linkLabel2, "Xinput emulator for Ps4 controllers.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(linkLabel3, "With HidHide it is possible to deny a specific application access to one or more human interface devices.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            CustomToolTips.SetToolTip(linkLabel4, "Xinput emulator for Ps3 controllers.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });

            btn_Extract.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
            btn_AutoSearch.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
            button_UpdateAvailable.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
            btnSearch.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
            btn_gameOptions.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
            btn_Download.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
            btn_Play.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
            btn_Prev.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
            btn_Next.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
            btn_dlFromHub.FlatAppearance.MouseOverBackColor = MouseOverBackColor;
            gameContextMenuStrip.BackColor = MenuStripBackColor;
            gameContextMenuStrip.ForeColor = MenuStripFontColor;

            if (useButtonsBorder)
            {
                btn_AutoSearch.FlatAppearance.BorderSize = 1;
                btn_AutoSearch.FlatAppearance.BorderColor = ButtonsBorderColor;
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
                btn_Prev.FlatAppearance.BorderSize = 1;
                btn_Prev.FlatAppearance.BorderColor = ButtonsBorderColor;
                btn_Next.FlatAppearance.BorderSize = 1;
                btn_Next.FlatAppearance.BorderColor = ButtonsBorderColor;
                btn_dlFromHub.FlatAppearance.BorderSize = 1;
                btn_dlFromHub.FlatAppearance.BorderColor = ButtonsBorderColor;
            }

            linksPanel.Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, linksPanel.Width, linksPanel.Height, 15, 15));
            third_party_tools_container.Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, third_party_tools_container.Width, third_party_tools_container.Height, 10, 10));
            scriptAuthorTxtSizer.Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, scriptAuthorTxtSizer.Width, scriptAuthorTxtSizer.Height, 20, 20));

            btn_magnifier.Cursor = hand_Cursor;
            btn_textSwitcher.Cursor = hand_Cursor;

            linkLabel1.Cursor = hand_Cursor;
            linkLabel2.Cursor = hand_Cursor;
            linkLabel3.Cursor = hand_Cursor;
            linkLabel4.Cursor = hand_Cursor;
            gameContextMenuStrip.Cursor = hand_Cursor;
            button_UpdateAvailable.Cursor = hand_Cursor;

            Globals.PlayButton = btn_Play;
            Globals.NoteZoomButton = btn_magnifier;

            if (coverBorderOff)
            {
                cover.BorderStyle = BorderStyle.None;
            }

            if (noteBorderOff)
            {
                scriptAuthorTxtSizer.BorderStyle = BorderStyle.None;
            }
               
            controlscollect();

            foreach (Control control in ctrls)
            {
                if (control.Name != "btn_Links" && control.Name != "btn_thirdPartytools" && control.Name != "HandlerNoteTitle" && control.Name != "scriptAuthorTxt")//Close "third_party_tools_container" control when an other control in the form is clicked.
                {
                    control.Font = new Font(customFont, fontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    control.Click += new EventHandler(this.this_Click);
                }

                if (control.GetType() == typeof(Button))
                {
                    control.Cursor = hand_Cursor;
                }

                control.Click += new EventHandler(button_Click);

                if (mouseClick)
                {
                    handleClickSound(true);
                }
            }
#if DEBUG
            txt_version.ForeColor = Color.LightSteelBlue;
            txt_version.Text = "DEBUG " + version;
#else
            if (bool.Parse(themeIni.IniReadValue("Misc", "HideVersion")) == false)
            {
                txt_version.Text = version;
            }
            else
            {
                txt_version.Text = "";
            }
#endif
            ResumeLayout();

            minimizeBtn.Click += new EventHandler(this.minimizeButtonClick);
            maximizeBtn.Click += new EventHandler(this.maximizeButtonClick);
            closeBtn.Click += new EventHandler(this.closeButtonClick);

            defBackground = clientAreaPanel.BackgroundImage as Bitmap;

            setupScreen = new SetupScreenControl();

            setupScreen.handlerNoteZoom.BackColor = HandlerNoteBackColor;
            setupScreen.handlerNoteZoom.ForeColor = HandlerNoteFontColor;
            setupScreen.profileSettings_btn.Click += new EventHandler(this.ProfileSettings_btn_Click);
            setupScreen.gameProfilesList_btn.Click += new EventHandler(this.gameProfilesList_btn_Click);
            setupScreen.OnCanPlayUpdated += StepCanPlay;
            setupScreen.Click += new EventHandler(this_Click);
            
            settings = new Settings(this, setupScreen);
            profileSettings = new ProfileSettings(this, setupScreen);
            searchDisksForm = new SearchDisksForm(this);

            setupScreen.Paint += setupScreen_Paint;

            settings.RegHotkeys(this);

            controls = new Dictionary<UserGameInfo, GameControl>();
            gameManager = new GameManager(this);
            assetsDownloader = new AssetsDownloader();
            optionsControl = new PlayerOptionsControl();
            jsControl = new JSUserInputControl();

            optionsControl.OnCanPlayUpdated += StepCanPlay;
            jsControl.OnCanPlayUpdated += StepCanPlay;

            scriptDownloader = new ScriptDownloader(this);
            
            downloadPrompt = new DownloadPrompt(handler, this, null, true);
            Xinput_S_Setup = new XInputShortcutsSetup();

            favoriteOnlyLabel = new Label
            {
                AutoSize = true,
                Text = "Favorite Games",
                BackColor = Color.Transparent,
                ForeColor = this.ForeColor,
            };

            favoriteOnly = new PictureBox
            {
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
                Cursor = hand_Cursor
            };

            favoriteOnly.Image = showFavoriteOnly ? favorite_Selected : favorite_Unselected;
            favoriteOnly.Click += new EventHandler(FavoriteOnly_Click);

            mainButtonFrame.Controls.Add(favoriteOnlyLabel);
            mainButtonFrame.Controls.Add(favoriteOnly);

            hotkeysLockedTimer = new System.Windows.Forms.Timer();
            hotkeysLockedTimer.Tick += new EventHandler(hotkeysLockedTimerTick);

            gameContextMenuStrip.Renderer = new CustomToolStripRenderer.MyRenderer();
            gameContextMenuStrip.BackgroundImage = ImageCache.GetImage(theme + "other_backgrounds.jpg");

            RefreshGames();

            Rectangle area = Screen.PrimaryScreen.Bounds;

            if (ini.IniReadValue("Misc", "WindowLocation") != "")
            {
                string[] windowLocation = ini.IniReadValue("Misc", "WindowLocation").Split('X');
                Location = new Point(area.X + int.Parse(windowLocation[0]), area.Y + int.Parse(windowLocation[1]));
            }
            else 
            {
                CenterToScreen();
            }
            
            //Enable only for Windows versions with default support for xinput1.4.dll ,
            //might be fixable by placing the dll at the root of our exe but not for now.
            string windowsVersion = MachineSpecs.GetPCspecs(null);
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
        

       

        private void FavoriteOnly_Click(object sender, EventArgs e)
        {
            bool selected = favoriteOnly.Image.Equals(favorite_Selected);

            if (selected)
            {
                favoriteOnly.Image = favorite_Unselected;
                showFavoriteOnly = false;
            }
            else
            {
                favoriteOnly.Image = favorite_Selected;
                showFavoriteOnly = true;
            }

            ini.IniWriteValue("Dev", "ShowFavoriteOnly", showFavoriteOnly.ToString());         
            RefreshGames();
        }

        public new void UpdateSize(float scale)
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

            gameContextMenuStrip.Font = new Font(gameContextMenuStrip.Font.FontFamily, 10.25f, FontStyle.Regular, GraphicsUnit.Pixel, 0);
            
            btn_Play.Font = new Font(customFont, mainButtonFrameFont, FontStyle.Bold, GraphicsUnit.Pixel, 0);
            
            scriptAuthorTxt.Font = new Font(customFont, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
            scriptAuthorTxt.Size = new Size((int)(189 * scale), (int)(191 * scale));
           
            favoriteOnlyLabel.Font = new Font(customFont, mainButtonFrameFont, FontStyle.Regular, GraphicsUnit.Pixel, 0);
            favoriteOnlyLabel.Location = new Point(1,( mainButtonFrame.Bottom - 23) - (favoriteOnlyLabel.Height / 2));
            favoriteOnly.Size = new Size(favoriteOnlyLabel.Height, favoriteOnlyLabel.Height);
            float favoriteY = favoriteOnlyLabel.Right + (5 * scale);
            favoriteOnly.Location = new Point((int)(favoriteY), (mainButtonFrame.Bottom - 23) - (favoriteOnlyLabel.Height / 2));

            ResumeLayout();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            DPIManager.ForceUpdate();
        }

        public void handleClickSound(bool enable)
        {
            mouseClick = enable;
        }

        private void MainTimerTick(Object Object, EventArgs EventArgs)
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
                if (!DisableOfflineIcon) { btn_noHub.Visible = true; }
                btn_downloadAssets.Enabled = false;
                btn_Download.Enabled = false;
            }
        }

        private void setupScreen_Paint(object sender, PaintEventArgs e)
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
            Rectangle outRect = new Rectangle(RESIZE_HANDLE_SIZE, RESIZE_HANDLE_SIZE, Width - RESIZE_HANDLE_SIZE*2, Height - RESIZE_HANDLE_SIZE*2);

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
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == KillProcess_HotkeyID)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    if (hotkeysLocked)
                    {
                        return;
                    }

                    User32Util.ShowTaskBar();
                    Close();
                }
                else
                {
                    TriggerOSD(1600, $"Unlock Inputs First (Press {lockKeyIniString} key)");
                }
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == TopMost_HotkeyID)
            {

                if (hotkeysLocked || I_GameHandler == null)
                {
                    return;
                }

                GlobalWindowMethods.ShowHideWindows(currentGame);
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == StopSession_HotkeyID)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    if (hotkeysLocked || I_GameHandler == null)
                    {
                        return;
                    }

                    if (btn_Play.Text == "S T O P")
                    {
                        btn_Play.PerformClick();
                    }

                    TriggerOSD(2000, "Session Ended");
                }
                else
                {
                    TriggerOSD(1600, $"Unlock Inputs First (Press {lockKeyIniString} key)");
                }
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == SetFocus_HotkeyID)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    if (hotkeysLocked || I_GameHandler == null)
                    {
                        return;
                    }

                    GlobalWindowMethods.ChangeForegroundWindow();
                    TriggerOSD(2000, "Game Windows Unfocused");
                    mainButtonFrame.Focus();
                }
                else
                {
                    TriggerOSD(1600, $"Unlock Inputs First (Press {lockKeyIniString} key)");
                }
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == ResetWindows_HotkeyID)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    if (hotkeysLocked || I_GameHandler == null)
                    {
                        return;
                    }

                    I_GameHandler.Update(currentGame.HandlerInterval, true);                   
                }
                else
                {
                    TriggerOSD(1600, $"Unlock Inputs First (Press {lockKeyIniString} key)");
                }
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == Cutscenes_HotkeyID)
            {
                if (hotkeysLocked || I_GameHandler == null)
                {
                    return;
                }

                if (!ToggleCutscenes)
                {
                    GlobalWindowMethods.ToggleCutScenesMode(true);
                    ToggleCutscenes = true;
                }
                else
                {
                    GlobalWindowMethods.ToggleCutScenesMode(false);
                    ToggleCutscenes = false;
                }
            }
            else if (m.Msg == 0x0312 && m.WParam.ToInt32() == Switch_HotkeyID)
            {
                if (!Gaming.Coop.InputManagement.LockInput.IsLocked)
                {
                    if (hotkeysLocked || I_GameHandler == null)
                    {
                        return;
                    }

                    GlobalWindowMethods.SwitchLayout();
                }
                else
                {
                    TriggerOSD(1600, $"Unlock Inputs First (Press {lockKeyIniString} key)");
                }
            }

            base.WndProc(ref m);
        }

        public void TriggerOSD(int timerMS, string text)
        {
            if (!hotkeysLocked)
            {
                hotkeysLockedTimer.Stop();
                hotkeysLocked = true;
                hotkeysLockedTimer.Interval = (timerMS); //millisecond
                hotkeysLockedTimer.Start();

                Globals.MainOSD.Show(timerMS, text);
            }
        }

        private void hotkeysLockedTimerTick(Object Object, EventArgs EventArgs)
        {
            hotkeysLocked = false;
            hotkeysLockedTimer.Stop();
        }

        public void RefreshGames()
        {
            List<UserGameInfo> games;

            lock (controls)
            {
                foreach (KeyValuePair<UserGameInfo, GameControl> con in controls)
                {
                    if (con.Value != null)
                    {
                        con.Value.Dispose();
                    }
                }

                list_Games.Controls.Clear();
                controls.Clear();

                games = gameManager.User.Games;

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
                    list_Games.Visible = false;///smoother transition

                    for (int i = 0; i < games.Count; i++)
                    {
                        UserGameInfo game = games[i];           
                        NewUserGame(game);
                    }

                    list_Games.Visible = true;  
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

            bool favorite = game.Favorite;

            GameControl con = new GameControl(game.Game, game, favorite)
            {
                Width = game_listSizer.Width,
            };
            
            if (showFavoriteOnly)
            {
                if (favorite)
                {
                    controls.Add(game, con);
                    list_Games.Controls.Add(con);
                    ThreadPool.QueueUserWorkItem(GetIcon, game);
                }
            }
            else
            {
                controls.Add(game, con);
                list_Games.Controls.Add(con);
                ThreadPool.QueueUserWorkItem(GetIcon, game);
            }

            if (currentControl != null)
            {
                if (currentControl.TitleText == con.TitleText)
                    con.BackColor = SelectionBackColor;
            }

            list_Games.ResumeLayout();
        }

        public void RefreshUI(bool refreshAll)
        {
            SuspendLayout();

            if (refreshAll)
            {
                rightFrame.Visible = false;
                StepPanel.Visible = false;
                clientAreaPanel.BackgroundImage = defBackground;
                stepPanelPictureBox.Visible = true;
                rainbowTimer?.Dispose();
                rainbowTimerRunning = false;
                btn_Next.Enabled = false;
                btn_Prev.BackgroundImage = ImageCache.GetImage(theme + "arrow_left.png");
                btn_Prev.Enabled = false;
            }

            hubShowcase?.Dispose();
            hubShowcase = null;

            if (currentControl != null)
            {
                RefreshGames();
            }

            mainButtonFrame.Focus();

            ResumeLayout();
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
                    bmp = ImageCache.GetImage(Path.Combine(Directory.GetCurrentDirectory() + "\\gui\\icons\\default.png"));
                    Icon icon = Shell32.GetIcon(iconPath, false);
                    bmp = icon.ToBitmap();
                    icon.Dispose();
                }
                else
                {
                    if (File.Exists(iconPath))
                    {
                        bmp = ImageCache.GetImage(iconPath);
                    }
                    else
                    {
                        if (File.Exists(Path.Combine(Directory.GetCurrentDirectory() + "\\gui\\icons\\default.png")))
                        {
                            bmp = ImageCache.GetImage(Path.Combine(Directory.GetCurrentDirectory() + "\\gui\\icons\\default.png"));
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

        private void btn_downloadAssets_Click(object sender, EventArgs e)
        {
            if (gameManager.User.Games.Count == 0)
            {
                TriggerOSD(1600, $"Add Game(s) In Your List");
                return;
            }
           
            assetsDownloader.DownloadGameAssets(this, gameManager, scriptDownloader, currentControl);          
        }

        private int r = 0;
        private int g = 0;
        private int b = 0;
        private bool loop = false;

        private void rainbowTimerTick(Object Object, EventArgs EventArgs)
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

                HandlerNoteTitle.ForeColor =  Color.FromArgb(r,r, 255, b);
            }
            else if (text.Contains("Description"))
            {
                HandlerNoteTitle.ForeColor = Color.Gold;
                HandlerNoteTitle.Font = new Font(HandlerNoteTitle.Font.FontFamily, (float)HandlerNoteTitle.Font.Size, FontStyle.Regular);
            }
        }

        private bool rainbowTimerRunning = false;

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

            if (currentGameInfo != null)
            {
                if (!CheckGameRequirements.MatchRequirements(currentGameInfo))
                {
                    RefreshUI(true);
                    return;
                }
            }

            profileSettings.Visible = false;
            setupScreen.textZoomContainer.Visible = false;
            btn_magnifier.Image = ImageCache.GetImage(theme + "magnifier.png");
            btn_textSwitcher.Visible = false;

            screenshotImg?.Dispose();

            if (currentGameInfo == null)
            {
                return;
            }
            else
            {
                currentGame = currentGameInfo.Game;

                if (!currentGameInfo.KeepSymLink && !currentGameInfo.Game.KeepSymLinkOnExit)
                {
                    //Cursor.Current = Cursors.WaitCursor;
                    CleanGameContent.CleanContentFolder(currentGame);
                    //Cursor.Current = default_Cursor;
                }

                button_UpdateAvailable.Visible = currentGameInfo.Game.UpdateAvailable;

                HandlerNoteTitle.Text = "Handler Notes";
                hubShowcase?.Dispose();
                hubShowcase = null;
                
                if (!rainbowTimerRunning)
                {
                    rainbowTimer = new System.Windows.Forms.Timer();
                    rainbowTimer.Interval = (25); //millisecond                   
                    rainbowTimer.Tick += new EventHandler(rainbowTimerTick);
                    rainbowTimer.Start();
                    rainbowTimerRunning = true;
                }             
                         
                icons_Container.Controls.Clear();
                icons_Container.Controls.AddRange(InputIcons.SetInputsIcons(this, currentGame));
              
                btn_Play.Enabled = false;
                rightFrame.Visible = true;
                                
                StepPanel.Visible = true;
                setupScreen.textZoomContainer.Visible = false;
                stepPanelPictureBox.Visible = false;

                SetBackroundAndCover.ApplyBackgroundAndCover(this, currentGame.GUID);

                game_listSizer.Refresh();
                mainButtonFrame.Refresh();
                StepPanel.Refresh();
                rightFrame.Refresh();

                currentProfile = new GameProfile();

                GameProfile.GameGUID = currentGame.GUID;

                stepsList = new List<UserInputControl>
                {
                   setupScreen,
                   optionsControl
                };

                for (int i = 0; i < currentGame.CustomSteps.Count; i++)
                {
                    stepsList.Add(jsControl);
                }

               

                currentProfile.InitializeDefault(currentGame, setupScreen);
                gameManager.UpdateCurrentGameProfile(currentProfile);

                if (!disableGameProfiles)
                {
                    setupScreen.profileSettings_btn.Visible = true;
                            
                    ProfilesList.profilesList.Update_ProfilesList();

                    bool showList = GameProfile.profilesPathList.Count > 0;
                    setupScreen.gameProfilesList.Visible = showList; 
                    setupScreen.gameProfilesList_btn.Image = ImageCache.GetImage(theme + "profiles_list_opened.png");
                    setupScreen.gameProfilesList_btn.Visible = showList;

                    ProfilesList.profilesList.Locked = false;
                }
                else
                {
                    setupScreen.profileSettings_btn.Visible = false;
                    setupScreen.gameProfilesList.Visible = false;
                    setupScreen.gameProfilesList_btn.Visible = false;
                }

                btn_textSwitcher.Visible = File.Exists(Path.Combine(Application.StartupPath, $"gui\\descriptions\\{currentGame.GUID}.txt"));

                if (currentGame.Description?.Length > 0)
                {
                    scriptAuthorTxt.Text = currentGame.Description;
                    scriptAuthorTxtSizer.Visible = true;
                }
                else if (File.Exists(Path.Combine(Application.StartupPath, $"gui\\descriptions\\{currentGame.GUID}.txt")))
                {
                    StreamReader desc = new StreamReader(Path.Combine(Application.StartupPath, $"gui\\descriptions\\{currentGame.GUID}.txt"));

                    HandlerNoteTitle.Text = "Game Description";
                    scriptAuthorTxt.Text = desc.ReadToEnd();
                    btn_textSwitcher.Visible = false;
                    desc.Dispose();
                }
                else
                {
                    scriptAuthorTxtSizer.Visible = false;
                    scriptAuthorTxt.Text = "";
                }

                content?.Dispose();

              
                // content manager is shared within the same game
                content = new ContentManager(currentGame);

                GoToStep(0);
            }
        }

        private void EnablePlay()
        {
            btn_Play.Enabled = true;
        }

        private void StepCanPlay(UserControl obj, bool canProceed, bool autoProceed)
        {
            if (btn_Prev.Enabled)
            {
                btn_Prev.BackgroundImage = ImageCache.GetImage(theme + "arrow_left_mousehover.png");
            }
            else
            {
                btn_Prev.BackgroundImage = ImageCache.GetImage(theme + "arrow_left.png");
            }

            if (!canProceed)
            {
                btn_Prev.Enabled = false;

                if (btn_Play.Text == "PLAY" || btn_Next.Enabled)
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
                if (btn_Play.Text == "PLAY")
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

        private void btnNext_Click(object sender, EventArgs e) => GoToStep(currentStepIndex + 1);

        private void KillCurrentStep()
        {
            foreach (Control c in StepPanel.Controls)
            {
                if (!c.Name.Equals("scriptAuthorTxtSizer"))
                {
                    StepPanel.Controls.Remove(c);
                }
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
                    label_StepTitle.Text = currentStep.Title;

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
            label_StepTitle.Text = currentStep.Title;

            btn_Next.Enabled = currentStep.CanProceed && step != stepsList.Count - 1;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            formClosing = true;

            if (I_GameHandler != null)
            {
                Log("OnFormClosed method calling Handler End function");

                try
                {
                    I_GameHandler.End(false);
                }
                catch {}
            }

            User32Util.ShowTaskBar();
           
            if (!restartRequired)
            {
                Process.GetCurrentProcess().Kill();
            }
        }

        private void btn_Play_Click(object sender, EventArgs e)
        {
            if (btn_Play.Text == "S T O P")
            {
                try
                {
                    if (I_GameHandler != null)
                    {
                        Log("Stop button clicked, calling Handler End function");
                        I_GameHandler.End(true);
                    }
                }
                catch { }

                GameProfile._GameProfile.Reset();
                DevicesFunctions.gamepadTimer = new System.Threading.Timer(DevicesFunctions.GamepadTimer_Tick, null, 0, 500);
                return;
            }

            currentStep?.Ended();

            btn_Play.Text = "S T O P";

            btn_Prev.Enabled = false;

            gameManager.AddScript(Path.GetFileNameWithoutExtension(currentGame.JsFileName));

            currentGame = gameManager.GetGame(currentGameInfo.ExePath);
            currentGameInfo.InitializeDefault(currentGame, currentGameInfo.ExePath);

            I_GameHandler = gameManager.MakeHandler(currentGame);
                         
            I_GameHandler.Initialize(currentGameInfo, GameProfile.CleanClone(currentProfile), I_GameHandler);
            I_GameHandler.Ended += handler_Ended;

            GameProfile.Game = currentGame;
           
            if (profileSettings.Visible)
            {
                profileSettings.Visible = false;
            }
                     
            if (!currentGame.ToggleUnfocusOnInputsLock)///Not sure if necessary
            {
                 WindowState = FormWindowState.Minimized;
            }

            RefreshUI(true);
        }

        private void handler_Ended()
        {
            Log("Handler ended method called");

            User32Util.ShowTaskBar();

            this.Invoke((MethodInvoker)delegate ()
             {
                 I_GameHandler = null;
                 currentControl = null;
                
                 WindowState = FormWindowState.Normal;

                 mainButtonFrame.Focus();
                 btn_Play.Text = "PLAY";
                 btn_Play.Enabled = false;

                 BringToFront();
             });
        }

        private void btn_Prev_Click(object sender, EventArgs e)
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

        private void btnSearch_Click(object sender, EventArgs e) => SearchGame.Search(this,null);

        private void btnAutoSearch_Click(object sender, EventArgs e)
        {
            if (!searchDisksForm.Visible)
            {
                searchDisksForm.BringToFront();
                searchDisksForm.Visible = true;
            }
            else
            {
                searchDisksForm.Visible = false;
            }
        }

        private void DetailsToolStripMenuItem_Click(object sender, EventArgs e) => GetGameDetails.GetDetails(currentGameInfo);

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e) => RemoveGame.Remove(this, currentGameInfo,false);
        

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
                gameContextMenuStrip.Items[20].Visible = false;

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
                                    catch (Exception)
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

                        if (i == 20)
                        {
                            if (currentGameInfo.KeepSymLink)
                            {
                                gameContextMenuStrip.Items[20].Image = ImageCache.GetImage(theme + "locked.png");
                            }
                            else
                            {
                                gameContextMenuStrip.Items[20].Image = ImageCache.GetImage(theme + "unlocked.png");

                            }
                        }

                        if (i == 21)
                        {
                            gameContextMenuStrip.Items[21].Visible = false;
                            gameContextMenuStrip.Items[21].Visible = currentGameInfo.Game.UpdateAvailable;
                            gameContextMenuStrip.Items[21].ForeColor = StripMenuUpdateItemFont;
                            gameContextMenuStrip.Items[21].BackColor = StripMenuUpdateItemBack;
                        }


                    }

                    for (int i = 1; i < gameContextMenuStrip.Items.Count; i++)
                    {
                        if ((gameContextMenuStrip.Items[i] as ToolStripMenuItem) != null)
                        {
                            if ((gameContextMenuStrip.Items[i] as ToolStripMenuItem).DropDownItems.Count > 0)
                            {
                                for (int d = 0; d < (gameContextMenuStrip.Items[i] as ToolStripMenuItem).DropDownItems.Count; d++)
                                {
                                    (gameContextMenuStrip.Items[i] as ToolStripMenuItem).DropDownItems[d].BackColor = MenuStripBackColor;
                                    (gameContextMenuStrip.Items[i] as ToolStripMenuItem).DropDownItems[d].ForeColor = MenuStripFontColor;
                                }
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

        private void gameContextMenuStrip_Opened(object sender, EventArgs e)
        => gameContextMenuStrip.Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(2, 2, gameContextMenuStrip.Width - 1, gameContextMenuStrip.Height, 20, 20));
        

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

        private void OpenScriptToolStripMenuItem_Click(object sender, EventArgs e)
        => OpenHandler.OpenRawHandler(currentGameInfo);

        private void OpenDataFolderToolStripMenuItem_Click(object sender, EventArgs e) 
        => OpenGameContentFolder.OpenDataFolder(currentGameInfo);

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
                    if (dlg.FileName.EndsWith(".exe"))
                    {
                        Icon icon = Shell32.GetIcon(dlg.FileName, false);

                        Bitmap bmp = icon.ToBitmap();
                        icon.Dispose();
                        currentGameInfo.Icon = bmp;
                    }
                    else
                    {
                        currentGameInfo.Icon = ImageCache.GetImage(dlg.FileName);
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
            NucleusMessageBox.Show("Handler Author's Notes", currentGameInfo.Game.Description,true);
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

        private void btn_Download_Click(object sender, EventArgs e)
        {                     
            if (scriptDownloader.Visible)
            {
                scriptDownloader.Visible = false;
            }
            else
            {               
                scriptDownloader.Visible = true;
            }
        }

        private void button_UpdateAvailable_Click(object sender, EventArgs e)
        {
            handler = scriptDownloader.GetHandler(currentGameInfo.Game.HandlerId);

            if (handler == null)
            {
                button_UpdateAvailable.Visible = false;
                MessageBox.Show("Error fetching update information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("An update to this handler is available, download it?", "Update Available", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.Yes)
                {
                    RemoveGame.Remove(this, currentGameInfo, true);
                    StepPanel.Visible = false;
                    label_StepTitle.Text = "Select a game";
                    btn_Play.Enabled = false;
                    btn_Next.Enabled = false;
                    button_UpdateAvailable.Visible = false;
                    stepsList.Clear();

                    downloadPrompt = new DownloadPrompt(handler, this, null, true);
                    list_Games.SuspendLayout();
                    downloadPrompt.ShowDialog();
                    list_Games.ResumeLayout();
                    StepPanel.Visible = true;
                }
            }
        }

        private void updateHandlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            handler = scriptDownloader.GetHandler(currentGameInfo.Game.HandlerId);

            if (handler == null)
            {
                MessageBox.Show("Error fetching update information", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                btn_Play.Enabled = false;
                btn_Next.Enabled = false;

                downloadPrompt = new DownloadPrompt(handler, this, null, true);
                downloadPrompt.gameExeNoUpdate = true;
                list_Games.SuspendLayout();
                downloadPrompt.ShowDialog();
                list_Games.ResumeLayout();
        
                for (int i = 0; i < gameManager.User.Games.Count; i++)
                {
                    if (gameManager.User.Games[i].Game != null)
                    {
                        if (gameManager.User.Games[i].Game.GameName == currentGameInfo.Game.GameName)
                        {                  
                            string path = gameManager.User.Games[i].ExePath;
                            gameManager.User.Games.RemoveAt(i);
                            List<GenericGameInfo> info = gameManager.GetGames(path);

                            if (info.Count == 1)
                            {
                                GameManager.Instance.TryAddGame(path, info[0]);
                                break;
                            }
                        }
                    }
                }

                if (StepPanel.Visible)
                {
                    rightFrame.Visible = false;
                    StepPanel.Visible = false;
                    clientAreaPanel.BackgroundImage = defBackground;
                    stepPanelPictureBox.Visible = true;
                }

                RefreshGames();
            }
        }

        private void btn_Extract_Click(object sender, EventArgs e)
        {
            ExtractHandler.Extract(this);
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

        private void btn_noHub_Click(object sender, EventArgs e)
        {
            Connected = StartChecks.CheckHubResponse();
        }

        private void btn_Links_Click(object sender, EventArgs e)
        {
            if (linksPanel.Visible)
            {
                linksPanel.Visible = false;
                btn_Links.BackgroundImage = ImageCache.GetImage(theme + "title_dropdown_closed.png");

                if (third_party_tools_container.Visible)
                {
                    third_party_tools_container.Visible = false;
                }
            }
            else
            {
                linksPanel.BringToFront();
                linksPanel.Visible = true;
                btn_Links.BackgroundImage = ImageCache.GetImage(theme + "title_dropdown_opened.png");
            }
        }

        private void this_Click(object sender, EventArgs e)
        {
            linksPanel.Visible = false;
            btn_Links.BackgroundImage = ImageCache.GetImage(theme + "title_dropdown_closed.png");

            if (third_party_tools_container.Visible)
            {
                third_party_tools_container.Visible = false;
            }
        }

        private void button1_Click_2(object sender, EventArgs e) => Process.Start("https://hub.splitscreen.me/");

        private void button1_Click(object sender, EventArgs e) => Process.Start("https://discord.com/invite/QDUt8HpCvr");

        private void button2_Click(object sender, EventArgs e) => Process.Start("https://www.reddit.com/r/nucleuscoop/");

        private void logo_Click(object sender, EventArgs e) => Process.Start("https://github.com/SplitScreen-Me/splitscreenme-nucleus/releases");

        private void link_faq_Click(object sender, EventArgs e) => Process.Start(faq_link);

        private void linkLabel4_LinkClicked(object sender, EventArgs e) { Process.Start("https://github.com/nefarius/ScpToolkit/releases"); third_party_tools_container.Visible = false; }

        private void linkLabel3_LinkClicked(object sender, EventArgs e) { Process.Start("https://github.com/ViGEm/HidHide/releases"); third_party_tools_container.Visible = false; }

        private void linkLabel2_LinkClicked(object sender, EventArgs e) { Process.Start("https://github.com/Ryochan7/DS4Windows/releases"); third_party_tools_container.Visible = false; }

        private void linkLabel1_LinkClicked(object sender, EventArgs e) { Process.Start("https://github.com/csutorasa/XOutput/releases"); third_party_tools_container.Visible = false; }

        private void btn_thirdPartytools_Click(object sender, EventArgs e) { if (third_party_tools_container.Visible) { third_party_tools_container.Visible = false; } else { third_party_tools_container.Visible = true; } }

        private void scriptAuthorTxt_LinkClicked(object sender, LinkClickedEventArgs e) => Process.Start(e.LinkText);

        private void closeBtn_MouseEnter(object sender, EventArgs e) => closeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_close_mousehover.png");

        private void closeBtn_MouseLeave(object sender, EventArgs e) => closeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_close.png");

        private void maximizeBtn_MouseEnter(object sender, EventArgs e) => maximizeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_maximize_mousehover.png");

        private void maximizeBtn_MouseLeave(object sender, EventArgs e) => maximizeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_maximize.png");

        private void minimizeBtn_MouseLeave(object sender, EventArgs e) => minimizeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_minimize.png");
            
        private void minimizeBtn_MouseEnter(object sender, EventArgs e) => minimizeBtn.BackgroundImage = ImageCache.GetImage(theme + "title_minimize_mousehover.png");

        private void btn_settings_MouseEnter(object sender, EventArgs e) { if (profileSettings.Visible) { return; } btn_settings.BackgroundImage = ImageCache.GetImage(theme + "title_settings_mousehover.png"); }

        private void btn_settings_MouseLeave(object sender, EventArgs e) => btn_settings.BackgroundImage = ImageCache.GetImage(theme + "title_settings.png");

        private void btn_downloadAssets_MouseEnter(object sender, EventArgs e) => btn_downloadAssets.BackgroundImage = ImageCache.GetImage(theme + "title_download_assets_mousehover.png"); 

        private void btn_downloadAssets_MouseLeave(object sender, EventArgs e) => btn_downloadAssets.BackgroundImage = ImageCache.GetImage(theme + "title_download_assets.png");

        private void btn_faq_MouseEnter(object sender, EventArgs e) => btn_faq.BackgroundImage = ImageCache.GetImage(theme + "faq_mousehover.png");

        private void btn_faq_MouseLeave(object sender, EventArgs e) => btn_faq.BackgroundImage = ImageCache.GetImage(theme + "faq.png");

        private void btn_reddit_MouseEnter(object sender, EventArgs e) => btn_reddit.BackgroundImage = ImageCache.GetImage(theme + "reddit_mousehover.png");

        private void btn_reddit_MouseLeave(object sender, EventArgs e) => btn_reddit.BackgroundImage = ImageCache.GetImage(theme + "reddit.png");

        private void btn_Discord_MouseEnter(object sender, EventArgs e) => btn_Discord.BackgroundImage = ImageCache.GetImage(theme + "discord_mousehover.png");

        private void btn_Discord_MouseLeave(object sender, EventArgs e) => btn_Discord.BackgroundImage = ImageCache.GetImage(theme + "discord.png");

        private void btn_SplitCalculator_MouseEnter(object sender, EventArgs e) => btn_SplitCalculator.BackgroundImage = ImageCache.GetImage(theme + "splitcalculator_mousehover.png");

        private void btn_SplitCalculator_MouseLeave(object sender, EventArgs e) => btn_SplitCalculator.BackgroundImage = ImageCache.GetImage(theme + "splitcalculator.png");

        private void btn_thirdPartytools_MouseEnter(object sender, EventArgs e) => btn_thirdPartytools.BackgroundImage = ImageCache.GetImage(theme + "thirdPartytools_mousehover.png");

        private void btn_thirdPartytools_MouseLeave(object sender, EventArgs e) => btn_thirdPartytools.BackgroundImage = ImageCache.GetImage(theme + "thirdPartytools.png");

        private void btn_magnifier_Click(object sender, EventArgs e)
        {
            if (!setupScreen.textZoomContainer.Visible)
            {
                setupScreen.textZoomContainer.Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, setupScreen.textZoomContainer.Width, setupScreen.textZoomContainer.Height, 15, 15));
                setupScreen.handlerNoteZoom.Text = scriptAuthorTxt.Text;
                setupScreen.handlerNoteZoom.Visible = true;
                setupScreen.textZoomContainer.Visible = true;
                setupScreen.textZoomContainer.BringToFront();
                btn_magnifier.Image = ImageCache.GetImage(theme + "magnifier_close.png");
            }
            else
            {
                setupScreen.textZoomContainer.Visible = false;
                btn_magnifier.Image = ImageCache.GetImage(theme + "magnifier.png");
            }
        }

        private void btn_textSwitcher_Click(object sender, EventArgs e)
        {
            if (setupScreen.textZoomContainer.Visible)
            {
                return;
            }

            bool gameDesExist = !setupScreen.textZoomContainer.Visible && File.Exists(Path.Combine(Application.StartupPath, $@"gui\descriptions\" + currentGame.GUID + ".txt"));
            bool notesExist = currentGame.Description != null;

            if (gameDesExist && !HandlerNoteTitle.Text.Contains("Profile n°"))
            {
                StreamReader desc = new StreamReader(Path.Combine(Application.StartupPath, $@"gui\descriptions\" + currentGame.GUID + ".txt"));
                if (HandlerNoteTitle.Text == "Handler Notes" || HandlerNoteTitle.Text == "Read First")
                {
                    HandlerNoteTitle.Text = "Game Description";
                    scriptAuthorTxt.Text = desc.ReadToEnd();
                    desc.Dispose();
                }
                else if (notesExist)
                {
                    HandlerNoteTitle.Text = "Handler Notes";
                    scriptAuthorTxt.Text = currentGame.Description;
                    desc.Dispose();
                }
            }

            btn_textSwitcher.Visible = (gameDesExist && notesExist);
        }

        public void button_Click(object sender, EventArgs e)
        {
            if (mouseClick)
            {
                SoundPlayer(theme + "button_click.wav");
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

            if (profileSettings.Visible)
            {
                return;
            }

            if (!settings.Visible)
            {            
                settings.BringToFront();
                settings.Visible = true;
            }
            else
            {
                settings.Visible = false;
            }
        }

        private void gameProfilesList_btn_Click(object sender, EventArgs e)
        {
            if (settings.Visible)
            {
                return;
            }

            if (GameProfile.profilesPathList.Count == 0)
            {
                setupScreen.gameProfilesList.Visible = false;
                setupScreen.gameProfilesList_btn.Image = ImageCache.GetImage(theme + "profiles_list.png");
                return;
            }

            if (setupScreen.gameProfilesList.Visible)
            {
                setupScreen.gameProfilesList.Visible = false;
                setupScreen.gameProfilesList_btn.Image = ImageCache.GetImage(theme + "profiles_list.png");
            }
            else
            {
                setupScreen.gameProfilesList.Visible = true;
                setupScreen.gameProfilesList_btn.Image = ImageCache.GetImage(theme + "profiles_list_opened.png");
            }
        }

        public void ProfileSettings_btn_Click(object sender, EventArgs e)
        {
            if (settings.Visible)
            {
                return;
            }

            if (!profileSettings.Visible)
            {
                profileSettings.BringToFront();
                profileSettings.Visible = true;
                ProfilesList.profilesList.Locked = true;
                ProfileSettings.UpdateProfileSettingsUiValues();
            }
        }

        private void minimizeButtonClick(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void maximizeButtonClick(object sender, EventArgs e)
        {
            WindowState = WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
        }

        private void MainForm_ClientSizeChanged(object sender, EventArgs e)
        {
            Invalidate();

            if (roundedcorners)
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

            if (profileSettings != null) profileSettings.Visible = false;
            if (settings != null) settings.Visible = false;
            if (searchDisksForm != null) searchDisksForm.Visible = false;

            if (setupScreen != null && I_GameHandler == null)
            {
                setupScreen.textZoomContainer.Visible = false;
                btn_magnifier.Image = ImageCache.GetImage(theme + "magnifier.png");

                GameProfile._GameProfile?.Reset();
                if (stepsList != null)
                {
                    GoToStep(0);
                }

                if (ProfilesList.profilesList != null)
                {
                    ProfilesList.profilesList.Locked = false;
                }
            }         
        }

        private void MainForm_ResizeBegin(object sender, EventArgs e)
        {
            foreach (Control titleBarButtons in Controls)
            {
                titleBarButtons.Visible = false;
            }

            btn_Links.BackgroundImage = ImageCache.GetImage(theme + "title_dropdown_closed.png");
            clientAreaPanel.Visible = false;
            Opacity = 0.6D;
        }

        private void MainForm_ResizeEnd(object sender, EventArgs e)
        {
            foreach (Control titleBarButtons in Controls)
            {
                titleBarButtons.Visible = titleBarButtons.Name != "third_party_tools_container" && titleBarButtons.Name != "linksPanel";
            }

            if (connected || DisableOfflineIcon) { btn_noHub.Visible = false; }

            clientAreaPanel.Visible = true;

            Opacity = 1.0D;
        }

        private void closeButtonClick(object sender, EventArgs e)
        {
            FadeOut();       
        }

        private void keepInstancesFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentGameInfo.KeepSymLink)
            {
                currentGameInfo.KeepSymLink = false;
                gameContextMenuStrip.Items[20].Image = ImageCache.GetImage(theme + "locked.png");
            }
            else
            {
                currentGameInfo.KeepSymLink = true;
                gameContextMenuStrip.Items[20].Image = ImageCache.GetImage(theme + "unlocked.png");
            }

            GameManager.Instance.SaveUserProfile();
        }

        private void SaveNucleusWindowPosAndLoc()
        {
            if(Location.X == -32000 || Width == 0)
            {
                return;
            }

            ini.IniWriteValue("Misc", "WindowSize", Width + "X" + Height);
            ini.IniWriteValue("Misc", "WindowLocation", Location.X + "X" + Location.Y);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) => SaveNucleusWindowPosAndLoc();

        private void stepPanelPictureBox_DoubleClick(object sender, EventArgs e)
        {
            if (connected && hubShowcase == null)
            {
                hubShowcase = new HubShowcase(this);
                hubShowcase.Size = new Size(clientAreaPanel.Width - game_listSizer.Width, hubShowcase.Height);
                hubShowcase.Location = new Point(game_listSizer.Right, mainButtonFrame.Bottom + (clientAreaPanel.Height / 2 - hubShowcase.Height / 2));

                stepPanelPictureBox.Visible = false;
                clientAreaPanel.Controls.Add(hubShowcase);
            }
        }

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;       

        private void mainButtonFrame_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                User32Interop.ReleaseCapture();
                IntPtr nucHwnd = User32Interop.FindWindow(null, Text);
                User32Interop.SendMessage(nucHwnd, WM_NCLBUTTONDOWN,(IntPtr) HT_CAPTION, (IntPtr)0);
            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            int edgingHeight = clientAreaPanel.Location.Y; 
            Rectangle top = new Rectangle(0,0,Width, edgingHeight);
            Rectangle bottom = new Rectangle(0, Height - edgingHeight, Width, edgingHeight);

            Color edgingColor = mainButtonFrame.Enabled ? TitleBarColor : Color.Red;

            LinearGradientBrush linearGradientBrush =
            new LinearGradientBrush(top, Color.Transparent, edgingColor, 0F);

            ColorBlend cblend = new ColorBlend(3);

            cblend.Colors = new Color[3] { Color.Transparent, edgingColor, Color.Transparent };
            cblend.Positions = new float[3] { 0f, 0.5f, 1f };

            linearGradientBrush.InterpolationColors = cblend;

            e.Graphics.FillRectangle(linearGradientBrush, top);
            e.Graphics.FillRectangle(linearGradientBrush, bottom);        
        }

    }
}
