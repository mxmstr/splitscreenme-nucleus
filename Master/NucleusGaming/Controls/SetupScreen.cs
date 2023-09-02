using Jint.Native.Function;
using Nucleus.Gaming;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Controls;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.InputManagement;
using Nucleus.Gaming.Forms.NucleusMessageBox;
using Nucleus.Gaming.Properties;
using SharpDX.DirectInput;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;


namespace Nucleus.Coop
{
    public class SetupScreen : UserInputControl, IDynamicSized
    {
        private readonly IniFile themeIni = Globals.ThemeIni;
        private string theme = Globals.Theme;
        private static SetupScreen _setupScreen;

        // array of users's screens
        public static UserScreen[] screens;

        private Font playerFont;
        private Font playerCustomFont;
        private Font smallTextFont;
        private Font playerTextFont;
        private Font extraSmallTextFont;
        private Font tinyTextFont;

        // the total bounds of all the connected monitors together
        private Rectangle totalBounds;
        private RectangleF screensArea;
        private RectangleF playersArea;
        private Rectangle setupScr;
        private Rectangle draggingScreenIndicator;
        private Rectangle destEditBounds;
        private Rectangle destMonitorBounds;

        private int playerSize;
        private int draggingIndex = -1;
        private int draggingScreen = -1;
        public int gamePadPressed = -1;
        private int testDinputPlayers = -1;// 16;
        private int testXinputPlayers = -1;// 16;
        private int TotalPlayers = 0;//K&m player will count as one only if 2 devices(keyboard & mouse) has the same bounds.
        private int destScreenIndex = -1;

        private bool canProceed;
        private bool controllerIdentification;
        private bool dragging = false;
        public bool isDisconnected;
        private bool insideGamepadTick = false;
        private bool UseSetupScreenBorder;
        private bool UseLayoutSelectionBorder;
        private bool UseSetupScreenImage;
        private bool scaled = false;
        public override bool CanProceed => canProceed;
        public override bool CanPlay => false;
        private bool profileDisabled;
        private bool showError = false;
        private bool autoFill;
        private bool expandedKM;
        private bool showIndicator;

        private bool useGamepadApiIndex;
        public bool UseXinputIndex
        {
            get => useGamepadApiIndex;
            set
            {
                useGamepadApiIndex = value;
                if (profile != null)
                {
                    profile.Reset();
                    profile.PlayersList.Clear();
                }
            }
        }

        private Point draggingOffset;
        private Point mousePos;

        private Rectangle draggingScreenRec;
        private Rectangle draggingScreenBounds;

        // the factor to scale all screens to fit them inside the edit area
        private float screensAreaScale;
        private float newplayerCustomFontSize;
        private float scale;


        public System.Threading.Timer gamepadTimer;
        public System.Threading.Timer vibrationTimer;

        public Button btn_Play;
        public PictureBox instruction_btn;
        private PictureBox closeZoomBtn;
        private PictureBox instructionImg;
        public PictureBox profileSettings_btn;
        public PictureBox gameProfilesList_btn;
        public ProfilesList gameProfilesList;
        private ImageAttributes flashImageAttributes;

        private Bitmap instructionCloseImg;
        private Bitmap instructionOpenImg;
        private Bitmap xinputPic;
        private Bitmap dinputPic;
        private Bitmap keyboardPic;
        private Bitmap protoKeyboardPic;
        private Bitmap protoMousePic;
        private Bitmap virtualKeyboardPic;
        private Bitmap virtualMousePic;
        private Bitmap screenimg;
        private Bitmap draggingScreenImg;
        private Bitmap fullscreen;
        private Bitmap horizontal2;
        private Bitmap vertical2;
        private Bitmap players4;
        private Bitmap players6;
        private Bitmap players8;
        private Bitmap players16;
        private Bitmap customLayout;
        private Bitmap plyrsSettingsIcon;

        private Brush[] colors;

        private SolidBrush myBrush;
        private SolidBrush notEnoughPlyrsSBrush;
        private SolidBrush tagBrush;

        private Pen PositionPlayerScreenPen;
        private Pen PositionScreenPen;
        private Pen ghostBoundsPen;
        private Pen availBoundsPen;
        private Pen destEditBoundsPen;

        private Cursor hand_Cursor;
        private Cursor default_Cursor;

        public RichTextBox handlerNoteZoom;
        public Panel textZoomContainer;

        private ToolTip gameProfilesList_btnTooltip;
        public ToolTip profileSettings_Tooltip;

        public List<PlayerInfo> loadedProfilePlayers = new List<PlayerInfo>();       
        //second device (keyboard or mouse) is added to an other list(devicesToMerge) so we can count players more efficiently.
        //devicesToMerge & loadedProfilePlayers are merged in GameProfile.
        public List<PlayerInfo> devicesToMerge = new List<PlayerInfo>();

        /// <summary>
        /// availableBounds: 
        /// item1 => all available MonitorBounds, item2 => all available editorBounds
        /// </summary>
        private Dictionary<Rectangle, Rectangle> availableBounds = new Dictionary<Rectangle, Rectangle>();

        private string customFont;
        public override string Title => "Position Players";

        public SetupScreen()
        {
            SetupScreen._setupScreen = this;
            Initialize();
        }

        private void Initialize()
        {
            Name = "setupScreen";

            string[] rgb_PositionControlsFontColor = themeIni.IniReadValue("Colors", "SetupScreenFont").Split(',');
            string[] rgb_PositionScreenColor = themeIni.IniReadValue("Colors", "SetupScreenBorder").Split(',');
            string[] rgb_PositionPlayerScreenColor = themeIni.IniReadValue("Colors", "SetupScreenPlayerBorder").Split(',');

            controllerIdentification = bool.Parse(themeIni.IniReadValue("Misc", "ControllerIdentificationOn"));
            UseSetupScreenBorder = bool.Parse(themeIni.IniReadValue("Misc", "UseSetupScreenBorder"));
            UseLayoutSelectionBorder = bool.Parse(themeIni.IniReadValue("Misc", "UseLayoutSelectionBorder"));
            UseSetupScreenImage = bool.Parse(themeIni.IniReadValue("Misc", "UseSetupScreenImage"));
            customFont = themeIni.IniReadValue("Font", "FontFamily");
            useGamepadApiIndex = bool.Parse(Globals.ini.IniReadValue("Dev", "UseXinputIndex"));

            if (gamepadTimer == null)
            {
                gamepadTimer = new System.Threading.Timer(GamepadTimer_Tick, null, 0, 500);
            }

            SuspendLayout();

            default_Cursor = new Cursor(theme + "cursor.ico");
            Cursor = default_Cursor;
            hand_Cursor = new Cursor(theme + "cursor_hand.ico");

            Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            playerFont = new Font(customFont, 20.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            playerCustomFont = new Font(customFont, 16.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            playerTextFont = new Font(customFont, 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            smallTextFont = new Font(customFont, 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            extraSmallTextFont = new Font(customFont, 10.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            tinyTextFont = new Font(customFont, 8.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            PositionScreenPen = new Pen(Color.FromArgb(int.Parse(rgb_PositionScreenColor[0]), int.Parse(rgb_PositionScreenColor[1]), int.Parse(rgb_PositionScreenColor[2])));
            PositionPlayerScreenPen = new Pen(Color.FromArgb(int.Parse(rgb_PositionPlayerScreenColor[0]), int.Parse(rgb_PositionPlayerScreenColor[1]), int.Parse(rgb_PositionPlayerScreenColor[2])));
            myBrush = new SolidBrush(Color.FromArgb(int.Parse(rgb_PositionControlsFontColor[0]), int.Parse(rgb_PositionControlsFontColor[1]), int.Parse(rgb_PositionControlsFontColor[2])));
            tagBrush = new SolidBrush(Color.FromArgb(200, 0, 0, 0));
            ghostBoundsPen = new Pen(Color.Red);

            BackColor = Color.Transparent;

            instructionCloseImg = ImageCache.GetImage(theme + "instruction_closed.png");
            instructionOpenImg = ImageCache.GetImage(theme + "instruction_opened.png");
            xinputPic = ImageCache.GetImage(theme + "xinput.png");
            dinputPic = ImageCache.GetImage(theme + "dinput.png");
            keyboardPic = ImageCache.GetImage(theme + "keyboard.png");
            protoKeyboardPic = ImageCache.GetImage(theme + "proto_keyboard.png");
            protoMousePic = ImageCache.GetImage(theme + "proto_mouse.png");
            virtualKeyboardPic = ImageCache.GetImage(theme + "virtual_keyboard.png");
            virtualMousePic = ImageCache.GetImage(theme + "virtual_mouse.png");
            screenimg = ImageCache.GetImage(theme + "screen.png");
            draggingScreenImg = ImageCache.GetImage(theme + "dragging_indicator.png");
            fullscreen = ImageCache.GetImage(theme + "fullscreen.png");
            horizontal2 = ImageCache.GetImage(theme + "2horizontal.png");
            vertical2 = ImageCache.GetImage(theme + "2vertical.png");
            players4 = ImageCache.GetImage(theme + "4players.png");
            players6 = ImageCache.GetImage(theme + "6players.png");
            players8 = ImageCache.GetImage(theme + "8players.png");
            players16 = ImageCache.GetImage(theme + "16players.png");
            customLayout = ImageCache.GetImage(theme + "customLayout.png");
            plyrsSettingsIcon = ImageCache.GetImage(theme + "profile_settings.png");

            instruction_btn = new PictureBox();//using a button cause focus issues
            instruction_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            instruction_btn.Size = new Size(25, 25);
            instruction_btn.Location = new Point((Width - instruction_btn.Width) - 5, 5);
            instruction_btn.Font = playerFont;
            instruction_btn.BackColor = Color.Transparent;
            instruction_btn.ForeColor = Color.White;
            instruction_btn.BackgroundImage = instructionCloseImg;
            instruction_btn.BackgroundImageLayout = ImageLayout.Stretch;
            instruction_btn.Cursor = hand_Cursor;
            instruction_btn.Click += new EventHandler(this.instruction_Click);
            CustomToolTips.SetToolTip(instruction_btn, "How to setup players.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });

            instructionImg = new PictureBox()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.Black,
                Image = Resources.instructions,
                BackgroundImageLayout = ImageLayout.Stretch,
                Cursor = hand_Cursor,
                ///Size\Location  see => UpdateScreens() 
                SizeMode = PictureBoxSizeMode.StretchImage,
                Visible = false
            };

            handlerNoteZoom = new RichTextBox();
            handlerNoteZoom.Name = "handlerNoteZoom";
            handlerNoteZoom.Visible = true;
            handlerNoteZoom.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            handlerNoteZoom.BorderStyle = BorderStyle.None;
            handlerNoteZoom.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            handlerNoteZoom.ReadOnly = true;
            handlerNoteZoom.WordWrap = true;
            handlerNoteZoom.LinkClicked += new LinkClickedEventHandler(handlerNoteZoom_LinkClicked);
            handlerNoteZoom.Text = "";

            closeZoomBtn = new PictureBox
            {
                Name = "closeZoomBtn",
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Image = ImageCache.GetImage(theme + "title_close.png"),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
                ///Size\Location  see => UpdateScreens() 
            };


            closeZoomBtn.MouseEnter += new EventHandler(CloseZoomBtn_MouseEnter);
            closeZoomBtn.MouseLeave += new EventHandler(CloseZoomBtn_MouseLeave);
            closeZoomBtn.Click += new EventHandler(CloseZoomBtn_Click);

            textZoomContainer = new Panel();
            textZoomContainer.Visible = false;
            textZoomContainer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            textZoomContainer.BorderStyle = BorderStyle.None;
            textZoomContainer.BackgroundImageLayout = ImageLayout.Stretch;
            textZoomContainer.BackColor = Color.FromArgb(255, 20, 20, 20);
            textZoomContainer.Controls.Add(handlerNoteZoom);
            textZoomContainer.Controls.Add(closeZoomBtn);

            profileSettings_btn = new PictureBox();///using a button cause focus issues
            profileSettings_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            profileSettings_btn.BackColor = Color.Transparent;
            profileSettings_btn.Image = plyrsSettingsIcon;
            profileSettings_btn.SizeMode = PictureBoxSizeMode.StretchImage;
            profileSettings_btn.Cursor = hand_Cursor;
            profileSettings_btn.Font = instruction_btn.Font;
            profileSettings_btn.Visible = false;

            gameProfilesList_btn = new PictureBox();///using a button cause focus issues
            gameProfilesList_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            gameProfilesList_btn.AutoSize = false;
            gameProfilesList_btn.SizeMode = PictureBoxSizeMode.StretchImage;
            gameProfilesList_btn.BackColor = Color.Transparent;
            gameProfilesList_btn.Image = ImageCache.GetImage(theme + "profiles_list.png");
            gameProfilesList_btn.Text = "Profiles List";
            gameProfilesList_btn.Cursor = hand_Cursor;
            gameProfilesList_btn.Visible = false;

            gameProfilesList = new ProfilesList(this);

            ResumeLayout();

            notEnoughPlyrsSBrush = new SolidBrush(Color.FromArgb(255, 245, 4, 68));
            availBoundsPen = new Pen(Color.FromArgb(255, 80, 240, 20));
            destEditBoundsPen = new Pen(Color.FromArgb(255, 255, 255, 102));

            colors = new Brush[]
            {
              Brushes.Red, Brushes.DodgerBlue, Brushes.LimeGreen, Brushes.Yellow,Brushes.SaddleBrown, Brushes.BlueViolet, Brushes.Aqua, Brushes.DarkOrange, Brushes.Silver,
              Brushes.Magenta, Brushes.SpringGreen, Brushes.Indigo, Brushes.Black, Brushes.White, Brushes.Bisque, Brushes.SkyBlue, Brushes.SeaGreen,Brushes.Wheat, Brushes.Crimson, Brushes.Turquoise, Brushes.Chocolate,
              Brushes.OrangeRed, Brushes.Olive, Brushes.DarkRed, Brushes.Lavender
            };

            Controls.Add(textZoomContainer);
            Controls.Add(instruction_btn);
            Controls.Add(profileSettings_btn);
            Controls.Add(gameProfilesList_btn);
            Controls.Add(gameProfilesList);
            Controls.Add(instructionImg);

            ///Flash image attributes
            {
                ColorMatrix colorMatrix = new ColorMatrix(new[]
                {
                    new float[] {1, 0, 0, 0, 0},
                    new float[] {0, 0, 0, 0, 0},
                    new float[] {0, 0, 0, 0, 0},
                    new float[] {0, 1, 0, 1, 0},
                    new float[] {0.4f, 0, 0, 0, 1}
                });

                flashImageAttributes = new ImageAttributes();
                flashImageAttributes.SetColorMatrix(colorMatrix);
            }

            DPIManager.Register(this);
            DPIManager.Update(this);
            RemoveFlicker();
        }

        private void CloseZoomBtn_MouseEnter(object sender, EventArgs e) => closeZoomBtn.Image = ImageCache.GetImage(theme + "title_close_mousehover.png");

        private void CloseZoomBtn_MouseLeave(object sender, EventArgs e) => closeZoomBtn.Image = ImageCache.GetImage(theme + "title_close.png");

        private void CloseZoomBtn_Click(object sender, EventArgs e)
        {
            textZoomContainer.Visible = false;
            PictureBox btn_magnifier = TopLevelControl.Controls.Find("btn_magnifier", true)[0] as PictureBox;
            btn_magnifier.Image = ImageCache.GetImage(theme + "magnifier.png");
        }

        public override void Initialize(UserGameInfo game, GameProfile profile)
        {
            base.Initialize(game, profile);

            ClearDInputDevicesList();

            gamepadTimer = new System.Threading.Timer(GamepadTimer_Tick, null, 0, 500);

            profileDisabled = bool.Parse(Globals.ini.IniReadValue("Misc", "DisableGameProfiles"));

            if (!profileDisabled)
            {
                gameProfilesList_btnTooltip = CustomToolTips.SetToolTip(gameProfilesList_btn, $"{GameProfile.Game.GameName} profiles list.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
            }

            UpdatePlayers();
        }

        private void handlerNoteZoom_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void instruction_Click(object sender, EventArgs e)
        {
            if (instructionImg.Visible)
            {
                SuspendLayout();
                instruction_btn.BackgroundImage = instructionCloseImg;
                ResumeLayout();
                instructionImg.Visible = false;
            }
            else
            {
                SuspendLayout();
                instruction_btn.BackgroundImage = instructionOpenImg;
                ResumeLayout();
                instructionImg.Visible = true;
                instructionImg.BringToFront();
            }
        }

        public void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }

            this.scale = scale;

            SuspendLayout();

            if (!scaled)
            {
                newplayerCustomFontSize = playerCustomFont.Size;
                handlerNoteZoom.Font = new Font(customFont, 14f * scale, FontStyle.Regular, GraphicsUnit.Pixel, 0);

                float instructionW = instruction_btn.Width * scale;
                float instructionH = instruction_btn.Height * scale;

                instruction_btn.Width = (int)instructionW;
                instruction_btn.Height = (int)instructionH;
                instruction_btn.Location = new Point((Width - instruction_btn.Width), 5);

                textZoomContainer.Size = new Size(Width - (int)(60 * scale), Height - (int)(50 * scale));
                textZoomContainer.Location = new Point(Width / 2 - textZoomContainer.Width / 2, instruction_btn.Height + (int)(10 * scale));


                handlerNoteZoom.Size = new Size(textZoomContainer.Width + (int)(17 * scale), textZoomContainer.Height - 40);
                handlerNoteZoom.Location = new Point(0, 20);

                profileSettings_btn.Size = instruction_btn.Size;
                profileSettings_btn.Location = new Point(((instruction_btn.Left - profileSettings_btn.Width) - 3), instruction_btn.Top);

                gameProfilesList_btn.Size = instruction_btn.Size;
                gameProfilesList_btn.Location = new Point(profileSettings_btn.Left - gameProfilesList_btn.Width - 3, instruction_btn.Location.Y);
                gameProfilesList.UpdateSize(scale);

                closeZoomBtn.Size = new Size(18, 18);
                closeZoomBtn.Location = new Point(textZoomContainer.Width - (closeZoomBtn.Width + 4), 1);

                scaled = true;
            }

            ResumeLayout();
        }

        public static void InvalidateFlash()
        {
            _setupScreen?.Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (profile != null && profile.PlayersList != null)
            {
                List<PlayerInfo> data = profile.PlayersList;

                foreach (PlayerInfo player in data)
                {
                    if (player.DInputJoystick != null)
                    {
                        player.DInputJoystick.Dispose();
                    }
                }
            }
        }

        public override void Ended()
        {
            base.Ended();

            List<PlayerInfo> data = profile.PlayersList;

            foreach (PlayerInfo player in data)
            {
                player.DInputJoystick?.Dispose();
            }

            ClearDInputDevicesList();

            gamepadTimer.Dispose();
        }

        private void ClearDInputDevicesList()
        {
            foreach (Joystick joystick in JoyStickList)
            {
                if (!joystick.IsDisposed)
                {
                    joystick.Dispose();
                }
            }

            JoyStickList.Clear();
        }

        private bool PollDInputGamepad(PlayerInfo player)
        {
            if (player.DInputJoystick == null)
            {
                return false;
            }

            if ((bool)player.DInputJoystick?.IsDisposed)
            {
                player.DInputJoystick = null;
                return false;
            }

            try
            {
                if ((bool)player.DInputJoystick?.GetCurrentState().Buttons.Any(b => b == true))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                player.DInputJoystick.Dispose();
                player.DInputJoystick = null;
                return false;
            }

            return false;
        }

        private void Vibration_Tick(object state)
        {
            if (profile == null)
            {
                return;
            }

            foreach (PlayerInfo player in profile.PlayersList)
            {
                if (!player.IsXInput)
                {
                    continue;
                }

                if (player.XInputJoystick.IsConnected)
                {
                    Vibration vibration = new Vibration
                    {
                        RightMotorSpeed = (ushort)0,
                        LeftMotorSpeed = (ushort)0
                    };

                    player.XInputJoystick.SetVibration(vibration);
                }
            }

            vibrationTimer.Dispose();
        }

        private void Vibrate(PlayerInfo player)
        {
            if (!player.Vibrate)
            {
                Vibration vibration = new Vibration
                {
                    RightMotorSpeed = (ushort)65535,///make it full strenght because controllers can have different sensitivity
                    LeftMotorSpeed = (ushort)65535///make it full strenght because controllers can have different sensitivity
                };

                player.XInputJoystick.SetVibration(vibration);
                vibrationTimer = new System.Threading.Timer(Vibration_Tick, null, 90, 0);
                player.Vibrate = true;
            }
        }

        private bool polling = false;

        private bool PollXInputGamepad(PlayerInfo player)
        {
            if (polling || player.IsFake)
            {
                return false;
            }

            try
            {
                if (!player.XInputJoystick.IsConnected)
                {
                    return false;
                }

                if (player.XInputJoystick.GetState().Gamepad.Buttons != 0)
                {
                    if (useGamepadApiIndex)
                    {
                        Vibrate(player);
                        return true;
                    }

                    if (player.DInputJoystick == null)
                    {
                        int pressedCount = 0;

                        Joystick joystick = null;

                        for (int i = 0; i < JoyStickList.Count; i++)
                        {
                            joystick = JoyStickList[i];

                            if (joystick.GetCurrentState().Buttons.Any(b => b == true))
                            {
                                player.DInputJoystick = joystick;
                                ++pressedCount;
                            }
                        }

                        if (pressedCount > 1)
                        {
                            player.DInputJoystick.Dispose();
                            player.DInputJoystick = null;

                            ClearDInputDevicesList();//Clear all beacause if one is broken all others could potentialy be too.

                            return false;
                        }

                        if (pressedCount == 0)//if 0 the device does not return any state so it's a "broken" one.
                        {
                            ClearDInputDevicesList();

                            return false;
                        }

                        if (player.DInputJoystick != null)
                        {
                            player.GamepadGuid = player.DInputJoystick.Information.InstanceGuid;
                            player.GamepadProductGuid = player.DInputJoystick.Information.ProductGuid;
                            player.GamepadName = player.DInputJoystick.Information.InstanceName;
                            string hid = player.DInputJoystick.Properties.InterfacePath;
                            player.RawHID = hid;
                            int start = hid.IndexOf("hid#");
                            int end = hid.LastIndexOf("#{");
                            string fhid = hid.Substring(start, end - start).Replace('#', '\\').ToUpper();
                            player.HIDDeviceID = new string[] { fhid, "" };
                            player.IsInputUsed = true;
                            Vibrate(player);
                        }
                    }

                    if (player.DInputJoystick != null)
                    {
                        return true;
                    }

                    return false;
                }

            }
            catch (Exception)
            {
                ///Some wireless devices can disconnect even with the receiver plugged which mess everything up           
                profile.Reset();

                ClearDInputDevicesList();

                UpdatePlayers();

                Console.WriteLine("Something went wrong with one or more DInput device(s)");

                return false;
            }

            return false;
        }

        private List<Joystick> JoyStickList = new List<Joystick>();

        public void GamepadTimer_Tick(object state)
        {
            if (insideGamepadTick || profile == null)
            {
                return;
            }

            List<PlayerInfo> data = profile.PlayersList;

            try
            {
                insideGamepadTick = true;

                bool changed = false;

                GenericGameInfo g = game.Game;

                DirectInput dinput = new DirectInput();

                if (g.Hook.DInputEnabled || g.Hook.XInputReroute || g.ProtoInput.DinputDeviceHook)
                {
                    IList<DeviceInstance> devices = dinput.GetDevices(DeviceClass.GameControl, DeviceEnumerationFlags.AllDevices);

                    // first search for disconnected gamepads
                    for (int j = 0; j < data.Count; j++)
                    {
                        PlayerInfo p = data[j];

                        if (!p.IsDInput || p.IsFake)
                        {
                            continue;
                        }

                        bool foundGamepad = false;

                        for (int i = 0; i < devices.Count; i++)
                        {
                            if (devices[i].InstanceGuid == p.GamepadGuid && i == data[j].GamepadId)
                            {
                                foundGamepad = true;
                                break;
                            }
                        }

                        if (!foundGamepad)
                        {
                            if (data[j].DInputJoystick != null)
                            {
                                data[j].DInputJoystick.Unacquire();
                                data[j].DInputJoystick.Dispose();
                            }

                            changed = true;

                            if (loadedProfilePlayers.Contains(data[j]))
                            {
                                screens[data[j].ScreenIndex].PlayerOnScreen--;
                                TotalPlayers--;
                                loadedProfilePlayers.Remove(data[j]);
                            }

                            data.RemoveAt(j);
                            j--;
                            isDisconnected = true;
                        }
                    }

                    for (int i = 0; i < devices.Count; i++)
                    {
                        bool already = false;

                        // see if this gamepad is already on a player
                        for (int j = 0; j < data.Count; j++)
                        {
                            PlayerInfo p = data[j];

                            if (p.GamepadGuid == devices[i].InstanceGuid)
                            {
                                already = true;
                                break;
                            }
                        }

                        if (already)
                        {
                            continue;
                        }

                        changed = true;

                        // new gamepad
                        PlayerInfo player = new PlayerInfo
                        {
                            DInputJoystick = new Joystick(dinput, devices[i].InstanceGuid)
                        };

                        if (player.DInputJoystick.Properties.InterfacePath.ToUpper().Contains("IG_") && !g.Hook.XInputReroute && g.Hook.XInputEnabled)
                        {
                            continue;
                        }

                        player.GamepadProductGuid = devices[i].ProductGuid;
                        player.GamepadGuid = devices[i].InstanceGuid;
                        player.GamepadName = devices[i].InstanceName;
                        player.IsDInput = true;
                        player.IsController = true;
                        player.GamepadId = i;
                        string hid = player.DInputJoystick.Properties.InterfacePath;
                        player.RawHID = hid;
                        int start = hid.IndexOf("hid#");
                        int end = hid.LastIndexOf("#{");
                        string fhid = hid.Substring(start, end - start).Replace('#', '\\').ToUpper();
                        player.HIDDeviceID = new string[] { fhid, "" };
                        player.DInputJoystick.Acquire();
                        player.IsInputUsed = true;

                        data.Add(player);
                    }
                }

                ///Create a list of all DInput Joysticks available (Only used to grab gamepads hardware informations for XInput devices when polling)                
                IList<DeviceInstance> dInputList = dinput.GetDevices(SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AttachedOnly);

                for (int x = 0; x < dInputList.Count; x++)
                {
                    DeviceInstance device = dInputList[x];

                    if (JoyStickList.All(j => j.Information.InstanceGuid != device.InstanceGuid))
                    {
                        Joystick joy = new Joystick(dinput, device.InstanceGuid);
                        joy.Acquire();
                        //joy.Disposed += new EventHandler<EventArgs>(DisposeJoystick);
                        JoyStickList.Add(joy);
                    }
                }

                ///Using OpenXinput with more than 4 players means we can use more than 4 xinput controllers
                if ((g.Hook.XInputEnabled && !g.Hook.XInputReroute && !g.ProtoInput.DinputDeviceHook) || g.ProtoInput.XinputHook)
                {
                    for (int j = 0; j < data.Count; j++)
                    {
                        PlayerInfo p = data[j];

                        if (p.IsXInput && !p.IsFake)
                        {
                            OpenXinputController c = new OpenXinputController(g.ProtoInput.UseOpenXinput, p.GamepadId);

                            if (!c.IsConnected)
                            {
                                if (data[j].DInputJoystick != null)
                                {
                                    JoyStickList.Remove(data[j].DInputJoystick);
                                    data[j].DInputJoystick.Dispose();
                                }

                                changed = true;

                                if (loadedProfilePlayers.Contains(data[j]))
                                {
                                    loadedProfilePlayers.Remove(data[j]);
                                    screens[data[j].ScreenIndex].PlayerOnScreen--;
                                    TotalPlayers--;
                                }

                                data.RemoveAt(j);
                                j--;
                                isDisconnected = true;
                            }
                        }
                    }

                    int numControllers = g.ProtoInput.UseOpenXinput ? 32 : 4;
                    for (int i = 0; i < numControllers; i++)
                    {
                        OpenXinputController c = new OpenXinputController(g.ProtoInput.UseOpenXinput, i);

                        bool already = false;

                        if (!c.IsConnected)
                        {
                            continue;
                        }
                        else
                        {
                            ///Check if this gamepad is already assigned to a player
                            foreach (PlayerInfo p in data)
                            {
                                if (p.IsXInput && p.GamepadId == i)
                                {
                                    var s = c.GetState();

                                    int newmask = (int)s.Gamepad.Buttons;

                                    if (p.GamepadMask != newmask)
                                    {
                                        p.GamepadMask = newmask;
                                    }

                                    already = true;
                                    break;
                                }
                            }

                            if (already)
                            {
                                continue;
                            }

                            changed = true;

                            //new gamepad
                            PlayerInfo player = new PlayerInfo();

                            player.HIDDeviceID = new string[] { "Not required", "Not required" };
                            player.XInputJoystick = c;
                            player.IsXInput = true;
                            player.IsController = true;
                            player.GamepadId = i;

                            if (useGamepadApiIndex || profileDisabled)
                            {
                                player.GamepadGuid = new Guid($"00000000-0000-0000-0000-20000000000{player.GamepadId + 1}");
                                player.IsInputUsed = true;
                            }

                            data.Add(player);
                        }
                    }
                }

                if (changed)
                {
                    if (InvokeRequired)
                    {
                        Invoke(new MethodInvoker(UpdatePlayers));
                        Invoke(new MethodInvoker(Refresh));
                        insideGamepadTick = false;
                        return;
                    }
                }

                dinput?.Dispose();
                insideGamepadTick = false;
            }
            catch (Exception)
            {
                insideGamepadTick = false;
                return;
            }

            try
            {
                PlayerInfo brokenPlayer = null;

                foreach (PlayerInfo player in data)
                {
                    if (player.IsDInput)
                    {
                        if (player.DInputJoystick == null)
                        {
                            brokenPlayer = player;
                            continue;
                        }
                    }
                }

                if (brokenPlayer != null)
                {
                    if (loadedProfilePlayers.Contains(brokenPlayer))
                    {
                        loadedProfilePlayers.Remove(brokenPlayer);
                        screens[brokenPlayer.ScreenIndex].PlayerOnScreen--;
                        TotalPlayers--;
                    }

                    data.Remove(brokenPlayer);
                }
            }
            catch
            {
            }

            Invoke(new Action(() => Invalidate()));
        }

        public void UpdatePlayers()
        {
            if (profile == null)
            {
                return;
            }

            GenericGameInfo g = game.Game;
            List<PlayerInfo> playerData = profile.PlayersList;

            if (GameProfile.TotalPlayers == 0)
            {
                canProceed = playerData.Count(c => c.ScreenIndex != -1) >= 1;
            }
            else
            {
                canProceed = (TotalPlayers == GameProfile.TotalPlayers);
            }

            if (playerData.Count == 0)
            {
                if (game.Game.SupportsKeyboard)
                {
                    ///add keyboard data
                    PlayerInfo kbPlayer = new PlayerInfo
                    {
                        IsKeyboardPlayer = true,
                        GamepadId = 99,
                        IsInputUsed = true,
                        HIDDeviceID = new string[] { "Not required", "Not required" },
                        GamepadGuid = new Guid("10000000-1000-1000-1000-100000000000")
                    };

                    playerData.Add(kbPlayer);
                }

                if (game.Game.SupportsMultipleKeyboardsAndMice)///Raw mice/keyboards
                {
                    playerData.AddRange(RawInputManager.GetDeviceInputInfos());
                }

                if (testDinputPlayers != -1)///make fake data if needed
                {
                    for (int i = 0; i < testDinputPlayers; i++)
                    {
                        ///new gamepad
                        PlayerInfo player = new PlayerInfo
                        {
                            GamepadGuid = new Guid(),
                            GamepadName = "Player",
                            IsDInput = true,
                            IsFake = true,
                            IsInputUsed = true,
                            HIDDeviceID = new string[] { "Not required", "Not required" }
                        };

                        playerData.Add(player);
                    }
                }

                if (testXinputPlayers != -1)
                {
                    for (int i = 0; i < testXinputPlayers; i++)
                    {
                        PlayerInfo player = new PlayerInfo
                        {
                            GamepadGuid = new Guid(),
                            GamepadName = "XPlayer",
                            IsXInput = true,
                            IsController = true,
                            GamepadId = i,
                            IsFake = true,
                            IsInputUsed = true,
                            HIDDeviceID = new string[] { "Not required", "Not required" }
                        };

                        playerData.Add(player);
                    }
                }
            }

            UpdateScreens();

            float playersWidth = Width * 0.65f;

            float playerCount = playerData.Count;
            float playerWidth = playersWidth * 0.9f;
            float playerHeight = Height * 0.2f;
            playersArea = new RectangleF(10, 27, playersWidth, playerHeight);

            float playersAreaArea = playersArea.Width * playersArea.Height;
            float maxArea = playersAreaArea / playerCount;
            playerSize = (int)Math.Round(Math.Sqrt(maxArea) - 0.5);///force the round down
                                                                   ///see if the size can fit it or we need to make some further adjustments
            int horizontal = (int)Math.Round((playersWidth / playerSize) - 0.5);
            int vertical = (int)Math.Round((playerHeight / playerSize) - 0.5);
            int total = vertical * horizontal;

            if (total < playerCount)
            {
                int newVertical = vertical + 1;
                playerCustomFont = new Font("Franklin Gothic Medium", newplayerCustomFontSize * 0.8f, FontStyle.Regular, GraphicsUnit.Point, 0);
                playerSize = (int)Math.Round(((playerHeight / 1.2f) / newVertical));
            }

            List<PlayerInfo> reorder = playerData.OrderBy(player => player.IsKeyboardPlayer).ThenBy(player => player.IsRawMouse).ThenBy(player => player.IsInputUsed).ToList();

            for (int i = 0; i < reorder.Count; i++)
            {
                PlayerInfo info = reorder[i];

                if (info.ScreenIndex == -1)
                {
                    info.EditBounds = GetDefaultBounds(i);
                    info.SourceEditBounds = info.EditBounds;
                    info.DisplayIndex = -1;
                }

                Invalidate();
            }

            // Invalidate();
            CanPlayUpdated(canProceed, false);
        }

        private void UpdateScreens()
        {
            if (screens == null)
            {
                screens = ScreensUtil.AllScreens();
                totalBounds = RectangleUtil.Union(screens);
            }
            else
            {
                UserScreen[] newScreens = ScreensUtil.AllScreens();
                Rectangle newBounds = RectangleUtil.Union(newScreens);

                if (newBounds.Equals(totalBounds))
                {
                    return;
                }

                ///screens got updated, need to reflect in our window
                screens = newScreens;
                totalBounds = newBounds;

                ///remove all players screens
                List<PlayerInfo> playerData = profile.PlayersList;
                if (playerData != null)
                {
                    for (int i = 0; i < playerData.Count; i++)
                    {
                        PlayerInfo player = playerData[i];
                        player.EditBounds = GetDefaultBounds(draggingIndex);
                        player.ScreenIndex = -1;
                        player.DisplayIndex = -1;
                    }
                }
            }

            if (screens.Length > 1)
            {
                screensArea = new RectangleF(5, this.Height / 2.3f, Width - 20, Height / 2.1f);
            }
            else
            {
                screensArea = new RectangleF(10, 50 + Height * 0.2f + 10, Width - 20, Height * 0.5f);
            }

            screensAreaScale = screensArea.Width / (float)totalBounds.Width;

            if (totalBounds.Height * screensAreaScale > screensArea.Height)
            {
                screensAreaScale = (float)screensArea.Height / (float)totalBounds.Height;
            }

            Rectangle scaledBounds = RectangleUtil.Scale(totalBounds, screensAreaScale);
            scaledBounds.X = (int)screensArea.X;
            scaledBounds.Y = (int)screensArea.Y;

            int minY = 0;
            for (int i = 0; i < screens.Length; i++)
            {
                UserScreen screen = screens[i];
                screen.priority = screen.MonitorBounds.X + screen.MonitorBounds.Y;
                Rectangle bounds = RectangleUtil.Scale(screen.MonitorBounds, screensAreaScale);

                Rectangle uiBounds = new Rectangle(bounds.X, bounds.Y + scaledBounds.Y, bounds.Width, bounds.Height);
       
                screen.UIBounds = uiBounds;

                minY = Math.Min(minY, uiBounds.X);
            }

            ///remove negative monitors
            minY = -minY;
            for (int i = 0; i < screens.Length; i++)
            {
                UserScreen screen = screens[i];

                Rectangle uiBounds = screen.UIBounds;

                uiBounds.X += minY + scaledBounds.X;
                screen.UIBounds = uiBounds;
                screen.SwapTypeBounds = RectangleUtil.Float(uiBounds.X, uiBounds.Y, uiBounds.Width * 0.1f, uiBounds.Width * 0.1f);
            }

            float instWidth = screensArea.Width / 1.77f;
            instructionImg.Location = new Point((int)screensArea.X, (int)screensArea.Y / 4);
            instructionImg.Size = new Size((int)screensArea.Width, (int)instWidth);

            UpdatePlayers();
        }

        public void RefreshAll()
        {
            Refresh();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            totalBounds = Rectangle.Empty;
            Invalidate();
        }

        private bool GetScreenDivisionBounds(UserScreenType screenType, int index, out Rectangle? monitorBounds, out Rectangle? editorBounds, Rectangle bounds, Rectangle ebounds)
        {
            monitorBounds = null;
            editorBounds = null;

            bool Regular(int width, int height, out Rectangle? _monitorBounds, out Rectangle? _editorBounds)
            {
                int y = index % height;
                int x = (index - y) / height;

                int halfw = (int)(bounds.Width / (float)height);//2.1.2 screen assignation
                int halfh = (int)(bounds.Height / (float)width);//2.1.2 screen assignation

                _monitorBounds = new Rectangle(bounds.X + (halfw * y), bounds.Y + (halfh * x), halfw, halfh);//2.1.2 screen assignation
                int halfwe = (int)(ebounds.Width / (float)height);//2.1.2 screen assignation
                int halfhe = (int)(ebounds.Height / (float)width);//2.1.2 screen assignation
                _editorBounds = new Rectangle(ebounds.X + (halfwe * y), ebounds.Y + (halfhe * x), halfwe, halfhe);//2.1.2 screen assignation
              
                return true;
            }

            switch (screenType)
            {
                case UserScreenType.FullScreen:
                    {
                        if (index >= 1)
                        {
                            return false;
                        }

                        monitorBounds = bounds;
                        editorBounds = ebounds;
                        return true;
                    }
                case UserScreenType.DualHorizontal:
                    {
                        if (index >= 2)
                        {
                            return false;
                        }
                        return Regular(2, 1, out monitorBounds, out editorBounds);//2.1.2 screen assignation
                    }
                case UserScreenType.DualVertical:
                    {
                        if (index >= 2)
                        {
                            return false;
                        }
                        return Regular(1, 2, out monitorBounds, out editorBounds);//2.1.2 screen assignation
                    }
                case UserScreenType.FourPlayers:
                    {
                        if (index >= 4)
                        {
                            return false;
                        }
                        return Regular(2, 2, out monitorBounds, out editorBounds);
                    }
                case UserScreenType.SixPlayers:
                    {
                        if (index >= 6)
                        {
                            return false;
                        }
                        return Regular(2, 3, out monitorBounds, out editorBounds);
                    }
                case UserScreenType.EightPlayers:
                    {
                        if (index >= 8)
                        {
                            return false;
                        }
                        return Regular(2, 4, out monitorBounds, out editorBounds);
                    }
                case UserScreenType.SixteenPlayers:
                    {
                        if (index >= 16)
                        {
                            return false;
                        }
                        return Regular(4, 4, out monitorBounds, out editorBounds);
                    }
                case UserScreenType.Custom:
                    {
                        int horLines = GameProfile.CustomLayout_Hor;
                        int verLines = GameProfile.CustomLayout_Ver;
                        int maxPlayers = GameProfile.CustomLayout_Max;

                        horLines++;
                        verLines++;

                        if (index >= maxPlayers)
                        {
                            return false;
                        }
                        return Regular(horLines, verLines, out monitorBounds, out editorBounds);
                    }
            }

            return false;
        }
        
        private bool GetFreeSpace(int screenIndex, out Rectangle? editorBounds, out Rectangle? monitorBounds, PlayerInfo playerToInsert)
        {
            editorBounds = null;
            monitorBounds = null;

            List<PlayerInfo> players = profile.PlayersList;
            UserScreen screen = screens[screenIndex];
            Rectangle bounds = screen.MonitorBounds;
            Rectangle ebounds = screen.UIBounds;

            int index = -1;
            

            while (GetScreenDivisionBounds(screen.Type, ++index, out Rectangle? divMonitorBounds, out Rectangle? divEditorBounds, bounds, ebounds))
            {
                if (!availableBounds.Keys.Any(k => k == divMonitorBounds) && !autoFill)
                {
                    availableBounds.Add(divMonitorBounds.Value, divEditorBounds.Value);

                    if (divMonitorBounds.Value.Bottom == screen.MonitorBounds.Bottom)
                    {
                        screen.UIBounds = new Rectangle(screen.UIBounds.X,  screen.UIBounds.Y, screen.UIBounds.Width, screen.UIBounds.Height - (screen.UIBounds.Bottom - divEditorBounds.Value.Bottom));
                    }

                    if (divMonitorBounds.Value.Right == screen.MonitorBounds.Right)
                    {
                        screen.UIBounds = new Rectangle(screen.UIBounds.X, screen.UIBounds.Y, screen.UIBounds.Width - (screen.UIBounds.Right - divEditorBounds.Value.Right), screen.UIBounds.Height);
                    }

                    continue;
                }
               
                IEnumerable<PlayerInfo> playersInDiv;

                if (autoFill || GameProfile.Loaded)
                {
                    playersInDiv = players.Where(
                        x => x.ScreenIndex == screenIndex && (x.MonitorBounds == divMonitorBounds.Value || (x.MonitorBounds.Width == divMonitorBounds.Value.Width * 2 && x.MonitorBounds.Y == divMonitorBounds.Value.Y && x.MonitorBounds.X == divMonitorBounds.Value.X) || (x.MonitorBounds.Height == divMonitorBounds.Value.Height * 2 && x.MonitorBounds.X == divMonitorBounds.Value.X && x.MonitorBounds.Y == divMonitorBounds.Value.Y)));
                }
                else
                {
                    playersInDiv = players.Where(
                        x => x.ScreenIndex == screenIndex && (x.MonitorBounds == destMonitorBounds || (x.MonitorBounds.Width == destMonitorBounds.Width * 2 
                        && x.MonitorBounds.Y == destMonitorBounds.Y && x.MonitorBounds.X == destMonitorBounds.X) || (x.MonitorBounds.Height == destMonitorBounds.Height * 2 && x.MonitorBounds.X == destMonitorBounds.X && x.MonitorBounds.Y == destMonitorBounds.Y)) || x.MonitorBounds.Contains(destMonitorBounds) == true);
                }
               
                //Raw mice/keyboards can go as pairs, nothing else can.
                if (
                playerToInsert.IsRawMouse && !(playerToInsert.IsRawMouse && playerToInsert.IsRawKeyboard) ? playersInDiv.All(x => x.IsRawKeyboard && !x.IsRawMouse) :
                playerToInsert.IsRawKeyboard && !(playerToInsert.IsRawMouse && playerToInsert.IsRawKeyboard) ? playersInDiv.All(x => x.IsRawMouse && !x.IsRawKeyboard) :
                !playersInDiv.Any())
                {
                    if (GameProfile.Loaded)
                    {
                        //Check if these bounds corresponds to a profile player. 
                        ProfilePlayer profilePlayer = GameProfile.ProfilePlayersList.Where(ppl => TranslateBounds(ppl, ppl.ScreenIndex).Item1.Location == divMonitorBounds.Value.Location).FirstOrDefault();

                        if (TotalPlayers == GameProfile.TotalPlayers || profilePlayer == null)
                        {
                            continue;
                        }

                        var translatedBounds = TranslateBounds(profilePlayer, profilePlayer.ScreenIndex);

                        monitorBounds = translatedBounds.Item1;
                        editorBounds = translatedBounds.Item2;

                        return true;
                    }

                    PlayerInfo playerInbounds = players.Where(pl => (pl != playerToInsert) && pl.MonitorBounds.IntersectsWith(destMonitorBounds)).FirstOrDefault();

                    if(playerInbounds != null)
                    {
                        monitorBounds = playerInbounds.MonitorBounds;
                        editorBounds = playerInbounds.EditBounds;
                        expandedKM = true;
                        showIndicator = true;
                        return true;
                    }

                    monitorBounds = divMonitorBounds;
                    editorBounds = divEditorBounds;

                    if (autoFill)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private (Rectangle,Rectangle) TranslateBounds(ProfilePlayer profilePlayer, int screenIndex)
        {
            Rectangle ogScrUiBounds = GameProfile.AllScreens[profilePlayer.ScreenIndex];
            Rectangle ogEditBounds = profilePlayer.EditBounds;

            Vector2 ogScruiLoc = new Vector2(ogScrUiBounds.X, ogScrUiBounds.Y);///original screen ui location
            Vector2 ogpEb = new Vector2(ogEditBounds.X, ogEditBounds.Y);///original on ui screen player location(editbounds)
            Vector2 ogOnUIScrLoc = Vector2.Subtract(ogpEb, ogScruiLoc);///relative og ui player loc on og player ui screen

            float ratioEW = (float)ogScrUiBounds.Width / (float)screens[screenIndex].UIBounds.Width;
            float ratioEH = (float)ogScrUiBounds.Height / (float)screens[screenIndex].UIBounds.Height;
            Rectangle translatedEditBounds = new Rectangle(screens[screenIndex].UIBounds.X + (Convert.ToInt32(ogOnUIScrLoc.X / ratioEW)), screens[screenIndex].UIBounds.Y + (Convert.ToInt32(ogOnUIScrLoc.Y / ratioEH)),
                                              Convert.ToInt32(ogEditBounds.Width / ratioEW), Convert.ToInt32(ogEditBounds.Height / ratioEH));

            ///## Re-calcul & scale players monitor bounds if needed ##///
            Rectangle ogScr = profilePlayer.OwnerDisplay;
            Rectangle ogMb = profilePlayer.MonitorBounds;

            Vector2 ogscr = new Vector2(ogScr.X, ogScr.Y);///original player screen location
            Vector2 ogPMb = new Vector2(ogMb.X, ogMb.Y);///original on screen player location(monitorBounds)
            Vector2 VogOnScrLoc = Vector2.Subtract(ogPMb, ogscr);///relative og player loc on og player screen

            float ratioMW = (float)ogScr.Width / (float)screens[screenIndex].MonitorBounds.Width;
            float ratioMH = (float)ogScr.Height / (float)screens[screenIndex].MonitorBounds.Height;
            Rectangle translatedMonitorBounds = new Rectangle(screens[screenIndex].MonitorBounds.X + (Convert.ToInt32(VogOnScrLoc.X / ratioMW)), screens[screenIndex].MonitorBounds.Y + (Convert.ToInt32(VogOnScrLoc.Y / ratioMH)),
                                                 Convert.ToInt32(ogMb.Width / ratioMW), Convert.ToInt32(ogMb.Height / ratioMH));

            return (translatedMonitorBounds,translatedEditBounds);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if (dragging || GameProfile.Loaded)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                autoFill = true;
                // first count how many devices we have
                bool changed = false;
                for (int i = 0; i < screens.Length; i++)
                {
                    UserScreen screen = screens[i];

                    if (screen.UIBounds.Contains(e.Location) && !screen.SwapTypeBounds.Contains(e.Location))
                    {
                        List<PlayerInfo> playerData = profile.PlayersList;

                        // add all possible players!
                        for (int j = 0; j < playerData.Count; j++)
                        {
                            PlayerInfo player = playerData[j];

                            bool hasFreeSpace = GetFreeSpace(i, out Rectangle? editor, out Rectangle? monitor, player);

                            if (hasFreeSpace)
                            {
                                if (player.ScreenIndex == -1)
                                {                                
                                    changed = true;

                                    if ((player.IsRawKeyboard || player.IsRawMouse) && !(player.IsRawKeyboard && player.IsRawMouse))
                                    {
                                        if(profile.PlayersList.Any(pl => pl.MonitorBounds == monitor.Value))
                                        {                                           
                                            devicesToMerge.Add(player);                                          
                                        }
                                        else
                                        {
                                            loadedProfilePlayers.Add(player);
                                        }

                                        if (profile.PlayersList.Where(pp => pp.MonitorBounds == monitor.Value).Count() == 1)
                                        {
                                            screens[i].PlayerOnScreen++;
                                            TotalPlayers++;
                                        }
                                    }
                                    else
                                    {                                  
                                        loadedProfilePlayers.Add(player);
                                        screens[i].PlayerOnScreen++;
                                        TotalPlayers++;
                                    }
                                   

                                    player.IsInputUsed = true;
                                    player.Owner = screens[i];
                                    player.ScreenIndex = i;
                                    player.MonitorBounds = monitor.Value;
                                    player.EditBounds = editor.Value;
                                    player.ScreenPriority = screens[i].priority;
                                    player.DisplayIndex = screens[i].DisplayIndex;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        break;
                    }
                }

                if (changed)
                {
                    UpdatePlayers();
                }

                autoFill = false;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Cursor = hand_Cursor;

            List<PlayerInfo> players = profile.PlayersList;

            if (dragging)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < screens.Length; i++)
                {
                    UserScreen screen = screens[i];

                    if (screen.SwapTypeBounds.Contains(e.Location))
                    {                      
                        if (GameProfile.Loaded)
                        {
                            return;
                        }

                        if (screen.Type == UserScreenType.Custom)
                        {
                            screen.Type = 0;
                        }
                        else
                        {
                            screen.Type++;
                        }

                        ///invalidate all players inside screen
                        for (int j = 0; j < players.Count; j++)
                        {
                            ///return to default position
                            PlayerInfo p = players[j];

                            if (p.ScreenIndex == i)
                            {                                                                                         
                                if ((p.IsRawKeyboard || p.IsRawMouse) && !(p.IsRawKeyboard && p.IsRawMouse))
                                {
                                    if (loadedProfilePlayers.Contains(p))
                                    {
                                        loadedProfilePlayers.Remove(p);
                                    }
                                    else if (devicesToMerge.Contains(p))
                                    {                                     
                                        devicesToMerge.Remove(p);                                       
                                    }

                                    if(profile.PlayersList.Where(pp => pp.MonitorBounds == p.MonitorBounds).Count() == 2)
                                    {
                                        screens[i].PlayerOnScreen--;
                                        TotalPlayers--;
                                    }
                                }
                                else
                                {                                                                     
                                    loadedProfilePlayers.Remove(p);
                                    screens[i].PlayerOnScreen--;
                                    TotalPlayers--;
                                }

                                p.EditBounds = GetDefaultBounds(j);
                                p.Owner = null;
                                p.ScreenIndex = -1;
                                p.MonitorBounds = new Rectangle(0, 0, 0, 0);
                                p.DisplayIndex = -1;
                            }

                        }

                        UpdatePlayers();
                        Invalidate();
                        return;
                    }
                }

                for (int i = 0; i < players.Count; i++)
                {
                    Rectangle r = players[i].EditBounds;

                    if (r.Contains(e.Location))
                    {
                        if (!players[i].IsInputUsed)
                        {
                            return;
                        }

                        dragging = true;
                        draggingIndex = i;
                        draggingOffset = new Point(r.X - e.X, r.Y - e.Y);
                        Rectangle newBounds = GetDefaultBounds(draggingIndex);

                        profile.PlayersList[draggingIndex].EditBounds = newBounds;

                        if (draggingOffset.X < -newBounds.Width ||
                            draggingOffset.Y < -newBounds.Height)
                        {
                            draggingOffset = new Point(0, 0);
                        }

                        break;
                    }
                }
            }
            else if (e.Button == MouseButtons.Right || e.Button == MouseButtons.Middle)
            {
                for (int i = 0; i < screens.Length; i++)
                {
                    UserScreen screen = screens[i];

                    if (screen.SwapTypeBounds.Contains(e.Location))
                    {
                        if (GameProfile.Loaded)
                        {
                            return;
                        }

                        if (screen.Type == UserScreenType.FullScreen)
                        {
                            screen.Type = UserScreenType.Custom;
                        }
                        else
                        {
                            screen.Type--;
                        }

                        ///invalidate all players inside screen
                        for (int j = 0; j < players.Count; j++)
                        {
                            ///return to default position
                            PlayerInfo p = players[j];

                            if (p.ScreenIndex == i)
                            {
                                if ((p.IsRawKeyboard || p.IsRawMouse) && !(p.IsRawKeyboard && p.IsRawMouse))
                                {
                                    if (loadedProfilePlayers.Contains(p))
                                    {
                                        loadedProfilePlayers.Remove(p);
                                    }
                                    else if (devicesToMerge.Contains(p))
                                    {
                                        devicesToMerge.Remove(p);
                                    }

                                    if (profile.PlayersList.Where(pp => pp.MonitorBounds == p.MonitorBounds).Count() == 2)
                                    {
                                        screens[i].PlayerOnScreen--;
                                        TotalPlayers--;
                                    }
                                }
                                else
                                {
                                    loadedProfilePlayers.Remove(p);
                                    screens[i].PlayerOnScreen--;
                                    TotalPlayers--;
                                }

                                p.EditBounds = GetDefaultBounds(j);
                                p.Owner = null;
                                p.ScreenIndex = -1;
                                p.MonitorBounds = new Rectangle(0, 0, 0, 0);
                                p.DisplayIndex = -1;

                            }
                        }

                        Invalidate();
                        return;
                    }
                }

                ///if over a player on a screen, expand player bounds
                for (int i = 0; i < players.Count; i++)
                {
                    PlayerInfo p = players[i];
                    Rectangle r = p.EditBounds;
                    Rectangle pib = Rectangle.Empty;

                    PlayerInfo playerInbounds = players.Where(pl => pl != p && pl.EditBounds == r).FirstOrDefault();

                    if (playerInbounds != null)
                    {
                        pib = playerInbounds.EditBounds;
                    }

                    if (r.Contains(e.Location) || pib.Contains(e.Location))
                    {
                        if (p.ScreenIndex != -1)
                        {
                            UserScreen screen = screens[p.ScreenIndex];

                            int verLines = 2;
                            int horLines = 2;

                            switch (screen.Type)
                            {
                                case UserScreenType.FourPlayers:
                                    {
                                        verLines = 2;
                                        horLines = 2;
                                    }
                                    break;
                                case UserScreenType.SixPlayers:
                                    {
                                        verLines = 3;
                                        horLines = 2;
                                    }
                                    break;
                                case UserScreenType.EightPlayers:
                                    {
                                        verLines = 4;
                                        horLines = 2;
                                    }
                                    break;
                                case UserScreenType.SixteenPlayers:
                                    {
                                        verLines = 4;
                                        horLines = 4;
                                    }
                                    break;
                                case UserScreenType.Custom:
                                    {
                                        horLines = GameProfile.CustomLayout_Hor + 1;
                                        verLines = GameProfile.CustomLayout_Ver + 1;
                                    }
                                    break;
                            }                     

                            int halfWidth = screen.MonitorBounds.Width / verLines;
                            int halfHeight = screen.MonitorBounds.Height / horLines;

                            Rectangle bounds = p.MonitorBounds;
                            if ((int)screen.Type >= 3)
                            {
                                ///check if the size is 1/4th of screen
                                if (bounds.Width == halfWidth && bounds.Height == halfHeight)
                                {
                                    bool hasLeftRightSpace = true;
                                    bool hasTopBottomSpace = true;

                                    if (players.Where(pl => (pl != p && pl.ScreenIndex != -1) && (pl.EditBounds.Left == r.Right || (pl.EditBounds.Right == r.Left && r.Right == screen.UIBounds.Right)) && (pl.EditBounds.Y == r.Y)).Count() > 0)
                                    {
                                        hasLeftRightSpace = false;
                                    }

                                    if (players.Where(pl => (pl != p && pl.ScreenIndex != -1) && (pl.EditBounds.Top == r.Bottom || (pl.EditBounds.Top == r.Bottom && r.Bottom == screen.UIBounds.Bottom)) && pl.EditBounds.X == r.X).Count() > 0)
                                    {
                                        hasTopBottomSpace = false;
                                    }

                                    ///check if we have something left/right or top/bottom
                                    if (hasLeftRightSpace)
                                    {
                                        Rectangle edit = r;

                                        //if (edit.X > screen.UIBounds.X + edit.Width)
                                        //{
                                        //    bounds.X -= bounds.Width;
                                        //    edit.X -= edit.Width;
                                        //}

                                        bounds.Width *= verLines;
                                        edit.Width *= verLines;

                                        while (players.Where(pl => (pl != p) && (pl.ScreenIndex != -1) && bounds.IntersectsWith(pl.MonitorBounds) && pl.EditBounds != r).Count() > 0 ||
                                            edit.Right > screen.UIBounds.Right)
                                        {
                                            bounds.Width -= p.MonitorBounds.Width;
                                            edit.Width -= p.EditBounds.Width;
                                        }

                                        if (edit != r)
                                        {


                                            p.EditBounds = edit;
                                            p.MonitorBounds = bounds;

                                            if (playerInbounds != null)
                                            {
                                                playerInbounds.MonitorBounds = p.MonitorBounds;
                                                playerInbounds.EditBounds = p.EditBounds;
                                            }

                                            Invalidate();
                                            break;
                                        }

                                    }

                                    if (hasTopBottomSpace)
                                    {
                                        Rectangle edit = r;

                                        //if (edit.Y > screen.UIBounds.Y + edit.Height)
                                        //{
                                        //    bounds.Y -= bounds.Height;
                                        //    edit.Y -= edit.Height;
                                        //}

                                        bounds.Height *= horLines;
                                        edit.Height *=  horLines;

                                        while (players.Where(pl => (pl != p) && (pl.ScreenIndex != -1) && bounds.IntersectsWith(pl.MonitorBounds) && pl.EditBounds != r).Count() > 0 ||
                                            edit.Bottom > screen.UIBounds.Bottom)
                                        {
                                            bounds.Height -= p.MonitorBounds.Height;
                                            edit.Height -= r.Height;
                                        }

                                        p.EditBounds = edit;
                                        p.MonitorBounds = bounds;

                                        if (playerInbounds != null)
                                        {
                                            playerInbounds.MonitorBounds = p.MonitorBounds;
                                            playerInbounds.EditBounds = p.EditBounds;
                                        }

                                        Invalidate();
                                        break;
                                    }
                                }
                                else
                                {
                                    bounds.Width = screen.MonitorBounds.Width / verLines;
                                    bounds.Height = screen.MonitorBounds.Height / horLines;
                                    p.MonitorBounds = bounds;

                                    Rectangle edit = p.EditBounds;
                                    edit.Width = screen.UIBounds.Width / verLines;
                                    edit.Height = screen.UIBounds.Height / horLines;
                                    p.EditBounds = edit;

                                    if (playerInbounds != null)
                                    {
                                        playerInbounds.MonitorBounds = p.MonitorBounds;
                                        playerInbounds.EditBounds = p.EditBounds;
                                    }

                                    Invalidate();
                                }
                            }
                        }
                    }
                }
            }
        }
        

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            mousePos = e.Location;

            if (dragging)
            {
                List<PlayerInfo> players = profile.PlayersList;

                PlayerInfo player = players[draggingIndex];

                if (!player.IsInputUsed)
                {
                    return;
                }

                Rectangle p = player.EditBounds;

                if (draggingScreen == -1)
                {
                    for (int i = 0; i < screens.Length; i++)
                    {
                        UserScreen screen = screens[i];

                        Rectangle s = screen.UIBounds;
                        float pc = RectangleUtil.PcInside(p, s);

                        ///bigger than 60% = major part inside this screen
                        if (pc > 0.6f)
                        {
                            float offset = s.Width * 0.05f;
                            ///check if there's space available on this screen
                            List<PlayerInfo> playas = profile.PlayersList;
                    
                            GetFreeSpace(i, out Rectangle? editor, out Rectangle? monitor, player);

                            if (editor != null && monitor != null)
                            {
                                if(autoFill || GameProfile.Loaded || expandedKM)
                                {
                                    draggingScreenRec = editor.Value;
                                    draggingScreenBounds = monitor.Value;
                                    destScreenIndex = i;
                                    draggingScreen = -1;
                                    expandedKM = false;
                                    break;
                                }
                                else if (destEditBounds.IntersectsWith(player.EditBounds))
                                {
                                    draggingScreenBounds = availableBounds.Where(b => b.Value == destEditBounds).FirstOrDefault().Key;
                                    draggingScreenRec = destEditBounds;
                                    destScreenIndex = i;
                                    draggingScreen = -1;
                                }                               
                            }
                        }
                    }
                }
                else
                {
                    Rectangle s = screens[draggingScreen].UIBounds;

                    float pc = RectangleUtil.PcInside(p, s);
                    if (pc < 0.6f)
                    {
                        draggingScreen = -1;
                    }
                }

                p = new Rectangle(mousePos.X + draggingOffset.X, mousePos.Y + draggingOffset.Y, p.Width, p.Height);
                players[draggingIndex].EditBounds = p;
                Invalidate();
            }       
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
           
            Cursor = hand_Cursor;

            if (e.Button == MouseButtons.Left)
            {
                if (dragging)
                {
                    PlayerInfo p = profile.PlayersList[draggingIndex];
                    dragging = false;

                    if (destScreenIndex != -1)
                    {
                        if (devicesToMerge.Contains(p))
                        {
                            devicesToMerge.Remove(p);
                        }
                        else if (loadedProfilePlayers.Contains(p))
                        {                           
                            loadedProfilePlayers.Remove(p);
                            
                            //controllers or merged to single k&m
                            if (!p.IsRawKeyboard && !p.IsRawMouse || (p.IsRawKeyboard && p.IsRawMouse))
                            {
                                screens[destScreenIndex].PlayerOnScreen--;
                                TotalPlayers--;
                            }
                        }

                        if (p.ScreenIndex != -1)
                        {
                            //if 2 devices share the same bounds(non merged keyboards or mice) 
                            if (profile.PlayersList.Where(pp => pp.MonitorBounds == p.MonitorBounds).Count() == 2)
                            {
                                screens[destScreenIndex].PlayerOnScreen--;
                                TotalPlayers--;
                            }
                        }

                        if ((p.IsRawKeyboard || p.IsRawMouse) && !(p.IsRawKeyboard && p.IsRawMouse))
                        {
                            if (loadedProfilePlayers.Any(pl => pl.MonitorBounds == draggingScreenBounds))
                            {
                                devicesToMerge.Add(p);
                            }
                            else
                            {
                                loadedProfilePlayers.Add(p);
                            }

                            if (profile.PlayersList.Where(pp => pp.MonitorBounds == draggingScreenBounds).Count() == 1)
                            {
                                screens[destScreenIndex].PlayerOnScreen++;
                                TotalPlayers++;
                            }
                        }
                        else
                        {
                            loadedProfilePlayers.Add(p);
                            screens[destScreenIndex].PlayerOnScreen++;
                            TotalPlayers++;
                        }

                        p.Owner = screens[destScreenIndex];
                        p.ScreenIndex = destScreenIndex;


                        p.MonitorBounds = draggingScreenBounds;
                        p.EditBounds = draggingScreenRec;

                        p.ScreenPriority = screens[destScreenIndex].priority;
                        p.DisplayIndex = screens[destScreenIndex].DisplayIndex;
                        destScreenIndex = -1;

                    }
                    else
                    {
                        for (int i = 0; i < screens.Length; i++)
                        {
                            if (p.ScreenIndex == i)
                            {
                                if ((p.IsRawKeyboard || p.IsRawMouse) && !(p.IsRawKeyboard && p.IsRawMouse))
                                {
                                    if (loadedProfilePlayers.Contains(p))
                                    {
                                        loadedProfilePlayers.Remove(p);
                                    }
                                    else if (devicesToMerge.Contains(p))
                                    {
                                        devicesToMerge.Remove(p);
                                    }

                                    if (profile.PlayersList.Where(pp => pp.MonitorBounds == p.MonitorBounds).Count() == 2)
                                    {
                                        screens[i].PlayerOnScreen--;
                                        TotalPlayers--;
                                    }
                                }
                                else
                                {
                                    loadedProfilePlayers.Remove(p);
                                    screens[i].PlayerOnScreen--;
                                    TotalPlayers--;
                                }

                                // return to default position
                                p.Owner = null;
                                p.EditBounds = GetDefaultBounds(draggingIndex);
                                p.MonitorBounds = new Rectangle(0, 0, 0, 0);
                                p.ScreenPriority = -1;
                                p.ScreenIndex = -1;
                            }
                        }
                    }

                    UpdatePlayers(); //force players update                    

                    Invalidate();
                }
            }

            availableBounds.Clear();
            showIndicator = false;
            Cursor = default_Cursor;
        }


        private Rectangle GetDefaultBounds(int index)
        {
            float lineWidth = index * playerSize;
            float line = (float)Math.Round(((lineWidth + playerSize) / (double)playersArea.Width) - 0.5);
            int perLine = (int)Math.Round((playersArea.Width / (double)playerSize) - 0.5);

            float x = playersArea.X + (index * playerSize) - (perLine * playerSize * line);
            float y = playersArea.Y + (playerSize * line);

            return new Rectangle((int)x, (int)y, playerSize, playerSize);
        }

        public void ResetScreensTotalPlayers()
        {
            showError = false;
            screens = null;
            UpdateScreens();

            if (screens != null)
                for (int i = 0; i < screens.Length; i++)
                {
                    UserScreen s = screens[i];
                    s.PlayerOnScreen = 0;
                    TotalPlayers = 0;
                }
        }

        private void LoadPlayerProfile(PlayerInfo player)
        {
            if (loadedProfilePlayers.Contains(player))
            {
                return;
            }

            if (showError)
            {
                GameProfile.currentProfile.Reset();
                return;
            }

            if (GameProfile.Loaded)
            {
                if (TotalPlayers < 0 && !showError)
                {
                    showError = true;
                    NucleusMessageBox.Show("error", "Oops!\nSomething went wrong, profile has been unloaded.", false);
                    return;
                }
            }

            if (TotalPlayers < GameProfile.TotalPlayers)
            {
                ///Merge raw keyboards and raw mice devices  to single one as saved in the game profile
                for (int i = 0; i < GameProfile.TotalPlayers; i++)
                {
                    List<PlayerInfo> groupedPlayers = new List<PlayerInfo>();

                    ProfilePlayer kbPlayer = GameProfile.ProfilePlayersList[i];

                    for (int pl = 0; pl < profile.PlayersList.Count; pl++)
                    {
                        PlayerInfo p = profile.PlayersList[pl];

                        if (p.IsRawKeyboard || p.IsRawMouse)
                        {
                            if (kbPlayer.HIDDeviceIDs.Any(hid => hid == p.HIDDeviceID[0]))
                            {
                                groupedPlayers.Add(p);
                            }
                        }

                        if (groupedPlayers.Count == 2)
                        {
                            var firstInGroup = groupedPlayers.First();
                            var secondInGroup = groupedPlayers.Last();

                            firstInGroup.IsRawKeyboard = groupedPlayers.Count(x => x.IsRawKeyboard) > 0;
                            firstInGroup.IsRawMouse = groupedPlayers.Count(x => x.IsRawMouse) > 0;

                            if (firstInGroup.IsRawKeyboard) firstInGroup.RawKeyboardDeviceHandle = groupedPlayers.First(x => x.RawKeyboardDeviceHandle != (IntPtr)(-1)).RawKeyboardDeviceHandle;
                            if (firstInGroup.IsRawMouse) firstInGroup.RawMouseDeviceHandle = groupedPlayers.First(x => x.RawMouseDeviceHandle != (IntPtr)(-1)).RawMouseDeviceHandle;

                            firstInGroup.HIDDeviceID = new string[2] { firstInGroup.HIDDeviceID[0], secondInGroup.HIDDeviceID[0] };

                            profile.PlayersList.Remove(secondInGroup);
                            p = firstInGroup;
                            p.IsInputUsed = true;///needed else device is invisible and can't be moved

                            break;
                        }
                    }
                }

                ProfilePlayer profilePlayer = null;

                bool skipGuid = false;

                //DInput && XInput (follow gamepad api indexes)
                if (player.IsController && useGamepadApiIndex)
                {
                    foreach (ProfilePlayer pp in GameProfile.ProfilePlayersList)
                    {
                        if (loadedProfilePlayers.All(lp => lp.PlayerID != pp.PlayerID) && (pp.IsDInput || pp.IsXInput))
                        {
                            profilePlayer = pp;
                            skipGuid = true;
                            break;
                        }
                    }
                }

                //DInput && XInput using GamepadGuid(do not follow gamepad api indexes)
                if (player.IsController && !skipGuid && !useGamepadApiIndex)
                {
                    profilePlayer = GameProfile.ProfilePlayersList.Where(pl => (pl.IsDInput || pl.IsXInput) && (pl.GamepadGuid == player.GamepadGuid)).FirstOrDefault();
                }

                //single k&m 
                if (player.GamepadGuid.ToString() == "10000000-1000-1000-1000-100000000000")
                {
                    profilePlayer = GameProfile.ProfilePlayersList.Where(pl => pl.GamepadGuid.ToString() == "10000000-1000-1000-1000-100000000000").FirstOrDefault();
                }

                //Multiple k&m
                if (player.IsRawKeyboard && player.IsRawMouse)///Merged raw keyboard and raw mouse player
                {
                    profilePlayer = GameProfile.ProfilePlayersList.Where(pl => (pl.IsRawMouse && pl.IsKeyboardPlayer) && (pl.HIDDeviceIDs.Contains(player.HIDDeviceID[0]) && pl.HIDDeviceIDs.Contains(player.HIDDeviceID[1]))).FirstOrDefault();
                }

                for (int i = 0 ; i < screens.Length ; i++)
                {
                    UserScreen anyScr = screens[i];

                    if(GameProfile.ProfilePlayersList.Any(pp => pp.ScreenIndex == i))
                    {
                        anyScr.Type = (UserScreenType) GameProfile.ProfilePlayersList.Where(pp => pp.ScreenIndex == i).FirstOrDefault().OwnerType;
                    }
                }

                if (profilePlayer == null)
                {
                    return;
                }

                UserScreen scr = screens.ElementAtOrDefault(profilePlayer.ScreenIndex);
                scr.Type = (UserScreenType)profilePlayer.OwnerType;
   
                if (profile.PlayersList.All(lpp => lpp.MonitorBounds != TranslateBounds(profilePlayer, profilePlayer.ScreenIndex).Item1) &&//check for translated bounds
                    GameProfile.ProfilePlayersList.FindIndex(pp => pp == profilePlayer) == loadedProfilePlayers.Count)//make sure to insert player like saved in the game profile
                {

                    player.Owner = scr;
                    player.Owner.Type = scr.Type;
                    player.ScreenIndex = profilePlayer.ScreenIndex;
                    player.ScreenPriority = scr.priority;
                    player.DisplayIndex = scr.DisplayIndex;

                    var translatedBounds = TranslateBounds(profilePlayer, profilePlayer.ScreenIndex);

                    player.EditBounds = translatedBounds.Item2;

                    player.MonitorBounds = translatedBounds.Item1;

                    player.Nickname = profilePlayer.Nickname;
                    player.PlayerID = profilePlayer.PlayerID;
                    player.SteamID = profilePlayer.SteamID;
                    player.IsInputUsed = true;

                    loadedProfilePlayers.Add(player);

                    scr.PlayerOnScreen++;
                    TotalPlayers++;

                    Invalidate();

                    if (TotalPlayers == GameProfile.TotalPlayers && GameProfile.Ready &&
                        GameProfile.AutoPlay && !GameProfile.Updating)
                    {
                        CanPlayUpdated(true, true);
                        btn_Play.PerformClick();
                        GameProfile.Ready = false;
                        return;
                    }
                }
                
            }

            if (TotalPlayers == GameProfile.TotalPlayers)
            {
                if (GameProfile.Ready && (!GameProfile.AutoPlay || GameProfile.Updating))
                {
                    CanPlayUpdated(true, true);
                    GameProfile.Updating = false;
                    return;
                }           
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            if (totalBounds == Rectangle.Empty)///Avoid resizing conflicts with "OnSizeChange" event.
            {
                GameProfile.currentProfile.Reset();
            }

            for (int i = 0; i < screens.Length; i++)
            {
                UserScreen s = screens[i];

                if (UseLayoutSelectionBorder)
                {
                    g.DrawRectangle(PositionScreenPen, s.SwapTypeBounds);
                }

                if (UseSetupScreenBorder)
                {
                    g.DrawRectangle(PositionScreenPen, s.UIBounds);
                }

                if (UseSetupScreenImage)
                {
                    setupScr = new Rectangle((int)s.UIBounds.X, (int)s.UIBounds.Y, (int)s.UIBounds.Width, (int)s.UIBounds.Height);
                    g.DrawImage(screenimg, setupScr);
                }

                switch (s.Type)
                {
                    case UserScreenType.FullScreen:
                        g.DrawImage(fullscreen, s.SwapTypeBounds);
                        break;
                    case UserScreenType.DualHorizontal:
                        g.DrawImage(horizontal2, s.SwapTypeBounds);
                        break;
                    case UserScreenType.DualVertical:
                        g.DrawImage(vertical2, s.SwapTypeBounds);
                        break;
                    case UserScreenType.FourPlayers:
                        g.DrawImage(players4, s.SwapTypeBounds);
                        break;
                    case UserScreenType.SixPlayers:
                        g.DrawImage(players6, s.SwapTypeBounds);
                        break;
                    case UserScreenType.EightPlayers:
                        g.DrawImage(players8, s.SwapTypeBounds);
                        break;
                    case UserScreenType.SixteenPlayers:
                        g.DrawImage(players16, s.SwapTypeBounds);
                        break;
                    case UserScreenType.Custom:
                        g.DrawImage(customLayout, s.SwapTypeBounds);
                        break;
                }           
            }

            List<PlayerInfo> players = profile.PlayersList;

            if (players.Count == 0)
            {
                g.DrawString(game.Game.SupportsMultipleKeyboardsAndMice ? "Press A Key Or Move A Mouse" : "Waiting For Controllers...", playerTextFont, myBrush, new PointF(10, 10));
            }
            else
            {
                g.ResetClip();

                string msg = String.Empty;

                Brush brush = myBrush;

                if (GameProfile.Loaded)
                {
                    if (TotalPlayers > GameProfile.TotalPlayers)
                    {
                        msg = $"There Is Too Much Players!";
                        brush = notEnoughPlyrsSBrush;
                    }
                    else if ((GameProfile.TotalPlayers - TotalPlayers) > 0)
                    {
                        string st = GameProfile.TotalPlayers - TotalPlayers > 1 ? "Players." : "Player.";
                        msg = $"{GameProfile.GamepadCount} Controller(s), {GameProfile.KeyboardCount} K&M Were Used Last Time.";
                        brush = notEnoughPlyrsSBrush;
                    }
                    else if (GameProfile.TotalPlayers == TotalPlayers)
                    {
                        msg = $"Profile Ready!";
                    }
                }
                else
                {
                    string screenText = screens.Length > 1 ? "On The Desired Screens" : "On The Screen";

                    if (game.Game.SupportsMultipleKeyboardsAndMice)
                    {
                        if (useGamepadApiIndex || profileDisabled)
                        {
                            msg = $"Drop Devices {screenText}.";
                        }
                        else
                        {
                            if ((game.Game.Hook.XInputEnabled && !game.Game.Hook.XInputReroute && !game.Game.ProtoInput.DinputDeviceHook) || game.Game.ProtoInput.XinputHook)
                            {
                                msg = $"Press A Button On Each Gamepad & Drop Devices {screenText}.";
                            }
                            else if (game.Game.Hook.DInputEnabled || game.Game.Hook.XInputReroute || game.Game.ProtoInput.DinputDeviceHook)
                            {
                                msg = $"Drop Devices {screenText}.";
                            }
                        }
                    }
                    else if (!game.Game.SupportsMultipleKeyboardsAndMice && !game.Game.SupportsKeyboard)
                    {
                        if (useGamepadApiIndex || profileDisabled)
                        {
                            msg = $"Drop Gamepads {screenText}.";
                        }
                        else
                        {
                            msg = $"Press A Button On Each Gamepad & Drop Them {screenText}.";
                        }
                    }
                    else
                    {
                        if (useGamepadApiIndex || profileDisabled)
                        {
                            msg = $"Drop Gamepads Or Keyboard & Mouse {screenText}.";
                        }
                        else
                        {
                            msg = $"Press A Button On Each Gamepad & Drop Devices {screenText}.";
                        }
                    }
                }

                g.DrawString(msg, playerTextFont, brush, new PointF(10, 10));

                for (int i = 0; i < players.Count; i++)
                {
                    PlayerInfo player = players[i];

                    if (!profileDisabled)
                    {
                        LoadPlayerProfile(player);

                        //Draw all the profile players bounds until they get filled
                        if (i < GameProfile.TotalPlayers)
                        {
                            g.ResetClip();

                            var ghostBounds = TranslateBounds(GameProfile.ProfilePlayersList[i], GameProfile.ProfilePlayersList[i].ScreenIndex);

                            Rectangle ghostMBounds = ghostBounds.Item1;
                            Rectangle ghostEBounds = ghostBounds.Item2;

                            if (profile.PlayersList.All(p => p.MonitorBounds != ghostMBounds))
                            {
                                string ghostTag = $"P{i + 1}: {GameProfile.ProfilePlayersList[i].Nickname}";

                                SizeF ghostTagSize = g.MeasureString(ghostTag, playerTextFont);
                                Point ghostTagLocation = new Point(((int)ghostEBounds.Left + (int)ghostEBounds.Width / 2) - ((int)ghostTagSize.Width / 2), (int)(ghostEBounds.Bottom + 1 - ghostTagSize.Height));
                                RectangleF ghostTagBack = new RectangleF(ghostTagLocation.X, ghostTagLocation.Y, ghostTagSize.Width, ghostTagSize.Height);
                                Rectangle ghostTagBorder = new Rectangle(ghostTagLocation.X, ghostTagLocation.Y, (int)ghostTagSize.Width, (int)ghostTagSize.Height);

                                g.FillRectangle(Brushes.DarkSlateGray, ghostTagBack);
                                g.DrawRectangle(ghostBoundsPen, ghostEBounds);
                                g.DrawRectangle(ghostBoundsPen, ghostTagBorder);
                                g.DrawString(ghostTag, playerTextFont, Brushes.Orange, ghostTagLocation.X, ghostTagLocation.Y);
                            }
                        }
                    }

                    string str = (player.GamepadId + 1).ToString();

                    Rectangle s = player.EditBounds;

                    g.Clip = new Region(new RectangleF(s.X, s.Y, s.Width + 1, s.Height + 1));

                    Rectangle gamepadRect = RectangleUtil.ScaleAndCenter(keyboardPic.Size, s);

                    SizeF size = g.MeasureString(str, playerCustomFont);
                    PointF loc = RectangleUtil.Center(size, s);
                    Pen color = (player.GamepadId > colors.Count()) ? new Pen(Color.Magenta) : new Pen(colors[player.GamepadId]);

                    if (player.IsXInput)
                    {
                        loc.Y -= gamepadRect.Height * 0.2f;
                        var playerColor = colors[player.GamepadId];
                        str = (player.GamepadId + 1).ToString();

                        size = g.MeasureString(str, playerCustomFont);
                        loc = RectangleUtil.Center(size, s);
                        loc.Y -= 5;

                        if (PollXInputGamepad(player))
                        {
                            polling = true;
                            g.DrawImage(xinputPic, gamepadRect, 0, 0, xinputPic.Width, xinputPic.Height, GraphicsUnit.Pixel, flashImageAttributes);
                        }
                        else
                        {
                            g.DrawImage(xinputPic, gamepadRect);
                        }

                        if (controllerIdentification)
                        {
                            g.DrawString(str, playerCustomFont, playerColor, loc);
                        }
                    }
                    else if (player.IsKeyboardPlayer && !player.IsRawKeyboard && !player.IsRawMouse)
                    {
                        if (player.ShouldFlash)
                        {
                            player.IsInputUsed = true;
                            g.DrawImage(keyboardPic, gamepadRect, 0, 0, keyboardPic.Width, keyboardPic.Height, GraphicsUnit.Pixel, flashImageAttributes);
                        }
                        else if (player.IsInputUsed)
                        {
                            g.DrawImage(keyboardPic, gamepadRect);
                        }
                    }
                    else if ((player.IsRawKeyboard || player.IsRawMouse))
                    {
                        Image img = player.IsRawKeyboard ? protoKeyboardPic : protoMousePic;

                        if (player.RawMouseDeviceHandle != IntPtr.Zero && player.RawKeyboardDeviceHandle != IntPtr.Zero)
                        {
                            if (player.IsRawKeyboard && player.IsRawMouse)///grouped m&k profile player so add same picture as single k&m player
                            {
                                img = keyboardPic;
                            }

                            if (player.ShouldFlash)
                            {
                                player.IsInputUsed = true;
                                g.DrawImage(img, gamepadRect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, flashImageAttributes);
                            }
                            else if (player.IsInputUsed)
                            {
                                g.DrawImage(img, gamepadRect);
                            }
                        }
                        else
                        {
                            Image virtualImg = player.IsRawKeyboard ? virtualKeyboardPic : virtualMousePic;

                            if (player.ShouldFlash)
                            {
                                player.IsInputUsed = true;
                                g.DrawImage(virtualImg, gamepadRect, 0, 0, virtualImg.Width, virtualImg.Height, GraphicsUnit.Pixel, flashImageAttributes);
                            }
                            else if (player.IsInputUsed)
                            {
                                g.DrawImage(virtualImg, gamepadRect);
                            }
                        }
                    }
                    else
                    {
                        loc.Y -= gamepadRect.Height * 0.2f;
                        var playerColor = colors[player.GamepadId];
                        str = (player.GamepadId + 1).ToString();
                        size = g.MeasureString(str, playerCustomFont);
                        loc = RectangleUtil.Center(size, s);
                        loc.Y -= 5;

                        if (controllerIdentification)
                        {
                            g.DrawString(str, playerCustomFont, playerColor, loc);
                        }

                        if (PollDInputGamepad(player))
                        {
                            g.DrawImage(dinputPic, gamepadRect, 0, 0, dinputPic.Width, dinputPic.Height, GraphicsUnit.Pixel, flashImageAttributes);
                        }
                        else
                        {
                            g.DrawImage(dinputPic, gamepadRect);
                        }
                    }

                    if (player.ScreenIndex != -1)
                    {
                        g.Clip = new Region(new RectangleF(s.X, s.Y, s.Width + 1, s.Height + 1));
                                 
                        g.DrawRectangle(PositionPlayerScreenPen, s);
                
                        var secondInBounds = loadedProfilePlayers.Where(pl => pl.EditBounds == player.EditBounds && pl != player && pl.ScreenIndex != -1).FirstOrDefault();

                        if (loadedProfilePlayers.Contains(player) || secondInBounds != null)
                        {
                            PlayerInfo plToUpdate = secondInBounds != null ? secondInBounds : player;
                            UserScreen screen = screens[plToUpdate.ScreenIndex];

                            if ((s.Height + s.Y) - (screen.UIBounds.Height + screen.UIBounds.Y) == -1)
                            {
                                s.Height += 1;
                            }

                            int playerIndex = loadedProfilePlayers.FindIndex(pl => pl == plToUpdate);
                            int ProfilePlayersListCount = GameProfile.ProfilePlayersList.Count;
                            string nickname = ProfilePlayersListCount != 0 && ProfilePlayersListCount >= playerIndex ? GameProfile.ProfilePlayersList[playerIndex].Nickname :
                                              Globals.ini.IniReadValue("ControllerMapping", "Player_" + (playerIndex + 1).ToString());
                            
                            if(nickname == "" || nickname == null)
                            {
                                nickname = $"Player{playerIndex + 1}";
                            }

                            string tag = $"P{playerIndex + 1}: {nickname}";

                            SizeF tagSize = g.MeasureString(tag, playerTextFont);
                            Point tagLocation = new Point(((int)s.Left + (int)s.Width / 2) - ((int)tagSize.Width / 2), (int)(s.Bottom + 1 - tagSize.Height));
                            RectangleF tagBack = new RectangleF(tagLocation.X, tagLocation.Y, tagSize.Width, tagSize.Height);
                            Rectangle tagBorder = new Rectangle(tagLocation.X, tagLocation.Y, (int)tagSize.Width, (int)tagSize.Height);

                            plToUpdate.Nickname = nickname;
                          
                            //make sure that the player has the right steam id assigned if a device(player) is swapped with an other that is not part of the profile after loading a profile.                        
                            string steamID = "";

                            if (GameProfile.ProfilePlayersList.Count > 0)
                            {
                                steamID = GameProfile.ProfilePlayersList[playerIndex].SteamID.ToString();

                            }
                            else if (Globals.ini.IniReadValue("SteamIDs", "Player_" + (playerIndex + 1)).ToString() != "")
                            {
                                steamID = Globals.ini.IniReadValue("SteamIDs", "Player_" + (playerIndex + 1)).ToString();
                            }
                            else
                            {
                                steamID = "-1";
                            }

                            if (steamID != "")
                            {
                                plToUpdate.SteamID = long.Parse(steamID);
                            }

                            g.Clip = new Region(tagBack);

                            g.FillRectangle(tagBrush, tagBack);
                            g.DrawRectangle(PositionScreenPen, tagBorder);
                            g.DrawString(tag, playerTextFont, Brushes.GreenYellow, tagLocation.X, tagLocation.Y);
                        }
                    }            

                }
            }

            g.ResetClip();

            destEditBounds = Rectangle.Empty;

            if (!GameProfile.Loaded)
            {
                if (availableBounds.Count > 0)
                {
                    for (int b = 0; b < availableBounds.Count; b++)
                    {
                        Rectangle ebound = availableBounds.ElementAt(b).Value;
                        if (profile.PlayersList.All(pl => pl.EditBounds.Contains(ebound) == false))
                        {
                            g.DrawRectangle(availBoundsPen, ebound);
                        }
                    }
                }

                destEditBounds = availableBounds.Where(b => b.Value.Contains(mousePos)).FirstOrDefault().Value;
                destMonitorBounds = availableBounds.Where(b => b.Value.Contains(mousePos)).FirstOrDefault().Key;
 
                if (profile.PlayersList.All(pl => pl.EditBounds.Contains(destEditBounds) == false) || showIndicator)
                {
                    if(showIndicator)
                    {
                        draggingScreenIndicator = new Rectangle(draggingScreenRec.Right - draggingScreenRec.Height, draggingScreenRec.Y, draggingScreenRec.Height, draggingScreenRec.Height);
                        g.DrawImage(draggingScreenImg, draggingScreenIndicator);
                    }
                    else 
                    {
                        draggingScreenIndicator = new Rectangle(destEditBounds.Right - destEditBounds.Height, destEditBounds.Y, destEditBounds.Height, destEditBounds.Height);
                        g.DrawRectangle(destEditBoundsPen, destEditBounds);
                        g.DrawImage(draggingScreenImg, draggingScreenIndicator);
                    }                                                
                }

            }
            else if (dragging && destScreenIndex != -1)
            {
                draggingScreenIndicator = new Rectangle(draggingScreenRec.Right - draggingScreenRec.Height, draggingScreenRec.Y, draggingScreenRec.Height, draggingScreenRec.Height);

                g.DrawRectangle(destEditBoundsPen, draggingScreenRec);
                g.DrawImage(draggingScreenImg, draggingScreenIndicator);
            }

            polling = false;
            //Console.WriteLine(TotalPlayers);
        }
    }
}
