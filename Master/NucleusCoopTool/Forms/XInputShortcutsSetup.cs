using Nucleus.Gaming;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Controls;
using Nucleus.Gaming.Coop.InputManagement.Gamepads;
using Nucleus.Gaming.Tools.GlobalWindowMethods;
using Nucleus.Gaming.UI;
using Nucleus.Gaming.Windows.Interop;
using SharpDX.XInput;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Windows.Forms;

namespace Nucleus.Coop.Forms
{
    public partial class XInputShortcutsSetup : Form, IDynamicSized
    {
        private IniFile ini = Globals.ini;
        private Bitmap controllerTop;
        private Bitmap controllerFront;
        private PictureBox activeControl;
        private Timer refreshTimer = new Timer();
        private float scale;
        private Cursor default_Cursor;
        private Cursor hand_Cursor;
        private SolidBrush brush;
        public static new bool Enabled;
        private string gamepadType;
        private RectangleF top;
        private RectangleF front;

        public XInputShortcutsSetup()
        {
            InitializeComponent();
            bool roundedcorners = bool.Parse(Globals.ThemeConfigFile.IniReadValue("Misc", "UseRoundedCorners"));
            gamepadType = ini.IniReadValue("XUINav", "Type");

            default_Cursor = Theme_Settings.Default_Cursor;
            hand_Cursor = Theme_Settings.Hand_Cursor;
            Cursor = default_Cursor;

            shortContainer.Cursor = default_Cursor;
            enabled_chk.Cursor = hand_Cursor;

            refreshTimer.Tick += new EventHandler(RefreshTimerTick);
            refreshTimer.Interval = 500;
            refreshTimer.Start();

            BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "xinput_background.jpg");
            BackgroundImageLayout = ImageLayout.Stretch;

            controllerTop = Nucleus.Coop.Properties.Resources.xboxControllerTop;
            controllerFront = Nucleus.Coop.Properties.Resources.xboxControllerFront;
            Close.BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "title_close.png");

            CustomToolTips.SetToolTip(enabled_chk, "Automatically locked when all instances are set and ready.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });

            brush = new SolidBrush(Color.FromArgb(120, 0, 255, 60));
            switch15.KeyPress += new KeyPressEventHandler(this.Num_KeyPress);

            if (roundedcorners)
            {
                Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
            }

            if (ini.IniReadValue("XShortcuts", "SetFocus").Contains('+'))
            {
                string[] SetFocus = ini.IniReadValue("XShortcuts", "SetFocus").Split('+');
                switch1.Tag = int.Parse(SetFocus[0]);
                slave1.Tag = int.Parse(SetFocus[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "SetFocus", "");
            }

            if (ini.IniReadValue("XShortcuts", "Close").Contains('+'))
            {
                string[] Close = ini.IniReadValue("XShortcuts", "Close").Split('+');
                switch2.Tag = int.Parse(Close[0]);
                slave2.Tag = int.Parse(Close[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "Close", "");
            }

            if (ini.IniReadValue("XShortcuts", "Stop").Contains('+'))
            {
                string[] Stop = ini.IniReadValue("XShortcuts", "Stop").Split('+');
                switch3.Tag = int.Parse(Stop[0]);
                slave3.Tag = int.Parse(Stop[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "Stop", "");
            }

            if (ini.IniReadValue("XShortcuts", "TopMost").Contains('+'))
            {
                string[] TopMost = ini.IniReadValue("XShortcuts", "TopMost").Split('+');
                switch4.Tag = int.Parse(TopMost[0]);
                slave4.Tag = int.Parse(TopMost[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "TopMost", "");
            }

            if (ini.IniReadValue("XShortcuts", "ResetWindows").Contains('+'))
            {
                string[] ResetWindows = ini.IniReadValue("XShortcuts", "ResetWindows").Split('+');
                switch5.Tag = int.Parse(ResetWindows[0]);
                slave5.Tag = int.Parse(ResetWindows[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "ResetWindows", "");
            }

            if (ini.IniReadValue("XShortcuts", "Cutscenes").Contains('+'))
            {
                string[] Cutscenes = ini.IniReadValue("XShortcuts", "Cutscenes").Split('+');
                switch6.Tag = int.Parse(Cutscenes[0]);
                slave6.Tag = int.Parse(Cutscenes[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "Cutscenes", "");
            }

            if (ini.IniReadValue("XShortcuts", "Switch").Contains('+'))
            {
                string[] Switch = ini.IniReadValue("XShortcuts", "Switch").Split('+');
                switch7.Tag = int.Parse(Switch[0]);
                slave7.Tag = int.Parse(Switch[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "Switch", "");
            }

            if (ini.IniReadValue("XShortcuts", "LockInputs").Contains('+'))
            {
                string[] LockInputs = ini.IniReadValue("XShortcuts", "LockInputs").Split('+');
                switch8.Tag = int.Parse(LockInputs[0]);
                slave8.Tag = int.Parse(LockInputs[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "LockInputs", "");
            }

            if (ini.IniReadValue("XShortcuts", "ReleaseCursor").Contains('+'))
            {
                string[] ReleaseCursor = ini.IniReadValue("XShortcuts", "ReleaseCursor").Split('+');
                switch9.Tag = int.Parse(ReleaseCursor[0]);
                slave9.Tag = int.Parse(ReleaseCursor[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "LockInputs", "");
            }

            if (ini.IniReadValue("XUINav", "LockUIControl").Contains('+'))
            {
                string[] LockUIControl = ini.IniReadValue("XUINav", "LockUIControl").Split('+');
                switch10.Tag = int.Parse(LockUIControl[0]);
                slave10.Tag = int.Parse(LockUIControl[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "LockInputs", "");
            }

            if (ini.IniReadValue("XUINav", "LockUIControl").Contains('+'))
            {
                string[] OpenOsk = ini.IniReadValue("XUINav", "OpenOsk").Split('+');
                switch11.Tag = int.Parse(OpenOsk[0]);
                slave11.Tag = int.Parse(OpenOsk[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "LockInputs", "");
            }

            enabled_chk.Checked = bool.Parse(ini.IniReadValue("XUINav", "Enabled"));
            CustomToolTips.SetToolTip(enabled_chk, "Requires admin rights for full controller support.", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });

            switch12.Tag = int.Parse(ini.IniReadValue("XUINav", "DragDrop"));
            switch13.Tag = int.Parse(ini.IniReadValue("XUINav", "RightClick"));
            switch14.Tag = int.Parse(ini.IniReadValue("XUINav", "LeftClick"));
            switch15.Text = ini.IniReadValue("XUINav", "Deadzone");

            foreach (RadioButton rb in groupBoxType.Controls)
            {
                if (rb.Tag.ToString() == gamepadType)
                {
                    rb.Checked = true;
                }
            }

            foreach (Control c in shortContainer.Controls)
            {
                if (c is PictureBox)
                {
                    PictureBox pictureBox = (PictureBox)c;
                    pictureBox.BackgroundImageLayout = ImageLayout.Stretch;
                    pictureBox.MouseClick += new MouseEventHandler(SetActiveControl);
                    pictureBox.BackgroundImage = GamepadButtons.Image((int)pictureBox.Tag, gamepadType);
                    pictureBox.Cursor = hand_Cursor;
                }
            }

            foreach (Control c in UINavContainer.Controls)
            {
                if (c is PictureBox)
                {
                    PictureBox pictureBox = (PictureBox)c;
                    pictureBox.BackgroundImageLayout = ImageLayout.Stretch;
                    pictureBox.MouseClick += new MouseEventHandler(SetActiveControl);
                    pictureBox.BackgroundImage = GamepadButtons.Image((int)pictureBox.Tag, gamepadType);
                    pictureBox.Cursor = hand_Cursor;
                }

                if (c.Name == "switch15")
                {
                    c.KeyPress += new KeyPressEventHandler(Num_KeyPress);
                    CustomToolTips.SetToolTip(c, "If the cursor move without touching the\r\nleft stick set a higher value(steps of +1000).", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
                }
            }

            reminderPict.BackgroundImage = GamepadButtons.Image(1024, gamepadType);//Guide

            Rectangle area = Screen.PrimaryScreen.Bounds;
            if (ini.IniReadValue("Misc", "GamepadSettingsLocation") != "")
            {
                string[] windowLocation = ini.IniReadValue("Misc", "GamepadSettingsLocation").Split('X');

                if (ScreensUtil.AllScreens().All(s => !s.MonitorBounds.Contains(int.Parse(windowLocation[0]), int.Parse(windowLocation[1]))))
                {
                    CenterToScreen();
                }
                else
                {
                    Location = new Point(area.X + int.Parse(windowLocation[0]), area.Y + int.Parse(windowLocation[1]));
                }
            }
            else
            {
                CenterToScreen();
            }

            DPIManager.Register(this);
            DPIManager.Update(this);
        }

        public void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }

            this.scale = scale;

            float newFontSize = 7.25F * scale;

            foreach (Control c in shortContainer.Controls)
            {
                if (c is Label)
                {
                    if (c.Text != ("+"))
                    {
                        c.Location = new Point(switch1.Left - c.Width, c.Location.Y);
                    }
                }
            }

            foreach (Control c in UINavContainer.Controls)
            {
                if (scale > 1.0F)
                {
                    if (c is TextBox)
                    {
                        c.Font = new Font(c.Font.FontFamily, newFontSize, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                    }
                }

                if (c is Label)
                {
                    if (c.Text != ("+"))
                    {
                        c.Location = new Point(switch10.Left - c.Width, c.Location.Y);
                    }
                }
            }
        }

        private void RefreshTimerTick(Object Object, EventArgs EventArgs)
        {
            if (Visible)
                gamepadTopFront.Invalidate();
        }

        private void GamepadTopFront_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            float factor = 1.5F;

            //Draw top & front picture
            float topWidth = (controllerTop.Width / factor) * scale;
            float topHeight = (controllerTop.Height / factor) * scale;
            float frontWidth = (controllerFront.Width / factor) * scale;
            float frontHeight = (controllerFront.Height / factor) * scale;


            Vector2 p1 = new Vector2(gamepadTopFront.Location.X, gamepadTopFront.Location.Y);
            Vector2 p2 = new Vector2(this.Width, gamepadTopFront.Location.X);
            Vector2 size1 = Vector2.Subtract(p2, p1);
            float locX = size1.X / 2 - topWidth / 2;

            top = new RectangleF(locX, 10, topWidth, topHeight);
            front = new RectangleF(top.X, top.Bottom + 20, frontWidth, frontHeight);

            g.DrawImage(controllerTop, top);
            g.DrawImage(controllerFront, front);

            float radius = 25;
            float offset = ((radius / 2) / factor) * scale;
            float rec = (30 / factor) * scale;
            RectangleF Guide = new RectangleF((front.X + (125 / factor) * scale) - offset, (front.Y + (46 / factor) * scale) - offset, rec, rec);
            RectangleF RB = new RectangleF((top.X + (194 / factor) * scale) - offset, (top.Y + (64 / factor) * scale) - offset, rec, rec);
            RectangleF LB = new RectangleF((top.X + (56 / factor) * scale) - offset, (top.Y + (64 / factor) * scale) - offset, rec, rec);
            RectangleF RS = new RectangleF((front.X + (158 / factor) * scale) - offset, (front.Y + (85 / factor) * scale) - offset, rec, rec);
            RectangleF LS = new RectangleF((front.X + (56 / factor) * scale) - offset, (front.Y + (44 / factor) * scale) - offset, rec, rec);
            RectangleF A = new RectangleF((front.X + (193 / factor) * scale) - offset, (front.Y + (63 / factor) * scale) - offset, rec, rec);
            RectangleF B = new RectangleF((front.X + (211 / factor) * scale) - offset, (front.Y + (44 / factor) * scale) - offset, rec, rec);
            RectangleF X = new RectangleF((front.X + (175 / factor) * scale) - offset, (front.Y + (44 / factor) * scale) - offset, rec, rec);
            RectangleF Y = new RectangleF((front.X + (193 / factor) * scale) - offset, (front.Y + (26 / factor) * scale) - offset, rec, rec);
            RectangleF Back = new RectangleF((front.X + (100 / factor) * scale) - offset, (front.Y + (45 / factor) * scale) - offset, rec, rec);
            RectangleF Start = new RectangleF((front.X + (150 / factor) * scale) - offset, (front.Y + (45 / factor) * scale) - offset, rec, rec);

            RectangleF D_Up = new RectangleF((front.X + (91.5F / factor) * scale) - offset, (front.Y + (70 / factor) * scale) - offset, rec, rec);
            RectangleF D_Right = new RectangleF((front.X + (104 / factor) * scale) - offset, (front.Y + (85 / factor) * scale) - offset, rec, rec);
            RectangleF D_Down = new RectangleF((front.X + (91.5F / factor) * scale) - offset, (front.Y + (101 / factor) * scale) - offset, rec, rec);
            RectangleF D_Left = new RectangleF((front.X + (76 / factor) * scale) - offset, (front.Y + (85 / factor) * scale) - offset, rec, rec);
            RectangleF LT = new RectangleF((top.X + (59 / factor) * scale) - offset, (top.Y + (32 / factor) * scale) - offset, rec, rec);
            RectangleF RT = new RectangleF((top.X + (187 / factor) * scale) - offset, (top.Y + (32 / factor) * scale) - offset, rec, rec);

            RectangleF highlight = RectangleF.Empty;

            for (int i = 0; i < GamepadState.Controllers.Length; i++)
            {
                {////draw a circle at the pressed button location
                    int pressed = GamepadState.GetPressedButtons(i);
                    int rtValue = GamepadState.GetRightTriggerValue(i);
                    int rlValue = GamepadState.GetLeftTriggerValue(i);
                    bool isRT = false;
                    bool isRL = false;
                    bool isTRigger = false;

                    if (rtValue > 0)
                    {
                        isRT = true;
                        isTRigger = true;
                    }

                    if (rlValue > 0)
                    {
                        isRL = true;
                        isTRigger = true;
                    }

                    int.TryParse("None", out int noButtonPressed);
                    if (pressed == noButtonPressed && !isRL && !isRT)
                    {
                        continue;
                    }

                    switch (pressed)
                    {
                        case 1024://Guide button
                            {
                                highlight = Guide;
                                break;
                            }
                        case (int)GamepadButtonFlags.RightShoulder:
                            {
                                highlight = RB;
                                break;
                            }
                        case (int)GamepadButtonFlags.LeftShoulder:
                            {
                                highlight = LB;
                                break;
                            }
                        case (int)GamepadButtonFlags.RightThumb:
                            {
                                highlight = RS;
                                break;
                            }
                        case (int)GamepadButtonFlags.LeftThumb:
                            {
                                highlight = LS;
                                break;
                            }
                        case (int)GamepadButtonFlags.A:
                            {
                                highlight = A;
                                break;
                            }
                        case (int)GamepadButtonFlags.B:
                            {
                                highlight = B;
                                break;
                            }
                        case (int)GamepadButtonFlags.X:
                            {
                                highlight = X;
                                break;
                            }
                        case (int)GamepadButtonFlags.Y:
                            {
                                highlight = Y;
                                break;
                            }
                        case (int)GamepadButtonFlags.Back:
                            {
                                highlight = Back;
                                break;
                            }
                        case (int)GamepadButtonFlags.Start:
                            {
                                highlight = Start;
                                break;
                            }
                        case (int)GamepadButtonFlags.DPadUp:
                            {
                                highlight = D_Up;
                                break;
                            }
                        case (int)GamepadButtonFlags.DPadRight:
                            {
                                highlight = D_Right;
                                break;
                            }
                        case (int)GamepadButtonFlags.DPadDown:
                            {
                                highlight = D_Down;
                                break;
                            }
                        case (int)GamepadButtonFlags.DPadLeft:
                            {
                                highlight = D_Left;
                                break;
                            }
                    }

                    if (isRT)
                    {
                        highlight = RT;
                    }
                    else if (isRL)
                    {
                        highlight = LT;
                    }

                    g.ResetClip();

                    g.Clip = new Region(new RectangleF(highlight.X, highlight.Y, highlight.Width, highlight.Height));

                    g.FillEllipse(brush, highlight);

                    if (activeControl != null)
                    {
                        if (!isTRigger)
                        {
                            if ((int)pressed == noButtonPressed)
                            {
                                return;
                            }

                            activeControl.Tag = pressed;

                        }
                        else if (isRT)
                        {
                            activeControl.Tag = 9999;

                        }
                        else if (isRL)
                        {
                            activeControl.Tag = 10000;
                        }

                        activeControl.BackgroundImage = GamepadButtons.Image((int)activeControl.Tag, gamepadType);
                    }
                }
            }
        }

        private void Num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void SetActiveControl(object sender, EventArgs e)
        {
            if (!(sender is PictureBox))
            {
                return;
            }

            PictureBox pb = sender as PictureBox;

            if (activeControl != null)
            {
                activeControl.BorderStyle = BorderStyle.None;
            }

            pb.BorderStyle = BorderStyle.FixedSingle;
            activeControl = pb;
        }

        private void Enabled_chk_CheckedChanged(object sender, EventArgs e)
        {
            ini.IniWriteValue("XUINav", "Enabled", enabled_chk.Checked.ToString());
            GamepadNavigation.UpdateUINavSettings();
        }

        private void radioButtonXbox_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            if (radio.Checked)
            {
                ini.IniWriteValue("XUINav", "Type", radio.Tag.ToString());
                gamepadType = radio.Tag.ToString();
                UpdateGamepadType();
            }
        }

        private void RadioButtonPs_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;
            if (radio.Checked)
            {
                ini.IniWriteValue("XUINav", "Type", radio.Tag.ToString());
                gamepadType = radio.Tag.ToString();
                UpdateGamepadType();
            }
        }

        private void UpdateGamepadType()
        {
            foreach (Control c in shortContainer.Controls)
            {
                if (c is PictureBox)
                {
                    PictureBox pictureBox = (PictureBox)c;
                    pictureBox.BackgroundImage = GamepadButtons.Image((int)pictureBox.Tag, gamepadType);
                }
            }

            foreach (Control c in UINavContainer.Controls)
            {
                if (c is PictureBox)
                {
                    PictureBox pictureBox = (PictureBox)c;
                    pictureBox.BackgroundImage = GamepadButtons.Image((int)pictureBox.Tag, gamepadType);
                }
            }

            reminderPict.BackgroundImage = GamepadButtons.Image(1024, gamepadType);//Guide
        }

        private void Close_Click(object sender, EventArgs e)
        {
            #region take a picture of the controller shortcuts

            Color defColor = shortContainer.BackColor;

            try
            {
                shortContainer.BackColor = Color.Black;
                Graphics g = shortContainer.CreateGraphics();
                Bitmap bmp = new Bitmap(shortContainer.Width, shortContainer.Height);
                shortContainer.DrawToBitmap(bmp, new Rectangle(0, 0, shortContainer.Width, shortContainer.Height));

                string savePath = Path.Combine(Application.StartupPath, $@"gui\shortcuts");

                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                bmp.Save(Path.Combine(savePath, "XShortcutsReminder.jpeg"), ImageFormat.Jpeg);
                bmp.Dispose();
                g.Dispose();

                shortContainer.BackColor = defColor;
            }
            catch
            {
                shortContainer.BackColor = defColor;
            }

            #endregion

            ini.IniWriteValue("XShortcuts", "SetFocus", switch1.Tag.ToString() + "+" + slave1.Tag.ToString());
            ini.IniWriteValue("XShortcuts", "Close", switch2.Tag.ToString() + "+" + slave2.Tag.ToString());
            ini.IniWriteValue("XShortcuts", "Stop", switch3.Tag.ToString() + "+" + slave3.Tag.ToString());
            ini.IniWriteValue("XShortcuts", "TopMost", switch4.Tag.ToString() + "+" + slave4.Tag.ToString());
            ini.IniWriteValue("XShortcuts", "ResetWindows", switch5.Tag.ToString() + "+" + slave5.Tag.ToString());
            ini.IniWriteValue("XShortcuts", "Cutscenes", switch6.Tag.ToString() + "+" + slave6.Tag.ToString());
            ini.IniWriteValue("XShortcuts", "Switch", switch7.Tag.ToString() + "+" + slave7.Tag.ToString());
            ini.IniWriteValue("XShortcuts", "LockInputs", switch8.Tag.ToString() + "+" + slave8.Tag.ToString());
            ini.IniWriteValue("XShortcuts", "ReleaseCursor", switch9.Tag.ToString() + "+" + slave9.Tag.ToString());

            ini.IniWriteValue("XUINav", "LockUIControl", switch10.Tag.ToString() + "+" + slave10.Tag.ToString());
            ini.IniWriteValue("XUINav", "OpenOsk", switch11.Tag.ToString() + "+" + slave11.Tag.ToString());
            ini.IniWriteValue("XUINav", "DragDrop", switch12.Tag.ToString());
            ini.IniWriteValue("XUINav", "RightClick", switch13.Tag.ToString());
            ini.IniWriteValue("XUINav", "LeftClick", switch14.Tag.ToString());

            if (switch15.Text == "")
            {
                switch15.Text = "10000";
            }

            ini.IniWriteValue("XUINav", "Deadzone", switch15.Text);
            ini.IniWriteValue("Misc", "GamepadSettingsLocation", Location.X + "X" + Location.Y);

            GamepadShortcuts.UpdateShortcutsValue();
            GamepadNavigation.UpdateUINavSettings();

            Visible = false;
        }

        private void Close_MouseEnter(object sender, EventArgs e)
        {
            Close.BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "title_close_mousehover.png");
        }

        private void Close_MouseLeave(object sender, EventArgs e)
        {
            Close.BackgroundImage = ImageCache.GetImage(Globals.ThemeFolder + "title_close.png");
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        private void XInputShortcutsSetup_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                User32Interop.ReleaseCapture();
                IntPtr nucHwnd = User32Interop.FindWindow(null, Text);
                User32Interop.SendMessage(nucHwnd, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, (IntPtr)0);
            }
        }

        private void GamepadTopFront_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                User32Interop.ReleaseCapture();
                IntPtr nucHwnd = User32Interop.FindWindow(null, Text);
                User32Interop.SendMessage(nucHwnd, WM_NCLBUTTONDOWN, (IntPtr)HT_CAPTION, (IntPtr)0);
            }
        }
    }
}
