using Nucleus.Gaming;
using Nucleus.Gaming.Controls;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.Coop.InputManagement;
using Nucleus.Gaming.Properties;
using SharpDX.DirectInput;
using SharpDX.XInput;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace Nucleus.Coop
{
    public class PositionsControl : UserInputControl, IDynamicSized
    {
        private readonly IniFile themeIni = Globals.ThemeIni;
        private string theme = Globals.Theme;
        private static PositionsControl _positionsControl;

        // array of users's screens
        private UserScreen[] screens;

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
        private Rectangle _draggingScreen;

        private int playerSize;
        private int draggingIndex = -1;
        private int draggingScreen = -1;
        private int dinputPressed = -1;
        private int gamePadPressed = -1;
        private int testDinputPlayers = -1;// 16;
        private int testXinputPlayers = -1;// 16;

        private bool canProceed;
        private bool controllerIdentification;
        private bool dragging = false;
        public bool isDisconnected;
        private bool insideGamepadTick = false;
        private bool UseSetupScreenBorder;
        private bool UseLayoutSelectionBorder;
        private bool UseSetupScreenImage;
        private bool appStart = false;
        public override bool CanProceed => canProceed;
        public override bool CanPlay => false;
        public bool CanUpdate = true;
        private Point draggingOffset;
        private Point mousePos;

        private Rectangle draggingScreenRec;
        private Rectangle draggingScreenBounds;

        // the factor to scale all screens to fit them inside the edit area
        private float screensAreaScale;
        private float newplayerCustomFontSize;

        public System.Threading.Timer gamepadTimer;
        public System.Threading.Timer gamepadPollTimer;

        public PictureBox instruction_btn;
        private PictureBox instructionImg;
        public PictureBox playerSetup_btn;
        private Label gameProfiles_btn;
        public ProfilesPanel gameProfilesPanel = new ProfilesPanel();
        private ImageAttributes flashImageAttributes;

        private Bitmap instructionCloseImg;
        private Bitmap instructionOpenImg;
        private Bitmap xinputPic;
        private Bitmap dinputPic;
        private Bitmap keyboardPic;
        private Bitmap protKeyboardPic;
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
        private Pen PositionPlayerScreenPen;
        private Pen PositionScreenPen;
        private Cursor hand_Cursor;
        private Cursor default_Cursor;
        public RichTextBox handlerNoteZoom;
        public Panel textZoomContainer;

        private string customFont;
        public override string Title => "Position Players";

        public PositionsControl()
        {
            PositionsControl._positionsControl = this;
            Initialize();
        }

        private void Initialize()
        {
            string[] rgb_PositionControlsFontColor = themeIni.IniReadValue("Colors", "SetupScreenFont").Split(',');
            string[] rgb_PositionScreenColor = themeIni.IniReadValue("Colors", "SetupScreenBorder").Split(',');
            string[] rgb_PositionPlayerScreenColor = themeIni.IniReadValue("Colors", "SetupScreenPlayerBorder").Split(',');
            controllerIdentification = bool.Parse(themeIni.IniReadValue("Misc", "ControllerIdentificationOn"));
            UseSetupScreenBorder = bool.Parse(themeIni.IniReadValue("Misc", "UseSetupScreenBorder"));
            UseLayoutSelectionBorder = bool.Parse(themeIni.IniReadValue("Misc", "UseLayoutSelectionBorder"));
            UseSetupScreenImage = bool.Parse(themeIni.IniReadValue("Misc", "UseSetupScreenImage"));
            customFont = themeIni.IniReadValue("Font", "FontFamily");

            if (gamepadTimer == null)
            {
                gamepadTimer = new System.Threading.Timer(GamepadTimer_Tick, null, 0, 1000);
            }

            if (gamepadPollTimer == null)
            {
                gamepadPollTimer = new System.Threading.Timer(GamepadPollTimer_Tick, null, 0, 1001);
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

            BackgroundImageLayout = ImageLayout.Stretch;
            instructionCloseImg = new Bitmap(theme + "instruction_closed.png");
            instructionOpenImg = new Bitmap(theme + "instruction_opened.png");
            xinputPic = new Bitmap(theme + "xinput.png");
            dinputPic = new Bitmap(theme + "dinput.png");
            keyboardPic = new Bitmap(theme + "keyboard.png");
            protKeyboardPic = new Bitmap(theme + "proto_keyboard.png");
            protoMousePic = new Bitmap(theme + "proto_mouse.png");
            virtualKeyboardPic = new Bitmap(theme + "virtual_keyboard.png");
            virtualMousePic = new Bitmap(theme + "virtual_mouse.png");
            screenimg = new Bitmap(theme + "screen.png");
            draggingScreenImg = new Bitmap(theme + "dragging_indicator.png");
            fullscreen = new Bitmap(theme + "fullscreen.png");
            horizontal2 = new Bitmap(theme + "2horizontal.png");
            vertical2 = new Bitmap(theme + "2vertical.png");
            players4 = new Bitmap(theme + "4players.png");
            players6 = new Bitmap(theme + "6players.png");
            players8 = new Bitmap(theme + "8players.png");
            players16 = new Bitmap(theme + "16players.png");
            customLayout = new Bitmap(theme + "customLayout.png");
            plyrsSettingsIcon = new Bitmap(theme + "player_settings.png");

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

            instructionImg = new PictureBox()
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BackColor = Color.Black,
                Image = Resources.instructions,
                BackgroundImageLayout = ImageLayout.Stretch,
                Cursor = hand_Cursor,
                //Size\Location  see => UpdateScreens() 
                SizeMode = PictureBoxSizeMode.StretchImage,
                Visible = false
            };

            handlerNoteZoom = new RichTextBox();
            handlerNoteZoom.Visible = true;
            handlerNoteZoom.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            handlerNoteZoom.BorderStyle = BorderStyle.None;
            handlerNoteZoom.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            handlerNoteZoom.ReadOnly = true;
            handlerNoteZoom.WordWrap = true;
            handlerNoteZoom.LinkClicked += new LinkClickedEventHandler(handlerNoteZoom_LinkClicked);
            handlerNoteZoom.Text = "";

            textZoomContainer = new Panel();
            textZoomContainer.Visible = false;
            textZoomContainer.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            textZoomContainer.BorderStyle = BorderStyle.None;
            textZoomContainer.BackgroundImageLayout = ImageLayout.Stretch;

            playerSetup_btn = new PictureBox();//using a button cause focus issues
            playerSetup_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            playerSetup_btn.BackColor = Color.Transparent;
            playerSetup_btn.Image = plyrsSettingsIcon;
            playerSetup_btn.SizeMode = PictureBoxSizeMode.StretchImage;
            playerSetup_btn.Cursor = hand_Cursor;
            playerSetup_btn.Font = instruction_btn.Font;

            gameProfiles_btn = new Label();//using a button cause focus issues
            gameProfiles_btn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            gameProfiles_btn.AutoSize = false;
            gameProfiles_btn.BackgroundImageLayout = ImageLayout.Zoom;
            gameProfiles_btn.BackgroundImage = new Bitmap(Globals.Theme + "button.png");
            gameProfiles_btn.BackColor = Color.Transparent;
            gameProfiles_btn.ForeColor = Color.White;
            gameProfiles_btn.TextAlign = ContentAlignment.MiddleCenter;
            gameProfiles_btn.Text = "Game Profiles";
            gameProfiles_btn.Cursor = hand_Cursor;
            gameProfiles_btn.Click += new EventHandler(this.gameProfilesBtn_Click);

            ResumeLayout();

            colors = new Brush[]
            {
              Brushes.Red, Brushes.DodgerBlue, Brushes.LimeGreen, Brushes.Yellow,Brushes.SaddleBrown, Brushes.BlueViolet, Brushes.Aqua, Brushes.DarkOrange, Brushes.Silver,
              Brushes.Magenta, Brushes.SpringGreen, Brushes.Indigo, Brushes.Black, Brushes.White, Brushes.Bisque, Brushes.SkyBlue, Brushes.SeaGreen,Brushes.Wheat, Brushes.Crimson, Brushes.Turquoise, Brushes.Chocolate,
              Brushes.OrangeRed, Brushes.Olive, Brushes.DarkRed, Brushes.Lavender
            };

            textZoomContainer.Controls.Add(handlerNoteZoom);
            Controls.Add(textZoomContainer);
            Controls.Add(instruction_btn);
            Controls.Add(playerSetup_btn);
            Controls.Add(gameProfiles_btn);
            Controls.Add(gameProfilesPanel);
            Controls.Add(instructionImg);

            //Flash image attributes
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

        private void handlerNoteZoom_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void gameProfilesBtn_Click(object sender, EventArgs e)
        {
            if (GameProfile.profilesPathList.Count == 0)
            {
                gameProfilesPanel.Visible = false;
                return;
            }

            if (gameProfilesPanel.Visible)
            {
                gameProfilesPanel.Visible = false;
            }
            else
            {
                gameProfilesPanel.Visible = true;
            }
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
            }
        }

        public void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }

            SuspendLayout();
            if (!appStart)
            {
                newplayerCustomFontSize = playerCustomFont.Size;
                handlerNoteZoom.Font = new Font(customFont, 14f * scale, FontStyle.Regular, GraphicsUnit.Pixel, 0);

                float instructionW = instruction_btn.Width * scale;
                float instructionH = instruction_btn.Height * scale;

                instruction_btn.Width = (int)instructionW;
                instruction_btn.Height = (int)instructionH;
                instruction_btn.Location = new Point((Width - instruction_btn.Width) - 5, 5);

                textZoomContainer.Size = new Size(Width - (int)(60 * scale), Height - (int)(50 * scale));
                textZoomContainer.Location = new Point(Width / 2 - textZoomContainer.Width / 2, instruction_btn.Height + (int)(10 * scale));
                handlerNoteZoom.Size = new Size(textZoomContainer.Width + (int)(18 * scale), textZoomContainer.Height - 40);
                handlerNoteZoom.Location = new Point(0, 20);

                playerSetup_btn.Size = instruction_btn.Size;
                playerSetup_btn.Location = new Point(((instruction_btn.Left - playerSetup_btn.Width) - 5), instruction_btn.Top);

                gameProfiles_btn.Font = new Font(customFont, 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);

                gameProfiles_btn.Size = new Size((int)(100 * scale), (int)(20 * scale));

                gameProfiles_btn.Location = new Point(((playerSetup_btn.Left - gameProfiles_btn.Width) - 10), (playerSetup_btn.Location.Y + playerSetup_btn.Height/2) - gameProfiles_btn.Height/2);
                gameProfilesPanel.Location = new Point(gameProfiles_btn.Left, gameProfiles_btn.Bottom);
                gameProfilesPanel.UpdateSize(scale);
                appStart = true;
            }
            ResumeLayout();
        }

        public static void InvalidateFlash()
        {
            _positionsControl?.Invalidate();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (profile != null && profile.PlayerData != null)
            {
                List<PlayerInfo> data = profile.PlayerData;

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

            List<PlayerInfo> data = profile.PlayerData;
            foreach (PlayerInfo player in data)
            {
                if (player.DInputJoystick != null)
                {
                    player.DInputJoystick.Dispose();
                }
            }

            gamepadTimer.Dispose();
            gamepadPollTimer.Dispose();
        }

        public void GamepadPollTimer_Tick(object state)
        {
            if (profile == null)
            {
                return;
            }

            gamePadPressed = -1;

            try
            {
                List<PlayerInfo> data = profile.PlayerData;
                foreach (PlayerInfo player in data)
                {
                    if (!player.IsKeyboardPlayer && !player.IsRawKeyboard && !player.IsRawMouse)
                    {
                        Invoke(new Action(() => PollGamepad(player)));
                    }
                }
            }
            catch (Exception)
            {
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(UpdatePlayers));
                    Invoke(new MethodInvoker(Refresh));

                    return;
                }

                gamePadPressed = -1;
            }
        }

        private void PollGamepad(PlayerInfo player)
        {
            gamePadPressed = -1;

            try
            {
                if (player.DInputJoystick.IsDisposed)
                {
                    return;
                }

                JoystickState state = player.DInputJoystick.GetCurrentState();

                bool[] buttonsPressed = state.Buttons;

                bool btnPressed = false;
                for (int b = 0; b < buttonsPressed.Length; b++)
                {
                    if (buttonsPressed[b])
                    {
                        btnPressed = true;
                        if (player.IsDInput)
                        {
                            dinputPressed = player.GamepadId;
                        }
                        gamePadPressed = player.GamepadId;
                        Refresh();
                        break;
                    }
                }
                if (player.IsDInput && !btnPressed && dinputPressed == player.GamepadId)
                {
                    dinputPressed = -1;
                    Invalidate();
                }
            }
            catch (Exception)
            {
                UpdatePlayers();
                Refresh();
                gamePadPressed = -1;
            }
        }


        public void GamepadTimer_Tick(object state)
        {
            if (insideGamepadTick)
            {
                return;
            }

            try
            {
                insideGamepadTick = true;

                gamePadPressed = -1;
                if (profile == null)
                {
                    insideGamepadTick = false;
                    return;
                }

                List<PlayerInfo> data = profile.PlayerData;

                bool changed = false;

                GenericGameInfo g = game.Game;

                DirectInput dinput = new DirectInput();

                // Using OpenXinput with more than 4 players means we can use more than 4 xinput controllers

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
                            data[j].DInputJoystick.Unacquire();
                            changed = true;
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
                        player.GamepadId = i;
                        string hid = player.DInputJoystick.Properties.InterfacePath;
                        player.RawHID = hid;
                        int start = hid.IndexOf("hid#");
                        int end = hid.LastIndexOf("#{");
                        string fhid = hid.Substring(start, end - start).Replace('#', '\\').ToUpper();
                        player.HIDDeviceID = fhid;
                        player.DInputJoystick.Acquire();
                        player.IsInputUsed = true;

                        data.Add(player);
                    }

                }

                if ((g.Hook.XInputEnabled && !g.Hook.XInputReroute && !g.ProtoInput.DinputDeviceHook) || g.ProtoInput.XinputHook)
                {
                    // XInput is only really enabled inside Nucleus Coop when
                    // we have 4 or less players, else we need to force DirectInput to grab everything

                    for (int j = 0; j < data.Count; j++)
                    {
                        PlayerInfo p = data[j];
                        if (p.IsXInput && !p.IsFake)
                        {
                            OpenXinputController c = new OpenXinputController(g.ProtoInput.UseOpenXinput, p.GamepadId);
                            if (!c.IsConnected)
                            {
                                data[j].DInputJoystick.Unacquire();
                                changed = true;
                                data.RemoveAt(j);
                                j--;
                                isDisconnected = true;
                            }
                        }
                    }

                    IList<DeviceInstance> devices = dinput.GetDevices(
                    SharpDX.DirectInput.DeviceType.Gamepad, DeviceEnumerationFlags.AttachedOnly);

                    int cOffset = 0;
                    int numControllers = g.ProtoInput.UseOpenXinput ? 32 : 4;
                    for (int i = 0; i < numControllers; i++)
                    {
                        OpenXinputController c = new OpenXinputController(g.ProtoInput.UseOpenXinput, i);

                        bool already = false;

                        if (!c.IsConnected)
                        {
                            cOffset++;
                        }
                        else
                        {
                            // see if this gamepad is already on a player
                            foreach (PlayerInfo p in data)
                            {
                                if (p.IsXInput && p.GamepadId == i)
                                {
                                    State s = c.GetState();
                                    int newmask = (int)s.Gamepad.Buttons;
                                    if (p.GamepadMask != newmask)
                                    {
                                        changed = true;
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

                            PlayerInfo player = new PlayerInfo();

                            for (int x = 0; x < devices.Count; x++)
                            {
                                DeviceInstance device = devices[x];

                                if ((x + cOffset) == i)
                                {
                                    //instanceIds.Add(device.InstanceGuid.ToString());
                                    player.GamepadGuid = device.InstanceGuid;
                                    player.GamepadProductGuid = device.ProductGuid;
                                    player.GamepadName = device.InstanceName;
                                    player.DInputJoystick = new Joystick(dinput, device.InstanceGuid);
                                    string hid = player.DInputJoystick.Properties.InterfacePath;
                                    player.RawHID = hid;
                                    int start = hid.IndexOf("hid#");
                                    int end = hid.LastIndexOf("#{");
                                    string fhid = hid.Substring(start, end - start).Replace('#', '\\').ToUpper();
                                    player.HIDDeviceID = fhid;
                                    player.DInputJoystick.Acquire();

                                    break;
                                }
                            }

                            // new gamepad
                            player.IsXInput = true;
                            player.IsInputUsed = true;
                            player.GamepadId = i;

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

            }
        }

        private void AddPlayer(int i, float playerWidth, float playerHeight, float offset)
        {
            Rectangle r = RectangleUtil.Float(50 + ((playerWidth + offset) * i), 100, playerWidth, playerHeight);
            PlayerInfo player = new PlayerInfo
            {
                EditBounds = r
            };

            profile.PlayerData.Add(player);
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

                // screens got updated, need to reflect in our window
                screens = newScreens;
                totalBounds = newBounds;

                // remove all players screens
                List<PlayerInfo> playerData = profile.PlayerData;
                if (playerData != null)
                {
                    for (int i = 0; i < playerData.Count; i++)
                    {
                        PlayerInfo player = playerData[i];
                        player.EditBounds = GetDefaultBounds(draggingIndex);
                        player.ScreenIndex = -1;
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
                screensAreaScale = screensArea.Height / (float)totalBounds.Height;
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

            // remove negative monitors
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

        public override void Initialize(UserGameInfo game, GameProfile profile)
        {
            base.Initialize(game, profile);

            if (gamepadTimer == null)
            {
                gamepadTimer = new System.Threading.Timer(GamepadTimer_Tick, null, 0, 1000);
            }

            if (gamepadPollTimer == null)
            {
                gamepadPollTimer = new System.Threading.Timer(GamepadPollTimer_Tick, null, 0, 1001);
            }

            UpdatePlayers();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            totalBounds = Rectangle.Empty;
            Invalidate();
        }


        public void UpdatePlayers()
        {
            if (profile == null)
            {
                return;
            }

            GenericGameInfo g = game.Game;
            List<PlayerInfo> playerData = profile.PlayerData;

            canProceed = playerData.Count(c => c.ScreenIndex != -1) >= 1;

            if (playerData.Count == 0)
            {
                if (game.Game.SupportsKeyboard)
                {
                    // add keyboard data
                    PlayerInfo kbPlayer = new PlayerInfo
                    {
                        IsKeyboardPlayer = true,
                        GamepadId = 99,
                        IsInputUsed = true
                    };
                    playerData.Add(kbPlayer);
                }

                if (game.Game.SupportsMultipleKeyboardsAndMice) //Raw mice/keyboards
                {
                    playerData.AddRange(RawInputManager.GetDeviceInputInfos());
                }

                if (testDinputPlayers != -1) // make fake data if needed
                {
                    for (int i = 0; i < testDinputPlayers; i++)
                    {
                        // new gamepad
                        PlayerInfo player = new PlayerInfo
                        {
                            GamepadGuid = new Guid(),
                            GamepadName = "Player",
                            IsDInput = true,
                            IsFake = true,
                            IsInputUsed = true
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
                            GamepadId = i,
                            IsFake = true,
                            IsInputUsed = true
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
            playerSize = (int)Math.Round(Math.Sqrt(maxArea) - 0.5); // force the round down
            // see if the size can fit it or we need to make some further adjustments
            int horizontal = (int)Math.Round((playersWidth / playerSize) - 0.5);
            int vertical = (int)Math.Round((playerHeight / playerSize) - 0.5);
            int total = vertical * horizontal;

            if (total < playerCount)
            {
                int newVertical = vertical + 1;
                SuspendLayout();
                playerCustomFont = new Font("Franklin Gothic Medium", newplayerCustomFontSize * 0.8f, FontStyle.Regular, GraphicsUnit.Point, 0);
                ResumeLayout();
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
                }
            }

            Invalidate();
            CanPlayUpdated(canProceed, false);
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
                        //TODO: needs to be cached
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

            List<PlayerInfo> players = profile.PlayerData;
            UserScreen screen = screens[screenIndex];
            Rectangle bounds = screen.MonitorBounds;
            Rectangle ebounds = screen.UIBounds;

            int index = -1;
            while (GetScreenDivisionBounds(screen.Type, ++index, out Rectangle? divMonitorBounds, out Rectangle? divEditorBounds, bounds, ebounds))
            {

                IEnumerable<PlayerInfo> playersInDiv = players.Where(
                    x => x.ScreenIndex == screenIndex && (x.MonitorBounds == divMonitorBounds.Value || (x.MonitorBounds.Width == divMonitorBounds.Value.Width * 2 && x.MonitorBounds.Y == divMonitorBounds.Value.Y && x.MonitorBounds.X == divMonitorBounds.Value.X) || (x.MonitorBounds.Height == divMonitorBounds.Value.Height * 2 && x.MonitorBounds.X == divMonitorBounds.Value.X && x.MonitorBounds.Y == divMonitorBounds.Value.Y)));

                //Raw mice/keyboards can go as pairs, nothing else can.
                if (
                    playerToInsert.IsRawMouse ? playersInDiv.All(x => x.IsRawKeyboard) :
                    playerToInsert.IsRawKeyboard ? playersInDiv.All(x => x.IsRawMouse) :

                    !playersInDiv.Any())
                {
                    monitorBounds = divMonitorBounds;
                    editorBounds = divEditorBounds;
                    return true;
                }

            }

            return false;
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if (dragging)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                // first count how many gamepads we have
                bool changed = false;
                for (int i = 0; i < screens.Length; i++)
                {
                    UserScreen screen = screens[i];
                    if (screen.UIBounds.Contains(e.Location) && !screen.SwapTypeBounds.Contains(e.Location))
                    {
                        List<PlayerInfo> playerData = profile.PlayerData;

                        // add all possible players!
                        for (int j = 0; j < playerData.Count; j++)
                        {
                            PlayerInfo player = playerData[j];

                            bool hasFreeSpace = GetFreeSpace(i, out Rectangle? editor, out Rectangle? monitor, player);

                            if (hasFreeSpace)
                            {
                                if (player.ScreenIndex == -1 && player.IsInputUsed)
                                {
                                    player.Owner = screens[i];
                                    player.ScreenIndex = i;
                                    player.MonitorBounds = monitor.Value;
                                    player.EditBounds = editor.Value;
                                    player.screenPriority = player.Owner.display.X + player.Owner.display.Y;
                                    changed = true;
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
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            Cursor = hand_Cursor;
            List<PlayerInfo> players = profile.PlayerData;

            if (dragging)// || GameProfile.PlayerIDs.Count > 0)
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
                        if (screen.Type == UserScreenType.Custom)
                        {
                            screen.Type = 0;
                        }
                        else
                        {
                            screen.Type++;
                        }
                        // invalidate all players inside screen
                        for (int j = 0; j < players.Count; j++)
                        {
                            // return to default position
                            PlayerInfo p = players[j];
                            if (p.ScreenIndex == i)
                            {
                                p.EditBounds = GetDefaultBounds(j);
                                p.ScreenIndex = -1;
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
                        dragging = true;
                        draggingIndex = i;
                        draggingOffset = new Point(r.X - e.X, r.Y - e.Y);
                        Rectangle newBounds = GetDefaultBounds(draggingIndex);
                        profile.PlayerData[draggingIndex].EditBounds = newBounds;

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
                        if (screen.Type == UserScreenType.FullScreen)
                        {
                            screen.Type = UserScreenType.Custom;
                        }
                        else
                        {
                            screen.Type--;
                        }
                        // invalidate all players inside screen
                        for (int j = 0; j < players.Count; j++)
                        {
                            // return to default position
                            PlayerInfo p = players[j];
                            if (p.ScreenIndex == i)
                            {
                                p.EditBounds = GetDefaultBounds(j);
                                p.ScreenIndex = -1;
                            }
                        }

                        Invalidate();
                        return;
                    }
                }
                // if over a player on a screen, change the type
                for (int i = 0; i < players.Count; i++)
                {
                    PlayerInfo p = players[i];
                    Rectangle r = p.EditBounds;

                    if (r.Contains(e.Location))
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
                                // check if the size is 1/4th of screen
                                if (bounds.Width == halfWidth && bounds.Height == halfHeight)
                                {
                                    bool hasLeftRightSpace = true;
                                    bool hasTopBottomSpace = true;

                                    // check if we have something left/right or top/bottom
                                    for (int j = 0; j < players.Count; j++)
                                    {
                                        if (i == j)
                                        {
                                            continue;
                                        }

                                        PlayerInfo other = players[j];

                                        if (other.MonitorBounds == new Rectangle(0, 0, 0, 0))
                                        {
                                            continue;
                                        }

                                        if (((p.IsKeyboardPlayer && !p.IsRawKeyboard && !p.IsRawMouse) || p.IsXInput || p.IsDInput || (p.IsRawKeyboard && !other.IsRawMouse) || (p.IsRawMouse && !other.IsRawKeyboard)) && (p.MonitorBounds.Y == other.MonitorBounds.Y || (p.MonitorBounds.Y < other.MonitorBounds.Height && p.MonitorBounds.Y > other.MonitorBounds.Y)))
                                        {
                                            hasLeftRightSpace = false;

                                        }
                                        if (((p.IsKeyboardPlayer && !p.IsRawKeyboard && !p.IsRawMouse) || p.IsXInput || p.IsDInput || (p.IsRawKeyboard && !other.IsRawMouse) || (p.IsRawMouse && !other.IsRawKeyboard)) && (p.MonitorBounds.X == other.MonitorBounds.X || (p.MonitorBounds.X < other.MonitorBounds.Width && p.MonitorBounds.X > other.MonitorBounds.X)))
                                        {
                                            hasTopBottomSpace = false;

                                        }

                                    }

                                    if (hasLeftRightSpace)
                                    {
                                        Rectangle edit = p.EditBounds;

                                        if (bounds.X == screen.MonitorBounds.X + bounds.Width)
                                        {
                                            bounds.X -= bounds.Width;
                                            edit.X -= edit.Width;
                                        }

                                        bounds.Width *= verLines;
                                        edit.Width *= verLines;

                                        p.EditBounds = edit;
                                        p.MonitorBounds = bounds;

                                        Invalidate();
                                    }
                                    else if (hasTopBottomSpace)
                                    {
                                        Rectangle edit = p.EditBounds;
                                        if (bounds.Y == screen.MonitorBounds.Y + bounds.Height)
                                        {
                                            bounds.Y -= bounds.Height;
                                            edit.Y -= edit.Height;
                                        }

                                        bounds.Height *= horLines;
                                        edit.Height *= horLines;

                                        p.EditBounds = edit;
                                        p.MonitorBounds = bounds;

                                        Invalidate();
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

            if (dragging )//&& GameProfile.PlayerIDs.Count == 0)
            {
                List<PlayerInfo> players = profile.PlayerData;

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

                        // bigger than 60% = major part inside this screen
                        if (pc > 0.6f)
                        {
                            float offset = s.Width * 0.05f;
                            // check if there's space available on this screen
                            List<PlayerInfo> playas = profile.PlayerData;
                            GetFreeSpace(i, out Rectangle? editor, out Rectangle? monitor, player);

                            if (editor != null)
                            {
                                draggingScreenRec = editor.Value;
                                draggingScreenBounds = monitor.Value;
                                draggingScreen = i;
                            }

                            break;
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
                    PlayerInfo p = profile.PlayerData[draggingIndex];
                    dragging = false;

                    if (draggingScreen != -1)
                    {
                        p.Owner = screens[draggingScreen];
                        p.ScreenIndex = draggingScreen;
                        p.MonitorBounds = draggingScreenBounds;
                        p.EditBounds = draggingScreenRec;
                        p.screenPriority = p.Owner.display.X + p.Owner.display.Y;
                        draggingScreen = -1;
                    }
                    else
                    {
                        // return to default position
                        p.Owner = null;
                        p.EditBounds = GetDefaultBounds(draggingIndex);
                        p.MonitorBounds = new Rectangle(0, 0, 0, 0);
                        p.screenPriority = -1;
                        p.ScreenIndex = -1;
                    }

                    UpdatePlayers(); // force a player update                    

                    Invalidate();
                }

            }

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

        public void RefreshAll()
        {
            Refresh();
        }

        public List<PlayerInfo> loadedProfilePlayers = new List<PlayerInfo>();
        private int playerProfile = 0;

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            if (totalBounds == Rectangle.Empty)//Avoid problems with "OnSizeChange" event.
            {             
                GameProfile.currentProfile.Reset();  
            }

            int gamepadCount = 0;

            for (int i = 0; i < screens.Length; i++)
            {
                UserScreen s = screens[i];

                if (GameProfile.OwnerType.Count > 0 && GameProfile.OwnerType.Count >= i)
                {
                    UserScreenType scrType = (UserScreenType)GameProfile.OwnerType[i];
                    s.Type = scrType;
                }

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

                List<UserScreen> screenPriority = screens.OrderBy(c => c.priority).ToList();

                if (screenPriority.Count > 1)
                {
                    StringFormat centerStr = new StringFormat(StringFormatFlags.NoClip);
                    centerStr.Alignment = StringAlignment.Center;

                    float rectDim = 10;
                    float ratio = ((float)s.UIBounds.Height / (float)rectDim) / 10;

                    Rectangle scrIndexRect = new Rectangle(s.UIBounds.Right - (int)(rectDim * ratio), s.UIBounds.Y, (int)(rectDim * ratio), (int)(rectDim * ratio));

                    foreach (UserScreen scr in screenPriority)
                    {
                        if (screens[i] == scr)
                            g.DrawString((screenPriority.IndexOf(scr) + 1).ToString(), new Font(customFont, 8.25f * ratio, FontStyle.Bold, GraphicsUnit.Pixel, 0), myBrush, scrIndexRect, centerStr);
                            g.DrawRectangle(PositionScreenPen, scrIndexRect);
                    }
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

            List<PlayerInfo> players = profile.PlayerData;

            if (loadedProfilePlayers.Count > 0 && (loadedProfilePlayers.Count == GameProfile.PlayerIDs.Count))
            {
                players = loadedProfilePlayers;
                playerProfile = 0;
                CanPlayUpdated(true, true);
             
                if (GameProfile.AutoPlay)
                {
                    Control mainform = this.TopLevelControl;
                    Control[] parent = mainform.Controls.Find("btn_Play", true);
                    foreach (Button btn_Play in parent)
                    {
                        btn_Play.PerformClick();
                        break;
                    }
           
                    return;
                }
            }
           
            if (players.Count == 0)
            {
                g.DrawString(game.Game.SupportsMultipleKeyboardsAndMice ? "Press a key or move a mouse" : "Waiting for controllers...", playerTextFont, myBrush, new PointF(10, 10));
            }
            else
            { 
                for (int i = 0; i < players.Count; i++)
                {        
                    PlayerInfo info = players[i];
                    string str = (i + 1).ToString();

                    if (GameProfile.PlayerIDs.Count > 0)
                    {
                        if (playerProfile < GameProfile.PlayerIDs.Count)
                        {
                            if (info.GamepadGuid == GameProfile.GamepadsGuid[playerProfile] && !loadedProfilePlayers.Contains(info))//!!!TESTER MULTI SCREEN!!!<= a l'air de fonctionner
                            {
                                info.IsInputUsed = true;
                                info.ScreenIndex = GameProfile.ScreenIndexes[playerProfile];
                                info.EditBounds = GameProfile.EditBounds[playerProfile];
                                info.Nickname = GameProfile.Nicknames[playerProfile];
                                info.PlayerID = GameProfile.PlayerIDs[playerProfile];
                                info.Owner = new UserScreen(GameProfile.OwnerDisplays[playerProfile]);
                                info.Owner.Type = (UserScreenType)GameProfile.OwnerType[playerProfile];
                                info.Owner.display = GameProfile.OwnerDisplays[playerProfile];
                                info.screenPriority = GameProfile.ScreenPrioritys[playerProfile];

                                GetFreeSpace(info.ScreenIndex, out Rectangle? editor, out Rectangle? monitor, info);//Not same screen than profile? Do this again!

                                if (editor != null)
                                {
                                    info.MonitorBounds = monitor.Value;
                                    info.EditBounds = editor.Value;
                                }

                                loadedProfilePlayers.Add(info);
                                playerProfile++;
                                Console.WriteLine(info.GamepadGuid);
                                Invalidate();
                            }
                        }
                    }

                    Rectangle s = info.EditBounds;

                    g.Clip = new Region(new RectangleF(s.X, s.Y, s.Width + 1, s.Height + 1));

                    Rectangle gamepadRect = RectangleUtil.ScaleAndCenter(xinputPic.Size, s);
                    
                    SizeF size = g.MeasureString(str, playerCustomFont);
                    PointF loc = RectangleUtil.Center(size, s);

                    if (gamePadPressed == -1)
                    {
                        g.ResetClip();
                        string msg = String.Empty;

                        if (game.Game.SupportsMultipleKeyboardsAndMice)
                        {
                            msg = (GameProfile.PlayerIDs.Count > 0) ? "Click Play!" : "Drag & Drop device(s) On Desired Screen(s) (Press A Key Or Move A Mouse)";
                        }
                        else
                        {
                            msg = (GameProfile.PlayerIDs.Count > 0) ? "Click Play!" : "Drag & Drop device(s) On Desired Screen(s)";
                        }

                        Brush brush = myBrush;
                        g.DrawString(msg, playerTextFont, brush, new PointF(10, 10));

                    }
                    else
                    {
                        if (gamePadPressed == info.GamepadId)
                        {
                            g.ResetClip();
                            g.DrawString("Gamepad " + (info.GamepadId + 1), playerTextFont, colors[info.GamepadId], new PointF(10, 10));
                        }
                    }

                    if (info.IsXInput)
                    {
                        loc.Y -= gamepadRect.Height * 0.2f;
                        var playerColor = colors[info.GamepadId];
                        gamepadCount++;
                        str = (GameProfile.Nicknames.Count > 0 && info.Nickname != null) ? info.Nickname : Convert.ToString(gamepadCount);

                        size = g.MeasureString(str, playerCustomFont);
                        loc = RectangleUtil.Center(size, s);
                        loc.Y -= 5;
                        info.IsInputUsed = true;
                        if (controllerIdentification)
                        {
                            g.DrawString(str, playerCustomFont, playerColor, loc);
                        }

                        if (gamePadPressed == info.GamepadId)
                        {
                            g.DrawImage(xinputPic, gamepadRect, 0, 0, xinputPic.Width, xinputPic.Height, GraphicsUnit.Pixel, flashImageAttributes);
                        }
                        else
                        {
                            g.DrawImage(xinputPic, gamepadRect);
                        }
                    }
                    else if (info.IsKeyboardPlayer && !info.IsRawKeyboard && !info.IsRawMouse)
                    {
                        g.DrawImage(keyboardPic, gamepadRect);
                    }
                    else if (info.IsRawKeyboard || info.IsRawMouse)
                    {
                        Image img = info.IsRawKeyboard ? protKeyboardPic : protoMousePic;

                        if (info.RawMouseDeviceHandle != IntPtr.Zero && info.RawKeyboardDeviceHandle != IntPtr.Zero)
                        {
                            if (info.ShouldFlash)
                            {
                                info.IsInputUsed = true;
                                g.DrawImage(img, gamepadRect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, flashImageAttributes);
                            }
                            else if (info.IsInputUsed)
                            {
                                g.DrawImage(img, gamepadRect);
                            }
                        }
                        else
                        {
                            Image virtualImg = info.IsRawKeyboard ? virtualKeyboardPic : virtualMousePic;

                            if (info.ShouldFlash)
                            {
                                info.IsInputUsed = true;
                                g.DrawImage(virtualImg, gamepadRect, 0, 0, virtualImg.Width, virtualImg.Height, GraphicsUnit.Pixel, flashImageAttributes);
                            }
                            else if (info.IsInputUsed)
                            {
                                g.DrawImage(virtualImg, gamepadRect);
                            }
                        }
                    }
                    else
                    {
                        loc.Y -= gamepadRect.Height * 0.2f;
                        var playerColor = colors[info.GamepadId];
                        gamepadCount++;
                        str = (GameProfile.Nicknames.Count > 0 && info.Nickname != null) ? info.Nickname : Convert.ToString(gamepadCount);
                        size = g.MeasureString(str, playerCustomFont);
                        loc = RectangleUtil.Center(size, s);
                        loc.Y -= 5;
                        info.IsInputUsed = true;

                        if (controllerIdentification)
                        {
                            g.DrawString(str, playerCustomFont, playerColor, loc);
                        }

                        if (gamePadPressed == info.GamepadId)
                        {
                            g.DrawImage(dinputPic, gamepadRect, 0, 0, dinputPic.Width, dinputPic.Height, GraphicsUnit.Pixel, flashImageAttributes);
                        }
                        else
                        {
                            g.DrawImage(dinputPic, gamepadRect);
                        }
                    }

                    if (info.ScreenIndex != -1)
                    {
                       
                        UserScreen screen = screens[info.ScreenIndex];

                        if ((s.Height + s.Y) - (screen.UIBounds.Height + screen.UIBounds.Y) == -1)
                        {
                            s.Height += 1;
                        }

                        g.Clip = new Region(new RectangleF(s.X, s.Y, s.Width + 1, s.Height + 1));

                        g.DrawRectangle(PositionPlayerScreenPen, s);
                    }

                    if (gamePadPressed != info.GamepadId)
                    {
                        g.FillEllipse(Brushes.Transparent, gamepadRect);
                    }
                }
            }

            g.ResetClip();

            if (dragging && draggingScreen != -1)
            {
                _draggingScreen = new Rectangle(draggingScreenRec.Right - draggingScreenRec.Height, draggingScreenRec.Y, draggingScreenRec.Height, draggingScreenRec.Height);
                g.DrawRectangle(PositionPlayerScreenPen, draggingScreenRec);
                g.DrawImage(draggingScreenImg, _draggingScreen);
            }

            UpdateScreens();
        }
    }
}
