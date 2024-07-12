using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.App.Settings
{
    public static class Settings_Loader
    {
        public static bool InitializeSettings()
        {
            try
            {
                App_Misc.LoadSettings();
                App_Hotkeys.LoadSettings();
                App_GamePadShortcuts.LoadSettings();
                App_GamePadNavigation.LoadSettings();
                App_Layouts.LoadSettings();                
                App_Audio.LoadSettings();
            }
            catch (Exception ex )
            {
                //Add message box here.
                return false;
            }

            return true;
        }



    }
}
