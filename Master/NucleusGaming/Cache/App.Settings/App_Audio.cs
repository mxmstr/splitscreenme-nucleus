using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleus.Gaming.App.Settings
{
    public static class App_Audio
    {
        public static  Dictionary<string,string> Instances_AudioOutput = new Dictionary<string,string>();

        private static string custom;
        public static string Custom 
        { 
            get => custom; 
            set 
            { 
                custom = value;
                Globals.ini.IniWriteValue("Audio", "Custom", value);
            } 
        }

        public static void SaveAudioOutput(string key, string value)
        {
            Globals.ini.IniWriteValue("Audio", key, value); 
            Instances_AudioOutput[key] = value;
        }

        public static bool LoadSettings()
        {
            try
            {
                for(int i = 0; i < 8; i++)
                {
                    Instances_AudioOutput.Add("AudioInstance" + (i + 1), Globals.ini.IniReadValue("Audio", "AudioInstance"+ (i + 1)));
                }

                custom = Globals.ini.IniReadValue("Audio", "Custom");
            }
            catch (Exception ex)
            {
                // Handle the exception, e.g., log it, show a message box, etc.
                return false;
            }

            return true;
        }

    }
}
