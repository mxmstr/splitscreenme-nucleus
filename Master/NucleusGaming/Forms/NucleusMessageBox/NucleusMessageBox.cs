
namespace Nucleus.Gaming.Forms.NucleusMessageBox
{
    public static class NucleusMessageBox
    {
        public static void Show(string title ,string message)
        {
            CustomMessageBox messageBox = new CustomMessageBox(title, message);
            messageBox.ShowDialog();
        }
    }
}
