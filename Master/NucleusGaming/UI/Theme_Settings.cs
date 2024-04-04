using System.Drawing;
using System.Windows.Forms;

namespace Nucleus.Gaming.UI
{
    public static class Theme_Settings
    {
        public static string ThemeFolder => Globals.ThemeFolder;
        public static IniFile ThemeConfigFile => Globals.ThemeConfigFile;

        public static Cursor Default_Cursor => GetCursorDefaut();
        private static Cursor default_Cursor;   
        
        private static Cursor GetCursorDefaut()
        {
            if(default_Cursor == null)
            {
                default_Cursor = new Cursor(ThemeFolder + "cursor.ico");               
            }

            return default_Cursor;
        }

        public static Cursor Hand_Cursor => GetCursorHand();
        private static Cursor hand_Cursor;     

        private static Cursor GetCursorHand()
        {
            if (hand_Cursor == null)
            {
                hand_Cursor = new Cursor(ThemeFolder + "cursor_hand.ico");
            }

            return hand_Cursor;
        }

        public static Color SelectedBackColor => GetSelectedBackColor();
        private static string[] selectedBackColor = null;

        private static Color GetSelectedBackColor()
        {
            if (selectedBackColor == null)
            {
                selectedBackColor = ThemeConfigFile.IniReadValue("Colors", "Selection").Split(',');
                return Color.FromArgb(int.Parse(selectedBackColor[0]), int.Parse(selectedBackColor[1]), int.Parse(selectedBackColor[2]), int.Parse(selectedBackColor[3]));
            }

            return Color.FromArgb(int.Parse(selectedBackColor[0]), int.Parse(selectedBackColor[1]), int.Parse(selectedBackColor[2]), int.Parse(selectedBackColor[3]));
        }

    }
}
