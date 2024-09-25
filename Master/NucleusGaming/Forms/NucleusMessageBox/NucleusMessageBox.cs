
using System;

namespace Nucleus.Gaming.Forms
{
    public static class NucleusMessageBox
    {
        public static CustomMessageBox MessageBox;

        public static void Show(string title, string message, bool formating)
        {
            if(MessageBox != null)
            {
                MessageBox.BringToFront();
                return;
            }

            CustomMessageBox messageBox = new CustomMessageBox(title, message, formating);
            messageBox.Disposed += MessageBox_Disposed;
            messageBox.Show();
            MessageBox = messageBox;
        }

        private static void MessageBox_Disposed(object sender, EventArgs e)
        {
            MessageBox = null;
        }
    }
}
