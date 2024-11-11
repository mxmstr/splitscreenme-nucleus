using Nucleus.Coop.Controls;
using Nucleus.Gaming;
using Nucleus.Gaming.Cache;
using Nucleus.Gaming.Controls;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Coop
{
    public static class InputIcons
    {
        private static Size iconsSize;

        public static Control[] SetInputsIcons(GenericGameInfo game)
        {
            MainForm mainForm = MainForm.Instance;

            List<PictureBox> icons = new List<PictureBox>();

            if(iconsSize == Size.Empty)
            {
               iconsSize = new Size(mainForm.icons_Container.Height + 5, mainForm.icons_Container.Height - 2);
            }
       
            if ((game.Hook.XInputEnabled && !game.Hook.XInputReroute && !game.ProtoInput.DinputDeviceHook) || game.ProtoInput.XinputHook)
            {
                PictureBox icon = new PictureBox
                {
                    Name = "icon1",
                    Size = iconsSize,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = ImageCache.GetImage(Globals.ThemeFolder + "xinput_icon.png")
                };

                CustomToolTips.SetToolTip(icon, "Supports xinput gamepads (e.g. X360).", "icon1", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
                icons.Add(icon);
            }

            if ((game.Hook.DInputEnabled || game.Hook.XInputReroute || game.ProtoInput.DinputDeviceHook) && (game.Hook.XInputEnabled || game.ProtoInput.XinputHook))
            {
                PictureBox icon = new PictureBox
                {
                    Name = "icon2",
                    Size = iconsSize,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = ImageCache.GetImage(Globals.ThemeFolder + "dinput_icon.png")
                };


                CustomToolTips.SetToolTip(icon, "Supports dinput gamepads (e.g. Ps3).", "icon2", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
                icons.Add(icon);
            }
            else if ((game.Hook.DInputEnabled || game.Hook.XInputReroute || game.ProtoInput.DinputDeviceHook) && (!game.Hook.XInputEnabled || !game.ProtoInput.XinputHook))
            {
                PictureBox icon = new PictureBox
                {
                    Name = "icon3",
                    Size = iconsSize,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = ImageCache.GetImage(Globals.ThemeFolder + "dinput_icon.png")
                };

                CustomToolTips.SetToolTip(icon, "Supports dinput gamepads (e.g. Ps3).", "icon3", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
                icons.Add(icon);
            }

            if (game.SupportsKeyboard)
            {
                PictureBox icon = new PictureBox
                {
                    Name = "icon4",
                    Size = iconsSize,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = ImageCache.GetImage(Globals.ThemeFolder + "keyboard_icon.png")
                };

                CustomToolTips.SetToolTip(icon, @"Supports 1 keyboard\mouse.", "icon4", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
                icons.Add(icon);
            }

            if (game.SupportsMultipleKeyboardsAndMice) //Raw mice/keyboards
            {
                PictureBox iconKB1 = new PictureBox
                {
                    Name = "icon5",
                    Size = iconsSize,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = ImageCache.GetImage(Globals.ThemeFolder + "keyboard_icon.png")
                };

                PictureBox iconKB2 = new PictureBox
                {
                    Name = "icon6",
                    Size = iconsSize,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Image = ImageCache.GetImage(Globals.ThemeFolder + "keyboard_icon.png")
                };


                CustomToolTips.SetToolTip(iconKB1, @"Supports multiple keyboards/mice.", "iconKB1", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
                icons.Add(iconKB1);

                CustomToolTips.SetToolTip(iconKB2, @"Supports multiple keyboards/mice.", "iconKB2", new int[] { 190, 0, 0, 0 }, new int[] { 255, 255, 255, 255 });
                icons.Add(iconKB2);
            }

            return icons.ToArray();
        }

    }
}
