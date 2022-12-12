using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Coop
{

    public static class InputIcons 
    {
        private static MainForm main;
        private static PictureBox icon;

        public static void InputIcon(MainForm mainform,string name,Size size, Bitmap image)
        {
            icon = new PictureBox(); 
            main = mainform;

            icon.Name = name;
            icon.Size = size;
            icon.SizeMode = PictureBoxSizeMode.StretchImage;
            icon.Image = image;
            icon.MouseEnter += inputIcons_MouseEnter;
            icon.MouseLeave += inputIcons_MouseLeave;
            main.icons_Container.Controls.Add(icon);
        }

        private static void inputIcons_MouseEnter(object sender, EventArgs e)
        {
            PictureBox inputIcon = (PictureBox)sender;

            if (inputIcon.Name == "icon1")
            {
                main.inputsIconsDesc.Text  = "Supports xinput gamepads (e.g., X360)";
            }
            else if (inputIcon.Name == "icon2" || inputIcon.Name == "icon3")
            {
                main.inputsIconsDesc.Text = "Supports dinput gamepads (e.g., Ps3)";
            }
            else if (icon.Name == "icon4")
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
