using Games;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Coop;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;

namespace Nucleus.Gaming.Controls.SetupScreen
{
    internal class Draw
    {
        private static readonly IniFile themeIni = Globals.ThemeConfigFile;
        private static string theme = Globals.ThemeFolder;
        public static string customFont;

        private static Bitmap xinputPic;
        private static Bitmap dinputPic;
        private static Bitmap keyboardPic;
        private static Bitmap protoKeyboardPic;
        private static Bitmap protoMousePic;
        private static Bitmap virtualKeyboardPic;
        private static Bitmap virtualMousePic;
        private static Bitmap screenimg;
        private static Bitmap fullscreen;
        private static Bitmap horizontal2;
        private static Bitmap vertical2;
        private static Bitmap players4;
        private static Bitmap players6;
        private static Bitmap players8;
        private static Bitmap players16;
        private static Bitmap customLayout;
        private static Bitmap manualLayout;

        public static  Cursor hand_Cursor;
        public static Cursor default_Cursor;

        private static bool controllerIdentification;

        private static bool UseSetupScreenBorder;
        private static bool UseLayoutSelectionBorder;
        private static bool UseSetupScreenImage;

        internal static SolidBrush myBrush;
        internal static SolidBrush notEnoughPlyrsSBrush;
        private static SolidBrush tagBrush;
        private static SolidBrush sizerBrush;

        private static Pen PositionPlayerScreenPen;
        private static Pen PositionScreenPen;
        private static Pen ghostBoundsPen;
        private static Pen destEditBoundsPen;

        private static Brush[] colors;

        private static ImageAttributes flashImageAttributes;

        public static Font playerFont;
        public static Font playerCustomFont;
        public static Font playerTextFont;

        private static SetupScreenControl parent;
        private static UserGameInfo userGameInfo;

        public static void Initialize(SetupScreenControl _parent, UserGameInfo _userGameInfo, GameProfile _profile)
        {
            parent = _parent;
            userGameInfo = _userGameInfo;

            customFont = themeIni.IniReadValue("Font", "FontFamily");

            default_Cursor = new Cursor(theme + "cursor.ico");
            hand_Cursor = new Cursor(theme + "cursor_hand.ico");

            playerFont = new Font(customFont, 20.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            playerCustomFont = new Font(customFont, 16.0f, FontStyle.Bold, GraphicsUnit.Point, 0);
            playerTextFont = new Font(customFont, 9.0f, FontStyle.Regular, GraphicsUnit.Point, 0);

            string[] rgb_PositionControlsFontColor = themeIni.IniReadValue("Colors", "SetupScreenFont").Split(',');
            string[] rgb_PositionScreenColor = themeIni.IniReadValue("Colors", "SetupScreenBorder").Split(',');
            string[] rgb_PositionPlayerScreenColor = themeIni.IniReadValue("Colors", "SetupScreenPlayerBorder").Split(',');

            controllerIdentification = bool.Parse(themeIni.IniReadValue("Misc", "ControllerIdentificationOn"));
            UseSetupScreenBorder = bool.Parse(themeIni.IniReadValue("Misc", "UseSetupScreenBorder"));
            UseLayoutSelectionBorder = bool.Parse(themeIni.IniReadValue("Misc", "UseLayoutSelectionBorder"));
            UseSetupScreenImage = bool.Parse(themeIni.IniReadValue("Misc", "UseSetupScreenImage"));

            xinputPic = ImageCache.GetImage(theme + "xinput.png");
            dinputPic = ImageCache.GetImage(theme + "dinput.png");
            keyboardPic = ImageCache.GetImage(theme + "keyboard.png");
            protoKeyboardPic = ImageCache.GetImage(theme + "proto_keyboard.png");
            protoMousePic = ImageCache.GetImage(theme + "proto_mouse.png");
            virtualKeyboardPic = ImageCache.GetImage(theme + "virtual_keyboard.png");
            virtualMousePic = ImageCache.GetImage(theme + "virtual_mouse.png");
            screenimg = ImageCache.GetImage(theme + "screen.png");
            fullscreen = ImageCache.GetImage(theme + "fullscreen.png");
            horizontal2 = ImageCache.GetImage(theme + "2horizontal.png");
            vertical2 = ImageCache.GetImage(theme + "2vertical.png");
            players4 = ImageCache.GetImage(theme + "4players.png");
            players6 = ImageCache.GetImage(theme + "6players.png");
            players8 = ImageCache.GetImage(theme + "8players.png");
            players16 = ImageCache.GetImage(theme + "16players.png");
            customLayout = ImageCache.GetImage(theme + "customLayout.png");
            manualLayout = ImageCache.GetImage(theme + "manualLayout.png");
            PositionScreenPen = new Pen(Color.FromArgb(int.Parse(rgb_PositionScreenColor[0]), int.Parse(rgb_PositionScreenColor[1]), int.Parse(rgb_PositionScreenColor[2])), 1);
            PositionPlayerScreenPen = new Pen(Color.FromArgb(int.Parse(rgb_PositionPlayerScreenColor[0]), int.Parse(rgb_PositionPlayerScreenColor[1]), int.Parse(rgb_PositionPlayerScreenColor[2])), 1);
            myBrush = new SolidBrush(Color.FromArgb(int.Parse(rgb_PositionControlsFontColor[0]), int.Parse(rgb_PositionControlsFontColor[1]), int.Parse(rgb_PositionControlsFontColor[2])));
            tagBrush = new SolidBrush(Color.FromArgb(200, 0, 0, 0));
            sizerBrush = new SolidBrush(Color.FromArgb(50, 15, 220, 15));
            ghostBoundsPen = new Pen(Color.Red);
            notEnoughPlyrsSBrush = new SolidBrush(Color.FromArgb(255, 245, 4, 68));
            destEditBoundsPen = new Pen(Color.FromArgb(255, 15, 220, 15));

            colors = new Brush[]
            {
              Brushes.Red, Brushes.DodgerBlue, Brushes.LimeGreen, Brushes.Yellow,Brushes.SaddleBrown, Brushes.BlueViolet, Brushes.Aqua, Brushes.DarkOrange, Brushes.Silver,
              Brushes.Magenta, Brushes.SpringGreen, Brushes.Indigo, Brushes.Black, Brushes.White, Brushes.Bisque, Brushes.SkyBlue, Brushes.SeaGreen,Brushes.Wheat, Brushes.Crimson, Brushes.Turquoise, Brushes.Chocolate,
              Brushes.OrangeRed, Brushes.Olive, Brushes.DarkRed, Brushes.Lavender
            };

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

        }


        public static void UIScreens(Graphics g)
        {
            var screens = BoundsFunctions.screens;

            for (int i = 0; i < screens.Length; i++)
            {
                UserScreen s = screens[i];

                if (UseLayoutSelectionBorder)
                {
                    g.DrawRectangles(PositionScreenPen, new RectangleF[] { s.SwapTypeBounds});
                }

                if (UseSetupScreenImage)
                {
                    g.DrawImage(screenimg, s.UIBounds);
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
                    case UserScreenType.Manual:
                        g.DrawImage(manualLayout, s.SwapTypeBounds);
                        break;
                }

                if (UseSetupScreenBorder)
                {
                    g.DrawRectangles(PositionScreenPen, new RectangleF[] { s.UIBounds });
                }
            }
        }
  
        
        public static void UIDevices(Graphics g,PlayerInfo player)
        {
            string str = (player.GamepadId + 1).ToString();

            RectangleF s = player.EditBounds;

            g.Clip = new Region(new RectangleF(s.X, s.Y, s.Width + 2, s.Height + 1));

            Rectangle gamepadRect = RectangleUtil.ScaleAndCenter(keyboardPic.Size, new Rectangle((int)s.X, (int)s.Y, (int)s.Width, (int)s.Height));

            SizeF size = g.MeasureString(str, playerCustomFont);
            PointF loc = RectangleUtil.Center(size, s);
            Pen color = (player.GamepadId > colors.Count()) ? new Pen(Color.Magenta) : new Pen(colors[player.GamepadId]);

            if (player.ScreenIndex != -1 && player.MonitorBounds != Rectangle.Empty)
            {
                g.DrawRectangle(PositionPlayerScreenPen, new Rectangle((int)s.X + 1, (int)s.Y + 1, (int)s.Width, (int)s.Height));
            }

            if (player.IsXInput)
            {
                loc.Y -= gamepadRect.Height * 0.2f;
                var playerColor = colors[player.GamepadId];
                str = (player.GamepadId + 1).ToString();

                size = g.MeasureString(str, playerCustomFont);
                loc = RectangleUtil.Center(size, s);
                loc.Y -= 5;

                if (DevicesFunctions.PollXInputGamepad(player))
                {
                    DevicesFunctions.polling = true;
                    g.DrawImage(xinputPic, gamepadRect, 0, 0, xinputPic.Width, xinputPic.Height, GraphicsUnit.Pixel, flashImageAttributes);
                }
                else
                {
                    if(player.IsInputUsed)
                    g.DrawImage(xinputPic, gamepadRect);
                }

                if (controllerIdentification && player.IsInputUsed)
                    g.DrawString(str, playerCustomFont, playerColor, loc);

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
                    //grouped m&k profile player so add same picture as single k&m player
                    if (player.IsRawKeyboard && player.IsRawMouse)
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

                if (DevicesFunctions.PollDInputGamepad(player))
                {
                    g.DrawImage(dinputPic, gamepadRect, 0, 0, dinputPic.Width, dinputPic.Height, GraphicsUnit.Pixel, flashImageAttributes);
                }
                else
                {
                    g.DrawImage(dinputPic, gamepadRect);
                }
            }

        }


        public static void GhostBounds(Graphics g)
        {
            g.ResetClip();

            //Draw all the profile players bounds until they get filled
            for (int b = 0; b < GameProfile.GhostBounds.Count(); b++)
            {
                var ghostBounds = GameProfile.GhostBounds[b];

                Rectangle ghostMBounds = ghostBounds.Item1;
                RectangleF ghostEBounds = ghostBounds.Item2;

                if (GameProfile.Instance.DevicesList.All(p => p.MonitorBounds != ghostMBounds))
                {
                    string ghostTag = $"P{b + 1}: {GameProfile.ProfilePlayersList[b].Nickname}";

                    SizeF ghostTagSize = g.MeasureString(ghostTag, playerTextFont);
                    Point ghostTagLocation = new Point(((int)ghostEBounds.Left + (int)ghostEBounds.Width / 2) - ((int)ghostTagSize.Width / 2), (int)(ghostEBounds.Bottom + 1 - ghostTagSize.Height));
                    RectangleF ghostTagBack = new RectangleF(ghostTagLocation.X, ghostTagLocation.Y, ghostTagSize.Width, ghostTagSize.Height);
                    Rectangle ghostTagBorder = new Rectangle(ghostTagLocation.X, ghostTagLocation.Y, (int)ghostTagSize.Width, (int)ghostTagSize.Height);

                    g.FillRectangle(Brushes.DarkSlateGray, ghostTagBack);
                    g.DrawRectangles(ghostBoundsPen, new RectangleF[] { ghostEBounds });
                    g.DrawRectangle(ghostBoundsPen, ghostTagBorder);
                    g.DrawString(ghostTag, playerTextFont, Brushes.Orange, ghostTagLocation.X, ghostTagLocation.Y);
                }
            }
        }


        public static void SelectedPlayerBounds(Graphics g)
        {
            g.FillRectangle(sizerBrush, BoundsFunctions.selectedPlayer.EditBounds);
        }


        public static void InputsText(Graphics g)
        {
            var inputText = GetInputText();
            g.DrawString(inputText.Item1, playerTextFont, inputText.Item2, new PointF(10, 10));
        }


        private static (string, Brush) GetInputText()
        {
            Brush brush = myBrush;

            string msg = string.Empty;

            if (GameProfile.Loaded)
            {
                if (GameProfile.TotalAssignedPlayers > GameProfile.TotalProfilePlayers)
                {
                    msg = $"There Is Too Much Players!";
                    brush = notEnoughPlyrsSBrush;
                }
                else if ((GameProfile.TotalProfilePlayers - GameProfile.TotalAssignedPlayers) > 0)
                {
                    string st = GameProfile.GamepadCount > 1 ? "Controllers" : "Controller";
                    string sc = GameProfile.AllScreens.Count() > 1 ? "Screens" : "Screen";
                    msg = $"{GameProfile.GamepadCount} {st}, {GameProfile.KeyboardCount} K&M And {GameProfile.AllScreens.Count()} {sc}, Were Used Last Time.";
                    brush = notEnoughPlyrsSBrush;
                }
                else if (GameProfile.TotalProfilePlayers == GameProfile.TotalAssignedPlayers)
                {
                    msg = $"Profile Ready!";
                }
            }
            else
            {
                string screenText = BoundsFunctions.screens.Length > 1 ? "On The Desired Screens" : "On The Screen";

                if (userGameInfo.Game.SupportsMultipleKeyboardsAndMice)
                {
                    msg = $"Press A Key\\Button On Each Device & Drop Them {screenText}.";

                }
                else if (!userGameInfo.Game.SupportsMultipleKeyboardsAndMice && !userGameInfo.Game.SupportsKeyboard)
                {
                    if (DevicesFunctions.UseGamepadApiIndex || parent.profileDisabled)
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
                    if (DevicesFunctions.UseGamepadApiIndex || parent.profileDisabled)
                    {
                        msg = $"Drop Gamepads Or Keyboard & Mouse {screenText}.";
                    }
                    else
                    {
                        msg = $"Press A Button On Each Gamepad & Drop Devices {screenText}.";
                    }
                }
            }

            return (msg, brush);
        }


        public static void DestinationBounds(Graphics g)
        {
            g.DrawRectangles(destEditBoundsPen, new RectangleF[] { BoundsFunctions.destEditBounds });
        }

        public static void PlayerBoundsInfo(Graphics g)
        {
            g.DrawString(BoundsFunctions.CalculAspectRatio(BoundsFunctions.selectedPlayer), playerTextFont, Brushes.White, parent.Left + 10, parent.Height - 40);

            //g.DrawRectangles(PositionScreenPen, new RectangleF[] { BoundsFunctions.sizerBtnLeft});
            //g.DrawRectangles(PositionScreenPen, new RectangleF[] { BoundsFunctions.sizerBtnRight });
            //g.DrawRectangles(PositionScreenPen, new RectangleF[] { BoundsFunctions.sizerBtnTop});
            //g.DrawRectangles(PositionScreenPen, new RectangleF[] { BoundsFunctions.sizerBtnBottom});
        }


        public static void NoPlayerText(Graphics g)
        {
            g.DrawString("Waiting For Compatible Devices...", playerTextFont, myBrush, new PointF(10, 10));
        }


        public static void PlayerTag(Graphics g, PlayerInfo player)
        {
            RectangleF s = player.EditBounds;

            g.Clip = new Region(new RectangleF(s.X, s.Y, s.Width + 1, s.Height + 1));
            int playerIndex = GameProfile.loadedProfilePlayers.FindIndex(pl => pl == player);

            string tag = $"P{playerIndex + 1}: {player.Nickname}";

            SizeF tagSize = g.MeasureString(tag, playerTextFont);
            Point tagLocation = new Point(((int)s.Left + (int)s.Width / 2) - ((int)tagSize.Width / 2), (int)(s.Bottom + 1 - tagSize.Height));
            RectangleF tagBack = new RectangleF(tagLocation.X, tagLocation.Y, tagSize.Width, tagSize.Height);
            Rectangle tagBorder = new Rectangle(tagLocation.X, tagLocation.Y, (int)tagSize.Width, (int)tagSize.Height);

            g.Clip = new Region(tagBack);

            g.FillRectangle(tagBrush, tagBack);
            g.DrawRectangle(PositionScreenPen, tagBorder);
            g.DrawString(tag, playerTextFont, Brushes.GreenYellow, tagLocation.X, tagLocation.Y);
        }

    }
}
