using Nucleus.Gaming;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Coop
{

    public static class InputIcons
    {
        private static MainForm main;

        public static void SetInputsIcons(MainForm mainform, GenericGameInfo game)
        {
            main = mainform;
            main.icons_Container.Visible = false;
            main.icons_Container.Controls.Clear();
            
           Size iconsSize = new Size(main.icons_Container.Height + 6, main.icons_Container.Height - 2);
            if ((game.Hook.XInputEnabled && !game.Hook.XInputReroute && !game.ProtoInput.DinputDeviceHook) || game.ProtoInput.XinputHook)
            {
                PictureBox icon = new PictureBox
                {
                    Name = "icon1",
                    Size = iconsSize,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = new Bitmap(Globals.Theme + "xinput_icon.png")
                };

                icon.MouseEnter += inputIcons_MouseEnter;
                icon.MouseLeave += inputIcons_MouseLeave;
                main.icons_Container.Controls.Add(icon);

            }

            if ((game.Hook.DInputEnabled || game.Hook.XInputReroute || game.ProtoInput.DinputDeviceHook) && (game.Hook.XInputEnabled || game.ProtoInput.XinputHook))
            {
                PictureBox icon = new PictureBox
                {
                    Name = "icon2",
                    Size = iconsSize,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = new Bitmap(Globals.Theme + "dinput_icon.png")
                };

                icon.MouseEnter += inputIcons_MouseEnter;
                icon.MouseLeave += inputIcons_MouseLeave;
                main.icons_Container.Controls.Add(icon);
            }
            else if ((game.Hook.DInputEnabled || game.Hook.XInputReroute || game.ProtoInput.DinputDeviceHook) && (!game.Hook.XInputEnabled || !game.ProtoInput.XinputHook))
            {
                PictureBox icon = new PictureBox
                {
                    Name = "icon3",
                    Size = iconsSize,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = new Bitmap(Globals.Theme + "dinput_icon.png")
                };

                icon.MouseEnter += inputIcons_MouseEnter;
                icon.MouseLeave += inputIcons_MouseLeave;
                main.icons_Container.Controls.Add(icon);
            }

            if (game.SupportsKeyboard)
            {
                PictureBox icon = new PictureBox
                {
                    Name = "icon4",
                    Size = iconsSize,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = new Bitmap(Globals.Theme + "keyboard_icon.png")
                };

                icon.MouseEnter += inputIcons_MouseEnter;
                icon.MouseLeave += inputIcons_MouseLeave;
                main.icons_Container.Controls.Add(icon);
            }

            if (game.SupportsMultipleKeyboardsAndMice) //Raw mice/keyboards
            {
                PictureBox iconKB1 = new PictureBox
                {
                    Name = "icon5",
                    Size = iconsSize,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = new Bitmap(Globals.Theme + "keyboard_icon.png")
                };

                PictureBox iconKB2 = new PictureBox
                {
                    Name = "icon6",
                    Size = iconsSize,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = new Bitmap(Globals.Theme + "keyboard_icon.png")
                };

                iconKB1.MouseEnter += inputIcons_MouseEnter;
                iconKB1.MouseLeave += inputIcons_MouseLeave;
                main.icons_Container.Controls.Add(iconKB1);

                iconKB2.MouseEnter += inputIcons_MouseEnter;
                iconKB2.MouseLeave += inputIcons_MouseLeave;
                main.icons_Container.Controls.Add(iconKB2);
            }

            main.icons_Container.Visible = true;
        }


        private static void inputIcons_MouseEnter(object sender, EventArgs e)
        {
            PictureBox inputIcon = (PictureBox)sender;

            if (inputIcon.Name == "icon1")
            {
                main.inputsIconsDesc.Text = "Supports xinput gamepads (e.g., X360)";
            }
            else if (inputIcon.Name == "icon2" || inputIcon.Name == "icon3")
            {
                main.inputsIconsDesc.Text = "Supports dinput gamepads (e.g., Ps3)";
            }
            else if (inputIcon.Name == "icon4")
            {
                main.inputsIconsDesc.Text = @"Supports 1 keyboard\mouse";
            }
            else if (inputIcon.Name == "icon5" || inputIcon.Name == "icon6")
            {
                main.inputsIconsDesc.Text = @"Supports multiple keyboards/mice";
            }

            main.inputsIconsDesc.Location = new Point((int)main.icons_Container.Left - 5, main.icons_Container.Bottom);
            main.inputsIconsDesc.BringToFront();
            main.inputsIconsDesc.Visible = true;
        }

        private static void inputIcons_MouseLeave(object sender, EventArgs e)
        {
            main.inputsIconsDesc.Visible = false;
            main.inputsIconsDesc.Text = "";
        }

    }
}
