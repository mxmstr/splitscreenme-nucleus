using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Coop;
using Nucleus.Gaming.UI;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Controls.Primitives;
using System.Windows.Forms;
using System.Windows.Media.Media3D;

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

        private static bool controllerIdentification;

        private static bool UseSetupScreenBorder;
        private static bool UseLayoutSelectionBorder;
        private static bool UseSetupScreenImage;

        private static SolidBrush tagBrush;
        private static SolidBrush sizerBrush;
        private static SolidBrush screenBackBrush;

        private static Pen PositionPlayerScreenPen;
        private static Pen PositionScreenPen;
        private static Pen ghostBoundsPen;
        private static Pen destEditBoundsPen;

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

            playerFont = new Font(customFont, 20.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
            playerCustomFont = new Font("Vermin Vibes 2 Soft", 12.0f, FontStyle.Regular, GraphicsUnit.Point, 0);
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
            tagBrush = new SolidBrush(Color.FromArgb(200, 0, 0, 0));
            screenBackBrush = new SolidBrush(Color.FromArgb(60, 0, 0, 0));
            sizerBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0));
            ghostBoundsPen = new Pen(Color.Red);
            destEditBoundsPen = new Pen(Color.FromArgb(255, 15, 220, 15));

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

                if (s.Type != UserScreenType.Manual && s.Type != UserScreenType.FullScreen)
                {
                    try
                    {
                        var boundsToDraw = s.SubScreensBounds.Values.Where(sb => !GameProfile.Instance.DevicesList.Any(pl => pl.EditBounds.IntersectsWith(sb))).ToArray();

                        if (boundsToDraw.Length > 0)
                        {
                            g.DrawRectangles(PositionScreenPen, boundsToDraw);
                        }
                    }
                    catch
                    { }
                }

                bool intersect = false;
                RectangleF minimizedSwapType = new Rectangle();

                try
                {
                    var interstcWithSwapTypeBound = GameProfile.Instance.DevicesList.Where(dv => dv.EditBounds.IntersectsWith(s.SwapTypeBounds)).ToArray();
                    intersect = interstcWithSwapTypeBound.Length == 0 || s.SwapTypeBounds.Contains(BoundsFunctions.MousePos);
                    minimizedSwapType = new RectangleF(s.SwapTypeBounds.X, s.SwapTypeBounds.Y, s.SwapTypeBounds.Width / 2, s.SwapTypeBounds.Height / 2);
                }
                catch
                { }

                if (UseLayoutSelectionBorder)
                {
                    g.DrawRectangles(PositionScreenPen, new RectangleF[] { intersect ? s.SwapTypeBounds : minimizedSwapType });
                }

                if (UseSetupScreenImage)
                {
                    g.DrawImage(screenimg, s.UIBounds);
                }

                switch (s.Type)
                {
                    case UserScreenType.FullScreen:
                        g.DrawImage(fullscreen, intersect ? s.SwapTypeBounds : minimizedSwapType);
                        break;
                    case UserScreenType.DualHorizontal:
                        g.DrawImage(horizontal2, intersect ? s.SwapTypeBounds : minimizedSwapType);
                        break;
                    case UserScreenType.DualVertical:
                        g.DrawImage(vertical2, intersect ? s.SwapTypeBounds : minimizedSwapType);
                        break;
                    case UserScreenType.FourPlayers:
                        g.DrawImage(players4, intersect ? s.SwapTypeBounds : minimizedSwapType);
                        break;
                    case UserScreenType.SixPlayers:
                        g.DrawImage(players6, intersect ? s.SwapTypeBounds : minimizedSwapType);
                        break;
                    case UserScreenType.EightPlayers:
                        g.DrawImage(players8, intersect ? s.SwapTypeBounds : minimizedSwapType);
                        break;
                    case UserScreenType.SixteenPlayers:
                        g.DrawImage(players16, intersect ? s.SwapTypeBounds : minimizedSwapType);
                        break;
                    case UserScreenType.Custom:
                        g.DrawImage(customLayout, intersect ? s.SwapTypeBounds : minimizedSwapType);
                        break;
                    case UserScreenType.Manual:
                        g.DrawImage(manualLayout, intersect ? s.SwapTypeBounds : minimizedSwapType);
                        break;
                }

                if (UseSetupScreenBorder)
                {
                    g.DrawRectangles(PositionScreenPen, new RectangleF[] { s.UIBounds });
                }
            }
        }


        public static void UIDevices(Graphics g, PlayerInfo player)
        {
            RectangleF s = player.EditBounds;

            g.Clip = new Region(new RectangleF(s.X, s.Y, s.Width + 2, s.Height + 1));

            Rectangle gamepadRect = RectangleUtil.ScaleAndCenter(keyboardPic.Size, new Rectangle((int)s.X, (int)s.Y, (int)s.Width, (int)s.Height));

            float height = player.EditBounds.Height / 2.8f > 0 ? (player.EditBounds.Height / 2.8f) : 1 ;

            Font fontToScale = new Font(playerCustomFont.FontFamily, height, FontStyle.Regular, GraphicsUnit.Pixel);

            if (player.ScreenIndex != -1 && player.MonitorBounds != Rectangle.Empty)
            {
                g.DrawRectangle(PositionPlayerScreenPen, new Rectangle((int)s.X + 1, (int)s.Y + 1, (int)s.Width, (int)s.Height));
            }

            if (player.IsXInput)
            {
                string str = (player.GamepadId + 1).ToString();

                SizeF size = g.MeasureString(str, fontToScale);
                PointF loc = RectangleUtil.Center(size, s);
                loc.Y -= gamepadRect.Height * 0.10f;

                if (DevicesFunctions.PollXInputGamepad(player))
                {
                    DevicesFunctions.polling = true;
                    g.DrawImage(xinputPic, gamepadRect, 0, 0, xinputPic.Width, xinputPic.Height, GraphicsUnit.Pixel, flashImageAttributes);
                }
                else
                {
                    if (player.IsInputUsed)
                        g.DrawImage(xinputPic, gamepadRect);
                }

                if (controllerIdentification && player.IsInputUsed)
                {
                   g.DrawString(str, fontToScale, Brushes.White, loc);
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
                    if (player.ShouldFlash)
                    {
                        player.IsInputUsed = true;
                        g.DrawImage(img, gamepadRect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel, flashImageAttributes);
                    }
                    else if (player.IsInputUsed)
                    {                       
                        g.DrawImage(img, gamepadRect);
                    }

                    if (player.IsInputUsed)
                    { 
                        float virtualheight = player.EditBounds.Height / 4f > 0 ? (player.EditBounds.Height / 4f) : 1;
                        Font virtualfontToScale = new Font("Franklin Gothic", virtualheight, FontStyle.Regular, GraphicsUnit.Pixel);
                        string str = "virtual";

                        SizeF size = g.MeasureString(str, virtualfontToScale);
                        PointF loc = RectangleUtil.Center(size, s);
                        loc.Y -= gamepadRect.Height * 0.10f;

                        RectangleF gradientBrushbounds = new RectangleF(loc.X, loc.Y, size.Width, size.Height);
                        RectangleF bounds = new RectangleF(loc.X, loc.Y, size.Width, size.Height);

                        Color vcolor = Color.FromArgb(150, 0, 0, 0);
                        Color vcolor2 = Color.FromArgb(255, 0, 0, 0);

                        LinearGradientBrush lgb =
                        new LinearGradientBrush(gradientBrushbounds, vcolor2, vcolor, 90f);

                        ColorBlend topcblend = new ColorBlend(4);
                        topcblend.Colors = new Color[3] { vcolor, vcolor2, vcolor};
                        topcblend.Positions = new float[3] { 0f, 0.8f, 1f };

                        lgb.InterpolationColors = topcblend;

                        g.FillRectangle(lgb, bounds);
                        g.DrawString(str, virtualfontToScale, Brushes.YellowGreen,loc);

                        virtualfontToScale.Dispose();
                        lgb.Dispose();
                    }
                }
            }
            else
            {
                string str = (player.GamepadId + 1).ToString();
                SizeF size = g.MeasureString(str, fontToScale);
                PointF loc = RectangleUtil.Center(size, gamepadRect);
                loc.Y -= gamepadRect.Height * 0.12f;

                if (DevicesFunctions.PollDInputGamepad(player))
                {
                    g.DrawImage(dinputPic, gamepadRect, 0, 0, dinputPic.Width, dinputPic.Height, GraphicsUnit.Pixel, flashImageAttributes);
                }
                else
                {
                    g.DrawImage(dinputPic, gamepadRect);
                }

                if (controllerIdentification)
                {
                    g.DrawString(str, fontToScale, Brushes.White, loc);
                }
            }

            fontToScale.Dispose();
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

        public static void DestinationBounds(Graphics g)
        {
            g.DrawRectangles(destEditBoundsPen, new RectangleF[] { BoundsFunctions.destEditBounds });
        }

        public static void PlayerBoundsInfo(Graphics g)
        {
            g.DrawString(BoundsFunctions.PlayerBoundsInfoText(BoundsFunctions.selectedPlayer), playerTextFont, Brushes.White, parent.Left + 10, parent.Height - 40);

            //g.FillRectangles(sizerBrush, new RectangleF[] { BoundsFunctions.sizerBtnLeft });
            //g.FillRectangles(sizerBrush, new RectangleF[] { BoundsFunctions.sizerBtnRight });
            //g.FillRectangles(sizerBrush, new RectangleF[] { BoundsFunctions.sizerBtnTop });
            //g.FillRectangles(sizerBrush, new RectangleF[] { BoundsFunctions.sizerBtnBottom });
        }

        public static void PlayerTag(Graphics g, PlayerInfo player)
        {
            RectangleF s = player.EditBounds;

            g.Clip = new Region(new RectangleF(s.X, s.Y, s.Width + 1, s.Height + 1));
            int playerIndex = GameProfile.AssignedDevices.FindIndex(pl => pl == player);

            string tag = $"P{playerIndex + 1}: {player.Nickname}";

            SizeF tagSize = g.MeasureString(tag, playerTextFont);
            Point tagLocation = new Point(((int)s.Left + (int)s.Width / 2) - ((int)tagSize.Width / 2), (int)(s.Bottom + 1 - tagSize.Height));
            RectangleF tagBack = new RectangleF(tagLocation.X, tagLocation.Y, tagSize.Width, tagSize.Height);
            Rectangle tagBorder = new Rectangle(tagLocation.X, tagLocation.Y, (int)tagSize.Width, (int)tagSize.Height);

            g.Clip = new Region(tagBack);

            g.FillRectangle(tagBrush, tagBack);
            g.DrawRectangle(PositionScreenPen, tagBorder);
            g.DrawString(tag, playerTextFont, Brushes.White, tagLocation.X, tagLocation.Y);
        }

    }
}
