using Nucleus.Gaming;
using Nucleus.Gaming.Coop.InputManagement;
using Nucleus.Gaming.Tools.GlobalWindowMethods;
using SharpDX.XInput;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Nucleus.Coop.Forms
{
    public partial class XInputShortcutsSetup : Form, IDynamicSized
    {
        private IniFile ini = Globals.ini;
        private Bitmap controllerTop;
        private Bitmap controllerFront;
        private TextBox activeControl;
        private Timer refreshTimer = new Timer();
        private float scale;
        private Cursor default_Cursor;
        private Cursor hand_Cursor;
        private SolidBrush brush;
        public static new bool Enabled;

        public XInputShortcutsSetup()
        {
            InitializeComponent();
            bool roundedcorners = bool.Parse(Globals.ThemeIni.IniReadValue("Misc", "UseRoundedCorners"));

            default_Cursor = new Cursor(Globals.Theme + "cursor.ico");
            hand_Cursor = new Cursor(Globals.Theme + "cursor_hand.ico");
            Cursor.Current = default_Cursor;
            Cursor = default_Cursor;
            shortContainer.Cursor = default_Cursor;
            refreshTimer.Tick += new EventHandler(RefreshTimerTick);
            refreshTimer.Interval = 50;
            refreshTimer.Start();
            controllerTop = new Bitmap(Nucleus.Coop.Properties.Resources.xboxControllerTop);
            controllerFront = new Bitmap(Nucleus.Coop.Properties.Resources.xboxControllerFront);
            Close.BackgroundImage = new Bitmap(Globals.Theme + "title_close.png");
            brush = new SolidBrush(Color.FromArgb(90, 0, 255, 60));
            deadzone_txt.KeyPress += new KeyPressEventHandler(this.num_KeyPress);

            if (roundedcorners)
            {
                Region = Region.FromHrgn(GlobalWindowMethods.CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            }
            if (ini.IniReadValue("XShortcuts", "SetFocus").Contains('+'))
            {
                string[] SetFocus = ini.IniReadValue("XShortcuts", "SetFocus").Split('+');
                switch1.Text = ((GamepadButtonFlags)Convert.ToInt32(SetFocus[0])).ToString();
                switch1.Tag = int.Parse(SetFocus[0]);
                slave1.Text = ((GamepadButtonFlags)Convert.ToInt32(SetFocus[1])).ToString();
                slave1.Tag = int.Parse(SetFocus[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "SetFocus", "");
            }

            if (ini.IniReadValue("XShortcuts", "Close").Contains('+'))
            {
                string[] Close = ini.IniReadValue("XShortcuts", "Close").Split('+');
                switch2.Text = ((GamepadButtonFlags)Convert.ToInt32(Close[0])).ToString();
                switch2.Tag = int.Parse(Close[0]);
                slave2.Text = ((GamepadButtonFlags)Convert.ToInt32(Close[1])).ToString();
                slave2.Tag = int.Parse(Close[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "Close", "");
            }

            if (ini.IniReadValue("XShortcuts", "Stop").Contains('+'))
            {
                string[] Stop = ini.IniReadValue("XShortcuts", "Stop").Split('+');
                switch3.Text = ((GamepadButtonFlags)Convert.ToInt32(Stop[0])).ToString();
                switch3.Tag = int.Parse(Stop[0]);
                slave3.Text = ((GamepadButtonFlags)Convert.ToInt32(Stop[1])).ToString();
                slave3.Tag = int.Parse(Stop[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "Stop", "");
            }

            if (ini.IniReadValue("XShortcuts", "TopMost").Contains('+'))
            {
                string[] TopMost = ini.IniReadValue("XShortcuts", "TopMost").Split('+');
                switch4.Text = ((GamepadButtonFlags)Convert.ToInt32(TopMost[0])).ToString();
                switch4.Tag = int.Parse(TopMost[0]);
                slave4.Text = ((GamepadButtonFlags)Convert.ToInt32(TopMost[1])).ToString();
                slave4.Tag = int.Parse(TopMost[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "TopMost", "");
            }

            if (ini.IniReadValue("XShortcuts", "ResetWindows").Contains('+'))
            {
                string[] ResetWindows = ini.IniReadValue("XShortcuts", "ResetWindows").Split('+');
                switch5.Text = ((GamepadButtonFlags)Convert.ToInt32(ResetWindows[0])).ToString();
                switch5.Tag = int.Parse(ResetWindows[0]);
                slave5.Text = ((GamepadButtonFlags)Convert.ToInt32(ResetWindows[1])).ToString();
                slave5.Tag = int.Parse(ResetWindows[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "ResetWindows", "");
            }

            if (ini.IniReadValue("XShortcuts", "Cutscenes").Contains('+'))
            {
                string[] Cutscenes = ini.IniReadValue("XShortcuts", "Cutscenes").Split('+');
                switch6.Text = ((GamepadButtonFlags)Convert.ToInt32(Cutscenes[0])).ToString();
                switch6.Tag = int.Parse(Cutscenes[0]);
                slave6.Text = ((GamepadButtonFlags)Convert.ToInt32(Cutscenes[1])).ToString();
                slave6.Tag = int.Parse(Cutscenes[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "Cutscenes", "");
            }

            if (ini.IniReadValue("XShortcuts", "Switch").Contains('+'))
            {
                string[] Switch = ini.IniReadValue("XShortcuts", "Switch").Split('+');
                switch7.Text = ((GamepadButtonFlags)Convert.ToInt32(Switch[0])).ToString();
                switch7.Tag = int.Parse(Switch[0]);
                slave7.Text = ((GamepadButtonFlags)Convert.ToInt32(Switch[1])).ToString();
                slave7.Tag = int.Parse(Switch[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "Switch", "");
            }

            if (ini.IniReadValue("XShortcuts", "LockInputs").Contains('+'))
            {
                string[] LockInputs = ini.IniReadValue("XShortcuts", "LockInputs").Split('+');
                switch8.Text = ((GamepadButtonFlags)Convert.ToInt32(LockInputs[0])).ToString();
                switch8.Tag = int.Parse(LockInputs[0]);
                slave8.Text = ((GamepadButtonFlags)Convert.ToInt32(LockInputs[1])).ToString();
                slave8.Tag = int.Parse(LockInputs[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "LockInputs", "");
            }

            if (ini.IniReadValue("XShortcuts", "ReleaseCursor").Contains('+'))
            {
                string[] ReleaseCursor = ini.IniReadValue("XShortcuts", "ReleaseCursor").Split('+');
                switch9.Text = ((GamepadButtonFlags)Convert.ToInt32(ReleaseCursor[0])).ToString();
                switch9.Tag = int.Parse(ReleaseCursor[0]);
                slave9.Text = ((GamepadButtonFlags)Convert.ToInt32(ReleaseCursor[1])).ToString();
                slave9.Tag = int.Parse(ReleaseCursor[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "LockInputs", "");
            }

            if (ini.IniReadValue("XUINav", "LockUIControl").Contains('+'))
            {
                string[] LockUIControl = ini.IniReadValue("XUINav", "LockUIControl").Split('+');
                switch10.Text = ((GamepadButtonFlags)Convert.ToInt32(LockUIControl[0])).ToString();
                switch10.Tag = int.Parse(LockUIControl[0]);
                slave10.Text = ((GamepadButtonFlags)Convert.ToInt32(LockUIControl[1])).ToString();
                slave10.Tag = int.Parse(LockUIControl[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "LockInputs", "");
            }

            if (ini.IniReadValue("XUINav", "LockUIControl").Contains('+'))
            {
                string[] OpenOsk = ini.IniReadValue("XUINav", "OpenOsk").Split('+');
                switch11.Text = ((GamepadButtonFlags)Convert.ToInt32(OpenOsk[0])).ToString();
                switch11.Tag = int.Parse(OpenOsk[0]);
                slave11.Text = ((GamepadButtonFlags)Convert.ToInt32(OpenOsk[1])).ToString();
                slave11.Tag = int.Parse(OpenOsk[1]);
            }
            else
            {
                ini.IniWriteValue("XShortcuts", "LockInputs", "");
            }
            enabled_chk.Checked = bool.Parse(ini.IniReadValue("XUINav", "Enabled"));
            deadzone_txt.Text = ini.IniReadValue("XUINav", "Deadzone");
            dragdrop_txt.Text = ((GamepadButtonFlags)Convert.ToInt32(ini.IniReadValue("XUINav", "DragDrop"))).ToString();
            dragdrop_txt.Tag = int.Parse(ini.IniReadValue("XUINav", "DragDrop"));
            rclick_txt.Text = ((GamepadButtonFlags)Convert.ToInt32(ini.IniReadValue("XUINav", "RightClick"))).ToString();
            rclick_txt.Tag = int.Parse(ini.IniReadValue("XUINav", "RightClick"));
            lclick_txt.Text = ((GamepadButtonFlags)Convert.ToInt32(ini.IniReadValue("XUINav", "LeftClick"))).ToString();
            lclick_txt.Tag = int.Parse(ini.IniReadValue("XUINav", "LeftClick"));

            foreach (Control c in shortContainer.Controls)
            {
                if (c.Text == "1024"){
                    c.Text = "Guide";
                }
                else if (c.Text == "9999")
                {
                    c.Text = "RightTrigger";
                }
                else if (c.Text == "10000")
                {
                    c.Text = "LeftTrigger";
                }

                if (c.Text.Length > 5)
                {
                    if (c.GetType() == typeof(TextBox))
                    {
                        TextBox textBox = c as TextBox;
                        textBox.CharacterCasing = CharacterCasing.Normal;
                        //c.Font = new Font(textBox.Font.FontFamily,textBox.Font.Size - 1.5F);
                    }
                }
                c.Cursor = hand_Cursor;
            }

            foreach (Control c in UINavContainer.Controls)
            {
                if (c.Text == "1024")
                {
                    c.Text = "Guide";
                }
                else if(c.Text == "9999")
                {
                    c.Text = "RightTrigger";
                }
                else if (c.Text == "10000")
                {
                    c.Text = "LeftTrigger";
                }

                if (c.Name != "deadzone_txt")
                {
                    c.KeyPress += new KeyPressEventHandler(ReadOnly_KeyPress);
                }

                if (c.Text.Length > 5)
                {
                    if (c.GetType() == typeof(TextBox))
                    {
                        TextBox textBox = c as TextBox;
                        textBox.CharacterCasing = CharacterCasing.Normal;
                        //c.Font = new Font(textBox.Font.FontFamily,textBox.Font.Size - 1.5F);
                    }
                }
                c.Cursor = hand_Cursor;
            }

            DPIManager.Register(this);
            DPIManager.Update(this);
        }
        private bool scaled = false;

        public void UpdateSize(float scale)
        {
            if (IsDisposed)
            {
                DPIManager.Unregister(this);
                return;
            }
         
            if (!scaled)
            {
                SuspendLayout();

                this.scale = scale;

                if (scale > 1.0F)
                {
                    float newFontSize = 7.25F * scale;

                    foreach (Control c in shortContainer.Controls)
                    {
                        if (c.GetType() == typeof(Label))
                        {
                            c.Font = new Font(c.Font.FontFamily, Font.Size, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                            if (c.Text != ("+"))
                            {
                                c.Location = new Point(switch1.Left - c.Width, c.Location.Y);
                            }
                        }

                        if (c.GetType() == typeof(ComboBox) || c.GetType() == typeof(TextBox))
                        {
                            c.Font = new Font(c.Font.FontFamily, c.Font.Size * scale, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                           
                        }
                    }

                    foreach (Control c in UINavContainer.Controls)
                    {
                        if (c.GetType() == typeof(ComboBox) || c.GetType() == typeof(TextBox))
                        {
                            c.Font = new Font(c.Font.FontFamily, c.Font.Size * scale, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                            // c.Size = new Size(c.Width, (c.Height + 25) * (int)scale);
                        }

                        if (c.GetType() == typeof(Label))
                        {
                            c.Font = new Font(c.Font.FontFamily, Font.Size, FontStyle.Regular, GraphicsUnit.Pixel, 0);
                            if (c.Text !=("+"))
                            {
                                c.Location = new Point(switch10.Left - c.Width, c.Location.Y);
                            }
                        }

                    }
                }
                scaled = true;
                ResumeLayout();
            }
        }

        private void RefreshTimerTick(Object Object, EventArgs EventArgs)
        {
            if(Visible)
            Invalidate();
        }

        //private int prevPressed;
        private RectangleF top;
        private RectangleF front;
        private void XInputShortcutsSetup_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            float factor = 1.5F;

            //Draw top & front picture
            float topWidth = (controllerTop.Width / factor) * scale;
            float topHeight = (controllerTop.Height / factor) * scale;
            float frontWidth = (controllerFront.Width / factor) * scale;
            float frontHeight = (controllerFront.Height / factor) * scale;

            top = new RectangleF(shortContainer.Right + 40, 25, topWidth, topHeight);
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
            RectangleF LS = new RectangleF((front.X + (56 / factor) * scale) - offset, (front.Y + (43 / factor) * scale) - offset, rec, rec);
            RectangleF A = new RectangleF((front.X + (194 / factor) * scale) - offset, (front.Y + (62 / factor) * scale) - offset, rec, rec);
            RectangleF B = new RectangleF((front.X + (211 / factor) * scale) - offset, (front.Y + (45 / factor) * scale) - offset, rec, rec);
            RectangleF X = new RectangleF((front.X + (175 / factor) * scale) - offset, (front.Y + (45 / factor) * scale) - offset, rec, rec);
            RectangleF Y = new RectangleF((front.X + (194 / factor) * scale) - offset, (front.Y + (26 / factor) * scale) - offset, rec, rec);
            RectangleF Back = new RectangleF((front.X + (100 / factor) * scale) - offset, (front.Y + (45 / factor) * scale) - offset, rec, rec);
            RectangleF Start = new RectangleF((front.X + (150 / factor) * scale) - offset, (front.Y + (45 / factor) * scale) - offset, rec, rec);

            RectangleF D_Up = new RectangleF((front.X + (89.5F / factor) * scale) - offset, (front.Y + (70 / factor) * scale) - offset, rec, rec);
            RectangleF D_Right = new RectangleF((front.X + (104 / factor) * scale) - offset, (front.Y + (85 / factor) * scale) - offset, rec, rec);
            RectangleF D_Down = new RectangleF((front.X + (89.5F / factor) * scale) - offset, (front.Y + (101 / factor) * scale) - offset, rec, rec);
            RectangleF D_Left = new RectangleF((front.X + (76 / factor) * scale) - offset, (front.Y + (85 / factor) * scale) - offset, rec, rec);
            RectangleF LT = new RectangleF((top.X + (59 / factor) * scale) - offset, (top.Y + (32 / factor) * scale) - offset, rec, rec);
            RectangleF RT = new RectangleF((top.X + (187 / factor) * scale) - offset, (top.Y + (32 / factor) * scale) - offset, rec, rec);

            RectangleF toFill = RectangleF.Empty;

            for (int i = 0; i < XController.Controllers.Length; i++)
            {
                {////draw a circle at the pressed button location
                    int pressed = XController.GetPressedButtons(i);
                    int rtValue = XController.GetRightTriggerValue(i);
                    int rlValue = XController.GetLeftTriggerValue(i);
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
                    if(pressed == noButtonPressed && !isRL && !isRT)
                    {
                        continue;
                    }

                    switch (pressed)
                    {
                        case 1024://Guide button
                            {
                                toFill = Guide;
                                break;
                            }
                        case (int)GamepadButtonFlags.RightShoulder:
                            {

                                toFill = RB;
                                break;
                            }
                        case (int)GamepadButtonFlags.LeftShoulder:
                            {
                                toFill = LB;
                                break;
                            }

                        case (int)GamepadButtonFlags.RightThumb:
                            {
                                toFill = RS;
                                break;
                            }
                        case (int)GamepadButtonFlags.LeftThumb:
                            {
                                toFill = LS;
                                break;
                            }
                        case (int)GamepadButtonFlags.A:
                            {
                                toFill = A;
                                break;
                            }
                        case (int)GamepadButtonFlags.B:
                            {
                                toFill = B;
                                break;
                            }
                        case (int)GamepadButtonFlags.X:
                            {
                                toFill = X;
                                break;
                            }
                        case (int)GamepadButtonFlags.Y:
                            {
                                toFill = Y;
                                break;
                            }
                        case (int)GamepadButtonFlags.Back:
                            {
                                toFill = Back;
                                break;
                            }
                        case (int)GamepadButtonFlags.Start:
                            {
                                toFill = Start;
                                break;
                            }
                        case (int)GamepadButtonFlags.DPadUp:
                            {
                                toFill = D_Up;
                                break;
                            }
                        case (int)GamepadButtonFlags.DPadRight:
                            {
                                toFill = D_Right;
                                break;
                            }
                        case (int)GamepadButtonFlags.DPadDown:
                            {
                                toFill = D_Down;
                                break;
                            }
                        case (int)GamepadButtonFlags.DPadLeft:
                            {
                                toFill = D_Left;
                                break;
                            }
                    }

                    if (isRT)
                    {
                        toFill = RT;
                    }
                    else if (isRL)
                    {
                        toFill = LT;
                    }

                    g.ResetClip();

                    g.Clip = new Region(new RectangleF(toFill.X, toFill.Y, toFill.Width, toFill.Height));

                    g.FillEllipse(brush, toFill);

                    //prevPressed = (int)pressed;
                    if (this.ActiveControl != null)
                    {
                        if (ActiveControl.GetType() == typeof(TextBox) && ActiveControl.Name != "deadzone_txt")
                        {
                            activeControl = (TextBox)ActiveControl;
                            if (!isTRigger)
                            {
                                if ((int)pressed == noButtonPressed)
                                {
                                    return;
                                }

                                if (pressed != 1024)//Guide button only have int returned
                                {
                                    activeControl.Text = ((GamepadButtonFlags)pressed).ToString();
                                    activeControl.Tag = pressed;
                                }
                                else
                                {
                                    activeControl.Text = "GUIDE";
                                    activeControl.Tag = pressed;
                                }
                            }
                            else if (isRT)
                            {
                                activeControl.Text = "RightTrigger";
                                activeControl.Tag = 9999;

                            }
                            else if (isRL)
                            {
                                activeControl.Text = "LeftTrigger";
                                activeControl.Tag = 10000;
                            }
                        }
                    }
                }
            }
        }

        //private Point mousePos;
        private void XInputShortcutsSetup_MouseMove(object sender, MouseEventArgs e)
        {
            //mousePos = e.Location;
            //Console.WriteLine(Height - front.Y);
            //Console.WriteLine("X" + (e.X - top.X)+" "+ "Y" + (e.Y - top.Y));//postion in image rectangle
            //Console.WriteLine("X" + (e.X - front.X) + " " + "Y" + (e.Y - front.Y));

        }

        private void Close_Click(object sender, EventArgs e)
        {
            ini.IniWriteValue("XShortcuts", "SetFocus", switch1.Tag.ToString() + "+" + slave1.Tag.ToString());
            ini.IniWriteValue("XShortcuts", "Close", switch2.Tag.ToString() + "+" + slave2.Tag.ToString());
            ini.IniWriteValue("XShortcuts", "Stop", switch3.Tag.ToString() + "+" + slave3.Tag.ToString());
            ini.IniWriteValue("XShortcuts", "TopMost", switch4.Tag.ToString() + "+" + slave4.Tag.ToString());
            ini.IniWriteValue("XShortcuts", "ResetWindows", switch5.Tag.ToString() + "+" + slave5.Tag.ToString());
            ini.IniWriteValue("XShortcuts", "Cutscenes", switch6.Tag.ToString() + "+" + slave6.Tag.ToString());
            ini.IniWriteValue("XShortcuts", "Switch", switch7.Tag.ToString() + "+" + slave7.Tag.ToString());
            ini.IniWriteValue("XShortcuts", "LockInputs", switch8.Tag.ToString() + "+" + slave8.Tag.ToString());
            ini.IniWriteValue("XShortcuts", "ReleaseCursor", switch9.Tag.ToString() + "+" + slave9.Tag.ToString());

            ini.IniWriteValue("XUINav", "Deadzone", deadzone_txt.Text);
            ini.IniWriteValue("XUINav", "DragDrop", dragdrop_txt.Tag.ToString());
            ini.IniWriteValue("XUINav", "RightClick", rclick_txt.Tag.ToString());
            ini.IniWriteValue("XUINav", "LeftClick", lclick_txt.Tag.ToString());
            ini.IniWriteValue("XUINav", "LockUIControl", switch10.Tag.ToString() + "+" + slave10.Tag.ToString());
            ini.IniWriteValue("XUINav", "OpenOsk", switch11.Tag.ToString() + "+" + slave11.Tag.ToString());
            ControllersShortcuts.UpdateShortcutsValue();
            ControllersUINav.UpdateUINavSettings();

            Visible = false;
        }

        private void num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void ReadOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }        private void enabled_chk_CheckedChanged(object sender, EventArgs e)
        {      
            ini.IniWriteValue("XUINav", "Enabled", enabled_chk.Checked.ToString());
            ControllersUINav.UpdateUINavSettings();
        }

        private void deadzone_txt_MouseEnter(object sender, EventArgs e)
        {
            deadzone_tip.Visible = true;
        }

        private void deadzone_txt_MouseLeave(object sender, EventArgs e)
        {
            deadzone_tip.Visible = false;
        }
    }
}
