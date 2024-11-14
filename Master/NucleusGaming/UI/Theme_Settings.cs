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
                Bitmap bmp = new Bitmap(ThemeFolder + "cursor.ico");

                bmp = new Bitmap(bmp, new Size(Cursor.Current.Size.Width, Cursor.Current.Size.Height));
                default_Cursor = new Cursor(bmp.GetHicon());
                bmp.Dispose();
            }

            return default_Cursor;
        }

        public static Cursor Hand_Cursor => GetCursorHand();
        private static Cursor hand_Cursor;     

        private static Cursor GetCursorHand()
        {
            if (hand_Cursor == null)
            {
                Bitmap bmp = new Bitmap(ThemeFolder + "cursor_hand.ico");
                bmp = new Bitmap(bmp, new Size(Cursor.Current.Size.Width, Cursor.Current.Size.Height));
                hand_Cursor = new Cursor(bmp.GetHicon());
                bmp.Dispose();
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

        public static Color MainButtonFrameBackColor => GetMainButtonFrameBackColor();
        private static string[] mainButtonFrameBackColor = null;

        private static Color GetMainButtonFrameBackColor()
        {
            if (mainButtonFrameBackColor == null)
            {
                mainButtonFrameBackColor = ThemeConfigFile.IniReadValue("Colors", "HandlerNoteBackground").Split(',');
                return Color.FromArgb(int.Parse(mainButtonFrameBackColor[0]), int.Parse(mainButtonFrameBackColor[1]), int.Parse(mainButtonFrameBackColor[2]), int.Parse(mainButtonFrameBackColor[3]));
            }

            return Color.FromArgb(int.Parse(mainButtonFrameBackColor[0]), int.Parse(mainButtonFrameBackColor[1]), int.Parse(mainButtonFrameBackColor[2]), int.Parse(mainButtonFrameBackColor[3]));
        }

        public static Color RightFrameBackColor => GetRightFrameBackColor();
        private static string[] rightFrameBackColor = null;

        private static Color GetRightFrameBackColor()
        {
            if (rightFrameBackColor == null)
            {
                rightFrameBackColor = ThemeConfigFile.IniReadValue("Colors", "RightFrameBackground").Split(',');
                return Color.FromArgb(int.Parse(rightFrameBackColor[0]), int.Parse(rightFrameBackColor[1]), int.Parse(rightFrameBackColor[2]), int.Parse(rightFrameBackColor[3]));
            }

            return Color.FromArgb(int.Parse(rightFrameBackColor[0]), int.Parse(rightFrameBackColor[1]), int.Parse(rightFrameBackColor[2]), int.Parse(rightFrameBackColor[3]));
        }

        public static Color GameListBackColor => GetGameListBackColor();
        private static string[] gameListBackColor = null;

        private static Color GetGameListBackColor()
        {
            if (gameListBackColor == null)
            {
                gameListBackColor = ThemeConfigFile.IniReadValue("Colors", "GameListBackground").Split(',');
                return Color.FromArgb(int.Parse(gameListBackColor[0]), int.Parse(gameListBackColor[1]), int.Parse(gameListBackColor[2]), int.Parse(gameListBackColor[3]));
            }

            return Color.FromArgb(int.Parse(gameListBackColor[0]), int.Parse(gameListBackColor[1]), int.Parse(gameListBackColor[2]), int.Parse(gameListBackColor[3]));
        }

        public static Color SetupScreenBackColor => GetSetupScreenBackColor();
        private static string[] setupScreentBackColor = null;

        private static Color GetSetupScreenBackColor()
        {
            if (setupScreentBackColor == null)
            {
                setupScreentBackColor = ThemeConfigFile.IniReadValue("Colors", "SetupScreenBackground").Split(',');
                return Color.FromArgb(int.Parse(setupScreentBackColor[0]), int.Parse(setupScreentBackColor[1]), int.Parse(setupScreentBackColor[2]), int.Parse(setupScreentBackColor[3]));
            }

            return Color.FromArgb(int.Parse(setupScreentBackColor[0]), int.Parse(setupScreentBackColor[1]), int.Parse(setupScreentBackColor[2]), int.Parse(setupScreentBackColor[3]));
        }

        public static Color BackgroundGradientColor => GetBackgroundGradientColor();
        private static string[] backgroundGradientColor = null;

        private static Color GetBackgroundGradientColor()
        {
            if (backgroundGradientColor == null)
            {
                backgroundGradientColor = ThemeConfigFile.IniReadValue("Colors", "BackgroundGradient").Split(',');
                return Color.FromArgb(int.Parse(backgroundGradientColor[0]), int.Parse(backgroundGradientColor[1]), int.Parse(backgroundGradientColor[2]), int.Parse(backgroundGradientColor[3]));
            }

            return Color.FromArgb(int.Parse(backgroundGradientColor[0]), int.Parse(backgroundGradientColor[1]), int.Parse(backgroundGradientColor[2]), int.Parse(backgroundGradientColor[3]));
        }

        public static Color HandlerNoteBackColor => GetHandlerNoteBackColor();
        private static string[] handlerNoteBackColor = null;

        private static Color GetHandlerNoteBackColor()
        {
            if (handlerNoteBackColor == null)
            {
                handlerNoteBackColor = ThemeConfigFile.IniReadValue("Colors", "HandlerNoteBackground").Split(',');
                return Color.FromArgb(int.Parse(handlerNoteBackColor[0]), int.Parse(handlerNoteBackColor[1]), int.Parse(handlerNoteBackColor[2]), int.Parse(handlerNoteBackColor[3]));
            }

            return Color.FromArgb(int.Parse(handlerNoteBackColor[0]), int.Parse(handlerNoteBackColor[1]), int.Parse(handlerNoteBackColor[2]), int.Parse(handlerNoteBackColor[3]));
        }

        public static Color ButtonsBackColor => GetButtonsBackColor();
        private static string[] buttonsBackColor = null;

        private static Color GetButtonsBackColor()
        {
            if (buttonsBackColor == null)
            {
                buttonsBackColor = ThemeConfigFile.IniReadValue("Colors", "ButtonsBackground").Split(',');
                return Color.FromArgb(int.Parse(buttonsBackColor[0]), int.Parse(buttonsBackColor[1]), int.Parse(buttonsBackColor[2]), int.Parse(buttonsBackColor[3]));
            }

            return Color.FromArgb(int.Parse(buttonsBackColor[0]), int.Parse(buttonsBackColor[1]), int.Parse(buttonsBackColor[2]), int.Parse(buttonsBackColor[3]));
        }
    }
}
