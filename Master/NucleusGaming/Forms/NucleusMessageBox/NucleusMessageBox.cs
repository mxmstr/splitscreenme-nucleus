﻿
namespace Nucleus.Gaming.Forms
{
    public static class NucleusMessageBox
    {
        public static void Show(string title, string message, bool formating)
        {
            CustomMessageBox messageBox = new CustomMessageBox(title, message, formating);
            messageBox.ShowDialog();
        }
    }
}
